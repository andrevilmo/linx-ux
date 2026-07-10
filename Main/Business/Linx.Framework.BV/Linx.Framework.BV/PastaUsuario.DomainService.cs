					
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

namespace Linx.Framework.BV.PastaUsuario
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_MODULO_MENU.UID_MODULO_MENU", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[Pastas dos Usuários ];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsPastaUsuario];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[UidPastaUsuario];ReadOnly[false];Entities[TCS_MODULO_MENU:UidPastaUsuario];SubQueryInfo[];EdmEntityName[TCS_MODULO_MENU];EntityRelations[MODULO_MENU_SUPERIOR(TCS_MODULO_MENU)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsPastaUsuario")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.PastaUsuario.TcsPastaUsuario")]
	public partial class TcsPastaUsuario : Linx.Data.Entity
	{

	

	    public TcsPastaUsuario() : this(true) { }

	    public TcsPastaUsuario(bool setDefaults) 
	    {

	        if (setDefaults)
	        {
	        	        IdLinx = 0;
	        	        Usuario = String.Empty;
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
	 

	    //Extensibility Partial Method Definitions For DescPastaUsuario
	    partial void OnDescPastaUsuarioChanging(System.String value);
	    partial void OnDescPastaUsuarioChanged();

	    private System.String _DescPastaUsuario;

	    [DataMember(IsRequired = true, Name = "DescPastaUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Desc Modulo Menu", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_MENU.DESC_MODULO_MENU];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_MENU.DESC_MODULO_MENU")]
	    public System.String DescPastaUsuario
	    {
	    	    get
	    	    {
	    	          return _DescPastaUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescPastaUsuario != value)
	    	          {
	    	              this.ValidateProperty("DescPastaUsuario", value);
	    	              this.OnDescPastaUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("DescPastaUsuario");
	    	              this._DescPastaUsuario = value;
	    	              this.RaiseDataMemberChanged("DescPastaUsuario");
	    	              this.OnDescPastaUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLinx
	    partial void OnIdLinxChanging(Int32 value);
	    partial void OnIdLinxChanged();

	    private Int32 _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[0];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_MENU.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_MENU.ID_LINX")]
	    public Int32 IdLinx
	    {
	    	    get
	    	    {
	    	          return _IdLinx;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLinx != value)
	    	          {
	    	              this.ValidateProperty("IdLinx", value);
	    	              this.OnIdLinxChanging(value);
	    	              this.RaiseDataMemberChanging("IdLinx");
	    	              this._IdLinx = value;
	    	              this.RaiseDataMemberChanged("IdLinx");
	    	              this.OnIdLinxChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For OrdemNavegacao
	    partial void OnOrdemNavegacaoChanging(Byte value);
	    partial void OnOrdemNavegacaoChanged();

	    private Byte _OrdemNavegacao;

	    [DataMember(IsRequired = true, Name = "OrdemNavegacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ordem Navegacao", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_MENU.ORDEM_NAVEGACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_MENU.ORDEM_NAVEGACAO")]
	    public Byte OrdemNavegacao
	    {
	    	    get
	    	    {
	    	          return _OrdemNavegacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._OrdemNavegacao != value)
	    	          {
	    	              this.ValidateProperty("OrdemNavegacao", value);
	    	              this.OnOrdemNavegacaoChanging(value);
	    	              this.RaiseDataMemberChanging("OrdemNavegacao");
	    	              this._OrdemNavegacao = value;
	    	              this.RaiseDataMemberChanged("OrdemNavegacao");
	    	              this.OnOrdemNavegacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For TemFilhos
	    partial void OnTemFilhosChanging(bool value);
	    partial void OnTemFilhosChanged();

	    private bool _TemFilhos;

	    [DataMember(IsRequired = true, Name = "TemFilhos", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[false];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="false")]
	    public bool TemFilhos
	    {
	    	    get
	    	    {
	    	          return _TemFilhos;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemFilhos != value)
	    	          {
	    	              this.ValidateProperty("TemFilhos", value);
	    	              this.OnTemFilhosChanging(value);
	    	              this.RaiseDataMemberChanging("TemFilhos");
	    	              this._TemFilhos = value;
	    	              this.RaiseDataMemberChanged("TemFilhos");
	    	              this.OnTemFilhosChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidPastaUsuario
	    partial void OnUidPastaUsuarioChanging(System.Guid value);
	    partial void OnUidPastaUsuarioChanged();

	    private System.Guid _UidPastaUsuario;

	    [DataMember(IsRequired = true, Name = "UidPastaUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Modulo Menu", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_MENU.UID_MODULO_MENU];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_MENU.UID_MODULO_MENU")]
	    public System.Guid UidPastaUsuario
	    {
	    	    get
	    	    {
	    	          return _UidPastaUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidPastaUsuario != value)
	    	          {
	    	              this.ValidateProperty("UidPastaUsuario", value);
	    	              this.OnUidPastaUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("UidPastaUsuario");
	    	              this._UidPastaUsuario = value;
	    	              this.RaiseDataMemberChanged("UidPastaUsuario");
	    	              this.OnUidPastaUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidPastaUsuarioSuperior
	    partial void OnUidPastaUsuarioSuperiorChanging(System.Nullable<System.Guid> value);
	    partial void OnUidPastaUsuarioSuperiorChanged();

	    private System.Nullable<System.Guid> _UidPastaUsuarioSuperior;

	    [DataMember(Name = "UidPastaUsuarioSuperior", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Modulo Menu Superior Fk", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_MENU.UID_MODULO_MENU_SUPERIOR_FK];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_MENU.UID_MODULO_MENU_SUPERIOR_FK")]
	    public System.Nullable<System.Guid> UidPastaUsuarioSuperior
	    {
	    	    get
	    	    {
	    	          return _UidPastaUsuarioSuperior;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidPastaUsuarioSuperior != value)
	    	          {
	    	              this.ValidateProperty("UidPastaUsuarioSuperior", value);
	    	              this.OnUidPastaUsuarioSuperiorChanging(value);
	    	              this.RaiseDataMemberChanging("UidPastaUsuarioSuperior");
	    	              this._UidPastaUsuarioSuperior = value;
	    	              this.RaiseDataMemberChanged("UidPastaUsuarioSuperior");
	    	              this.OnUidPastaUsuarioSuperiorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Usuario
	    partial void OnUsuarioChanging(string value);
	    partial void OnUsuarioChanged();

	    private string _Usuario;

	    [DataMember(IsRequired = true, Name = "Usuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[String.Empty];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[String.Empty];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="String.Empty")]
	    public string Usuario
	    {
	    	    get
	    	    {
	    	          return _Usuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._Usuario != value)
	    	          {
	    	              this.ValidateProperty("Usuario", value);
	    	              this.OnUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("Usuario");
	    	              this._Usuario = value;
	    	              this.RaiseDataMemberChanged("Usuario");
	    	              this.OnUsuarioChanged();
	    	          }
	    	    }
	    }

	    private System.Guid _TemporaryUidPastaUsuario;
	    [DataMember(Name = "TemporaryUidPastaUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Modulo Menu (Tmp)", Description="Temporary Key", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public System.Guid TemporaryUidPastaUsuario
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryUidPastaUsuario.IsNullOrEmpty())
	    	                this._TemporaryUidPastaUsuario = this._UidPastaUsuario;
	    	          return this._TemporaryUidPastaUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryUidPastaUsuario != value)
	    	              this._TemporaryUidPastaUsuario = value;
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_MODULO_MENU").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_MODULO_MENU), QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO_MENU" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_MENU.ID_LINX", Source = "IdLinx", Target = "ID_LINX", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO_MENU", RelationPropertyName = "TCS_MODULO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_MENU.ORDEM_NAVEGACAO", Source = "OrdemNavegacao", Target = "ORDEM_NAVEGACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO_MENU", RelationPropertyName = "TCS_MODULO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_MENU.UID_MODULO_MENU", Source = "UidPastaUsuario", Target = "UID_MODULO_MENU", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO_MENU", RelationPropertyName = "TCS_MODULO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_MENU.DESC_MODULO_MENU", Source = "DescPastaUsuario", Target = "DESC_MODULO_MENU", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO_MENU", RelationPropertyName = "TCS_MODULO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_MODULO_MENU.UID_MODULO_MENU_SUPERIOR_FK", Source = "UidPastaUsuarioSuperior", Target = "UID_MODULO_MENU_SUPERIOR_FK", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_MODULO_MENU", RelationPropertyName = "TCS_MODULO_MENU" });

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

		

	[LinxPublicationView(PrimaryKeys="TcsDocumentoUsuario.EntityUniqueKey", IsUpdatable=false, EdmName="Linx.Framework.ControleSistema.BM.ControleSistemaContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsDocumentoUsuario];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];Entities[TCS_TRANSACAO:UidDocumentoUsuario];SubQueryInfo[];EdmEntityName[TCS_TRANSACAO];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsDocumentoUsuario")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.PastaUsuario.TcsDocumentoUsuario")]
	public partial class TcsDocumentoUsuario : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For Conteudo
	    partial void OnConteudoChanging(string value);
	    partial void OnConteudoChanged();

	    private string _Conteudo;

	    [DataMember(IsRequired = true, Name = "Conteudo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="Conteudo do Arquivo", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[String.Empty];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="String.Empty")]
	    public string Conteudo
	    {
	    	    get
	    	    {
	    	          return _Conteudo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Conteudo != value)
	    	          {
	    	              this.ValidateProperty("Conteudo", value);
	    	              this.OnConteudoChanging(value);
	    	              this.RaiseDataMemberChanging("Conteudo");
	    	              this._Conteudo = value;
	    	              this.RaiseDataMemberChanged("Conteudo");
	    	              this.OnConteudoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DocumentoLinx
	    partial void OnDocumentoLinxChanging(bool value);
	    partial void OnDocumentoLinxChanged();

	    private bool _DocumentoLinx;

	    [DataMember(IsRequired = true, Name = "DocumentoLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[false];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="false")]
	    public bool DocumentoLinx
	    {
	    	    get
	    	    {
	    	          return _DocumentoLinx;
	    	    }
	    	    set
	    	    {
	    	          if (this._DocumentoLinx != value)
	    	          {
	    	              this.ValidateProperty("DocumentoLinx", value);
	    	              this.OnDocumentoLinxChanging(value);
	    	              this.RaiseDataMemberChanging("DocumentoLinx");
	    	              this._DocumentoLinx = value;
	    	              this.RaiseDataMemberChanged("DocumentoLinx");
	    	              this.OnDocumentoLinxChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidTransacao
	    partial void OnUidTransacaoChanging(System.Guid value);
	    partial void OnUidTransacaoChanged();

	    private System.Guid _UidTransacao;

	    [DataMember(IsRequired = true, Name = "UidTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[TCS_TRANSACAO.UID_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_TRANSACAO.UID_TRANSACAO")]
	    public System.Guid UidTransacao
	    {
	    	    get
	    	    {
	    	          return _UidTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidTransacao != value)
	    	          {
	    	              this.ValidateProperty("UidTransacao", value);
	    	              this.OnUidTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("UidTransacao");
	    	              this._UidTransacao = value;
	    	              this.RaiseDataMemberChanged("UidTransacao");
	    	              this.OnUidTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For OrdemNavegacao
	    partial void OnOrdemNavegacaoChanging(Byte value);
	    partial void OnOrdemNavegacaoChanged();

	    private Byte _OrdemNavegacao;

	    [DataMember(IsRequired = true, Name = "OrdemNavegacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[0];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="0")]
	    public Byte OrdemNavegacao
	    {
	    	    get
	    	    {
	    	          return _OrdemNavegacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._OrdemNavegacao != value)
	    	          {
	    	              this.ValidateProperty("OrdemNavegacao", value);
	    	              this.OnOrdemNavegacaoChanging(value);
	    	              this.RaiseDataMemberChanging("OrdemNavegacao");
	    	              this._OrdemNavegacao = value;
	    	              this.RaiseDataMemberChanged("OrdemNavegacao");
	    	              this.OnOrdemNavegacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidDocumentoUsuario
	    partial void OnUidDocumentoUsuarioChanging(Guid value);
	    partial void OnUidDocumentoUsuarioChanged();

	    private Guid _UidDocumentoUsuario;

	    [DataMember(IsRequired = true, Name = "UidDocumentoUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Transacao", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[Guid.Empty];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Guid.Empty")]
	    public Guid UidDocumentoUsuario
	    {
	    	    get
	    	    {
	    	          return _UidDocumentoUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidDocumentoUsuario != value)
	    	          {
	    	              this.ValidateProperty("UidDocumentoUsuario", value);
	    	              this.OnUidDocumentoUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("UidDocumentoUsuario");
	    	              this._UidDocumentoUsuario = value;
	    	              this.RaiseDataMemberChanged("UidDocumentoUsuario");
	    	              this.OnUidDocumentoUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescDocumentoUsuario
	    partial void OnDescDocumentoUsuarioChanging(string value);
	    partial void OnDescDocumentoUsuarioChanged();

	    private string _DescDocumentoUsuario;

	    [DataMember(IsRequired = true, Name = "DescDocumentoUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[''];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="\"\"")]
	    public string DescDocumentoUsuario
	    {
	    	    get
	    	    {
	    	          return _DescDocumentoUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescDocumentoUsuario != value)
	    	          {
	    	              this.ValidateProperty("DescDocumentoUsuario", value);
	    	              this.OnDescDocumentoUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("DescDocumentoUsuario");
	    	              this._DescDocumentoUsuario = value;
	    	              this.RaiseDataMemberChanged("DescDocumentoUsuario");
	    	              this.OnDescDocumentoUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidObjeto
	    partial void OnUidObjetoChanging(Guid value);
	    partial void OnUidObjetoChanged();

	    private Guid _UidObjeto;

	    [DataMember(IsRequired = true, Name = "UidObjeto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [RoundtripOriginal()]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[Guid.Empty];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="Guid.Empty")]
	    public Guid UidObjeto
	    {
	    	    get
	    	    {
	    	          return _UidObjeto;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidObjeto != value)
	    	          {
	    	              this.ValidateProperty("UidObjeto", value);
	    	              this.OnUidObjetoChanging(value);
	    	              this.RaiseDataMemberChanging("UidObjeto");
	    	              this._UidObjeto = value;
	    	              this.RaiseDataMemberChanged("UidObjeto");
	    	              this.OnUidObjetoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For EntityUniqueKey
	    partial void OnEntityUniqueKeyChanging(System.Guid value);
	    partial void OnEntityUniqueKeyChanged();

	    private System.Guid _entityUniqueKey;
	    [DataMember(Name = "EntityUniqueKey", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [RoundtripOriginal()]
	    [Editable(true)]
	    [Key()]
	    public System.Guid EntityUniqueKey
	    {
	    	    get
	    	    {
	    	          if (_entityUniqueKey.IsNullOrEmpty())
	    	             _entityUniqueKey =  System.Guid.NewGuid();
	    	          return _entityUniqueKey; 
	    	    }
	    	    set
	    	    {
	    	          if (this._entityUniqueKey != value)
	    	          {
	    	              this.ValidateProperty("EntityUniqueKey", value);
	    	              this.OnEntityUniqueKeyChanging(value);
	    	              this.RaiseDataMemberChanging("EntityUniqueKey");
	    	              this._entityUniqueKey = value;
	    	              this.RaiseDataMemberChanged("EntityUniqueKey");
	    	              this.OnEntityUniqueKeyChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ControleSistemaContext.TCS_TRANSACAO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.ControleSistema.BM.TCS_TRANSACAO), QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_TRANSACAO.UID_TRANSACAO", Source = "UidTransacao", Target = "UID_TRANSACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ControleSistemaContext.TCS_TRANSACAO", RelationPropertyName = "TCS_TRANSACAO" });

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
	[DomainIdentifier("ProcessorOverviewPastaUsuarioDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class PastaUsuarioDomainService : DomainService, IDataServiceContext 
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

	
	    private Linx.Framework.ControleSistema.BM.ControleSistemaContext _dbContext;
	    protected Linx.Framework.ControleSistema.BM.ControleSistemaContext DbContext 
	    { 
	    	get 
	    	{
	        	if (this._dbContext == null)
	        	{
	        		this._dbContext = new Linx.Framework.ControleSistema.BM.ControleSistemaContext(connectionString, this.Headers);
	        		((System.Data.Entity.Infrastructure.IObjectContextAdapter)this._dbContext).ObjectContext.CommandTimeout = 180;
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

		
	    public PastaUsuarioDomainService() : this("", null, null){ }
	    public PastaUsuarioDomainService(string connectionString) : this(connectionString, null, null) { }
	    public PastaUsuarioDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public PastaUsuarioDomainService(Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public PastaUsuarioDomainService(string connectionString, Linx.Framework.ControleSistema.BM.ControleSistemaContext dataContext, Dictionary<string, string> headers) : base() 
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
	
		

	        if (entityName.InList("Linx.Framework.BV.PastaUsuario.TcsPastaUsuario"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsPastaUsuario",
	        			NameSpace = "Linx.Framework.BV.PastaUsuario",
	        			ParentClassName = null,	
	        			DisplayName = "Pastas dos Usuários ",
	        			ClearMethodName = "ClearTcsPastaUsuario",
	        			QueryMethodName  = "GetPagedTcsPastaUsuario",	
	        			CountingMethodName  = "GetTcsPastaUsuario" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.PastaUsuario.TcsPastaUsuario"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.PastaUsuario.TcsPastaUsuario"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.PastaUsuario.TcsDocumentoUsuario"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsDocumentoUsuario",
	        			NameSpace = "Linx.Framework.BV.PastaUsuario",
	        			ParentClassName = null,	
	        			DisplayName = "TcsDocumentoUsuario",
	        			ClearMethodName = "ClearTcsDocumentoUsuario",
	        			QueryMethodName  = "GetPagedTcsDocumentoUsuario",	
	        			CountingMethodName  = "GetTcsDocumentoUsuario" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.PastaUsuario.TcsDocumentoUsuario"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.PastaUsuario.TcsDocumentoUsuario"), forceAll: forceAll)
	        		});
	        }
	
            return result;
        }
	
	    [Ignore]
	    public string[] GetClientDomains()
        {	


             return new string[] { "Framework_MobileDataDomains", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.MobileDataDomains.res", System.Reflection.Assembly.GetExecutingAssembly()) };	

        }

	    [Ignore]
	    public string[] GetClientService()
        {	


             return new string[] { "Framework_pastaUsuarioService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.pastaUsuarioService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	

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
	    //Clear TcsPastaUsuario.
	    public IEnumerable<TcsPastaUsuario> ClearTcsPastaUsuario()
	    {
	        List<TcsPastaUsuario> result = new List<TcsPastaUsuario>();
	        result.Add(new TcsPastaUsuario(false));	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsDocumentoUsuario.
	    public IEnumerable<TcsDocumentoUsuario> ClearTcsDocumentoUsuario()
	    {
	        List<TcsDocumentoUsuario> result = new List<TcsDocumentoUsuario>();
	        result.Add(new TcsDocumentoUsuario());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Ignore]
	    //Get TcsPastaUsuario.
	    public IQueryable<TcsPastaUsuario> GetTcsPastaUsuario()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsPastaUsuario> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_MENU
	            
	            	
	            select new TcsPastaUsuario()		
	            {
	            
                DescPastaUsuario = entity0.DESC_MODULO_MENU
                , IdLinx = entity0.ID_LINX
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
                , TemFilhos = false
                , UidPastaUsuario = entity0.UID_MODULO_MENU
                , UidPastaUsuarioSuperior = entity0.UID_MODULO_MENU_SUPERIOR_FK
                , Usuario = String.Empty
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsPastaUsuarioNoAssociations.
	    public IQueryable<TcsPastaUsuario> GetTcsPastaUsuarioNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsPastaUsuario> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_MENU
	            
	            	
	            select new TcsPastaUsuario()		
	            {
	            
                DescPastaUsuario = entity0.DESC_MODULO_MENU
                , IdLinx = entity0.ID_LINX
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
                , TemFilhos = false
                , UidPastaUsuario = entity0.UID_MODULO_MENU
                , UidPastaUsuarioSuperior = entity0.UID_MODULO_MENU_SUPERIOR_FK
                , Usuario = String.Empty
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsDocumentoUsuario.
	    public IQueryable<TcsDocumentoUsuario> GetTcsDocumentoUsuario()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsDocumentoUsuario> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO
	            
	            	
	            select new TcsDocumentoUsuario()		
	            {
	            
                Conteudo = String.Empty
                , DocumentoLinx = false
                , UidTransacao = entity0.UID_TRANSACAO
                , OrdemNavegacao = 0
                , UidDocumentoUsuario = Guid.Empty
                , DescDocumentoUsuario = ""
                , UidObjeto = Guid.Empty
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsDocumentoUsuarioNoAssociations.
	    public IQueryable<TcsDocumentoUsuario> GetTcsDocumentoUsuarioNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsDocumentoUsuario> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO
	            
	            	
	            select new TcsDocumentoUsuario()		
	            {
	            
                Conteudo = String.Empty
                , DocumentoLinx = false
                , UidTransacao = entity0.UID_TRANSACAO
                , OrdemNavegacao = 0
                , UidDocumentoUsuario = Guid.Empty
                , DescDocumentoUsuario = ""
                , UidObjeto = Guid.Empty
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	result.Add("TcsPastaUsuario|TemFilhos");
	    	result.Add("TcsPastaUsuario|false");
	    	result.Add("TcsPastaUsuario|Usuario");
	    	result.Add("TcsPastaUsuario|String.Empty");
	    	//Add filtering disabled property for TCS_MODULO_MENU
	    	string[] bmDisabledTcsPastaUsuarioList = this.GetEDM().GetFilteringDisabledList("TCS_MODULO_MENU");
	    	if (bmDisabledTcsPastaUsuarioList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsPastaUsuarioList.Contains("TCS_MODULO_MENU.DESC_MODULO_MENU"))
	    		{
	    			result.Add("TcsPastaUsuario|DescPastaUsuario");
	    			result.Add("TcsPastaUsuario|TCS_MODULO_MENU.DESC_MODULO_MENU");
	    		}
	
	    		if (bmDisabledTcsPastaUsuarioList.Contains("TCS_MODULO_MENU.ID_LINX"))
	    		{
	    			result.Add("TcsPastaUsuario|IdLinx");
	    			result.Add("TcsPastaUsuario|TCS_MODULO_MENU.ID_LINX");
	    		}
	
	    		if (bmDisabledTcsPastaUsuarioList.Contains("TCS_MODULO_MENU.ORDEM_NAVEGACAO"))
	    		{
	    			result.Add("TcsPastaUsuario|OrdemNavegacao");
	    			result.Add("TcsPastaUsuario|TCS_MODULO_MENU.ORDEM_NAVEGACAO");
	    		}
	
	    		if (bmDisabledTcsPastaUsuarioList.Contains("TCS_MODULO_MENU.UID_MODULO_MENU"))
	    		{
	    			result.Add("TcsPastaUsuario|UidPastaUsuario");
	    			result.Add("TcsPastaUsuario|TCS_MODULO_MENU.UID_MODULO_MENU");
	    		}
	
	    		if (bmDisabledTcsPastaUsuarioList.Contains("TCS_MODULO_MENU.UID_MODULO_MENU_SUPERIOR_FK"))
	    		{
	    			result.Add("TcsPastaUsuario|UidPastaUsuarioSuperior");
	    			result.Add("TcsPastaUsuario|TCS_MODULO_MENU.UID_MODULO_MENU_SUPERIOR_FK");
	    		}
	    	}
	    	result.Add("TcsDocumentoUsuario|Conteudo");
	    	result.Add("TcsDocumentoUsuario|String.Empty");
	    	result.Add("TcsDocumentoUsuario|DocumentoLinx");
	    	result.Add("TcsDocumentoUsuario|false");
	    	result.Add("TcsDocumentoUsuario|UidTransacao");
	    	result.Add("TcsDocumentoUsuario|TCS_TRANSACAO.UID_TRANSACAO");
	    	result.Add("TcsDocumentoUsuario|OrdemNavegacao");
	    	result.Add("TcsDocumentoUsuario|0");
	    	result.Add("TcsDocumentoUsuario|UidDocumentoUsuario");
	    	result.Add("TcsDocumentoUsuario|Guid.Empty");
	    	result.Add("TcsDocumentoUsuario|DescDocumentoUsuario");
	    	result.Add("TcsDocumentoUsuario|''");
	    	result.Add("TcsDocumentoUsuario|UidObjeto");
	    	result.Add("TcsDocumentoUsuario|Guid.Empty");
	    	//Add filtering disabled property for TCS_TRANSACAO
	    	string[] bmDisabledTcsDocumentoUsuarioList = this.GetEDM().GetFilteringDisabledList("TCS_TRANSACAO");
	    	if (bmDisabledTcsDocumentoUsuarioList.Length > 0)
	    	{
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
	    //Get TcsPastaUsuario By EntitySearchId.
	    public IQueryable<TcsPastaUsuario> GetTcsPastaUsuarioByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsPastaUsuarioByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get TcsPastaUsuario By EntitySearchId.
	    public IQueryable<TcsPastaUsuario> GetTcsPastaUsuarioByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsPastaUsuarioByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get TcsDocumentoUsuario By EntitySearchId.
	    public IQueryable<TcsDocumentoUsuario> GetTcsDocumentoUsuarioByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsDocumentoUsuarioByEntitySearch(queryAnalysis);
	    }
				
	    [Query(HasSideEffects = false)]
	    //Get TcsDocumentoUsuario By EntitySearchId.
	    public IQueryable<TcsDocumentoUsuario> GetTcsDocumentoUsuarioByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsDocumentoUsuarioByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get TcsPastaUsuario By Example.
	    [Ignore]
	    public IQueryable<TcsPastaUsuario> GetTcsPastaUsuarioByExample(TcsPastaUsuario entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsPastaUsuarioByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsPastaUsuario By Example.
	    [Ignore]
	    public IQueryable<TcsPastaUsuario> GetTcsPastaUsuarioByExampleNoAssociations(TcsPastaUsuario entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsPastaUsuarioByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsDocumentoUsuario By Example.
	    [Ignore]
	    public IQueryable<TcsDocumentoUsuario> GetTcsDocumentoUsuarioByExample(TcsDocumentoUsuario entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsDocumentoUsuarioByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsDocumentoUsuario By Example.
	    [Ignore]
	    public IQueryable<TcsDocumentoUsuario> GetTcsDocumentoUsuarioByExampleNoAssociations(TcsDocumentoUsuario entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsDocumentoUsuarioByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public TcsPastaUsuario GetTcsPastaUsuarioByKey(System.Guid uidPastaUsuario)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsPastaUsuario");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidPastaUsuario"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, uidPastaUsuario));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsPastaUsuarioByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }



	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get TcsPastaUsuarioByEntitySearch.
	    public IQueryable<TcsPastaUsuario> GetTcsPastaUsuarioByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsPastaUsuario));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

		
	
	        
		
	        
	
	        IQueryable<TcsPastaUsuario> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_MENU.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsPastaUsuario()		
	            {
	            
                DescPastaUsuario = entity0.DESC_MODULO_MENU
                , IdLinx = entity0.ID_LINX
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
                , TemFilhos = false
                , UidPastaUsuario = entity0.UID_MODULO_MENU
                , UidPastaUsuarioSuperior = entity0.UID_MODULO_MENU_SUPERIOR_FK
                , Usuario = String.Empty
		
	            }
	            );
	
	        SetTcsPastaUsuarioBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsPastaUsuarioByEntitySearchNoAssociations.
	    public IQueryable<TcsPastaUsuario> GetTcsPastaUsuarioByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsPastaUsuario));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

		
	
	        
		
	        
	
	        IQueryable<TcsPastaUsuario> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_MENU.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsPastaUsuario()		
	            {
	            
                DescPastaUsuario = entity0.DESC_MODULO_MENU
                , IdLinx = entity0.ID_LINX
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
                , TemFilhos = false
                , UidPastaUsuario = entity0.UID_MODULO_MENU
                , UidPastaUsuarioSuperior = entity0.UID_MODULO_MENU_SUPERIOR_FK
                , Usuario = String.Empty
		
	            }
	            );
	
	        SetTcsPastaUsuarioBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
	
	    [Ignore()]
	    private void SetTcsPastaUsuarioBusinessFilter(ref IQueryable<TcsPastaUsuario> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "TcsPastaUsuario"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "TemFilhos" || e.Value.ToString() == "false")))
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
	    										bool tmpTemFilhos1 = (bool)value;
	    										query = from r in query where r.TemFilhos == tmpTemFilhos1 select r;
	    										break;
	    									case "!=":
	    										bool tmpTemFilhos2 = (bool)value;
	    										query = from r in query where r.TemFilhos != tmpTemFilhos2 select r;
	    										break;

	
	    									default:
	    										break;
	    								}                                
	    							}
	    						}
        					} 

    
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "Usuario" || e.Value.ToString() == "String.Empty")))
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

	
	    								//Adjust Like operator
	    								if (operatorValue == "Like")
	    								{
	    								    string enteredVal = value.ToString();
	    								    if (enteredVal.Right(1) == "%" && enteredVal.Left(1) == "%")
	    								    {
	    								        enteredVal = enteredVal.Replace("%", "");
	    								        operatorValue = "Contains";
	    								    }
	    								    else if (enteredVal.Left(1) == "%")
	    								    {
	    								        enteredVal = enteredVal.Replace("%", "");
	    								        operatorValue = "EndsWith";
	    								    }
	    								    else
	    								    {
	    								        enteredVal = enteredVal.Replace("%", "");
	    								        operatorValue = "StartsWith";
	    								    }
	    								    value = enteredVal;
	    								}

	
	    								switch (operatorValue)
	    								{
	    									case "==":
	    										string tmpUsuario1 = (string)value;
	    										query = from r in query where r.Usuario == tmpUsuario1 select r;
	    										break;
	    									case "!=":
	    										string tmpUsuario2 = (string)value;
	    										query = from r in query where r.Usuario != tmpUsuario2 select r;
	    										break;

	
	    									case "Contains":
	    										string tmpUsuario7 = (string)value;
	    									    query = from r in query where r.Usuario.Contains(tmpUsuario7) select r;
	    									    break;
	    									case "StartsWith":
	    										string tmpUsuario8 = (string)value;
	    									    query = from r in query where r.Usuario.StartsWith(tmpUsuario8) select r;
	    									    break;
	    									case "EndsWith":
	    										string tmpUsuario9 = (string)value;
	    									    query = from r in query where r.Usuario.EndsWith(tmpUsuario9) select r;
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


		
	
	    
	    [Ignore]
	    //Get TcsDocumentoUsuarioByEntitySearch.
	    public IQueryable<TcsDocumentoUsuario> GetTcsDocumentoUsuarioByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsDocumentoUsuario));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

		
	
	        
		
	        
	
	        IQueryable<TcsDocumentoUsuario> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsDocumentoUsuario()		
	            {
	            
                Conteudo = String.Empty
                , DocumentoLinx = false
                , UidTransacao = entity0.UID_TRANSACAO
                , OrdemNavegacao = 0
                , UidDocumentoUsuario = Guid.Empty
                , DescDocumentoUsuario = ""
                , UidObjeto = Guid.Empty
		
	            }
	            );
	
	        SetTcsDocumentoUsuarioBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsDocumentoUsuarioByEntitySearchNoAssociations.
	    public IQueryable<TcsDocumentoUsuario> GetTcsDocumentoUsuarioByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsDocumentoUsuario));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

		
	
	        
		
	        
	
	        IQueryable<TcsDocumentoUsuario> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsDocumentoUsuario()		
	            {
	            
                Conteudo = String.Empty
                , DocumentoLinx = false
                , UidTransacao = entity0.UID_TRANSACAO
                , OrdemNavegacao = 0
                , UidDocumentoUsuario = Guid.Empty
                , DescDocumentoUsuario = ""
                , UidObjeto = Guid.Empty
		
	            }
	            );
	
	        SetTcsDocumentoUsuarioBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
	
	    [Ignore()]
	    private void SetTcsDocumentoUsuarioBusinessFilter(ref IQueryable<TcsDocumentoUsuario> query, List<EntitySearch> entitySearchList)
	    {
	    		int idxElement;
	    		string operatorValue;
	    		object value;
	    		//Get query by functions
	    		if (entitySearchList.Count > 0)
	    		{
	    			foreach (EntitySearch search in entitySearchList.Where(e => e.EntityName == "TcsDocumentoUsuario"))
	    			{

	
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "Conteudo" || e.Value.ToString() == "String.Empty")))
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

	
	    								//Adjust Like operator
	    								if (operatorValue == "Like")
	    								{
	    								    string enteredVal = value.ToString();
	    								    if (enteredVal.Right(1) == "%" && enteredVal.Left(1) == "%")
	    								    {
	    								        enteredVal = enteredVal.Replace("%", "");
	    								        operatorValue = "Contains";
	    								    }
	    								    else if (enteredVal.Left(1) == "%")
	    								    {
	    								        enteredVal = enteredVal.Replace("%", "");
	    								        operatorValue = "EndsWith";
	    								    }
	    								    else
	    								    {
	    								        enteredVal = enteredVal.Replace("%", "");
	    								        operatorValue = "StartsWith";
	    								    }
	    								    value = enteredVal;
	    								}

	
	    								switch (operatorValue)
	    								{
	    									case "==":
	    										string tmpConteudo1 = (string)value;
	    										query = from r in query where r.Conteudo == tmpConteudo1 select r;
	    										break;
	    									case "!=":
	    										string tmpConteudo2 = (string)value;
	    										query = from r in query where r.Conteudo != tmpConteudo2 select r;
	    										break;

	
	    									case "Contains":
	    										string tmpConteudo7 = (string)value;
	    									    query = from r in query where r.Conteudo.Contains(tmpConteudo7) select r;
	    									    break;
	    									case "StartsWith":
	    										string tmpConteudo8 = (string)value;
	    									    query = from r in query where r.Conteudo.StartsWith(tmpConteudo8) select r;
	    									    break;
	    									case "EndsWith":
	    										string tmpConteudo9 = (string)value;
	    									    query = from r in query where r.Conteudo.EndsWith(tmpConteudo9) select r;
	    									    break;

	
	    									default:
	    										break;
	    								}                                
	    							}
	    						}
        					} 

    
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "DocumentoLinx" || e.Value.ToString() == "false")))
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
	    										bool tmpDocumentoLinx1 = (bool)value;
	    										query = from r in query where r.DocumentoLinx == tmpDocumentoLinx1 select r;
	    										break;
	    									case "!=":
	    										bool tmpDocumentoLinx2 = (bool)value;
	    										query = from r in query where r.DocumentoLinx != tmpDocumentoLinx2 select r;
	    										break;

	
	    									default:
	    										break;
	    								}                                
	    							}
	    						}
        					} 

    
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "UidTransacao" || e.Value.ToString() == "TCS_TRANSACAO.UID_TRANSACAO")))
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
	    										System.Guid tmpUidTransacao1 = (System.Guid)value;
	    										query = from r in query where r.UidTransacao == tmpUidTransacao1 select r;
	    										break;
	    									case "!=":
	    										System.Guid tmpUidTransacao2 = (System.Guid)value;
	    										query = from r in query where r.UidTransacao != tmpUidTransacao2 select r;
	    										break;

	
	    									default:
	    										break;
	    								}                                
	    							}
	    						}
        					} 

    
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "OrdemNavegacao" || e.Value.ToString() == "0")))
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
	    										Byte tmpOrdemNavegacao1 = (Byte)value;
	    										query = from r in query where r.OrdemNavegacao == tmpOrdemNavegacao1 select r;
	    										break;
	    									case "!=":
	    										Byte tmpOrdemNavegacao2 = (Byte)value;
	    										query = from r in query where r.OrdemNavegacao != tmpOrdemNavegacao2 select r;
	    										break;

	
	    									case "<":
	    										Byte tmpOrdemNavegacao3 = (Byte)value;
	    										query = from r in query where r.OrdemNavegacao < tmpOrdemNavegacao3 select r;
	    										break;
	    									case "<=":
	    										Byte tmpOrdemNavegacao4 = (Byte)value;
	    										query = from r in query where r.OrdemNavegacao <= tmpOrdemNavegacao4 select r;
	    										break;
	    									case ">":
	    										Byte tmpOrdemNavegacao5 = (Byte)value;
	    										query = from r in query where r.OrdemNavegacao > tmpOrdemNavegacao5 select r;
	    										break;
	    									case ">=":
	    										Byte tmpOrdemNavegacao6 = (Byte)value;
	    										query = from r in query where r.OrdemNavegacao >= tmpOrdemNavegacao6 select r;
	    										break;	

	
	    									default:
	    										break;
	    								}                                
	    							}
	    						}
        					} 

    
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "UidDocumentoUsuario" || e.Value.ToString() == "Guid.Empty")))
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
	    										Guid tmpUidDocumentoUsuario1 = (Guid)value;
	    										query = from r in query where r.UidDocumentoUsuario == tmpUidDocumentoUsuario1 select r;
	    										break;
	    									case "!=":
	    										Guid tmpUidDocumentoUsuario2 = (Guid)value;
	    										query = from r in query where r.UidDocumentoUsuario != tmpUidDocumentoUsuario2 select r;
	    										break;

	
	    									default:
	    										break;
	    								}                                
	    							}
	    						}
        					} 

    
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "DescDocumentoUsuario" || e.Value.ToString() == "''")))
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

	
	    								//Adjust Like operator
	    								if (operatorValue == "Like")
	    								{
	    								    string enteredVal = value.ToString();
	    								    if (enteredVal.Right(1) == "%" && enteredVal.Left(1) == "%")
	    								    {
	    								        enteredVal = enteredVal.Replace("%", "");
	    								        operatorValue = "Contains";
	    								    }
	    								    else if (enteredVal.Left(1) == "%")
	    								    {
	    								        enteredVal = enteredVal.Replace("%", "");
	    								        operatorValue = "EndsWith";
	    								    }
	    								    else
	    								    {
	    								        enteredVal = enteredVal.Replace("%", "");
	    								        operatorValue = "StartsWith";
	    								    }
	    								    value = enteredVal;
	    								}

	
	    								switch (operatorValue)
	    								{
	    									case "==":
	    										string tmpDescDocumentoUsuario1 = (string)value;
	    										query = from r in query where r.DescDocumentoUsuario == tmpDescDocumentoUsuario1 select r;
	    										break;
	    									case "!=":
	    										string tmpDescDocumentoUsuario2 = (string)value;
	    										query = from r in query where r.DescDocumentoUsuario != tmpDescDocumentoUsuario2 select r;
	    										break;

	
	    									case "Contains":
	    										string tmpDescDocumentoUsuario7 = (string)value;
	    									    query = from r in query where r.DescDocumentoUsuario.Contains(tmpDescDocumentoUsuario7) select r;
	    									    break;
	    									case "StartsWith":
	    										string tmpDescDocumentoUsuario8 = (string)value;
	    									    query = from r in query where r.DescDocumentoUsuario.StartsWith(tmpDescDocumentoUsuario8) select r;
	    									    break;
	    									case "EndsWith":
	    										string tmpDescDocumentoUsuario9 = (string)value;
	    									    query = from r in query where r.DescDocumentoUsuario.EndsWith(tmpDescDocumentoUsuario9) select r;
	    									    break;

	
	    									default:
	    										break;
	    								}                                
	    							}
	    						}
        					} 

    
	    				foreach (var exp in search.Expressions.Where(e => e.Name == "Field" && (e.Value.ToString() == "UidObjeto" || e.Value.ToString() == "Guid.Empty")))
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
	    										Guid tmpUidObjeto1 = (Guid)value;
	    										query = from r in query where r.UidObjeto == tmpUidObjeto1 select r;
	    										break;
	    									case "!=":
	    										Guid tmpUidObjeto2 = (Guid)value;
	    										query = from r in query where r.UidObjeto != tmpUidObjeto2 select r;
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
	
			
	
	    
	    [Ignore]
	    //Get PagedTcsPastaUsuario.
	    public IQueryable<TcsPastaUsuario> GetPagedTcsPastaUsuario(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsPastaUsuario));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

		
	
	        
		
	        
	
	        IQueryable<TcsPastaUsuario> result = 
	            (from entity0 in this.DbContext.TCS_MODULO_MENU.Where(dynQuery, parameters.ToArray())
                orderby entity0.UID_MODULO_MENU ascending
	            
	            	
	            select new TcsPastaUsuario()		
	            {
	            
                DescPastaUsuario = entity0.DESC_MODULO_MENU
                , IdLinx = entity0.ID_LINX
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
                , TemFilhos = false
                , UidPastaUsuario = entity0.UID_MODULO_MENU
                , UidPastaUsuarioSuperior = entity0.UID_MODULO_MENU_SUPERIOR_FK
                , Usuario = String.Empty
		
	            }
	            ).Skip(skip).Take(take);
	
	        SetTcsPastaUsuarioBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsPastaUsuarioCounting(string serializedEntitySearch)
	    {	
		 	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsPastaUsuario));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_MODULO_MENU.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsDocumentoUsuario.
	    public IQueryable<TcsDocumentoUsuario> GetPagedTcsDocumentoUsuario(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsDocumentoUsuario));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

		
	
	        
		
	        
	
	        IQueryable<TcsDocumentoUsuario> result = 
	            (from entity0 in this.DbContext.TCS_TRANSACAO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new TcsDocumentoUsuario()		
	            {
	            
                Conteudo = String.Empty
                , DocumentoLinx = false
                , UidTransacao = entity0.UID_TRANSACAO
                , OrdemNavegacao = 0
                , UidDocumentoUsuario = Guid.Empty
                , DescDocumentoUsuario = ""
                , UidObjeto = Guid.Empty
		
	            }
	            ).Skip(skip).Take(take);
	
	        SetTcsDocumentoUsuarioBusinessFilter(ref result, entitySearchList);

			
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsDocumentoUsuarioCounting(string serializedEntitySearch)
	    {	
		 	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsDocumentoUsuario));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_TRANSACAO.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update TcsPastaUsuario.
	    public void UpdateTcsPastaUsuario(TcsPastaUsuario entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsPastaUsuario.
	    public void InsertTcsPastaUsuario(TcsPastaUsuario entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsPastaUsuario.
	    public void DeleteTcsPastaUsuario(TcsPastaUsuario entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsDocumentoUsuario.
	    public void UpdateTcsDocumentoUsuario(TcsDocumentoUsuario entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsDocumentoUsuario.
	    public void InsertTcsDocumentoUsuario(TcsDocumentoUsuario entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsDocumentoUsuario.
	    public void DeleteTcsDocumentoUsuario(TcsDocumentoUsuario entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}