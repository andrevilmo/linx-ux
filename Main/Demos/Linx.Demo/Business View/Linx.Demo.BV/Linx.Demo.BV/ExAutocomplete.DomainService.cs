					
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

namespace Linx.Demo.BV.ExAutocomplete
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TBNMCOMPLETO.id_NomeCompleto", IsUpdatable=false, EdmName="Linx.Demo.BM.BMDTesteFrame")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[Tbnmcompleto];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[TBNMCOMPLETO];EntityRelations[TBNMMEIO(TBNMMEIO)#TBNOME(TBNOME)#TBSOBRENM(TBSOBRENM)#CLIENTE(CLIENTE)#ESTADO(ESTADO)#PAIS(PAIS)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "Tbnmcompleto")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Demo.BV.ExAutocomplete.Tbnmcompleto")]
	public partial class Tbnmcompleto : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For IdCliente
	    partial void OnIdClienteChanging(int value);
	    partial void OnIdClienteChanged();

	    private int _IdCliente;

	    [DataMember(IsRequired = true, Name = "IdCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id__Cliente", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpCliente];LookUpTitle[Seleção de (Id__Cliente)];LookUpQuery[executeLookUpCliente];LookUpFinalize[finalizeLookUpCliente];LookUpDisplayColumns[{\"IdCliente\" : \"Id Cliente\", \"IdCliente2\" : \"Id Cliente\"}];LookUpColumns[{\"IdCliente\" : true, \"IdCliente2\" : true}];FilterDataKey[TBNMCOMPLETO.CLIENTE.ID_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="int#IdCliente#true##10:0##Id Cliente#0#true##::LookUpCliente##false#false#CLIENTE#CLIENTE#Linx.Demo.BV.ExAutocomplete#IQueryable###true#false", EdmKey="TBNMCOMPLETO.CLIENTE.ID_CLIENTE")]
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
	    //Extensibility Partial Method Definitions For IdNome
	    partial void OnIdNomeChanging(System.Nullable<int> value);
	    partial void OnIdNomeChanged();

	    private System.Nullable<int> _IdNome;

	    [DataMember(Name = "IdNome", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Nome", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTbnome];LookUpTitle[Seleção de (Id Nome)];LookUpQuery[executeLookUpTbnome];LookUpFinalize[finalizeLookUpTbnome];LookUpDisplayColumns[{\"IdNome\" : \"Id Nome\", \"Nome\" : \"Nome\"}];LookUpColumns[{\"IdNome\" : true, \"Nome\" : true}];FilterDataKey[TBNMCOMPLETO.TBNOME.id_nome];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<int>#IdNome#true##10:0##Id Nome#0#true##::LookUpTbnome##false#false#TBNOME#TBNOME#Linx.Demo.BV.ExAutocomplete#IQueryable###true#false", EdmKey="TBNMCOMPLETO.TBNOME.id_nome")]
	    public System.Nullable<int> IdNome
	    {
	    	    get
	    	    {
	    	          return _IdNome;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdNome != value)
	    	          {
	    	              this.ValidateProperty("IdNome", value);
	    	              this.OnIdNomeChanging(value);
	    	              this.RaiseDataMemberChanging("IdNome");
	    	              this._IdNome = value;
	    	              this.RaiseDataMemberChanged("IdNome");
	    	              this.OnIdNomeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For idNomeCompleto
	    partial void OnidNomeCompletoChanging(int value);
	    partial void OnidNomeCompletoChanged();

	    private int _idNomeCompleto;

	    [DataMember(IsRequired = true, Name = "idNomeCompleto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "id Nome Completo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBNMCOMPLETO.id_NomeCompleto];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBNMCOMPLETO.id_NomeCompleto")]
	    public int idNomeCompleto
	    {
	    	    get
	    	    {
	    	          return _idNomeCompleto;
	    	    }
	    	    set
	    	    {
	    	          if (this._idNomeCompleto != value)
	    	          {
	    	              this.ValidateProperty("idNomeCompleto", value);
	    	              this.OnidNomeCompletoChanging(value);
	    	              this.RaiseDataMemberChanging("idNomeCompleto");
	    	              this._idNomeCompleto = value;
	    	              this.RaiseDataMemberChanged("idNomeCompleto");
	    	              this.OnidNomeCompletoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For idnomeMeio
	    partial void OnidnomeMeioChanging(int value);
	    partial void OnidnomeMeioChanged();

	    private int _idnomeMeio;

	    [DataMember(IsRequired = true, Name = "idnomeMeio", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "id nomeMeio", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTbnmmeio];LookUpTitle[Seleção de (id nomeMeio)];LookUpQuery[executeLookUpTbnmmeio];LookUpFinalize[finalizeLookUpTbnmmeio];LookUpDisplayColumns[{\"idnomeMeio\" : \"id nomeMeio\", \"Nomedomeio\" : \"Nomedomeio\"}];LookUpColumns[{\"idnomeMeio\" : true, \"Nomedomeio\" : true}];FilterDataKey[TBNMCOMPLETO.TBNMMEIO.id_nomeMeio];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="int#idnomeMeio#true##10:0##id nomeMeio#0#true##::LookUpTbnmmeio##false#false#TBNMMEIO#TBNMMEIO#Linx.Demo.BV.ExAutocomplete#IQueryable###true#false", EdmKey="TBNMCOMPLETO.TBNMMEIO.id_nomeMeio")]
	    public int idnomeMeio
	    {
	    	    get
	    	    {
	    	          return _idnomeMeio;
	    	    }
	    	    set
	    	    {
	    	          if (this._idnomeMeio != value)
	    	          {
	    	              this.ValidateProperty("idnomeMeio", value);
	    	              this.OnidnomeMeioChanging(value);
	    	              this.RaiseDataMemberChanging("idnomeMeio");
	    	              this._idnomeMeio = value;
	    	              this.RaiseDataMemberChanged("idnomeMeio");
	    	              this.OnidnomeMeioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdSobrenome
	    partial void OnIdSobrenomeChanging(int value);
	    partial void OnIdSobrenomeChanged();

	    private int _IdSobrenome;

	    [DataMember(IsRequired = true, Name = "IdSobrenome", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Sobrenome", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTbsobrenm];LookUpTitle[Seleção de (Id Sobrenome)];LookUpQuery[executeLookUpTbsobrenm];LookUpFinalize[finalizeLookUpTbsobrenm];LookUpDisplayColumns[{\"IdSobrenome\" : \"Id Sobrenome\", \"SobreNome\" : \"SobreNome\"}];LookUpColumns[{\"IdSobrenome\" : true, \"SobreNome\" : true}];FilterDataKey[TBNMCOMPLETO.TBSOBRENM.id_sobrenome];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="int#IdSobrenome#true##10:0##Id Sobrenome#0#true##::LookUpTbsobrenm##false#false#TBSOBRENM#TBSOBRENM#Linx.Demo.BV.ExAutocomplete#IQueryable###true#false", EdmKey="TBNMCOMPLETO.TBSOBRENM.id_sobrenome")]
	    public int IdSobrenome
	    {
	    	    get
	    	    {
	    	          return _IdSobrenome;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdSobrenome != value)
	    	          {
	    	              this.ValidateProperty("IdSobrenome", value);
	    	              this.OnIdSobrenomeChanging(value);
	    	              this.RaiseDataMemberChanging("IdSobrenome");
	    	              this._IdSobrenome = value;
	    	              this.RaiseDataMemberChanged("IdSobrenome");
	    	              this.OnIdSobrenomeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Nome
	    partial void OnNomeChanging(string value);
	    partial void OnNomeChanged();

	    private string _Nome;

	    [DataMember(Name = "Nome", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(100)]
	    [FunctionalPoint("Precision[100:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTbnome];LookUpTitle[Seleção de (Nome)];LookUpQuery[executeLookUpTbnome];LookUpFinalize[finalizeLookUpTbnome];LookUpDisplayColumns[{\"IdNome\" : \"Id Nome\", \"Nome\" : \"Nome\"}];LookUpColumns[{\"IdNome\" : true, \"Nome\" : true}];FilterDataKey[TBNMCOMPLETO.TBNOME.Nome];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#Nome#false##100:0##Nome#1#true##::LookUpTbnome##false#false#TBNOME#TBNOME#Linx.Demo.BV.ExAutocomplete#IQueryable###true#false", EdmKey="TBNMCOMPLETO.TBNOME.Nome")]
	    public string Nome
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
	    //Extensibility Partial Method Definitions For NomeCompleto
	    partial void OnNomeCompletoChanging(string value);
	    partial void OnNomeCompletoChanged();

	    private string _NomeCompleto;

	    [DataMember(Name = "NomeCompleto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "NomeCompleto", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(100)]
	    [FunctionalPoint("Precision[100:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBNMCOMPLETO.NomeCompleto];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBNMCOMPLETO.NomeCompleto")]
	    public string NomeCompleto
	    {
	    	    get
	    	    {
	    	          return _NomeCompleto;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeCompleto != value)
	    	          {
	    	              this.ValidateProperty("NomeCompleto", value);
	    	              this.OnNomeCompletoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeCompleto");
	    	              this._NomeCompleto = value;
	    	              this.RaiseDataMemberChanged("NomeCompleto");
	    	              this.OnNomeCompletoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Nomedomeio
	    partial void OnNomedomeioChanging(string value);
	    partial void OnNomedomeioChanged();

	    private string _Nomedomeio;

	    [DataMember(IsRequired = true, Name = "Nomedomeio", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome do Meio", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(100)]
	    [FunctionalPoint("Precision[100:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTbnmmeio];LookUpTitle[Seleção de (Nome do Meio)];LookUpQuery[executeLookUpTbnmmeio];LookUpFinalize[finalizeLookUpTbnmmeio];LookUpDisplayColumns[{\"idnomeMeio\" : \"id nomeMeio\", \"Nomedomeio\" : \"Nomedomeio\"}];LookUpColumns[{\"idnomeMeio\" : true, \"Nomedomeio\" : true}];FilterDataKey[TBNMCOMPLETO.TBNMMEIO.Nomedomeio];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#Nomedomeio#false##100:0##Nomedomeio#1#true##::LookUpTbnmmeio##false#false#TBNMMEIO#TBNMMEIO#Linx.Demo.BV.ExAutocomplete#IQueryable###true#false", EdmKey="TBNMCOMPLETO.TBNMMEIO.Nomedomeio")]
	    public string Nomedomeio
	    {
	    	    get
	    	    {
	    	          return _Nomedomeio;
	    	    }
	    	    set
	    	    {
	    	          if (this._Nomedomeio != value)
	    	          {
	    	              this.ValidateProperty("Nomedomeio", value);
	    	              this.OnNomedomeioChanging(value);
	    	              this.RaiseDataMemberChanging("Nomedomeio");
	    	              this._Nomedomeio = value;
	    	              this.RaiseDataMemberChanged("Nomedomeio");
	    	              this.OnNomedomeioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For SobreNome
	    partial void OnSobreNomeChanging(string value);
	    partial void OnSobreNomeChanged();

	    private string _SobreNome;

	    [DataMember(IsRequired = true, Name = "SobreNome", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Sobre Nome", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(100)]
	    [FunctionalPoint("Precision[100:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTbsobrenm];LookUpTitle[Seleção de (Sobre Nome)];LookUpQuery[executeLookUpTbsobrenm];LookUpFinalize[finalizeLookUpTbsobrenm];LookUpDisplayColumns[{\"IdSobrenome\" : \"Id Sobrenome\", \"SobreNome\" : \"SobreNome\"}];LookUpColumns[{\"IdSobrenome\" : true, \"SobreNome\" : true}];FilterDataKey[TBNMCOMPLETO.TBSOBRENM.SobreNome];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#SobreNome#false##100:0##SobreNome#1#true##::LookUpTbsobrenm##false#false#TBSOBRENM#TBSOBRENM#Linx.Demo.BV.ExAutocomplete#IQueryable###true#false", EdmKey="TBNMCOMPLETO.TBSOBRENM.SobreNome")]
	    public string SobreNome
	    {
	    	    get
	    	    {
	    	          return _SobreNome;
	    	    }
	    	    set
	    	    {
	    	          if (this._SobreNome != value)
	    	          {
	    	              this.ValidateProperty("SobreNome", value);
	    	              this.OnSobreNomeChanging(value);
	    	              this.RaiseDataMemberChanging("SobreNome");
	    	              this._SobreNome = value;
	    	              this.RaiseDataMemberChanged("SobreNome");
	    	              this.OnSobreNomeChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BMDTesteFrame.TBNMCOMPLETO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Demo.BM.TBNMCOMPLETO), QualifiedEntitySetName = "BMDTesteFrame.TBNMCOMPLETO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TBNMCOMPLETO.NomeCompleto", Source = "NomeCompleto", Target = "NomeCompleto", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.TBNMCOMPLETO", RelationPropertyName = "TBNMCOMPLETO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TBNMCOMPLETO.TBNOME.id_nome", Source = "IdNome", Target = "id_nome", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BMDTesteFrame.TBNOME", RelationPropertyName = "TBNOME" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TBNMCOMPLETO.id_NomeCompleto", Source = "idNomeCompleto", Target = "id_NomeCompleto", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.TBNMCOMPLETO", RelationPropertyName = "TBNMCOMPLETO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TBNMCOMPLETO.CLIENTE.ID_CLIENTE", Source = "IdCliente", Target = "ID_CLIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BMDTesteFrame.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TBNMCOMPLETO.TBNMMEIO.id_nomeMeio", Source = "idnomeMeio", Target = "id_nomeMeio", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BMDTesteFrame.TBNMMEIO", RelationPropertyName = "TBNMMEIO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TBNMCOMPLETO.TBSOBRENM.id_sobrenome", Source = "IdSobrenome", Target = "id_sobrenome", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BMDTesteFrame.TBSOBRENM", RelationPropertyName = "TBSOBRENM" });

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

		

	[LinxPublicationView(PrimaryKeys="TesteCkb.id_qualquer", IsUpdatable=false, EdmName="Linx.Demo.BM.BMDTesteFrame")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TesteCkbView];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[TesteCkb];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TesteCkbView")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Demo.BV.ExAutocomplete.TesteCkbView")]
	public partial class TesteCkbView : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For IdQualquer
	    partial void OnIdQualquerChanging(int value);
	    partial void OnIdQualquerChanged();

	    private int _IdQualquer;

	    [DataMember(IsRequired = true, Name = "IdQualquer", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Qualquer", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TesteCkb.id_qualquer];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TesteCkb.id_qualquer")]
	    public int IdQualquer
	    {
	    	    get
	    	    {
	    	          return _IdQualquer;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdQualquer != value)
	    	          {
	    	              this.ValidateProperty("IdQualquer", value);
	    	              this.OnIdQualquerChanging(value);
	    	              this.RaiseDataMemberChanging("IdQualquer");
	    	              this._IdQualquer = value;
	    	              this.RaiseDataMemberChanged("IdQualquer");
	    	              this.OnIdQualquerChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NaoObrigatorio
	    partial void OnNaoObrigatorioChanging(System.Nullable<bool> value);
	    partial void OnNaoObrigatorioChanged();

	    private System.Nullable<bool> _NaoObrigatorio;

	    [DataMember(Name = "NaoObrigatorio", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "NaoObrigatorio", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TesteCkb.NaoObrigatorio];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TesteCkb.NaoObrigatorio")]
	    public System.Nullable<bool> NaoObrigatorio
	    {
	    	    get
	    	    {
	    	          return _NaoObrigatorio;
	    	    }
	    	    set
	    	    {
	    	          if (this._NaoObrigatorio != value)
	    	          {
	    	              this.ValidateProperty("NaoObrigatorio", value);
	    	              this.OnNaoObrigatorioChanging(value);
	    	              this.RaiseDataMemberChanging("NaoObrigatorio");
	    	              this._NaoObrigatorio = value;
	    	              this.RaiseDataMemberChanged("NaoObrigatorio");
	    	              this.OnNaoObrigatorioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Obrigatorio
	    partial void OnObrigatorioChanging(bool value);
	    partial void OnObrigatorioChanged();

	    private bool _Obrigatorio;

	    [DataMember(IsRequired = true, Name = "Obrigatorio", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Obrigatorio", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TesteCkb.Obrigatorio];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TesteCkb.Obrigatorio")]
	    public bool Obrigatorio
	    {
	    	    get
	    	    {
	    	          return _Obrigatorio;
	    	    }
	    	    set
	    	    {
	    	          if (this._Obrigatorio != value)
	    	          {
	    	              this.ValidateProperty("Obrigatorio", value);
	    	              this.OnObrigatorioChanging(value);
	    	              this.RaiseDataMemberChanging("Obrigatorio");
	    	              this._Obrigatorio = value;
	    	              this.RaiseDataMemberChanged("Obrigatorio");
	    	              this.OnObrigatorioChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BMDTesteFrame.TesteCkb").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Demo.BM.TesteCkb), QualifiedEntitySetName = "BMDTesteFrame.TesteCkb" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TesteCkb.id_qualquer", Source = "IdQualquer", Target = "id_qualquer", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.TesteCkb", RelationPropertyName = "TesteCkb" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TesteCkb.Obrigatorio", Source = "Obrigatorio", Target = "Obrigatorio", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.TesteCkb", RelationPropertyName = "TesteCkb" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TesteCkb.NaoObrigatorio", Source = "NaoObrigatorio", Target = "NaoObrigatorio", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.TesteCkb", RelationPropertyName = "TesteCkb" });

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

		

	[LinxPublicationView(PrimaryKeys="CLIENTE.ID_CLIENTE", IsUpdatable=false, EdmName="Linx.Demo.BM.BMDTesteFrame")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[Cliente];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[CLIENTE];EntityRelations[ESTADO(ESTADO)#PAIS(PAIS)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "Cliente")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Demo.BV.ExAutocomplete.Cliente")]
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
	 

	    //Extensibility Partial Method Definitions For ComboboxCliente
	    partial void OnComboboxClienteChanging(byte value);
	    partial void OnComboboxClienteChanged();

	    private byte _ComboboxCliente;

	    [DataMember(IsRequired = true, Name = "ComboboxCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Combobox Cliente", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.COMBOBOX_CLIENTE];IsMeasure[false]")]
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
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.DATETIME_CLIENTE];IsMeasure[false]")]
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
	    //Extensibility Partial Method Definitions For IdCliente
	    partial void OnIdClienteChanging(int value);
	    partial void OnIdClienteChanged();

	    private int _IdCliente;

	    [DataMember(IsRequired = true, Name = "IdCliente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Cliente", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.ID_CLIENTE];IsMeasure[false]")]
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
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpEstado];LookUpTitle[Seleção de (Id Estado)];LookUpQuery[executeLookUpEstado];LookUpFinalize[finalizeLookUpEstado];LookUpDisplayColumns[{\"IdEstado\" : \"Id Estado\", \"StringEstado\" : \"String Estado\"}];LookUpColumns[{\"IdEstado\" : true, \"StringEstado\" : true}];FilterDataKey[CLIENTE.ESTADO.ID_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<int>#IdEstado#true##10:0##Id Estado#0#true##::LookUpEstado##false#false#ESTADO#ESTADO#Linx.Demo.BV.ExAutocomplete#IQueryable###true#false", EdmKey="CLIENTE.ESTADO.ID_ESTADO")]
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
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.INT_CLIENTE];IsMeasure[false]")]
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
	    [FunctionalPoint("Precision[5:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[CLIENTE.SMALL_INT_CLIENTE];IsMeasure[false]")]
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
	    [FunctionalPoint("Precision[50:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LkpTbnmcompleto];LookUpTitle[Seleção de (String Cliente)];LookUpQuery[executeLkpTbnmcompleto];LookUpFinalize[finalizeLkpTbnmcompleto];LookUpDisplayColumns[{\"idNomeCompleto\" : \"\", \"NomeCompleto\" : \"\"}];LookUpColumns[{\"idNomeCompleto\" : true, \"NomeCompleto\" : true}];FilterDataKey[CLIENTE.STRING_CLIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#NomeCompleto#false##0###1#true##::LkpTbnmcompleto#Linx.Demo.BV.SPA/ExemploAutoComplete#false#false###Linx.Demo.BV.ExAutocomplete#IQueryable###true#false", EdmKey="CLIENTE.STRING_CLIENTE")]
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
	    partial void OnStringEstadoChanging(string value);
	    partial void OnStringEstadoChanged();

	    private string _StringEstado;

	    [DataMember(Name = "StringEstado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "String Estado", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(50)]
	    [FunctionalPoint("Precision[50:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpEstado];LookUpTitle[Seleção de (String Estado)];LookUpQuery[executeLookUpEstado];LookUpFinalize[finalizeLookUpEstado];LookUpDisplayColumns[{\"IdEstado\" : \"Id Estado\", \"StringEstado\" : \"String Estado\"}];LookUpColumns[{\"IdEstado\" : true, \"StringEstado\" : true}];FilterDataKey[CLIENTE.ESTADO.STRING_ESTADO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#StringEstado#false##50:0##String Estado#1#true##::LookUpEstado##false#false#ESTADO#ESTADO#Linx.Demo.BV.ExAutocomplete#IQueryable###true#false", EdmKey="CLIENTE.ESTADO.STRING_ESTADO")]
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
	    //Extensibility Partial Method Definitions For IdNmCompleto
	    partial void OnIdNmCompletoChanging(int value);
	    partial void OnIdNmCompletoChanged();

	    private int _IdNmCompleto;

	    [DataMember(IsRequired = true, Name = "IdNmCompleto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id nome completo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];LookUpName[LkpTbnmcompleto];LookUpTitle[Seleção de (Id nome completo)];LookUpQuery[executeLkpTbnmcompleto];LookUpFinalize[finalizeLkpTbnmcompleto];LookUpDisplayColumns[{\"idNomeCompleto\" : \"\", \"NomeCompleto\" : \"\"}];LookUpColumns[{\"idNomeCompleto\" : true, \"NomeCompleto\" : true}];FilterDataKey[0];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="int#idNomeCompleto#false##0###0#true##::LkpTbnmcompleto#Linx.Demo.BV.SPA/ExemploAutoComplete#false#false###Linx.Demo.BV.ExAutocomplete#IQueryable###true#false", EdmKey="0")]
	    public int IdNmCompleto
	    {
	    	    get
	    	    {
	    	          return _IdNmCompleto;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdNmCompleto != value)
	    	          {
	    	              this.ValidateProperty("IdNmCompleto", value);
	    	              this.OnIdNmCompletoChanging(value);
	    	              this.RaiseDataMemberChanging("IdNmCompleto");
	    	              this._IdNmCompleto = value;
	    	              this.RaiseDataMemberChanged("IdNmCompleto");
	    	              this.OnIdNmCompletoChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "BMDTesteFrame.CLIENTE").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Demo.BM.CLIENTE), QualifiedEntitySetName = "BMDTesteFrame.CLIENTE" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.ID_CLIENTE", Source = "IdCliente", Target = "ID_CLIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.INT_CLIENTE", Source = "IntCliente", Target = "INT_CLIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.STRING_CLIENTE", Source = "StringCliente", Target = "STRING_CLIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.DECIMAL_CLIENTE", Source = "DecimalCliente", Target = "DECIMAL_CLIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.COMBOBOX_CLIENTE", Source = "ComboboxCliente", Target = "COMBOBOX_CLIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.DATETIME_CLIENTE", Source = "DatetimeCliente", Target = "DATETIME_CLIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.CLIENTE", RelationPropertyName = "CLIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.ESTADO.ID_ESTADO", Source = "IdEstado", Target = "ID_ESTADO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "BMDTesteFrame.ESTADO", RelationPropertyName = "ESTADO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="CLIENTE.SMALL_INT_CLIENTE", Source = "SmallIntCliente", Target = "SMALL_INT_CLIENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "BMDTesteFrame.CLIENTE", RelationPropertyName = "CLIENTE" });

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
	[DomainIdentifier("ProcessorOverviewExAutocompleteDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class ExAutocompleteDomainService : DomainService, IDataServiceContext 
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

		
	    public ExAutocompleteDomainService() : this("", null, null) { }
	    public ExAutocompleteDomainService(string connectionString) : this(connectionString, null, null) { }
	    public ExAutocompleteDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public ExAutocompleteDomainService(Linx.Demo.BM.BMDTesteFrame dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public ExAutocompleteDomainService(string connectionString, Linx.Demo.BM.BMDTesteFrame dataContext, Dictionary<string, string> headers) : base() 
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
	    //Get All LookUpTbnmmeio.
	    public IQueryable<LookUpTbnmmeio> GetAllLookUpTbnmmeio()
	    {
	        return this.GetLookUpTbnmmeio(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTbnmmeio By EntitySearch.
	    public IQueryable<LookUpTbnmmeio> GetLookUpTbnmmeioByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTbnmmeio(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTbnmmeio.
	    public IQueryable<LookUpTbnmmeio> GetLookUpTbnmmeio(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TBNMMEIO" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTbnmmeio";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTbnmmeio));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTbnmmeio> query =  
	
	            (from entity in this.DbContext.TBNMMEIO.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTbnmmeio()		
	            {
	            
                idnomeMeio = entity.id_nomeMeio
                , Nomedomeio = entity.Nomedomeio
                , IdNome = entity.id_nome
	            });

	            
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTbnome.
	    public IQueryable<LookUpTbnome> GetAllLookUpTbnome()
	    {
	        return this.GetLookUpTbnome(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTbnome By EntitySearch.
	    public IQueryable<LookUpTbnome> GetLookUpTbnomeByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTbnome(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTbnome.
	    public IQueryable<LookUpTbnome> GetLookUpTbnome(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TBNOME" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTbnome";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTbnome));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTbnome> query =  
	
	            (from entity in this.DbContext.TBNOME.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTbnome()		
	            {
	            
                IdNome = entity.id_nome
                , Nome = entity.Nome
	            });

	            
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LookUpTbsobrenm.
	    public IQueryable<LookUpTbsobrenm> GetAllLookUpTbsobrenm()
	    {
	        return this.GetLookUpTbsobrenm(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LookUpTbsobrenm By EntitySearch.
	    public IQueryable<LookUpTbsobrenm> GetLookUpTbsobrenmByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLookUpTbsobrenm(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LookUpTbsobrenm.
	    public IQueryable<LookUpTbsobrenm> GetLookUpTbsobrenm(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "TBSOBRENM" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LookUpTbsobrenm";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LookUpTbsobrenm));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LookUpTbsobrenm> query =  
	
	            (from entity in this.DbContext.TBSOBRENM.Where(dynQuery, parameters.ToArray())
	            
	            select new LookUpTbsobrenm()		
	            {
	            
                IdSobrenome = entity.id_sobrenome
                , SobreNome = entity.SobreNome
                , IdNome = entity.id_nome
                , idnomeMeio = entity.id_nomeMeio
	            });

	            
	
		
	
	
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
                , IdCliente2 = entity.ID_CLIENTE
	            });

	            
	
		
	
	
	        return query;

	    }
		
			
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
                , StringEstado = entity.STRING_ESTADO
	            });

	            
	
		
	
	
	        return query;

	    }
		
			
        [Ignore]
	    //Get All LkpTbnmcompleto.
	    public IQueryable<LkpTbnmcompleto> GetAllLkpTbnmcompleto()
	    {
	        return this.GetLkpTbnmcompleto(String.Empty, String.Empty, String.Empty);
	    }    

	    [Ignore]
	    //Get LkpTbnmcompleto By EntitySearch.
	    public IQueryable<LkpTbnmcompleto> GetLkpTbnmcompletoByEntitySearch(string propertyName, string serializedEntitySearch)
	    {
	        return this.GetLkpTbnmcompleto(propertyName, String.Empty, serializedEntitySearch);
	    }
	
	    [Ignore]
	    //Get LkpTbnmcompleto.
	    public IQueryable<LkpTbnmcompleto> GetLkpTbnmcompleto(string propertyName, string serializedPropertyValue, string serializedEntitySearch)
	    {	
	        EntitySearch entitySearch = (serializedEntitySearch.IsNullOrEmpty() ? new EntitySearch() { EdmEntityName = "" } : SerializationManager<EntitySearch>.StringToObject(serializedEntitySearch));
	        entitySearch.EntityName = "LkpTbnmcompleto";
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
	        	List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LkpTbnmcompleto));
	        	replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);		
	        } 
	
	        if (dynQuery.IsNullOrEmpty())
	        	dynQuery = "true"; 

	        IQueryable<LkpTbnmcompleto> query =  null;
		
	
	
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
	
		

	        if (entityName.InList("Linx.Demo.BV.ExAutocomplete.Tbnmcompleto"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "Tbnmcompleto",
	        			NameSpace = "Linx.Demo.BV.ExAutocomplete",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "Tbnmcompleto",
	        			ClearMethodName = "ClearTbnmcompleto",
	        			QueryMethodName  = "GetPagedTbnmcompleto",	
	        			CountingMethodName  = "GetTbnmcompleto" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Demo.BV.ExAutocomplete.Tbnmcompleto"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Demo.BV.ExAutocomplete.Tbnmcompleto"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Demo.BV.ExAutocomplete.TesteCkbView"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TesteCkbView",
	        			NameSpace = "Linx.Demo.BV.ExAutocomplete",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TesteCkbView",
	        			ClearMethodName = "ClearTesteCkbView",
	        			QueryMethodName  = "GetPagedTesteCkbView",	
	        			CountingMethodName  = "GetTesteCkbView" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Demo.BV.ExAutocomplete.TesteCkbView"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Demo.BV.ExAutocomplete.TesteCkbView"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Demo.BV.ExAutocomplete.Cliente"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "Cliente",
	        			NameSpace = "Linx.Demo.BV.ExAutocomplete",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "Cliente",
	        			ClearMethodName = "ClearCliente",
	        			QueryMethodName  = "GetPagedCliente",	
	        			CountingMethodName  = "GetCliente" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Demo.BV.ExAutocomplete.Cliente"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Demo.BV.ExAutocomplete.Cliente"), forceAll: forceAll)
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

         		    return new string[] { "Demo_ExAutocompleteClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Demo.BV.ClientResources.ExAutocompleteClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Demo_exAutocompleteService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Demo.BV.ClientResources.exAutocompleteService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear Tbnmcompleto.
	    public IEnumerable<Tbnmcompleto> ClearTbnmcompleto()
	    {
	        List<Tbnmcompleto> result = new List<Tbnmcompleto>();
	        result.Add(new Tbnmcompleto());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TesteCkbView.
	    public IEnumerable<TesteCkbView> ClearTesteCkbView()
	    {
	        List<TesteCkbView> result = new List<TesteCkbView>();
	        result.Add(new TesteCkbView());	
		
	        

	
	        return result;
	    }
		
	
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
	
		
	
	    [TbnmcompletoQueryCustomAuthorizationAuto()]
	    [Query(HasSideEffects = false)]
	    //Get Tbnmcompleto.
	    public IQueryable<Tbnmcompleto> GetTbnmcompleto()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTbnmcompleto")))
 	        {
 	             AuthorizationResult authorizationResult = (new TbnmcompletoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<Tbnmcompleto> result = 
	            (from entity0 in this.DbContext.TBNMCOMPLETO
                  let entity0Al2 = entity0.TBNOME
                  let entity0Al1 = entity0.CLIENTE
                  let entity0Al3 = entity0.TBNMMEIO
                  let entity0Al4 = entity0.TBSOBRENM
	            
	            	
	            select new Tbnmcompleto()		
	            {
	            
                IdCliente = entity0Al1.ID_CLIENTE
                , IdNome = entity0Al2.id_nome
                , idNomeCompleto = entity0.id_NomeCompleto
                , idnomeMeio = entity0Al3.id_nomeMeio
                , IdSobrenome = entity0Al4.id_sobrenome
                , Nome = entity0Al2.Nome
                , NomeCompleto = entity0.NomeCompleto
                , Nomedomeio = entity0Al3.Nomedomeio
                , SobreNome = entity0Al4.SobreNome
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TbnmcompletoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TbnmcompletoNoAssociations.
	    public IQueryable<Tbnmcompleto> GetTbnmcompletoNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTbnmcompletoNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TbnmcompletoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<Tbnmcompleto> result = 
	            (from entity0 in this.DbContext.TBNMCOMPLETO
                  let entity0Al2 = entity0.TBNOME
                  let entity0Al1 = entity0.CLIENTE
                  let entity0Al3 = entity0.TBNMMEIO
                  let entity0Al4 = entity0.TBSOBRENM
	            
	            	
	            select new Tbnmcompleto()		
	            {
	            
                IdCliente = entity0Al1.ID_CLIENTE
                , IdNome = entity0Al2.id_nome
                , idNomeCompleto = entity0.id_NomeCompleto
                , idnomeMeio = entity0Al3.id_nomeMeio
                , IdSobrenome = entity0Al4.id_sobrenome
                , Nome = entity0Al2.Nome
                , NomeCompleto = entity0.NomeCompleto
                , Nomedomeio = entity0Al3.Nomedomeio
                , SobreNome = entity0Al4.SobreNome
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TesteCkbViewQueryCustomAuthorizationAuto()]
	    [Query(HasSideEffects = false)]
	    //Get TesteCkbView.
	    public IQueryable<TesteCkbView> GetTesteCkbView()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTesteCkbView")))
 	        {
 	             AuthorizationResult authorizationResult = (new TesteCkbViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<TesteCkbView> result = 
	            (from entity0 in this.DbContext.TesteCkb
	            
	            	
	            select new TesteCkbView()		
	            {
	            
                IdQualquer = entity0.id_qualquer
                , NaoObrigatorio = entity0.NaoObrigatorio
                , Obrigatorio = entity0.Obrigatorio
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TesteCkbViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TesteCkbViewNoAssociations.
	    public IQueryable<TesteCkbView> GetTesteCkbViewNoAssociations()
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTesteCkbViewNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TesteCkbViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

		
	
	        
		
	        
	
	        IQueryable<TesteCkbView> result = 
	            (from entity0 in this.DbContext.TesteCkb
	            
	            	
	            select new TesteCkbView()		
	            {
	            
                IdQualquer = entity0.id_qualquer
                , NaoObrigatorio = entity0.NaoObrigatorio
                , Obrigatorio = entity0.Obrigatorio
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [ClienteQueryCustomAuthorizationAuto()]
	    [Query(HasSideEffects = false)]
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
	            
                ComboboxCliente = entity0.COMBOBOX_CLIENTE
                , DatetimeCliente = entity0.DATETIME_CLIENTE
                , DecimalCliente = entity0.DECIMAL_CLIENTE
                , IdCliente = entity0.ID_CLIENTE
                , IdEstado = entity0Al1.ID_ESTADO
                , IntCliente = entity0.INT_CLIENTE
                , SmallIntCliente = entity0.SMALL_INT_CLIENTE
                , StringCliente = entity0.STRING_CLIENTE
                , StringEstado = entity0Al1.STRING_ESTADO
                , IdNmCompleto = 0
		
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
	            
                ComboboxCliente = entity0.COMBOBOX_CLIENTE
                , DatetimeCliente = entity0.DATETIME_CLIENTE
                , DecimalCliente = entity0.DECIMAL_CLIENTE
                , IdCliente = entity0.ID_CLIENTE
                , IdEstado = entity0Al1.ID_ESTADO
                , IntCliente = entity0.INT_CLIENTE
                , SmallIntCliente = entity0.SMALL_INT_CLIENTE
                , StringCliente = entity0.STRING_CLIENTE
                , StringEstado = entity0Al1.STRING_ESTADO
                , IdNmCompleto = 0
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for TBNMCOMPLETO
	    	string[] bmDisabledTbnmcompletoList = this.GetEDM().GetFilteringDisabledList("TBNMCOMPLETO");
	    	if (bmDisabledTbnmcompletoList.Length > 0)
	    	{
	
	    		if (bmDisabledTbnmcompletoList.Contains("TBNMCOMPLETO.id_NomeCompleto"))
	    		{
	    			result.Add("Tbnmcompleto|idNomeCompleto");
	    			result.Add("Tbnmcompleto|TBNMCOMPLETO.id_NomeCompleto");
	    		}
	
	    		if (bmDisabledTbnmcompletoList.Contains("TBNMCOMPLETO.NomeCompleto"))
	    		{
	    			result.Add("Tbnmcompleto|NomeCompleto");
	    			result.Add("Tbnmcompleto|TBNMCOMPLETO.NomeCompleto");
	    		}
	    	}
	    	//Add filtering disabled property for TesteCkb
	    	string[] bmDisabledTesteCkbViewList = this.GetEDM().GetFilteringDisabledList("TesteCkb");
	    	if (bmDisabledTesteCkbViewList.Length > 0)
	    	{
	
	    		if (bmDisabledTesteCkbViewList.Contains("TesteCkb.id_qualquer"))
	    		{
	    			result.Add("TesteCkbView|IdQualquer");
	    			result.Add("TesteCkbView|TesteCkb.id_qualquer");
	    		}
	
	    		if (bmDisabledTesteCkbViewList.Contains("TesteCkb.NaoObrigatorio"))
	    		{
	    			result.Add("TesteCkbView|NaoObrigatorio");
	    			result.Add("TesteCkbView|TesteCkb.NaoObrigatorio");
	    		}
	
	    		if (bmDisabledTesteCkbViewList.Contains("TesteCkb.Obrigatorio"))
	    		{
	    			result.Add("TesteCkbView|Obrigatorio");
	    			result.Add("TesteCkbView|TesteCkb.Obrigatorio");
	    		}
	    	}
	    	result.Add("Cliente|IdNmCompleto");
	    	result.Add("Cliente|0");
	    	//Add filtering disabled property for CLIENTE
	    	string[] bmDisabledClienteList = this.GetEDM().GetFilteringDisabledList("CLIENTE");
	    	if (bmDisabledClienteList.Length > 0)
	    	{
	
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
				
	    [Ignore]
	    //Get Tbnmcompleto By EntitySearchId.
	    public IQueryable<Tbnmcompleto> GetTbnmcompletoByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTbnmcompletoByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get Tbnmcompleto By EntitySearchId.
	    public IQueryable<Tbnmcompleto> GetTbnmcompletoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTbnmcompletoByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TesteCkbView By EntitySearchId.
	    public IQueryable<TesteCkbView> GetTesteCkbViewByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTesteCkbViewByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TesteCkbView By EntitySearchId.
	    public IQueryable<TesteCkbView> GetTesteCkbViewByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTesteCkbViewByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get Cliente By EntitySearchId.
	    public IQueryable<Cliente> GetClienteByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetClienteByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get Cliente By EntitySearchId.
	    public IQueryable<Cliente> GetClienteByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetClienteByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get Tbnmcompleto By Example.
	    [Ignore]
	    public IQueryable<Tbnmcompleto> GetTbnmcompletoByExample(Tbnmcompleto entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTbnmcompletoByEntitySearch(queryAnalysis);
	    }
			
	    //Get Tbnmcompleto By Example.
	    [Ignore]
	    public IQueryable<Tbnmcompleto> GetTbnmcompletoByExampleNoAssociations(Tbnmcompleto entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTbnmcompletoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TesteCkbView By Example.
	    [Ignore]
	    public IQueryable<TesteCkbView> GetTesteCkbViewByExample(TesteCkbView entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTesteCkbViewByEntitySearch(queryAnalysis);
	    }
			
	    //Get TesteCkbView By Example.
	    [Ignore]
	    public IQueryable<TesteCkbView> GetTesteCkbViewByExampleNoAssociations(TesteCkbView entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTesteCkbViewByEntitySearchNoAssociations(queryAnalysis);
	    }
			
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
	    public Tbnmcompleto GetTbnmcompletoByKey(int idNomeCompleto)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("Tbnmcompleto");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "idNomeCompleto"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idNomeCompleto));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTbnmcompletoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TesteCkbView GetTesteCkbViewByKey(int idQualquer)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TesteCkbView");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdQualquer"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idQualquer));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTesteCkbViewByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


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
	
			
	
	    [TbnmcompletoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TbnmcompletoByEntitySearch.
	    public IQueryable<Tbnmcompleto> GetTbnmcompletoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTbnmcompletoByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new TbnmcompletoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Tbnmcompleto));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Tbnmcompleto> result = 
	            (from entity0 in this.DbContext.TBNMCOMPLETO.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.TBNOME
                  let entity0Al1 = entity0.CLIENTE
                  let entity0Al3 = entity0.TBNMMEIO
                  let entity0Al4 = entity0.TBSOBRENM
	            
	            	
	            select new Tbnmcompleto()		
	            {
	            
                IdCliente = entity0Al1.ID_CLIENTE
                , IdNome = entity0Al2.id_nome
                , idNomeCompleto = entity0.id_NomeCompleto
                , idnomeMeio = entity0Al3.id_nomeMeio
                , IdSobrenome = entity0Al4.id_sobrenome
                , Nome = entity0Al2.Nome
                , NomeCompleto = entity0.NomeCompleto
                , Nomedomeio = entity0Al3.Nomedomeio
                , SobreNome = entity0Al4.SobreNome
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TbnmcompletoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TbnmcompletoByEntitySearchNoAssociations.
	    public IQueryable<Tbnmcompleto> GetTbnmcompletoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTbnmcompletoByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TbnmcompletoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Tbnmcompleto));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Tbnmcompleto> result = 
	            (from entity0 in this.DbContext.TBNMCOMPLETO.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.TBNOME
                  let entity0Al1 = entity0.CLIENTE
                  let entity0Al3 = entity0.TBNMMEIO
                  let entity0Al4 = entity0.TBSOBRENM
	            
	            	
	            select new Tbnmcompleto()		
	            {
	            
                IdCliente = entity0Al1.ID_CLIENTE
                , IdNome = entity0Al2.id_nome
                , idNomeCompleto = entity0.id_NomeCompleto
                , idnomeMeio = entity0Al3.id_nomeMeio
                , IdSobrenome = entity0Al4.id_sobrenome
                , Nome = entity0Al2.Nome
                , NomeCompleto = entity0.NomeCompleto
                , Nomedomeio = entity0Al3.Nomedomeio
                , SobreNome = entity0Al4.SobreNome
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TesteCkbViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TesteCkbViewByEntitySearch.
	    public IQueryable<TesteCkbView> GetTesteCkbViewByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTesteCkbViewByEntitySearch")))
 	        {
 	             AuthorizationResult authorizationResult = (new TesteCkbViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TesteCkbView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TesteCkbView> result = 
	            (from entity0 in this.DbContext.TesteCkb.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TesteCkbView()		
	            {
	            
                IdQualquer = entity0.id_qualquer
                , NaoObrigatorio = entity0.NaoObrigatorio
                , Obrigatorio = entity0.Obrigatorio
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    [TesteCkbViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get TesteCkbViewByEntitySearchNoAssociations.
	    public IQueryable<TesteCkbView> GetTesteCkbViewByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetTesteCkbViewByEntitySearchNoAssociations")))
 	        {
 	             AuthorizationResult authorizationResult = (new TesteCkbViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TesteCkbView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TesteCkbView> result = 
	            (from entity0 in this.DbContext.TesteCkb.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TesteCkbView()		
	            {
	            
                IdQualquer = entity0.id_qualquer
                , NaoObrigatorio = entity0.NaoObrigatorio
                , Obrigatorio = entity0.Obrigatorio
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
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
	            
                ComboboxCliente = entity0.COMBOBOX_CLIENTE
                , DatetimeCliente = entity0.DATETIME_CLIENTE
                , DecimalCliente = entity0.DECIMAL_CLIENTE
                , IdCliente = entity0.ID_CLIENTE
                , IdEstado = entity0Al1.ID_ESTADO
                , IntCliente = entity0.INT_CLIENTE
                , SmallIntCliente = entity0.SMALL_INT_CLIENTE
                , StringCliente = entity0.STRING_CLIENTE
                , StringEstado = entity0Al1.STRING_ESTADO
                , IdNmCompleto = 0
		
	            }
	            );
	
	        SetClienteBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
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
	            
                ComboboxCliente = entity0.COMBOBOX_CLIENTE
                , DatetimeCliente = entity0.DATETIME_CLIENTE
                , DecimalCliente = entity0.DECIMAL_CLIENTE
                , IdCliente = entity0.ID_CLIENTE
                , IdEstado = entity0Al1.ID_ESTADO
                , IntCliente = entity0.INT_CLIENTE
                , SmallIntCliente = entity0.SMALL_INT_CLIENTE
                , StringCliente = entity0.STRING_CLIENTE
                , StringEstado = entity0Al1.STRING_ESTADO
                , IdNmCompleto = 0
		
	            }
	            );
	
	        SetClienteBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
	
	    [Ignore()]
	    private void SetClienteBusinessFilter(ref IQueryable<Cliente> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "Cliente"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "IdNmCompleto" || e.Value.ToString() == "0")))
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
	    										int tmpIdNmCompleto1 = (int)value;
	    										query = from r in query where r.IdNmCompleto == tmpIdNmCompleto1 select r;
	    										break;
	    									case "!=":
	    										int tmpIdNmCompleto2 = (int)value;
	    										query = from r in query where r.IdNmCompleto != tmpIdNmCompleto2 select r;
	    										break;

	
	    									case "<":
	    										int tmpIdNmCompleto3 = (int)value;
	    										query = from r in query where r.IdNmCompleto < tmpIdNmCompleto3 select r;
	    										break;
	    									case "<=":
	    										int tmpIdNmCompleto4 = (int)value;
	    										query = from r in query where r.IdNmCompleto <= tmpIdNmCompleto4 select r;
	    										break;
	    									case ">":
	    										int tmpIdNmCompleto5 = (int)value;
	    										query = from r in query where r.IdNmCompleto > tmpIdNmCompleto5 select r;
	    										break;
	    									case ">=":
	    										int tmpIdNmCompleto6 = (int)value;
	    										query = from r in query where r.IdNmCompleto >= tmpIdNmCompleto6 select r;
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
	
			
	
	    [TbnmcompletoQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedTbnmcompleto.
	    public IQueryable<Tbnmcompleto> GetPagedTbnmcompleto(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedTbnmcompleto")))
 	        {
 	             AuthorizationResult authorizationResult = (new TbnmcompletoQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Tbnmcompleto));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<Tbnmcompleto> result = 
	            (from entity0 in this.DbContext.TBNMCOMPLETO.Where(dynQuery, parameters.ToArray())
                  let entity0Al2 = entity0.TBNOME
                  let entity0Al1 = entity0.CLIENTE
                  let entity0Al3 = entity0.TBNMMEIO
                  let entity0Al4 = entity0.TBSOBRENM
                orderby entity0.id_NomeCompleto ascending
	            
	            	
	            select new Tbnmcompleto()		
	            {
	            
                IdCliente = entity0Al1.ID_CLIENTE
                , IdNome = entity0Al2.id_nome
                , idNomeCompleto = entity0.id_NomeCompleto
                , idnomeMeio = entity0Al3.id_nomeMeio
                , IdSobrenome = entity0Al4.id_sobrenome
                , Nome = entity0Al2.Nome
                , NomeCompleto = entity0.NomeCompleto
                , Nomedomeio = entity0Al3.Nomedomeio
                , SobreNome = entity0Al4.SobreNome
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTbnmcompletoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(Tbnmcompleto));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TBNMCOMPLETO.Where(dynQuery, parameters.ToArray())
                  let entityAl2 = entity.TBNOME
                  let entityAl1 = entity.CLIENTE
                  let entityAl3 = entity.TBNMMEIO
                  let entityAl4 = entity.TBSOBRENM
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    [TesteCkbViewQueryCustomAuthorizationAuto()]
	    [Ignore]
	    //Get PagedTesteCkbView.
	    public IQueryable<TesteCkbView> GetPagedTesteCkbView(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {



 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "GetPagedTesteCkbView")))
 	        {
 	             AuthorizationResult authorizationResult = (new TesteCkbViewQueryCustomAuthorizationAutoAttribute()).Authorize(this);
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
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TesteCkbView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TesteCkbView> result = 
	            (from entity0 in this.DbContext.TesteCkb.Where(dynQuery, parameters.ToArray())
                orderby entity0.id_qualquer ascending
	            
	            	
	            select new TesteCkbView()		
	            {
	            
                IdQualquer = entity0.id_qualquer
                , NaoObrigatorio = entity0.NaoObrigatorio
                , Obrigatorio = entity0.Obrigatorio
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTesteCkbViewCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TesteCkbView));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TesteCkb.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
			
	
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
	            
                ComboboxCliente = entity0.COMBOBOX_CLIENTE
                , DatetimeCliente = entity0.DATETIME_CLIENTE
                , DecimalCliente = entity0.DECIMAL_CLIENTE
                , IdCliente = entity0.ID_CLIENTE
                , IdEstado = entity0Al1.ID_ESTADO
                , IntCliente = entity0.INT_CLIENTE
                , SmallIntCliente = entity0.SMALL_INT_CLIENTE
                , StringCliente = entity0.STRING_CLIENTE
                , StringEstado = entity0Al1.STRING_ESTADO
                , IdNmCompleto = 0
		
	            }
	            ).Skip(skip).Take(take);
	
	        SetClienteBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
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
	
			
	    [TbnmcompletoUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update Tbnmcompleto.
	    public void UpdateTbnmcompleto(Tbnmcompleto entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateTbnmcompleto")))
 	        {
 	             AuthorizationResult authorizationResult = (new TbnmcompletoUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    [TbnmcompletoInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert Tbnmcompleto.
	    public void InsertTbnmcompleto(Tbnmcompleto entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertTbnmcompleto")))
 	        {
 	             AuthorizationResult authorizationResult = (new TbnmcompletoInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    [TbnmcompletoDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete Tbnmcompleto.
	    public void DeleteTbnmcompleto(Tbnmcompleto entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteTbnmcompleto")))
 	        {
 	             AuthorizationResult authorizationResult = (new TbnmcompletoDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    [TesteCkbViewUpdateCustomAuthorizationAuto()]
	    [Update()]	
	    //Update TesteCkbView.
	    public void UpdateTesteCkbView(TesteCkbView entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "UpdateTesteCkbView")))
 	        {
 	             AuthorizationResult authorizationResult = (new TesteCkbViewUpdateCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    [TesteCkbViewInsertCustomAuthorizationAuto()]
	    [Insert()]
	    //Insert TesteCkbView.
	    public void InsertTesteCkbView(TesteCkbView entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "InsertTesteCkbView")))
 	        {
 	             AuthorizationResult authorizationResult = (new TesteCkbViewInsertCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    [TesteCkbViewDeleteCustomAuthorizationAuto()]
	    [Delete()]
	    //Delete TesteCkbView.
	    public void DeleteTesteCkbView(TesteCkbView entity)
	    {


 	        if (!this.IsSecure && (this.AuthorizationContext == null || !(this.ServiceContext != null && this.ServiceContext.Operation != null && this.ServiceContext.Operation.Name == "DeleteTesteCkbView")))
 	        {
 	             AuthorizationResult authorizationResult = (new TesteCkbViewDeleteCustomAuthorizationAutoAttribute()).Authorize(this);
 	             if (authorizationResult != AuthorizationResult.Allowed)
 	                 throw new DomainException(authorizationResult.ErrorMessage);
 	             else
 	                 this.IsSecure = true;
 	        }

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
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