					
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

namespace Linx.Framework.BV.Layout
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_OBJETO_CONTEUDO.ID_OBJETO_CONTEUDO", IsUpdatable=true, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsLayout];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[TCS_OBJETO_CONTEUDO];EntityRelations[TCS_LAYOUT_LISTA(TCS_LAYOUT)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsLayout")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.Layout.TcsLayout")]
	public partial class TcsLayout : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For ConteudoXml
	    partial void OnConteudoXmlChanging(string value);
	    partial void OnConteudoXmlChanged();

	    private string _ConteudoXml;

	    [DataMember(IsRequired = true, Name = "ConteudoXml", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Conteudo Xml", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO_CONTEUDO.CONTEUDO_XML];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_CONTEUDO.CONTEUDO_XML")]
	    public string ConteudoXml
	    {
	    	    get
	    	    {
	    	          return _ConteudoXml;
	    	    }
	    	    set
	    	    {
	    	          if (this._ConteudoXml != value)
	    	          {
	    	              this.ValidateProperty("ConteudoXml", value);
	    	              this.OnConteudoXmlChanging(value);
	    	              this.RaiseDataMemberChanging("ConteudoXml");
	    	              this._ConteudoXml = value;
	    	              this.RaiseDataMemberChanged("ConteudoXml");
	    	              this.OnConteudoXmlChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescLayout
	    partial void OnDescLayoutChanging(string value);
	    partial void OnDescLayoutChanged();

	    private string _DescLayout;

	    [DataMember(Name = "DescLayout", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Desc Layout", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO_CONTEUDO.TCS_LAYOUT_LISTA.DESC_LAYOUT];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_CONTEUDO.TCS_LAYOUT_LISTA.DESC_LAYOUT")]
	    public string DescLayout
	    {
	    	    get
	    	    {
	    	          return _DescLayout;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescLayout != value)
	    	          {
	    	              this.ValidateProperty("DescLayout", value);
	    	              this.OnDescLayoutChanging(value);
	    	              this.RaiseDataMemberChanging("DescLayout");
	    	              this._DescLayout = value;
	    	              this.RaiseDataMemberChanged("DescLayout");
	    	              this.OnDescLayoutChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Detalhes
	    partial void OnDetalhesChanging(string value);
	    partial void OnDetalhesChanged();

	    private string _Detalhes;

	    [DataMember(Name = "Detalhes", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Detalhes", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(500)]
	    [FunctionalPoint("Precision[500:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO_CONTEUDO.TCS_LAYOUT_LISTA.DETALHES];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_CONTEUDO.TCS_LAYOUT_LISTA.DETALHES")]
	    public string Detalhes
	    {
	    	    get
	    	    {
	    	          return _Detalhes;
	    	    }
	    	    set
	    	    {
	    	          if (this._Detalhes != value)
	    	          {
	    	              this.ValidateProperty("Detalhes", value);
	    	              this.OnDetalhesChanging(value);
	    	              this.RaiseDataMemberChanging("Detalhes");
	    	              this._Detalhes = value;
	    	              this.RaiseDataMemberChanged("Detalhes");
	    	              this.OnDetalhesChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Idioma
	    partial void OnIdiomaChanging(string value);
	    partial void OnIdiomaChanged();

	    private string _Idioma;

	    [DataMember(Name = "Idioma", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Idioma", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(18)]
	    [FunctionalPoint("Precision[18:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO_CONTEUDO.TCS_LAYOUT_LISTA.IDIOMA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_CONTEUDO.TCS_LAYOUT_LISTA.IDIOMA")]
	    public string Idioma
	    {
	    	    get
	    	    {
	    	          return _Idioma;
	    	    }
	    	    set
	    	    {
	    	          if (this._Idioma != value)
	    	          {
	    	              this.ValidateProperty("Idioma", value);
	    	              this.OnIdiomaChanging(value);
	    	              this.RaiseDataMemberChanging("Idioma");
	    	              this._Idioma = value;
	    	              this.RaiseDataMemberChanged("Idioma");
	    	              this.OnIdiomaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdObjeto
	    partial void OnIdObjetoChanging(long value);
	    partial void OnIdObjetoChanged();

	    private long _IdObjeto;

	    [DataMember(IsRequired = true, Name = "IdObjeto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Objeto", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO_CONTEUDO.ID_OBJETO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_CONTEUDO.ID_OBJETO")]
	    public long IdObjeto
	    {
	    	    get
	    	    {
	    	          return _IdObjeto;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdObjeto != value)
	    	          {
	    	              this.ValidateProperty("IdObjeto", value);
	    	              this.OnIdObjetoChanging(value);
	    	              this.RaiseDataMemberChanging("IdObjeto");
	    	              this._IdObjeto = value;
	    	              this.RaiseDataMemberChanged("IdObjeto");
	    	              this.OnIdObjetoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdObjetoConteudo
	    partial void OnIdObjetoConteudoChanging(System.Nullable<long> value);
	    partial void OnIdObjetoConteudoChanged();

	    private System.Nullable<long> _IdObjetoConteudo;

	    [DataMember(Name = "IdObjetoConteudo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Objeto Conteudo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO_CONTEUDO.TCS_LAYOUT_LISTA.ID_OBJETO_CONTEUDO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_CONTEUDO.TCS_LAYOUT_LISTA.ID_OBJETO_CONTEUDO")]
	    public System.Nullable<long> IdObjetoConteudo
	    {
	    	    get
	    	    {
	    	          return _IdObjetoConteudo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdObjetoConteudo != value)
	    	          {
	    	              this.ValidateProperty("IdObjetoConteudo", value);
	    	              this.OnIdObjetoConteudoChanging(value);
	    	              this.RaiseDataMemberChanging("IdObjetoConteudo");
	    	              this._IdObjetoConteudo = value;
	    	              this.RaiseDataMemberChanged("IdObjetoConteudo");
	    	              this.OnIdObjetoConteudoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdObjetoConteudo_Fk
	    partial void OnIdObjetoConteudo_FkChanging(long value);
	    partial void OnIdObjetoConteudo_FkChanged();

	    private long _IdObjetoConteudo_Fk;

	    [DataMember(IsRequired = true, Name = "IdObjetoConteudo_Fk", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Objeto Conteudo_Fk", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO_CONTEUDO.ID_OBJETO_CONTEUDO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_CONTEUDO.ID_OBJETO_CONTEUDO")]
	    public long IdObjetoConteudo_Fk
	    {
	    	    get
	    	    {
	    	          return _IdObjetoConteudo_Fk;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdObjetoConteudo_Fk != value)
	    	          {
	    	              this.ValidateProperty("IdObjetoConteudo_Fk", value);
	    	              this.OnIdObjetoConteudo_FkChanging(value);
	    	              this.RaiseDataMemberChanging("IdObjetoConteudo_Fk");
	    	              this._IdObjetoConteudo_Fk = value;
	    	              this.RaiseDataMemberChanged("IdObjetoConteudo_Fk");
	    	              this.OnIdObjetoConteudo_FkChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Inativo
	    partial void OnInativoChanging(System.Nullable<bool> value);
	    partial void OnInativoChanged();

	    private System.Nullable<bool> _Inativo;

	    [DataMember(Name = "Inativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO_CONTEUDO.TCS_LAYOUT_LISTA.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_CONTEUDO.TCS_LAYOUT_LISTA.INATIVO")]
	    public System.Nullable<bool> Inativo
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
	    //Extensibility Partial Method Definitions For LayoutPadrao
	    partial void OnLayoutPadraoChanging(System.Nullable<bool> value);
	    partial void OnLayoutPadraoChanged();

	    private System.Nullable<bool> _LayoutPadrao;

	    [DataMember(Name = "LayoutPadrao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Layout Padrao", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO_CONTEUDO.TCS_LAYOUT_LISTA.LAYOUT_PADRAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_CONTEUDO.TCS_LAYOUT_LISTA.LAYOUT_PADRAO")]
	    public System.Nullable<bool> LayoutPadrao
	    {
	    	    get
	    	    {
	    	          return _LayoutPadrao;
	    	    }
	    	    set
	    	    {
	    	          if (this._LayoutPadrao != value)
	    	          {
	    	              this.ValidateProperty("LayoutPadrao", value);
	    	              this.OnLayoutPadraoChanging(value);
	    	              this.RaiseDataMemberChanging("LayoutPadrao");
	    	              this._LayoutPadrao = value;
	    	              this.RaiseDataMemberChanged("LayoutPadrao");
	    	              this.OnLayoutPadraoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxConteudoObjeto
	    partial void OnLxConteudoObjetoChanging(byte value);
	    partial void OnLxConteudoObjetoChanged();

	    private byte _LxConteudoObjeto;

	    [DataMember(IsRequired = true, Name = "LxConteudoObjeto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lx Conteudo Objeto", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[TipoConteudoObjeto];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO_CONTEUDO.LX_CONTEUDO_OBJETO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_CONTEUDO.LX_CONTEUDO_OBJETO")]
	    public byte LxConteudoObjeto
	    {
	    	    get
	    	    {
	    	          return _LxConteudoObjeto;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxConteudoObjeto != value)
	    	          {
	    	              this.ValidateProperty("LxConteudoObjeto", value);
	    	              this.OnLxConteudoObjetoChanging(value);
	    	              this.RaiseDataMemberChanging("LxConteudoObjeto");
	    	              this._LxConteudoObjeto = value;
	    	              this.RaiseDataMemberChanged("LxConteudoObjeto");
	    	              this.OnLxConteudoObjetoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoLayout
	    partial void OnLxTipoLayoutChanging(System.Nullable<byte> value);
	    partial void OnLxTipoLayoutChanged();

	    private System.Nullable<byte> _LxTipoLayout;

	    [DataMember(Name = "LxTipoLayout", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lx Tipo Layout", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[TipoLayout];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO_CONTEUDO.TCS_LAYOUT_LISTA.LX_TIPO_LAYOUT];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_CONTEUDO.TCS_LAYOUT_LISTA.LX_TIPO_LAYOUT")]
	    public System.Nullable<byte> LxTipoLayout
	    {
	    	    get
	    	    {
	    	          return _LxTipoLayout;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoLayout != value)
	    	          {
	    	              this.ValidateProperty("LxTipoLayout", value);
	    	              this.OnLxTipoLayoutChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoLayout");
	    	              this._LxTipoLayout = value;
	    	              this.RaiseDataMemberChanged("LxTipoLayout");
	    	              this.OnLxTipoLayoutChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidObjetoConteudo
	    partial void OnUidObjetoConteudoChanging(System.Nullable<Guid> value);
	    partial void OnUidObjetoConteudoChanged();

	    private System.Nullable<Guid> _UidObjetoConteudo;

	    [DataMember(Name = "UidObjetoConteudo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Objeto Conteudo", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO_CONTEUDO.TCS_LAYOUT_LISTA.UID_OBJETO_CONTEUDO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_CONTEUDO.TCS_LAYOUT_LISTA.UID_OBJETO_CONTEUDO")]
	    public System.Nullable<Guid> UidObjetoConteudo
	    {
	    	    get
	    	    {
	    	          return _UidObjetoConteudo;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidObjetoConteudo != value)
	    	          {
	    	              this.ValidateProperty("UidObjetoConteudo", value);
	    	              this.OnUidObjetoConteudoChanging(value);
	    	              this.RaiseDataMemberChanging("UidObjetoConteudo");
	    	              this._UidObjetoConteudo = value;
	    	              this.RaiseDataMemberChanged("UidObjetoConteudo");
	    	              this.OnUidObjetoConteudoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UltAtualizacao
	    partial void OnUltAtualizacaoChanging(System.Nullable<DateTime> value);
	    partial void OnUltAtualizacaoChanged();

	    private System.Nullable<DateTime> _UltAtualizacao;

	    [DataMember(Name = "UltAtualizacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ult Atualizacao", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_OBJETO_CONTEUDO.TCS_LAYOUT_LISTA.ULT_ATUALIZACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_OBJETO_CONTEUDO.TCS_LAYOUT_LISTA.ULT_ATUALIZACAO")]
	    public System.Nullable<DateTime> UltAtualizacao
	    {
	    	    get
	    	    {
	    	          return _UltAtualizacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._UltAtualizacao != value)
	    	          {
	    	              this.ValidateProperty("UltAtualizacao", value);
	    	              this.OnUltAtualizacaoChanging(value);
	    	              this.RaiseDataMemberChanging("UltAtualizacao");
	    	              this._UltAtualizacao = value;
	    	              this.RaiseDataMemberChanged("UltAtualizacao");
	    	              this.OnUltAtualizacaoChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_OBJETO_CONTEUDO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_OBJETO_CONTEUDO), QualifiedEntitySetName = "ControleSistemaContext.TCS_OBJETO_CONTEUDO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_CONTEUDO.ID_OBJETO", Source = "IdObjeto", Target = "ID_OBJETO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_OBJETO_CONTEUDO", RelationPropertyName = "TCS_OBJETO_CONTEUDO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_CONTEUDO.CONTEUDO_XML", Source = "ConteudoXml", Target = "CONTEUDO_XML", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_OBJETO_CONTEUDO", RelationPropertyName = "TCS_OBJETO_CONTEUDO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_CONTEUDO.ID_OBJETO_CONTEUDO", Source = "IdObjetoConteudo_Fk", Target = "ID_OBJETO_CONTEUDO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_OBJETO_CONTEUDO", RelationPropertyName = "TCS_OBJETO_CONTEUDO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_CONTEUDO.LX_CONTEUDO_OBJETO", Source = "LxConteudoObjeto", Target = "LX_CONTEUDO_OBJETO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_OBJETO_CONTEUDO", RelationPropertyName = "TCS_OBJETO_CONTEUDO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_CONTEUDO.TCS_LAYOUT_LISTA.ID_OBJETO_CONTEUDO", Source = "IdObjetoConteudo", Target = "ID_OBJETO_CONTEUDO", TargetKeyName = "ID_OBJETO_CONTEUDO", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ControleSistemaContext.TCS_LAYOUT", RelationPropertyName = "TCS_LAYOUT_LISTA" });
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_LAYOUT").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_LAYOUT), QualifiedEntitySetName = "ControleSistemaContext.TCS_LAYOUT" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_CONTEUDO.TCS_LAYOUT_LISTA.IDIOMA", Source = "Idioma", Target = "IDIOMA", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_LAYOUT", RelationPropertyName = "TCS_LAYOUT_LISTA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_CONTEUDO.TCS_LAYOUT_LISTA.INATIVO", Source = "Inativo", Target = "INATIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_LAYOUT", RelationPropertyName = "TCS_LAYOUT_LISTA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_CONTEUDO.TCS_LAYOUT_LISTA.DETALHES", Source = "Detalhes", Target = "DETALHES", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_LAYOUT", RelationPropertyName = "TCS_LAYOUT_LISTA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_CONTEUDO.TCS_LAYOUT_LISTA.DESC_LAYOUT", Source = "DescLayout", Target = "DESC_LAYOUT", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_LAYOUT", RelationPropertyName = "TCS_LAYOUT_LISTA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_CONTEUDO.TCS_LAYOUT_LISTA.LAYOUT_PADRAO", Source = "LayoutPadrao", Target = "LAYOUT_PADRAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_LAYOUT", RelationPropertyName = "TCS_LAYOUT_LISTA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_CONTEUDO.TCS_LAYOUT_LISTA.LX_TIPO_LAYOUT", Source = "LxTipoLayout", Target = "LX_TIPO_LAYOUT", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_LAYOUT", RelationPropertyName = "TCS_LAYOUT_LISTA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_CONTEUDO.TCS_LAYOUT_LISTA.ULT_ATUALIZACAO", Source = "UltAtualizacao", Target = "ULT_ATUALIZACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_LAYOUT", RelationPropertyName = "TCS_LAYOUT_LISTA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_CONTEUDO.TCS_LAYOUT_LISTA.ID_OBJETO_CONTEUDO", Source = "IdObjetoConteudo", Target = "ID_OBJETO_CONTEUDO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_LAYOUT", RelationPropertyName = "TCS_LAYOUT_LISTA" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_OBJETO_CONTEUDO.TCS_LAYOUT_LISTA.UID_OBJETO_CONTEUDO", Source = "UidObjetoConteudo", Target = "UID_OBJETO_CONTEUDO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_LAYOUT", RelationPropertyName = "TCS_LAYOUT_LISTA" });

	        return dataMaps;
	    }
	
	    #endregion MetaData Methods

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxConteudoObjetoValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoConteudoObjeto.GetValues();
	    }
	    private string _lxConteudoObjetoName;
	    [DataMember(IsRequired = false, Name = "LxConteudoObjetoName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Lx Conteudo Objeto", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxConteudoObjetoName
	    {
	    	    get { if (this.LxConteudoObjeto.IsNull()) { _lxConteudoObjetoName = String.Empty; } else { string key = this.LxConteudoObjeto.ToString(); var dmValues = this.GetLxConteudoObjetoValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxConteudoObjetoName) _lxConteudoObjetoName = domainName; } return _lxConteudoObjetoName; } set { _lxConteudoObjetoName = value;  }
	    }
	    public Dictionary<string, string> GetLxTipoLayoutValues()
	    {
	    	    return Linx.Framework.BV.Domains.TipoLayout.GetValues();
	    }
	    private string _lxTipoLayoutName;
	    [DataMember(IsRequired = false, Name = "LxTipoLayoutName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Lx Tipo Layout", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoLayoutName
	    {
	    	    get { if (this.LxTipoLayout.IsNull()) { _lxTipoLayoutName = String.Empty; } else { string key = this.LxTipoLayout.ToString(); var dmValues = this.GetLxTipoLayoutValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoLayoutName) _lxTipoLayoutName = domainName; } return _lxTipoLayoutName; } set { _lxTipoLayoutName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	//////////////////////// DomainService Class V1 ///////////////////////
	///////////////////////////////////////////////////////////////////////
	[EnableClientAccess()]	
	[DomainIdentifier("ProcessorOverviewLayoutDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class LayoutDomainService : DomainService, IDataServiceContext 
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

		
	    public LayoutDomainService() : this("", null, null) { }
	    public LayoutDomainService(string connectionString) : this(connectionString, null, null) { }
	    public LayoutDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public LayoutDomainService(Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public LayoutDomainService(string connectionString, Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : base() 
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
	
		

	        if (entityName.InList("Linx.Framework.BV.Layout.TcsLayout"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsLayout",
	        			NameSpace = "Linx.Framework.BV.Layout",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsLayout",
	        			ClearMethodName = "ClearTcsLayout",
	        			QueryMethodName  = "GetPagedTcsLayout",	
	        			CountingMethodName  = "GetTcsLayout" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.Layout.TcsLayout"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.Layout.TcsLayout"), forceAll: forceAll)
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

         		    return new string[] { "Framework_LayoutClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.LayoutClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_layoutService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.layoutService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear TcsLayout.
	    public IEnumerable<TcsLayout> ClearTcsLayout()
	    {
	        List<TcsLayout> result = new List<TcsLayout>();
	        result.Add(new TcsLayout());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsLayout.
	    public IQueryable<TcsLayout> GetTcsLayout()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsLayout> result = 
	            (from entity0 in this.DbContext.TCS_OBJETO_CONTEUDO
                  let entity0Al1 = entity0.TCS_LAYOUT_LISTA
	            
	            	
	            select new TcsLayout()		
	            {
	            
                ConteudoXml = entity0.CONTEUDO_XML
                , DescLayout = entity0Al1.DESC_LAYOUT
                , Detalhes = entity0Al1.DETALHES
                , Idioma = entity0Al1.IDIOMA
                , IdObjeto = entity0.ID_OBJETO
                , IdObjetoConteudo = entity0Al1.ID_OBJETO_CONTEUDO
                , IdObjetoConteudo_Fk = entity0.ID_OBJETO_CONTEUDO
                , Inativo = entity0Al1.INATIVO
                , LayoutPadrao = entity0Al1.LAYOUT_PADRAO
                , LxConteudoObjeto = entity0.LX_CONTEUDO_OBJETO
                , LxConteudoObjetoName = ((entity0.LX_CONTEUDO_OBJETO) == 3 ? "Configuração de Exportação para Excel" : ((entity0.LX_CONTEUDO_OBJETO) == 4 ? "Configuração de Exportação para Report" : ((entity0.LX_CONTEUDO_OBJETO) == 6 ? "Gravação de Layout para Grid" : ((entity0.LX_CONTEUDO_OBJETO) == 1 ? "Layout" : ((entity0.LX_CONTEUDO_OBJETO) == 2 ? "Mídia" : ((entity0.LX_CONTEUDO_OBJETO) == 5 ? "Gravação de Layout para Pivot Table" : ""))))))
                , LxTipoLayout = entity0Al1.LX_TIPO_LAYOUT
                , LxTipoLayoutName = ((entity0Al1.LX_TIPO_LAYOUT) == 1 ? "Layout do Sistema" : ((entity0Al1.LX_TIPO_LAYOUT) == 2 ? "Layout do Usuário" : ""))
                , UidObjetoConteudo = entity0Al1.UID_OBJETO_CONTEUDO
                , UltAtualizacao = entity0Al1.ULT_ATUALIZACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsLayoutNoAssociations.
	    public IQueryable<TcsLayout> GetTcsLayoutNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsLayout> result = 
	            (from entity0 in this.DbContext.TCS_OBJETO_CONTEUDO
                  let entity0Al1 = entity0.TCS_LAYOUT_LISTA
	            
	            	
	            select new TcsLayout()		
	            {
	            
                ConteudoXml = entity0.CONTEUDO_XML
                , DescLayout = entity0Al1.DESC_LAYOUT
                , Detalhes = entity0Al1.DETALHES
                , Idioma = entity0Al1.IDIOMA
                , IdObjeto = entity0.ID_OBJETO
                , IdObjetoConteudo = entity0Al1.ID_OBJETO_CONTEUDO
                , IdObjetoConteudo_Fk = entity0.ID_OBJETO_CONTEUDO
                , Inativo = entity0Al1.INATIVO
                , LayoutPadrao = entity0Al1.LAYOUT_PADRAO
                , LxConteudoObjeto = entity0.LX_CONTEUDO_OBJETO
                , LxConteudoObjetoName = ((entity0.LX_CONTEUDO_OBJETO) == 3 ? "Configuração de Exportação para Excel" : ((entity0.LX_CONTEUDO_OBJETO) == 4 ? "Configuração de Exportação para Report" : ((entity0.LX_CONTEUDO_OBJETO) == 6 ? "Gravação de Layout para Grid" : ((entity0.LX_CONTEUDO_OBJETO) == 1 ? "Layout" : ((entity0.LX_CONTEUDO_OBJETO) == 2 ? "Mídia" : ((entity0.LX_CONTEUDO_OBJETO) == 5 ? "Gravação de Layout para Pivot Table" : ""))))))
                , LxTipoLayout = entity0Al1.LX_TIPO_LAYOUT
                , LxTipoLayoutName = ((entity0Al1.LX_TIPO_LAYOUT) == 1 ? "Layout do Sistema" : ((entity0Al1.LX_TIPO_LAYOUT) == 2 ? "Layout do Usuário" : ""))
                , UidObjetoConteudo = entity0Al1.UID_OBJETO_CONTEUDO
                , UltAtualizacao = entity0Al1.ULT_ATUALIZACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for TCS_OBJETO_CONTEUDO
	    	string[] bmDisabledTcsLayoutList = this.GetEDM().GetFilteringDisabledList("TCS_OBJETO_CONTEUDO");
	    	if (bmDisabledTcsLayoutList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsLayoutList.Contains("TCS_OBJETO_CONTEUDO.CONTEUDO_XML"))
	    		{
	    			result.Add("TcsLayout|ConteudoXml");
	    			result.Add("TcsLayout|TCS_OBJETO_CONTEUDO.CONTEUDO_XML");
	    		}
	
	    		if (bmDisabledTcsLayoutList.Contains("TCS_OBJETO_CONTEUDO.ID_OBJETO"))
	    		{
	    			result.Add("TcsLayout|IdObjeto");
	    			result.Add("TcsLayout|TCS_OBJETO_CONTEUDO.ID_OBJETO");
	    		}
	
	    		if (bmDisabledTcsLayoutList.Contains("TCS_OBJETO_CONTEUDO.ID_OBJETO_CONTEUDO"))
	    		{
	    			result.Add("TcsLayout|IdObjetoConteudo_Fk");
	    			result.Add("TcsLayout|TCS_OBJETO_CONTEUDO.ID_OBJETO_CONTEUDO");
	    		}
	
	    		if (bmDisabledTcsLayoutList.Contains("TCS_OBJETO_CONTEUDO.LX_CONTEUDO_OBJETO"))
	    		{
	    			result.Add("TcsLayout|LxConteudoObjeto");
	    			result.Add("TcsLayout|TCS_OBJETO_CONTEUDO.LX_CONTEUDO_OBJETO");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get TcsLayout By EntitySearchId.
	    public IQueryable<TcsLayout> GetTcsLayoutByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsLayoutByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get TcsLayout By EntitySearchId.
	    public IQueryable<TcsLayout> GetTcsLayoutByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetTcsLayoutByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get TcsLayout By Example.
	    [Ignore]
	    public IQueryable<TcsLayout> GetTcsLayoutByExample(TcsLayout entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsLayoutByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsLayout By Example.
	    [Ignore]
	    public IQueryable<TcsLayout> GetTcsLayoutByExampleNoAssociations(TcsLayout entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsLayoutByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public TcsLayout GetTcsLayoutByKey(long idObjetoConteudo_Fk)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsLayout");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdObjetoConteudo_Fk"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idObjetoConteudo_Fk));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsLayoutByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get TcsLayoutByEntitySearch.
	    public IQueryable<TcsLayout> GetTcsLayoutByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsLayout));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsLayout> result = 
	            (from entity0 in this.DbContext.TCS_OBJETO_CONTEUDO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_LAYOUT_LISTA
	            
	            	
	            select new TcsLayout()		
	            {
	            
                ConteudoXml = entity0.CONTEUDO_XML
                , DescLayout = entity0Al1.DESC_LAYOUT
                , Detalhes = entity0Al1.DETALHES
                , Idioma = entity0Al1.IDIOMA
                , IdObjeto = entity0.ID_OBJETO
                , IdObjetoConteudo = entity0Al1.ID_OBJETO_CONTEUDO
                , IdObjetoConteudo_Fk = entity0.ID_OBJETO_CONTEUDO
                , Inativo = entity0Al1.INATIVO
                , LayoutPadrao = entity0Al1.LAYOUT_PADRAO
                , LxConteudoObjeto = entity0.LX_CONTEUDO_OBJETO
                , LxConteudoObjetoName = ((entity0.LX_CONTEUDO_OBJETO) == 3 ? "Configuração de Exportação para Excel" : ((entity0.LX_CONTEUDO_OBJETO) == 4 ? "Configuração de Exportação para Report" : ((entity0.LX_CONTEUDO_OBJETO) == 6 ? "Gravação de Layout para Grid" : ((entity0.LX_CONTEUDO_OBJETO) == 1 ? "Layout" : ((entity0.LX_CONTEUDO_OBJETO) == 2 ? "Mídia" : ((entity0.LX_CONTEUDO_OBJETO) == 5 ? "Gravação de Layout para Pivot Table" : ""))))))
                , LxTipoLayout = entity0Al1.LX_TIPO_LAYOUT
                , LxTipoLayoutName = ((entity0Al1.LX_TIPO_LAYOUT) == 1 ? "Layout do Sistema" : ((entity0Al1.LX_TIPO_LAYOUT) == 2 ? "Layout do Usuário" : ""))
                , UidObjetoConteudo = entity0Al1.UID_OBJETO_CONTEUDO
                , UltAtualizacao = entity0Al1.ULT_ATUALIZACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsLayoutByEntitySearchNoAssociations.
	    public IQueryable<TcsLayout> GetTcsLayoutByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsLayout));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsLayout> result = 
	            (from entity0 in this.DbContext.TCS_OBJETO_CONTEUDO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_LAYOUT_LISTA
	            
	            	
	            select new TcsLayout()		
	            {
	            
                ConteudoXml = entity0.CONTEUDO_XML
                , DescLayout = entity0Al1.DESC_LAYOUT
                , Detalhes = entity0Al1.DETALHES
                , Idioma = entity0Al1.IDIOMA
                , IdObjeto = entity0.ID_OBJETO
                , IdObjetoConteudo = entity0Al1.ID_OBJETO_CONTEUDO
                , IdObjetoConteudo_Fk = entity0.ID_OBJETO_CONTEUDO
                , Inativo = entity0Al1.INATIVO
                , LayoutPadrao = entity0Al1.LAYOUT_PADRAO
                , LxConteudoObjeto = entity0.LX_CONTEUDO_OBJETO
                , LxConteudoObjetoName = ((entity0.LX_CONTEUDO_OBJETO) == 3 ? "Configuração de Exportação para Excel" : ((entity0.LX_CONTEUDO_OBJETO) == 4 ? "Configuração de Exportação para Report" : ((entity0.LX_CONTEUDO_OBJETO) == 6 ? "Gravação de Layout para Grid" : ((entity0.LX_CONTEUDO_OBJETO) == 1 ? "Layout" : ((entity0.LX_CONTEUDO_OBJETO) == 2 ? "Mídia" : ((entity0.LX_CONTEUDO_OBJETO) == 5 ? "Gravação de Layout para Pivot Table" : ""))))))
                , LxTipoLayout = entity0Al1.LX_TIPO_LAYOUT
                , LxTipoLayoutName = ((entity0Al1.LX_TIPO_LAYOUT) == 1 ? "Layout do Sistema" : ((entity0Al1.LX_TIPO_LAYOUT) == 2 ? "Layout do Usuário" : ""))
                , UidObjetoConteudo = entity0Al1.UID_OBJETO_CONTEUDO
                , UltAtualizacao = entity0Al1.ULT_ATUALIZACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get PagedTcsLayout.
	    public IQueryable<TcsLayout> GetPagedTcsLayout(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsLayout));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsLayout> result = 
	            (from entity0 in this.DbContext.TCS_OBJETO_CONTEUDO.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_LAYOUT_LISTA
                orderby entity0.ID_OBJETO_CONTEUDO ascending
	            
	            	
	            select new TcsLayout()		
	            {
	            
                ConteudoXml = entity0.CONTEUDO_XML
                , DescLayout = entity0Al1.DESC_LAYOUT
                , Detalhes = entity0Al1.DETALHES
                , Idioma = entity0Al1.IDIOMA
                , IdObjeto = entity0.ID_OBJETO
                , IdObjetoConteudo = entity0Al1.ID_OBJETO_CONTEUDO
                , IdObjetoConteudo_Fk = entity0.ID_OBJETO_CONTEUDO
                , Inativo = entity0Al1.INATIVO
                , LayoutPadrao = entity0Al1.LAYOUT_PADRAO
                , LxConteudoObjeto = entity0.LX_CONTEUDO_OBJETO
                , LxConteudoObjetoName = ((entity0.LX_CONTEUDO_OBJETO) == 3 ? "Configuração de Exportação para Excel" : ((entity0.LX_CONTEUDO_OBJETO) == 4 ? "Configuração de Exportação para Report" : ((entity0.LX_CONTEUDO_OBJETO) == 6 ? "Gravação de Layout para Grid" : ((entity0.LX_CONTEUDO_OBJETO) == 1 ? "Layout" : ((entity0.LX_CONTEUDO_OBJETO) == 2 ? "Mídia" : ((entity0.LX_CONTEUDO_OBJETO) == 5 ? "Gravação de Layout para Pivot Table" : ""))))))
                , LxTipoLayout = entity0Al1.LX_TIPO_LAYOUT
                , LxTipoLayoutName = ((entity0Al1.LX_TIPO_LAYOUT) == 1 ? "Layout do Sistema" : ((entity0Al1.LX_TIPO_LAYOUT) == 2 ? "Layout do Usuário" : ""))
                , UidObjetoConteudo = entity0Al1.UID_OBJETO_CONTEUDO
                , UltAtualizacao = entity0Al1.ULT_ATUALIZACAO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsLayoutCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsLayout));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_OBJETO_CONTEUDO.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_LAYOUT_LISTA
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update TcsLayout.
	    public void UpdateTcsLayout(TcsLayout entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsLayout.
	    public void InsertTcsLayout(TcsLayout entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsLayout.
	    public void DeleteTcsLayout(TcsLayout entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}