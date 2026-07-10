					
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
using Linx.Framework.Loja.BM;

namespace Linx.Framework.BV.LojaVendedor
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="LJV_VENDEDOR.ID_VENDEDOR", IsUpdatable=false, EdmName="Linx.Framework.Loja.BM.ConectorPos")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[LjvVendedor];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdVendedor];ReadOnly[false];Entities[LJV_VENDEDOR:IdVendedor|LJV_LOJA:IdLoja];SubQueryInfo[];EdmEntityName[LJV_VENDEDOR];EntityRelations[LJV_VENDEDOR1(LJV_VENDEDOR)#LJV_LOJA(LJV_LOJA)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "LjvVendedor")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.LojaVendedor.LjvVendedor")]
	public partial class LjvVendedor : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For CodVendedor
	    partial void OnCodVendedorChanging(System.String value);
	    partial void OnCodVendedorChanged();

	    private System.String _CodVendedor;

	    [DataMember(Name = "CodVendedor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cod Vendedor", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[2];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_VENDEDOR.COD_VENDEDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_VENDEDOR.COD_VENDEDOR")]
	    public System.String CodVendedor
	    {
	    	    get
	    	    {
	    	          return _CodVendedor;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodVendedor != value)
	    	          {
	    	              this.ValidateProperty("CodVendedor", value);
	    	              this.OnCodVendedorChanging(value);
	    	              this.RaiseDataMemberChanging("CodVendedor");
	    	              this._CodVendedor = value;
	    	              this.RaiseDataMemberChanged("CodVendedor");
	    	              this.OnCodVendedorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DataAtivacao
	    partial void OnDataAtivacaoChanging(System.Nullable<System.DateTime> value);
	    partial void OnDataAtivacaoChanged();

	    private System.Nullable<System.DateTime> _DataAtivacao;

	    [DataMember(Name = "DataAtivacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Data Ativacao", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_VENDEDOR.DATA_ATIVACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_VENDEDOR.DATA_ATIVACAO")]
	    public System.Nullable<System.DateTime> DataAtivacao
	    {
	    	    get
	    	    {
	    	          return _DataAtivacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataAtivacao != value)
	    	          {
	    	              this.ValidateProperty("DataAtivacao", value);
	    	              this.OnDataAtivacaoChanging(value);
	    	              this.RaiseDataMemberChanging("DataAtivacao");
	    	              this._DataAtivacao = value;
	    	              this.RaiseDataMemberChanged("DataAtivacao");
	    	              this.OnDataAtivacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DataDesativacao
	    partial void OnDataDesativacaoChanging(System.Nullable<System.DateTime> value);
	    partial void OnDataDesativacaoChanged();

	    private System.Nullable<System.DateTime> _DataDesativacao;

	    [DataMember(Name = "DataDesativacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Data Desativacao", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_VENDEDOR.DATA_DESATIVACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_VENDEDOR.DATA_DESATIVACAO")]
	    public System.Nullable<System.DateTime> DataDesativacao
	    {
	    	    get
	    	    {
	    	          return _DataDesativacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataDesativacao != value)
	    	          {
	    	              this.ValidateProperty("DataDesativacao", value);
	    	              this.OnDataDesativacaoChanging(value);
	    	              this.RaiseDataMemberChanging("DataDesativacao");
	    	              this._DataDesativacao = value;
	    	              this.RaiseDataMemberChanged("DataDesativacao");
	    	              this.OnDataDesativacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdFilialPfj
	    partial void OnIdFilialPfjChanging(Int32 value);
	    partial void OnIdFilialPfjChanged();

	    private Int32 _IdFilialPfj;

	    [DataMember(IsRequired = true, Name = "IdFilialPfj", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Filial Pfj", Description="", Order = 21, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_VENDEDOR.LJV_LOJA.ID_FILIAL_PFJ];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_VENDEDOR.LJV_LOJA.ID_FILIAL_PFJ")]
	    public Int32 IdFilialPfj
	    {
	    	    get
	    	    {
	    	          return _IdFilialPfj;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdFilialPfj != value)
	    	          {
	    	              this.ValidateProperty("IdFilialPfj", value);
	    	              this.OnIdFilialPfjChanging(value);
	    	              this.RaiseDataMemberChanging("IdFilialPfj");
	    	              this._IdFilialPfj = value;
	    	              this.RaiseDataMemberChanged("IdFilialPfj");
	    	              this.OnIdFilialPfjChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLoja
	    partial void OnIdLojaChanging(Int32 value);
	    partial void OnIdLojaChanged();

	    private Int32 _IdLoja;

	    [DataMember(IsRequired = true, Name = "IdLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Loja", Description="", Order = 24, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_VENDEDOR.LJV_LOJA.ID_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_VENDEDOR.LJV_LOJA.ID_LOJA")]
	    public Int32 IdLoja
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
	    //Extensibility Partial Method Definitions For IdVendedor
	    partial void OnIdVendedorChanging(Int32 value);
	    partial void OnIdVendedorChanged();

	    private Int32 _IdVendedor;

	    [DataMember(IsRequired = true, Name = "IdVendedor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Vendedor", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_VENDEDOR.ID_VENDEDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_VENDEDOR.ID_VENDEDOR")]
	    public Int32 IdVendedor
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
	    //Extensibility Partial Method Definitions For Inativo
	    partial void OnInativoChanging(Boolean value);
	    partial void OnInativoChanged();

	    private Boolean _Inativo;

	    [DataMember(IsRequired = true, Name = "Inativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_VENDEDOR.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_VENDEDOR.INATIVO")]
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
	    //Extensibility Partial Method Definitions For IndicaGerente
	    partial void OnIndicaGerenteChanging(Boolean value);
	    partial void OnIndicaGerenteChanged();

	    private Boolean _IndicaGerente;

	    [DataMember(IsRequired = true, Name = "IndicaGerente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Indica Gerente", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_VENDEDOR.INDICA_GERENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_VENDEDOR.INDICA_GERENTE")]
	    public Boolean IndicaGerente
	    {
	    	    get
	    	    {
	    	          return _IndicaGerente;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaGerente != value)
	    	          {
	    	              this.ValidateProperty("IndicaGerente", value);
	    	              this.OnIndicaGerenteChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaGerente");
	    	              this._IndicaGerente = value;
	    	              this.RaiseDataMemberChanged("IndicaGerente");
	    	              this.OnIndicaGerenteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IndicaOperadorCaixa
	    partial void OnIndicaOperadorCaixaChanging(Boolean value);
	    partial void OnIndicaOperadorCaixaChanged();

	    private Boolean _IndicaOperadorCaixa;

	    [DataMember(IsRequired = true, Name = "IndicaOperadorCaixa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Indica Operador Caixa", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_VENDEDOR.INDICA_OPERADOR_CAIXA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_VENDEDOR.INDICA_OPERADOR_CAIXA")]
	    public Boolean IndicaOperadorCaixa
	    {
	    	    get
	    	    {
	    	          return _IndicaOperadorCaixa;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaOperadorCaixa != value)
	    	          {
	    	              this.ValidateProperty("IndicaOperadorCaixa", value);
	    	              this.OnIndicaOperadorCaixaChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaOperadorCaixa");
	    	              this._IndicaOperadorCaixa = value;
	    	              this.RaiseDataMemberChanged("IndicaOperadorCaixa");
	    	              this.OnIndicaOperadorCaixaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeVendedor
	    partial void OnNomeVendedorChanging(System.String value);
	    partial void OnNomeVendedorChanged();

	    private System.String _NomeVendedor;

	    [DataMember(Name = "NomeVendedor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Vendedor", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_VENDEDOR.NOME_VENDEDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_VENDEDOR.NOME_VENDEDOR")]
	    public System.String NomeVendedor
	    {
	    	    get
	    	    {
	    	          return _NomeVendedor;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeVendedor != value)
	    	          {
	    	              this.ValidateProperty("NomeVendedor", value);
	    	              this.OnNomeVendedorChanging(value);
	    	              this.RaiseDataMemberChanging("NomeVendedor");
	    	              this._NomeVendedor = value;
	    	              this.RaiseDataMemberChanged("NomeVendedor");
	    	              this.OnNomeVendedorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Senha
	    partial void OnSenhaChanging(System.String value);
	    partial void OnSenhaChanged();

	    private System.String _Senha;

	    [DataMember(Name = "Senha", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Senha", Description="", Order = 12, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_VENDEDOR.SENHA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_VENDEDOR.SENHA")]
	    public System.String Senha
	    {
	    	    get
	    	    {
	    	          return _Senha;
	    	    }
	    	    set
	    	    {
	    	          if (this._Senha != value)
	    	          {
	    	              this.ValidateProperty("Senha", value);
	    	              this.OnSenhaChanging(value);
	    	              this.RaiseDataMemberChanging("Senha");
	    	              this._Senha = value;
	    	              this.RaiseDataMemberChanged("Senha");
	    	              this.OnSenhaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Hash
	    partial void OnHashChanging(System.String value);
	    partial void OnHashChanged();

	    private System.String _Hash;

	    [DataMember(IsRequired = true, Name = "Hash", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public System.String Hash
	    {
	    	    get
	    	    {
	    	          if (_Hash != (GetHash()))
	    	             _Hash =  GetHash();
	    	          return _Hash;
	    	    }
	    	    set
	    	    {
	    	          if (this._Hash != value)
	    	          {
	    	              this.ValidateProperty("Hash", value);
	    	              this.OnHashChanging(value);
	    	              this.RaiseDataMemberChanging("Hash");
	    	              this._Hash = value;
	    	              this.RaiseDataMemberChanged("Hash");
	    	              this.OnHashChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdVendedor;
	    [DataMember(Name = "TemporaryIdVendedor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Vendedor (Tmp)", Description="Temporary Key", Order = 7, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdVendedor
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdVendedor.IsNullOrEmpty())
	    	                this._TemporaryIdVendedor = this._IdVendedor;
	    	          return this._TemporaryIdVendedor;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdVendedor != value)
	    	              this._TemporaryIdVendedor = value;
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ConectorPos.LJV_VENDEDOR").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Loja.BM.LJV_VENDEDOR), QualifiedEntitySetName = "ConectorPos.LJV_VENDEDOR" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_VENDEDOR.SENHA", Source = "Senha", Target = "SENHA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_VENDEDOR", RelationPropertyName = "LJV_VENDEDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_VENDEDOR.INATIVO", Source = "Inativo", Target = "INATIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_VENDEDOR", RelationPropertyName = "LJV_VENDEDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_VENDEDOR.ID_VENDEDOR", Source = "IdVendedor", Target = "ID_VENDEDOR", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_VENDEDOR", RelationPropertyName = "LJV_VENDEDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_VENDEDOR.COD_VENDEDOR", Source = "CodVendedor", Target = "COD_VENDEDOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_VENDEDOR", RelationPropertyName = "LJV_VENDEDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_VENDEDOR.DATA_ATIVACAO", Source = "DataAtivacao", Target = "DATA_ATIVACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_VENDEDOR", RelationPropertyName = "LJV_VENDEDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_VENDEDOR.NOME_VENDEDOR", Source = "NomeVendedor", Target = "NOME_VENDEDOR", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_VENDEDOR", RelationPropertyName = "LJV_VENDEDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_VENDEDOR.INDICA_GERENTE", Source = "IndicaGerente", Target = "INDICA_GERENTE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_VENDEDOR", RelationPropertyName = "LJV_VENDEDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_VENDEDOR.DATA_DESATIVACAO", Source = "DataDesativacao", Target = "DATA_DESATIVACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_VENDEDOR", RelationPropertyName = "LJV_VENDEDOR" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_VENDEDOR.LJV_LOJA.ID_LOJA", Source = "IdLoja", Target = "ID_LOJA", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ConectorPos.LJV_LOJA", RelationPropertyName = "LJV_LOJA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_VENDEDOR.INDICA_OPERADOR_CAIXA", Source = "IndicaOperadorCaixa", Target = "INDICA_OPERADOR_CAIXA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_VENDEDOR", RelationPropertyName = "LJV_VENDEDOR" });

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
	[DomainIdentifier("ProcessorOverviewLojaVendedorDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class LojaVendedorDomainService : DomainService, IDataServiceContext 
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
	
	    private Linx.Framework.Loja.BM.ConectorPos _dbContext;
	    protected Linx.Framework.Loja.BM.ConectorPos DbContext 
	    { 
	    	get 
	    	{
	        	if (this._dbContext == null)
	        	{
	        		this._dbContext = new Linx.Framework.Loja.BM.ConectorPos(connectionString, this.Headers);
	        		((System.Data.Entity.Infrastructure.IObjectContextAdapter)this._dbContext).ObjectContext.CommandTimeout = 180;
	        		this._hasGpeconControl = (!(this._dbContext.IsUserMultiGpecon && this._dbContext.IdGpecon == this._dbContext.IdLinx) && this._dbContext.IdGpecon > 0);		
	        	}
	        	return this._dbContext;
	    	}
	    }

	    public string GetModelAssemblyName()
	    {
	        return typeof(Linx.Framework.Loja.BM.ConectorPos).Assembly.FullName;
	    }

	    public System.Data.Entity.Database Database
	    {
	        get { return this.DbContext.Database; }
	    }

		
	    public LojaVendedorDomainService() : this("", null, null) { }
	    public LojaVendedorDomainService(string connectionString) : this(connectionString, null, null) { }
	    public LojaVendedorDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public LojaVendedorDomainService(Linx.Framework.Loja.BM.ConectorPos dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public LojaVendedorDomainService(string connectionString, Linx.Framework.Loja.BM.ConectorPos dataContext, Dictionary<string, string> headers) : base() 
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
	    public Linx.Framework.Loja.BM.ConectorPos GetEDM()
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
	
		

	        if (entityName.InList("Linx.Framework.BV.LojaVendedor.LjvVendedor"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "LjvVendedor",
	        			NameSpace = "Linx.Framework.BV.LojaVendedor",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "LjvVendedor",
	        			ClearMethodName = "ClearLjvVendedor",
	        			QueryMethodName  = "GetPagedLjvVendedor",	
	        			CountingMethodName  = "GetLjvVendedor" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.LojaVendedor.LjvVendedor"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.LojaVendedor.LjvVendedor"), forceAll: forceAll)
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

         		    return new string[] { "Framework_LojaVendedorClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.LojaVendedorClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_lojaVendedorService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.lojaVendedorService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear LjvVendedor.
	    public IEnumerable<LjvVendedor> ClearLjvVendedor()
	    {
	        List<LjvVendedor> result = new List<LjvVendedor>();
	        result.Add(new LjvVendedor());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Query(HasSideEffects = false)]
	    //Get LjvVendedor.
	    public IQueryable<LjvVendedor> GetLjvVendedor()
	    {




		
	
	        
		
	        
	
	        IQueryable<LjvVendedor> result = 
	            (from entity0 in this.DbContext.LJV_VENDEDOR
                  let entity0Al1 = entity0.LJV_LOJA
                orderby entity0.NOME_VENDEDOR ascending, entity0.COD_VENDEDOR ascending
	            
	            	
	            select new LjvVendedor()		
	            {
	            
                CodVendedor = entity0.COD_VENDEDOR
                , DataAtivacao = entity0.DATA_ATIVACAO
                , DataDesativacao = entity0.DATA_DESATIVACAO
                , IdFilialPfj = entity0Al1.ID_FILIAL_PFJ
                , IdLoja = entity0Al1.ID_LOJA
                , IdVendedor = entity0.ID_VENDEDOR
                , Inativo = entity0.INATIVO
                , IndicaGerente = entity0.INDICA_GERENTE
                , IndicaOperadorCaixa = entity0.INDICA_OPERADOR_CAIXA
                , NomeVendedor = entity0.NOME_VENDEDOR
                , Senha = entity0.SENHA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get LjvVendedorNoAssociations.
	    public IQueryable<LjvVendedor> GetLjvVendedorNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<LjvVendedor> result = 
	            (from entity0 in this.DbContext.LJV_VENDEDOR
                  let entity0Al1 = entity0.LJV_LOJA
                orderby entity0.NOME_VENDEDOR ascending, entity0.COD_VENDEDOR ascending
	            
	            	
	            select new LjvVendedor()		
	            {
	            
                CodVendedor = entity0.COD_VENDEDOR
                , DataAtivacao = entity0.DATA_ATIVACAO
                , DataDesativacao = entity0.DATA_DESATIVACAO
                , IdFilialPfj = entity0Al1.ID_FILIAL_PFJ
                , IdLoja = entity0Al1.ID_LOJA
                , IdVendedor = entity0.ID_VENDEDOR
                , Inativo = entity0.INATIVO
                , IndicaGerente = entity0.INDICA_GERENTE
                , IndicaOperadorCaixa = entity0.INDICA_OPERADOR_CAIXA
                , NomeVendedor = entity0.NOME_VENDEDOR
                , Senha = entity0.SENHA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for LJV_VENDEDOR
	    	string[] bmDisabledLjvVendedorList = this.GetEDM().GetFilteringDisabledList("LJV_VENDEDOR");
	    	if (bmDisabledLjvVendedorList.Length > 0)
	    	{
	
	    		if (bmDisabledLjvVendedorList.Contains("LJV_VENDEDOR.COD_VENDEDOR"))
	    		{
	    			result.Add("LjvVendedor|CodVendedor");
	    			result.Add("LjvVendedor|LJV_VENDEDOR.COD_VENDEDOR");
	    		}
	
	    		if (bmDisabledLjvVendedorList.Contains("LJV_VENDEDOR.DATA_ATIVACAO"))
	    		{
	    			result.Add("LjvVendedor|DataAtivacao");
	    			result.Add("LjvVendedor|LJV_VENDEDOR.DATA_ATIVACAO");
	    		}
	
	    		if (bmDisabledLjvVendedorList.Contains("LJV_VENDEDOR.DATA_DESATIVACAO"))
	    		{
	    			result.Add("LjvVendedor|DataDesativacao");
	    			result.Add("LjvVendedor|LJV_VENDEDOR.DATA_DESATIVACAO");
	    		}
	
	    		if (bmDisabledLjvVendedorList.Contains("LJV_VENDEDOR.ID_VENDEDOR"))
	    		{
	    			result.Add("LjvVendedor|IdVendedor");
	    			result.Add("LjvVendedor|LJV_VENDEDOR.ID_VENDEDOR");
	    		}
	
	    		if (bmDisabledLjvVendedorList.Contains("LJV_VENDEDOR.INATIVO"))
	    		{
	    			result.Add("LjvVendedor|Inativo");
	    			result.Add("LjvVendedor|LJV_VENDEDOR.INATIVO");
	    		}
	
	    		if (bmDisabledLjvVendedorList.Contains("LJV_VENDEDOR.INDICA_GERENTE"))
	    		{
	    			result.Add("LjvVendedor|IndicaGerente");
	    			result.Add("LjvVendedor|LJV_VENDEDOR.INDICA_GERENTE");
	    		}
	
	    		if (bmDisabledLjvVendedorList.Contains("LJV_VENDEDOR.INDICA_OPERADOR_CAIXA"))
	    		{
	    			result.Add("LjvVendedor|IndicaOperadorCaixa");
	    			result.Add("LjvVendedor|LJV_VENDEDOR.INDICA_OPERADOR_CAIXA");
	    		}
	
	    		if (bmDisabledLjvVendedorList.Contains("LJV_VENDEDOR.NOME_VENDEDOR"))
	    		{
	    			result.Add("LjvVendedor|NomeVendedor");
	    			result.Add("LjvVendedor|LJV_VENDEDOR.NOME_VENDEDOR");
	    		}
	
	    		if (bmDisabledLjvVendedorList.Contains("LJV_VENDEDOR.SENHA"))
	    		{
	    			result.Add("LjvVendedor|Senha");
	    			result.Add("LjvVendedor|LJV_VENDEDOR.SENHA");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get LjvVendedor By EntitySearchId.
	    public IQueryable<LjvVendedor> GetLjvVendedorByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetLjvVendedorByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get LjvVendedor By EntitySearchId.
	    public IQueryable<LjvVendedor> GetLjvVendedorByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetLjvVendedorByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get LjvVendedor By Example.
	    [Ignore]
	    public IQueryable<LjvVendedor> GetLjvVendedorByExample(LjvVendedor entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetLjvVendedorByEntitySearch(queryAnalysis);
	    }
			
	    //Get LjvVendedor By Example.
	    [Ignore]
	    public IQueryable<LjvVendedor> GetLjvVendedorByExampleNoAssociations(LjvVendedor entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetLjvVendedorByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public LjvVendedor GetLjvVendedorByKey(Int32 idVendedor)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("LjvVendedor");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdVendedor"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idVendedor));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetLjvVendedorByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get LjvVendedorByEntitySearch.
	    public IQueryable<LjvVendedor> GetLjvVendedorByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LjvVendedor));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<LjvVendedor> result = 
	            (from entity0 in this.DbContext.LJV_VENDEDOR.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.LJV_LOJA
                orderby entity0.NOME_VENDEDOR ascending, entity0.COD_VENDEDOR ascending
	            
	            	
	            select new LjvVendedor()		
	            {
	            
                CodVendedor = entity0.COD_VENDEDOR
                , DataAtivacao = entity0.DATA_ATIVACAO
                , DataDesativacao = entity0.DATA_DESATIVACAO
                , IdFilialPfj = entity0Al1.ID_FILIAL_PFJ
                , IdLoja = entity0Al1.ID_LOJA
                , IdVendedor = entity0.ID_VENDEDOR
                , Inativo = entity0.INATIVO
                , IndicaGerente = entity0.INDICA_GERENTE
                , IndicaOperadorCaixa = entity0.INDICA_OPERADOR_CAIXA
                , NomeVendedor = entity0.NOME_VENDEDOR
                , Senha = entity0.SENHA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get LjvVendedorByEntitySearchNoAssociations.
	    public IQueryable<LjvVendedor> GetLjvVendedorByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LjvVendedor));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<LjvVendedor> result = 
	            (from entity0 in this.DbContext.LJV_VENDEDOR.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.LJV_LOJA
                orderby entity0.NOME_VENDEDOR ascending, entity0.COD_VENDEDOR ascending
	            
	            	
	            select new LjvVendedor()		
	            {
	            
                CodVendedor = entity0.COD_VENDEDOR
                , DataAtivacao = entity0.DATA_ATIVACAO
                , DataDesativacao = entity0.DATA_DESATIVACAO
                , IdFilialPfj = entity0Al1.ID_FILIAL_PFJ
                , IdLoja = entity0Al1.ID_LOJA
                , IdVendedor = entity0.ID_VENDEDOR
                , Inativo = entity0.INATIVO
                , IndicaGerente = entity0.INDICA_GERENTE
                , IndicaOperadorCaixa = entity0.INDICA_OPERADOR_CAIXA
                , NomeVendedor = entity0.NOME_VENDEDOR
                , Senha = entity0.SENHA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get PagedLjvVendedor.
	    public IQueryable<LjvVendedor> GetPagedLjvVendedor(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LjvVendedor));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<LjvVendedor> result = 
	            (from entity0 in this.DbContext.LJV_VENDEDOR.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.LJV_LOJA
                orderby entity0.ID_VENDEDOR ascending
	            
	            	
	            select new LjvVendedor()		
	            {
	            
                CodVendedor = entity0.COD_VENDEDOR
                , DataAtivacao = entity0.DATA_ATIVACAO
                , DataDesativacao = entity0.DATA_DESATIVACAO
                , IdFilialPfj = entity0Al1.ID_FILIAL_PFJ
                , IdLoja = entity0Al1.ID_LOJA
                , IdVendedor = entity0.ID_VENDEDOR
                , Inativo = entity0.INATIVO
                , IndicaGerente = entity0.INDICA_GERENTE
                , IndicaOperadorCaixa = entity0.INDICA_OPERADOR_CAIXA
                , NomeVendedor = entity0.NOME_VENDEDOR
                , Senha = entity0.SENHA
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetLjvVendedorCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LjvVendedor));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.LJV_VENDEDOR.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.LJV_LOJA
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update LjvVendedor.
	    public void UpdateLjvVendedor(LjvVendedor entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert LjvVendedor.
	    public void InsertLjvVendedor(LjvVendedor entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete LjvVendedor.
	    public void DeleteLjvVendedor(LjvVendedor entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}