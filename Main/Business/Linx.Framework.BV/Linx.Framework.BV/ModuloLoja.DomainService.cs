					
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

namespace Linx.Framework.BV.ModuloLoja
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="LJV_MODULO.ID_MODULO", IsUpdatable=false, EdmName="Linx.Framework.Loja.BM.ConectorPos")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[LjvModulo,LjvModulo.LjvModuloMenu];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdModulo];ReadOnly[false];Entities[LJV_MODULO:IdModulo];SubQueryInfo[];EdmEntityName[LJV_MODULO];EntityRelations[];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "LjvModulo")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.ModuloLoja.LjvModulo")]
	public partial class LjvModulo : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.LjvModuloMenuList != null && this.LjvModuloMenuList.Count() > 0)
	      {
	         foreach (var entity in this.LjvModuloMenuList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.LjvModuloMenuList != null)
	      {
	         foreach (var detail in this.LjvModuloMenuList)
	         {
	            detail.ResetDetails();
	         }
	         this.LjvModuloMenuList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(ModuloLojaDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("LjvModuloMenu"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("LjvModuloMenu");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdModulo"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdModulo));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load LjvModuloMenu and all sub-details
	         if (this.LjvModuloMenuList == null || this.LjvModuloMenuList.Count() == 0)
	         {
	             if (take > 0)
	                 this.LjvModuloMenuList = context.GetPagedLjvModuloMenu(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.LjvModuloMenuList = (from r in context.GetLjvModuloMenuByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _LjvModuloMenuElements = changeSet.ChangeSetEntries.Where(e => e.Entity is LjvModuloMenu && ((LjvModuloMenu)e.Entity).LjvModulo == null && e.Associations == null && e.OriginalAssociations == null && ((LjvModuloMenu)e.Entity).IdModulo == this.IdModulo).ToList();
 	      if (_LjvModuloMenuElements.Count > 0 && this.LjvModuloMenuList.Count() == 0)
 	      {
 	          this.LjvModuloMenuList = _LjvModuloMenuElements.Select(e => (LjvModuloMenu)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _LjvModuloMenuElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((LjvModuloMenu)detail.Entity).LjvModulo = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("LjvModulo", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("LjvModuloMenuList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DescModulo
	    partial void OnDescModuloChanging(System.String value);
	    partial void OnDescModuloChanged();

	    private System.String _DescModulo;

	    [DataMember(IsRequired = true, Name = "DescModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Desc Modulo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(100)]
	    [FunctionalPoint("Precision[100:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_MODULO.DESC_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_MODULO.DESC_MODULO")]
	    public System.String DescModulo
	    {
	    	    get
	    	    {
	    	          return _DescModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescModulo != value)
	    	          {
	    	              this.ValidateProperty("DescModulo", value);
	    	              this.OnDescModuloChanging(value);
	    	              this.RaiseDataMemberChanging("DescModulo");
	    	              this._DescModulo = value;
	    	              this.RaiseDataMemberChanged("DescModulo");
	    	              this.OnDescModuloChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Icone
	    partial void OnIconeChanging(System.String value);
	    partial void OnIconeChanged();

	    private System.String _Icone;

	    [DataMember(Name = "Icone", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Icone", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_MODULO.ICONE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_MODULO.ICONE")]
	    public System.String Icone
	    {
	    	    get
	    	    {
	    	          return _Icone;
	    	    }
	    	    set
	    	    {
	    	          if (this._Icone != value)
	    	          {
	    	              this.ValidateProperty("Icone", value);
	    	              this.OnIconeChanging(value);
	    	              this.RaiseDataMemberChanging("Icone");
	    	              this._Icone = value;
	    	              this.RaiseDataMemberChanged("Icone");
	    	              this.OnIconeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdModulo
	    partial void OnIdModuloChanging(Int64 value);
	    partial void OnIdModuloChanged();

	    private Int64 _IdModulo;

	    [DataMember(IsRequired = true, Name = "IdModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_MODULO.ID_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_MODULO.ID_MODULO")]
	    public Int64 IdModulo
	    {
	    	    get
	    	    {
	    	          return _IdModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdModulo != value)
	    	          {
	    	              this.ValidateProperty("IdModulo", value);
	    	              this.OnIdModuloChanging(value);
	    	              this.RaiseDataMemberChanging("IdModulo");
	    	              this._IdModulo = value;
	    	              this.RaiseDataMemberChanged("IdModulo");
	    	              this.OnIdModuloChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsAplicativo
	    partial void OnIdTcsAplicativoChanging(Int32 value);
	    partial void OnIdTcsAplicativoChanged();

	    private Int32 _IdTcsAplicativo;

	    [DataMember(IsRequired = true, Name = "IdTcsAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Aplicativo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_MODULO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_MODULO.ID_TCS_APLICATIVO")]
	    public Int32 IdTcsAplicativo
	    {
	    	    get
	    	    {
	    	          return _IdTcsAplicativo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAplicativo != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAplicativo", value);
	    	              this.OnIdTcsAplicativoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAplicativo");
	    	              this._IdTcsAplicativo = value;
	    	              this.RaiseDataMemberChanged("IdTcsAplicativo");
	    	              this.OnIdTcsAplicativoChanged();
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
	    [Display(Name = "Inativo", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_MODULO.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_MODULO.INATIVO")]
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
	    //Extensibility Partial Method Definitions For LxCorFundo
	    partial void OnLxCorFundoChanging(System.Nullable<System.Int32> value);
	    partial void OnLxCorFundoChanged();

	    private System.Nullable<System.Int32> _LxCorFundo;

	    [DataMember(Name = "LxCorFundo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lx Cor Fundo", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_MODULO.LX_COR_FUNDO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_MODULO.LX_COR_FUNDO")]
	    public System.Nullable<System.Int32> LxCorFundo
	    {
	    	    get
	    	    {
	    	          return _LxCorFundo;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxCorFundo != value)
	    	          {
	    	              this.ValidateProperty("LxCorFundo", value);
	    	              this.OnLxCorFundoChanging(value);
	    	              this.RaiseDataMemberChanging("LxCorFundo");
	    	              this._LxCorFundo = value;
	    	              this.RaiseDataMemberChanged("LxCorFundo");
	    	              this.OnLxCorFundoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeCurto
	    partial void OnNomeCurtoChanging(System.String value);
	    partial void OnNomeCurtoChanged();

	    private System.String _NomeCurto;

	    [DataMember(IsRequired = true, Name = "NomeCurto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Curto", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_MODULO.NOME_CURTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_MODULO.NOME_CURTO")]
	    public System.String NomeCurto
	    {
	    	    get
	    	    {
	    	          return _NomeCurto;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeCurto != value)
	    	          {
	    	              this.ValidateProperty("NomeCurto", value);
	    	              this.OnNomeCurtoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeCurto");
	    	              this._NomeCurto = value;
	    	              this.RaiseDataMemberChanged("NomeCurto");
	    	              this.OnNomeCurtoChanged();
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
	    [Display(Name = "Ordem Navegacao", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_MODULO.ORDEM_NAVEGACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_MODULO.ORDEM_NAVEGACAO")]
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

	    private Int64 _TemporaryIdModulo;
	    [DataMember(Name = "TemporaryIdModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo (Tmp)", Description="Temporary Key", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdModulo
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdModulo.IsNullOrEmpty())
	    	                this._TemporaryIdModulo = this._IdModulo;
	    	          return this._TemporaryIdModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdModulo != value)
	    	              this._TemporaryIdModulo = value;
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<LjvModuloMenu> _LjvModuloMenuList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_LjvModulo_LjvModuloMenu", "IdModulo", "IdModulo", IsForeignKey=false)]
	    [DataMember(Name = "LjvModuloMenuList", EmitDefaultValue = true)]
	    public IEnumerable<LjvModuloMenu> LjvModuloMenuList
	    {
	        get
	        {
	
	            if (this._LjvModuloMenuList == null)
	            	this._LjvModuloMenuList = new List<LjvModuloMenu>();
	
	            return this._LjvModuloMenuList;
	        }
	        set
	        {
	            if (this._LjvModuloMenuList != value)
	            {
	                this._LjvModuloMenuList = value;
	                this.RaisePropertyChanged("LjvModuloMenuList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ConectorPos.LJV_MODULO").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Loja.BM.LJV_MODULO), QualifiedEntitySetName = "ConectorPos.LJV_MODULO" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_MODULO.ICONE", Source = "Icone", Target = "ICONE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_MODULO", RelationPropertyName = "LJV_MODULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_MODULO.INATIVO", Source = "Inativo", Target = "INATIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_MODULO", RelationPropertyName = "LJV_MODULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_MODULO.ID_MODULO", Source = "IdModulo", Target = "ID_MODULO", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_MODULO", RelationPropertyName = "LJV_MODULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_MODULO.NOME_CURTO", Source = "NomeCurto", Target = "NOME_CURTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_MODULO", RelationPropertyName = "LJV_MODULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_MODULO.DESC_MODULO", Source = "DescModulo", Target = "DESC_MODULO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_MODULO", RelationPropertyName = "LJV_MODULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_MODULO.LX_COR_FUNDO", Source = "LxCorFundo", Target = "LX_COR_FUNDO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_MODULO", RelationPropertyName = "LJV_MODULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_MODULO.ORDEM_NAVEGACAO", Source = "OrdemNavegacao", Target = "ORDEM_NAVEGACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_MODULO", RelationPropertyName = "LJV_MODULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_MODULO.ID_TCS_APLICATIVO", Source = "IdTcsAplicativo", Target = "ID_TCS_APLICATIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_MODULO", RelationPropertyName = "LJV_MODULO" });

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

		

	[LinxPublicationView(PrimaryKeys="LJV_MODULO_MENU.ID_MODULO_MENU", IsUpdatable=false, EdmName="Linx.Framework.Loja.BM.ConectorPos")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdModuloMenu];ReadOnly[false];Entities[LJV_MODULO_MENU:IdModuloMenu];SubQueryInfo[Select 1 From #ParentAlias#.LJV_MODULO_MENU_LISTA as #Alias#];EdmEntityName[LJV_MODULO_MENU];EntityRelations[LJV_MODULO(LJV_MODULO)#LJV_MODULO_MENU1(LJV_MODULO_MENU)];EdmParentEntityName[LJV_MODULO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "LjvModuloMenu")]
	[Serializable()]
	public partial class LjvModuloMenu : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(ModuloLojaDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("LjvModulo");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdModulo"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdModulo));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load LjvModulo
	         this.LjvModulo = (from r in context.GetLjvModuloByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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

	    #endregion Flat Entities

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DescModulo
	    partial void OnDescModuloChanging(System.String value);
	    partial void OnDescModuloChanged();

	    private System.String _DescModulo;

	    [DataMember(IsRequired = true, Name = "DescModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Desc Modulo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(100)]
	    [FunctionalPoint("Precision[100:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_MODULO_MENU.LJV_MODULO.DESC_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_MODULO_MENU.LJV_MODULO.DESC_MODULO")]
	    public System.String DescModulo
	    {
	    	    get
	    	    {
	    	          return _DescModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescModulo != value)
	    	          {
	    	              this.ValidateProperty("DescModulo", value);
	    	              this.OnDescModuloChanging(value);
	    	              this.RaiseDataMemberChanging("DescModulo");
	    	              this._DescModulo = value;
	    	              this.RaiseDataMemberChanged("DescModulo");
	    	              this.OnDescModuloChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescModuloMenu
	    partial void OnDescModuloMenuChanging(System.String value);
	    partial void OnDescModuloMenuChanged();

	    private System.String _DescModuloMenu;

	    [DataMember(IsRequired = true, Name = "DescModuloMenu", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Desc Modulo Menu", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(100)]
	    [FunctionalPoint("Precision[100:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_MODULO_MENU.DESC_MODULO_MENU];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_MODULO_MENU.DESC_MODULO_MENU")]
	    public System.String DescModuloMenu
	    {
	    	    get
	    	    {
	    	          return _DescModuloMenu;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescModuloMenu != value)
	    	          {
	    	              this.ValidateProperty("DescModuloMenu", value);
	    	              this.OnDescModuloMenuChanging(value);
	    	              this.RaiseDataMemberChanging("DescModuloMenu");
	    	              this._DescModuloMenu = value;
	    	              this.RaiseDataMemberChanged("DescModuloMenu");
	    	              this.OnDescModuloMenuChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescModuloMenuSuperior
	    partial void OnDescModuloMenuSuperiorChanging(System.String value);
	    partial void OnDescModuloMenuSuperiorChanged();

	    private System.String _DescModuloMenuSuperior;

	    [DataMember(Name = "DescModuloMenuSuperior", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Desc Modulo Menu Superior", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(100)]
	    [FunctionalPoint("Precision[100:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_MODULO_MENU.LJV_MODULO_MENU1.DESC_MODULO_MENU];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_MODULO_MENU.LJV_MODULO_MENU1.DESC_MODULO_MENU")]
	    public System.String DescModuloMenuSuperior
	    {
	    	    get
	    	    {
	    	          return _DescModuloMenuSuperior;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescModuloMenuSuperior != value)
	    	          {
	    	              this.ValidateProperty("DescModuloMenuSuperior", value);
	    	              this.OnDescModuloMenuSuperiorChanging(value);
	    	              this.RaiseDataMemberChanging("DescModuloMenuSuperior");
	    	              this._DescModuloMenuSuperior = value;
	    	              this.RaiseDataMemberChanged("DescModuloMenuSuperior");
	    	              this.OnDescModuloMenuSuperiorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Icone
	    partial void OnIconeChanging(System.String value);
	    partial void OnIconeChanged();

	    private System.String _Icone;

	    [DataMember(Name = "Icone", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Icone", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_MODULO_MENU.ICONE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_MODULO_MENU.ICONE")]
	    public System.String Icone
	    {
	    	    get
	    	    {
	    	          return _Icone;
	    	    }
	    	    set
	    	    {
	    	          if (this._Icone != value)
	    	          {
	    	              this.ValidateProperty("Icone", value);
	    	              this.OnIconeChanging(value);
	    	              this.RaiseDataMemberChanging("Icone");
	    	              this._Icone = value;
	    	              this.RaiseDataMemberChanged("Icone");
	    	              this.OnIconeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdModulo
	    partial void OnIdModuloChanging(Int64 value);
	    partial void OnIdModuloChanged();

	    private Int64 _IdModulo;

	    [DataMember(IsRequired = true, Name = "IdModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_MODULO_MENU.LJV_MODULO.ID_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_MODULO_MENU.LJV_MODULO.ID_MODULO")]
	    public Int64 IdModulo
	    {
	    	    get
	    	    {
	    	          return _IdModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdModulo != value)
	    	          {
	    	              this.ValidateProperty("IdModulo", value);
	    	              this.OnIdModuloChanging(value);
	    	              this.RaiseDataMemberChanging("IdModulo");
	    	              this._IdModulo = value;
	    	              this.RaiseDataMemberChanged("IdModulo");
	    	              this.OnIdModuloChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdModuloMenu
	    partial void OnIdModuloMenuChanging(Int64 value);
	    partial void OnIdModuloMenuChanged();

	    private Int64 _IdModuloMenu;

	    [DataMember(IsRequired = true, Name = "IdModuloMenu", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo Menu", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_MODULO_MENU.ID_MODULO_MENU];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_MODULO_MENU.ID_MODULO_MENU")]
	    public Int64 IdModuloMenu
	    {
	    	    get
	    	    {
	    	          return _IdModuloMenu;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdModuloMenu != value)
	    	          {
	    	              this.ValidateProperty("IdModuloMenu", value);
	    	              this.OnIdModuloMenuChanging(value);
	    	              this.RaiseDataMemberChanging("IdModuloMenu");
	    	              this._IdModuloMenu = value;
	    	              this.RaiseDataMemberChanged("IdModuloMenu");
	    	              this.OnIdModuloMenuChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdModuloMenuSuperior
	    partial void OnIdModuloMenuSuperiorChanging(System.Nullable<Int64> value);
	    partial void OnIdModuloMenuSuperiorChanged();

	    private System.Nullable<Int64> _IdModuloMenuSuperior;

	    [DataMember(Name = "IdModuloMenuSuperior", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo Menu Superior", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_MODULO_MENU.LJV_MODULO_MENU1.ID_MODULO_MENU];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_MODULO_MENU.LJV_MODULO_MENU1.ID_MODULO_MENU")]
	    public System.Nullable<Int64> IdModuloMenuSuperior
	    {
	    	    get
	    	    {
	    	          return _IdModuloMenuSuperior;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdModuloMenuSuperior != value)
	    	          {
	    	              this.ValidateProperty("IdModuloMenuSuperior", value);
	    	              this.OnIdModuloMenuSuperiorChanging(value);
	    	              this.RaiseDataMemberChanging("IdModuloMenuSuperior");
	    	              this._IdModuloMenuSuperior = value;
	    	              this.RaiseDataMemberChanged("IdModuloMenuSuperior");
	    	              this.OnIdModuloMenuSuperiorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsAplicativo
	    partial void OnIdTcsAplicativoChanging(Int32 value);
	    partial void OnIdTcsAplicativoChanged();

	    private Int32 _IdTcsAplicativo;

	    [DataMember(IsRequired = true, Name = "IdTcsAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Aplicativo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_MODULO_MENU.LJV_MODULO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_MODULO_MENU.LJV_MODULO.ID_TCS_APLICATIVO")]
	    public Int32 IdTcsAplicativo
	    {
	    	    get
	    	    {
	    	          return _IdTcsAplicativo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAplicativo != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAplicativo", value);
	    	              this.OnIdTcsAplicativoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAplicativo");
	    	              this._IdTcsAplicativo = value;
	    	              this.RaiseDataMemberChanged("IdTcsAplicativo");
	    	              this.OnIdTcsAplicativoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxCorFundo
	    partial void OnLxCorFundoChanging(System.Nullable<System.Int32> value);
	    partial void OnLxCorFundoChanged();

	    private System.Nullable<System.Int32> _LxCorFundo;

	    [DataMember(Name = "LxCorFundo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lx Cor Fundo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_MODULO_MENU.LX_COR_FUNDO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_MODULO_MENU.LX_COR_FUNDO")]
	    public System.Nullable<System.Int32> LxCorFundo
	    {
	    	    get
	    	    {
	    	          return _LxCorFundo;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxCorFundo != value)
	    	          {
	    	              this.ValidateProperty("LxCorFundo", value);
	    	              this.OnLxCorFundoChanging(value);
	    	              this.RaiseDataMemberChanging("LxCorFundo");
	    	              this._LxCorFundo = value;
	    	              this.RaiseDataMemberChanged("LxCorFundo");
	    	              this.OnLxCorFundoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeCurto
	    partial void OnNomeCurtoChanging(System.String value);
	    partial void OnNomeCurtoChanged();

	    private System.String _NomeCurto;

	    [DataMember(IsRequired = true, Name = "NomeCurto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Curto", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_MODULO_MENU.NOME_CURTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_MODULO_MENU.NOME_CURTO")]
	    public System.String NomeCurto
	    {
	    	    get
	    	    {
	    	          return _NomeCurto;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeCurto != value)
	    	          {
	    	              this.ValidateProperty("NomeCurto", value);
	    	              this.OnNomeCurtoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeCurto");
	    	              this._NomeCurto = value;
	    	              this.RaiseDataMemberChanged("NomeCurto");
	    	              this.OnNomeCurtoChanged();
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
	    [Display(Name = "Ordem Navegacao", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_MODULO_MENU.ORDEM_NAVEGACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_MODULO_MENU.ORDEM_NAVEGACAO")]
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

	    private Int64 _TemporaryIdModuloMenu;
	    [DataMember(Name = "TemporaryIdModuloMenu", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo Menu (Tmp)", Description="Temporary Key", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdModuloMenu
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdModuloMenu.IsNullOrEmpty())
	    	                this._TemporaryIdModuloMenu = this._IdModuloMenu;
	    	          return this._TemporaryIdModuloMenu;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdModuloMenu != value)
	    	              this._TemporaryIdModuloMenu = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private LjvModulo _LjvModulo;
	    [DataMember(Name = "LjvModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_LjvModulo_LjvModuloMenu", "IdModulo", "IdModulo", IsForeignKey=true)]
	    public LjvModulo LjvModulo
	    {
	        get
	        {
	            return this._LjvModulo;
	        }
	        set
	        {
	            if (this._LjvModulo != value)
	            {
	                this._LjvModulo = value;
	                this.RaisePropertyChanged("LjvModuloList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ConectorPos.LJV_MODULO_MENU").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Loja.BM.LJV_MODULO_MENU), QualifiedEntitySetName = "ConectorPos.LJV_MODULO_MENU" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_MODULO_MENU.ICONE", Source = "Icone", Target = "ICONE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_MODULO_MENU", RelationPropertyName = "LJV_MODULO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_MODULO_MENU.NOME_CURTO", Source = "NomeCurto", Target = "NOME_CURTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_MODULO_MENU", RelationPropertyName = "LJV_MODULO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_MODULO_MENU.LX_COR_FUNDO", Source = "LxCorFundo", Target = "LX_COR_FUNDO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_MODULO_MENU", RelationPropertyName = "LJV_MODULO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_MODULO_MENU.ID_MODULO_MENU", Source = "IdModuloMenu", Target = "ID_MODULO_MENU", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_MODULO_MENU", RelationPropertyName = "LJV_MODULO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_MODULO_MENU.ORDEM_NAVEGACAO", Source = "OrdemNavegacao", Target = "ORDEM_NAVEGACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_MODULO_MENU", RelationPropertyName = "LJV_MODULO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_MODULO_MENU.DESC_MODULO_MENU", Source = "DescModuloMenu", Target = "DESC_MODULO_MENU", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_MODULO_MENU", RelationPropertyName = "LJV_MODULO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_MODULO_MENU.LJV_MODULO.ID_MODULO", Source = "IdModulo", Target = "ID_MODULO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ConectorPos.LJV_MODULO", RelationPropertyName = "LJV_MODULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_MODULO_MENU.LJV_MODULO_MENU1.ID_MODULO_MENU", Source = "IdModuloMenuSuperior", Target = "ID_MODULO_MENU", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ConectorPos.LJV_MODULO_MENU", RelationPropertyName = "LJV_MODULO_MENU1" });

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

		

	[LinxPublicationView(PrimaryKeys="LJV_TRANSACAO_MENU.ID_TRANSACAO_MENU", IsUpdatable=false, EdmName="Linx.Framework.Loja.BM.ConectorPos")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[LjvTransacaoMenu];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTransacaoMenu];ReadOnly[false];Entities[LJV_TRANSACAO_MENU:IdTransacaoMenu];SubQueryInfo[];EdmEntityName[LJV_TRANSACAO_MENU];EntityRelations[LJV_MODULO_MENU(LJV_MODULO_MENU)#LJV_MODULO(LJV_MODULO)#LJV_MODULO_MENU1(LJV_MODULO_MENU)#LJV_TRANSACAO(LJV_TRANSACAO)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "LjvTransacaoMenu")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.BV.ModuloLoja.LjvTransacaoMenu")]
	public partial class LjvTransacaoMenu : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For ClasseNome
	    partial void OnClasseNomeChanging(System.String value);
	    partial void OnClasseNomeChanged();

	    private System.String _ClasseNome;

	    [DataMember(IsRequired = true, Name = "ClasseNome", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Classe Nome", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(400)]
	    [FunctionalPoint("Precision[400:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[EditBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_TRANSACAO_MENU.LJV_TRANSACAO.CLASSE_NOME];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_TRANSACAO_MENU.LJV_TRANSACAO.CLASSE_NOME")]
	    public System.String ClasseNome
	    {
	    	    get
	    	    {
	    	          return _ClasseNome;
	    	    }
	    	    set
	    	    {
	    	          if (this._ClasseNome != value)
	    	          {
	    	              this.ValidateProperty("ClasseNome", value);
	    	              this.OnClasseNomeChanging(value);
	    	              this.RaiseDataMemberChanging("ClasseNome");
	    	              this._ClasseNome = value;
	    	              this.RaiseDataMemberChanged("ClasseNome");
	    	              this.OnClasseNomeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CodTransacao
	    partial void OnCodTransacaoChanging(System.String value);
	    partial void OnCodTransacaoChanged();

	    private System.String _CodTransacao;

	    [DataMember(IsRequired = true, Name = "CodTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cod Transacao", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_TRANSACAO_MENU.LJV_TRANSACAO.COD_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_TRANSACAO_MENU.LJV_TRANSACAO.COD_TRANSACAO")]
	    public System.String CodTransacao
	    {
	    	    get
	    	    {
	    	          return _CodTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodTransacao != value)
	    	          {
	    	              this.ValidateProperty("CodTransacao", value);
	    	              this.OnCodTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("CodTransacao");
	    	              this._CodTransacao = value;
	    	              this.RaiseDataMemberChanged("CodTransacao");
	    	              this.OnCodTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescTransacao
	    partial void OnDescTransacaoChanging(System.String value);
	    partial void OnDescTransacaoChanged();

	    private System.String _DescTransacao;

	    [DataMember(IsRequired = true, Name = "DescTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Desc Transacao", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(100)]
	    [FunctionalPoint("Precision[100:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_TRANSACAO_MENU.LJV_TRANSACAO.DESC_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_TRANSACAO_MENU.LJV_TRANSACAO.DESC_TRANSACAO")]
	    public System.String DescTransacao
	    {
	    	    get
	    	    {
	    	          return _DescTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescTransacao != value)
	    	          {
	    	              this.ValidateProperty("DescTransacao", value);
	    	              this.OnDescTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("DescTransacao");
	    	              this._DescTransacao = value;
	    	              this.RaiseDataMemberChanged("DescTransacao");
	    	              this.OnDescTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Icone
	    partial void OnIconeChanging(System.String value);
	    partial void OnIconeChanged();

	    private System.String _Icone;

	    [DataMember(Name = "Icone", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Icone", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_TRANSACAO_MENU.LJV_TRANSACAO.ICONE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_TRANSACAO_MENU.LJV_TRANSACAO.ICONE")]
	    public System.String Icone
	    {
	    	    get
	    	    {
	    	          return _Icone;
	    	    }
	    	    set
	    	    {
	    	          if (this._Icone != value)
	    	          {
	    	              this.ValidateProperty("Icone", value);
	    	              this.OnIconeChanging(value);
	    	              this.RaiseDataMemberChanging("Icone");
	    	              this._Icone = value;
	    	              this.RaiseDataMemberChanged("Icone");
	    	              this.OnIconeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdModuloMenu
	    partial void OnIdModuloMenuChanging(Int64 value);
	    partial void OnIdModuloMenuChanged();

	    private Int64 _IdModuloMenu;

	    [DataMember(IsRequired = true, Name = "IdModuloMenu", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo Menu", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_TRANSACAO_MENU.LJV_MODULO_MENU.ID_MODULO_MENU];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_TRANSACAO_MENU.LJV_MODULO_MENU.ID_MODULO_MENU")]
	    public Int64 IdModuloMenu
	    {
	    	    get
	    	    {
	    	          return _IdModuloMenu;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdModuloMenu != value)
	    	          {
	    	              this.ValidateProperty("IdModuloMenu", value);
	    	              this.OnIdModuloMenuChanging(value);
	    	              this.RaiseDataMemberChanging("IdModuloMenu");
	    	              this._IdModuloMenu = value;
	    	              this.RaiseDataMemberChanged("IdModuloMenu");
	    	              this.OnIdModuloMenuChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTransacao
	    partial void OnIdTransacaoChanging(Int64 value);
	    partial void OnIdTransacaoChanged();

	    private Int64 _IdTransacao;

	    [DataMember(IsRequired = true, Name = "IdTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Transacao", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_TRANSACAO_MENU.LJV_TRANSACAO.ID_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_TRANSACAO_MENU.LJV_TRANSACAO.ID_TRANSACAO")]
	    public Int64 IdTransacao
	    {
	    	    get
	    	    {
	    	          return _IdTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTransacao != value)
	    	          {
	    	              this.ValidateProperty("IdTransacao", value);
	    	              this.OnIdTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTransacao");
	    	              this._IdTransacao = value;
	    	              this.RaiseDataMemberChanged("IdTransacao");
	    	              this.OnIdTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTransacaoMenu
	    partial void OnIdTransacaoMenuChanging(Int64 value);
	    partial void OnIdTransacaoMenuChanged();

	    private Int64 _IdTransacaoMenu;

	    [DataMember(IsRequired = true, Name = "IdTransacaoMenu", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Transacao Menu", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_TRANSACAO_MENU.ID_TRANSACAO_MENU];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_TRANSACAO_MENU.ID_TRANSACAO_MENU")]
	    public Int64 IdTransacaoMenu
	    {
	    	    get
	    	    {
	    	          return _IdTransacaoMenu;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTransacaoMenu != value)
	    	          {
	    	              this.ValidateProperty("IdTransacaoMenu", value);
	    	              this.OnIdTransacaoMenuChanging(value);
	    	              this.RaiseDataMemberChanging("IdTransacaoMenu");
	    	              this._IdTransacaoMenu = value;
	    	              this.RaiseDataMemberChanged("IdTransacaoMenu");
	    	              this.OnIdTransacaoMenuChanged();
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
	    [Display(Name = "Inativo", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_TRANSACAO_MENU.LJV_TRANSACAO.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_TRANSACAO_MENU.LJV_TRANSACAO.INATIVO")]
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
	    //Extensibility Partial Method Definitions For InativoMenu
	    partial void OnInativoMenuChanging(Boolean value);
	    partial void OnInativoMenuChanged();

	    private Boolean _InativoMenu;

	    [DataMember(IsRequired = true, Name = "InativoMenu", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo Menu", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_TRANSACAO_MENU.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_TRANSACAO_MENU.INATIVO")]
	    public Boolean InativoMenu
	    {
	    	    get
	    	    {
	    	          return _InativoMenu;
	    	    }
	    	    set
	    	    {
	    	          if (this._InativoMenu != value)
	    	          {
	    	              this.ValidateProperty("InativoMenu", value);
	    	              this.OnInativoMenuChanging(value);
	    	              this.RaiseDataMemberChanging("InativoMenu");
	    	              this._InativoMenu = value;
	    	              this.RaiseDataMemberChanged("InativoMenu");
	    	              this.OnInativoMenuChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxCorFundo
	    partial void OnLxCorFundoChanging(System.Nullable<System.Int32> value);
	    partial void OnLxCorFundoChanged();

	    private System.Nullable<System.Int32> _LxCorFundo;

	    [DataMember(Name = "LxCorFundo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lx Cor Fundo", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_TRANSACAO_MENU.LJV_TRANSACAO.LX_COR_FUNDO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_TRANSACAO_MENU.LJV_TRANSACAO.LX_COR_FUNDO")]
	    public System.Nullable<System.Int32> LxCorFundo
	    {
	    	    get
	    	    {
	    	          return _LxCorFundo;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxCorFundo != value)
	    	          {
	    	              this.ValidateProperty("LxCorFundo", value);
	    	              this.OnLxCorFundoChanging(value);
	    	              this.RaiseDataMemberChanging("LxCorFundo");
	    	              this._LxCorFundo = value;
	    	              this.RaiseDataMemberChanged("LxCorFundo");
	    	              this.OnLxCorFundoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoTransacao
	    partial void OnLxTipoTransacaoChanging(Byte value);
	    partial void OnLxTipoTransacaoChanged();

	    private Byte _LxTipoTransacao;

	    [DataMember(IsRequired = true, Name = "LxTipoTransacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lx Tipo Transacao", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_TRANSACAO_MENU.LJV_TRANSACAO.LX_TIPO_TRANSACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_TRANSACAO_MENU.LJV_TRANSACAO.LX_TIPO_TRANSACAO")]
	    public Byte LxTipoTransacao
	    {
	    	    get
	    	    {
	    	          return _LxTipoTransacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoTransacao != value)
	    	          {
	    	              this.ValidateProperty("LxTipoTransacao", value);
	    	              this.OnLxTipoTransacaoChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoTransacao");
	    	              this._LxTipoTransacao = value;
	    	              this.RaiseDataMemberChanged("LxTipoTransacao");
	    	              this.OnLxTipoTransacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeCurto
	    partial void OnNomeCurtoChanging(System.String value);
	    partial void OnNomeCurtoChanged();

	    private System.String _NomeCurto;

	    [DataMember(IsRequired = true, Name = "NomeCurto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Curto", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_TRANSACAO_MENU.LJV_TRANSACAO.NOME_CURTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_TRANSACAO_MENU.LJV_TRANSACAO.NOME_CURTO")]
	    public System.String NomeCurto
	    {
	    	    get
	    	    {
	    	          return _NomeCurto;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeCurto != value)
	    	          {
	    	              this.ValidateProperty("NomeCurto", value);
	    	              this.OnNomeCurtoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeCurto");
	    	              this._NomeCurto = value;
	    	              this.RaiseDataMemberChanged("NomeCurto");
	    	              this.OnNomeCurtoChanged();
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
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_TRANSACAO_MENU.ORDEM_NAVEGACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_TRANSACAO_MENU.ORDEM_NAVEGACAO")]
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

	    private Int64 _TemporaryIdTransacaoMenu;
	    [DataMember(Name = "TemporaryIdTransacaoMenu", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Transacao Menu (Tmp)", Description="Temporary Key", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdTransacaoMenu
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTransacaoMenu.IsNullOrEmpty())
	    	                this._TemporaryIdTransacaoMenu = this._IdTransacaoMenu;
	    	          return this._TemporaryIdTransacaoMenu;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTransacaoMenu != value)
	    	              this._TemporaryIdTransacaoMenu = value;
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ConectorPos.LJV_TRANSACAO_MENU").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Loja.BM.LJV_TRANSACAO_MENU), QualifiedEntitySetName = "ConectorPos.LJV_TRANSACAO_MENU" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_TRANSACAO_MENU.INATIVO", Source = "InativoMenu", Target = "INATIVO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_TRANSACAO_MENU", RelationPropertyName = "LJV_TRANSACAO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_TRANSACAO_MENU.ORDEM_NAVEGACAO", Source = "OrdemNavegacao", Target = "ORDEM_NAVEGACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_TRANSACAO_MENU", RelationPropertyName = "LJV_TRANSACAO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_TRANSACAO_MENU.ID_TRANSACAO_MENU", Source = "IdTransacaoMenu", Target = "ID_TRANSACAO_MENU", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_TRANSACAO_MENU", RelationPropertyName = "LJV_TRANSACAO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_TRANSACAO_MENU.LJV_TRANSACAO.ID_TRANSACAO", Source = "IdTransacao", Target = "ID_TRANSACAO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ConectorPos.LJV_TRANSACAO", RelationPropertyName = "LJV_TRANSACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_TRANSACAO_MENU.LJV_MODULO_MENU.ID_MODULO_MENU", Source = "IdModuloMenu", Target = "ID_MODULO_MENU", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ConectorPos.LJV_MODULO_MENU", RelationPropertyName = "LJV_MODULO_MENU" });

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

	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdModuloMenu];ReadOnly[false];Entities[LJV_MODULO_MENU:IdModuloMenu];SubQueryInfo[Select 1 From #ParentAlias#.LJV_MODULO_MENU_LISTA as #Alias#];EdmEntityName[LJV_MODULO_MENU];EntityRelations[LJV_MODULO(LJV_MODULO)#LJV_MODULO_MENU1(LJV_MODULO_MENU)];EdmParentEntityName[LJV_MODULO];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "LjvModuloMenu")]
	[Serializable()]
	public partial class LjvModuloMenuParentComposition : Linx.Data.Entity
	{

	
	
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DescModulo
	    partial void OnDescModuloChanging(System.String value);
	    partial void OnDescModuloChanged();

	    private System.String _DescModulo;

	    [DataMember(IsRequired = true, Name = "DescModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Desc Modulo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(100)]
	    [FunctionalPoint("Precision[100:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_MODULO_MENU.LJV_MODULO.DESC_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_MODULO_MENU.LJV_MODULO.DESC_MODULO")]
	    public System.String DescModulo
	    {
	    	    get
	    	    {
	    	          return _DescModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescModulo != value)
	    	          {
	    	              this.ValidateProperty("DescModulo", value);
	    	              this.OnDescModuloChanging(value);
	    	              this.RaiseDataMemberChanging("DescModulo");
	    	              this._DescModulo = value;
	    	              this.RaiseDataMemberChanged("DescModulo");
	    	              this.OnDescModuloChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescModuloMenu
	    partial void OnDescModuloMenuChanging(System.String value);
	    partial void OnDescModuloMenuChanged();

	    private System.String _DescModuloMenu;

	    [DataMember(IsRequired = true, Name = "DescModuloMenu", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Desc Modulo Menu", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(100)]
	    [FunctionalPoint("Precision[100:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_MODULO_MENU.DESC_MODULO_MENU];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_MODULO_MENU.DESC_MODULO_MENU")]
	    public System.String DescModuloMenu
	    {
	    	    get
	    	    {
	    	          return _DescModuloMenu;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescModuloMenu != value)
	    	          {
	    	              this.ValidateProperty("DescModuloMenu", value);
	    	              this.OnDescModuloMenuChanging(value);
	    	              this.RaiseDataMemberChanging("DescModuloMenu");
	    	              this._DescModuloMenu = value;
	    	              this.RaiseDataMemberChanged("DescModuloMenu");
	    	              this.OnDescModuloMenuChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescModuloMenuSuperior
	    partial void OnDescModuloMenuSuperiorChanging(System.String value);
	    partial void OnDescModuloMenuSuperiorChanged();

	    private System.String _DescModuloMenuSuperior;

	    [DataMember(Name = "DescModuloMenuSuperior", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Desc Modulo Menu Superior", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(100)]
	    [FunctionalPoint("Precision[100:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_MODULO_MENU.LJV_MODULO_MENU1.DESC_MODULO_MENU];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_MODULO_MENU.LJV_MODULO_MENU1.DESC_MODULO_MENU")]
	    public System.String DescModuloMenuSuperior
	    {
	    	    get
	    	    {
	    	          return _DescModuloMenuSuperior;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescModuloMenuSuperior != value)
	    	          {
	    	              this.ValidateProperty("DescModuloMenuSuperior", value);
	    	              this.OnDescModuloMenuSuperiorChanging(value);
	    	              this.RaiseDataMemberChanging("DescModuloMenuSuperior");
	    	              this._DescModuloMenuSuperior = value;
	    	              this.RaiseDataMemberChanged("DescModuloMenuSuperior");
	    	              this.OnDescModuloMenuSuperiorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Icone
	    partial void OnIconeChanging(System.String value);
	    partial void OnIconeChanged();

	    private System.String _Icone;

	    [DataMember(Name = "Icone", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Icone", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_MODULO_MENU.ICONE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_MODULO_MENU.ICONE")]
	    public System.String Icone
	    {
	    	    get
	    	    {
	    	          return _Icone;
	    	    }
	    	    set
	    	    {
	    	          if (this._Icone != value)
	    	          {
	    	              this.ValidateProperty("Icone", value);
	    	              this.OnIconeChanging(value);
	    	              this.RaiseDataMemberChanging("Icone");
	    	              this._Icone = value;
	    	              this.RaiseDataMemberChanged("Icone");
	    	              this.OnIconeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdModulo
	    partial void OnIdModuloChanging(Int64 value);
	    partial void OnIdModuloChanged();

	    private Int64 _IdModulo;

	    [DataMember(IsRequired = true, Name = "IdModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_MODULO_MENU.LJV_MODULO.ID_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_MODULO_MENU.LJV_MODULO.ID_MODULO")]
	    public Int64 IdModulo
	    {
	    	    get
	    	    {
	    	          return _IdModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdModulo != value)
	    	          {
	    	              this.ValidateProperty("IdModulo", value);
	    	              this.OnIdModuloChanging(value);
	    	              this.RaiseDataMemberChanging("IdModulo");
	    	              this._IdModulo = value;
	    	              this.RaiseDataMemberChanged("IdModulo");
	    	              this.OnIdModuloChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdModuloMenu
	    partial void OnIdModuloMenuChanging(Int64 value);
	    partial void OnIdModuloMenuChanged();

	    private Int64 _IdModuloMenu;

	    [DataMember(IsRequired = true, Name = "IdModuloMenu", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo Menu", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_MODULO_MENU.ID_MODULO_MENU];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_MODULO_MENU.ID_MODULO_MENU")]
	    public Int64 IdModuloMenu
	    {
	    	    get
	    	    {
	    	          return _IdModuloMenu;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdModuloMenu != value)
	    	          {
	    	              this.ValidateProperty("IdModuloMenu", value);
	    	              this.OnIdModuloMenuChanging(value);
	    	              this.RaiseDataMemberChanging("IdModuloMenu");
	    	              this._IdModuloMenu = value;
	    	              this.RaiseDataMemberChanged("IdModuloMenu");
	    	              this.OnIdModuloMenuChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdModuloMenuSuperior
	    partial void OnIdModuloMenuSuperiorChanging(System.Nullable<Int64> value);
	    partial void OnIdModuloMenuSuperiorChanged();

	    private System.Nullable<Int64> _IdModuloMenuSuperior;

	    [DataMember(Name = "IdModuloMenuSuperior", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo Menu Superior", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_MODULO_MENU.LJV_MODULO_MENU1.ID_MODULO_MENU];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_MODULO_MENU.LJV_MODULO_MENU1.ID_MODULO_MENU")]
	    public System.Nullable<Int64> IdModuloMenuSuperior
	    {
	    	    get
	    	    {
	    	          return _IdModuloMenuSuperior;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdModuloMenuSuperior != value)
	    	          {
	    	              this.ValidateProperty("IdModuloMenuSuperior", value);
	    	              this.OnIdModuloMenuSuperiorChanging(value);
	    	              this.RaiseDataMemberChanging("IdModuloMenuSuperior");
	    	              this._IdModuloMenuSuperior = value;
	    	              this.RaiseDataMemberChanged("IdModuloMenuSuperior");
	    	              this.OnIdModuloMenuSuperiorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsAplicativo
	    partial void OnIdTcsAplicativoChanging(Int32 value);
	    partial void OnIdTcsAplicativoChanged();

	    private Int32 _IdTcsAplicativo;

	    [DataMember(IsRequired = true, Name = "IdTcsAplicativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Aplicativo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_MODULO_MENU.LJV_MODULO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_MODULO_MENU.LJV_MODULO.ID_TCS_APLICATIVO")]
	    public Int32 IdTcsAplicativo
	    {
	    	    get
	    	    {
	    	          return _IdTcsAplicativo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAplicativo != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAplicativo", value);
	    	              this.OnIdTcsAplicativoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAplicativo");
	    	              this._IdTcsAplicativo = value;
	    	              this.RaiseDataMemberChanged("IdTcsAplicativo");
	    	              this.OnIdTcsAplicativoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxCorFundo
	    partial void OnLxCorFundoChanging(System.Nullable<System.Int32> value);
	    partial void OnLxCorFundoChanged();

	    private System.Nullable<System.Int32> _LxCorFundo;

	    [DataMember(Name = "LxCorFundo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lx Cor Fundo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_MODULO_MENU.LX_COR_FUNDO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_MODULO_MENU.LX_COR_FUNDO")]
	    public System.Nullable<System.Int32> LxCorFundo
	    {
	    	    get
	    	    {
	    	          return _LxCorFundo;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxCorFundo != value)
	    	          {
	    	              this.ValidateProperty("LxCorFundo", value);
	    	              this.OnLxCorFundoChanging(value);
	    	              this.RaiseDataMemberChanging("LxCorFundo");
	    	              this._LxCorFundo = value;
	    	              this.RaiseDataMemberChanged("LxCorFundo");
	    	              this.OnLxCorFundoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeCurto
	    partial void OnNomeCurtoChanging(System.String value);
	    partial void OnNomeCurtoChanged();

	    private System.String _NomeCurto;

	    [DataMember(IsRequired = true, Name = "NomeCurto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Curto", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(40)]
	    [FunctionalPoint("Precision[40:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_MODULO_MENU.NOME_CURTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_MODULO_MENU.NOME_CURTO")]
	    public System.String NomeCurto
	    {
	    	    get
	    	    {
	    	          return _NomeCurto;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeCurto != value)
	    	          {
	    	              this.ValidateProperty("NomeCurto", value);
	    	              this.OnNomeCurtoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeCurto");
	    	              this._NomeCurto = value;
	    	              this.RaiseDataMemberChanged("NomeCurto");
	    	              this.OnNomeCurtoChanged();
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
	    [Display(Name = "Ordem Navegacao", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_MODULO_MENU.ORDEM_NAVEGACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_MODULO_MENU.ORDEM_NAVEGACAO")]
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
	    //Extensibility Partial Method Definitions For Inativo
	    partial void OnInativoChanging(Boolean value);
	    partial void OnInativoChanged();

	    private Boolean _Inativo;

	    [DataMember(IsRequired = true, Name = "Inativo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[LJV_MODULO_MENU.LJV_MODULO.INATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_MODULO.INATIVO")]
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

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "ConectorPos.LJV_MODULO_MENU").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Loja.BM.LJV_MODULO_MENU), QualifiedEntitySetName = "ConectorPos.LJV_MODULO_MENU" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_MODULO_MENU.ICONE", Source = "Icone", Target = "ICONE", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_MODULO_MENU", RelationPropertyName = "LJV_MODULO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_MODULO_MENU.NOME_CURTO", Source = "NomeCurto", Target = "NOME_CURTO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_MODULO_MENU", RelationPropertyName = "LJV_MODULO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_MODULO_MENU.LX_COR_FUNDO", Source = "LxCorFundo", Target = "LX_COR_FUNDO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_MODULO_MENU", RelationPropertyName = "LJV_MODULO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_MODULO_MENU.ID_MODULO_MENU", Source = "IdModuloMenu", Target = "ID_MODULO_MENU", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_MODULO_MENU", RelationPropertyName = "LJV_MODULO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_MODULO_MENU.ORDEM_NAVEGACAO", Source = "OrdemNavegacao", Target = "ORDEM_NAVEGACAO", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_MODULO_MENU", RelationPropertyName = "LJV_MODULO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_MODULO_MENU.DESC_MODULO_MENU", Source = "DescModuloMenu", Target = "DESC_MODULO_MENU", NoUpdatable = false, IsKey = false, IsFK = false, QualifiedEntitySetName = "ConectorPos.LJV_MODULO_MENU", RelationPropertyName = "LJV_MODULO_MENU" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_MODULO_MENU.LJV_MODULO.ID_MODULO", Source = "IdModulo", Target = "ID_MODULO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ConectorPos.LJV_MODULO", RelationPropertyName = "LJV_MODULO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="LJV_MODULO_MENU.LJV_MODULO_MENU1.ID_MODULO_MENU", Source = "IdModuloMenuSuperior", Target = "ID_MODULO_MENU", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "ConectorPos.LJV_MODULO_MENU", RelationPropertyName = "LJV_MODULO_MENU1" });

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
	[DomainIdentifier("ProcessorOverviewModuloLojaDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class ModuloLojaDomainService : DomainService, IDataServiceContext 
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

		
	    public ModuloLojaDomainService() : this("", null, null) { }
	    public ModuloLojaDomainService(string connectionString) : this(connectionString, null, null) { }
	    public ModuloLojaDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public ModuloLojaDomainService(Linx.Framework.Loja.BM.ConectorPos dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public ModuloLojaDomainService(string connectionString, Linx.Framework.Loja.BM.ConectorPos dataContext, Dictionary<string, string> headers) : base() 
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

		
 
 	        bool createNewChangeSet = false;
 
 	        //Adjust data hierarchy
 	        var _LjvModuloElements = changeSet.ChangeSetEntries.Where(e => e.Entity is LjvModulo && e.Entity.GetType().Name == "LjvModulo" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _LjvModuloElements)
 	           if (((LjvModulo)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is LjvModuloMenu && e.Entity.GetType().Name == "LjvModuloMenu" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
	
		

	        if (entityName.InList("Linx.Framework.BV.ModuloLoja.LjvModulo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "LjvModulo",
	        			NameSpace = "Linx.Framework.BV.ModuloLoja",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "LjvModulo",
	        			ClearMethodName = "ClearLjvModulo",
	        			QueryMethodName  = "GetPagedLjvModulo",	
	        			CountingMethodName  = "GetLjvModulo" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.ModuloLoja.LjvModulo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.ModuloLoja.LjvModulo"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.ModuloLoja.LjvModulo", "Linx.Framework.BV.ModuloLoja.LjvModuloMenu"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "LjvModuloMenu" + (removeParentComposition ? "" : "ParentComposition"),
	        			NameSpace = "Linx.Framework.BV.ModuloLoja",
	        			HasQuickSearch = false,
	        			ParentClassName = "LjvModulo",	
	        			DisplayName = "LjvModuloMenu",
	        			ClearMethodName = "ClearLjvModuloMenu" + (removeParentComposition ? "" : "ParentComposition"),
	        			QueryMethodName  = "GetPagedLjvModuloMenu" + (removeParentComposition ? "" : "ParentComposition"),	
	        			CountingMethodName  = "GetLjvModuloMenu" + (removeParentComposition ? "" : "ParentComposition") + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.ModuloLoja.LjvModuloMenu"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.ModuloLoja.LjvModuloMenu" + (removeParentComposition ? "" : "ParentComposition")), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.BV.ModuloLoja.LjvTransacaoMenu"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "LjvTransacaoMenu",
	        			NameSpace = "Linx.Framework.BV.ModuloLoja",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "LjvTransacaoMenu",
	        			ClearMethodName = "ClearLjvTransacaoMenu",
	        			QueryMethodName  = "GetPagedLjvTransacaoMenu",	
	        			CountingMethodName  = "GetLjvTransacaoMenu" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.BV.ModuloLoja.LjvTransacaoMenu"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.BV.ModuloLoja.LjvTransacaoMenu"), forceAll: forceAll)
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

         		    return new string[] { "Framework_ModuloLojaClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.ModuloLojaClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "Framework_moduloLojaService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.BV.ClientResources.moduloLojaService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear LjvModulo.
	    public IEnumerable<LjvModulo> ClearLjvModulo()
	    {
	        List<LjvModulo> result = new List<LjvModulo>();
	        result.Add(new LjvModulo());	
			
	        result[0].LjvModuloMenuList = new List<LjvModuloMenu>();
	        ((List<LjvModuloMenu>)result[0].LjvModuloMenuList).Add(new LjvModuloMenu());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear LjvModuloMenu.
	    public IEnumerable<LjvModuloMenu> ClearLjvModuloMenu()
	    {
	        List<LjvModuloMenu> result = new List<LjvModuloMenu>();
	        result.Add(new LjvModuloMenu());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear LjvTransacaoMenu.
	    public IEnumerable<LjvTransacaoMenu> ClearLjvTransacaoMenu()
	    {
	        List<LjvTransacaoMenu> result = new List<LjvTransacaoMenu>();
	        result.Add(new LjvTransacaoMenu());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Query(HasSideEffects = false)]
	    //Get LjvModulo.
	    public IQueryable<LjvModulo> GetLjvModulo()
	    {




		
	
	        
		
	        
	
	        IQueryable<LjvModulo> result = 
	            (from entity0 in this.DbContext.LJV_MODULO
	            
	            	
	            select new LjvModulo()		
	            {
	            
                DescModulo = entity0.DESC_MODULO
                , Icone = entity0.ICONE
                , IdModulo = entity0.ID_MODULO
                , IdTcsAplicativo = entity0.ID_TCS_APLICATIVO
                , Inativo = entity0.INATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , NomeCurto = entity0.NOME_CURTO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
			
                ,LjvModuloMenuList = 
	                        (from entity1 in entity0.LJV_MODULO_MENU_LISTA
                                  let entity1Al1 = entity1.LJV_MODULO
                                  let entity1Al2 = entity1.LJV_MODULO_MENU1
	                        
	                        	
	                        select new LjvModuloMenu()
	                        {
	                        
                                DescModulo = entity1Al1.DESC_MODULO
                                , DescModuloMenu = entity1.DESC_MODULO_MENU
                                , DescModuloMenuSuperior = entity1Al2.DESC_MODULO_MENU
                                , Icone = entity1.ICONE
                                , IdModulo = entity1Al1.ID_MODULO
                                , IdModuloMenu = entity1.ID_MODULO_MENU
                                , IdModuloMenuSuperior = entity1Al2.ID_MODULO_MENU
                                , IdTcsAplicativo = entity1Al1.ID_TCS_APLICATIVO
                                , LxCorFundo = entity1.LX_COR_FUNDO
                                , NomeCurto = entity1.NOME_CURTO
                                , OrdemNavegacao = entity1.ORDEM_NAVEGACAO
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get LjvModuloMenu.
	    public IQueryable<LjvModuloMenu> GetLjvModuloMenu()
	    {




		
	
	        
		
	        
	
	        IQueryable<LjvModuloMenu> result = 
	            (from entity0 in this.DbContext.LJV_MODULO_MENU
                  let entity0Al1 = entity0.LJV_MODULO
                  let entity0Al2 = entity0.LJV_MODULO_MENU1
	            
	            	
	            select new LjvModuloMenu()		
	            {
	            
                DescModulo = entity0Al1.DESC_MODULO
                , DescModuloMenu = entity0.DESC_MODULO_MENU
                , DescModuloMenuSuperior = entity0Al2.DESC_MODULO_MENU
                , Icone = entity0.ICONE
                , IdModulo = entity0Al1.ID_MODULO
                , IdModuloMenu = entity0.ID_MODULO_MENU
                , IdModuloMenuSuperior = entity0Al2.ID_MODULO_MENU
                , IdTcsAplicativo = entity0Al1.ID_TCS_APLICATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , NomeCurto = entity0.NOME_CURTO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get LjvModuloNoAssociations.
	    public IQueryable<LjvModulo> GetLjvModuloNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<LjvModulo> result = 
	            (from entity0 in this.DbContext.LJV_MODULO
	            
	            	
	            select new LjvModulo()		
	            {
	            
                DescModulo = entity0.DESC_MODULO
                , Icone = entity0.ICONE
                , IdModulo = entity0.ID_MODULO
                , IdTcsAplicativo = entity0.ID_TCS_APLICATIVO
                , Inativo = entity0.INATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , NomeCurto = entity0.NOME_CURTO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get LjvModuloMenuNoAssociations.
	    public IQueryable<LjvModuloMenu> GetLjvModuloMenuNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<LjvModuloMenu> result = 
	            (from entity0 in this.DbContext.LJV_MODULO_MENU
                  let entity0Al1 = entity0.LJV_MODULO
                  let entity0Al2 = entity0.LJV_MODULO_MENU1
	            
	            	
	            select new LjvModuloMenu()		
	            {
	            
                DescModulo = entity0Al1.DESC_MODULO
                , DescModuloMenu = entity0.DESC_MODULO_MENU
                , DescModuloMenuSuperior = entity0Al2.DESC_MODULO_MENU
                , Icone = entity0.ICONE
                , IdModulo = entity0Al1.ID_MODULO
                , IdModuloMenu = entity0.ID_MODULO_MENU
                , IdModuloMenuSuperior = entity0Al2.ID_MODULO_MENU
                , IdTcsAplicativo = entity0Al1.ID_TCS_APLICATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , NomeCurto = entity0.NOME_CURTO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get LjvTransacaoMenu.
	    public IQueryable<LjvTransacaoMenu> GetLjvTransacaoMenu()
	    {




		
	
	        
		
	        
	
	        IQueryable<LjvTransacaoMenu> result = 
	            (from entity0 in this.DbContext.LJV_TRANSACAO_MENU
                  let entity0Al1 = entity0.LJV_TRANSACAO
                  let entity0Al2 = entity0.LJV_MODULO_MENU
	            
	            	
	            select new LjvTransacaoMenu()		
	            {
	            
                ClasseNome = entity0Al1.CLASSE_NOME
                , CodTransacao = entity0Al1.COD_TRANSACAO
                , DescTransacao = entity0Al1.DESC_TRANSACAO
                , Icone = entity0Al1.ICONE
                , IdModuloMenu = entity0Al2.ID_MODULO_MENU
                , IdTransacao = entity0Al1.ID_TRANSACAO
                , IdTransacaoMenu = entity0.ID_TRANSACAO_MENU
                , Inativo = entity0Al1.INATIVO
                , InativoMenu = entity0.INATIVO
                , LxCorFundo = entity0Al1.LX_COR_FUNDO
                , LxTipoTransacao = entity0Al1.LX_TIPO_TRANSACAO
                , NomeCurto = entity0Al1.NOME_CURTO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get LjvTransacaoMenuNoAssociations.
	    public IQueryable<LjvTransacaoMenu> GetLjvTransacaoMenuNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<LjvTransacaoMenu> result = 
	            (from entity0 in this.DbContext.LJV_TRANSACAO_MENU
                  let entity0Al1 = entity0.LJV_TRANSACAO
                  let entity0Al2 = entity0.LJV_MODULO_MENU
	            
	            	
	            select new LjvTransacaoMenu()		
	            {
	            
                ClasseNome = entity0Al1.CLASSE_NOME
                , CodTransacao = entity0Al1.COD_TRANSACAO
                , DescTransacao = entity0Al1.DESC_TRANSACAO
                , Icone = entity0Al1.ICONE
                , IdModuloMenu = entity0Al2.ID_MODULO_MENU
                , IdTransacao = entity0Al1.ID_TRANSACAO
                , IdTransacaoMenu = entity0.ID_TRANSACAO_MENU
                , Inativo = entity0Al1.INATIVO
                , InativoMenu = entity0.INATIVO
                , LxCorFundo = entity0Al1.LX_COR_FUNDO
                , LxTipoTransacao = entity0Al1.LX_TIPO_TRANSACAO
                , NomeCurto = entity0Al1.NOME_CURTO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for LJV_MODULO
	    	string[] bmDisabledLjvModuloList = this.GetEDM().GetFilteringDisabledList("LJV_MODULO");
	    	if (bmDisabledLjvModuloList.Length > 0)
	    	{
	
	    		if (bmDisabledLjvModuloList.Contains("LJV_MODULO.DESC_MODULO"))
	    		{
	    			result.Add("LjvModulo|DescModulo");
	    			result.Add("LjvModulo|LJV_MODULO.DESC_MODULO");
	    		}
	
	    		if (bmDisabledLjvModuloList.Contains("LJV_MODULO.ICONE"))
	    		{
	    			result.Add("LjvModulo|Icone");
	    			result.Add("LjvModulo|LJV_MODULO.ICONE");
	    		}
	
	    		if (bmDisabledLjvModuloList.Contains("LJV_MODULO.ID_MODULO"))
	    		{
	    			result.Add("LjvModulo|IdModulo");
	    			result.Add("LjvModulo|LJV_MODULO.ID_MODULO");
	    		}
	
	    		if (bmDisabledLjvModuloList.Contains("LJV_MODULO.ID_TCS_APLICATIVO"))
	    		{
	    			result.Add("LjvModulo|IdTcsAplicativo");
	    			result.Add("LjvModulo|LJV_MODULO.ID_TCS_APLICATIVO");
	    		}
	
	    		if (bmDisabledLjvModuloList.Contains("LJV_MODULO.INATIVO"))
	    		{
	    			result.Add("LjvModulo|Inativo");
	    			result.Add("LjvModulo|LJV_MODULO.INATIVO");
	    		}
	
	    		if (bmDisabledLjvModuloList.Contains("LJV_MODULO.LX_COR_FUNDO"))
	    		{
	    			result.Add("LjvModulo|LxCorFundo");
	    			result.Add("LjvModulo|LJV_MODULO.LX_COR_FUNDO");
	    		}
	
	    		if (bmDisabledLjvModuloList.Contains("LJV_MODULO.NOME_CURTO"))
	    		{
	    			result.Add("LjvModulo|NomeCurto");
	    			result.Add("LjvModulo|LJV_MODULO.NOME_CURTO");
	    		}
	
	    		if (bmDisabledLjvModuloList.Contains("LJV_MODULO.ORDEM_NAVEGACAO"))
	    		{
	    			result.Add("LjvModulo|OrdemNavegacao");
	    			result.Add("LjvModulo|LJV_MODULO.ORDEM_NAVEGACAO");
	    		}
	    	}
	    	//Add filtering disabled property for LJV_MODULO_MENU
	    	string[] bmDisabledLjvModuloMenuList = this.GetEDM().GetFilteringDisabledList("LJV_MODULO_MENU");
	    	if (bmDisabledLjvModuloMenuList.Length > 0)
	    	{
	
	    		if (bmDisabledLjvModuloMenuList.Contains("LJV_MODULO_MENU.DESC_MODULO_MENU"))
	    		{
	    			result.Add("LjvModuloMenu|DescModuloMenu");
	    			result.Add("LjvModuloMenu|LJV_MODULO_MENU.DESC_MODULO_MENU");
	    		}
	
	    		if (bmDisabledLjvModuloMenuList.Contains("LJV_MODULO_MENU.ICONE"))
	    		{
	    			result.Add("LjvModuloMenu|Icone");
	    			result.Add("LjvModuloMenu|LJV_MODULO_MENU.ICONE");
	    		}
	
	    		if (bmDisabledLjvModuloMenuList.Contains("LJV_MODULO_MENU.ID_MODULO_MENU"))
	    		{
	    			result.Add("LjvModuloMenu|IdModuloMenu");
	    			result.Add("LjvModuloMenu|LJV_MODULO_MENU.ID_MODULO_MENU");
	    		}
	
	    		if (bmDisabledLjvModuloMenuList.Contains("LJV_MODULO_MENU.LX_COR_FUNDO"))
	    		{
	    			result.Add("LjvModuloMenu|LxCorFundo");
	    			result.Add("LjvModuloMenu|LJV_MODULO_MENU.LX_COR_FUNDO");
	    		}
	
	    		if (bmDisabledLjvModuloMenuList.Contains("LJV_MODULO_MENU.NOME_CURTO"))
	    		{
	    			result.Add("LjvModuloMenu|NomeCurto");
	    			result.Add("LjvModuloMenu|LJV_MODULO_MENU.NOME_CURTO");
	    		}
	
	    		if (bmDisabledLjvModuloMenuList.Contains("LJV_MODULO_MENU.ORDEM_NAVEGACAO"))
	    		{
	    			result.Add("LjvModuloMenu|OrdemNavegacao");
	    			result.Add("LjvModuloMenu|LJV_MODULO_MENU.ORDEM_NAVEGACAO");
	    		}
	    	}
	    	//Add filtering disabled property for LJV_TRANSACAO_MENU
	    	string[] bmDisabledLjvTransacaoMenuList = this.GetEDM().GetFilteringDisabledList("LJV_TRANSACAO_MENU");
	    	if (bmDisabledLjvTransacaoMenuList.Length > 0)
	    	{
	
	    		if (bmDisabledLjvTransacaoMenuList.Contains("LJV_TRANSACAO_MENU.ID_TRANSACAO_MENU"))
	    		{
	    			result.Add("LjvTransacaoMenu|IdTransacaoMenu");
	    			result.Add("LjvTransacaoMenu|LJV_TRANSACAO_MENU.ID_TRANSACAO_MENU");
	    		}
	
	    		if (bmDisabledLjvTransacaoMenuList.Contains("LJV_TRANSACAO_MENU.INATIVO"))
	    		{
	    			result.Add("LjvTransacaoMenu|InativoMenu");
	    			result.Add("LjvTransacaoMenu|LJV_TRANSACAO_MENU.INATIVO");
	    		}
	
	    		if (bmDisabledLjvTransacaoMenuList.Contains("LJV_TRANSACAO_MENU.ORDEM_NAVEGACAO"))
	    		{
	    			result.Add("LjvTransacaoMenu|OrdemNavegacao");
	    			result.Add("LjvTransacaoMenu|LJV_TRANSACAO_MENU.ORDEM_NAVEGACAO");
	    		}
	    	}
	    	return result;
	    }

	    #endregion Excluded Filters

	    #region Get By EntitySearchId Methods Definitions.
	
				
	    [Ignore]
	    //Get LjvModulo By EntitySearchId.
	    public IQueryable<LjvModulo> GetLjvModuloByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetLjvModuloByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get LjvModuloMenu By EntitySearchId.
	    public IQueryable<LjvModuloMenu> GetLjvModuloMenuByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetLjvModuloMenuByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get LjvModulo By EntitySearchId.
	    public IQueryable<LjvModulo> GetLjvModuloByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetLjvModuloByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get LjvModuloMenu By EntitySearchId.
	    public IQueryable<LjvModuloMenu> GetLjvModuloMenuByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetLjvModuloMenuByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get LjvTransacaoMenu By EntitySearchId.
	    public IQueryable<LjvTransacaoMenu> GetLjvTransacaoMenuByEntitySearchId(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetLjvTransacaoMenuByEntitySearch(queryAnalysis[0], queryAnalysis[1]);
	    }
				
	    [Ignore]
	    //Get LjvTransacaoMenu By EntitySearchId.
	    public IQueryable<LjvTransacaoMenu> GetLjvTransacaoMenuByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string[] queryAnalysis = BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchId);
	            return this.GetLjvTransacaoMenuByEntitySearchNoAssociations(queryAnalysis[0], queryAnalysis[1]);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get LjvModulo By Example.
	    [Ignore]
	    public IQueryable<LjvModulo> GetLjvModuloByExample(LjvModulo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetLjvModuloByEntitySearch(queryAnalysis);
	    }
			
	    //Get LjvModuloMenu By Example.
	    [Ignore]
	    public IQueryable<LjvModuloMenu> GetLjvModuloMenuByExample(LjvModuloMenu entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetLjvModuloMenuByEntitySearch(queryAnalysis);
	    }
			
	    //Get LjvModulo By Example.
	    [Ignore]
	    public IQueryable<LjvModulo> GetLjvModuloByExampleNoAssociations(LjvModulo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetLjvModuloByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get LjvModuloMenu By Example.
	    [Ignore]
	    public IQueryable<LjvModuloMenu> GetLjvModuloMenuByExampleNoAssociations(LjvModuloMenu entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetLjvModuloMenuByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get LjvTransacaoMenu By Example.
	    [Ignore]
	    public IQueryable<LjvTransacaoMenu> GetLjvTransacaoMenuByExample(LjvTransacaoMenu entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetLjvTransacaoMenuByEntitySearch(queryAnalysis);
	    }
			
	    //Get LjvTransacaoMenu By Example.
	    [Ignore]
	    public IQueryable<LjvTransacaoMenu> GetLjvTransacaoMenuByExampleNoAssociations(LjvTransacaoMenu entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetLjvTransacaoMenuByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public LjvModulo GetLjvModuloByKey(Int64 idModulo)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("LjvModulo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdModulo"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idModulo));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetLjvModuloByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public LjvModuloMenu GetLjvModuloMenuByKey(Int64 idModuloMenu)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("LjvModuloMenu");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdModuloMenu"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idModuloMenu));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetLjvModuloMenuByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public LjvTransacaoMenu GetLjvTransacaoMenuByKey(Int64 idTransacaoMenu)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("LjvTransacaoMenu");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTransacaoMenu"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTransacaoMenu));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetLjvTransacaoMenuByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get LjvModuloByEntitySearch.
	    public IQueryable<LjvModulo> GetLjvModuloByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LjvModulo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<LjvModulo> result = 
	            (from entity0 in this.DbContext.LJV_MODULO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new LjvModulo()		
	            {
	            
                DescModulo = entity0.DESC_MODULO
                , Icone = entity0.ICONE
                , IdModulo = entity0.ID_MODULO
                , IdTcsAplicativo = entity0.ID_TCS_APLICATIVO
                , Inativo = entity0.INATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , NomeCurto = entity0.NOME_CURTO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
			
                ,LjvModuloMenuList = 
	                        (from entity1 in entity0.LJV_MODULO_MENU_LISTA
                                  let entity1Al1 = entity1.LJV_MODULO
                                  let entity1Al2 = entity1.LJV_MODULO_MENU1
	                        
	                        	
	                        select new LjvModuloMenu()
	                        {
	                        
                                DescModulo = entity1Al1.DESC_MODULO
                                , DescModuloMenu = entity1.DESC_MODULO_MENU
                                , DescModuloMenuSuperior = entity1Al2.DESC_MODULO_MENU
                                , Icone = entity1.ICONE
                                , IdModulo = entity1Al1.ID_MODULO
                                , IdModuloMenu = entity1.ID_MODULO_MENU
                                , IdModuloMenuSuperior = entity1Al2.ID_MODULO_MENU
                                , IdTcsAplicativo = entity1Al1.ID_TCS_APLICATIVO
                                , LxCorFundo = entity1.LX_COR_FUNDO
                                , NomeCurto = entity1.NOME_CURTO
                                , OrdemNavegacao = entity1.ORDEM_NAVEGACAO
		
	                        }
	                        )
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get LjvModuloMenuByEntitySearch.
	    public IQueryable<LjvModuloMenu> GetLjvModuloMenuByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LjvModuloMenu));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<LjvModuloMenu> result = 
	            (from entity0 in this.DbContext.LJV_MODULO_MENU.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.LJV_MODULO
                  let entity0Al2 = entity0.LJV_MODULO_MENU1
	            
	            	
	            select new LjvModuloMenu()		
	            {
	            
                DescModulo = entity0Al1.DESC_MODULO
                , DescModuloMenu = entity0.DESC_MODULO_MENU
                , DescModuloMenuSuperior = entity0Al2.DESC_MODULO_MENU
                , Icone = entity0.ICONE
                , IdModulo = entity0Al1.ID_MODULO
                , IdModuloMenu = entity0.ID_MODULO_MENU
                , IdModuloMenuSuperior = entity0Al2.ID_MODULO_MENU
                , IdTcsAplicativo = entity0Al1.ID_TCS_APLICATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , NomeCurto = entity0.NOME_CURTO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get LjvModuloByEntitySearchNoAssociations.
	    public IQueryable<LjvModulo> GetLjvModuloByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LjvModulo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<LjvModulo> result = 
	            (from entity0 in this.DbContext.LJV_MODULO.Where(dynQuery, parameters.ToArray())
	            
	            	
	            select new LjvModulo()		
	            {
	            
                DescModulo = entity0.DESC_MODULO
                , Icone = entity0.ICONE
                , IdModulo = entity0.ID_MODULO
                , IdTcsAplicativo = entity0.ID_TCS_APLICATIVO
                , Inativo = entity0.INATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , NomeCurto = entity0.NOME_CURTO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get LjvModuloMenuByEntitySearchNoAssociations.
	    public IQueryable<LjvModuloMenu> GetLjvModuloMenuByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LjvModuloMenu));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<LjvModuloMenu> result = 
	            (from entity0 in this.DbContext.LJV_MODULO_MENU.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.LJV_MODULO
                  let entity0Al2 = entity0.LJV_MODULO_MENU1
	            
	            	
	            select new LjvModuloMenu()		
	            {
	            
                DescModulo = entity0Al1.DESC_MODULO
                , DescModuloMenu = entity0.DESC_MODULO_MENU
                , DescModuloMenuSuperior = entity0Al2.DESC_MODULO_MENU
                , Icone = entity0.ICONE
                , IdModulo = entity0Al1.ID_MODULO
                , IdModuloMenu = entity0.ID_MODULO_MENU
                , IdModuloMenuSuperior = entity0Al2.ID_MODULO_MENU
                , IdTcsAplicativo = entity0Al1.ID_TCS_APLICATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , NomeCurto = entity0.NOME_CURTO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get LjvModuloMenuParentCompositionByEntitySearchNoAssociations.
	    public IQueryable<LjvModuloMenuParentComposition> GetLjvModuloMenuParentCompositionByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceParentCompositionDataKey(entitySearchList, "LJV_MODULO", "LJV_MODULO_MENU", "LJV_MODULO", typeof(LjvModuloMenuParentComposition));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<LjvModuloMenuParentComposition> result = 
	            (from entity0 in this.DbContext.LJV_MODULO_MENU.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.LJV_MODULO
                  let entity0Al2 = entity0.LJV_MODULO_MENU1
	            
	            	
	            select new LjvModuloMenuParentComposition()		
	            {
	            
                DescModulo = entity0Al1.DESC_MODULO
                , DescModuloMenu = entity0.DESC_MODULO_MENU
                , DescModuloMenuSuperior = entity0Al2.DESC_MODULO_MENU
                , Icone = entity0.ICONE
                , IdModulo = entity0Al1.ID_MODULO
                , IdModuloMenu = entity0.ID_MODULO_MENU
                , IdModuloMenuSuperior = entity0Al2.ID_MODULO_MENU
                , IdTcsAplicativo = entity0Al1.ID_TCS_APLICATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , NomeCurto = entity0.NOME_CURTO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
                //LjvModulo Properties.
                , Inativo = entity0.LJV_MODULO.INATIVO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get LjvTransacaoMenuByEntitySearch.
	    public IQueryable<LjvTransacaoMenu> GetLjvTransacaoMenuByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LjvTransacaoMenu));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<LjvTransacaoMenu> result = 
	            (from entity0 in this.DbContext.LJV_TRANSACAO_MENU.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.LJV_TRANSACAO
                  let entity0Al2 = entity0.LJV_MODULO_MENU
	            
	            	
	            select new LjvTransacaoMenu()		
	            {
	            
                ClasseNome = entity0Al1.CLASSE_NOME
                , CodTransacao = entity0Al1.COD_TRANSACAO
                , DescTransacao = entity0Al1.DESC_TRANSACAO
                , Icone = entity0Al1.ICONE
                , IdModuloMenu = entity0Al2.ID_MODULO_MENU
                , IdTransacao = entity0Al1.ID_TRANSACAO
                , IdTransacaoMenu = entity0.ID_TRANSACAO_MENU
                , Inativo = entity0Al1.INATIVO
                , InativoMenu = entity0.INATIVO
                , LxCorFundo = entity0Al1.LX_COR_FUNDO
                , LxTipoTransacao = entity0Al1.LX_TIPO_TRANSACAO
                , NomeCurto = entity0Al1.NOME_CURTO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get LjvTransacaoMenuByEntitySearchNoAssociations.
	    public IQueryable<LjvTransacaoMenu> GetLjvTransacaoMenuByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LjvTransacaoMenu));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<LjvTransacaoMenu> result = 
	            (from entity0 in this.DbContext.LJV_TRANSACAO_MENU.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.LJV_TRANSACAO
                  let entity0Al2 = entity0.LJV_MODULO_MENU
	            
	            	
	            select new LjvTransacaoMenu()		
	            {
	            
                ClasseNome = entity0Al1.CLASSE_NOME
                , CodTransacao = entity0Al1.COD_TRANSACAO
                , DescTransacao = entity0Al1.DESC_TRANSACAO
                , Icone = entity0Al1.ICONE
                , IdModuloMenu = entity0Al2.ID_MODULO_MENU
                , IdTransacao = entity0Al1.ID_TRANSACAO
                , IdTransacaoMenu = entity0.ID_TRANSACAO_MENU
                , Inativo = entity0Al1.INATIVO
                , InativoMenu = entity0.INATIVO
                , LxCorFundo = entity0Al1.LX_COR_FUNDO
                , LxTipoTransacao = entity0Al1.LX_TIPO_TRANSACAO
                , NomeCurto = entity0Al1.NOME_CURTO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get PagedLjvModulo.
	    public IQueryable<LjvModulo> GetPagedLjvModulo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LjvModulo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<LjvModulo> result = 
	            (from entity0 in this.DbContext.LJV_MODULO.Where(dynQuery, parameters.ToArray())
                orderby entity0.ID_MODULO ascending
	            
	            	
	            select new LjvModulo()		
	            {
	            
                DescModulo = entity0.DESC_MODULO
                , Icone = entity0.ICONE
                , IdModulo = entity0.ID_MODULO
                , IdTcsAplicativo = entity0.ID_TCS_APLICATIVO
                , Inativo = entity0.INATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , NomeCurto = entity0.NOME_CURTO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedLjvModuloMenu.
	    public IQueryable<LjvModuloMenu> GetPagedLjvModuloMenu(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LjvModuloMenu));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<LjvModuloMenu> result = 
	            (from entity0 in this.DbContext.LJV_MODULO_MENU.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.LJV_MODULO
                  let entity0Al2 = entity0.LJV_MODULO_MENU1
                orderby entity0.ID_MODULO_MENU ascending
	            
	            	
	            select new LjvModuloMenu()		
	            {
	            
                DescModulo = entity0Al1.DESC_MODULO
                , DescModuloMenu = entity0.DESC_MODULO_MENU
                , DescModuloMenuSuperior = entity0Al2.DESC_MODULO_MENU
                , Icone = entity0.ICONE
                , IdModulo = entity0Al1.ID_MODULO
                , IdModuloMenu = entity0.ID_MODULO_MENU
                , IdModuloMenuSuperior = entity0Al2.ID_MODULO_MENU
                , IdTcsAplicativo = entity0Al1.ID_TCS_APLICATIVO
                , LxCorFundo = entity0.LX_COR_FUNDO
                , NomeCurto = entity0.NOME_CURTO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetLjvModuloCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LjvModulo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.LJV_MODULO.Where(dynQuery, parameters.ToArray())
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetLjvModuloMenuCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LjvModuloMenu));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.LJV_MODULO_MENU.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.LJV_MODULO
                  let entityAl2 = entity.LJV_MODULO_MENU1
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedLjvTransacaoMenu.
	    public IQueryable<LjvTransacaoMenu> GetPagedLjvTransacaoMenu(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LjvTransacaoMenu));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<LjvTransacaoMenu> result = 
	            (from entity0 in this.DbContext.LJV_TRANSACAO_MENU.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.LJV_TRANSACAO
                  let entity0Al2 = entity0.LJV_MODULO_MENU
                orderby entity0.ID_TRANSACAO_MENU ascending
	            
	            	
	            select new LjvTransacaoMenu()		
	            {
	            
                ClasseNome = entity0Al1.CLASSE_NOME
                , CodTransacao = entity0Al1.COD_TRANSACAO
                , DescTransacao = entity0Al1.DESC_TRANSACAO
                , Icone = entity0Al1.ICONE
                , IdModuloMenu = entity0Al2.ID_MODULO_MENU
                , IdTransacao = entity0Al1.ID_TRANSACAO
                , IdTransacaoMenu = entity0.ID_TRANSACAO_MENU
                , Inativo = entity0Al1.INATIVO
                , InativoMenu = entity0.INATIVO
                , LxCorFundo = entity0Al1.LX_COR_FUNDO
                , LxTipoTransacao = entity0Al1.LX_TIPO_TRANSACAO
                , NomeCurto = entity0Al1.NOME_CURTO
                , OrdemNavegacao = entity0.ORDEM_NAVEGACAO
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetLjvTransacaoMenuCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(LjvTransacaoMenu));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.LJV_TRANSACAO_MENU.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.LJV_TRANSACAO
                  let entityAl2 = entity.LJV_MODULO_MENU
	            
	            select 1
	            ).Count();	
		
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update LjvModulo.
	    public void UpdateLjvModulo(LjvModulo entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert LjvModulo.
	    public void InsertLjvModulo(LjvModulo entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete LjvModulo.
	    public void DeleteLjvModulo(LjvModulo entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update LjvModuloMenu.
	    public void UpdateLjvModuloMenu(LjvModuloMenu entity)
	    {



	
	        if (entity.LjvModulo.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.LjvModulo) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, entity.LjvModulo); 	
	            

	
	        }
	
	    }

	    
	    [Insert()]
	    //Insert LjvModuloMenu.
	    public void InsertLjvModuloMenu(LjvModuloMenu entity)
	    {



	
	        if (entity.LjvModulo.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.LjvModulo) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, entity.LjvModulo);
	            

	
	        }
	
	    }

	    
	    [Delete()]
	    //Delete LjvModuloMenu.
	    public void DeleteLjvModuloMenu(LjvModuloMenu entity)
	    {



	
	        if (entity.LjvModulo.IsNull() || this.ChangeSet.IsNull() || this.ChangeSet.GetChangeOperation(entity.LjvModulo) != ChangeOperation.Delete)
	        {

	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, entity.LjvModulo);
	            

	
	        }

	
	    }
		
			
	    
	    [Update()]	
	    //Update LjvTransacaoMenu.
	    public void UpdateLjvTransacaoMenu(LjvTransacaoMenu entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert LjvTransacaoMenu.
	    public void InsertLjvTransacaoMenu(LjvTransacaoMenu entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete LjvTransacaoMenu.
	    public void DeleteLjvTransacaoMenu(LjvTransacaoMenu entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}