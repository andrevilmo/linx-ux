					
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
using Linx.Framework.Autorizacao.BM;

namespace Linx.Framework.Setup.LinxAutoSetup
{  

	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_EMPRESA_AUTENTICACAO.ID_LINX", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsEmpresaAutenticacao,TcsEmpresaAutenticacao.TcsEmpresaAutenticacaoModulo];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[];EntityRelations[ ];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsEmpresaAutenticacao")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.Setup.LinxAutoSetup.TcsEmpresaAutenticacao")]
	public partial class TcsEmpresaAutenticacao : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.TcsEmpresaAutenticacaoModuloList != null && this.TcsEmpresaAutenticacaoModuloList.Count() > 0)
	      {
	         foreach (var entity in this.TcsEmpresaAutenticacaoModuloList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.TcsEmpresaAutenticacaoModuloList != null)
	      {
	         foreach (var detail in this.TcsEmpresaAutenticacaoModuloList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsEmpresaAutenticacaoModuloList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(LinxAutoSetupDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsEmpresaAutenticacaoModulo"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsEmpresaAutenticacaoModulo");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdLinx"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdLinx));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsEmpresaAutenticacaoModulo and all sub-details
	         if (this.TcsEmpresaAutenticacaoModuloList == null || this.TcsEmpresaAutenticacaoModuloList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsEmpresaAutenticacaoModuloList = context.GetPagedTcsEmpresaAutenticacaoModulo(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsEmpresaAutenticacaoModuloList = (from r in context.GetTcsEmpresaAutenticacaoModuloByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsEmpresaAutenticacaoModuloElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsEmpresaAutenticacaoModulo && ((TcsEmpresaAutenticacaoModulo)e.Entity).TcsEmpresaAutenticacao == null && e.Associations == null && e.OriginalAssociations == null && ((TcsEmpresaAutenticacaoModulo)e.Entity).IdLinx == this.IdLinx).ToList();
 	      if (_TcsEmpresaAutenticacaoModuloElements.Count > 0 && this.TcsEmpresaAutenticacaoModuloList.Count() == 0)
 	      {
 	          this.TcsEmpresaAutenticacaoModuloList = _TcsEmpresaAutenticacaoModuloElements.Select(e => (TcsEmpresaAutenticacaoModulo)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsEmpresaAutenticacaoModuloElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsEmpresaAutenticacaoModulo)detail.Entity).TcsEmpresaAutenticacao = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsEmpresaAutenticacao", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsEmpresaAutenticacaoModuloList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For CnpjCpf
	    partial void OnCnpjCpfChanging(string value);
	    partial void OnCnpjCpfChanged();

	    private string _CnpjCpf;

	    [DataMember(Name = "CnpjCpf", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = " Cnpj", Description="", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[##.###.###/####-##];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_EMPRESA_AUTENTICACAO.CNPJ_CPF];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_AUTENTICACAO.CNPJ_CPF")]
	    public string CnpjCpf
	    {
	    	    get
	    	    {
	    	          return _CnpjCpf;
	    	    }
	    	    set
	    	    {
	    	          if (this._CnpjCpf != value)
	    	          {
	    	              this.ValidateProperty("CnpjCpf", value);
	    	              this.OnCnpjCpfChanging(value);
	    	              this.RaiseDataMemberChanging("CnpjCpf");
	    	              this._CnpjCpf = value;
	    	              this.RaiseDataMemberChanged("CnpjCpf");
	    	              this.OnCnpjCpfChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLinx
	    partial void OnIdLinxChanging(int value);
	    partial void OnIdLinxChanged();

	    private int _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Linx", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
	    public int IdLinx
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
	    //Extensibility Partial Method Definitions For NomeEmpresa
	    partial void OnNomeEmpresaChanging(string value);
	    partial void OnNomeEmpresaChanged();

	    private string _NomeEmpresa;

	    [DataMember(IsRequired = true, Name = "NomeEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Empresa", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_AUTENTICACAO.NOME_EMPRESA")]
	    public string NomeEmpresa
	    {
	    	    get
	    	    {
	    	          return _NomeEmpresa;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeEmpresa != value)
	    	          {
	    	              this.ValidateProperty("NomeEmpresa", value);
	    	              this.OnNomeEmpresaChanging(value);
	    	              this.RaiseDataMemberChanging("NomeEmpresa");
	    	              this._NomeEmpresa = value;
	    	              this.RaiseDataMemberChanged("NomeEmpresa");
	    	              this.OnNomeEmpresaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidEmpresa
	    partial void OnUidEmpresaChanging(Guid value);
	    partial void OnUidEmpresaChanged();

	    private Guid _UidEmpresa;

	    [DataMember(IsRequired = true, Name = "UidEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Empresa", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA")]
	    public Guid UidEmpresa
	    {
	    	    get
	    	    {
	    	          return _UidEmpresa;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidEmpresa != value)
	    	          {
	    	              this.ValidateProperty("UidEmpresa", value);
	    	              this.OnUidEmpresaChanging(value);
	    	              this.RaiseDataMemberChanging("UidEmpresa");
	    	              this._UidEmpresa = value;
	    	              this.RaiseDataMemberChanged("UidEmpresa");
	    	              this.OnUidEmpresaChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<TcsEmpresaAutenticacaoModulo> _TcsEmpresaAutenticacaoModuloList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsEmpresaAutenticacao_TcsEmpresaAutenticacaoModulo", "IdLinx", "IdLinx", IsForeignKey=false)]
	    [DataMember(Name = "TcsEmpresaAutenticacaoModuloList", EmitDefaultValue = true)]
	    public IEnumerable<TcsEmpresaAutenticacaoModulo> TcsEmpresaAutenticacaoModuloList
	    {
	        get
	        {
	
	            if (this._TcsEmpresaAutenticacaoModuloList == null)
	            	this._TcsEmpresaAutenticacaoModuloList = new List<TcsEmpresaAutenticacaoModulo>();
	
	            return this._TcsEmpresaAutenticacaoModuloList;
	        }
	        set
	        {
	            if (this._TcsEmpresaAutenticacaoModuloList != value)
	            {
	                this._TcsEmpresaAutenticacaoModuloList = value;
	                this.RaisePropertyChanged("TcsEmpresaAutenticacaoModuloList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		
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

		

	[LinxPublicationView(PrimaryKeys="TCS_EMPRESA_MODULO.ID_TCS_EMPRESA_MODULO", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsEmpresaModulo];ReadOnly[false];Entities[:IdTcsEmpresaModulo];SubQueryInfo[Select 1 From #ParentAlias#. as #Alias#];EdmEntityName[];EntityRelations[ ];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsEmpresaAutenticacaoModulo")]
	[Serializable()]
	public partial class TcsEmpresaAutenticacaoModulo : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(LinxAutoSetupDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsEmpresaAutenticacao");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdLinx"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdLinx));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsEmpresaAutenticacao
	         this.TcsEmpresaAutenticacao = (from r in context.GetTcsEmpresaAutenticacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For IdLinx
	    partial void OnIdLinxChanging(Int32 value);
	    partial void OnIdLinxChanged();

	    private Int32 _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "ID Linx", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_EMPRESA_MODULO.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_MODULO.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
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
	    //Extensibility Partial Method Definitions For IdModulo
	    partial void OnIdModuloChanging(Int64 value);
	    partial void OnIdModuloChanged();

	    private Int64 _IdModulo;

	    [DataMember(IsRequired = true, Name = "IdModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsModuloAutorizacao];LookUpTitle[Seleção de (Id Modulo)];LookUpQuery[executeLookUpTcsModuloAutorizacao];LookUpFinalize[finalizeLookUpTcsModuloAutorizacao];LookUpDisplayColumns[{\"IdModulo\" : \"Id Modulo\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\"}];LookUpColumns[{\"IdModulo\" : false, \"IdTcsAplicativo\" : false}];FilterDataKey[TCS_EMPRESA_MODULO.TCS_MODULO_AUTORIZACAO.ID_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdModulo#true##24:0##Id Modulo#1#false##::LookUpTcsModuloAutorizacao##true#false#TCS_MODULO_AUTORIZACAO#TCS_MODULO_AUTORIZACAO#Linx.Framework.BV.Empresa#IQueryable#DescricaoAplicativo,IdTcsAplicativo[DescricaoAplicativo,IdTcsAplicativo]#DescModulo[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdModulo[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo]#true#true", EdmKey="TCS_EMPRESA_MODULO.TCS_MODULO_AUTORIZACAO.ID_MODULO")]
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
	    [Display(Name = "Id Tcs Aplicativo", Description="", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsModuloAutorizacao];LookUpTitle[Seleção de (Id Tcs Aplicativo)];LookUpQuery[executeLookUpTcsModuloAutorizacao];LookUpFinalize[finalizeLookUpTcsModuloAutorizacao];LookUpDisplayColumns[{\"IdModulo\" : \"Id Modulo\", \"IdTcsAplicativo\" : \"Id Tcs Aplicativo\"}];LookUpColumns[{\"IdModulo\" : false, \"IdTcsAplicativo\" : false}];FilterDataKey[TCS_EMPRESA_MODULO.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdTcsAplicativo#false##12:0##Id Tcs Aplicativo#3#false##::LookUpTcsModuloAutorizacao##true#false#TCS_MODULO_AUTORIZACAO#TCS_MODULO_AUTORIZACAO#Linx.Framework.BV.Empresa#IQueryable#DescricaoAplicativo,IdTcsAplicativo[DescricaoAplicativo,IdTcsAplicativo]#DescModulo[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdModulo[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo]#true#true", EdmKey="TCS_EMPRESA_MODULO.TCS_MODULO_AUTORIZACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For IdTcsEmpresaModulo
	    partial void OnIdTcsEmpresaModuloChanging(Int32 value);
	    partial void OnIdTcsEmpresaModuloChanged();

	    private Int32 _IdTcsEmpresaModulo;

	    [DataMember(IsRequired = true, Name = "IdTcsEmpresaModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Empresa Modulo", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_EMPRESA_MODULO.ID_TCS_EMPRESA_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_MODULO.ID_TCS_EMPRESA_MODULO")]
	    public Int32 IdTcsEmpresaModulo
	    {
	    	    get
	    	    {
	    	          return _IdTcsEmpresaModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsEmpresaModulo != value)
	    	          {
	    	              this.ValidateProperty("IdTcsEmpresaModulo", value);
	    	              this.OnIdTcsEmpresaModuloChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsEmpresaModulo");
	    	              this._IdTcsEmpresaModulo = value;
	    	              this.RaiseDataMemberChanged("IdTcsEmpresaModulo");
	    	              this.OnIdTcsEmpresaModuloChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdTcsEmpresaModulo;
	    [DataMember(Name = "TemporaryIdTcsEmpresaModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Empresa Modulo (Tmp)", Description="Temporary Key", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdTcsEmpresaModulo
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsEmpresaModulo.IsNullOrEmpty())
	    	                this._TemporaryIdTcsEmpresaModulo = this._IdTcsEmpresaModulo;
	    	          return this._TemporaryIdTcsEmpresaModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsEmpresaModulo != value)
	    	              this._TemporaryIdTcsEmpresaModulo = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsEmpresaAutenticacao _TcsEmpresaAutenticacao;
	    [DataMember(Name = "TcsEmpresaAutenticacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsEmpresaAutenticacao_TcsEmpresaAutenticacaoModulo", "IdLinx", "IdLinx", IsForeignKey=true)]
	    public TcsEmpresaAutenticacao TcsEmpresaAutenticacao
	    {
	        get
	        {
	            return this._TcsEmpresaAutenticacao;
	        }
	        set
	        {
	            if (this._TcsEmpresaAutenticacao != value)
	            {
	                this._TcsEmpresaAutenticacao = value;
	                this.RaisePropertyChanged("TcsEmpresaAutenticacaoList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		
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

		

	[LinxPublicationView(PrimaryKeys="TCS_USUARIO_AUTENTICACAO.ID_USUARIO", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsUsuarioAutenticacao,TcsUsuarioAutenticacao.TcsUsuarioAutenticacaoAcesso];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdUsuario];ReadOnly[false];Entities[:IdUsuario];SubQueryInfo[];EdmEntityName[];EntityRelations[ ];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioAutenticacao")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.Setup.LinxAutoSetup.TcsUsuarioAutenticacao")]
	public partial class TcsUsuarioAutenticacao : Linx.Data.Entity
	{

	

	    public TcsUsuarioAutenticacao() : this(true) { }

	    public TcsUsuarioAutenticacao(bool setDefaults) 
	    {

	        if (setDefaults)
	        {
	        	        DataAlteracao = DateTime.Now;
	        	        DataCadastro = DateTime.Now;
	        	        DataExpiracaoSenha = DateTime.Now;
	        	        GeraSenhaUsuario = false;
	        	        VigenciaFinal = new DateTime(2099, 12, 31);
	        	        VigenciaInicial = DateTime.Now;
	        }	

	    }

			
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.TcsUsuarioAutenticacaoAcessoList != null && this.TcsUsuarioAutenticacaoAcessoList.Count() > 0)
	      {
	         foreach (var entity in this.TcsUsuarioAutenticacaoAcessoList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.TcsUsuarioAutenticacaoAcessoList != null)
	      {
	         foreach (var detail in this.TcsUsuarioAutenticacaoAcessoList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsUsuarioAutenticacaoAcessoList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(LinxAutoSetupDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsUsuarioAutenticacaoAcesso"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsUsuarioAutenticacaoAcesso");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdUsuario));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsUsuarioAutenticacaoAcesso and all sub-details
	         if (this.TcsUsuarioAutenticacaoAcessoList == null || this.TcsUsuarioAutenticacaoAcessoList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsUsuarioAutenticacaoAcessoList = context.GetPagedTcsUsuarioAutenticacaoAcesso(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsUsuarioAutenticacaoAcessoList = (from r in context.GetTcsUsuarioAutenticacaoAcessoByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsUsuarioAutenticacaoAcessoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioAutenticacaoAcesso && ((TcsUsuarioAutenticacaoAcesso)e.Entity).TcsUsuarioAutenticacao == null && e.Associations == null && e.OriginalAssociations == null && ((TcsUsuarioAutenticacaoAcesso)e.Entity).IdUsuario == this.IdUsuario).ToList();
 	      if (_TcsUsuarioAutenticacaoAcessoElements.Count > 0 && this.TcsUsuarioAutenticacaoAcessoList.Count() == 0)
 	      {
 	          this.TcsUsuarioAutenticacaoAcessoList = _TcsUsuarioAutenticacaoAcessoElements.Select(e => (TcsUsuarioAutenticacaoAcesso)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsUsuarioAutenticacaoAcessoElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsUsuarioAutenticacaoAcesso)detail.Entity).TcsUsuarioAutenticacao = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsUsuarioAutenticacao", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsUsuarioAutenticacaoAcessoList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For AutenticacaoWindows
	    partial void OnAutenticacaoWindowsChanging(Boolean value);
	    partial void OnAutenticacaoWindowsChanged();

	    private Boolean _AutenticacaoWindows;

	    [DataMember(IsRequired = true, Name = "AutenticacaoWindows", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Utiliza Autenticação Windows", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.AUTENTICACAO_WINDOWS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.AUTENTICACAO_WINDOWS")]
	    public Boolean AutenticacaoWindows
	    {
	    	    get
	    	    {
	    	          return _AutenticacaoWindows;
	    	    }
	    	    set
	    	    {
	    	          if (this._AutenticacaoWindows != value)
	    	          {
	    	              this.ValidateProperty("AutenticacaoWindows", value);
	    	              this.OnAutenticacaoWindowsChanging(value);
	    	              this.RaiseDataMemberChanging("AutenticacaoWindows");
	    	              this._AutenticacaoWindows = value;
	    	              this.RaiseDataMemberChanged("AutenticacaoWindows");
	    	              this.OnAutenticacaoWindowsChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CnpjCpf
	    partial void OnCnpjCpfChanging(String value);
	    partial void OnCnpjCpfChanged();

	    private String _CnpjCpf;

	    [DataMember(Name = "CnpjCpf", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "CPF/CNPJ", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[###.###.###-##];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.CNPJ_CPF];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.CNPJ_CPF")]
	    public String CnpjCpf
	    {
	    	    get
	    	    {
	    	          return _CnpjCpf;
	    	    }
	    	    set
	    	    {
	    	          if (this._CnpjCpf != value)
	    	          {
	    	              this.ValidateProperty("CnpjCpf", value);
	    	              this.OnCnpjCpfChanging(value);
	    	              this.RaiseDataMemberChanging("CnpjCpf");
	    	              this._CnpjCpf = value;
	    	              this.RaiseDataMemberChanged("CnpjCpf");
	    	              this.OnCnpjCpfChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ConfirmacaoUsuario
	    partial void OnConfirmacaoUsuarioChanging(String value);
	    partial void OnConfirmacaoUsuarioChanged();

	    private String _ConfirmacaoUsuario;

	    [DataMember(Name = "ConfirmacaoUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Senha", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[''];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="\"\"")]
	    public String ConfirmacaoUsuario
	    {
	    	    get
	    	    {
	    	          return _ConfirmacaoUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._ConfirmacaoUsuario != value)
	    	          {
	    	              this.ValidateProperty("ConfirmacaoUsuario", value);
	    	              this.OnConfirmacaoUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("ConfirmacaoUsuario");
	    	              this._ConfirmacaoUsuario = value;
	    	              this.RaiseDataMemberChanged("ConfirmacaoUsuario");
	    	              this.OnConfirmacaoUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ConfirmacaoUsuario1
	    partial void OnConfirmacaoUsuario1Changing(String value);
	    partial void OnConfirmacaoUsuario1Changed();

	    private String _ConfirmacaoUsuario1;

	    [DataMember(Name = "ConfirmacaoUsuario1", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Confirmação", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[''];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="\"\"")]
	    public String ConfirmacaoUsuario1
	    {
	    	    get
	    	    {
	    	          return _ConfirmacaoUsuario1;
	    	    }
	    	    set
	    	    {
	    	          if (this._ConfirmacaoUsuario1 != value)
	    	          {
	    	              this.ValidateProperty("ConfirmacaoUsuario1", value);
	    	              this.OnConfirmacaoUsuario1Changing(value);
	    	              this.RaiseDataMemberChanging("ConfirmacaoUsuario1");
	    	              this._ConfirmacaoUsuario1 = value;
	    	              this.RaiseDataMemberChanged("ConfirmacaoUsuario1");
	    	              this.OnConfirmacaoUsuario1Changed();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CriaUsuario
	    partial void OnCriaUsuarioChanging(Boolean value);
	    partial void OnCriaUsuarioChanged();

	    private Boolean _CriaUsuario;

	    [DataMember(IsRequired = true, Name = "CriaUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "CriaUsuario", Description="", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[false];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="false")]
	    public Boolean CriaUsuario
	    {
	    	    get
	    	    {
	    	          return _CriaUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._CriaUsuario != value)
	    	          {
	    	              this.ValidateProperty("CriaUsuario", value);
	    	              this.OnCriaUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("CriaUsuario");
	    	              this._CriaUsuario = value;
	    	              this.RaiseDataMemberChanged("CriaUsuario");
	    	              this.OnCriaUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DataAlteracao
	    partial void OnDataAlteracaoChanging(System.Nullable<DateTime> value);
	    partial void OnDataAlteracaoChanged();

	    private System.Nullable<DateTime> _DataAlteracao;

	    [DataMember(Name = "DataAlteracao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Alteração", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[DateTime.Now];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.DATA_ALTERACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.DATA_ALTERACAO")]
	    public System.Nullable<DateTime> DataAlteracao
	    {
	    	    get
	    	    {
	    	          return _DataAlteracao;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataAlteracao != value)
	    	          {
	    	              this.ValidateProperty("DataAlteracao", value);
	    	              this.OnDataAlteracaoChanging(value);
	    	              this.RaiseDataMemberChanging("DataAlteracao");
	    	              this._DataAlteracao = value;
	    	              this.RaiseDataMemberChanged("DataAlteracao");
	    	              this.OnDataAlteracaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DataCadastro
	    partial void OnDataCadastroChanging(System.Nullable<DateTime> value);
	    partial void OnDataCadastroChanged();

	    private System.Nullable<DateTime> _DataCadastro;

	    [DataMember(Name = "DataCadastro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cadastro", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[DateTime.Now];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.DATA_CADASTRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.DATA_CADASTRO")]
	    public System.Nullable<DateTime> DataCadastro
	    {
	    	    get
	    	    {
	    	          return _DataCadastro;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataCadastro != value)
	    	          {
	    	              this.ValidateProperty("DataCadastro", value);
	    	              this.OnDataCadastroChanging(value);
	    	              this.RaiseDataMemberChanging("DataCadastro");
	    	              this._DataCadastro = value;
	    	              this.RaiseDataMemberChanged("DataCadastro");
	    	              this.OnDataCadastroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DataExpiracaoSenha
	    partial void OnDataExpiracaoSenhaChanging(DateTime value);
	    partial void OnDataExpiracaoSenhaChanged();

	    private DateTime _DataExpiracaoSenha;

	    [DataMember(IsRequired = true, Name = "DataExpiracaoSenha", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Expiração Senha", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[DateTime.Now];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.DATA_EXPIRACAO_SENHA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.DATA_EXPIRACAO_SENHA")]
	    public DateTime DataExpiracaoSenha
	    {
	    	    get
	    	    {
	    	          return _DataExpiracaoSenha;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataExpiracaoSenha != value)
	    	          {
	    	              this.ValidateProperty("DataExpiracaoSenha", value);
	    	              this.OnDataExpiracaoSenhaChanging(value);
	    	              this.RaiseDataMemberChanging("DataExpiracaoSenha");
	    	              this._DataExpiracaoSenha = value;
	    	              this.RaiseDataMemberChanged("DataExpiracaoSenha");
	    	              this.OnDataExpiracaoSenhaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Email
	    partial void OnEmailChanging(String value);
	    partial void OnEmailChanged();

	    private String _Email;

	    [DataMember(IsRequired = true, Name = "Email", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Email", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.EMAIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.EMAIL")]
	    public String Email
	    {
	    	    get
	    	    {
	    	          return _Email;
	    	    }
	    	    set
	    	    {
	    	          if (this._Email != value)
	    	          {
	    	              this.ValidateProperty("Email", value);
	    	              this.OnEmailChanging(value);
	    	              this.RaiseDataMemberChanging("Email");
	    	              this._Email = value;
	    	              this.RaiseDataMemberChanged("Email");
	    	              this.OnEmailChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For GeraSenhaUsuario
	    partial void OnGeraSenhaUsuarioChanging(Boolean value);
	    partial void OnGeraSenhaUsuarioChanged();

	    private Boolean _GeraSenhaUsuario;

	    [DataMember(IsRequired = true, Name = "GeraSenhaUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "GeraSenhaUsuario", Description="", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[false];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[false];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="false")]
	    public Boolean GeraSenhaUsuario
	    {
	    	    get
	    	    {
	    	          return _GeraSenhaUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._GeraSenhaUsuario != value)
	    	          {
	    	              this.ValidateProperty("GeraSenhaUsuario", value);
	    	              this.OnGeraSenhaUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("GeraSenhaUsuario");
	    	              this._GeraSenhaUsuario = value;
	    	              this.RaiseDataMemberChanged("GeraSenhaUsuario");
	    	              this.OnGeraSenhaUsuarioChanged();
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
	    [Display(Name = "Grupo Econômico", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioEmpresaAutenticacao];LookUpTitle[Seleção de (Grupo Econômico)];LookUpQuery[executeLookUpTcsUsuarioEmpresaAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioEmpresaAutenticacao];LookUpDisplayColumns[{\"IdLinx\" : \"Grupo Econômico\"}];LookUpColumns[{\"IdLinx\" : true}];FilterDataKey[TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdLinx#true##12:0##Grupo Econômico#0#true##::LookUpTcsUsuarioEmpresaAutenticacao##false#false#TCS_EMPRESA_AUTENTICACAO#TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.UsuarioAutorizacao#IQueryable###true#false", EdmKey="TCS_USUARIO_AUTENTICACAO.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
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
	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(Int64 value);
	    partial void OnIdUsuarioChanged();

	    private Int64 _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.ID_USUARIO")]
	    public Int64 IdUsuario
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
	    //Extensibility Partial Method Definitions For LxPfjFisicaJuridica
	    partial void OnLxPfjFisicaJuridicaChanging(System.Nullable<Byte> value);
	    partial void OnLxPfjFisicaJuridicaChanged();

	    private System.Nullable<Byte> _LxPfjFisicaJuridica;

	    [DataMember(Name = "LxPfjFisicaJuridica", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Pessoa Física / Juridíca", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LX_PFJ_FISICA_JURIDICA];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.LX_PFJ_FISICA_JURIDICA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.LX_PFJ_FISICA_JURIDICA")]
	    public System.Nullable<Byte> LxPfjFisicaJuridica
	    {
	    	    get
	    	    {
	    	          return _LxPfjFisicaJuridica;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxPfjFisicaJuridica != value)
	    	          {
	    	              this.ValidateProperty("LxPfjFisicaJuridica", value);
	    	              this.OnLxPfjFisicaJuridicaChanging(value);
	    	              this.RaiseDataMemberChanging("LxPfjFisicaJuridica");
	    	              this._LxPfjFisicaJuridica = value;
	    	              this.RaiseDataMemberChanged("LxPfjFisicaJuridica");
	    	              this.OnLxPfjFisicaJuridicaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeAutenticacao
	    partial void OnNomeAutenticacaoChanging(String value);
	    partial void OnNomeAutenticacaoChanged();

	    private String _NomeAutenticacao;

	    [DataMember(IsRequired = true, Name = "NomeAutenticacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário Autenticação", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO")]
	    public String NomeAutenticacao
	    {
	    	    get
	    	    {
	    	          return _NomeAutenticacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeAutenticacao != value)
	    	          {
	    	              this.ValidateProperty("NomeAutenticacao", value);
	    	              this.OnNomeAutenticacaoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeAutenticacao");
	    	              this._NomeAutenticacao = value;
	    	              this.RaiseDataMemberChanged("NomeAutenticacao");
	    	              this.OnNomeAutenticacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeCurtoUsuario
	    partial void OnNomeCurtoUsuarioChanging(String value);
	    partial void OnNomeCurtoUsuarioChanged();

	    private String _NomeCurtoUsuario;

	    [DataMember(IsRequired = true, Name = "NomeCurtoUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Apelido", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.NOME_CURTO_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.NOME_CURTO_USUARIO")]
	    public String NomeCurtoUsuario
	    {
	    	    get
	    	    {
	    	          return _NomeCurtoUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeCurtoUsuario != value)
	    	          {
	    	              this.ValidateProperty("NomeCurtoUsuario", value);
	    	              this.OnNomeCurtoUsuarioChanging(value);
	    	              this.RaiseDataMemberChanging("NomeCurtoUsuario");
	    	              this._NomeCurtoUsuario = value;
	    	              this.RaiseDataMemberChanged("NomeCurtoUsuario");
	    	              this.OnNomeCurtoUsuarioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeUsuario
	    partial void OnNomeUsuarioChanging(String value);
	    partial void OnNomeUsuarioChanged();

	    private String _NomeUsuario;

	    [DataMember(IsRequired = true, Name = "NomeUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.NOME_USUARIO")]
	    public String NomeUsuario
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
	    //Extensibility Partial Method Definitions For UidUsuario
	    partial void OnUidUsuarioChanging(Guid value);
	    partial void OnUidUsuarioChanged();

	    private Guid _UidUsuario;

	    [DataMember(IsRequired = true, Name = "UidUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Usuario", Description="", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.UID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.UID_USUARIO")]
	    public Guid UidUsuario
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
	    //Extensibility Partial Method Definitions For VigenciaFinal
	    partial void OnVigenciaFinalChanging(DateTime value);
	    partial void OnVigenciaFinalChanged();

	    private DateTime _VigenciaFinal;

	    [DataMember(IsRequired = true, Name = "VigenciaFinal", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Vigência Final", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[new DateTime(2099, 12, 31)];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.VIGENCIA_FINAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.VIGENCIA_FINAL")]
	    public DateTime VigenciaFinal
	    {
	    	    get
	    	    {
	    	          return _VigenciaFinal;
	    	    }
	    	    set
	    	    {
	    	          if (this._VigenciaFinal != value)
	    	          {
	    	              this.ValidateProperty("VigenciaFinal", value);
	    	              this.OnVigenciaFinalChanging(value);
	    	              this.RaiseDataMemberChanging("VigenciaFinal");
	    	              this._VigenciaFinal = value;
	    	              this.RaiseDataMemberChanged("VigenciaFinal");
	    	              this.OnVigenciaFinalChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For VigenciaInicial
	    partial void OnVigenciaInicialChanging(DateTime value);
	    partial void OnVigenciaInicialChanged();

	    private DateTime _VigenciaInicial;

	    [DataMember(IsRequired = true, Name = "VigenciaInicial", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Vigência Inicial", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[DateTime.Now];DataFormatString[d];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[DateTimeTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_AUTENTICACAO.VIGENCIA_INICIAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_AUTENTICACAO.VIGENCIA_INICIAL")]
	    public DateTime VigenciaInicial
	    {
	    	    get
	    	    {
	    	          return _VigenciaInicial;
	    	    }
	    	    set
	    	    {
	    	          if (this._VigenciaInicial != value)
	    	          {
	    	              this.ValidateProperty("VigenciaInicial", value);
	    	              this.OnVigenciaInicialChanging(value);
	    	              this.RaiseDataMemberChanging("VigenciaInicial");
	    	              this._VigenciaInicial = value;
	    	              this.RaiseDataMemberChanged("VigenciaInicial");
	    	              this.OnVigenciaInicialChanged();
	    	          }
	    	    }
	    }

	    private Int64 _TemporaryIdUsuario;
	    [DataMember(Name = "TemporaryIdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario (Tmp)", Description="Temporary Key", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdUsuario
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdUsuario.IsNullOrEmpty())
	    	                this._TemporaryIdUsuario = this._IdUsuario;
	    	          return this._TemporaryIdUsuario;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdUsuario != value)
	    	              this._TemporaryIdUsuario = value;
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<TcsUsuarioAutenticacaoAcesso> _TcsUsuarioAutenticacaoAcessoList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsUsuarioAutenticacao_TcsUsuarioAutenticacaoAcesso", "IdUsuario", "IdUsuario", IsForeignKey=false)]
	    [DataMember(Name = "TcsUsuarioAutenticacaoAcessoList", EmitDefaultValue = true)]
	    public IEnumerable<TcsUsuarioAutenticacaoAcesso> TcsUsuarioAutenticacaoAcessoList
	    {
	        get
	        {
	
	            if (this._TcsUsuarioAutenticacaoAcessoList == null)
	            	this._TcsUsuarioAutenticacaoAcessoList = new List<TcsUsuarioAutenticacaoAcesso>();
	
	            return this._TcsUsuarioAutenticacaoAcessoList;
	        }
	        set
	        {
	            if (this._TcsUsuarioAutenticacaoAcessoList != value)
	            {
	                this._TcsUsuarioAutenticacaoAcessoList = value;
	                this.RaisePropertyChanged("TcsUsuarioAutenticacaoAcessoList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxPfjFisicaJuridicaValues()
	    {
	    	    return Linx.Framework.Setup.Domains.LX_PFJ_FISICA_JURIDICA.GetValues();
	    }
	    private string _lxPfjFisicaJuridicaName;
	    [DataMember(IsRequired = false, Name = "LxPfjFisicaJuridicaName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Pessoa Física / Juridíca", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxPfjFisicaJuridicaName
	    {
	    	    get { if (this.LxPfjFisicaJuridica.IsNull()) { _lxPfjFisicaJuridicaName = String.Empty; } else { string key = this.LxPfjFisicaJuridica.ToString(); var dmValues = this.GetLxPfjFisicaJuridicaValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxPfjFisicaJuridicaName) _lxPfjFisicaJuridicaName = domainName; } return _lxPfjFisicaJuridicaName; } set { _lxPfjFisicaJuridicaName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsUsuarioAcesso];ReadOnly[false];Entities[:IdTcsUsuarioAcesso];SubQueryInfo[Select 1 From #ParentAlias#. as #Alias#];EdmEntityName[];EntityRelations[ ];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioAutenticacaoAcesso")]
	[Serializable()]
	public partial class TcsUsuarioAutenticacaoAcesso : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(LinxAutoSetupDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsUsuarioAutenticacao");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdUsuario));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsUsuarioAutenticacao
	         this.TcsUsuarioAutenticacao = (from r in context.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For IdTcsAmbiente
	    partial void OnIdTcsAmbienteChanging(Int32 value);
	    partial void OnIdTcsAmbienteChanged();

	    private Int32 _IdTcsAmbiente;

	    [DataMember(IsRequired = true, Name = "IdTcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente];LookUpTitle[Seleção de (Id Tcs Ambiente)];LookUpQuery[executeLookUpTcsAmbiente];LookUpFinalize[finalizeLookUpTcsAmbiente];LookUpDisplayColumns[{\"IdTcsAmbiente\" : \"Id Tcs Ambiente\"}];LookUpColumns[{\"IdTcsAmbiente\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdTcsAmbiente#true##12:0##Id Tcs Ambiente#5#false##::LookUpTcsAmbiente##true#false#TCS_AMBIENTE#TCS_AMBIENTE#Linx.Framework.BV.UsuarioAutorizacao#IQueryable#DescricaoAplicacao,EmDesenvolvimento,UidAplicacao,Url[DescricaoAplicacao,EmDesenvolvimento,DescricaoAplicativo,UidAplicacao,Url,IdTcsAplicativo];DescricaoAplicativo,IdTcsAplicativo[DescricaoAplicativo,IdTcsAplicativo]#DescricaoAmbiente[DescricaoAplicacao=DescricaoAplicacao,EmDesenvolvimento=EmDesenvolvimento,IdLinxEmpresa=IdLinxEmpresa,DescricaoAplicativo=DescricaoAplicativo,NomeEmpresa=NomeEmpresa,UidAplicacao=UidAplicacao,UidEmpresa=UidEmpresa,Url=Url,IdTcsAplicativo=IdTcsAplicativo];DescricaoAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];EmDesenvolvimento[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdLinxEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];IdTcsAmbiente[DescricaoAplicacao=DescricaoAplicacao,EmDesenvolvimento=EmDesenvolvimento,IdLinxEmpresa=IdLinxEmpresa,DescricaoAplicativo=DescricaoAplicativo,NomeEmpresa=NomeEmpresa,UidAplicacao=UidAplicacao,UidEmpresa=UidEmpresa,Url=Url,IdTcsAplicativo=IdTcsAplicativo];NomeEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidAplicacao[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];UidEmpresa[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo];Url[DescricaoAplicativo=DescricaoAplicativo,IdTcsAplicativo=IdTcsAplicativo]#true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.ID_TCS_AMBIENTE")]
	    public Int32 IdTcsAmbiente
	    {
	    	    get
	    	    {
	    	          return _IdTcsAmbiente;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAmbiente != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAmbiente", value);
	    	              this.OnIdTcsAmbienteChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAmbiente");
	    	              this._IdTcsAmbiente = value;
	    	              this.RaiseDataMemberChanged("IdTcsAmbiente");
	    	              this.OnIdTcsAmbienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsAmbienteRelacionado
	    partial void OnIdTcsAmbienteRelacionadoChanging(System.Nullable<Int32> value);
	    partial void OnIdTcsAmbienteRelacionadoChanged();

	    private System.Nullable<Int32> _IdTcsAmbienteRelacionado;

	    [DataMember(Name = "IdTcsAmbienteRelacionado", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente1", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAmbiente1];LookUpTitle[Seleção de (Id Tcs Ambiente1)];LookUpQuery[executeLookUpTcsAmbiente1];LookUpFinalize[finalizeLookUpTcsAmbiente1];LookUpDisplayColumns[{\"IdTcsAmbienteRelacionado\" : \"Id Tcs Ambiente1\"}];LookUpColumns[{\"IdTcsAmbienteRelacionado\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE1.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Int32>#IdTcsAmbienteRelacionado#true##12:0##Id Tcs Ambiente1#4#false##::LookUpTcsAmbiente1##false#false#TCS_AMBIENTE1#TCS_AMBIENTE#Linx.Framework.BV.UsuarioAutorizacao#IQueryable#NomeEmpresaAmbienteRelacionado,IdLinxAmbienteRelacionado[NomeEmpresaAmbienteRelacionado,IdLinxAmbienteRelacionado]#DescricaoAmbienteRelacionado[NomeEmpresaAmbienteRelacionado=NomeEmpresaAmbienteRelacionado,DescricaoAplicacaoAmbienteRelacionado=DescricaoAplicacaoAmbienteRelacionado,IdLinxAmbienteRelacionado=IdLinxAmbienteRelacionado,IdAplicacao=IdAplicacao];IdTcsAmbienteRelacionado[NomeEmpresaAmbienteRelacionado=NomeEmpresaAmbienteRelacionado,DescricaoAplicacaoAmbienteRelacionado=DescricaoAplicacaoAmbienteRelacionado,IdLinxAmbienteRelacionado=IdLinxAmbienteRelacionado,IdAplicacao=IdAplicacao]#true#false", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE1.ID_TCS_AMBIENTE")]
	    public System.Nullable<Int32> IdTcsAmbienteRelacionado
	    {
	    	    get
	    	    {
	    	          return _IdTcsAmbienteRelacionado;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAmbienteRelacionado != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAmbienteRelacionado", value);
	    	              this.OnIdTcsAmbienteRelacionadoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAmbienteRelacionado");
	    	              this._IdTcsAmbienteRelacionado = value;
	    	              this.RaiseDataMemberChanged("IdTcsAmbienteRelacionado");
	    	              this.OnIdTcsAmbienteRelacionadoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsUsuarioAcesso
	    partial void OnIdTcsUsuarioAcessoChanging(Int32 value);
	    partial void OnIdTcsUsuarioAcessoChanged();

	    private Int32 _IdTcsUsuarioAcesso;

	    [DataMember(IsRequired = true, Name = "IdTcsUsuarioAcesso", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Acesso", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO")]
	    public Int32 IdTcsUsuarioAcesso
	    {
	    	    get
	    	    {
	    	          return _IdTcsUsuarioAcesso;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsUsuarioAcesso != value)
	    	          {
	    	              this.ValidateProperty("IdTcsUsuarioAcesso", value);
	    	              this.OnIdTcsUsuarioAcessoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsUsuarioAcesso");
	    	              this._IdTcsUsuarioAcesso = value;
	    	              this.RaiseDataMemberChanged("IdTcsUsuarioAcesso");
	    	              this.OnIdTcsUsuarioAcessoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(Int64 value);
	    partial void OnIdUsuarioChanged();

	    private Int64 _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.ID_USUARIO")]
	    public Int64 IdUsuario
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
	    //Extensibility Partial Method Definitions For IndicaAcessoPadrao
	    partial void OnIndicaAcessoPadraoChanging(Boolean value);
	    partial void OnIndicaAcessoPadraoChanged();

	    private Boolean _IndicaAcessoPadrao;

	    [DataMember(IsRequired = true, Name = "IndicaAcessoPadrao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Acesso Padrão", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.INDICA_ACESSO_PADRAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.INDICA_ACESSO_PADRAO")]
	    public Boolean IndicaAcessoPadrao
	    {
	    	    get
	    	    {
	    	          return _IndicaAcessoPadrao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaAcessoPadrao != value)
	    	          {
	    	              this.ValidateProperty("IndicaAcessoPadrao", value);
	    	              this.OnIndicaAcessoPadraoChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaAcessoPadrao");
	    	              this._IndicaAcessoPadrao = value;
	    	              this.RaiseDataMemberChanged("IndicaAcessoPadrao");
	    	              this.OnIndicaAcessoPadraoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IndicaAdministrador
	    partial void OnIndicaAdministradorChanging(Boolean value);
	    partial void OnIndicaAdministradorChanged();

	    private Boolean _IndicaAdministrador;

	    [DataMember(IsRequired = true, Name = "IndicaAdministrador", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Administrador", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR")]
	    public Boolean IndicaAdministrador
	    {
	    	    get
	    	    {
	    	          return _IndicaAdministrador;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaAdministrador != value)
	    	          {
	    	              this.ValidateProperty("IndicaAdministrador", value);
	    	              this.OnIndicaAdministradorChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaAdministrador");
	    	              this._IndicaAdministrador = value;
	    	              this.RaiseDataMemberChanged("IndicaAdministrador");
	    	              this.OnIndicaAdministradorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IndicaMultiGpecon
	    partial void OnIndicaMultiGpeconChanging(Boolean value);
	    partial void OnIndicaMultiGpeconChanged();

	    private Boolean _IndicaMultiGpecon;

	    [DataMember(IsRequired = true, Name = "IndicaMultiGpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Multi Grupo Econômico", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.INDICA_MULTI_GPECON];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.INDICA_MULTI_GPECON")]
	    public Boolean IndicaMultiGpecon
	    {
	    	    get
	    	    {
	    	          return _IndicaMultiGpecon;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaMultiGpecon != value)
	    	          {
	    	              this.ValidateProperty("IndicaMultiGpecon", value);
	    	              this.OnIndicaMultiGpeconChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaMultiGpecon");
	    	              this._IndicaMultiGpecon = value;
	    	              this.RaiseDataMemberChanged("IndicaMultiGpecon");
	    	              this.OnIndicaMultiGpeconChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdTcsUsuarioAcesso;
	    [DataMember(Name = "TemporaryIdTcsUsuarioAcesso", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Acesso (Tmp)", Description="Temporary Key", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdTcsUsuarioAcesso
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsUsuarioAcesso.IsNullOrEmpty())
	    	                this._TemporaryIdTcsUsuarioAcesso = this._IdTcsUsuarioAcesso;
	    	          return this._TemporaryIdTcsUsuarioAcesso;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsUsuarioAcesso != value)
	    	              this._TemporaryIdTcsUsuarioAcesso = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsUsuarioAutenticacao _TcsUsuarioAutenticacao;
	    [DataMember(Name = "TcsUsuarioAutenticacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsUsuarioAutenticacao_TcsUsuarioAutenticacaoAcesso", "IdUsuario", "IdUsuario", IsForeignKey=true)]
	    public TcsUsuarioAutenticacao TcsUsuarioAutenticacao
	    {
	        get
	        {
	            return this._TcsUsuarioAutenticacao;
	        }
	        set
	        {
	            if (this._TcsUsuarioAutenticacao != value)
	            {
	                this._TcsUsuarioAutenticacao = value;
	                this.RaisePropertyChanged("TcsUsuarioAutenticacaoList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		
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

		

	[LinxPublicationView(PrimaryKeys="TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsUsuarioPerfil];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsUsuarioPerfil];ReadOnly[false];Entities[:IdTcsUsuarioPerfil];SubQueryInfo[];EdmEntityName[];EntityRelations[ ];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsUsuarioPerfil")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.Setup.LinxAutoSetup.TcsUsuarioPerfil")]
	public partial class TcsUsuarioPerfil : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For IdPerfil
	    partial void OnIdPerfilChanging(Int64 value);
	    partial void OnIdPerfilChanged();

	    private Int64 _IdPerfil;

	    [DataMember(IsRequired = true, Name = "IdPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Perfil", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_PERFIL.TCS_PERFIL.ID_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_PERFIL.TCS_PERFIL.ID_PERFIL")]
	    public Int64 IdPerfil
	    {
	    	    get
	    	    {
	    	          return _IdPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdPerfil != value)
	    	          {
	    	              this.ValidateProperty("IdPerfil", value);
	    	              this.OnIdPerfilChanging(value);
	    	              this.RaiseDataMemberChanging("IdPerfil");
	    	              this._IdPerfil = value;
	    	              this.RaiseDataMemberChanged("IdPerfil");
	    	              this.OnIdPerfilChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsUsuarioPerfil
	    partial void OnIdTcsUsuarioPerfilChanging(Int64 value);
	    partial void OnIdTcsUsuarioPerfilChanged();

	    private Int64 _IdTcsUsuarioPerfil;

	    [DataMember(IsRequired = true, Name = "IdTcsUsuarioPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Perfil", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL")]
	    public Int64 IdTcsUsuarioPerfil
	    {
	    	    get
	    	    {
	    	          return _IdTcsUsuarioPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsUsuarioPerfil != value)
	    	          {
	    	              this.ValidateProperty("IdTcsUsuarioPerfil", value);
	    	              this.OnIdTcsUsuarioPerfilChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsUsuarioPerfil");
	    	              this._IdTcsUsuarioPerfil = value;
	    	              this.RaiseDataMemberChanged("IdTcsUsuarioPerfil");
	    	              this.OnIdTcsUsuarioPerfilChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(Int64 value);
	    partial void OnIdUsuarioChanged();

	    private Int64 _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_PERFIL.TCS_USUARIO.ID_USUARIO")]
	    public Int64 IdUsuario
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

	    private Int64 _TemporaryIdTcsUsuarioPerfil;
	    [DataMember(Name = "TemporaryIdTcsUsuarioPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Perfil (Tmp)", Description="Temporary Key", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdTcsUsuarioPerfil
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsUsuarioPerfil.IsNullOrEmpty())
	    	                this._TemporaryIdTcsUsuarioPerfil = this._IdTcsUsuarioPerfil;
	    	          return this._TemporaryIdTcsUsuarioPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsUsuarioPerfil != value)
	    	              this._TemporaryIdTcsUsuarioPerfil = value;
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
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_AMBIENTE.ID_TCS_AMBIENTE", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsAmbiente,TcsAmbiente.TcsAmbienteConexao,TcsAmbiente.TcsAmbienteUsuarioAcesso];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsAmbiente];ReadOnly[false];Entities[TCS_AMBIENTE:IdTcsAmbiente];SubQueryInfo[];EdmEntityName[TCS_AMBIENTE];EntityRelations[ ];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsAmbiente")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.Setup.LinxAutoSetup.TcsAmbiente")]
	public partial class TcsAmbiente : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.TcsAmbienteConexaoList != null && this.TcsAmbienteConexaoList.Count() > 0)
	      {
	         foreach (var entity in this.TcsAmbienteConexaoList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      if (this.TcsAmbienteUsuarioAcessoList != null && this.TcsAmbienteUsuarioAcessoList.Count() > 0)
	      {
	         foreach (var entity in this.TcsAmbienteUsuarioAcessoList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.TcsAmbienteConexaoList != null)
	      {
	         foreach (var detail in this.TcsAmbienteConexaoList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsAmbienteConexaoList = null;
	      }
	      if (this.TcsAmbienteUsuarioAcessoList != null)
	      {
	         foreach (var detail in this.TcsAmbienteUsuarioAcessoList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsAmbienteUsuarioAcessoList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(LinxAutoSetupDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsAmbienteConexao"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsAmbienteConexao");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsAmbiente"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdTcsAmbiente));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsAmbienteConexao and all sub-details
	         if (this.TcsAmbienteConexaoList == null || this.TcsAmbienteConexaoList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsAmbienteConexaoList = context.GetPagedTcsAmbienteConexao(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsAmbienteConexaoList = (from r in context.GetTcsAmbienteConexaoByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	      if (viewNames == null || viewNames.Contains("TcsAmbienteUsuarioAcesso"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsAmbienteUsuarioAcesso");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsAmbiente"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdTcsAmbiente));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsAmbienteUsuarioAcesso and all sub-details
	         if (this.TcsAmbienteUsuarioAcessoList == null || this.TcsAmbienteUsuarioAcessoList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsAmbienteUsuarioAcessoList = context.GetPagedTcsAmbienteUsuarioAcesso(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsAmbienteUsuarioAcessoList = (from r in context.GetTcsAmbienteUsuarioAcessoByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsAmbienteConexaoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsAmbienteConexao && ((TcsAmbienteConexao)e.Entity).TcsAmbiente == null && e.Associations == null && e.OriginalAssociations == null && ((TcsAmbienteConexao)e.Entity).IdTcsAmbiente == this.IdTcsAmbiente).ToList();
 	      if (_TcsAmbienteConexaoElements.Count > 0 && this.TcsAmbienteConexaoList.Count() == 0)
 	      {
 	          this.TcsAmbienteConexaoList = _TcsAmbienteConexaoElements.Select(e => (TcsAmbienteConexao)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsAmbienteConexaoElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsAmbienteConexao)detail.Entity).TcsAmbiente = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsAmbiente", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsAmbienteConexaoList", indexDetails.ToArray());
 	      }
 
 	      var _TcsAmbienteUsuarioAcessoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsAmbienteUsuarioAcesso && ((TcsAmbienteUsuarioAcesso)e.Entity).TcsAmbiente == null && e.Associations == null && e.OriginalAssociations == null && ((TcsAmbienteUsuarioAcesso)e.Entity).IdTcsAmbiente == this.IdTcsAmbiente).ToList();
 	      if (_TcsAmbienteUsuarioAcessoElements.Count > 0 && this.TcsAmbienteUsuarioAcessoList.Count() == 0)
 	      {
 	          this.TcsAmbienteUsuarioAcessoList = _TcsAmbienteUsuarioAcessoElements.Select(e => (TcsAmbienteUsuarioAcesso)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsAmbienteUsuarioAcessoElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsAmbienteUsuarioAcesso)detail.Entity).TcsAmbiente = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsAmbiente", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsAmbienteUsuarioAcessoList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DescricaoAmbiente
	    partial void OnDescricaoAmbienteChanging(String value);
	    partial void OnDescricaoAmbienteChanged();

	    private String _DescricaoAmbiente;

	    [DataMember(IsRequired = true, Name = "DescricaoAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_AMBIENTE.DESCRICAO_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.DESCRICAO_AMBIENTE")]
	    public String DescricaoAmbiente
	    {
	    	    get
	    	    {
	    	          return _DescricaoAmbiente;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoAmbiente != value)
	    	          {
	    	              this.ValidateProperty("DescricaoAmbiente", value);
	    	              this.OnDescricaoAmbienteChanging(value);
	    	              this.RaiseDataMemberChanging("DescricaoAmbiente");
	    	              this._DescricaoAmbiente = value;
	    	              this.RaiseDataMemberChanged("DescricaoAmbiente");
	    	              this.OnDescricaoAmbienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdAplicacao
	    partial void OnIdAplicacaoChanging(Int32 value);
	    partial void OnIdAplicacaoChanged();

	    private Int32 _IdAplicacao;

	    [DataMember(IsRequired = true, Name = "IdAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Aplicacao", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicacao];LookUpTitle[Seleção de (Id Aplicacao)];LookUpQuery[executeLookUpTcsAplicacao];LookUpFinalize[finalizeLookUpTcsAplicacao];LookUpDisplayColumns[{\"IdAplicacao\" : \"Id Aplicacao\"}];LookUpColumns[{\"IdAplicacao\" : false}];FilterDataKey[TCS_AMBIENTE.TCS_APLICACAO.ID_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdAplicacao#true##12:0##Id Aplicacao#4#false##::LookUpTcsAplicacao##false#false#TCS_APLICACAO#TCS_APLICACAO#Linx.Framework.BV.Ambiente#IQueryable#DescricaoAplicativo", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.ID_APLICACAO")]
	    public Int32 IdAplicacao
	    {
	    	    get
	    	    {
	    	          return _IdAplicacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdAplicacao != value)
	    	          {
	    	              this.ValidateProperty("IdAplicacao", value);
	    	              this.OnIdAplicacaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdAplicacao");
	    	              this._IdAplicacao = value;
	    	              this.RaiseDataMemberChanged("IdAplicacao");
	    	              this.OnIdAplicacaoChanged();
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
	    [Display(Name = "ID Linx Ambiente", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsEmpresaAutenticacao];LookUpTitle[Seleção de (ID Linx Ambiente)];LookUpQuery[executeLookUpTcsEmpresaAutenticacao];LookUpFinalize[finalizeLookUpTcsEmpresaAutenticacao];LookUpDisplayColumns[{\"IdLinx\" : \"ID Linx\", \"UidEmpresa\" : \"Uid Empresa\"}];LookUpColumns[{\"IdLinx\" : true, \"UidEmpresa\" : false}];FilterDataKey[TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdLinx#true##12:0##ID Linx#0#true##::LookUpTcsEmpresaAutenticacao##false#false#TCS_EMPRESA_AUTENTICACAO#TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
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
	    //Extensibility Partial Method Definitions For IdTcsAmbiente
	    partial void OnIdTcsAmbienteChanging(Int32 value);
	    partial void OnIdTcsAmbienteChanged();

	    private Int32 _IdTcsAmbiente;

	    [DataMember(IsRequired = true, Name = "IdTcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_AMBIENTE.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.ID_TCS_AMBIENTE")]
	    public Int32 IdTcsAmbiente
	    {
	    	    get
	    	    {
	    	          return _IdTcsAmbiente;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAmbiente != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAmbiente", value);
	    	              this.OnIdTcsAmbienteChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAmbiente");
	    	              this._IdTcsAmbiente = value;
	    	              this.RaiseDataMemberChanged("IdTcsAmbiente");
	    	              this.OnIdTcsAmbienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidEmpresa
	    partial void OnUidEmpresaChanging(Guid value);
	    partial void OnUidEmpresaChanged();

	    private Guid _UidEmpresa;

	    [DataMember(IsRequired = true, Name = "UidEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Empresa", Description="", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsEmpresaAutenticacao];LookUpTitle[Seleção de (Uid Empresa)];LookUpQuery[executeLookUpTcsEmpresaAutenticacao];LookUpFinalize[finalizeLookUpTcsEmpresaAutenticacao];LookUpDisplayColumns[{\"IdLinx\" : \"ID Linx\", \"UidEmpresa\" : \"Uid Empresa\"}];LookUpColumns[{\"IdLinx\" : true, \"UidEmpresa\" : false}];FilterDataKey[TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Guid#UidEmpresa#false##36:0##Uid Empresa#2#false##::LookUpTcsEmpresaAutenticacao##false#false#TCS_EMPRESA_AUTENTICACAO#TCS_EMPRESA_AUTENTICACAO#Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA")]
	    public Guid UidEmpresa
	    {
	    	    get
	    	    {
	    	          return _UidEmpresa;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidEmpresa != value)
	    	          {
	    	              this.ValidateProperty("UidEmpresa", value);
	    	              this.OnUidEmpresaChanging(value);
	    	              this.RaiseDataMemberChanging("UidEmpresa");
	    	              this._UidEmpresa = value;
	    	              this.RaiseDataMemberChanged("UidEmpresa");
	    	              this.OnUidEmpresaChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdTcsAmbiente;
	    [DataMember(Name = "TemporaryIdTcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente (Tmp)", Description="Temporary Key", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdTcsAmbiente
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsAmbiente.IsNullOrEmpty())
	    	                this._TemporaryIdTcsAmbiente = this._IdTcsAmbiente;
	    	          return this._TemporaryIdTcsAmbiente;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsAmbiente != value)
	    	              this._TemporaryIdTcsAmbiente = value;
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<TcsAmbienteConexao> _TcsAmbienteConexaoList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsAmbiente_TcsAmbienteConexao", "IdTcsAmbiente", "IdTcsAmbiente", IsForeignKey=false)]
	    [DataMember(Name = "TcsAmbienteConexaoList", EmitDefaultValue = true)]
	    public IEnumerable<TcsAmbienteConexao> TcsAmbienteConexaoList
	    {
	        get
	        {
	
	            if (this._TcsAmbienteConexaoList == null)
	            	this._TcsAmbienteConexaoList = new List<TcsAmbienteConexao>();
	
	            return this._TcsAmbienteConexaoList;
	        }
	        set
	        {
	            if (this._TcsAmbienteConexaoList != value)
	            {
	                this._TcsAmbienteConexaoList = value;
	                this.RaisePropertyChanged("TcsAmbienteConexaoList");
	            }
	        }
	    }	 
		
	    private IEnumerable<TcsAmbienteUsuarioAcesso> _TcsAmbienteUsuarioAcessoList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsAmbiente_TcsAmbienteUsuarioAcesso", "IdTcsAmbiente", "IdTcsAmbiente", IsForeignKey=false)]
	    [DataMember(Name = "TcsAmbienteUsuarioAcessoList", EmitDefaultValue = true)]
	    public IEnumerable<TcsAmbienteUsuarioAcesso> TcsAmbienteUsuarioAcessoList
	    {
	        get
	        {
	
	            if (this._TcsAmbienteUsuarioAcessoList == null)
	            	this._TcsAmbienteUsuarioAcessoList = new List<TcsAmbienteUsuarioAcesso>();
	
	            return this._TcsAmbienteUsuarioAcessoList;
	        }
	        set
	        {
	            if (this._TcsAmbienteUsuarioAcessoList != value)
	            {
	                this._TcsAmbienteUsuarioAcessoList = value;
	                this.RaisePropertyChanged("TcsAmbienteUsuarioAcessoList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		
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

		

	[LinxPublicationView(PrimaryKeys="TCS_AMBIENTE_CONEXAO.ID_TCS_AMBIENTE_CONEXAO", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsAmbienteConexao];ReadOnly[false];Entities[:IdTcsAmbienteConexao];SubQueryInfo[Select 1 From #ParentAlias#. as #Alias#];EdmEntityName[];EntityRelations[ ];EdmParentEntityName[TCS_AMBIENTE];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsAmbienteConexao")]
	[Serializable()]
	public partial class TcsAmbienteConexao : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(LinxAutoSetupDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsAmbiente");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsAmbiente"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdTcsAmbiente));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsAmbiente
	         this.TcsAmbiente = (from r in context.GetTcsAmbienteByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For IdTcsAmbiente
	    partial void OnIdTcsAmbienteChanging(Int32 value);
	    partial void OnIdTcsAmbienteChanged();

	    private Int32 _IdTcsAmbiente;

	    [DataMember(IsRequired = true, Name = "IdTcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_AMBIENTE.ID_TCS_AMBIENTE")]
	    public Int32 IdTcsAmbiente
	    {
	    	    get
	    	    {
	    	          return _IdTcsAmbiente;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAmbiente != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAmbiente", value);
	    	              this.OnIdTcsAmbienteChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAmbiente");
	    	              this._IdTcsAmbiente = value;
	    	              this.RaiseDataMemberChanged("IdTcsAmbiente");
	    	              this.OnIdTcsAmbienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsAmbienteConexao
	    partial void OnIdTcsAmbienteConexaoChanging(Int32 value);
	    partial void OnIdTcsAmbienteConexaoChanged();

	    private Int32 _IdTcsAmbienteConexao;

	    [DataMember(IsRequired = true, Name = "IdTcsAmbienteConexao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente Conexao", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_AMBIENTE_CONEXAO.ID_TCS_AMBIENTE_CONEXAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE_CONEXAO.ID_TCS_AMBIENTE_CONEXAO")]
	    public Int32 IdTcsAmbienteConexao
	    {
	    	    get
	    	    {
	    	          return _IdTcsAmbienteConexao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAmbienteConexao != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAmbienteConexao", value);
	    	              this.OnIdTcsAmbienteConexaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAmbienteConexao");
	    	              this._IdTcsAmbienteConexao = value;
	    	              this.RaiseDataMemberChanged("IdTcsAmbienteConexao");
	    	              this.OnIdTcsAmbienteConexaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsAplicativoConexao
	    partial void OnIdTcsAplicativoConexaoChanging(Int32 value);
	    partial void OnIdTcsAplicativoConexaoChanged();

	    private Int32 _IdTcsAplicativoConexao;

	    [DataMember(IsRequired = true, Name = "IdTcsAplicativoConexao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Aplicativo Conexao", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicativoConexao];LookUpTitle[Seleção de (Id Tcs Aplicativo Conexao)];LookUpQuery[executeLookUpTcsAplicativoConexao];LookUpFinalize[finalizeLookUpTcsAplicativoConexao];LookUpDisplayColumns[{\"IdTcsAplicativoConexao\" : \"Id Tcs Aplicativo Conexao\"}];LookUpColumns[{\"IdTcsAplicativoConexao\" : false}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_APLICATIVO_CONEXAO.ID_TCS_APLICATIVO_CONEXAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdTcsAplicativoConexao#true##12:0##Id Tcs Aplicativo Conexao#0#false##::LookUpTcsAplicativoConexao##true#false#TCS_APLICATIVO_CONEXAO#TCS_APLICATIVO_CONEXAO#Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_APLICATIVO_CONEXAO.ID_TCS_APLICATIVO_CONEXAO")]
	    public Int32 IdTcsAplicativoConexao
	    {
	    	    get
	    	    {
	    	          return _IdTcsAplicativoConexao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAplicativoConexao != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAplicativoConexao", value);
	    	              this.OnIdTcsAplicativoConexaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAplicativoConexao");
	    	              this._IdTcsAplicativoConexao = value;
	    	              this.RaiseDataMemberChanged("IdTcsAplicativoConexao");
	    	              this.OnIdTcsAplicativoConexaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsBancoServidor
	    partial void OnIdTcsBancoServidorChanging(Int32 value);
	    partial void OnIdTcsBancoServidorChanged();

	    private Int32 _IdTcsBancoServidor;

	    [DataMember(IsRequired = true, Name = "IdTcsBancoServidor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Banco Servidor", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsBancoServidor];LookUpTitle[Seleção de (Id Tcs Banco Servidor)];LookUpQuery[executeLookUpTcsBancoServidor];LookUpFinalize[finalizeLookUpTcsBancoServidor];LookUpDisplayColumns[{\"IdTcsBancoServidor\" : \"Id Tcs Banco Servidor\"}];LookUpColumns[{\"IdTcsBancoServidor\" : false}];FilterDataKey[TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.ID_TCS_BANCO_SERVIDOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdTcsBancoServidor#true##12:0##Id Tcs Banco Servidor#0#false##::LookUpTcsBancoServidor##false#false#TCS_BANCO_SERVIDOR#TCS_BANCO_SERVIDOR#Linx.Framework.BV.Ambiente#IQueryable###true#false", EdmKey="TCS_AMBIENTE_CONEXAO.TCS_BANCO_SERVIDOR.ID_TCS_BANCO_SERVIDOR")]
	    public Int32 IdTcsBancoServidor
	    {
	    	    get
	    	    {
	    	          return _IdTcsBancoServidor;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsBancoServidor != value)
	    	          {
	    	              this.ValidateProperty("IdTcsBancoServidor", value);
	    	              this.OnIdTcsBancoServidorChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsBancoServidor");
	    	              this._IdTcsBancoServidor = value;
	    	              this.RaiseDataMemberChanged("IdTcsBancoServidor");
	    	              this.OnIdTcsBancoServidorChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdTcsAmbienteConexao;
	    [DataMember(Name = "TemporaryIdTcsAmbienteConexao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente Conexao (Tmp)", Description="Temporary Key", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdTcsAmbienteConexao
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsAmbienteConexao.IsNullOrEmpty())
	    	                this._TemporaryIdTcsAmbienteConexao = this._IdTcsAmbienteConexao;
	    	          return this._TemporaryIdTcsAmbienteConexao;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsAmbienteConexao != value)
	    	              this._TemporaryIdTcsAmbienteConexao = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsAmbiente _TcsAmbiente;
	    [DataMember(Name = "TcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsAmbiente_TcsAmbienteConexao", "IdTcsAmbiente", "IdTcsAmbiente", IsForeignKey=true)]
	    public TcsAmbiente TcsAmbiente
	    {
	        get
	        {
	            return this._TcsAmbiente;
	        }
	        set
	        {
	            if (this._TcsAmbiente != value)
	            {
	                this._TcsAmbiente = value;
	                this.RaisePropertyChanged("TcsAmbienteList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		
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

		

	[LinxPublicationView(PrimaryKeys="TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsUsuarioAcesso];ReadOnly[false];Entities[:IdTcsUsuarioAcesso];SubQueryInfo[Select 1 From #ParentAlias#. as #Alias#];EdmEntityName[];EntityRelations[ ];EdmParentEntityName[TCS_AMBIENTE];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsAmbienteUsuarioAcesso")]
	[Serializable()]
	public partial class TcsAmbienteUsuarioAcesso : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(LinxAutoSetupDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsAmbiente");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsAmbiente"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdTcsAmbiente));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsAmbiente
	         this.TcsAmbiente = (from r in context.GetTcsAmbienteByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For IdTcsAmbiente
	    partial void OnIdTcsAmbienteChanging(Int32 value);
	    partial void OnIdTcsAmbienteChanged();

	    private Int32 _IdTcsAmbiente;

	    [DataMember(IsRequired = true, Name = "IdTcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ambiente", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.TCS_AMBIENTE.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.TCS_AMBIENTE.ID_TCS_AMBIENTE")]
	    public Int32 IdTcsAmbiente
	    {
	    	    get
	    	    {
	    	          return _IdTcsAmbiente;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAmbiente != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAmbiente", value);
	    	              this.OnIdTcsAmbienteChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAmbiente");
	    	              this._IdTcsAmbiente = value;
	    	              this.RaiseDataMemberChanged("IdTcsAmbiente");
	    	              this.OnIdTcsAmbienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsUsuarioAcesso
	    partial void OnIdTcsUsuarioAcessoChanging(Int32 value);
	    partial void OnIdTcsUsuarioAcessoChanged();

	    private Int32 _IdTcsUsuarioAcesso;

	    [DataMember(IsRequired = true, Name = "IdTcsUsuarioAcesso", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Acesso", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO")]
	    public Int32 IdTcsUsuarioAcesso
	    {
	    	    get
	    	    {
	    	          return _IdTcsUsuarioAcesso;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsUsuarioAcesso != value)
	    	          {
	    	              this.ValidateProperty("IdTcsUsuarioAcesso", value);
	    	              this.OnIdTcsUsuarioAcessoChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsUsuarioAcesso");
	    	              this._IdTcsUsuarioAcesso = value;
	    	              this.RaiseDataMemberChanged("IdTcsUsuarioAcesso");
	    	              this.OnIdTcsUsuarioAcessoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(Int64 value);
	    partial void OnIdUsuarioChanged();

	    private Int64 _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacao];LookUpTitle[Seleção de (Id Usuario)];LookUpQuery[executeLookUpTcsUsuarioAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacao];LookUpDisplayColumns[{\"NomeUsuario\" : \"Nome\", \"IdUsuario\" : \"Id Usuario\", \"UidUsuario\" : \"Uid Usuario\", \"NomeAutenticacao\" : \"Nome Autenticacao\"}];LookUpColumns[{\"NomeUsuario\" : true, \"IdUsuario\" : false, \"UidUsuario\" : false, \"NomeAutenticacao\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdUsuario#true##24:0##Id Usuario#3#false##::LookUpTcsUsuarioAutenticacao##true#false#TCS_USUARIO_AUTENTICACAO#TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Ambiente#IQueryable###true#true", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.ID_USUARIO")]
	    public Int64 IdUsuario
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
	    //Extensibility Partial Method Definitions For IndicaAdministrador
	    partial void OnIndicaAdministradorChanging(Boolean value);
	    partial void OnIndicaAdministradorChanged();

	    private Boolean _IndicaAdministrador;

	    [DataMember(IsRequired = true, Name = "IndicaAdministrador", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Administrador", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR")]
	    public Boolean IndicaAdministrador
	    {
	    	    get
	    	    {
	    	          return _IndicaAdministrador;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaAdministrador != value)
	    	          {
	    	              this.ValidateProperty("IndicaAdministrador", value);
	    	              this.OnIndicaAdministradorChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaAdministrador");
	    	              this._IndicaAdministrador = value;
	    	              this.RaiseDataMemberChanged("IndicaAdministrador");
	    	              this.OnIndicaAdministradorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IndicaMultiGpecon
	    partial void OnIndicaMultiGpeconChanging(Boolean value);
	    partial void OnIndicaMultiGpeconChanged();

	    private Boolean _IndicaMultiGpecon;

	    [DataMember(IsRequired = true, Name = "IndicaMultiGpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Multi Grupo Econômico", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_ACESSO.INDICA_MULTI_GPECON];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_ACESSO.INDICA_MULTI_GPECON")]
	    public Boolean IndicaMultiGpecon
	    {
	    	    get
	    	    {
	    	          return _IndicaMultiGpecon;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaMultiGpecon != value)
	    	          {
	    	              this.ValidateProperty("IndicaMultiGpecon", value);
	    	              this.OnIndicaMultiGpeconChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaMultiGpecon");
	    	              this._IndicaMultiGpecon = value;
	    	              this.RaiseDataMemberChanged("IndicaMultiGpecon");
	    	              this.OnIndicaMultiGpeconChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeAutenticacao
	    partial void OnNomeAutenticacaoChanging(String value);
	    partial void OnNomeAutenticacaoChanged();

	    private String _NomeAutenticacao;

	    [DataMember(IsRequired = true, Name = "NomeAutenticacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário Autenticação", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacao];LookUpTitle[Seleção de (Usuário Autenticação)];LookUpQuery[executeLookUpTcsUsuarioAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacao];LookUpDisplayColumns[{\"NomeUsuario\" : \"Nome\", \"IdUsuario\" : \"Id Usuario\", \"UidUsuario\" : \"Uid Usuario\", \"NomeAutenticacao\" : \"Nome Autenticacao\"}];LookUpColumns[{\"NomeUsuario\" : true, \"IdUsuario\" : false, \"UidUsuario\" : false, \"NomeAutenticacao\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#NomeAutenticacao#false##2500##Nome Autenticacao#5#false##::LookUpTcsUsuarioAutenticacao##true#false#TCS_USUARIO_AUTENTICACAO#TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Ambiente#IQueryable###true#true", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_AUTENTICACAO")]
	    public String NomeAutenticacao
	    {
	    	    get
	    	    {
	    	          return _NomeAutenticacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeAutenticacao != value)
	    	          {
	    	              this.ValidateProperty("NomeAutenticacao", value);
	    	              this.OnNomeAutenticacaoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeAutenticacao");
	    	              this._NomeAutenticacao = value;
	    	              this.RaiseDataMemberChanged("NomeAutenticacao");
	    	              this.OnNomeAutenticacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeUsuario
	    partial void OnNomeUsuarioChanging(String value);
	    partial void OnNomeUsuarioChanged();

	    private String _NomeUsuario;

	    [DataMember(IsRequired = true, Name = "NomeUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Usuário", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacao];LookUpTitle[Seleção de (Usuário)];LookUpQuery[executeLookUpTcsUsuarioAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacao];LookUpDisplayColumns[{\"NomeUsuario\" : \"Nome\", \"IdUsuario\" : \"Id Usuario\", \"UidUsuario\" : \"Uid Usuario\", \"NomeAutenticacao\" : \"Nome Autenticacao\"}];LookUpColumns[{\"NomeUsuario\" : true, \"IdUsuario\" : false, \"UidUsuario\" : false, \"NomeAutenticacao\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="String#NomeUsuario#false##250:0##Nome#0#true##::LookUpTcsUsuarioAutenticacao##true#false#TCS_USUARIO_AUTENTICACAO#TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Ambiente#IQueryable###true#true", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.NOME_USUARIO")]
	    public String NomeUsuario
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
	    //Extensibility Partial Method Definitions For UidUsuario
	    partial void OnUidUsuarioChanging(Guid value);
	    partial void OnUidUsuarioChanged();

	    private Guid _UidUsuario;

	    [DataMember(IsRequired = true, Name = "UidUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Usuario", Description="", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuarioAutenticacao];LookUpTitle[Seleção de (Uid Usuario)];LookUpQuery[executeLookUpTcsUsuarioAutenticacao];LookUpFinalize[finalizeLookUpTcsUsuarioAutenticacao];LookUpDisplayColumns[{\"NomeUsuario\" : \"Nome\", \"IdUsuario\" : \"Id Usuario\", \"UidUsuario\" : \"Uid Usuario\", \"NomeAutenticacao\" : \"Nome Autenticacao\"}];LookUpColumns[{\"NomeUsuario\" : true, \"IdUsuario\" : false, \"UidUsuario\" : false, \"NomeAutenticacao\" : false}];FilterDataKey[TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.UID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Guid#UidUsuario#false##12:0##Uid Usuario#4#false##::LookUpTcsUsuarioAutenticacao##true#false#TCS_USUARIO_AUTENTICACAO#TCS_USUARIO_AUTENTICACAO#Linx.Framework.BV.Ambiente#IQueryable###true#true", EdmKey="TCS_USUARIO_ACESSO.TCS_USUARIO_AUTENTICACAO.UID_USUARIO")]
	    public Guid UidUsuario
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

	    private Int32 _TemporaryIdTcsUsuarioAcesso;
	    [DataMember(Name = "TemporaryIdTcsUsuarioAcesso", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Acesso (Tmp)", Description="Temporary Key", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdTcsUsuarioAcesso
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsUsuarioAcesso.IsNullOrEmpty())
	    	                this._TemporaryIdTcsUsuarioAcesso = this._IdTcsUsuarioAcesso;
	    	          return this._TemporaryIdTcsUsuarioAcesso;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsUsuarioAcesso != value)
	    	              this._TemporaryIdTcsUsuarioAcesso = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsAmbiente _TcsAmbiente;
	    [DataMember(Name = "TcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsAmbiente_TcsAmbienteUsuarioAcesso", "IdTcsAmbiente", "IdTcsAmbiente", IsForeignKey=true)]
	    public TcsAmbiente TcsAmbiente
	    {
	        get
	        {
	            return this._TcsAmbiente;
	        }
	        set
	        {
	            if (this._TcsAmbiente != value)
	            {
	                this._TcsAmbiente = value;
	                this.RaisePropertyChanged("TcsAmbienteList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		
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

		

	[LinxPublicationView(PrimaryKeys="TCS_MODULO_GRUPO.ID_GRUPO_MODULO", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsModuloGrupo,TcsModuloGrupo.TcsModuloGrupoDetalhe];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdGrupoModulo];ReadOnly[false];Entities[:IdGrupoModulo];SubQueryInfo[];EdmEntityName[];EntityRelations[ ];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsModuloGrupo")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.Setup.LinxAutoSetup.TcsModuloGrupo")]
	public partial class TcsModuloGrupo : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.TcsModuloGrupoDetalheList != null && this.TcsModuloGrupoDetalheList.Count() > 0)
	      {
	         foreach (var entity in this.TcsModuloGrupoDetalheList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.TcsModuloGrupoDetalheList != null)
	      {
	         foreach (var detail in this.TcsModuloGrupoDetalheList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsModuloGrupoDetalheList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(LinxAutoSetupDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsModuloGrupoDetalhe"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsModuloGrupoDetalhe");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdGrupoModulo"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdGrupoModulo));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsModuloGrupoDetalhe and all sub-details
	         if (this.TcsModuloGrupoDetalheList == null || this.TcsModuloGrupoDetalheList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsModuloGrupoDetalheList = context.GetPagedTcsModuloGrupoDetalhe(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsModuloGrupoDetalheList = (from r in context.GetTcsModuloGrupoDetalheByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsModuloGrupoDetalheElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsModuloGrupoDetalhe && ((TcsModuloGrupoDetalhe)e.Entity).TcsModuloGrupo == null && e.Associations == null && e.OriginalAssociations == null && ((TcsModuloGrupoDetalhe)e.Entity).IdGrupoModulo == this.IdGrupoModulo).ToList();
 	      if (_TcsModuloGrupoDetalheElements.Count > 0 && this.TcsModuloGrupoDetalheList.Count() == 0)
 	      {
 	          this.TcsModuloGrupoDetalheList = _TcsModuloGrupoDetalheElements.Select(e => (TcsModuloGrupoDetalhe)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsModuloGrupoDetalheElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsModuloGrupoDetalhe)detail.Entity).TcsModuloGrupo = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsModuloGrupo", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsModuloGrupoDetalheList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DescGrupoModulo
	    partial void OnDescGrupoModuloChanging(String value);
	    partial void OnDescGrupoModuloChanged();

	    private String _DescGrupoModulo;

	    [DataMember(IsRequired = true, Name = "DescGrupoModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_GRUPO.DESC_GRUPO_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_GRUPO.DESC_GRUPO_MODULO")]
	    public String DescGrupoModulo
	    {
	    	    get
	    	    {
	    	          return _DescGrupoModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescGrupoModulo != value)
	    	          {
	    	              this.ValidateProperty("DescGrupoModulo", value);
	    	              this.OnDescGrupoModuloChanging(value);
	    	              this.RaiseDataMemberChanging("DescGrupoModulo");
	    	              this._DescGrupoModulo = value;
	    	              this.RaiseDataMemberChanged("DescGrupoModulo");
	    	              this.OnDescGrupoModuloChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdGrupoModulo
	    partial void OnIdGrupoModuloChanging(Int64 value);
	    partial void OnIdGrupoModuloChanged();

	    private Int64 _IdGrupoModulo;

	    [DataMember(IsRequired = true, Name = "IdGrupoModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_GRUPO.ID_GRUPO_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_GRUPO.ID_GRUPO_MODULO")]
	    public Int64 IdGrupoModulo
	    {
	    	    get
	    	    {
	    	          return _IdGrupoModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdGrupoModulo != value)
	    	          {
	    	              this.ValidateProperty("IdGrupoModulo", value);
	    	              this.OnIdGrupoModuloChanging(value);
	    	              this.RaiseDataMemberChanging("IdGrupoModulo");
	    	              this._IdGrupoModulo = value;
	    	              this.RaiseDataMemberChanged("IdGrupoModulo");
	    	              this.OnIdGrupoModuloChanged();
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
	    [Display(Name = "ID Aplicativo", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicativo];LookUpTitle[Seleção de (ID Aplicativo)];LookUpQuery[executeLookUpTcsAplicativo];LookUpFinalize[finalizeLookUpTcsAplicativo];LookUpDisplayColumns[{\"IdTcsAplicativo\" : \"\"}];LookUpColumns[{\"IdTcsAplicativo\" : false}];FilterDataKey[TCS_MODULO_GRUPO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="int#IdTcsAplicativo#false##12:0###0#false##::LookUpTcsAplicativo##false#false###Linx.Framework.BV.Modulo#IQueryable###true#false", EdmKey="TCS_MODULO_GRUPO.ID_TCS_APLICATIVO")]
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

	    private Int64 _TemporaryIdGrupoModulo;
	    [DataMember(Name = "TemporaryIdGrupoModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id (Tmp)", Description="Temporary Key", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdGrupoModulo
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdGrupoModulo.IsNullOrEmpty())
	    	                this._TemporaryIdGrupoModulo = this._IdGrupoModulo;
	    	          return this._TemporaryIdGrupoModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdGrupoModulo != value)
	    	              this._TemporaryIdGrupoModulo = value;
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<TcsModuloGrupoDetalhe> _TcsModuloGrupoDetalheList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsModuloGrupo_TcsModuloGrupoDetalhe", "IdGrupoModulo", "IdGrupoModulo", IsForeignKey=false)]
	    [DataMember(Name = "TcsModuloGrupoDetalheList", EmitDefaultValue = true)]
	    public IEnumerable<TcsModuloGrupoDetalhe> TcsModuloGrupoDetalheList
	    {
	        get
	        {
	
	            if (this._TcsModuloGrupoDetalheList == null)
	            	this._TcsModuloGrupoDetalheList = new List<TcsModuloGrupoDetalhe>();
	
	            return this._TcsModuloGrupoDetalheList;
	        }
	        set
	        {
	            if (this._TcsModuloGrupoDetalheList != value)
	            {
	                this._TcsModuloGrupoDetalheList = value;
	                this.RaisePropertyChanged("TcsModuloGrupoDetalheList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		
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

		

	[LinxPublicationView(PrimaryKeys="TCS_MODULO_DO_GRUPO.ID_MODULO_DO_GRUPO", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdModuloDoGrupo];ReadOnly[false];Entities[:IdModuloDoGrupo];SubQueryInfo[Select 1 From #ParentAlias#. as #Alias#];EdmEntityName[];EntityRelations[ ];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsModuloGrupoDetalhe")]
	[Serializable()]
	public partial class TcsModuloGrupoDetalhe : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(LinxAutoSetupDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsModuloGrupo");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdGrupoModulo"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdGrupoModulo));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsModuloGrupo
	         this.TcsModuloGrupo = (from r in context.GetTcsModuloGrupoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For IdGrupoModulo
	    partial void OnIdGrupoModuloChanging(Int64 value);
	    partial void OnIdGrupoModuloChanged();

	    private Int64 _IdGrupoModulo;

	    [DataMember(IsRequired = true, Name = "IdGrupoModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id", Description="", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_DO_GRUPO.TCS_MODULO_GRUPO.ID_GRUPO_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_DO_GRUPO.TCS_MODULO_GRUPO.ID_GRUPO_MODULO")]
	    public Int64 IdGrupoModulo
	    {
	    	    get
	    	    {
	    	          return _IdGrupoModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdGrupoModulo != value)
	    	          {
	    	              this.ValidateProperty("IdGrupoModulo", value);
	    	              this.OnIdGrupoModuloChanging(value);
	    	              this.RaiseDataMemberChanging("IdGrupoModulo");
	    	              this._IdGrupoModulo = value;
	    	              this.RaiseDataMemberChanged("IdGrupoModulo");
	    	              this.OnIdGrupoModuloChanged();
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
	    [Display(Name = "Id Modulo", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsModuloDoGrupoDetalhe];LookUpTitle[Seleção de (Id Modulo)];LookUpQuery[executeLookUpTcsModuloDoGrupoDetalhe];LookUpFinalize[finalizeLookUpTcsModuloDoGrupoDetalhe];LookUpDisplayColumns[{\"IdModulo\" : \"\"}];LookUpColumns[{\"IdModulo\" : false}];FilterDataKey[TCS_MODULO_DO_GRUPO.ID_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdModulo#true##12###0#false##::LookUpTcsModuloDoGrupoDetalhe##true#false###Linx.Framework.BV.Modulo#IQueryable###true#false", EdmKey="TCS_MODULO_DO_GRUPO.ID_MODULO")]
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
	    //Extensibility Partial Method Definitions For IdModuloDoGrupo
	    partial void OnIdModuloDoGrupoChanging(Int64 value);
	    partial void OnIdModuloDoGrupoChanged();

	    private Int64 _IdModuloDoGrupo;

	    [DataMember(IsRequired = true, Name = "IdModuloDoGrupo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo Do Grupo", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_MODULO_DO_GRUPO.ID_MODULO_DO_GRUPO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_MODULO_DO_GRUPO.ID_MODULO_DO_GRUPO")]
	    public Int64 IdModuloDoGrupo
	    {
	    	    get
	    	    {
	    	          return _IdModuloDoGrupo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdModuloDoGrupo != value)
	    	          {
	    	              this.ValidateProperty("IdModuloDoGrupo", value);
	    	              this.OnIdModuloDoGrupoChanging(value);
	    	              this.RaiseDataMemberChanging("IdModuloDoGrupo");
	    	              this._IdModuloDoGrupo = value;
	    	              this.RaiseDataMemberChanged("IdModuloDoGrupo");
	    	              this.OnIdModuloDoGrupoChanged();
	    	          }
	    	    }
	    }

	    private Int64 _TemporaryIdModuloDoGrupo;
	    [DataMember(Name = "TemporaryIdModuloDoGrupo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo Do Grupo (Tmp)", Description="Temporary Key", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdModuloDoGrupo
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdModuloDoGrupo.IsNullOrEmpty())
	    	                this._TemporaryIdModuloDoGrupo = this._IdModuloDoGrupo;
	    	          return this._TemporaryIdModuloDoGrupo;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdModuloDoGrupo != value)
	    	              this._TemporaryIdModuloDoGrupo = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsModuloGrupo _TcsModuloGrupo;
	    [DataMember(Name = "TcsModuloGrupo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsModuloGrupo_TcsModuloGrupoDetalhe", "IdGrupoModulo", "IdGrupoModulo", IsForeignKey=true)]
	    public TcsModuloGrupo TcsModuloGrupo
	    {
	        get
	        {
	            return this._TcsModuloGrupo;
	        }
	        set
	        {
	            if (this._TcsModuloGrupo != value)
	            {
	                this._TcsModuloGrupo = value;
	                this.RaisePropertyChanged("TcsModuloGrupoList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		
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

		

	[LinxPublicationView(PrimaryKeys="TcsParametroValorP1.IdParametroValor", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsParametroValor];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdParametroValor];ReadOnly[false];Entities[:IdParametroValor];SubQueryInfo[];EdmEntityName[];EntityRelations[ ];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsParametroValor")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.Setup.LinxAutoSetup.TcsParametroValor")]
	public partial class TcsParametroValor : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For IdParametro
	    partial void OnIdParametroChanging(Int64 value);
	    partial void OnIdParametroChanged();

	    private Int64 _IdParametro;

	    [DataMember(IsRequired = true, Name = "IdParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Parametro", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TcsParametroValorP1.IdParametro];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TcsParametroValorP1.IdParametro")]
	    public Int64 IdParametro
	    {
	    	    get
	    	    {
	    	          return _IdParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdParametro != value)
	    	          {
	    	              this.ValidateProperty("IdParametro", value);
	    	              this.OnIdParametroChanging(value);
	    	              this.RaiseDataMemberChanging("IdParametro");
	    	              this._IdParametro = value;
	    	              this.RaiseDataMemberChanged("IdParametro");
	    	              this.OnIdParametroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdParametroValor
	    partial void OnIdParametroValorChanging(Int64 value);
	    partial void OnIdParametroValorChanged();

	    private Int64 _IdParametroValor;

	    [DataMember(IsRequired = true, Name = "IdParametroValor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Parametro Valor", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TcsParametroValorP1.IdParametroValor];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TcsParametroValorP1.IdParametroValor")]
	    public Int64 IdParametroValor
	    {
	    	    get
	    	    {
	    	          return _IdParametroValor;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdParametroValor != value)
	    	          {
	    	              this.ValidateProperty("IdParametroValor", value);
	    	              this.OnIdParametroValorChanging(value);
	    	              this.RaiseDataMemberChanging("IdParametroValor");
	    	              this._IdParametroValor = value;
	    	              this.RaiseDataMemberChanged("IdParametroValor");
	    	              this.OnIdParametroValorChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ValorParametro
	    partial void OnValorParametroChanging(String value);
	    partial void OnValorParametroChanged();

	    private String _ValorParametro;

	    [DataMember(Name = "ValorParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Valor Padrão", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TcsParametroValorP1.ValorParametro];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TcsParametroValorP1.ValorParametro")]
	    public String ValorParametro
	    {
	    	    get
	    	    {
	    	          return _ValorParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._ValorParametro != value)
	    	          {
	    	              this.ValidateProperty("ValorParametro", value);
	    	              this.OnValorParametroChanging(value);
	    	              this.RaiseDataMemberChanging("ValorParametro");
	    	              this._ValorParametro = value;
	    	              this.RaiseDataMemberChanged("ValorParametro");
	    	              this.OnValorParametroChanged();
	    	          }
	    	    }
	    }

	    private Int64 _TemporaryIdParametroValor;
	    [DataMember(Name = "TemporaryIdParametroValor", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Parametro Valor (Tmp)", Description="Temporary Key", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdParametroValor
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdParametroValor.IsNullOrEmpty())
	    	                this._TemporaryIdParametroValor = this._IdParametroValor;
	    	          return this._TemporaryIdParametroValor;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdParametroValor != value)
	    	              this._TemporaryIdParametroValor = value;
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
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_PERFIL.ID_PERFIL", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsPerfil,TcsPerfil.TcsPerfilRegraModulo,TcsPerfil.TcsPerfilUsuario];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdPerfil];ReadOnly[false];Entities[:IdPerfil];SubQueryInfo[];EdmEntityName[];EntityRelations[ ];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsPerfil")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.Setup.LinxAutoSetup.TcsPerfil")]
	public partial class TcsPerfil : Linx.Data.Entity
	{

	
		
	

	
	    #region Flat Entities
		

	    public virtual List<object> GetFlatEntities()
	    {
	      List<object> result = new List<object>() { this };
	      if (this.TcsPerfilRegraModuloList != null && this.TcsPerfilRegraModuloList.Count() > 0)
	      {
	         foreach (var entity in this.TcsPerfilRegraModuloList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      if (this.TcsPerfilUsuarioList != null && this.TcsPerfilUsuarioList.Count() > 0)
	      {
	         foreach (var entity in this.TcsPerfilUsuarioList)
	         {
	             result.AddRange(entity.GetFlatEntities());
	         }
	      }
	      return result;
	    }

	    public virtual void ResetDetails()
	    {
	      if (this.TcsPerfilRegraModuloList != null)
	      {
	         foreach (var detail in this.TcsPerfilRegraModuloList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsPerfilRegraModuloList = null;
	      }
	      if (this.TcsPerfilUsuarioList != null)
	      {
	         foreach (var detail in this.TcsPerfilUsuarioList)
	         {
	            detail.ResetDetails();
	         }
	         this.TcsPerfilUsuarioList = null;
	      }
	    }

	    #endregion Flat Entities

	
	    #region FillDetails
		

	    public virtual void FillDetails(LinxAutoSetupDomainService context, string serializedEntitySearch = null, string jEntitySearch = null, string[] viewNames = null, int take = 0)
	    {
	      if (viewNames == null || viewNames.Contains("TcsPerfilRegraModulo"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsPerfilRegraModulo");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdPerfil"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdPerfil));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsPerfilRegraModulo and all sub-details
	         if (this.TcsPerfilRegraModuloList == null || this.TcsPerfilRegraModuloList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsPerfilRegraModuloList = context.GetPagedTcsPerfilRegraModulo(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsPerfilRegraModuloList = (from r in context.GetTcsPerfilRegraModuloByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	      if (viewNames == null || viewNames.Contains("TcsPerfilUsuario"))
	      {
	         List<EntitySearch> queryFilters = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	         EntitySearch childES = new EntitySearch("TcsPerfilUsuario");
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdPerfil"));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         childES.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdPerfil));
	         queryFilters.Add(childES);
	         string childSerializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsPerfilUsuario and all sub-details
	         if (this.TcsPerfilUsuarioList == null || this.TcsPerfilUsuarioList.Count() == 0)
	         {
	             if (take > 0)
	                 this.TcsPerfilUsuarioList = context.GetPagedTcsPerfilUsuario(childSerializedEntitySearch, 0, take, jEntitySearch).ToArray();
	             else
	                 this.TcsPerfilUsuarioList = (from r in context.GetTcsPerfilUsuarioByEntitySearchNoAssociations(childSerializedEntitySearch, jEntitySearch) select r).ToArray();
	         }
	      }
	    }

	    #endregion FillDetails

	
	    #region Adjust Hierarchy ForSaving
		

 	    public virtual bool AdjustHierarchyForSaving(ChangeSetEntry entity, ChangeSet changeSet)
 	    {
	      bool hasChanges = false;
 
 	      var _TcsPerfilRegraModuloElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsPerfilRegraModulo && ((TcsPerfilRegraModulo)e.Entity).TcsPerfil == null && e.Associations == null && e.OriginalAssociations == null && ((TcsPerfilRegraModulo)e.Entity).IdPerfil == this.IdPerfil).ToList();
 	      if (_TcsPerfilRegraModuloElements.Count > 0 && this.TcsPerfilRegraModuloList.Count() == 0)
 	      {
 	          this.TcsPerfilRegraModuloList = _TcsPerfilRegraModuloElements.Select(e => (TcsPerfilRegraModulo)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsPerfilRegraModuloElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsPerfilRegraModulo)detail.Entity).TcsPerfil = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsPerfil", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsPerfilRegraModuloList", indexDetails.ToArray());
 	      }
 
 	      var _TcsPerfilUsuarioElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsPerfilUsuario && ((TcsPerfilUsuario)e.Entity).TcsPerfil == null && e.Associations == null && e.OriginalAssociations == null && ((TcsPerfilUsuario)e.Entity).IdPerfil == this.IdPerfil).ToList();
 	      if (_TcsPerfilUsuarioElements.Count > 0 && this.TcsPerfilUsuarioList.Count() == 0)
 	      {
 	          this.TcsPerfilUsuarioList = _TcsPerfilUsuarioElements.Select(e => (TcsPerfilUsuario)e.Entity).ToList();
 	          List<int> indexDetails = new List<int>();
 	          int masterIndex = changeSet.ChangeSetEntries.IndexOf(entity);
 	          foreach (var detail in _TcsPerfilUsuarioElements)
 	          {
 	              indexDetails.Add(changeSet.ChangeSetEntries.IndexOf(detail));
 	              ((TcsPerfilUsuario)detail.Entity).TcsPerfil = this;
 	              detail.Associations = new Dictionary<string, int[]>();
 	              ((Dictionary<string, int[]>)detail.Associations).Add("TcsPerfil", new int[] { masterIndex });
 	          }
 	          hasChanges = true;
 	          if (entity.Associations == null) entity.Associations = new Dictionary<string, int[]>();
 	          ((Dictionary<string, int[]>)entity.Associations).Add("TcsPerfilUsuarioList", indexDetails.ToArray());
 	      }
 
	      return hasChanges;
 	    }

	    #endregion Adjust Hierarchy ForSaving

		
	    #region Data Properties	
	 

	    //Extensibility Partial Method Definitions For DescPerfil
	    partial void OnDescPerfilChanging(String value);
	    partial void OnDescPerfilChanged();

	    private String _DescPerfil;

	    [DataMember(IsRequired = true, Name = "DescPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PERFIL.DESC_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL.DESC_PERFIL")]
	    public String DescPerfil
	    {
	    	    get
	    	    {
	    	          return _DescPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescPerfil != value)
	    	          {
	    	              this.ValidateProperty("DescPerfil", value);
	    	              this.OnDescPerfilChanging(value);
	    	              this.RaiseDataMemberChanging("DescPerfil");
	    	              this._DescPerfil = value;
	    	              this.RaiseDataMemberChanged("DescPerfil");
	    	              this.OnDescPerfilChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdPerfil
	    partial void OnIdPerfilChanging(Int64 value);
	    partial void OnIdPerfilChanged();

	    private Int64 _IdPerfil;

	    [DataMember(IsRequired = true, Name = "IdPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Perfil", Description="", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PERFIL.ID_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL.ID_PERFIL")]
	    public Int64 IdPerfil
	    {
	    	    get
	    	    {
	    	          return _IdPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdPerfil != value)
	    	          {
	    	              this.ValidateProperty("IdPerfil", value);
	    	              this.OnIdPerfilChanging(value);
	    	              this.RaiseDataMemberChanging("IdPerfil");
	    	              this._IdPerfil = value;
	    	              this.RaiseDataMemberChanged("IdPerfil");
	    	              this.OnIdPerfilChanged();
	    	          }
	    	    }
	    }

	    private Int64 _TemporaryIdPerfil;
	    [DataMember(Name = "TemporaryIdPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Perfil (Tmp)", Description="Temporary Key", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdPerfil
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdPerfil.IsNullOrEmpty())
	    	                this._TemporaryIdPerfil = this._IdPerfil;
	    	          return this._TemporaryIdPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdPerfil != value)
	    	              this._TemporaryIdPerfil = value;
	    	    }
	    }	

	    #endregion Data Properties

	 	 
	    #region Detail Associations
	 
		
	    private IEnumerable<TcsPerfilRegraModulo> _TcsPerfilRegraModuloList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsPerfil_TcsPerfilRegraModulo", "IdPerfil", "IdPerfil", IsForeignKey=false)]
	    [DataMember(Name = "TcsPerfilRegraModuloList", EmitDefaultValue = true)]
	    public IEnumerable<TcsPerfilRegraModulo> TcsPerfilRegraModuloList
	    {
	        get
	        {
	
	            if (this._TcsPerfilRegraModuloList == null)
	            	this._TcsPerfilRegraModuloList = new List<TcsPerfilRegraModulo>();
	
	            return this._TcsPerfilRegraModuloList;
	        }
	        set
	        {
	            if (this._TcsPerfilRegraModuloList != value)
	            {
	                this._TcsPerfilRegraModuloList = value;
	                this.RaisePropertyChanged("TcsPerfilRegraModuloList");
	            }
	        }
	    }	 
		
	    private IEnumerable<TcsPerfilUsuario> _TcsPerfilUsuarioList;
	    [XmlIgnore()]
	    [XmlAttribute()]
	    [Include()]
	    [Composition()]
	    [Display(AutoGenerateField = false)]
	    [SoapIgnore()]
	    [Association("FK_TcsPerfil_TcsPerfilUsuario", "IdPerfil", "IdPerfil", IsForeignKey=false)]
	    [DataMember(Name = "TcsPerfilUsuarioList", EmitDefaultValue = true)]
	    public IEnumerable<TcsPerfilUsuario> TcsPerfilUsuarioList
	    {
	        get
	        {
	
	            if (this._TcsPerfilUsuarioList == null)
	            	this._TcsPerfilUsuarioList = new List<TcsPerfilUsuario>();
	
	            return this._TcsPerfilUsuarioList;
	        }
	        set
	        {
	            if (this._TcsPerfilUsuarioList != value)
	            {
	                this._TcsPerfilUsuarioList = value;
	                this.RaisePropertyChanged("TcsPerfilUsuarioList");
	            }
	        }
	    }	 
		 
	    #endregion Detail Associations		
		
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

		

	[LinxPublicationView(PrimaryKeys="TCS_PERFIL_REGRA_MODULO.ID_PERFIL_REGRA_MODULO", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdPerfilRegraModulo];ReadOnly[false];Entities[TCS_PERFIL_REGRA_MODULO:IdPerfilRegraModulo];SubQueryInfo[Select 1 From #ParentAlias#. as #Alias#];EdmEntityName[TCS_PERFIL_REGRA_MODULO];EntityRelations[ ];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsPerfilRegraModulo")]
	[Serializable()]
	public partial class TcsPerfilRegraModulo : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(LinxAutoSetupDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsPerfil");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdPerfil"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdPerfil));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsPerfil
	         this.TcsPerfil = (from r in context.GetTcsPerfilByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For IdModulo
	    partial void OnIdModuloChanging(Int64 value);
	    partial void OnIdModuloChanged();

	    private Int64 _IdModulo;

	    [DataMember(IsRequired = true, Name = "IdModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Modulo", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsPerfilRegraModulo];LookUpTitle[Seleção de (Id Modulo)];LookUpQuery[executeLookUpTcsPerfilRegraModulo];LookUpFinalize[finalizeLookUpTcsPerfilRegraModulo];LookUpDisplayColumns[{\"IdModulo\" : \"\"}];LookUpColumns[{\"IdModulo\" : false}];FilterDataKey[TCS_PERFIL_REGRA_MODULO.ID_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdModulo#true##12###0#false##::LookUpTcsPerfilRegraModulo##true#false###Linx.Framework.BV.Perfil#IQueryable###true#false", EdmKey="TCS_PERFIL_REGRA_MODULO.ID_MODULO")]
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
	    //Extensibility Partial Method Definitions For IdPerfil
	    partial void OnIdPerfilChanging(Int64 value);
	    partial void OnIdPerfilChanged();

	    private Int64 _IdPerfil;

	    [DataMember(IsRequired = true, Name = "IdPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Perfil", Description="", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PERFIL_REGRA_MODULO.TCS_PERFIL.ID_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL_REGRA_MODULO.TCS_PERFIL.ID_PERFIL")]
	    public Int64 IdPerfil
	    {
	    	    get
	    	    {
	    	          return _IdPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdPerfil != value)
	    	          {
	    	              this.ValidateProperty("IdPerfil", value);
	    	              this.OnIdPerfilChanging(value);
	    	              this.RaiseDataMemberChanging("IdPerfil");
	    	              this._IdPerfil = value;
	    	              this.RaiseDataMemberChanged("IdPerfil");
	    	              this.OnIdPerfilChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdPerfilRegraModulo
	    partial void OnIdPerfilRegraModuloChanging(Int64 value);
	    partial void OnIdPerfilRegraModuloChanged();

	    private Int64 _IdPerfilRegraModulo;

	    [DataMember(IsRequired = true, Name = "IdPerfilRegraModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Módulo", Description="", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PERFIL_REGRA_MODULO.ID_PERFIL_REGRA_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL_REGRA_MODULO.ID_PERFIL_REGRA_MODULO")]
	    public Int64 IdPerfilRegraModulo
	    {
	    	    get
	    	    {
	    	          return _IdPerfilRegraModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdPerfilRegraModulo != value)
	    	          {
	    	              this.ValidateProperty("IdPerfilRegraModulo", value);
	    	              this.OnIdPerfilRegraModuloChanging(value);
	    	              this.RaiseDataMemberChanging("IdPerfilRegraModulo");
	    	              this._IdPerfilRegraModulo = value;
	    	              this.RaiseDataMemberChanged("IdPerfilRegraModulo");
	    	              this.OnIdPerfilRegraModuloChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxRegraAcessoModulo
	    partial void OnLxRegraAcessoModuloChanging(Byte value);
	    partial void OnLxRegraAcessoModuloChanged();

	    private Byte _LxRegraAcessoModulo;

	    [DataMember(IsRequired = true, Name = "LxRegraAcessoModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Módulo", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[RegraAcesso];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PERFIL_REGRA_MODULO.LX_REGRA_ACESSO_MODULO")]
	    public Byte LxRegraAcessoModulo
	    {
	    	    get
	    	    {
	    	          return _LxRegraAcessoModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxRegraAcessoModulo != value)
	    	          {
	    	              this.ValidateProperty("LxRegraAcessoModulo", value);
	    	              this.OnLxRegraAcessoModuloChanging(value);
	    	              this.RaiseDataMemberChanging("LxRegraAcessoModulo");
	    	              this._LxRegraAcessoModulo = value;
	    	              this.RaiseDataMemberChanged("LxRegraAcessoModulo");
	    	              this.OnLxRegraAcessoModuloChanged();
	    	          }
	    	    }
	    }

	    private Int64 _TemporaryIdPerfilRegraModulo;
	    [DataMember(Name = "TemporaryIdPerfilRegraModulo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Regra Módulo (Tmp)", Description="Temporary Key", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdPerfilRegraModulo
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdPerfilRegraModulo.IsNullOrEmpty())
	    	                this._TemporaryIdPerfilRegraModulo = this._IdPerfilRegraModulo;
	    	          return this._TemporaryIdPerfilRegraModulo;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdPerfilRegraModulo != value)
	    	              this._TemporaryIdPerfilRegraModulo = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsPerfil _TcsPerfil;
	    [DataMember(Name = "TcsPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsPerfil_TcsPerfilRegraModulo", "IdPerfil", "IdPerfil", IsForeignKey=true)]
	    public TcsPerfil TcsPerfil
	    {
	        get
	        {
	            return this._TcsPerfil;
	        }
	        set
	        {
	            if (this._TcsPerfil != value)
	            {
	                this._TcsPerfil = value;
	                this.RaisePropertyChanged("TcsPerfilList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxRegraAcessoModuloValues()
	    {
	    	    return Linx.Framework.Setup.Domains.RegraAcesso.GetValues();
	    }
	    private string _lxRegraAcessoModuloName;
	    [DataMember(IsRequired = false, Name = "LxRegraAcessoModuloName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Regra Módulo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxRegraAcessoModuloName
	    {
	    	    get { if (this.LxRegraAcessoModulo.IsNull()) { _lxRegraAcessoModuloName = String.Empty; } else { string key = this.LxRegraAcessoModulo.ToString(); var dmValues = this.GetLxRegraAcessoModuloValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxRegraAcessoModuloName) _lxRegraAcessoModuloName = domainName; } return _lxRegraAcessoModuloName; } set { _lxRegraAcessoModuloName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdTcsUsuarioPerfil];ReadOnly[false];Entities[:IdTcsUsuarioPerfil];SubQueryInfo[Select 1 From #ParentAlias#. as #Alias#];EdmEntityName[];EntityRelations[ ];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsPerfilUsuario")]
	[Serializable()]
	public partial class TcsPerfilUsuario : Linx.Data.Entity
	{

	
		
	

	
	    #region Load Data Parent
		

	    public void LoadParent(LinxAutoSetupDomainService context)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch parentSearch = new EntitySearch("TcsPerfil");
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdPerfil"));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         parentSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.IdPerfil));
	         queryFilters.Add(parentSearch);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         //Load TcsPerfil
	         this.TcsPerfil = (from r in context.GetTcsPerfilByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
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
	 

	    //Extensibility Partial Method Definitions For IdPerfil
	    partial void OnIdPerfilChanging(Int64 value);
	    partial void OnIdPerfilChanged();

	    private Int64 _IdPerfil;

	    [DataMember(IsRequired = true, Name = "IdPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Perfil", Description="", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_PERFIL.TCS_PERFIL.ID_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_PERFIL.TCS_PERFIL.ID_PERFIL")]
	    public Int64 IdPerfil
	    {
	    	    get
	    	    {
	    	          return _IdPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdPerfil != value)
	    	          {
	    	              this.ValidateProperty("IdPerfil", value);
	    	              this.OnIdPerfilChanging(value);
	    	              this.RaiseDataMemberChanging("IdPerfil");
	    	              this._IdPerfil = value;
	    	              this.RaiseDataMemberChanged("IdPerfil");
	    	              this.OnIdPerfilChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdTcsUsuarioPerfil
	    partial void OnIdTcsUsuarioPerfilChanging(Int64 value);
	    partial void OnIdTcsUsuarioPerfilChanged();

	    private Int64 _IdTcsUsuarioPerfil;

	    [DataMember(IsRequired = true, Name = "IdTcsUsuarioPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Perfil", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_USUARIO_PERFIL.ID_TCS_USUARIO_PERFIL")]
	    public Int64 IdTcsUsuarioPerfil
	    {
	    	    get
	    	    {
	    	          return _IdTcsUsuarioPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsUsuarioPerfil != value)
	    	          {
	    	              this.ValidateProperty("IdTcsUsuarioPerfil", value);
	    	              this.OnIdTcsUsuarioPerfilChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsUsuarioPerfil");
	    	              this._IdTcsUsuarioPerfil = value;
	    	              this.RaiseDataMemberChanged("IdTcsUsuarioPerfil");
	    	              this.OnIdTcsUsuarioPerfilChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdUsuario
	    partial void OnIdUsuarioChanging(Int64 value);
	    partial void OnIdUsuarioChanged();

	    private Int64 _IdUsuario;

	    [DataMember(IsRequired = true, Name = "IdUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Usuario", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsUsuario];LookUpTitle[Seleção de (Id Usuario)];LookUpQuery[executeLookUpTcsUsuario];LookUpFinalize[finalizeLookUpTcsUsuario];LookUpDisplayColumns[{\"IdUsuario\" : \"Id Usuario\"}];LookUpColumns[{\"IdUsuario\" : false}];FilterDataKey[TCS_USUARIO_PERFIL.TCS_USUARIO.ID_USUARIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdUsuario#true##24:0##Id Usuario#1#false##::LookUpTcsUsuario##true#false#TCS_USUARIO#TCS_USUARIO#Linx.Framework.BV.Perfil#IQueryable###true#true", EdmKey="TCS_USUARIO_PERFIL.TCS_USUARIO.ID_USUARIO")]
	    public Int64 IdUsuario
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

	    private Int64 _TemporaryIdTcsUsuarioPerfil;
	    [DataMember(Name = "TemporaryIdTcsUsuarioPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Usuario Perfil (Tmp)", Description="Temporary Key", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdTcsUsuarioPerfil
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdTcsUsuarioPerfil.IsNullOrEmpty())
	    	                this._TemporaryIdTcsUsuarioPerfil = this._IdTcsUsuarioPerfil;
	    	          return this._TemporaryIdTcsUsuarioPerfil;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdTcsUsuarioPerfil != value)
	    	              this._TemporaryIdTcsUsuarioPerfil = value;
	    	    }
	    }	

	    #endregion Data Properties

		

	    #region Parent Association
	 
	    private TcsPerfil _TcsPerfil;
	    [DataMember(Name = "TcsPerfil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Include()]
	    [Display(AutoGenerateField = false)]	
	    [XmlIgnore()]	
	    [SoapIgnore()]
	    [Association("FK_TcsPerfil_TcsPerfilUsuario", "IdPerfil", "IdPerfil", IsForeignKey=true)]
	    public TcsPerfil TcsPerfil
	    {
	        get
	        {
	            return this._TcsPerfil;
	        }
	        set
	        {
	            if (this._TcsPerfil != value)
	            {
	                this._TcsPerfil = value;
	                this.RaisePropertyChanged("TcsPerfilList");
	            }
	        }
	    }	
	 
	    #endregion Parent Association		
		
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

		

	[LinxPublicationView(PrimaryKeys="AmbienteInfo.EntityUniqueKey", IsUpdatable=false, EdmName="")]
		
	[DataContract(IsReference = false, Name = "AmbienteInfo")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.Setup.LinxAutoSetup.AmbienteInfo")]
	public partial class AmbienteInfo 
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
	 


	    private int _IdLinx;

	    [DataMember(Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public int IdLinx
	    {
	    	    get
	    	    {
	    	          return _IdLinx;
	    	    }
	    	    set
	    	    {
	    	          this._IdLinx = value;
	    	    }
	    }

	    private string _NomeUsuario;

	    [DataMember(Name = "NomeUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string NomeUsuario
	    {
	    	    get
	    	    {
	    	          return _NomeUsuario;
	    	    }
	    	    set
	    	    {
	    	          this._NomeUsuario = value;
	    	    }
	    }

	    private string _NomeAutenticacao;

	    [DataMember(Name = "NomeAutenticacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string NomeAutenticacao
	    {
	    	    get
	    	    {
	    	          return _NomeAutenticacao;
	    	    }
	    	    set
	    	    {
	    	          this._NomeAutenticacao = value;
	    	    }
	    }

	    private string _Senha;

	    [DataMember(Name = "Senha", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Senha
	    {
	    	    get
	    	    {
	    	          return _Senha;
	    	    }
	    	    set
	    	    {
	    	          this._Senha = value;
	    	    }
	    }

	    private string _Email;

	    [DataMember(Name = "Email", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Email
	    {
	    	    get
	    	    {
	    	          return _Email;
	    	    }
	    	    set
	    	    {
	    	          this._Email = value;
	    	    }
	    }

	    private string _RazaoSocial;

	    [DataMember(Name = "RazaoSocial", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string RazaoSocial
	    {
	    	    get
	    	    {
	    	          return _RazaoSocial;
	    	    }
	    	    set
	    	    {
	    	          this._RazaoSocial = value;
	    	    }
	    }

	    private string _Cnpj;

	    [DataMember(Name = "Cnpj", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Cnpj
	    {
	    	    get
	    	    {
	    	          return _Cnpj;
	    	    }
	    	    set
	    	    {
	    	          this._Cnpj = value;
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
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_EMPRESA_GPECON.ID_LINX,TCS_EMPRESA_GPECON.ID_LINX_GPECON", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsEmpresaGpecon];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[];EntityRelations[ ];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsEmpresaGpecon")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.Setup.LinxAutoSetup.TcsEmpresaGpecon")]
	public partial class TcsEmpresaGpecon : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For IdLinx
	    partial void OnIdLinxChanging(Int32 value);
	    partial void OnIdLinxChanged();

	    private Int32 _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_EMPRESA_GPECON.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_GPECON.ID_LINX")]
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
	    //Extensibility Partial Method Definitions For IdLinxGpecon
	    partial void OnIdLinxGpeconChanging(Int32 value);
	    partial void OnIdLinxGpeconChanged();

	    private Int32 _IdLinxGpecon;

	    [DataMember(IsRequired = true, Name = "IdLinxGpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx Gpecon", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_EMPRESA_GPECON.ID_LINX_GPECON];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_EMPRESA_GPECON.ID_LINX_GPECON")]
	    public Int32 IdLinxGpecon
	    {
	    	    get
	    	    {
	    	          return _IdLinxGpecon;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLinxGpecon != value)
	    	          {
	    	              this.ValidateProperty("IdLinxGpecon", value);
	    	              this.OnIdLinxGpeconChanging(value);
	    	              this.RaiseDataMemberChanging("IdLinxGpecon");
	    	              this._IdLinxGpecon = value;
	    	              this.RaiseDataMemberChanged("IdLinxGpecon");
	    	              this.OnIdLinxGpeconChanged();
	    	          }
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
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TCS_AMBIENTE.ID_TCS_AMBIENTE", IsUpdatable=false, EdmName="Linx.Framework.Autorizacao.BM.AutorizacaoContext")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsAmbienteInfo];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[];ReadOnly[false];SubQueryInfo[];EdmEntityName[TCS_AMBIENTE];EntityRelations[TCS_APLICACAO(TCS_APLICACAO)#TCS_APLICATIVO(TCS_APLICATIVO)#TCS_EMPRESA_AUTENTICACAO(TCS_EMPRESA_AUTENTICACAO)];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsAmbienteInfo")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.Setup.LinxAutoSetup.TcsAmbienteInfo")]
	public partial class TcsAmbienteInfo : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For IdAplicacao
	    partial void OnIdAplicacaoChanging(int value);
	    partial void OnIdAplicacaoChanged();

	    private int _IdAplicacao;

	    [DataMember(IsRequired = true, Name = "IdAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Aplicacao", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_AMBIENTE.TCS_APLICACAO.ID_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.ID_APLICACAO")]
	    public int IdAplicacao
	    {
	    	    get
	    	    {
	    	          return _IdAplicacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdAplicacao != value)
	    	          {
	    	              this.ValidateProperty("IdAplicacao", value);
	    	              this.OnIdAplicacaoChanging(value);
	    	              this.RaiseDataMemberChanging("IdAplicacao");
	    	              this._IdAplicacao = value;
	    	              this.RaiseDataMemberChanged("IdAplicacao");
	    	              this.OnIdAplicacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLinx
	    partial void OnIdLinxChanging(int value);
	    partial void OnIdLinxChanged();

	    private int _IdLinx;

	    [DataMember(IsRequired = true, Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Linx", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX")]
	    public int IdLinx
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
	    //Extensibility Partial Method Definitions For IdTcsAmbiente
	    partial void OnIdTcsAmbienteChanging(int value);
	    partial void OnIdTcsAmbienteChanged();

	    private int _IdTcsAmbiente;

	    [DataMember(IsRequired = true, Name = "IdTcsAmbiente", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Tcs Ambiente", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_AMBIENTE.ID_TCS_AMBIENTE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.ID_TCS_AMBIENTE")]
	    public int IdTcsAmbiente
	    {
	    	    get
	    	    {
	    	          return _IdTcsAmbiente;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdTcsAmbiente != value)
	    	          {
	    	              this.ValidateProperty("IdTcsAmbiente", value);
	    	              this.OnIdTcsAmbienteChanging(value);
	    	              this.RaiseDataMemberChanging("IdTcsAmbiente");
	    	              this._IdTcsAmbiente = value;
	    	              this.RaiseDataMemberChanged("IdTcsAmbiente");
	    	              this.OnIdTcsAmbienteChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidAplicacao
	    partial void OnUidAplicacaoChanging(Guid value);
	    partial void OnUidAplicacaoChanged();

	    private Guid _UidAplicacao;

	    [DataMember(IsRequired = true, Name = "UidAplicacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Aplicacao", Description="", Order = 4, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_AMBIENTE.TCS_APLICACAO.UID_APLICACAO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.TCS_APLICACAO.UID_APLICACAO")]
	    public Guid UidAplicacao
	    {
	    	    get
	    	    {
	    	          return _UidAplicacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidAplicacao != value)
	    	          {
	    	              this.ValidateProperty("UidAplicacao", value);
	    	              this.OnUidAplicacaoChanging(value);
	    	              this.RaiseDataMemberChanging("UidAplicacao");
	    	              this._UidAplicacao = value;
	    	              this.RaiseDataMemberChanged("UidAplicacao");
	    	              this.OnUidAplicacaoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For UidEmpresa
	    partial void OnUidEmpresaChanging(Guid value);
	    partial void OnUidEmpresaChanged();

	    private Guid _UidEmpresa;

	    [DataMember(IsRequired = true, Name = "UidEmpresa", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Empresa", Description="", Order = 3, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[36:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.UID_EMPRESA")]
	    public Guid UidEmpresa
	    {
	    	    get
	    	    {
	    	          return _UidEmpresa;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidEmpresa != value)
	    	          {
	    	              this.ValidateProperty("UidEmpresa", value);
	    	              this.OnUidEmpresaChanging(value);
	    	              this.RaiseDataMemberChanging("UidEmpresa");
	    	              this._UidEmpresa = value;
	    	              this.RaiseDataMemberChanged("UidEmpresa");
	    	              this.OnUidEmpresaChanged();
	    	          }
	    	    }
	    }	

	    #endregion Data Properties

		  
	    #region MetaData Methods
	
	    public override List<EdmEntityMetaData> CreateMetaDataMaps()
	    {	

	        EdmEntityMetaData metaData;
	        List<EdmEntityMetaData> dataMaps = base.CreateMetaDataMaps();
	        metaData = dataMaps.Where(e => e.QualifiedEntitySetName == "AutorizacaoContext.TCS_AMBIENTE").FirstOrDefault();
	        if (metaData == null)
	        {
	             metaData = new EdmEntityMetaData() { CheckExistence = false, EdmEntityType = typeof(Linx.Framework.Autorizacao.BM.TCS_AMBIENTE), QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE" };
	             dataMaps.Add(metaData);
	        }

	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE.ID_TCS_AMBIENTE", Source = "IdTcsAmbiente", Target = "ID_TCS_AMBIENTE", TargetKeyName = "", NoUpdatable = false, IsKey = true, IsFK = false, QualifiedEntitySetName = "AutorizacaoContext.TCS_AMBIENTE", RelationPropertyName = "TCS_AMBIENTE" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE.TCS_APLICACAO.ID_APLICACAO", Source = "IdAplicacao", Target = "ID_APLICACAO", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_APLICACAO", RelationPropertyName = "TCS_APLICACAO" });
	        metaData.PropertiesMap.Add(new EdmEntityPropertydMap() { EdmKey ="TCS_AMBIENTE.TCS_EMPRESA_AUTENTICACAO.ID_LINX", Source = "IdLinx", Target = "ID_LINX", TargetKeyName = "", NoUpdatable = false, IsKey = false, IsFK = true, QualifiedEntitySetName = "AutorizacaoContext.TCS_EMPRESA_AUTENTICACAO", RelationPropertyName = "TCS_EMPRESA_AUTENTICACAO" });

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

		

	[LinxPublicationView(PrimaryKeys="TCS_PARAMETRO_AUTORIZACAO.ID_PARAMETRO", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TcsParametroAutorizacao];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdParametro];ReadOnly[false];Entities[:IdParametro];SubQueryInfo[];EdmEntityName[];EntityRelations[ ];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TcsParametroAutorizacao")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.Setup.LinxAutoSetup.TcsParametroAutorizacao")]
	public partial class TcsParametroAutorizacao : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For IdParametro
	    partial void OnIdParametroChanging(Int64 value);
	    partial void OnIdParametroChanged();

	    private Int64 _IdParametro;

	    [DataMember(IsRequired = true, Name = "IdParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Parametro", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[24:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_AUTORIZACAO.ID_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.ID_PARAMETRO")]
	    public Int64 IdParametro
	    {
	    	    get
	    	    {
	    	          return _IdParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdParametro != value)
	    	          {
	    	              this.ValidateProperty("IdParametro", value);
	    	              this.OnIdParametroChanging(value);
	    	              this.RaiseDataMemberChanging("IdParametro");
	    	              this._IdParametro = value;
	    	              this.RaiseDataMemberChanged("IdParametro");
	    	              this.OnIdParametroChanged();
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
	    [Display(Name = "Aplicativo", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTcsAplicativo];LookUpTitle[Seleção de (Aplicativo)];LookUpQuery[executeLookUpTcsAplicativo];LookUpFinalize[finalizeLookUpTcsAplicativo];LookUpDisplayColumns[{\"IdTcsAplicativo\" : \"Aplicativo\"}];LookUpColumns[{\"IdTcsAplicativo\" : true}];FilterDataKey[TCS_PARAMETRO_AUTORIZACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdTcsAplicativo#true##12:0##Aplicativo#0#true##::LookUpTcsAplicativo##false#false#TCS_APLICATIVO#TCS_APLICATIVO#Linx.Framework.BV.ParametroAutorizacao#IQueryable###true#false", EdmKey="TCS_PARAMETRO_AUTORIZACAO.TCS_APLICATIVO.ID_TCS_APLICATIVO")]
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
	    //Extensibility Partial Method Definitions For TituloParametro
	    partial void OnTituloParametroChanging(String value);
	    partial void OnTituloParametroChanged();

	    private String _TituloParametro;

	    [DataMember(IsRequired = true, Name = "TituloParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Título", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TCS_PARAMETRO_AUTORIZACAO.TITULO_PARAMETRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TCS_PARAMETRO_AUTORIZACAO.TITULO_PARAMETRO")]
	    public String TituloParametro
	    {
	    	    get
	    	    {
	    	          return _TituloParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._TituloParametro != value)
	    	          {
	    	              this.ValidateProperty("TituloParametro", value);
	    	              this.OnTituloParametroChanging(value);
	    	              this.RaiseDataMemberChanging("TituloParametro");
	    	              this._TituloParametro = value;
	    	              this.RaiseDataMemberChanged("TituloParametro");
	    	              this.OnTituloParametroChanged();
	    	          }
	    	    }
	    }

	    private Int64 _TemporaryIdParametro;
	    [DataMember(Name = "TemporaryIdParametro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Parametro (Tmp)", Description="Temporary Key", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int64 TemporaryIdParametro
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdParametro.IsNullOrEmpty())
	    	                this._TemporaryIdParametro = this._IdParametro;
	    	          return this._TemporaryIdParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdParametro != value)
	    	              this._TemporaryIdParametro = value;
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
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="MultimarcaInfo.EntityUniqueKey", IsUpdatable=false, EdmName="")]
		
	[DataContract(IsReference = false, Name = "MultimarcaInfo")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.Setup.LinxAutoSetup.MultimarcaInfo")]
	public partial class MultimarcaInfo 
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
	 


	    private int _IdLinx;

	    [DataMember(Name = "IdLinx", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public int IdLinx
	    {
	    	    get
	    	    {
	    	          return _IdLinx;
	    	    }
	    	    set
	    	    {
	    	          this._IdLinx = value;
	    	    }
	    }

	    private string _RazaoSocial;

	    [DataMember(Name = "RazaoSocial", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string RazaoSocial
	    {
	    	    get
	    	    {
	    	          return _RazaoSocial;
	    	    }
	    	    set
	    	    {
	    	          this._RazaoSocial = value;
	    	    }
	    }

	    private string _Cnpj;

	    [DataMember(Name = "Cnpj", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Cnpj
	    {
	    	    get
	    	    {
	    	          return _Cnpj;
	    	    }
	    	    set
	    	    {
	    	          this._Cnpj = value;
	    	    }
	    }

	    private string _InscrEstadual;

	    [DataMember(Name = "InscrEstadual", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string InscrEstadual
	    {
	    	    get
	    	    {
	    	          return _InscrEstadual;
	    	    }
	    	    set
	    	    {
	    	          this._InscrEstadual = value;
	    	    }
	    }

	    private byte _LxTipoLogradouro;

	    [DataMember(Name = "LxTipoLogradouro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public byte LxTipoLogradouro
	    {
	    	    get
	    	    {
	    	          return _LxTipoLogradouro;
	    	    }
	    	    set
	    	    {
	    	          this._LxTipoLogradouro = value;
	    	    }
	    }

	    private string _Logradouro;

	    [DataMember(Name = "Logradouro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Logradouro
	    {
	    	    get
	    	    {
	    	          return _Logradouro;
	    	    }
	    	    set
	    	    {
	    	          this._Logradouro = value;
	    	    }
	    }

	    private string _Numero;

	    [DataMember(Name = "Numero", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Numero
	    {
	    	    get
	    	    {
	    	          return _Numero;
	    	    }
	    	    set
	    	    {
	    	          this._Numero = value;
	    	    }
	    }

	    private string _Complemento;

	    [DataMember(Name = "Complemento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Complemento
	    {
	    	    get
	    	    {
	    	          return _Complemento;
	    	    }
	    	    set
	    	    {
	    	          this._Complemento = value;
	    	    }
	    }

	    private string _Bairro;

	    [DataMember(Name = "Bairro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Bairro
	    {
	    	    get
	    	    {
	    	          return _Bairro;
	    	    }
	    	    set
	    	    {
	    	          this._Bairro = value;
	    	    }
	    }

	    private string _Municipio;

	    [DataMember(Name = "Municipio", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Municipio
	    {
	    	    get
	    	    {
	    	          return _Municipio;
	    	    }
	    	    set
	    	    {
	    	          this._Municipio = value;
	    	    }
	    }

	    private string _Uf;

	    [DataMember(Name = "Uf", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Uf
	    {
	    	    get
	    	    {
	    	          return _Uf;
	    	    }
	    	    set
	    	    {
	    	          this._Uf = value;
	    	    }
	    }

	    private string _Cep;

	    [DataMember(Name = "Cep", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Cep
	    {
	    	    get
	    	    {
	    	          return _Cep;
	    	    }
	    	    set
	    	    {
	    	          this._Cep = value;
	    	    }
	    }

	    private string _Pais;

	    [DataMember(Name = "Pais", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Pais
	    {
	    	    get
	    	    {
	    	          return _Pais;
	    	    }
	    	    set
	    	    {
	    	          this._Pais = value;
	    	    }
	    }

	    private string _ObsEndereco;

	    [DataMember(Name = "ObsEndereco", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string ObsEndereco
	    {
	    	    get
	    	    {
	    	          return _ObsEndereco;
	    	    }
	    	    set
	    	    {
	    	          this._ObsEndereco = value;
	    	    }
	    }

	    private string _DddCelular;

	    [DataMember(Name = "DddCelular", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string DddCelular
	    {
	    	    get
	    	    {
	    	          return _DddCelular;
	    	    }
	    	    set
	    	    {
	    	          this._DddCelular = value;
	    	    }
	    }

	    private string _FoneCelular;

	    [DataMember(Name = "FoneCelular", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string FoneCelular
	    {
	    	    get
	    	    {
	    	          return _FoneCelular;
	    	    }
	    	    set
	    	    {
	    	          this._FoneCelular = value;
	    	    }
	    }

	    private string _DddFixo;

	    [DataMember(Name = "DddFixo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string DddFixo
	    {
	    	    get
	    	    {
	    	          return _DddFixo;
	    	    }
	    	    set
	    	    {
	    	          this._DddFixo = value;
	    	    }
	    }

	    private string _FoneFixo;

	    [DataMember(Name = "FoneFixo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string FoneFixo
	    {
	    	    get
	    	    {
	    	          return _FoneFixo;
	    	    }
	    	    set
	    	    {
	    	          this._FoneFixo = value;
	    	    }
	    }

	    private string _Email;

	    [DataMember(Name = "Email", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Email
	    {
	    	    get
	    	    {
	    	          return _Email;
	    	    }
	    	    set
	    	    {
	    	          this._Email = value;
	    	    }
	    }

	    private string _NomeUsuario;

	    [DataMember(Name = "NomeUsuario", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string NomeUsuario
	    {
	    	    get
	    	    {
	    	          return _NomeUsuario;
	    	    }
	    	    set
	    	    {
	    	          this._NomeUsuario = value;
	    	    }
	    }

	    private string _NomeAutenticacao;

	    [DataMember(Name = "NomeAutenticacao", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string NomeAutenticacao
	    {
	    	    get
	    	    {
	    	          return _NomeAutenticacao;
	    	    }
	    	    set
	    	    {
	    	          this._NomeAutenticacao = value;
	    	    }
	    }

	    private string _Senha;

	    [DataMember(Name = "Senha", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string Senha
	    {
	    	    get
	    	    {
	    	          return _Senha;
	    	    }
	    	    set
	    	    {
	    	          this._Senha = value;
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
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TBC_PFJ.ID_PFJ", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TbcFilial];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdPfj];ReadOnly[false];Entities[:IdPfj];SubQueryInfo[];EdmEntityName[];EntityRelations[ ];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TbcFilial")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.Setup.LinxAutoSetup.TbcFilial")]
	public partial class TbcFilial : Linx.Data.Entity
	{

	

	    public TbcFilial() : this(true) { }

	    public TbcFilial(bool setDefaults) 
	    {

	        if (setDefaults)
	        {
	        	        IndicaFilial = false;
	        	        LxPfjFisicaJuridica = 2;
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
	 

	    //Extensibility Partial Method Definitions For Bairro
	    partial void OnBairroChanging(String value);
	    partial void OnBairroChanged();

	    private String _Bairro;

	    [DataMember(Name = "Bairro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Bairro", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpWGeoEndereco];LookUpTitle[Seleção de (Bairro)];LookUpQuery[executeLookUpWGeoEndereco];LookUpFinalize[finalizeLookUpWGeoEndereco];LookUpDisplayColumns[{\"Cep\" : \"CEP\", \"LxTipoLogradouro\" : \"Tipo do Logradouro\", \"Logradouro\" : \"Logradouro\", \"Bairro\" : \"Bairro\", \"DescMunicipio\" : \"Município\", \"SiglaUf\" : \"Sigla da UF\", \"DescPais\" : \"País\", \"Obs\" : \"Observação\"}];LookUpColumns[{\"Cep\" : true, \"LxTipoLogradouro\" : true, \"Logradouro\" : true, \"Bairro\" : true, \"DescMunicipio\" : true, \"SiglaUf\" : true, \"DescPais\" : true, \"Obs\" : true}];FilterDataKey[TBC_PFJ.BAIRRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#Bairro#false##400##Bairro#3#true##::LookUpWGeoEndereco##false#false##W_GEO_ENDERECO#Linx.CadastroBase.BV.CadastroPfj#IQueryable#DescMunicipio,CodMunicipioIbge,IdMunicipio,InativoMunicipio[DescMunicipio,SiglaUf,DescUf,DescPais,CodMunicipioIbge,SiglaPais,IdMunicipio,IdPais,IdUf,InativoMunicipio,InativoUf,InativoPais];SiglaUf,DescUf,IdUf,InativoUf[SiglaUf,DescUf,DescPais,SiglaPais,IdPais,IdUf,InativoUf,InativoPais];DescPais,SiglaPais,IdPais,InativoPais[DescPais,SiglaPais,IdPais,InativoPais]#Cep[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];LxTipoLogradouro[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];Logradouro[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];Bairro[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];Municipio[SiglaUf=Uf,DescPais=Pais,IdPais=IdPais,IdUf=IdUf];Uf[DescPais=Pais,IdPais=IdPais];CodMunicipioIbge[SiglaUf=Uf,DescPais=Pais,IdPais=IdPais,IdUf=IdUf];IdCep[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];IdMunicipio[SiglaUf=Uf,DescPais=Pais,IdPais=IdPais,IdUf=IdUf];IdUf[DescPais=Pais,IdPais=IdPais];ObsEndereco[Cep=Cep,LxTipoLogradouro=LxTipoLogradouro,Logradouro=Logradouro,Bairro=Bairro,DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdCep=IdCep,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf]#true#false", EdmKey="TBC_PFJ.BAIRRO")]
	    public String Bairro
	    {
	    	    get
	    	    {
	    	          return _Bairro;
	    	    }
	    	    set
	    	    {
	    	          if (this._Bairro != value)
	    	          {
	    	              this.ValidateProperty("Bairro", value);
	    	              this.OnBairroChanging(value);
	    	              this.RaiseDataMemberChanging("Bairro");
	    	              this._Bairro = value;
	    	              this.RaiseDataMemberChanged("Bairro");
	    	              this.OnBairroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For BandeiraRede
	    partial void OnBandeiraRedeChanging(System.Nullable<Int32> value);
	    partial void OnBandeiraRedeChanged();

	    private System.Nullable<Int32> _BandeiraRede;

	    [DataMember(Name = "BandeiraRede", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Bandeira Rede", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTbcBandeiraRedeLoja];LookUpTitle[Seleção de (Id Bandeira Rede)];LookUpQuery[executeLookUpTbcBandeiraRedeLoja];LookUpFinalize[finalizeLookUpTbcBandeiraRedeLoja];LookUpDisplayColumns[{\"IdBandeiraRede\" : \"Id Bandeira Rede\"}];LookUpColumns[{\"IdBandeiraRede\" : false}];FilterDataKey[0];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdBandeiraRede#true##12:0##Id Bandeira Rede#2#false##::LookUpTbcBandeiraRedeLoja##false#false##TBC_BANDEIRA_REDE#Linx.CadastroBase.BV.CadastroPfj#IQueryable###true#false", EdmKey="0")]
	    public System.Nullable<Int32> BandeiraRede
	    {
	    	    get
	    	    {
	    	          return _BandeiraRede;
	    	    }
	    	    set
	    	    {
	    	          if (this._BandeiraRede != value)
	    	          {
	    	              this.ValidateProperty("BandeiraRede", value);
	    	              this.OnBandeiraRedeChanging(value);
	    	              this.RaiseDataMemberChanging("BandeiraRede");
	    	              this._BandeiraRede = value;
	    	              this.RaiseDataMemberChanged("BandeiraRede");
	    	              this.OnBandeiraRedeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Cep
	    partial void OnCepChanging(String value);
	    partial void OnCepChanged();

	    private String _Cep;

	    [DataMember(Name = "Cep", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "CEP", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(10)]
	    [FunctionalPoint("Precision[10:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpWGeoEndereco];LookUpTitle[Seleção de (CEP)];LookUpQuery[executeLookUpWGeoEndereco];LookUpFinalize[finalizeLookUpWGeoEndereco];LookUpDisplayColumns[{\"Cep\" : \"CEP\", \"LxTipoLogradouro\" : \"Tipo do Logradouro\", \"Logradouro\" : \"Logradouro\", \"Bairro\" : \"Bairro\", \"DescMunicipio\" : \"Município\", \"SiglaUf\" : \"Sigla da UF\", \"DescPais\" : \"País\", \"Obs\" : \"Observação\"}];LookUpColumns[{\"Cep\" : true, \"LxTipoLogradouro\" : true, \"Logradouro\" : true, \"Bairro\" : true, \"DescMunicipio\" : true, \"SiglaUf\" : true, \"DescPais\" : true, \"Obs\" : true}];FilterDataKey[TBC_PFJ.CEP];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#Cep#false##90##CEP#0#true##::LookUpWGeoEndereco##false#false##W_GEO_ENDERECO#Linx.CadastroBase.BV.CadastroPfj#IQueryable#DescMunicipio,CodMunicipioIbge,IdMunicipio,InativoMunicipio[DescMunicipio,SiglaUf,DescUf,DescPais,CodMunicipioIbge,SiglaPais,IdMunicipio,IdPais,IdUf,InativoMunicipio,InativoUf,InativoPais];SiglaUf,DescUf,IdUf,InativoUf[SiglaUf,DescUf,DescPais,SiglaPais,IdPais,IdUf,InativoUf,InativoPais];DescPais,SiglaPais,IdPais,InativoPais[DescPais,SiglaPais,IdPais,InativoPais]#Cep[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];LxTipoLogradouro[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];Logradouro[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];Bairro[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];Municipio[SiglaUf=Uf,DescPais=Pais,IdPais=IdPais,IdUf=IdUf];Uf[DescPais=Pais,IdPais=IdPais];CodMunicipioIbge[SiglaUf=Uf,DescPais=Pais,IdPais=IdPais,IdUf=IdUf];IdCep[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];IdMunicipio[SiglaUf=Uf,DescPais=Pais,IdPais=IdPais,IdUf=IdUf];IdUf[DescPais=Pais,IdPais=IdPais];ObsEndereco[Cep=Cep,LxTipoLogradouro=LxTipoLogradouro,Logradouro=Logradouro,Bairro=Bairro,DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdCep=IdCep,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf]#true#false", EdmKey="TBC_PFJ.CEP")]
	    public String Cep
	    {
	    	    get
	    	    {
	    	          return _Cep;
	    	    }
	    	    set
	    	    {
	    	          if (this._Cep != value)
	    	          {
	    	              this.ValidateProperty("Cep", value);
	    	              this.OnCepChanging(value);
	    	              this.RaiseDataMemberChanging("Cep");
	    	              this._Cep = value;
	    	              this.RaiseDataMemberChanged("Cep");
	    	              this.OnCepChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CnpjCpf
	    partial void OnCnpjCpfChanging(String value);
	    partial void OnCnpjCpfChanged();

	    private String _CnpjCpf;

	    [DataMember(Name = "CnpjCpf", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "CNPJ", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_PFJ.CNPJ_CPF];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_PFJ.CNPJ_CPF")]
	    public String CnpjCpf
	    {
	    	    get
	    	    {
	    	          return _CnpjCpf;
	    	    }
	    	    set
	    	    {
	    	          if (this._CnpjCpf != value)
	    	          {
	    	              this.ValidateProperty("CnpjCpf", value);
	    	              this.OnCnpjCpfChanging(value);
	    	              this.RaiseDataMemberChanging("CnpjCpf");
	    	              this._CnpjCpf = value;
	    	              this.RaiseDataMemberChanged("CnpjCpf");
	    	              this.OnCnpjCpfChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CodDeposito
	    partial void OnCodDepositoChanging(String value);
	    partial void OnCodDepositoChanged();

	    private String _CodDeposito;

	    [DataMember(Name = "CodDeposito", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Código do Depósito", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(6)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[''];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="\"\"")]
	    public String CodDeposito
	    {
	    	    get
	    	    {
	    	          return _CodDeposito;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodDeposito != value)
	    	          {
	    	              this.ValidateProperty("CodDeposito", value);
	    	              this.OnCodDepositoChanging(value);
	    	              this.RaiseDataMemberChanging("CodDeposito");
	    	              this._CodDeposito = value;
	    	              this.RaiseDataMemberChanged("CodDeposito");
	    	              this.OnCodDepositoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CodigoFilial
	    partial void OnCodigoFilialChanging(String value);
	    partial void OnCodigoFilialChanged();

	    private String _CodigoFilial;

	    [DataMember(Name = "CodigoFilial", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Código Filial", Description="", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [LinxStringLength(18)]
	    [FunctionalPoint("Precision[18:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_PFJ.TBC_FILIAL_LISTA.CODIGO_FILIAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_PFJ.TBC_FILIAL_LISTA.CODIGO_FILIAL")]
	    public String CodigoFilial
	    {
	    	    get
	    	    {
	    	          return _CodigoFilial;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodigoFilial != value)
	    	          {
	    	              this.ValidateProperty("CodigoFilial", value);
	    	              this.OnCodigoFilialChanging(value);
	    	              this.RaiseDataMemberChanging("CodigoFilial");
	    	              this._CodigoFilial = value;
	    	              this.RaiseDataMemberChanged("CodigoFilial");
	    	              this.OnCodigoFilialChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For CodigoPfj
	    partial void OnCodigoPfjChanging(String value);
	    partial void OnCodigoPfjChanged();

	    private String _CodigoPfj;

	    [DataMember(Name = "CodigoPfj", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Código", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(18)]
	    [FunctionalPoint("Precision[18:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_PFJ.CODIGO_PFJ];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_PFJ.CODIGO_PFJ")]
	    public String CodigoPfj
	    {
	    	    get
	    	    {
	    	          return _CodigoPfj;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodigoPfj != value)
	    	          {
	    	              this.ValidateProperty("CodigoPfj", value);
	    	              this.OnCodigoPfjChanging(value);
	    	              this.RaiseDataMemberChanging("CodigoPfj");
	    	              this._CodigoPfj = value;
	    	              this.RaiseDataMemberChanged("CodigoPfj");
	    	              this.OnCodigoPfjChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Complemento
	    partial void OnComplementoChanging(String value);
	    partial void OnComplementoChanged();

	    private String _Complemento;

	    [DataMember(Name = "Complemento", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Complemento", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_PFJ.COMPLEMENTO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_PFJ.COMPLEMENTO")]
	    public String Complemento
	    {
	    	    get
	    	    {
	    	          return _Complemento;
	    	    }
	    	    set
	    	    {
	    	          if (this._Complemento != value)
	    	          {
	    	              this.ValidateProperty("Complemento", value);
	    	              this.OnComplementoChanging(value);
	    	              this.RaiseDataMemberChanging("Complemento");
	    	              this._Complemento = value;
	    	              this.RaiseDataMemberChanged("Complemento");
	    	              this.OnComplementoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DddCelular
	    partial void OnDddCelularChanging(String value);
	    partial void OnDddCelularChanged();

	    private String _DddCelular;

	    [DataMember(Name = "DddCelular", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ddd Celular", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(5)]
	    [FunctionalPoint("Precision[5:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_PFJ.DDD_CELULAR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_PFJ.DDD_CELULAR")]
	    public String DddCelular
	    {
	    	    get
	    	    {
	    	          return _DddCelular;
	    	    }
	    	    set
	    	    {
	    	          if (this._DddCelular != value)
	    	          {
	    	              this.ValidateProperty("DddCelular", value);
	    	              this.OnDddCelularChanging(value);
	    	              this.RaiseDataMemberChanging("DddCelular");
	    	              this._DddCelular = value;
	    	              this.RaiseDataMemberChanged("DddCelular");
	    	              this.OnDddCelularChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DddFixo
	    partial void OnDddFixoChanging(String value);
	    partial void OnDddFixoChanged();

	    private String _DddFixo;

	    [DataMember(Name = "DddFixo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ddd Fixo", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(5)]
	    [FunctionalPoint("Precision[5:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_PFJ.DDD_FIXO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_PFJ.DDD_FIXO")]
	    public String DddFixo
	    {
	    	    get
	    	    {
	    	          return _DddFixo;
	    	    }
	    	    set
	    	    {
	    	          if (this._DddFixo != value)
	    	          {
	    	              this.ValidateProperty("DddFixo", value);
	    	              this.OnDddFixoChanging(value);
	    	              this.RaiseDataMemberChanging("DddFixo");
	    	              this._DddFixo = value;
	    	              this.RaiseDataMemberChanged("DddFixo");
	    	              this.OnDddFixoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Email
	    partial void OnEmailChanging(String value);
	    partial void OnEmailChanged();

	    private String _Email;

	    [DataMember(Name = "Email", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "E-mail", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_PFJ.EMAIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_PFJ.EMAIL")]
	    public String Email
	    {
	    	    get
	    	    {
	    	          return _Email;
	    	    }
	    	    set
	    	    {
	    	          if (this._Email != value)
	    	          {
	    	              this.ValidateProperty("Email", value);
	    	              this.OnEmailChanging(value);
	    	              this.RaiseDataMemberChanging("Email");
	    	              this._Email = value;
	    	              this.RaiseDataMemberChanged("Email");
	    	              this.OnEmailChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For FoneCelular
	    partial void OnFoneCelularChanging(String value);
	    partial void OnFoneCelularChanged();

	    private String _FoneCelular;

	    [DataMember(Name = "FoneCelular", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Fone Celular", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_PFJ.FONE_CELULAR];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_PFJ.FONE_CELULAR")]
	    public String FoneCelular
	    {
	    	    get
	    	    {
	    	          return _FoneCelular;
	    	    }
	    	    set
	    	    {
	    	          if (this._FoneCelular != value)
	    	          {
	    	              this.ValidateProperty("FoneCelular", value);
	    	              this.OnFoneCelularChanging(value);
	    	              this.RaiseDataMemberChanging("FoneCelular");
	    	              this._FoneCelular = value;
	    	              this.RaiseDataMemberChanged("FoneCelular");
	    	              this.OnFoneCelularChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For FoneFixo
	    partial void OnFoneFixoChanging(String value);
	    partial void OnFoneFixoChanged();

	    private String _FoneFixo;

	    [DataMember(Name = "FoneFixo", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Fone Fixo", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_PFJ.FONE_FIXO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_PFJ.FONE_FIXO")]
	    public String FoneFixo
	    {
	    	    get
	    	    {
	    	          return _FoneFixo;
	    	    }
	    	    set
	    	    {
	    	          if (this._FoneFixo != value)
	    	          {
	    	              this.ValidateProperty("FoneFixo", value);
	    	              this.OnFoneFixoChanging(value);
	    	              this.RaiseDataMemberChanging("FoneFixo");
	    	              this._FoneFixo = value;
	    	              this.RaiseDataMemberChanged("FoneFixo");
	    	              this.OnFoneFixoChanged();
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
	    [Display(Name = "Id Filial PFJ", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_PFJ.TBC_FILIAL_LISTA.ID_FILIAL_PFJ];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_PFJ.TBC_FILIAL_LISTA.ID_FILIAL_PFJ")]
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
	    //Extensibility Partial Method Definitions For IdGpecon
	    partial void OnIdGpeconChanging(System.Nullable<Int32> value);
	    partial void OnIdGpeconChanged();

	    private System.Nullable<Int32> _IdGpecon;

	    [DataMember(Name = "IdGpecon", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Código Grupo Econômico", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTbcGrupoEconomicoFilial];LookUpTitle[Seleção de (Código Grupo Econômico)];LookUpQuery[executeLookUpTbcGrupoEconomicoFilial];LookUpFinalize[finalizeLookUpTbcGrupoEconomicoFilial];LookUpDisplayColumns[{\"IdGpecon\" : \"Código\"}];LookUpColumns[{\"IdGpecon\" : true}];FilterDataKey[TBC_PFJ.TBC_FILIAL_LISTA.TBC_GRUPO_ECONOMICO.ID_GPECON];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Int32>#IdGpecon#true##12:0##Código#0#true##::LookUpTbcGrupoEconomicoFilial##false#false#TBC_GRUPO_ECONOMICO#TBC_GRUPO_ECONOMICO#Linx.CadastroBase.BV.CadastroPfj#IQueryable###true#false", EdmKey="TBC_PFJ.TBC_FILIAL_LISTA.TBC_GRUPO_ECONOMICO.ID_GPECON")]
	    public System.Nullable<Int32> IdGpecon
	    {
	    	    get
	    	    {
	    	          return _IdGpecon;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdGpecon != value)
	    	          {
	    	              this.ValidateProperty("IdGpecon", value);
	    	              this.OnIdGpeconChanging(value);
	    	              this.RaiseDataMemberChanging("IdGpecon");
	    	              this._IdGpecon = value;
	    	              this.RaiseDataMemberChanged("IdGpecon");
	    	              this.OnIdGpeconChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLjvCanalVenda
	    partial void OnIdLjvCanalVendaChanging(System.Nullable<Int32> value);
	    partial void OnIdLjvCanalVendaChanged();

	    private System.Nullable<Int32> _IdLjvCanalVenda;

	    [DataMember(Name = "IdLjvCanalVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Loja Canal Venda", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpLjvCanalVendaLoja];LookUpTitle[Seleção de (Id Loja Canal Venda)];LookUpQuery[executeLookUpLjvCanalVendaLoja];LookUpFinalize[finalizeLookUpLjvCanalVendaLoja];LookUpDisplayColumns[{\"IdLjvCanalVenda\" : \"Id Ljv Canal Venda\"}];LookUpColumns[{\"IdLjvCanalVenda\" : false}];FilterDataKey[0];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdLjvCanalVenda#true##12:0##Id Ljv Canal Venda#2#false##::LookUpLjvCanalVendaLoja##false#false##LJV_CANAL_VENDA#Linx.CadastroBase.BV.CadastroPfj#IQueryable###true#false", EdmKey="0")]
	    public System.Nullable<Int32> IdLjvCanalVenda
	    {
	    	    get
	    	    {
	    	          return _IdLjvCanalVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLjvCanalVenda != value)
	    	          {
	    	              this.ValidateProperty("IdLjvCanalVenda", value);
	    	              this.OnIdLjvCanalVendaChanging(value);
	    	              this.RaiseDataMemberChanging("IdLjvCanalVenda");
	    	              this._IdLjvCanalVenda = value;
	    	              this.RaiseDataMemberChanged("IdLjvCanalVenda");
	    	              this.OnIdLjvCanalVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdMatrizContabil
	    partial void OnIdMatrizContabilChanging(System.Nullable<Int32> value);
	    partial void OnIdMatrizContabilChanged();

	    private System.Nullable<Int32> _IdMatrizContabil;

	    [DataMember(Name = "IdMatrizContabil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Filial PFJ", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[true];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpMatrizContabil];LookUpTitle[Seleção de (Id Filial PFJ)];LookUpQuery[executeLookUpMatrizContabil];LookUpFinalize[finalizeLookUpMatrizContabil];LookUpDisplayColumns[{\"IdMatrizContabil\" : \"Id Filial Pfj\"}];LookUpColumns[{\"IdMatrizContabil\" : false}];FilterDataKey[TBC_PFJ.TBC_FILIAL_LISTA.MATRIZ_CONTABIL.ID_FILIAL_PFJ];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<Int32>#IdMatrizContabil#true##12:0##Id Filial Pfj#1#false##::LookUpMatrizContabil##false#false#MATRIZ_CONTABIL#TBC_FILIAL#Linx.CadastroBase.BV.CadastroPfj#IQueryable###true#false", EdmKey="TBC_PFJ.TBC_FILIAL_LISTA.MATRIZ_CONTABIL.ID_FILIAL_PFJ")]
	    public System.Nullable<Int32> IdMatrizContabil
	    {
	    	    get
	    	    {
	    	          return _IdMatrizContabil;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdMatrizContabil != value)
	    	          {
	    	              this.ValidateProperty("IdMatrizContabil", value);
	    	              this.OnIdMatrizContabilChanging(value);
	    	              this.RaiseDataMemberChanging("IdMatrizContabil");
	    	              this._IdMatrizContabil = value;
	    	              this.RaiseDataMemberChanged("IdMatrizContabil");
	    	              this.OnIdMatrizContabilChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdPfj
	    partial void OnIdPfjChanging(Int32 value);
	    partial void OnIdPfjChanged();

	    private Int32 _IdPfj;

	    [DataMember(IsRequired = true, Name = "IdPfj", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id PFJ", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_PFJ.ID_PFJ];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_PFJ.ID_PFJ")]
	    public Int32 IdPfj
	    {
	    	    get
	    	    {
	    	          return _IdPfj;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdPfj != value)
	    	          {
	    	              this.ValidateProperty("IdPfj", value);
	    	              this.OnIdPfjChanging(value);
	    	              this.RaiseDataMemberChanging("IdPfj");
	    	              this._IdPfj = value;
	    	              this.RaiseDataMemberChanged("IdPfj");
	    	              this.OnIdPfjChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IncluiDeposito
	    partial void OnIncluiDepositoChanging(System.Nullable<Boolean> value);
	    partial void OnIncluiDepositoChanged();

	    private System.Nullable<Boolean> _IncluiDeposito;

	    [DataMember(Name = "IncluiDeposito", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Incluir Depósito", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[false];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="false")]
	    public System.Nullable<Boolean> IncluiDeposito
	    {
	    	    get
	    	    {
	    	          return _IncluiDeposito;
	    	    }
	    	    set
	    	    {
	    	          if (this._IncluiDeposito != value)
	    	          {
	    	              this.ValidateProperty("IncluiDeposito", value);
	    	              this.OnIncluiDepositoChanging(value);
	    	              this.RaiseDataMemberChanging("IncluiDeposito");
	    	              this._IncluiDeposito = value;
	    	              this.RaiseDataMemberChanged("IncluiDeposito");
	    	              this.OnIncluiDepositoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IncluiLoja
	    partial void OnIncluiLojaChanging(System.Nullable<Boolean> value);
	    partial void OnIncluiLojaChanged();

	    private System.Nullable<Boolean> _IncluiLoja;

	    [DataMember(Name = "IncluiLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Incluir Loja", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[false];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="false")]
	    public System.Nullable<Boolean> IncluiLoja
	    {
	    	    get
	    	    {
	    	          return _IncluiLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._IncluiLoja != value)
	    	          {
	    	              this.ValidateProperty("IncluiLoja", value);
	    	              this.OnIncluiLojaChanging(value);
	    	              this.RaiseDataMemberChanging("IncluiLoja");
	    	              this._IncluiLoja = value;
	    	              this.RaiseDataMemberChanged("IncluiLoja");
	    	              this.OnIncluiLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IndicaEstrangeiro
	    partial void OnIndicaEstrangeiroChanging(Boolean value);
	    partial void OnIndicaEstrangeiroChanged();

	    private Boolean _IndicaEstrangeiro;

	    [DataMember(IsRequired = true, Name = "IndicaEstrangeiro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Indica Estrangeiro", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_PFJ.INDICA_ESTRANGEIRO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_PFJ.INDICA_ESTRANGEIRO")]
	    public Boolean IndicaEstrangeiro
	    {
	    	    get
	    	    {
	    	          return _IndicaEstrangeiro;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaEstrangeiro != value)
	    	          {
	    	              this.ValidateProperty("IndicaEstrangeiro", value);
	    	              this.OnIndicaEstrangeiroChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaEstrangeiro");
	    	              this._IndicaEstrangeiro = value;
	    	              this.RaiseDataMemberChanged("IndicaEstrangeiro");
	    	              this.OnIndicaEstrangeiroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IndicaFilial
	    partial void OnIndicaFilialChanging(Boolean value);
	    partial void OnIndicaFilialChanged();

	    private Boolean _IndicaFilial;

	    [DataMember(IsRequired = true, Name = "IndicaFilial", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Indica Filial", Description="", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[false];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_PFJ.INDICA_FILIAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_PFJ.INDICA_FILIAL")]
	    public Boolean IndicaFilial
	    {
	    	    get
	    	    {
	    	          return _IndicaFilial;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaFilial != value)
	    	          {
	    	              this.ValidateProperty("IndicaFilial", value);
	    	              this.OnIndicaFilialChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaFilial");
	    	              this._IndicaFilial = value;
	    	              this.RaiseDataMemberChanged("IndicaFilial");
	    	              this.OnIndicaFilialChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IndicaLoja
	    partial void OnIndicaLojaChanging(System.Nullable<Boolean> value);
	    partial void OnIndicaLojaChanged();

	    private System.Nullable<Boolean> _IndicaLoja;

	    [DataMember(Name = "IndicaLoja", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Loja", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_PFJ.TBC_FILIAL_LISTA.INDICA_LOJA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_PFJ.TBC_FILIAL_LISTA.INDICA_LOJA")]
	    public System.Nullable<Boolean> IndicaLoja
	    {
	    	    get
	    	    {
	    	          return _IndicaLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaLoja != value)
	    	          {
	    	              this.ValidateProperty("IndicaLoja", value);
	    	              this.OnIndicaLojaChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaLoja");
	    	              this._IndicaLoja = value;
	    	              this.RaiseDataMemberChanged("IndicaLoja");
	    	              this.OnIndicaLojaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IndicaMatrizContabil
	    partial void OnIndicaMatrizContabilChanging(System.Nullable<Boolean> value);
	    partial void OnIndicaMatrizContabilChanged();

	    private System.Nullable<Boolean> _IndicaMatrizContabil;

	    [DataMember(Name = "IndicaMatrizContabil", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Matriz Contábil", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[CheckBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_PFJ.TBC_FILIAL_LISTA.INDICA_MATRIZ_CONTABIL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_PFJ.TBC_FILIAL_LISTA.INDICA_MATRIZ_CONTABIL")]
	    public System.Nullable<Boolean> IndicaMatrizContabil
	    {
	    	    get
	    	    {
	    	          return _IndicaMatrizContabil;
	    	    }
	    	    set
	    	    {
	    	          if (this._IndicaMatrizContabil != value)
	    	          {
	    	              this.ValidateProperty("IndicaMatrizContabil", value);
	    	              this.OnIndicaMatrizContabilChanging(value);
	    	              this.RaiseDataMemberChanging("IndicaMatrizContabil");
	    	              this._IndicaMatrizContabil = value;
	    	              this.RaiseDataMemberChanged("IndicaMatrizContabil");
	    	              this.OnIndicaMatrizContabilChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For InscrEstadual
	    partial void OnInscrEstadualChanging(String value);
	    partial void OnInscrEstadualChanged();

	    private String _InscrEstadual;

	    [DataMember(Name = "InscrEstadual", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inscrição Estadual", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(20)]
	    [FunctionalPoint("Precision[20:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_PFJ.INSCR_ESTADUAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_PFJ.INSCR_ESTADUAL")]
	    public String InscrEstadual
	    {
	    	    get
	    	    {
	    	          return _InscrEstadual;
	    	    }
	    	    set
	    	    {
	    	          if (this._InscrEstadual != value)
	    	          {
	    	              this.ValidateProperty("InscrEstadual", value);
	    	              this.OnInscrEstadualChanging(value);
	    	              this.RaiseDataMemberChanging("InscrEstadual");
	    	              this._InscrEstadual = value;
	    	              this.RaiseDataMemberChanged("InscrEstadual");
	    	              this.OnInscrEstadualChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Logradouro
	    partial void OnLogradouroChanging(String value);
	    partial void OnLogradouroChanged();

	    private String _Logradouro;

	    [DataMember(IsRequired = true, Name = "Logradouro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Logradouro", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpWGeoEndereco];LookUpTitle[Seleção de (Logradouro)];LookUpQuery[executeLookUpWGeoEndereco];LookUpFinalize[finalizeLookUpWGeoEndereco];LookUpDisplayColumns[{\"Cep\" : \"CEP\", \"LxTipoLogradouro\" : \"Tipo do Logradouro\", \"Logradouro\" : \"Logradouro\", \"Bairro\" : \"Bairro\", \"DescMunicipio\" : \"Município\", \"SiglaUf\" : \"Sigla da UF\", \"DescPais\" : \"País\", \"Obs\" : \"Observação\"}];LookUpColumns[{\"Cep\" : true, \"LxTipoLogradouro\" : true, \"Logradouro\" : true, \"Bairro\" : true, \"DescMunicipio\" : true, \"SiglaUf\" : true, \"DescPais\" : true, \"Obs\" : true}];FilterDataKey[TBC_PFJ.LOGRADOURO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#Logradouro#false##600##Logradouro#2#true##::LookUpWGeoEndereco##false#false##W_GEO_ENDERECO#Linx.CadastroBase.BV.CadastroPfj#IQueryable#DescMunicipio,CodMunicipioIbge,IdMunicipio,InativoMunicipio[DescMunicipio,SiglaUf,DescUf,DescPais,CodMunicipioIbge,SiglaPais,IdMunicipio,IdPais,IdUf,InativoMunicipio,InativoUf,InativoPais];SiglaUf,DescUf,IdUf,InativoUf[SiglaUf,DescUf,DescPais,SiglaPais,IdPais,IdUf,InativoUf,InativoPais];DescPais,SiglaPais,IdPais,InativoPais[DescPais,SiglaPais,IdPais,InativoPais]#Cep[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];LxTipoLogradouro[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];Logradouro[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];Bairro[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];Municipio[SiglaUf=Uf,DescPais=Pais,IdPais=IdPais,IdUf=IdUf];Uf[DescPais=Pais,IdPais=IdPais];CodMunicipioIbge[SiglaUf=Uf,DescPais=Pais,IdPais=IdPais,IdUf=IdUf];IdCep[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];IdMunicipio[SiglaUf=Uf,DescPais=Pais,IdPais=IdPais,IdUf=IdUf];IdUf[DescPais=Pais,IdPais=IdPais];ObsEndereco[Cep=Cep,LxTipoLogradouro=LxTipoLogradouro,Logradouro=Logradouro,Bairro=Bairro,DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdCep=IdCep,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf]#true#false", EdmKey="TBC_PFJ.LOGRADOURO")]
	    public String Logradouro
	    {
	    	    get
	    	    {
	    	          return _Logradouro;
	    	    }
	    	    set
	    	    {
	    	          if (this._Logradouro != value)
	    	          {
	    	              this.ValidateProperty("Logradouro", value);
	    	              this.OnLogradouroChanging(value);
	    	              this.RaiseDataMemberChanging("Logradouro");
	    	              this._Logradouro = value;
	    	              this.RaiseDataMemberChanged("Logradouro");
	    	              this.OnLogradouroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxPfjFisicaJuridica
	    partial void OnLxPfjFisicaJuridicaChanging(System.Nullable<Byte> value);
	    partial void OnLxPfjFisicaJuridicaChanged();

	    private System.Nullable<Byte> _LxPfjFisicaJuridica;

	    [DataMember(Name = "LxPfjFisicaJuridica", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Fisica Juridica", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LX_PFJ_FISICA_JURIDICA];KpiName[];KpiRelatedAttribute[];DefaultValue[2];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_PFJ.LX_PFJ_FISICA_JURIDICA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_PFJ.LX_PFJ_FISICA_JURIDICA")]
	    public System.Nullable<Byte> LxPfjFisicaJuridica
	    {
	    	    get
	    	    {
	    	          return _LxPfjFisicaJuridica;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxPfjFisicaJuridica != value)
	    	          {
	    	              this.ValidateProperty("LxPfjFisicaJuridica", value);
	    	              this.OnLxPfjFisicaJuridicaChanging(value);
	    	              this.RaiseDataMemberChanging("LxPfjFisicaJuridica");
	    	              this._LxPfjFisicaJuridica = value;
	    	              this.RaiseDataMemberChanged("LxPfjFisicaJuridica");
	    	              this.OnLxPfjFisicaJuridicaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For LxTipoLogradouro
	    partial void OnLxTipoLogradouroChanging(System.Nullable<Byte> value);
	    partial void OnLxTipoLogradouroChanged();

	    private System.Nullable<Byte> _LxTipoLogradouro;

	    [DataMember(Name = "LxTipoLogradouro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Logradouro", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[3:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[LxTipoLogradouro];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpWGeoEndereco];LookUpTitle[Seleção de (Tipo Logradouro)];LookUpQuery[executeLookUpWGeoEndereco];LookUpFinalize[finalizeLookUpWGeoEndereco];LookUpDisplayColumns[{\"Cep\" : \"CEP\", \"LxTipoLogradouro\" : \"Tipo do Logradouro\", \"Logradouro\" : \"Logradouro\", \"Bairro\" : \"Bairro\", \"DescMunicipio\" : \"Município\", \"SiglaUf\" : \"Sigla da UF\", \"DescPais\" : \"País\", \"Obs\" : \"Observação\"}];LookUpColumns[{\"Cep\" : true, \"LxTipoLogradouro\" : true, \"Logradouro\" : true, \"Bairro\" : true, \"DescMunicipio\" : true, \"SiglaUf\" : true, \"DescPais\" : true, \"Obs\" : true}];FilterDataKey[TBC_PFJ.LX_TIPO_LOGRADOURO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Nullable<System.Byte>#LxTipoLogradouro#false##30#LxTipoLogradouro#Tipo do Logradouro#1#true##::LookUpWGeoEndereco##false#false##W_GEO_ENDERECO#Linx.CadastroBase.BV.CadastroPfj#IQueryable#DescMunicipio,CodMunicipioIbge,IdMunicipio,InativoMunicipio[DescMunicipio,SiglaUf,DescUf,DescPais,CodMunicipioIbge,SiglaPais,IdMunicipio,IdPais,IdUf,InativoMunicipio,InativoUf,InativoPais];SiglaUf,DescUf,IdUf,InativoUf[SiglaUf,DescUf,DescPais,SiglaPais,IdPais,IdUf,InativoUf,InativoPais];DescPais,SiglaPais,IdPais,InativoPais[DescPais,SiglaPais,IdPais,InativoPais]#Cep[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];LxTipoLogradouro[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];Logradouro[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];Bairro[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];Municipio[SiglaUf=Uf,DescPais=Pais,IdPais=IdPais,IdUf=IdUf];Uf[DescPais=Pais,IdPais=IdPais];CodMunicipioIbge[SiglaUf=Uf,DescPais=Pais,IdPais=IdPais,IdUf=IdUf];IdCep[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];IdMunicipio[SiglaUf=Uf,DescPais=Pais,IdPais=IdPais,IdUf=IdUf];IdUf[DescPais=Pais,IdPais=IdPais];ObsEndereco[Cep=Cep,LxTipoLogradouro=LxTipoLogradouro,Logradouro=Logradouro,Bairro=Bairro,DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdCep=IdCep,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf]#true#false", EdmKey="TBC_PFJ.LX_TIPO_LOGRADOURO")]
	    public System.Nullable<Byte> LxTipoLogradouro
	    {
	    	    get
	    	    {
	    	          return _LxTipoLogradouro;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoLogradouro != value)
	    	          {
	    	              this.ValidateProperty("LxTipoLogradouro", value);
	    	              this.OnLxTipoLogradouroChanging(value);
	    	              this.RaiseDataMemberChanging("LxTipoLogradouro");
	    	              this._LxTipoLogradouro = value;
	    	              this.RaiseDataMemberChanged("LxTipoLogradouro");
	    	              this.OnLxTipoLogradouroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Municipio
	    partial void OnMunicipioChanging(String value);
	    partial void OnMunicipioChanged();

	    private String _Municipio;

	    [DataMember(Name = "Municipio", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Município", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpWGeoEndereco];LookUpTitle[Seleção de (Município)];LookUpQuery[executeLookUpWGeoEndereco];LookUpFinalize[finalizeLookUpWGeoEndereco];LookUpDisplayColumns[{\"Cep\" : \"CEP\", \"LxTipoLogradouro\" : \"Tipo do Logradouro\", \"Logradouro\" : \"Logradouro\", \"Bairro\" : \"Bairro\", \"DescMunicipio\" : \"Município\", \"SiglaUf\" : \"Sigla da UF\", \"DescPais\" : \"País\", \"Obs\" : \"Observação\"}];LookUpColumns[{\"Cep\" : true, \"LxTipoLogradouro\" : true, \"Logradouro\" : true, \"Bairro\" : true, \"DescMunicipio\" : true, \"SiglaUf\" : true, \"DescPais\" : true, \"Obs\" : true}];FilterDataKey[TBC_PFJ.MUNICIPIO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescMunicipio#false##600##Município#4#true##::LookUpWGeoEndereco##false#false##W_GEO_ENDERECO#Linx.CadastroBase.BV.CadastroPfj#IQueryable#DescMunicipio,CodMunicipioIbge,IdMunicipio,InativoMunicipio[DescMunicipio,SiglaUf,DescUf,DescPais,CodMunicipioIbge,SiglaPais,IdMunicipio,IdPais,IdUf,InativoMunicipio,InativoUf,InativoPais];SiglaUf,DescUf,IdUf,InativoUf[SiglaUf,DescUf,DescPais,SiglaPais,IdPais,IdUf,InativoUf,InativoPais];DescPais,SiglaPais,IdPais,InativoPais[DescPais,SiglaPais,IdPais,InativoPais]#Cep[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];LxTipoLogradouro[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];Logradouro[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];Bairro[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];Municipio[SiglaUf=Uf,DescPais=Pais,IdPais=IdPais,IdUf=IdUf];Uf[DescPais=Pais,IdPais=IdPais];CodMunicipioIbge[SiglaUf=Uf,DescPais=Pais,IdPais=IdPais,IdUf=IdUf];IdCep[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];IdMunicipio[SiglaUf=Uf,DescPais=Pais,IdPais=IdPais,IdUf=IdUf];IdUf[DescPais=Pais,IdPais=IdPais];ObsEndereco[Cep=Cep,LxTipoLogradouro=LxTipoLogradouro,Logradouro=Logradouro,Bairro=Bairro,DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdCep=IdCep,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf]#true#false", EdmKey="TBC_PFJ.MUNICIPIO")]
	    public String Municipio
	    {
	    	    get
	    	    {
	    	          return _Municipio;
	    	    }
	    	    set
	    	    {
	    	          if (this._Municipio != value)
	    	          {
	    	              this.ValidateProperty("Municipio", value);
	    	              this.OnMunicipioChanging(value);
	    	              this.RaiseDataMemberChanging("Municipio");
	    	              this._Municipio = value;
	    	              this.RaiseDataMemberChanged("Municipio");
	    	              this.OnMunicipioChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeFantasiaApelido
	    partial void OnNomeFantasiaApelidoChanging(String value);
	    partial void OnNomeFantasiaApelidoChanged();

	    private String _NomeFantasiaApelido;

	    [DataMember(Name = "NomeFantasiaApelido", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Fantasia", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_PFJ.NOME_FANTASIA_APELIDO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_PFJ.NOME_FANTASIA_APELIDO")]
	    public String NomeFantasiaApelido
	    {
	    	    get
	    	    {
	    	          return _NomeFantasiaApelido;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeFantasiaApelido != value)
	    	          {
	    	              this.ValidateProperty("NomeFantasiaApelido", value);
	    	              this.OnNomeFantasiaApelidoChanging(value);
	    	              this.RaiseDataMemberChanging("NomeFantasiaApelido");
	    	              this._NomeFantasiaApelido = value;
	    	              this.RaiseDataMemberChanged("NomeFantasiaApelido");
	    	              this.OnNomeFantasiaApelidoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For NomeFilial
	    partial void OnNomeFilialChanging(String value);
	    partial void OnNomeFilialChanged();

	    private String _NomeFilial;

	    [DataMember(Name = "NomeFilial", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Filial", Description="", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[false];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_PFJ.TBC_FILIAL_LISTA.NOME_FILIAL];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_PFJ.TBC_FILIAL_LISTA.NOME_FILIAL")]
	    public String NomeFilial
	    {
	    	    get
	    	    {
	    	          return _NomeFilial;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeFilial != value)
	    	          {
	    	              this.ValidateProperty("NomeFilial", value);
	    	              this.OnNomeFilialChanging(value);
	    	              this.RaiseDataMemberChanging("NomeFilial");
	    	              this._NomeFilial = value;
	    	              this.RaiseDataMemberChanged("NomeFilial");
	    	              this.OnNomeFilialChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Numero
	    partial void OnNumeroChanging(String value);
	    partial void OnNumeroChanged();

	    private String _Numero;

	    [DataMember(Name = "Numero", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Número", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_PFJ.NUMERO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_PFJ.NUMERO")]
	    public String Numero
	    {
	    	    get
	    	    {
	    	          return _Numero;
	    	    }
	    	    set
	    	    {
	    	          if (this._Numero != value)
	    	          {
	    	              this.ValidateProperty("Numero", value);
	    	              this.OnNumeroChanging(value);
	    	              this.RaiseDataMemberChanging("Numero");
	    	              this._Numero = value;
	    	              this.RaiseDataMemberChanged("Numero");
	    	              this.OnNumeroChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For ObsEndereco
	    partial void OnObsEnderecoChanging(String value);
	    partial void OnObsEnderecoChanged();

	    private String _ObsEndereco;

	    [DataMember(Name = "ObsEndereco", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Obs. Endereço", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(250)]
	    [FunctionalPoint("Precision[250:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpWGeoEndereco];LookUpTitle[Seleção de (Obs. Endereço)];LookUpQuery[executeLookUpWGeoEndereco];LookUpFinalize[finalizeLookUpWGeoEndereco];LookUpDisplayColumns[{\"Cep\" : \"CEP\", \"LxTipoLogradouro\" : \"Tipo do Logradouro\", \"Logradouro\" : \"Logradouro\", \"Bairro\" : \"Bairro\", \"DescMunicipio\" : \"Município\", \"SiglaUf\" : \"Sigla da UF\", \"DescPais\" : \"País\", \"Obs\" : \"Observação\"}];LookUpColumns[{\"Cep\" : true, \"LxTipoLogradouro\" : true, \"Logradouro\" : true, \"Bairro\" : true, \"DescMunicipio\" : true, \"SiglaUf\" : true, \"DescPais\" : true, \"Obs\" : true}];FilterDataKey[TBC_PFJ.OBS_ENDERECO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#Obs#false##1500##Observação#18#true##::LookUpWGeoEndereco##false#false##W_GEO_ENDERECO#Linx.CadastroBase.BV.CadastroPfj#IQueryable#DescMunicipio,CodMunicipioIbge,IdMunicipio,InativoMunicipio[DescMunicipio,SiglaUf,DescUf,DescPais,CodMunicipioIbge,SiglaPais,IdMunicipio,IdPais,IdUf,InativoMunicipio,InativoUf,InativoPais];SiglaUf,DescUf,IdUf,InativoUf[SiglaUf,DescUf,DescPais,SiglaPais,IdPais,IdUf,InativoUf,InativoPais];DescPais,SiglaPais,IdPais,InativoPais[DescPais,SiglaPais,IdPais,InativoPais]#Cep[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];LxTipoLogradouro[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];Logradouro[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];Bairro[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];Municipio[SiglaUf=Uf,DescPais=Pais,IdPais=IdPais,IdUf=IdUf];Uf[DescPais=Pais,IdPais=IdPais];CodMunicipioIbge[SiglaUf=Uf,DescPais=Pais,IdPais=IdPais,IdUf=IdUf];IdCep[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];IdMunicipio[SiglaUf=Uf,DescPais=Pais,IdPais=IdPais,IdUf=IdUf];IdUf[DescPais=Pais,IdPais=IdPais];ObsEndereco[Cep=Cep,LxTipoLogradouro=LxTipoLogradouro,Logradouro=Logradouro,Bairro=Bairro,DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdCep=IdCep,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf]#true#false", EdmKey="TBC_PFJ.OBS_ENDERECO")]
	    public String ObsEndereco
	    {
	    	    get
	    	    {
	    	          return _ObsEndereco;
	    	    }
	    	    set
	    	    {
	    	          if (this._ObsEndereco != value)
	    	          {
	    	              this.ValidateProperty("ObsEndereco", value);
	    	              this.OnObsEnderecoChanging(value);
	    	              this.RaiseDataMemberChanging("ObsEndereco");
	    	              this._ObsEndereco = value;
	    	              this.RaiseDataMemberChanged("ObsEndereco");
	    	              this.OnObsEnderecoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Pais
	    partial void OnPaisChanging(String value);
	    partial void OnPaisChanged();

	    private String _Pais;

	    [DataMember(Name = "Pais", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "País", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpWGeoEndereco];LookUpTitle[Seleção de (País)];LookUpQuery[executeLookUpWGeoEndereco];LookUpFinalize[finalizeLookUpWGeoEndereco];LookUpDisplayColumns[{\"Cep\" : \"CEP\", \"LxTipoLogradouro\" : \"Tipo do Logradouro\", \"Logradouro\" : \"Logradouro\", \"Bairro\" : \"Bairro\", \"DescMunicipio\" : \"Município\", \"SiglaUf\" : \"Sigla da UF\", \"DescPais\" : \"País\", \"Obs\" : \"Observação\"}];LookUpColumns[{\"Cep\" : true, \"LxTipoLogradouro\" : true, \"Logradouro\" : true, \"Bairro\" : true, \"DescMunicipio\" : true, \"SiglaUf\" : true, \"DescPais\" : true, \"Obs\" : true}];FilterDataKey[TBC_PFJ.PAIS];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescPais#false##600##País#7#true##::LookUpWGeoEndereco##false#false##W_GEO_ENDERECO#Linx.CadastroBase.BV.CadastroPfj#IQueryable#DescMunicipio,CodMunicipioIbge,IdMunicipio,InativoMunicipio[DescMunicipio,SiglaUf,DescUf,DescPais,CodMunicipioIbge,SiglaPais,IdMunicipio,IdPais,IdUf,InativoMunicipio,InativoUf,InativoPais];SiglaUf,DescUf,IdUf,InativoUf[SiglaUf,DescUf,DescPais,SiglaPais,IdPais,IdUf,InativoUf,InativoPais];DescPais,SiglaPais,IdPais,InativoPais[DescPais,SiglaPais,IdPais,InativoPais]#Cep[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];LxTipoLogradouro[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];Logradouro[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];Bairro[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];Municipio[SiglaUf=Uf,DescPais=Pais,IdPais=IdPais,IdUf=IdUf];Uf[DescPais=Pais,IdPais=IdPais];CodMunicipioIbge[SiglaUf=Uf,DescPais=Pais,IdPais=IdPais,IdUf=IdUf];IdCep[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];IdMunicipio[SiglaUf=Uf,DescPais=Pais,IdPais=IdPais,IdUf=IdUf];IdUf[DescPais=Pais,IdPais=IdPais];ObsEndereco[Cep=Cep,LxTipoLogradouro=LxTipoLogradouro,Logradouro=Logradouro,Bairro=Bairro,DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdCep=IdCep,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf]#true#false", EdmKey="TBC_PFJ.PAIS")]
	    public String Pais
	    {
	    	    get
	    	    {
	    	          return _Pais;
	    	    }
	    	    set
	    	    {
	    	          if (this._Pais != value)
	    	          {
	    	              this.ValidateProperty("Pais", value);
	    	              this.OnPaisChanging(value);
	    	              this.RaiseDataMemberChanging("Pais");
	    	              this._Pais = value;
	    	              this.RaiseDataMemberChanged("Pais");
	    	              this.OnPaisChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For RazaoSocialNomeCompleto
	    partial void OnRazaoSocialNomeCompletoChanging(String value);
	    partial void OnRazaoSocialNomeCompletoChanged();

	    private String _RazaoSocialNomeCompleto;

	    [DataMember(Name = "RazaoSocialNomeCompleto", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Razão Social Nome Completo", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(120)]
	    [FunctionalPoint("Precision[120:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_PFJ.RAZAO_SOCIAL_NOME_COMPLETO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_PFJ.RAZAO_SOCIAL_NOME_COMPLETO")]
	    public String RazaoSocialNomeCompleto
	    {
	    	    get
	    	    {
	    	          return _RazaoSocialNomeCompleto;
	    	    }
	    	    set
	    	    {
	    	          if (this._RazaoSocialNomeCompleto != value)
	    	          {
	    	              this.ValidateProperty("RazaoSocialNomeCompleto", value);
	    	              this.OnRazaoSocialNomeCompletoChanging(value);
	    	              this.RaiseDataMemberChanging("RazaoSocialNomeCompleto");
	    	              this._RazaoSocialNomeCompleto = value;
	    	              this.RaiseDataMemberChanged("RazaoSocialNomeCompleto");
	    	              this.OnRazaoSocialNomeCompletoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For Uf
	    partial void OnUfChanging(String value);
	    partial void OnUfChanged();

	    private String _Uf;

	    [DataMember(Name = "Uf", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "UF", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(4)]
	    [FunctionalPoint("Precision[4:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[true];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpWGeoEndereco];LookUpTitle[Seleção de (UF)];LookUpQuery[executeLookUpWGeoEndereco];LookUpFinalize[finalizeLookUpWGeoEndereco];LookUpDisplayColumns[{\"Cep\" : \"CEP\", \"LxTipoLogradouro\" : \"Tipo do Logradouro\", \"Logradouro\" : \"Logradouro\", \"Bairro\" : \"Bairro\", \"DescMunicipio\" : \"Município\", \"SiglaUf\" : \"Sigla da UF\", \"DescPais\" : \"País\", \"Obs\" : \"Observação\"}];LookUpColumns[{\"Cep\" : true, \"LxTipoLogradouro\" : true, \"Logradouro\" : true, \"Bairro\" : true, \"DescMunicipio\" : true, \"SiglaUf\" : true, \"DescPais\" : true, \"Obs\" : true}];FilterDataKey[TBC_PFJ.UF];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#SiglaUf#false##100##Sigla da UF#5#true##::LookUpWGeoEndereco##false#false##W_GEO_ENDERECO#Linx.CadastroBase.BV.CadastroPfj#IQueryable#DescMunicipio,CodMunicipioIbge,IdMunicipio,InativoMunicipio[DescMunicipio,SiglaUf,DescUf,DescPais,CodMunicipioIbge,SiglaPais,IdMunicipio,IdPais,IdUf,InativoMunicipio,InativoUf,InativoPais];SiglaUf,DescUf,IdUf,InativoUf[SiglaUf,DescUf,DescPais,SiglaPais,IdPais,IdUf,InativoUf,InativoPais];DescPais,SiglaPais,IdPais,InativoPais[DescPais,SiglaPais,IdPais,InativoPais]#Cep[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];LxTipoLogradouro[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];Logradouro[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];Bairro[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];Municipio[SiglaUf=Uf,DescPais=Pais,IdPais=IdPais,IdUf=IdUf];Uf[DescPais=Pais,IdPais=IdPais];CodMunicipioIbge[SiglaUf=Uf,DescPais=Pais,IdPais=IdPais,IdUf=IdUf];IdCep[DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf];IdMunicipio[SiglaUf=Uf,DescPais=Pais,IdPais=IdPais,IdUf=IdUf];IdUf[DescPais=Pais,IdPais=IdPais];ObsEndereco[Cep=Cep,LxTipoLogradouro=LxTipoLogradouro,Logradouro=Logradouro,Bairro=Bairro,DescMunicipio=Municipio,SiglaUf=Uf,DescPais=Pais,CodMunicipioIbge=CodMunicipioIbge,IdCep=IdCep,IdMunicipio=IdMunicipio,IdPais=IdPais,IdUf=IdUf]#true#false", EdmKey="TBC_PFJ.UF")]
	    public String Uf
	    {
	    	    get
	    	    {
	    	          return _Uf;
	    	    }
	    	    set
	    	    {
	    	          if (this._Uf != value)
	    	          {
	    	              this.ValidateProperty("Uf", value);
	    	              this.OnUfChanging(value);
	    	              this.RaiseDataMemberChanging("Uf");
	    	              this._Uf = value;
	    	              this.RaiseDataMemberChanged("Uf");
	    	              this.OnUfChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdPfj;
	    [DataMember(Name = "TemporaryIdPfj", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id PFJ (Tmp)", Description="Temporary Key", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdPfj
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdPfj.IsNullOrEmpty())
	    	                this._TemporaryIdPfj = this._IdPfj;
	    	          return this._TemporaryIdPfj;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdPfj != value)
	    	              this._TemporaryIdPfj = value;
	    	    }
	    }	

	    #endregion Data Properties

		
	    #region Change State Control
	 
	

	    #endregion Change State Control

	    #region Media Storage	
	 
	



	    #endregion Media Storage

	    #region Special Enums	
	 

	    public Dictionary<string, string> GetLxPfjFisicaJuridicaValues()
	    {
	    	    return Linx.Framework.Setup.Domains.LX_PFJ_FISICA_JURIDICA.GetValues();
	    }
	    private string _lxPfjFisicaJuridicaName;
	    [DataMember(IsRequired = false, Name = "LxPfjFisicaJuridicaName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Fisica Juridica", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxPfjFisicaJuridicaName
	    {
	    	    get { if (this.LxPfjFisicaJuridica.IsNull()) { _lxPfjFisicaJuridicaName = String.Empty; } else { string key = this.LxPfjFisicaJuridica.ToString(); var dmValues = this.GetLxPfjFisicaJuridicaValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxPfjFisicaJuridicaName) _lxPfjFisicaJuridicaName = domainName; } return _lxPfjFisicaJuridicaName; } set { _lxPfjFisicaJuridicaName = value;  }
	    }
	    public Dictionary<string, string> GetLxTipoLogradouroValues()
	    {
	    	    return Linx.Framework.Setup.Domains.LxTipoLogradouro.GetValues();
	    }
	    private string _lxTipoLogradouroName;
	    [DataMember(IsRequired = false, Name = "LxTipoLogradouroName", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(false)]
	    [Display(Name = "Tipo Logradouro", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[ComboBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[true];FilterDataKey[1]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
	    public string LxTipoLogradouroName
	    {
	    	    get { if (this.LxTipoLogradouro.IsNull()) { _lxTipoLogradouroName = String.Empty; } else { string key = this.LxTipoLogradouro.ToString(); var dmValues = this.GetLxTipoLogradouroValues(); string domainName = (dmValues.ContainsKey(key) ? dmValues[key] : String.Empty); if (domainName != _lxTipoLogradouroName) _lxTipoLogradouroName = domainName; } return _lxTipoLogradouroName; } set { _lxTipoLogradouroName = value;  }
	    }	

	    #endregion Special Enums
	
	}	
	
		
	///////////////////////////////////////////////////////////////////////
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TBC_GRUPO_ECONOMICO.ID_GPECON", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TbcGrupoEconomico];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdGpeconCadastro];ReadOnly[false];Entities[:IdGpeconCadastro];SubQueryInfo[];EdmEntityName[];EntityRelations[ ];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TbcGrupoEconomico")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.Setup.LinxAutoSetup.TbcGrupoEconomico")]
	public partial class TbcGrupoEconomico : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For DescGrupoEconomico
	    partial void OnDescGrupoEconomicoChanging(String value);
	    partial void OnDescGrupoEconomicoChanged();

	    private String _DescGrupoEconomico;

	    [DataMember(IsRequired = true, Name = "DescGrupoEconomico", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Grupo Econômico", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTbcGrupoEconomico];LookUpTitle[Seleção de (Grupo Econômico)];LookUpQuery[executeLookUpTbcGrupoEconomico];LookUpFinalize[finalizeLookUpTbcGrupoEconomico];LookUpDisplayColumns[{\"IdGpecon\" : \"Código do Grupo Econômico\", \"DescGrupoEconomico\" : \"Grupo Econômico\"}];LookUpColumns[{\"IdGpecon\" : false, \"DescGrupoEconomico\" : true}];FilterDataKey[TBC_GRUPO_ECONOMICO.DESC_GRUPO_ECONOMICO];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescGrupoEconomico#false##600##Grupo Econômico#1#true##::LookUpTbcGrupoEconomico##false#false##TBC_GRUPO_ECONOMICO#Linx.Operacional.CadastroBase.BV.GrupoEconomico#IQueryable###true#true", EdmKey="TBC_GRUPO_ECONOMICO.DESC_GRUPO_ECONOMICO")]
	    public String DescGrupoEconomico
	    {
	    	    get
	    	    {
	    	          return _DescGrupoEconomico;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescGrupoEconomico != value)
	    	          {
	    	              this.ValidateProperty("DescGrupoEconomico", value);
	    	              this.OnDescGrupoEconomicoChanging(value);
	    	              this.RaiseDataMemberChanging("DescGrupoEconomico");
	    	              this._DescGrupoEconomico = value;
	    	              this.RaiseDataMemberChanged("DescGrupoEconomico");
	    	              this.OnDescGrupoEconomicoChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdGpeconCadastro
	    partial void OnIdGpeconCadastroChanging(Int32 value);
	    partial void OnIdGpeconCadastroChanged();

	    private Int32 _IdGpeconCadastro;

	    [DataMember(IsRequired = true, Name = "IdGpeconCadastro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Código do Grupo Econômico", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[LookUpTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];LookUpName[LookUpTbcGrupoEconomico];LookUpTitle[Seleção de (Código do Grupo Econômico)];LookUpQuery[executeLookUpTbcGrupoEconomico];LookUpFinalize[finalizeLookUpTbcGrupoEconomico];LookUpDisplayColumns[{\"IdGpecon\" : \"Código do Grupo Econômico\", \"DescGrupoEconomico\" : \"Grupo Econômico\"}];LookUpColumns[{\"IdGpecon\" : false, \"DescGrupoEconomico\" : true}];FilterDataKey[TBC_GRUPO_ECONOMICO.ID_GPECON];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int32#IdGpecon#true##12:0##Código do Grupo Econômico#0#false##::LookUpTbcGrupoEconomico##false#false##TBC_GRUPO_ECONOMICO#Linx.Operacional.CadastroBase.BV.GrupoEconomico#IQueryable###true#true", EdmKey="TBC_GRUPO_ECONOMICO.ID_GPECON")]
	    public Int32 IdGpeconCadastro
	    {
	    	    get
	    	    {
	    	          return _IdGpeconCadastro;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdGpeconCadastro != value)
	    	          {
	    	              this.ValidateProperty("IdGpeconCadastro", value);
	    	              this.OnIdGpeconCadastroChanging(value);
	    	              this.RaiseDataMemberChanging("IdGpeconCadastro");
	    	              this._IdGpeconCadastro = value;
	    	              this.RaiseDataMemberChanged("IdGpeconCadastro");
	    	              this.OnIdGpeconCadastroChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdGpeconCadastro;
	    [DataMember(Name = "TemporaryIdGpeconCadastro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Código do Grupo Econômico (Tmp)", Description="Temporary Key", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdGpeconCadastro
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdGpeconCadastro.IsNullOrEmpty())
	    	                this._TemporaryIdGpeconCadastro = this._IdGpeconCadastro;
	    	          return this._TemporaryIdGpeconCadastro;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdGpeconCadastro != value)
	    	              this._TemporaryIdGpeconCadastro = value;
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
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[TbcBandeiraRede];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdBandeiraRedeCadastro];ReadOnly[false];Entities[:IdBandeiraRedeCadastro];SubQueryInfo[];EdmEntityName[];EntityRelations[ ];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "TbcBandeiraRede")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.Setup.LinxAutoSetup.TbcBandeiraRede")]
	public partial class TbcBandeiraRede : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For CodBandeiraRede
	    partial void OnCodBandeiraRedeChanging(String value);
	    partial void OnCodBandeiraRedeChanged();

	    private String _CodBandeiraRede;

	    [DataMember(IsRequired = true, Name = "CodBandeiraRede", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Código da Bandeira / Rede", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(25)]
	    [FunctionalPoint("Precision[25:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_BANDEIRA_REDE.COD_BANDEIRA_REDE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_BANDEIRA_REDE.COD_BANDEIRA_REDE")]
	    public String CodBandeiraRede
	    {
	    	    get
	    	    {
	    	          return _CodBandeiraRede;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodBandeiraRede != value)
	    	          {
	    	              this.ValidateProperty("CodBandeiraRede", value);
	    	              this.OnCodBandeiraRedeChanging(value);
	    	              this.RaiseDataMemberChanging("CodBandeiraRede");
	    	              this._CodBandeiraRede = value;
	    	              this.RaiseDataMemberChanged("CodBandeiraRede");
	    	              this.OnCodBandeiraRedeChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdBandeiraRedeCadastro
	    partial void OnIdBandeiraRedeCadastroChanging(Int32 value);
	    partial void OnIdBandeiraRedeCadastroChanged();

	    private Int32 _IdBandeiraRedeCadastro;

	    [DataMember(IsRequired = true, Name = "IdBandeiraRedeCadastro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id.", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE")]
	    public Int32 IdBandeiraRedeCadastro
	    {
	    	    get
	    	    {
	    	          return _IdBandeiraRedeCadastro;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdBandeiraRedeCadastro != value)
	    	          {
	    	              this.ValidateProperty("IdBandeiraRedeCadastro", value);
	    	              this.OnIdBandeiraRedeCadastroChanging(value);
	    	              this.RaiseDataMemberChanging("IdBandeiraRedeCadastro");
	    	              this._IdBandeiraRedeCadastro = value;
	    	              this.RaiseDataMemberChanged("IdBandeiraRedeCadastro");
	    	              this.OnIdBandeiraRedeCadastroChanged();
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
	    [Display(Name = "Id Linx", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[TBC_BANDEIRA_REDE.ID_LINX];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="TBC_BANDEIRA_REDE.ID_LINX")]
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

	    private Int32 _TemporaryIdBandeiraRedeCadastro;
	    [DataMember(Name = "TemporaryIdBandeiraRedeCadastro", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id. (Tmp)", Description="Temporary Key", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdBandeiraRedeCadastro
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdBandeiraRedeCadastro.IsNullOrEmpty())
	    	                this._TemporaryIdBandeiraRedeCadastro = this._IdBandeiraRedeCadastro;
	    	          return this._TemporaryIdBandeiraRedeCadastro;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdBandeiraRedeCadastro != value)
	    	              this._TemporaryIdBandeiraRedeCadastro = value;
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
	////////////////////////// Data Class Definition //////////////////////
	///////////////////////////////////////////////////////////////////////		

		

	[LinxPublicationView(PrimaryKeys="LJV_CANAL_VENDA.ID_LJV_CANAL_VENDA", IsUpdatable=false, EdmName="")]
	
	[FunctionalPoint("ClassDescription[ ];DisplayName[];SizeGridConfigurations[];ReplicationKey[];CompositionHierarchy[LjvCanalVenda];IsOlap[false];OlapCatalogName[];CubeName[];IsAggregationView[false];ForceAggregationPaging[false];HasLocalResultEntityAdapters[false];TemporaryKeyName[IdLjvCanalVenda];ReadOnly[false];Entities[:IdLjvCanalVenda];SubQueryInfo[];EdmEntityName[];EntityRelations[ ];EdmParentEntityName[];IsIQueryable[true]")]
		
	[DataContract(IsReference = false, Name = "LjvCanalVenda")]
	[Serializable()]
	[Export(typeof(object))]
	[ExportMetadata("ImplementationName", "Linx.Framework.Setup.LinxAutoSetup.LjvCanalVenda")]
	public partial class LjvCanalVenda : Linx.Data.Entity
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
	 

	    //Extensibility Partial Method Definitions For CodCanalVenda
	    partial void OnCodCanalVendaChanging(String value);
	    partial void OnCodCanalVendaChanged();

	    private String _CodCanalVenda;

	    [DataMember(IsRequired = true, Name = "CodCanalVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Código do Canal Venda", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(6)]
	    [FunctionalPoint("Precision[6:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_CANAL_VENDA.COD_CANAL_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_CANAL_VENDA.COD_CANAL_VENDA")]
	    public String CodCanalVenda
	    {
	    	    get
	    	    {
	    	          return _CodCanalVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodCanalVenda != value)
	    	          {
	    	              this.ValidateProperty("CodCanalVenda", value);
	    	              this.OnCodCanalVendaChanging(value);
	    	              this.RaiseDataMemberChanging("CodCanalVenda");
	    	              this._CodCanalVenda = value;
	    	              this.RaiseDataMemberChanged("CodCanalVenda");
	    	              this.OnCodCanalVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For DescCanalVenda
	    partial void OnDescCanalVendaChanging(String value);
	    partial void OnDescCanalVendaChanged();

	    private String _DescCanalVenda;

	    [DataMember(IsRequired = true, Name = "DescCanalVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Canal de Venda", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [LinxStringLength(60)]
	    [FunctionalPoint("Precision[60:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[false];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[TextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_CANAL_VENDA.DESC_CANAL_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_CANAL_VENDA.DESC_CANAL_VENDA")]
	    public String DescCanalVenda
	    {
	    	    get
	    	    {
	    	          return _DescCanalVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescCanalVenda != value)
	    	          {
	    	              this.ValidateProperty("DescCanalVenda", value);
	    	              this.OnDescCanalVendaChanging(value);
	    	              this.RaiseDataMemberChanging("DescCanalVenda");
	    	              this._DescCanalVenda = value;
	    	              this.RaiseDataMemberChanged("DescCanalVenda");
	    	              this.OnDescCanalVendaChanged();
	    	          }
	    	    }
	    }
	    //Extensibility Partial Method Definitions For IdLjvCanalVenda
	    partial void OnIdLjvCanalVendaChanging(Int32 value);
	    partial void OnIdLjvCanalVendaChanged();

	    private Int32 _IdLjvCanalVenda;

	    [DataMember(IsRequired = true, Name = "IdLjvCanalVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Ljv Canal Venda", Description="", Order = -1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [Key()]
	    [FunctionalPoint("Precision[12:0];IsEditable[true];CustomMediaTable[];IsAutomaticSequency[true];IsNull[false];DomainName[];KpiName[];KpiRelatedAttribute[];DefaultValue[];DataFormatString[];OrderByOrientation[Ascending];OrderBySequence[-1];AggregationFunction[None];ObjectClass[NumericTextBox];ConnectedField[];Mask[];MaskType[];ExcludedAsFilter[false];FilterDataKey[LJV_CANAL_VENDA.ID_LJV_CANAL_VENDA];IsMeasure[false]")]
	    [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="LJV_CANAL_VENDA.ID_LJV_CANAL_VENDA")]
	    public Int32 IdLjvCanalVenda
	    {
	    	    get
	    	    {
	    	          return _IdLjvCanalVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLjvCanalVenda != value)
	    	          {
	    	              this.ValidateProperty("IdLjvCanalVenda", value);
	    	              this.OnIdLjvCanalVendaChanging(value);
	    	              this.RaiseDataMemberChanging("IdLjvCanalVenda");
	    	              this._IdLjvCanalVenda = value;
	    	              this.RaiseDataMemberChanged("IdLjvCanalVenda");
	    	              this.OnIdLjvCanalVendaChanged();
	    	          }
	    	    }
	    }

	    private Int32 _TemporaryIdLjvCanalVenda;
	    [DataMember(Name = "TemporaryIdLjvCanalVenda", EmitDefaultValue = true)]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Ljv Canal Venda (Tmp)", Description="Temporary Key", Order = -1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    public Int32 TemporaryIdLjvCanalVenda
	    {
	    	    get
	    	    {
	    	          if (this._TemporaryIdLjvCanalVenda.IsNullOrEmpty())
	    	                this._TemporaryIdLjvCanalVenda = this._IdLjvCanalVenda;
	    	          return this._TemporaryIdLjvCanalVenda;
	    	    }
	    	    set
	    	    {
	    	          if (this._TemporaryIdLjvCanalVenda != value)
	    	              this._TemporaryIdLjvCanalVenda = value;
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
	[DomainIdentifier("ProcessorOverviewLinxAutoSetupDomainService", CodeProcessor = typeof(MethodPatchingCodeProcessor))]
	public partial class LinxAutoSetupDomainService : DomainService, IDataServiceContext 
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

	
	    private Linx.Framework.Autorizacao.BM.AutorizacaoContext _dbContext;
	    protected Linx.Framework.Autorizacao.BM.AutorizacaoContext DbContext 
	    { 
	    	get 
	    	{
	        	if (this._dbContext == null)
	        	{
	        		this._dbContext = new Linx.Framework.Autorizacao.BM.AutorizacaoContext(connectionString, this.Headers);
	        		((System.Data.Entity.Infrastructure.IObjectContextAdapter)this._dbContext).ObjectContext.CommandTimeout = 180;
	        	}
	        	return this._dbContext;
	    	}
	    }

	    public string GetModelAssemblyName()
	    {
	        return typeof(Linx.Framework.Autorizacao.BM.AutorizacaoContext).Assembly.FullName;
	    }

	    public System.Data.Entity.Database Database
	    {
	        get { return this.DbContext.Database; }
	    }

		
	    public LinxAutoSetupDomainService() : this("", null, null) { }
	    public LinxAutoSetupDomainService(string connectionString) : this(connectionString, null, null) { }
	    public LinxAutoSetupDomainService(Dictionary<string, string> headers) : this("", null, headers) { }
	    public LinxAutoSetupDomainService(Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : this("", dataContext, headers) { }
	    public LinxAutoSetupDomainService(string connectionString, Linx.Framework.Autorizacao.BM.AutorizacaoContext dataContext, Dictionary<string, string> headers) : base() 
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
	    public Linx.Framework.Autorizacao.BM.AutorizacaoContext GetEDM()
        {
          return this.DbContext;
        }	

			
	    [Ignore]	
	    public void AddCustomChanges(Entity changedEntity, Entity originalEntity, ChangeOperation operation)
	    {
	
 	        if (changedEntity is TcsEmpresaAutenticacao)
 	        {
 	          List<EntityChange> entityChanges = this.GetRepresentations((TcsEmpresaAutenticacao)changedEntity, originalEntity as TcsEmpresaAutenticacao, operation);
 	          Linx.Framework.BV.Empresa.EmpresaDomainService serviceContext2 = new Linx.Framework.BV.Empresa.EmpresaDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
 	          foreach (var entityChange in entityChanges.Where(e => e.Mark == "serviceContext2").ToList())
 	          {
 	                serviceContext2.AddCustomChanges(entityChange.Entity, entityChange.Original, operation);
 	                serviceContext2.SaveCustomChanges();
 	                if (operation == ChangeOperation.Insert) entityChange.RefreshKeys();
 	          }
 	        }
 	        else if (changedEntity is TcsEmpresaAutenticacaoModulo)
 	        {
 	          List<EntityChange> entityChanges = this.GetRepresentations((TcsEmpresaAutenticacaoModulo)changedEntity, originalEntity as TcsEmpresaAutenticacaoModulo, operation);
 	          Linx.Framework.BV.Empresa.EmpresaDomainService serviceContext2 = new Linx.Framework.BV.Empresa.EmpresaDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
 	          foreach (var entityChange in entityChanges.Where(e => e.Mark == "serviceContext2").ToList())
 	          {
 	                serviceContext2.AddCustomChanges(entityChange.Entity, entityChange.Original, operation);
 	                serviceContext2.SaveCustomChanges();
 	                if (operation == ChangeOperation.Insert) entityChange.RefreshKeys();
 	          }
 	        }
 	        else if (changedEntity is TcsUsuarioAutenticacao)
 	        {
 	          List<EntityChange> entityChanges = this.GetRepresentations((TcsUsuarioAutenticacao)changedEntity, originalEntity as TcsUsuarioAutenticacao, operation);
 	          Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService serviceContext8 = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
 	          foreach (var entityChange in entityChanges.Where(e => e.Mark == "serviceContext8").ToList())
 	          {
 	                serviceContext8.AddCustomChanges(entityChange.Entity, entityChange.Original, operation);
 	                serviceContext8.SaveCustomChanges();
 	                if (operation == ChangeOperation.Insert) entityChange.RefreshKeys();
 	          }
 	        }
 	        else if (changedEntity is TcsUsuarioAutenticacaoAcesso)
 	        {
 	          List<EntityChange> entityChanges = this.GetRepresentations((TcsUsuarioAutenticacaoAcesso)changedEntity, originalEntity as TcsUsuarioAutenticacaoAcesso, operation);
 	          Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService serviceContext8 = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
 	          foreach (var entityChange in entityChanges.Where(e => e.Mark == "serviceContext8").ToList())
 	          {
 	                serviceContext8.AddCustomChanges(entityChange.Entity, entityChange.Original, operation);
 	                serviceContext8.SaveCustomChanges();
 	                if (operation == ChangeOperation.Insert) entityChange.RefreshKeys();
 	          }
 	        }
 	        else if (changedEntity is TcsUsuarioPerfil)
 	        {
 	          List<EntityChange> entityChanges = this.GetRepresentations((TcsUsuarioPerfil)changedEntity, originalEntity as TcsUsuarioPerfil, operation);
 	          Linx.Framework.BV.Usuario.UsuarioDomainService serviceContext7 = new Linx.Framework.BV.Usuario.UsuarioDomainService(this.Headers) { IsSecure = this.IsSecure };
 	          foreach (var entityChange in entityChanges.Where(e => e.Mark == "serviceContext7").ToList())
 	          {
 	                serviceContext7.AddCustomChanges(entityChange.Entity, entityChange.Original, operation);
 	                serviceContext7.SaveCustomChanges();
 	                if (operation == ChangeOperation.Insert) entityChange.RefreshKeys();
 	          }
 	        }
 	        else if (changedEntity is TcsAmbiente)
 	        {
 	          List<EntityChange> entityChanges = this.GetRepresentations((TcsAmbiente)changedEntity, originalEntity as TcsAmbiente, operation);
 	          Linx.Framework.BV.Ambiente.AmbienteDomainService serviceContext1 = new Linx.Framework.BV.Ambiente.AmbienteDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
 	          foreach (var entityChange in entityChanges.Where(e => e.Mark == "serviceContext1").ToList())
 	          {
 	                serviceContext1.AddCustomChanges(entityChange.Entity, entityChange.Original, operation);
 	                serviceContext1.SaveCustomChanges();
 	                if (operation == ChangeOperation.Insert) entityChange.RefreshKeys();
 	          }
 	        }
 	        else if (changedEntity is TcsAmbienteConexao)
 	        {
 	          List<EntityChange> entityChanges = this.GetRepresentations((TcsAmbienteConexao)changedEntity, originalEntity as TcsAmbienteConexao, operation);
 	          Linx.Framework.BV.Ambiente.AmbienteDomainService serviceContext1 = new Linx.Framework.BV.Ambiente.AmbienteDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
 	          foreach (var entityChange in entityChanges.Where(e => e.Mark == "serviceContext1").ToList())
 	          {
 	                serviceContext1.AddCustomChanges(entityChange.Entity, entityChange.Original, operation);
 	                serviceContext1.SaveCustomChanges();
 	                if (operation == ChangeOperation.Insert) entityChange.RefreshKeys();
 	          }
 	        }
 	        else if (changedEntity is TcsAmbienteUsuarioAcesso)
 	        {
 	          List<EntityChange> entityChanges = this.GetRepresentations((TcsAmbienteUsuarioAcesso)changedEntity, originalEntity as TcsAmbienteUsuarioAcesso, operation);
 	          Linx.Framework.BV.Ambiente.AmbienteDomainService serviceContext1 = new Linx.Framework.BV.Ambiente.AmbienteDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
 	          foreach (var entityChange in entityChanges.Where(e => e.Mark == "serviceContext1").ToList())
 	          {
 	                serviceContext1.AddCustomChanges(entityChange.Entity, entityChange.Original, operation);
 	                serviceContext1.SaveCustomChanges();
 	                if (operation == ChangeOperation.Insert) entityChange.RefreshKeys();
 	          }
 	        }
 	        else if (changedEntity is TcsModuloGrupo)
 	        {
 	          List<EntityChange> entityChanges = this.GetRepresentations((TcsModuloGrupo)changedEntity, originalEntity as TcsModuloGrupo, operation);
 	          Linx.Framework.BV.Modulo.ModuloDomainService serviceContext3 = new Linx.Framework.BV.Modulo.ModuloDomainService(this.Headers) { IsSecure = this.IsSecure };
 	          foreach (var entityChange in entityChanges.Where(e => e.Mark == "serviceContext3").ToList())
 	          {
 	                serviceContext3.AddCustomChanges(entityChange.Entity, entityChange.Original, operation);
 	                serviceContext3.SaveCustomChanges();
 	                if (operation == ChangeOperation.Insert) entityChange.RefreshKeys();
 	          }
 	        }
 	        else if (changedEntity is TcsModuloGrupoDetalhe)
 	        {
 	          List<EntityChange> entityChanges = this.GetRepresentations((TcsModuloGrupoDetalhe)changedEntity, originalEntity as TcsModuloGrupoDetalhe, operation);
 	          Linx.Framework.BV.Modulo.ModuloDomainService serviceContext3 = new Linx.Framework.BV.Modulo.ModuloDomainService(this.Headers) { IsSecure = this.IsSecure };
 	          foreach (var entityChange in entityChanges.Where(e => e.Mark == "serviceContext3").ToList())
 	          {
 	                serviceContext3.AddCustomChanges(entityChange.Entity, entityChange.Original, operation);
 	                serviceContext3.SaveCustomChanges();
 	                if (operation == ChangeOperation.Insert) entityChange.RefreshKeys();
 	          }
 	        }
 	        else if (changedEntity is TcsParametroValor)
 	        {
 	          List<EntityChange> entityChanges = this.GetRepresentations((TcsParametroValor)changedEntity, originalEntity as TcsParametroValor, operation);
 	          Linx.Framework.BV.Parametro.ParametroDomainService serviceContext4 = new Linx.Framework.BV.Parametro.ParametroDomainService(this.Headers) { IsSecure = this.IsSecure };
 	          foreach (var entityChange in entityChanges.Where(e => e.Mark == "serviceContext4").ToList())
 	          {
 	                serviceContext4.AddCustomChanges(entityChange.Entity, entityChange.Original, operation);
 	                serviceContext4.SaveCustomChanges();
 	                if (operation == ChangeOperation.Insert) entityChange.RefreshKeys();
 	          }
 	        }
 	        else if (changedEntity is TcsPerfil)
 	        {
 	          List<EntityChange> entityChanges = this.GetRepresentations((TcsPerfil)changedEntity, originalEntity as TcsPerfil, operation);
 	          Linx.Framework.BV.Perfil.PerfilDomainService serviceContext6 = new Linx.Framework.BV.Perfil.PerfilDomainService(this.Headers) { IsSecure = this.IsSecure };
 	          foreach (var entityChange in entityChanges.Where(e => e.Mark == "serviceContext6").ToList())
 	          {
 	                serviceContext6.AddCustomChanges(entityChange.Entity, entityChange.Original, operation);
 	                serviceContext6.SaveCustomChanges();
 	                if (operation == ChangeOperation.Insert) entityChange.RefreshKeys();
 	          }
 	        }
 	        else if (changedEntity is TcsPerfilRegraModulo)
 	        {
 	          List<EntityChange> entityChanges = this.GetRepresentations((TcsPerfilRegraModulo)changedEntity, originalEntity as TcsPerfilRegraModulo, operation);
 	          Linx.Framework.BV.Perfil.PerfilDomainService serviceContext6 = new Linx.Framework.BV.Perfil.PerfilDomainService(this.Headers) { IsSecure = this.IsSecure };
 	          foreach (var entityChange in entityChanges.Where(e => e.Mark == "serviceContext6").ToList())
 	          {
 	                serviceContext6.AddCustomChanges(entityChange.Entity, entityChange.Original, operation);
 	                serviceContext6.SaveCustomChanges();
 	                if (operation == ChangeOperation.Insert) entityChange.RefreshKeys();
 	          }
 	        }
 	        else if (changedEntity is TcsPerfilUsuario)
 	        {
 	          List<EntityChange> entityChanges = this.GetRepresentations((TcsPerfilUsuario)changedEntity, originalEntity as TcsPerfilUsuario, operation);
 	          Linx.Framework.BV.Perfil.PerfilDomainService serviceContext6 = new Linx.Framework.BV.Perfil.PerfilDomainService(this.Headers) { IsSecure = this.IsSecure };
 	          foreach (var entityChange in entityChanges.Where(e => e.Mark == "serviceContext6").ToList())
 	          {
 	                serviceContext6.AddCustomChanges(entityChange.Entity, entityChange.Original, operation);
 	                serviceContext6.SaveCustomChanges();
 	                if (operation == ChangeOperation.Insert) entityChange.RefreshKeys();
 	          }
 	        }
 	        else if (changedEntity is TcsEmpresaGpecon)
 	        {
 	          List<EntityChange> entityChanges = this.GetRepresentations((TcsEmpresaGpecon)changedEntity, originalEntity as TcsEmpresaGpecon, operation);
 	          Linx.Framework.BV.Empresa.EmpresaDomainService serviceContext2 = new Linx.Framework.BV.Empresa.EmpresaDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
 	          foreach (var entityChange in entityChanges.Where(e => e.Mark == "serviceContext2").ToList())
 	          {
 	                serviceContext2.AddCustomChanges(entityChange.Entity, entityChange.Original, operation);
 	                serviceContext2.SaveCustomChanges();
 	                if (operation == ChangeOperation.Insert) entityChange.RefreshKeys();
 	          }
 	        }
 	        else if (changedEntity is TbcFilial)
 	        {
 	          List<EntityChange> entityChanges = this.GetRepresentations((TbcFilial)changedEntity, originalEntity as TbcFilial, operation);
 	          Linx.CadastroBase.BV.CadastroPfj.CadastroPfjDomainService serviceContext0 = new Linx.CadastroBase.BV.CadastroPfj.CadastroPfjDomainService(this.Headers) { IsSecure = this.IsSecure };
 	          foreach (var entityChange in entityChanges.Where(e => e.Mark == "serviceContext0").ToList())
 	          {
 	                serviceContext0.AddCustomChanges(entityChange.Entity, entityChange.Original, operation);
 	                serviceContext0.SaveCustomChanges();
 	                if (operation == ChangeOperation.Insert) entityChange.RefreshKeys();
 	          }
 	        }
 	        else if (changedEntity is TbcGrupoEconomico)
 	        {
 	          List<EntityChange> entityChanges = this.GetRepresentations((TbcGrupoEconomico)changedEntity, originalEntity as TbcGrupoEconomico, operation);
 	          Linx.Operacional.CadastroBase.BV.GrupoEconomico.GrupoEconomicoDomainService serviceContext11 = new Linx.Operacional.CadastroBase.BV.GrupoEconomico.GrupoEconomicoDomainService(this.Headers) { IsSecure = this.IsSecure };
 	          foreach (var entityChange in entityChanges.Where(e => e.Mark == "serviceContext11").ToList())
 	          {
 	                serviceContext11.AddCustomChanges(entityChange.Entity, entityChange.Original, operation);
 	                serviceContext11.SaveCustomChanges();
 	                if (operation == ChangeOperation.Insert) entityChange.RefreshKeys();
 	          }
 	        }
 	        else changedEntity.ApplyChanges(this.DbContext, originalEntity, operation, null);
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
 	        var _TcsEmpresaAutenticacaoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsEmpresaAutenticacao && e.Entity.GetType().Name == "TcsEmpresaAutenticacao" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _TcsEmpresaAutenticacaoElements)
 	           if (((TcsEmpresaAutenticacao)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 	        var _TcsUsuarioAutenticacaoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioAutenticacao && e.Entity.GetType().Name == "TcsUsuarioAutenticacao" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _TcsUsuarioAutenticacaoElements)
 	           if (((TcsUsuarioAutenticacao)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 	        var _TcsAmbienteElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsAmbiente && e.Entity.GetType().Name == "TcsAmbiente" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _TcsAmbienteElements)
 	           if (((TcsAmbiente)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 	        var _TcsModuloGrupoElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsModuloGrupo && e.Entity.GetType().Name == "TcsModuloGrupo" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _TcsModuloGrupoElements)
 	           if (((TcsModuloGrupo)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 	        var _TcsPerfilElements = changeSet.ChangeSetEntries.Where(e => e.Entity is TcsPerfil && e.Entity.GetType().Name == "TcsPerfil" && e.Associations == null && e.OriginalAssociations == null).ToList();
 	        foreach (var entity in _TcsPerfilElements)
 	           if (((TcsPerfil)entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }
 
 	        //Remove inconsistent details
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsEmpresaAutenticacaoModulo && e.Entity.GetType().Name == "TcsEmpresaAutenticacaoModulo" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioAutenticacaoAcesso && e.Entity.GetType().Name == "TcsUsuarioAutenticacaoAcesso" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsAmbienteConexao && e.Entity.GetType().Name == "TcsAmbienteConexao" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsAmbienteUsuarioAcesso && e.Entity.GetType().Name == "TcsAmbienteUsuarioAcesso" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsModuloGrupoDetalhe && e.Entity.GetType().Name == "TcsModuloGrupoDetalhe" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsPerfilRegraModulo && e.Entity.GetType().Name == "TcsPerfilRegraModulo" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
 	        {
 	            entry.Operation = DomainOperation.None;
 	            if (!createNewChangeSet) createNewChangeSet = true;
 	        }
 	        foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is TcsPerfilUsuario && e.Entity.GetType().Name == "TcsPerfilUsuario" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())
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
	                SaveAllRepresentations();

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
		

	    #region Save Representations.

	    //Replace detail keys
	    [Ignore]
	    private void ReplaceDetailsByParent(List<EntityChange> entityChanges, Entity parent)
	    {
		
 	        if (parent is TcsEmpresaAutenticacao)
 	        {
 	          foreach (TcsEmpresaAutenticacaoModulo entity in ((TcsEmpresaAutenticacao)parent).TcsEmpresaAutenticacaoModuloList)
 	          {
 	              entity.IdLinx = ((TcsEmpresaAutenticacao)parent).IdLinx;
 	              var entityEntry = entityChanges.FirstOrDefault(e => e.Representation == entity);
 	              if (entityEntry != null)
 	                  entityEntry.Entity.SetPropertyValue("IdLinx", entity.IdLinx);
 	          }
 	        }
 	        if (parent is TcsUsuarioAutenticacao)
 	        {
 	          foreach (TcsUsuarioAutenticacaoAcesso entity in ((TcsUsuarioAutenticacao)parent).TcsUsuarioAutenticacaoAcessoList)
 	          {
 	              entity.IdUsuario = ((TcsUsuarioAutenticacao)parent).IdUsuario;
 	              var entityEntry = entityChanges.FirstOrDefault(e => e.Representation == entity);
 	              if (entityEntry != null)
 	                  entityEntry.Entity.SetPropertyValue("IdUsuario", entity.IdUsuario);
 	          }
 	        }
 	        if (parent is TcsAmbiente)
 	        {
 	          foreach (TcsAmbienteConexao entity in ((TcsAmbiente)parent).TcsAmbienteConexaoList)
 	          {
 	              entity.IdTcsAmbiente = ((TcsAmbiente)parent).IdTcsAmbiente;
 	              var entityEntry = entityChanges.FirstOrDefault(e => e.Representation == entity);
 	              if (entityEntry != null)
 	                  entityEntry.Entity.SetPropertyValue("IdTcsAmbiente", entity.IdTcsAmbiente);
 	          }
 	          foreach (TcsAmbienteUsuarioAcesso entity in ((TcsAmbiente)parent).TcsAmbienteUsuarioAcessoList)
 	          {
 	              entity.IdTcsAmbiente = ((TcsAmbiente)parent).IdTcsAmbiente;
 	              var entityEntry = entityChanges.FirstOrDefault(e => e.Representation == entity);
 	              if (entityEntry != null)
 	                  entityEntry.Entity.SetPropertyValue("IdTcsAmbiente", entity.IdTcsAmbiente);
 	          }
 	        }
 	        if (parent is TcsModuloGrupo)
 	        {
 	          foreach (TcsModuloGrupoDetalhe entity in ((TcsModuloGrupo)parent).TcsModuloGrupoDetalheList)
 	          {
 	              entity.IdGrupoModulo = ((TcsModuloGrupo)parent).IdGrupoModulo;
 	              var entityEntry = entityChanges.FirstOrDefault(e => e.Representation == entity);
 	              if (entityEntry != null)
 	                  entityEntry.Entity.SetPropertyValue("IdGrupoModulo", entity.IdGrupoModulo);
 	          }
 	        }
 	        if (parent is TcsPerfil)
 	        {
 	          foreach (TcsPerfilRegraModulo entity in ((TcsPerfil)parent).TcsPerfilRegraModuloList)
 	          {
 	              entity.IdPerfil = ((TcsPerfil)parent).IdPerfil;
 	              var entityEntry = entityChanges.FirstOrDefault(e => e.Representation == entity);
 	              if (entityEntry != null)
 	                  entityEntry.Entity.SetPropertyValue("IdPerfil", entity.IdPerfil);
 	          }
 	          foreach (TcsPerfilUsuario entity in ((TcsPerfil)parent).TcsPerfilUsuarioList)
 	          {
 	              entity.IdPerfil = ((TcsPerfil)parent).IdPerfil;
 	              var entityEntry = entityChanges.FirstOrDefault(e => e.Representation == entity);
 	              if (entityEntry != null)
 	                  entityEntry.Entity.SetPropertyValue("IdPerfil", entity.IdPerfil);
 	          }
 	        }	
	    }

	    //Save all entity representations
	    [Ignore]
	    private void SaveAllRepresentations()
	    {
	        List<EntityChange> entityChanges = new List<EntityChange>();
				
	        SaveBufferRepresentationsOfTcsEmpresaAutenticacao(entityChanges);		
				
	        SaveBufferRepresentationsOfTcsEmpresaAutenticacaoModulo(entityChanges);		
				
	        SaveBufferRepresentationsOfTcsUsuarioAutenticacao(entityChanges);		
				
	        SaveBufferRepresentationsOfTcsUsuarioAutenticacaoAcesso(entityChanges);		
				
	        SaveBufferRepresentationsOfTcsUsuarioPerfil(entityChanges);		
				
	        SaveBufferRepresentationsOfTcsAmbiente(entityChanges);		
				
	        SaveBufferRepresentationsOfTcsAmbienteConexao(entityChanges);		
				
	        SaveBufferRepresentationsOfTcsAmbienteUsuarioAcesso(entityChanges);		
				
	        SaveBufferRepresentationsOfTcsModuloGrupo(entityChanges);		
				
	        SaveBufferRepresentationsOfTcsModuloGrupoDetalhe(entityChanges);		
				
	        SaveBufferRepresentationsOfTcsParametroValor(entityChanges);		
				
	        SaveBufferRepresentationsOfTcsPerfil(entityChanges);		
				
	        SaveBufferRepresentationsOfTcsPerfilRegraModulo(entityChanges);		
				
	        SaveBufferRepresentationsOfTcsPerfilUsuario(entityChanges);		
				
	        SaveBufferRepresentationsOfTcsEmpresaGpecon(entityChanges);		
				
	        SaveBufferRepresentationsOfTbcFilial(entityChanges);		
				
	        SaveBufferRepresentationsOfTbcGrupoEconomico(entityChanges);		
		
	        if (entityChanges.Count == 0) return;
		
 
 	        //Submitting all data changes
 	        Linx.Framework.BV.Empresa.EmpresaDomainService serviceContext2 = new Linx.Framework.BV.Empresa.EmpresaDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
 	        var serviceContext2Changes = entityChanges.Where(e => e.Mark == "serviceContext2").ToList();
 	        serviceContext2.SubmitData(this.ServiceContext, serviceContext2Changes);
 	        //Replace keys from source
 	        foreach (var entityChange in serviceContext2Changes) { entityChange.RefreshKeys(); this.ReplaceDetailsByParent(entityChanges, entityChange.Representation); }
 	        Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService serviceContext8 = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
 	        var serviceContext8Changes = entityChanges.Where(e => e.Mark == "serviceContext8").ToList();
 	        serviceContext8.SubmitData(this.ServiceContext, serviceContext8Changes);
 	        //Replace keys from source
 	        foreach (var entityChange in serviceContext8Changes) { entityChange.RefreshKeys(); this.ReplaceDetailsByParent(entityChanges, entityChange.Representation); }
 	        Linx.Framework.BV.Usuario.UsuarioDomainService serviceContext7 = new Linx.Framework.BV.Usuario.UsuarioDomainService(this.Headers) { IsSecure = this.IsSecure };
 	        var serviceContext7Changes = entityChanges.Where(e => e.Mark == "serviceContext7").ToList();
 	        serviceContext7.SubmitData(this.ServiceContext, serviceContext7Changes);
 	        //Replace keys from source
 	        foreach (var entityChange in serviceContext7Changes) { entityChange.RefreshKeys(); this.ReplaceDetailsByParent(entityChanges, entityChange.Representation); }
 	        Linx.Framework.BV.Ambiente.AmbienteDomainService serviceContext1 = new Linx.Framework.BV.Ambiente.AmbienteDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
 	        var serviceContext1Changes = entityChanges.Where(e => e.Mark == "serviceContext1").ToList();
 	        serviceContext1.SubmitData(this.ServiceContext, serviceContext1Changes);
 	        //Replace keys from source
 	        foreach (var entityChange in serviceContext1Changes) { entityChange.RefreshKeys(); this.ReplaceDetailsByParent(entityChanges, entityChange.Representation); }
 	        Linx.Framework.BV.Modulo.ModuloDomainService serviceContext3 = new Linx.Framework.BV.Modulo.ModuloDomainService(this.Headers) { IsSecure = this.IsSecure };
 	        var serviceContext3Changes = entityChanges.Where(e => e.Mark == "serviceContext3").ToList();
 	        serviceContext3.SubmitData(this.ServiceContext, serviceContext3Changes);
 	        //Replace keys from source
 	        foreach (var entityChange in serviceContext3Changes) { entityChange.RefreshKeys(); this.ReplaceDetailsByParent(entityChanges, entityChange.Representation); }
 	        Linx.Framework.BV.Parametro.ParametroDomainService serviceContext4 = new Linx.Framework.BV.Parametro.ParametroDomainService(this.Headers) { IsSecure = this.IsSecure };
 	        var serviceContext4Changes = entityChanges.Where(e => e.Mark == "serviceContext4").ToList();
 	        serviceContext4.SubmitData(this.ServiceContext, serviceContext4Changes);
 	        //Replace keys from source
 	        foreach (var entityChange in serviceContext4Changes) { entityChange.RefreshKeys(); this.ReplaceDetailsByParent(entityChanges, entityChange.Representation); }
 	        Linx.Framework.BV.Perfil.PerfilDomainService serviceContext6 = new Linx.Framework.BV.Perfil.PerfilDomainService(this.Headers) { IsSecure = this.IsSecure };
 	        var serviceContext6Changes = entityChanges.Where(e => e.Mark == "serviceContext6").ToList();
 	        serviceContext6.SubmitData(this.ServiceContext, serviceContext6Changes);
 	        //Replace keys from source
 	        foreach (var entityChange in serviceContext6Changes) { entityChange.RefreshKeys(); this.ReplaceDetailsByParent(entityChanges, entityChange.Representation); }
 	        Linx.CadastroBase.BV.CadastroPfj.CadastroPfjDomainService serviceContext0 = new Linx.CadastroBase.BV.CadastroPfj.CadastroPfjDomainService(this.Headers) { IsSecure = this.IsSecure };
 	        var serviceContext0Changes = entityChanges.Where(e => e.Mark == "serviceContext0").ToList();
 	        serviceContext0.SubmitData(this.ServiceContext, serviceContext0Changes);
 	        //Replace keys from source
 	        foreach (var entityChange in serviceContext0Changes) { entityChange.RefreshKeys(); this.ReplaceDetailsByParent(entityChanges, entityChange.Representation); }
 	        Linx.Operacional.CadastroBase.BV.GrupoEconomico.GrupoEconomicoDomainService serviceContext11 = new Linx.Operacional.CadastroBase.BV.GrupoEconomico.GrupoEconomicoDomainService(this.Headers) { IsSecure = this.IsSecure };
 	        var serviceContext11Changes = entityChanges.Where(e => e.Mark == "serviceContext11").ToList();
 	        serviceContext11.SubmitData(this.ServiceContext, serviceContext11Changes);
 	        //Replace keys from source
 	        foreach (var entityChange in serviceContext11Changes) { entityChange.RefreshKeys(); this.ReplaceDetailsByParent(entityChanges, entityChange.Representation); }	

	    }

			
	  
 	    //Save All Representations Of Entity TcsEmpresaAutenticacao
 	    [Ignore]
 	    private void SaveBufferRepresentationsOfTcsEmpresaAutenticacao(List<EntityChange> entityChanges)
 	    {
 	      foreach (ChangeSetEntry entry in this.ChangeSet.ChangeSetEntries.Where(e => e.Entity is TcsEmpresaAutenticacao && e.Entity.GetType().Name == "TcsEmpresaAutenticacao"))
 	      {
 	          TcsEmpresaAutenticacao entity = (TcsEmpresaAutenticacao)entry.Entity;
 	          entityChanges.AddRange(this.GetRepresentations(entity, (this.ChangeSet.GetChangeOperation(entity) == ChangeOperation.Update ? this.ChangeSet.GetOriginal(entity) : null), this.ChangeSet.GetChangeOperation(entity)));
 	      }
 	    }
 
 	    //Get Representation Of TcsEmpresaAutenticacao
 	    [Ignore]
 	    private List<EntityChange> GetRepresentations(TcsEmpresaAutenticacao entity, TcsEmpresaAutenticacao original, ChangeOperation operation)
 	    {
 	          List<EntityChange> result = new List<EntityChange>();
 	          switch (operation)
 	          {
 	              case ChangeOperation.None:
 	                  //None TcsEmpresaAutenticacao
 	                  Linx.Framework.BV.Empresa.TcsEmpresaAutenticacao noneTcsEmpresaAutenticacao = new Linx.Framework.BV.Empresa.TcsEmpresaAutenticacao() {
 	                  CnpjCpf = entity.CnpjCpf,
 	                  IdLinx = entity.IdLinx,
 	                  NomeEmpresa = entity.NomeEmpresa,
 	                  UidEmpresa = entity.UidEmpresa
 	                  };
 	                  result.Add(new EntityChange() { Entity = noneTcsEmpresaAutenticacao, Original = noneTcsEmpresaAutenticacao, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext2" });
 	                  break;
 	              case ChangeOperation.Delete:
 	                  //Delete TcsEmpresaAutenticacao
 	                  Linx.Framework.BV.Empresa.TcsEmpresaAutenticacao deleteTcsEmpresaAutenticacao = new Linx.Framework.BV.Empresa.TcsEmpresaAutenticacao() {
 	                  CnpjCpf = entity.CnpjCpf,
 	                  IdLinx = entity.IdLinx,
 	                  NomeEmpresa = entity.NomeEmpresa,
 	                  UidEmpresa = entity.UidEmpresa
 	                  };
 	                  result.Add(new EntityChange() { Entity = deleteTcsEmpresaAutenticacao, Original = null, Operation = ChangeOperation.Delete, Representation = null, Mark = "serviceContext2" });
 	                  break;
 	              case ChangeOperation.Insert:
 	                  //Insert TcsEmpresaAutenticacao
 	                  Linx.Framework.BV.Empresa.TcsEmpresaAutenticacao insertTcsEmpresaAutenticacao = new Linx.Framework.BV.Empresa.TcsEmpresaAutenticacao() {
 	                  CnpjCpf = entity.CnpjCpf,
 	                  IdLinx = entity.IdLinx,
 	                  NomeEmpresa = entity.NomeEmpresa,
 	                  UidEmpresa = entity.UidEmpresa
 	                  };
 	                  result.Add(new EntityChange() { Entity = insertTcsEmpresaAutenticacao, Original = null, Operation = ChangeOperation.Insert, Representation = entity, Mark = "serviceContext2" });
 	                  foreach(var insertedEntity in result) insertedEntity.KeysForRefresh.Add("IdLinx", "IdLinx");
 	                  break;
 	              case ChangeOperation.Update:
 	                  //Update TcsEmpresaAutenticacao
 	                  Linx.Framework.BV.Empresa.TcsEmpresaAutenticacao updateTcsEmpresaAutenticacao = new Linx.Framework.BV.Empresa.TcsEmpresaAutenticacao() {
 	                  CnpjCpf = entity.CnpjCpf,
 	                  IdLinx = entity.IdLinx,
 	                  NomeEmpresa = entity.NomeEmpresa,
 	                  UidEmpresa = entity.UidEmpresa
 	                  };
 	                  //Original Definition
 	                  Linx.Framework.BV.Empresa.TcsEmpresaAutenticacao updateTcsEmpresaAutenticacaoOriginal = (original == null ? null : new Linx.Framework.BV.Empresa.TcsEmpresaAutenticacao() {
 	                  CnpjCpf = original.CnpjCpf,
 	                  IdLinx = original.IdLinx,
 	                  NomeEmpresa = original.NomeEmpresa,
 	                  UidEmpresa = original.UidEmpresa
 	                  });
 	                  result.Add(new EntityChange() { Entity = updateTcsEmpresaAutenticacao, Original = updateTcsEmpresaAutenticacaoOriginal, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext2" });
 	                  break;
 	              default:
 	                  break;
 	          }
 	          return result;
 	    }		
			
	  
 	    //Save All Representations Of Entity TcsEmpresaAutenticacaoModulo
 	    [Ignore]
 	    private void SaveBufferRepresentationsOfTcsEmpresaAutenticacaoModulo(List<EntityChange> entityChanges)
 	    {
 	      foreach (ChangeSetEntry entry in this.ChangeSet.ChangeSetEntries.Where(e => e.Entity is TcsEmpresaAutenticacaoModulo && e.Entity.GetType().Name == "TcsEmpresaAutenticacaoModulo"))
 	      {
 	          TcsEmpresaAutenticacaoModulo entity = (TcsEmpresaAutenticacaoModulo)entry.Entity;
 	          entityChanges.AddRange(this.GetRepresentations(entity, (this.ChangeSet.GetChangeOperation(entity) == ChangeOperation.Update ? this.ChangeSet.GetOriginal(entity) : null), this.ChangeSet.GetChangeOperation(entity)));
 	      }
 	    }
 
 	    //Get Representation Of TcsEmpresaAutenticacaoModulo
 	    [Ignore]
 	    private List<EntityChange> GetRepresentations(TcsEmpresaAutenticacaoModulo entity, TcsEmpresaAutenticacaoModulo original, ChangeOperation operation)
 	    {
 	          List<EntityChange> result = new List<EntityChange>();
 	          switch (operation)
 	          {
 	              case ChangeOperation.None:
 	                  //None TcsEmpresaModulo
 	                  Linx.Framework.BV.Empresa.TcsEmpresaModulo noneTcsEmpresaModulo = new Linx.Framework.BV.Empresa.TcsEmpresaModulo() {
 	                  IdLinx = entity.IdLinx,
 	                  IdModulo = entity.IdModulo,
 	                  IdTcsAplicativo = entity.IdTcsAplicativo,
 	                  IdTcsEmpresaModulo = entity.IdTcsEmpresaModulo
 	                  };
 	                  result.Add(new EntityChange() { Entity = noneTcsEmpresaModulo, Original = noneTcsEmpresaModulo, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext2" });
 	                  break;
 	              case ChangeOperation.Delete:
 	                  //Delete TcsEmpresaModulo
 	                  Linx.Framework.BV.Empresa.TcsEmpresaModulo deleteTcsEmpresaModulo = new Linx.Framework.BV.Empresa.TcsEmpresaModulo() {
 	                  IdLinx = entity.IdLinx,
 	                  IdModulo = entity.IdModulo,
 	                  IdTcsAplicativo = entity.IdTcsAplicativo,
 	                  IdTcsEmpresaModulo = entity.IdTcsEmpresaModulo
 	                  };
 	                  result.Add(new EntityChange() { Entity = deleteTcsEmpresaModulo, Original = null, Operation = ChangeOperation.Delete, Representation = null, Mark = "serviceContext2" });
 	                  break;
 	              case ChangeOperation.Insert:
 	                  //Insert TcsEmpresaModulo
 	                  Linx.Framework.BV.Empresa.TcsEmpresaModulo insertTcsEmpresaModulo = new Linx.Framework.BV.Empresa.TcsEmpresaModulo() {
 	                  IdLinx = entity.IdLinx,
 	                  IdModulo = entity.IdModulo,
 	                  IdTcsAplicativo = entity.IdTcsAplicativo,
 	                  IdTcsEmpresaModulo = entity.IdTcsEmpresaModulo
 	                  };
 	                  result.Add(new EntityChange() { Entity = insertTcsEmpresaModulo, Original = null, Operation = ChangeOperation.Insert, Representation = entity, Mark = "serviceContext2" });
 	                  foreach(var insertedEntity in result) insertedEntity.KeysForRefresh.Add("IdTcsEmpresaModulo", "IdTcsEmpresaModulo");
 	                  break;
 	              case ChangeOperation.Update:
 	                  //Update TcsEmpresaModulo
 	                  Linx.Framework.BV.Empresa.TcsEmpresaModulo updateTcsEmpresaModulo = new Linx.Framework.BV.Empresa.TcsEmpresaModulo() {
 	                  IdLinx = entity.IdLinx,
 	                  IdModulo = entity.IdModulo,
 	                  IdTcsAplicativo = entity.IdTcsAplicativo,
 	                  IdTcsEmpresaModulo = entity.IdTcsEmpresaModulo
 	                  };
 	                  //Original Definition
 	                  Linx.Framework.BV.Empresa.TcsEmpresaModulo updateTcsEmpresaModuloOriginal = (original == null ? null : new Linx.Framework.BV.Empresa.TcsEmpresaModulo() {
 	                  IdLinx = original.IdLinx,
 	                  IdModulo = original.IdModulo,
 	                  IdTcsAplicativo = original.IdTcsAplicativo,
 	                  IdTcsEmpresaModulo = original.IdTcsEmpresaModulo
 	                  });
 	                  result.Add(new EntityChange() { Entity = updateTcsEmpresaModulo, Original = updateTcsEmpresaModuloOriginal, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext2" });
 	                  break;
 	              default:
 	                  break;
 	          }
 	          return result;
 	    }		
			
	  
 	    //Save All Representations Of Entity TcsUsuarioAutenticacao
 	    [Ignore]
 	    private void SaveBufferRepresentationsOfTcsUsuarioAutenticacao(List<EntityChange> entityChanges)
 	    {
 	      foreach (ChangeSetEntry entry in this.ChangeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioAutenticacao && e.Entity.GetType().Name == "TcsUsuarioAutenticacao"))
 	      {
 	          TcsUsuarioAutenticacao entity = (TcsUsuarioAutenticacao)entry.Entity;
 	          entityChanges.AddRange(this.GetRepresentations(entity, (this.ChangeSet.GetChangeOperation(entity) == ChangeOperation.Update ? this.ChangeSet.GetOriginal(entity) : null), this.ChangeSet.GetChangeOperation(entity)));
 	      }
 	    }
 
 	    //Get Representation Of TcsUsuarioAutenticacao
 	    [Ignore]
 	    private List<EntityChange> GetRepresentations(TcsUsuarioAutenticacao entity, TcsUsuarioAutenticacao original, ChangeOperation operation)
 	    {
 	          List<EntityChange> result = new List<EntityChange>();
 	          switch (operation)
 	          {
 	              case ChangeOperation.None:
 	                  //None TcsUsuarioAutenticacao
 	                  Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacao noneTcsUsuarioAutenticacao = new Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacao() {
 	                  AutenticacaoWindows = entity.AutenticacaoWindows,
 	                  CnpjCpf = entity.CnpjCpf,
 	                  ConfirmacaoUsuario = entity.ConfirmacaoUsuario,
 	                  ConfirmacaoUsuario1 = entity.ConfirmacaoUsuario1,
 	                  CriaUsuario = entity.CriaUsuario,
 	                  DataAlteracao = entity.DataAlteracao,
 	                  DataCadastro = entity.DataCadastro,
 	                  DataExpiracaoSenha = entity.DataExpiracaoSenha,
 	                  Email = entity.Email,
 	                  GeraSenhaUsuario = entity.GeraSenhaUsuario,
 	                  IdLinx = entity.IdLinx,
 	                  IdUsuario = entity.IdUsuario,
 	                  LxPfjFisicaJuridica = entity.LxPfjFisicaJuridica,
 	                  NomeAutenticacao = entity.NomeAutenticacao,
 	                  NomeCurtoUsuario = entity.NomeCurtoUsuario,
 	                  NomeUsuario = entity.NomeUsuario,
 	                  UidUsuario = entity.UidUsuario,
 	                  VigenciaFinal = entity.VigenciaFinal,
 	                  VigenciaInicial = entity.VigenciaInicial
 	                  };
 	                  result.Add(new EntityChange() { Entity = noneTcsUsuarioAutenticacao, Original = noneTcsUsuarioAutenticacao, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext8" });
 	                  break;
 	              case ChangeOperation.Delete:
 	                  //Delete TcsUsuarioAutenticacao
 	                  Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacao deleteTcsUsuarioAutenticacao = new Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacao() {
 	                  AutenticacaoWindows = entity.AutenticacaoWindows,
 	                  CnpjCpf = entity.CnpjCpf,
 	                  ConfirmacaoUsuario = entity.ConfirmacaoUsuario,
 	                  ConfirmacaoUsuario1 = entity.ConfirmacaoUsuario1,
 	                  CriaUsuario = entity.CriaUsuario,
 	                  DataAlteracao = entity.DataAlteracao,
 	                  DataCadastro = entity.DataCadastro,
 	                  DataExpiracaoSenha = entity.DataExpiracaoSenha,
 	                  Email = entity.Email,
 	                  GeraSenhaUsuario = entity.GeraSenhaUsuario,
 	                  IdLinx = entity.IdLinx,
 	                  IdUsuario = entity.IdUsuario,
 	                  LxPfjFisicaJuridica = entity.LxPfjFisicaJuridica,
 	                  NomeAutenticacao = entity.NomeAutenticacao,
 	                  NomeCurtoUsuario = entity.NomeCurtoUsuario,
 	                  NomeUsuario = entity.NomeUsuario,
 	                  UidUsuario = entity.UidUsuario,
 	                  VigenciaFinal = entity.VigenciaFinal,
 	                  VigenciaInicial = entity.VigenciaInicial
 	                  };
 	                  result.Add(new EntityChange() { Entity = deleteTcsUsuarioAutenticacao, Original = null, Operation = ChangeOperation.Delete, Representation = null, Mark = "serviceContext8" });
 	                  break;
 	              case ChangeOperation.Insert:
 	                  //Insert TcsUsuarioAutenticacao
 	                  Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacao insertTcsUsuarioAutenticacao = new Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacao() {
 	                  AutenticacaoWindows = entity.AutenticacaoWindows,
 	                  CnpjCpf = entity.CnpjCpf,
 	                  ConfirmacaoUsuario = entity.ConfirmacaoUsuario,
 	                  ConfirmacaoUsuario1 = entity.ConfirmacaoUsuario1,
 	                  CriaUsuario = entity.CriaUsuario,
 	                  DataAlteracao = entity.DataAlteracao,
 	                  DataCadastro = entity.DataCadastro,
 	                  DataExpiracaoSenha = entity.DataExpiracaoSenha,
 	                  Email = entity.Email,
 	                  GeraSenhaUsuario = entity.GeraSenhaUsuario,
 	                  IdLinx = entity.IdLinx,
 	                  IdUsuario = entity.IdUsuario,
 	                  LxPfjFisicaJuridica = entity.LxPfjFisicaJuridica,
 	                  NomeAutenticacao = entity.NomeAutenticacao,
 	                  NomeCurtoUsuario = entity.NomeCurtoUsuario,
 	                  NomeUsuario = entity.NomeUsuario,
 	                  UidUsuario = entity.UidUsuario,
 	                  VigenciaFinal = entity.VigenciaFinal,
 	                  VigenciaInicial = entity.VigenciaInicial
 	                  };
 	                  result.Add(new EntityChange() { Entity = insertTcsUsuarioAutenticacao, Original = null, Operation = ChangeOperation.Insert, Representation = entity, Mark = "serviceContext8" });
 	                  foreach(var insertedEntity in result) insertedEntity.KeysForRefresh.Add("IdUsuario", "IdUsuario");
 	                  break;
 	              case ChangeOperation.Update:
 	                  //Update TcsUsuarioAutenticacao
 	                  Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacao updateTcsUsuarioAutenticacao = new Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacao() {
 	                  AutenticacaoWindows = entity.AutenticacaoWindows,
 	                  CnpjCpf = entity.CnpjCpf,
 	                  ConfirmacaoUsuario = entity.ConfirmacaoUsuario,
 	                  ConfirmacaoUsuario1 = entity.ConfirmacaoUsuario1,
 	                  CriaUsuario = entity.CriaUsuario,
 	                  DataAlteracao = entity.DataAlteracao,
 	                  DataCadastro = entity.DataCadastro,
 	                  DataExpiracaoSenha = entity.DataExpiracaoSenha,
 	                  Email = entity.Email,
 	                  GeraSenhaUsuario = entity.GeraSenhaUsuario,
 	                  IdLinx = entity.IdLinx,
 	                  IdUsuario = entity.IdUsuario,
 	                  LxPfjFisicaJuridica = entity.LxPfjFisicaJuridica,
 	                  NomeAutenticacao = entity.NomeAutenticacao,
 	                  NomeCurtoUsuario = entity.NomeCurtoUsuario,
 	                  NomeUsuario = entity.NomeUsuario,
 	                  UidUsuario = entity.UidUsuario,
 	                  VigenciaFinal = entity.VigenciaFinal,
 	                  VigenciaInicial = entity.VigenciaInicial
 	                  };
 	                  //Original Definition
 	                  Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacao updateTcsUsuarioAutenticacaoOriginal = (original == null ? null : new Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacao() {
 	                  AutenticacaoWindows = original.AutenticacaoWindows,
 	                  CnpjCpf = original.CnpjCpf,
 	                  ConfirmacaoUsuario = original.ConfirmacaoUsuario,
 	                  ConfirmacaoUsuario1 = original.ConfirmacaoUsuario1,
 	                  CriaUsuario = original.CriaUsuario,
 	                  DataAlteracao = original.DataAlteracao,
 	                  DataCadastro = original.DataCadastro,
 	                  DataExpiracaoSenha = original.DataExpiracaoSenha,
 	                  Email = original.Email,
 	                  GeraSenhaUsuario = original.GeraSenhaUsuario,
 	                  IdLinx = original.IdLinx,
 	                  IdUsuario = original.IdUsuario,
 	                  LxPfjFisicaJuridica = original.LxPfjFisicaJuridica,
 	                  NomeAutenticacao = original.NomeAutenticacao,
 	                  NomeCurtoUsuario = original.NomeCurtoUsuario,
 	                  NomeUsuario = original.NomeUsuario,
 	                  UidUsuario = original.UidUsuario,
 	                  VigenciaFinal = original.VigenciaFinal,
 	                  VigenciaInicial = original.VigenciaInicial
 	                  });
 	                  result.Add(new EntityChange() { Entity = updateTcsUsuarioAutenticacao, Original = updateTcsUsuarioAutenticacaoOriginal, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext8" });
 	                  break;
 	              default:
 	                  break;
 	          }
 	          return result;
 	    }		
			
	  
 	    //Save All Representations Of Entity TcsUsuarioAutenticacaoAcesso
 	    [Ignore]
 	    private void SaveBufferRepresentationsOfTcsUsuarioAutenticacaoAcesso(List<EntityChange> entityChanges)
 	    {
 	      foreach (ChangeSetEntry entry in this.ChangeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioAutenticacaoAcesso && e.Entity.GetType().Name == "TcsUsuarioAutenticacaoAcesso"))
 	      {
 	          TcsUsuarioAutenticacaoAcesso entity = (TcsUsuarioAutenticacaoAcesso)entry.Entity;
 	          entityChanges.AddRange(this.GetRepresentations(entity, (this.ChangeSet.GetChangeOperation(entity) == ChangeOperation.Update ? this.ChangeSet.GetOriginal(entity) : null), this.ChangeSet.GetChangeOperation(entity)));
 	      }
 	    }
 
 	    //Get Representation Of TcsUsuarioAutenticacaoAcesso
 	    [Ignore]
 	    private List<EntityChange> GetRepresentations(TcsUsuarioAutenticacaoAcesso entity, TcsUsuarioAutenticacaoAcesso original, ChangeOperation operation)
 	    {
 	          List<EntityChange> result = new List<EntityChange>();
 	          switch (operation)
 	          {
 	              case ChangeOperation.None:
 	                  //None TcsUsuarioAcesso
 	                  Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAcesso noneTcsUsuarioAcesso = new Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAcesso() {
 	                  IdTcsAmbiente = entity.IdTcsAmbiente,
 	                  IdTcsAmbienteRelacionado = entity.IdTcsAmbienteRelacionado,
 	                  IdTcsUsuarioAcesso = entity.IdTcsUsuarioAcesso,
 	                  IdUsuario = entity.IdUsuario,
 	                  IndicaAcessoPadrao = entity.IndicaAcessoPadrao,
 	                  IndicaAdministrador = entity.IndicaAdministrador,
 	                  IndicaMultiGpecon = entity.IndicaMultiGpecon
 	                  };
 	                  result.Add(new EntityChange() { Entity = noneTcsUsuarioAcesso, Original = noneTcsUsuarioAcesso, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext8" });
 	                  break;
 	              case ChangeOperation.Delete:
 	                  //Delete TcsUsuarioAcesso
 	                  Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAcesso deleteTcsUsuarioAcesso = new Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAcesso() {
 	                  IdTcsAmbiente = entity.IdTcsAmbiente,
 	                  IdTcsAmbienteRelacionado = entity.IdTcsAmbienteRelacionado,
 	                  IdTcsUsuarioAcesso = entity.IdTcsUsuarioAcesso,
 	                  IdUsuario = entity.IdUsuario,
 	                  IndicaAcessoPadrao = entity.IndicaAcessoPadrao,
 	                  IndicaAdministrador = entity.IndicaAdministrador,
 	                  IndicaMultiGpecon = entity.IndicaMultiGpecon
 	                  };
 	                  result.Add(new EntityChange() { Entity = deleteTcsUsuarioAcesso, Original = null, Operation = ChangeOperation.Delete, Representation = null, Mark = "serviceContext8" });
 	                  break;
 	              case ChangeOperation.Insert:
 	                  //Insert TcsUsuarioAcesso
 	                  Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAcesso insertTcsUsuarioAcesso = new Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAcesso() {
 	                  IdTcsAmbiente = entity.IdTcsAmbiente,
 	                  IdTcsAmbienteRelacionado = entity.IdTcsAmbienteRelacionado,
 	                  IdTcsUsuarioAcesso = entity.IdTcsUsuarioAcesso,
 	                  IdUsuario = entity.IdUsuario,
 	                  IndicaAcessoPadrao = entity.IndicaAcessoPadrao,
 	                  IndicaAdministrador = entity.IndicaAdministrador,
 	                  IndicaMultiGpecon = entity.IndicaMultiGpecon
 	                  };
 	                  result.Add(new EntityChange() { Entity = insertTcsUsuarioAcesso, Original = null, Operation = ChangeOperation.Insert, Representation = entity, Mark = "serviceContext8" });
 	                  foreach(var insertedEntity in result) insertedEntity.KeysForRefresh.Add("IdTcsUsuarioAcesso", "IdTcsUsuarioAcesso");
 	                  break;
 	              case ChangeOperation.Update:
 	                  //Update TcsUsuarioAcesso
 	                  Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAcesso updateTcsUsuarioAcesso = new Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAcesso() {
 	                  IdTcsAmbiente = entity.IdTcsAmbiente,
 	                  IdTcsAmbienteRelacionado = entity.IdTcsAmbienteRelacionado,
 	                  IdTcsUsuarioAcesso = entity.IdTcsUsuarioAcesso,
 	                  IdUsuario = entity.IdUsuario,
 	                  IndicaAcessoPadrao = entity.IndicaAcessoPadrao,
 	                  IndicaAdministrador = entity.IndicaAdministrador,
 	                  IndicaMultiGpecon = entity.IndicaMultiGpecon
 	                  };
 	                  //Original Definition
 	                  Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAcesso updateTcsUsuarioAcessoOriginal = (original == null ? null : new Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAcesso() {
 	                  IdTcsAmbiente = original.IdTcsAmbiente,
 	                  IdTcsAmbienteRelacionado = original.IdTcsAmbienteRelacionado,
 	                  IdTcsUsuarioAcesso = original.IdTcsUsuarioAcesso,
 	                  IdUsuario = original.IdUsuario,
 	                  IndicaAcessoPadrao = original.IndicaAcessoPadrao,
 	                  IndicaAdministrador = original.IndicaAdministrador,
 	                  IndicaMultiGpecon = original.IndicaMultiGpecon
 	                  });
 	                  result.Add(new EntityChange() { Entity = updateTcsUsuarioAcesso, Original = updateTcsUsuarioAcessoOriginal, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext8" });
 	                  break;
 	              default:
 	                  break;
 	          }
 	          return result;
 	    }		
			
	  
 	    //Save All Representations Of Entity TcsUsuarioPerfil
 	    [Ignore]
 	    private void SaveBufferRepresentationsOfTcsUsuarioPerfil(List<EntityChange> entityChanges)
 	    {
 	      foreach (ChangeSetEntry entry in this.ChangeSet.ChangeSetEntries.Where(e => e.Entity is TcsUsuarioPerfil && e.Entity.GetType().Name == "TcsUsuarioPerfil"))
 	      {
 	          TcsUsuarioPerfil entity = (TcsUsuarioPerfil)entry.Entity;
 	          entityChanges.AddRange(this.GetRepresentations(entity, (this.ChangeSet.GetChangeOperation(entity) == ChangeOperation.Update ? this.ChangeSet.GetOriginal(entity) : null), this.ChangeSet.GetChangeOperation(entity)));
 	      }
 	    }
 
 	    //Get Representation Of TcsUsuarioPerfil
 	    [Ignore]
 	    private List<EntityChange> GetRepresentations(TcsUsuarioPerfil entity, TcsUsuarioPerfil original, ChangeOperation operation)
 	    {
 	          List<EntityChange> result = new List<EntityChange>();
 	          switch (operation)
 	          {
 	              case ChangeOperation.None:
 	                  //None TcsUsuarioPerfilP
 	                  Linx.Framework.BV.Usuario.TcsUsuarioPerfilP noneTcsUsuarioPerfilP = new Linx.Framework.BV.Usuario.TcsUsuarioPerfilP() {
 	                  IdPerfil = entity.IdPerfil,
 	                  IdTcsUsuarioPerfil = entity.IdTcsUsuarioPerfil,
 	                  IdUsuario = entity.IdUsuario
 	                  };
 	                  result.Add(new EntityChange() { Entity = noneTcsUsuarioPerfilP, Original = noneTcsUsuarioPerfilP, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext7" });
 	                  break;
 	              case ChangeOperation.Delete:
 	                  //Delete TcsUsuarioPerfilP
 	                  Linx.Framework.BV.Usuario.TcsUsuarioPerfilP deleteTcsUsuarioPerfilP = new Linx.Framework.BV.Usuario.TcsUsuarioPerfilP() {
 	                  IdPerfil = entity.IdPerfil,
 	                  IdTcsUsuarioPerfil = entity.IdTcsUsuarioPerfil,
 	                  IdUsuario = entity.IdUsuario
 	                  };
 	                  result.Add(new EntityChange() { Entity = deleteTcsUsuarioPerfilP, Original = null, Operation = ChangeOperation.Delete, Representation = null, Mark = "serviceContext7" });
 	                  break;
 	              case ChangeOperation.Insert:
 	                  //Insert TcsUsuarioPerfilP
 	                  Linx.Framework.BV.Usuario.TcsUsuarioPerfilP insertTcsUsuarioPerfilP = new Linx.Framework.BV.Usuario.TcsUsuarioPerfilP() {
 	                  IdPerfil = entity.IdPerfil,
 	                  IdTcsUsuarioPerfil = entity.IdTcsUsuarioPerfil,
 	                  IdUsuario = entity.IdUsuario
 	                  };
 	                  result.Add(new EntityChange() { Entity = insertTcsUsuarioPerfilP, Original = null, Operation = ChangeOperation.Insert, Representation = entity, Mark = "serviceContext7" });
 	                  foreach(var insertedEntity in result) insertedEntity.KeysForRefresh.Add("IdTcsUsuarioPerfil", "IdTcsUsuarioPerfil");
 	                  break;
 	              case ChangeOperation.Update:
 	                  //Update TcsUsuarioPerfilP
 	                  Linx.Framework.BV.Usuario.TcsUsuarioPerfilP updateTcsUsuarioPerfilP = new Linx.Framework.BV.Usuario.TcsUsuarioPerfilP() {
 	                  IdPerfil = entity.IdPerfil,
 	                  IdTcsUsuarioPerfil = entity.IdTcsUsuarioPerfil,
 	                  IdUsuario = entity.IdUsuario
 	                  };
 	                  //Original Definition
 	                  Linx.Framework.BV.Usuario.TcsUsuarioPerfilP updateTcsUsuarioPerfilPOriginal = (original == null ? null : new Linx.Framework.BV.Usuario.TcsUsuarioPerfilP() {
 	                  IdPerfil = original.IdPerfil,
 	                  IdTcsUsuarioPerfil = original.IdTcsUsuarioPerfil,
 	                  IdUsuario = original.IdUsuario
 	                  });
 	                  result.Add(new EntityChange() { Entity = updateTcsUsuarioPerfilP, Original = updateTcsUsuarioPerfilPOriginal, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext7" });
 	                  break;
 	              default:
 	                  break;
 	          }
 	          return result;
 	    }		
			
	  
 	    //Save All Representations Of Entity TcsAmbiente
 	    [Ignore]
 	    private void SaveBufferRepresentationsOfTcsAmbiente(List<EntityChange> entityChanges)
 	    {
 	      foreach (ChangeSetEntry entry in this.ChangeSet.ChangeSetEntries.Where(e => e.Entity is TcsAmbiente && e.Entity.GetType().Name == "TcsAmbiente"))
 	      {
 	          TcsAmbiente entity = (TcsAmbiente)entry.Entity;
 	          entityChanges.AddRange(this.GetRepresentations(entity, (this.ChangeSet.GetChangeOperation(entity) == ChangeOperation.Update ? this.ChangeSet.GetOriginal(entity) : null), this.ChangeSet.GetChangeOperation(entity)));
 	      }
 	    }
 
 	    //Get Representation Of TcsAmbiente
 	    [Ignore]
 	    private List<EntityChange> GetRepresentations(TcsAmbiente entity, TcsAmbiente original, ChangeOperation operation)
 	    {
 	          List<EntityChange> result = new List<EntityChange>();
 	          switch (operation)
 	          {
 	              case ChangeOperation.None:
 	                  //None TcsAmbiente
 	                  Linx.Framework.BV.Ambiente.TcsAmbiente noneTcsAmbiente = new Linx.Framework.BV.Ambiente.TcsAmbiente() {
 	                  DescricaoAmbiente = entity.DescricaoAmbiente,
 	                  IdAplicacao = entity.IdAplicacao,
 	                  IdLinx = entity.IdLinx,
 	                  IdTcsAmbiente = entity.IdTcsAmbiente,
 	                  UidEmpresa = entity.UidEmpresa
 	                  };
 	                  result.Add(new EntityChange() { Entity = noneTcsAmbiente, Original = noneTcsAmbiente, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext1" });
 	                  break;
 	              case ChangeOperation.Delete:
 	                  //Delete TcsAmbiente
 	                  Linx.Framework.BV.Ambiente.TcsAmbiente deleteTcsAmbiente = new Linx.Framework.BV.Ambiente.TcsAmbiente() {
 	                  DescricaoAmbiente = entity.DescricaoAmbiente,
 	                  IdAplicacao = entity.IdAplicacao,
 	                  IdLinx = entity.IdLinx,
 	                  IdTcsAmbiente = entity.IdTcsAmbiente,
 	                  UidEmpresa = entity.UidEmpresa
 	                  };
 	                  result.Add(new EntityChange() { Entity = deleteTcsAmbiente, Original = null, Operation = ChangeOperation.Delete, Representation = null, Mark = "serviceContext1" });
 	                  break;
 	              case ChangeOperation.Insert:
 	                  //Insert TcsAmbiente
 	                  Linx.Framework.BV.Ambiente.TcsAmbiente insertTcsAmbiente = new Linx.Framework.BV.Ambiente.TcsAmbiente() {
 	                  DescricaoAmbiente = entity.DescricaoAmbiente,
 	                  IdAplicacao = entity.IdAplicacao,
 	                  IdLinx = entity.IdLinx,
 	                  IdTcsAmbiente = entity.IdTcsAmbiente,
 	                  UidEmpresa = entity.UidEmpresa
 	                  };
 	                  result.Add(new EntityChange() { Entity = insertTcsAmbiente, Original = null, Operation = ChangeOperation.Insert, Representation = entity, Mark = "serviceContext1" });
 	                  foreach(var insertedEntity in result) insertedEntity.KeysForRefresh.Add("IdTcsAmbiente", "IdTcsAmbiente");
 	                  break;
 	              case ChangeOperation.Update:
 	                  //Update TcsAmbiente
 	                  Linx.Framework.BV.Ambiente.TcsAmbiente updateTcsAmbiente = new Linx.Framework.BV.Ambiente.TcsAmbiente() {
 	                  DescricaoAmbiente = entity.DescricaoAmbiente,
 	                  IdAplicacao = entity.IdAplicacao,
 	                  IdLinx = entity.IdLinx,
 	                  IdTcsAmbiente = entity.IdTcsAmbiente,
 	                  UidEmpresa = entity.UidEmpresa
 	                  };
 	                  //Original Definition
 	                  Linx.Framework.BV.Ambiente.TcsAmbiente updateTcsAmbienteOriginal = (original == null ? null : new Linx.Framework.BV.Ambiente.TcsAmbiente() {
 	                  DescricaoAmbiente = original.DescricaoAmbiente,
 	                  IdAplicacao = original.IdAplicacao,
 	                  IdLinx = original.IdLinx,
 	                  IdTcsAmbiente = original.IdTcsAmbiente,
 	                  UidEmpresa = original.UidEmpresa
 	                  });
 	                  result.Add(new EntityChange() { Entity = updateTcsAmbiente, Original = updateTcsAmbienteOriginal, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext1" });
 	                  break;
 	              default:
 	                  break;
 	          }
 	          return result;
 	    }		
			
	  
 	    //Save All Representations Of Entity TcsAmbienteConexao
 	    [Ignore]
 	    private void SaveBufferRepresentationsOfTcsAmbienteConexao(List<EntityChange> entityChanges)
 	    {
 	      foreach (ChangeSetEntry entry in this.ChangeSet.ChangeSetEntries.Where(e => e.Entity is TcsAmbienteConexao && e.Entity.GetType().Name == "TcsAmbienteConexao"))
 	      {
 	          TcsAmbienteConexao entity = (TcsAmbienteConexao)entry.Entity;
 	          entityChanges.AddRange(this.GetRepresentations(entity, (this.ChangeSet.GetChangeOperation(entity) == ChangeOperation.Update ? this.ChangeSet.GetOriginal(entity) : null), this.ChangeSet.GetChangeOperation(entity)));
 	      }
 	    }
 
 	    //Get Representation Of TcsAmbienteConexao
 	    [Ignore]
 	    private List<EntityChange> GetRepresentations(TcsAmbienteConexao entity, TcsAmbienteConexao original, ChangeOperation operation)
 	    {
 	          List<EntityChange> result = new List<EntityChange>();
 	          switch (operation)
 	          {
 	              case ChangeOperation.None:
 	                  //None TcsAmbienteConexao
 	                  Linx.Framework.BV.Ambiente.TcsAmbienteConexao noneTcsAmbienteConexao = new Linx.Framework.BV.Ambiente.TcsAmbienteConexao() {
 	                  IdTcsAmbiente = entity.IdTcsAmbiente,
 	                  IdTcsAmbienteConexao = entity.IdTcsAmbienteConexao,
 	                  IdTcsAplicativoConexao = entity.IdTcsAplicativoConexao,
 	                  IdTcsBancoServidor = entity.IdTcsBancoServidor
 	                  };
 	                  result.Add(new EntityChange() { Entity = noneTcsAmbienteConexao, Original = noneTcsAmbienteConexao, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext1" });
 	                  break;
 	              case ChangeOperation.Delete:
 	                  //Delete TcsAmbienteConexao
 	                  Linx.Framework.BV.Ambiente.TcsAmbienteConexao deleteTcsAmbienteConexao = new Linx.Framework.BV.Ambiente.TcsAmbienteConexao() {
 	                  IdTcsAmbiente = entity.IdTcsAmbiente,
 	                  IdTcsAmbienteConexao = entity.IdTcsAmbienteConexao,
 	                  IdTcsAplicativoConexao = entity.IdTcsAplicativoConexao,
 	                  IdTcsBancoServidor = entity.IdTcsBancoServidor
 	                  };
 	                  result.Add(new EntityChange() { Entity = deleteTcsAmbienteConexao, Original = null, Operation = ChangeOperation.Delete, Representation = null, Mark = "serviceContext1" });
 	                  break;
 	              case ChangeOperation.Insert:
 	                  //Insert TcsAmbienteConexao
 	                  Linx.Framework.BV.Ambiente.TcsAmbienteConexao insertTcsAmbienteConexao = new Linx.Framework.BV.Ambiente.TcsAmbienteConexao() {
 	                  IdTcsAmbiente = entity.IdTcsAmbiente,
 	                  IdTcsAmbienteConexao = entity.IdTcsAmbienteConexao,
 	                  IdTcsAplicativoConexao = entity.IdTcsAplicativoConexao,
 	                  IdTcsBancoServidor = entity.IdTcsBancoServidor
 	                  };
 	                  result.Add(new EntityChange() { Entity = insertTcsAmbienteConexao, Original = null, Operation = ChangeOperation.Insert, Representation = entity, Mark = "serviceContext1" });
 	                  foreach(var insertedEntity in result) insertedEntity.KeysForRefresh.Add("IdTcsAmbienteConexao", "IdTcsAmbienteConexao");
 	                  break;
 	              case ChangeOperation.Update:
 	                  //Update TcsAmbienteConexao
 	                  Linx.Framework.BV.Ambiente.TcsAmbienteConexao updateTcsAmbienteConexao = new Linx.Framework.BV.Ambiente.TcsAmbienteConexao() {
 	                  IdTcsAmbiente = entity.IdTcsAmbiente,
 	                  IdTcsAmbienteConexao = entity.IdTcsAmbienteConexao,
 	                  IdTcsAplicativoConexao = entity.IdTcsAplicativoConexao,
 	                  IdTcsBancoServidor = entity.IdTcsBancoServidor
 	                  };
 	                  //Original Definition
 	                  Linx.Framework.BV.Ambiente.TcsAmbienteConexao updateTcsAmbienteConexaoOriginal = (original == null ? null : new Linx.Framework.BV.Ambiente.TcsAmbienteConexao() {
 	                  IdTcsAmbiente = original.IdTcsAmbiente,
 	                  IdTcsAmbienteConexao = original.IdTcsAmbienteConexao,
 	                  IdTcsAplicativoConexao = original.IdTcsAplicativoConexao,
 	                  IdTcsBancoServidor = original.IdTcsBancoServidor
 	                  });
 	                  result.Add(new EntityChange() { Entity = updateTcsAmbienteConexao, Original = updateTcsAmbienteConexaoOriginal, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext1" });
 	                  break;
 	              default:
 	                  break;
 	          }
 	          return result;
 	    }		
			
	  
 	    //Save All Representations Of Entity TcsAmbienteUsuarioAcesso
 	    [Ignore]
 	    private void SaveBufferRepresentationsOfTcsAmbienteUsuarioAcesso(List<EntityChange> entityChanges)
 	    {
 	      foreach (ChangeSetEntry entry in this.ChangeSet.ChangeSetEntries.Where(e => e.Entity is TcsAmbienteUsuarioAcesso && e.Entity.GetType().Name == "TcsAmbienteUsuarioAcesso"))
 	      {
 	          TcsAmbienteUsuarioAcesso entity = (TcsAmbienteUsuarioAcesso)entry.Entity;
 	          entityChanges.AddRange(this.GetRepresentations(entity, (this.ChangeSet.GetChangeOperation(entity) == ChangeOperation.Update ? this.ChangeSet.GetOriginal(entity) : null), this.ChangeSet.GetChangeOperation(entity)));
 	      }
 	    }
 
 	    //Get Representation Of TcsAmbienteUsuarioAcesso
 	    [Ignore]
 	    private List<EntityChange> GetRepresentations(TcsAmbienteUsuarioAcesso entity, TcsAmbienteUsuarioAcesso original, ChangeOperation operation)
 	    {
 	          List<EntityChange> result = new List<EntityChange>();
 	          switch (operation)
 	          {
 	              case ChangeOperation.None:
 	                  //None TcsAmbienteUsuarioAcesso
 	                  Linx.Framework.BV.Ambiente.TcsAmbienteUsuarioAcesso noneTcsAmbienteUsuarioAcesso = new Linx.Framework.BV.Ambiente.TcsAmbienteUsuarioAcesso() {
 	                  IdTcsAmbiente = entity.IdTcsAmbiente,
 	                  IdTcsUsuarioAcesso = entity.IdTcsUsuarioAcesso,
 	                  IdUsuario = entity.IdUsuario,
 	                  IndicaAdministrador = entity.IndicaAdministrador,
 	                  IndicaMultiGpecon = entity.IndicaMultiGpecon,
 	                  NomeAutenticacao = entity.NomeAutenticacao,
 	                  NomeUsuario = entity.NomeUsuario,
 	                  UidUsuario = entity.UidUsuario
 	                  };
 	                  result.Add(new EntityChange() { Entity = noneTcsAmbienteUsuarioAcesso, Original = noneTcsAmbienteUsuarioAcesso, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext1" });
 	                  break;
 	              case ChangeOperation.Delete:
 	                  //Delete TcsAmbienteUsuarioAcesso
 	                  Linx.Framework.BV.Ambiente.TcsAmbienteUsuarioAcesso deleteTcsAmbienteUsuarioAcesso = new Linx.Framework.BV.Ambiente.TcsAmbienteUsuarioAcesso() {
 	                  IdTcsAmbiente = entity.IdTcsAmbiente,
 	                  IdTcsUsuarioAcesso = entity.IdTcsUsuarioAcesso,
 	                  IdUsuario = entity.IdUsuario,
 	                  IndicaAdministrador = entity.IndicaAdministrador,
 	                  IndicaMultiGpecon = entity.IndicaMultiGpecon,
 	                  NomeAutenticacao = entity.NomeAutenticacao,
 	                  NomeUsuario = entity.NomeUsuario,
 	                  UidUsuario = entity.UidUsuario
 	                  };
 	                  result.Add(new EntityChange() { Entity = deleteTcsAmbienteUsuarioAcesso, Original = null, Operation = ChangeOperation.Delete, Representation = null, Mark = "serviceContext1" });
 	                  break;
 	              case ChangeOperation.Insert:
 	                  //Insert TcsAmbienteUsuarioAcesso
 	                  Linx.Framework.BV.Ambiente.TcsAmbienteUsuarioAcesso insertTcsAmbienteUsuarioAcesso = new Linx.Framework.BV.Ambiente.TcsAmbienteUsuarioAcesso() {
 	                  IdTcsAmbiente = entity.IdTcsAmbiente,
 	                  IdTcsUsuarioAcesso = entity.IdTcsUsuarioAcesso,
 	                  IdUsuario = entity.IdUsuario,
 	                  IndicaAdministrador = entity.IndicaAdministrador,
 	                  IndicaMultiGpecon = entity.IndicaMultiGpecon,
 	                  NomeAutenticacao = entity.NomeAutenticacao,
 	                  NomeUsuario = entity.NomeUsuario,
 	                  UidUsuario = entity.UidUsuario
 	                  };
 	                  result.Add(new EntityChange() { Entity = insertTcsAmbienteUsuarioAcesso, Original = null, Operation = ChangeOperation.Insert, Representation = entity, Mark = "serviceContext1" });
 	                  foreach(var insertedEntity in result) insertedEntity.KeysForRefresh.Add("IdTcsUsuarioAcesso", "IdTcsUsuarioAcesso");
 	                  break;
 	              case ChangeOperation.Update:
 	                  //Update TcsAmbienteUsuarioAcesso
 	                  Linx.Framework.BV.Ambiente.TcsAmbienteUsuarioAcesso updateTcsAmbienteUsuarioAcesso = new Linx.Framework.BV.Ambiente.TcsAmbienteUsuarioAcesso() {
 	                  IdTcsAmbiente = entity.IdTcsAmbiente,
 	                  IdTcsUsuarioAcesso = entity.IdTcsUsuarioAcesso,
 	                  IdUsuario = entity.IdUsuario,
 	                  IndicaAdministrador = entity.IndicaAdministrador,
 	                  IndicaMultiGpecon = entity.IndicaMultiGpecon,
 	                  NomeAutenticacao = entity.NomeAutenticacao,
 	                  NomeUsuario = entity.NomeUsuario,
 	                  UidUsuario = entity.UidUsuario
 	                  };
 	                  //Original Definition
 	                  Linx.Framework.BV.Ambiente.TcsAmbienteUsuarioAcesso updateTcsAmbienteUsuarioAcessoOriginal = (original == null ? null : new Linx.Framework.BV.Ambiente.TcsAmbienteUsuarioAcesso() {
 	                  IdTcsAmbiente = original.IdTcsAmbiente,
 	                  IdTcsUsuarioAcesso = original.IdTcsUsuarioAcesso,
 	                  IdUsuario = original.IdUsuario,
 	                  IndicaAdministrador = original.IndicaAdministrador,
 	                  IndicaMultiGpecon = original.IndicaMultiGpecon,
 	                  NomeAutenticacao = original.NomeAutenticacao,
 	                  NomeUsuario = original.NomeUsuario,
 	                  UidUsuario = original.UidUsuario
 	                  });
 	                  result.Add(new EntityChange() { Entity = updateTcsAmbienteUsuarioAcesso, Original = updateTcsAmbienteUsuarioAcessoOriginal, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext1" });
 	                  break;
 	              default:
 	                  break;
 	          }
 	          return result;
 	    }		
			
	  
 	    //Save All Representations Of Entity TcsModuloGrupo
 	    [Ignore]
 	    private void SaveBufferRepresentationsOfTcsModuloGrupo(List<EntityChange> entityChanges)
 	    {
 	      foreach (ChangeSetEntry entry in this.ChangeSet.ChangeSetEntries.Where(e => e.Entity is TcsModuloGrupo && e.Entity.GetType().Name == "TcsModuloGrupo"))
 	      {
 	          TcsModuloGrupo entity = (TcsModuloGrupo)entry.Entity;
 	          entityChanges.AddRange(this.GetRepresentations(entity, (this.ChangeSet.GetChangeOperation(entity) == ChangeOperation.Update ? this.ChangeSet.GetOriginal(entity) : null), this.ChangeSet.GetChangeOperation(entity)));
 	      }
 	    }
 
 	    //Get Representation Of TcsModuloGrupo
 	    [Ignore]
 	    private List<EntityChange> GetRepresentations(TcsModuloGrupo entity, TcsModuloGrupo original, ChangeOperation operation)
 	    {
 	          List<EntityChange> result = new List<EntityChange>();
 	          switch (operation)
 	          {
 	              case ChangeOperation.None:
 	                  //None TcsModuloGrupo
 	                  Linx.Framework.BV.Modulo.TcsModuloGrupo noneTcsModuloGrupo = new Linx.Framework.BV.Modulo.TcsModuloGrupo() {
 	                  DescGrupoModulo = entity.DescGrupoModulo,
 	                  IdGrupoModulo = entity.IdGrupoModulo,
 	                  IdTcsAplicativo = entity.IdTcsAplicativo
 	                  };
 	                  result.Add(new EntityChange() { Entity = noneTcsModuloGrupo, Original = noneTcsModuloGrupo, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext3" });
 	                  break;
 	              case ChangeOperation.Delete:
 	                  //Delete TcsModuloGrupo
 	                  Linx.Framework.BV.Modulo.TcsModuloGrupo deleteTcsModuloGrupo = new Linx.Framework.BV.Modulo.TcsModuloGrupo() {
 	                  DescGrupoModulo = entity.DescGrupoModulo,
 	                  IdGrupoModulo = entity.IdGrupoModulo,
 	                  IdTcsAplicativo = entity.IdTcsAplicativo
 	                  };
 	                  result.Add(new EntityChange() { Entity = deleteTcsModuloGrupo, Original = null, Operation = ChangeOperation.Delete, Representation = null, Mark = "serviceContext3" });
 	                  break;
 	              case ChangeOperation.Insert:
 	                  //Insert TcsModuloGrupo
 	                  Linx.Framework.BV.Modulo.TcsModuloGrupo insertTcsModuloGrupo = new Linx.Framework.BV.Modulo.TcsModuloGrupo() {
 	                  DescGrupoModulo = entity.DescGrupoModulo,
 	                  IdGrupoModulo = entity.IdGrupoModulo,
 	                  IdTcsAplicativo = entity.IdTcsAplicativo
 	                  };
 	                  result.Add(new EntityChange() { Entity = insertTcsModuloGrupo, Original = null, Operation = ChangeOperation.Insert, Representation = entity, Mark = "serviceContext3" });
 	                  foreach(var insertedEntity in result) insertedEntity.KeysForRefresh.Add("IdGrupoModulo", "IdGrupoModulo");
 	                  break;
 	              case ChangeOperation.Update:
 	                  //Update TcsModuloGrupo
 	                  Linx.Framework.BV.Modulo.TcsModuloGrupo updateTcsModuloGrupo = new Linx.Framework.BV.Modulo.TcsModuloGrupo() {
 	                  DescGrupoModulo = entity.DescGrupoModulo,
 	                  IdGrupoModulo = entity.IdGrupoModulo,
 	                  IdTcsAplicativo = entity.IdTcsAplicativo
 	                  };
 	                  //Original Definition
 	                  Linx.Framework.BV.Modulo.TcsModuloGrupo updateTcsModuloGrupoOriginal = (original == null ? null : new Linx.Framework.BV.Modulo.TcsModuloGrupo() {
 	                  DescGrupoModulo = original.DescGrupoModulo,
 	                  IdGrupoModulo = original.IdGrupoModulo,
 	                  IdTcsAplicativo = original.IdTcsAplicativo
 	                  });
 	                  result.Add(new EntityChange() { Entity = updateTcsModuloGrupo, Original = updateTcsModuloGrupoOriginal, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext3" });
 	                  break;
 	              default:
 	                  break;
 	          }
 	          return result;
 	    }		
			
	  
 	    //Save All Representations Of Entity TcsModuloGrupoDetalhe
 	    [Ignore]
 	    private void SaveBufferRepresentationsOfTcsModuloGrupoDetalhe(List<EntityChange> entityChanges)
 	    {
 	      foreach (ChangeSetEntry entry in this.ChangeSet.ChangeSetEntries.Where(e => e.Entity is TcsModuloGrupoDetalhe && e.Entity.GetType().Name == "TcsModuloGrupoDetalhe"))
 	      {
 	          TcsModuloGrupoDetalhe entity = (TcsModuloGrupoDetalhe)entry.Entity;
 	          entityChanges.AddRange(this.GetRepresentations(entity, (this.ChangeSet.GetChangeOperation(entity) == ChangeOperation.Update ? this.ChangeSet.GetOriginal(entity) : null), this.ChangeSet.GetChangeOperation(entity)));
 	      }
 	    }
 
 	    //Get Representation Of TcsModuloGrupoDetalhe
 	    [Ignore]
 	    private List<EntityChange> GetRepresentations(TcsModuloGrupoDetalhe entity, TcsModuloGrupoDetalhe original, ChangeOperation operation)
 	    {
 	          List<EntityChange> result = new List<EntityChange>();
 	          switch (operation)
 	          {
 	              case ChangeOperation.None:
 	                  //None TcsModuloDoGrupoDetalhe
 	                  Linx.Framework.BV.Modulo.TcsModuloDoGrupoDetalhe noneTcsModuloDoGrupoDetalhe = new Linx.Framework.BV.Modulo.TcsModuloDoGrupoDetalhe() {
 	                  IdGrupoModulo = entity.IdGrupoModulo,
 	                  IdModulo = entity.IdModulo,
 	                  IdModuloDoGrupo = entity.IdModuloDoGrupo
 	                  };
 	                  result.Add(new EntityChange() { Entity = noneTcsModuloDoGrupoDetalhe, Original = noneTcsModuloDoGrupoDetalhe, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext3" });
 	                  break;
 	              case ChangeOperation.Delete:
 	                  //Delete TcsModuloDoGrupoDetalhe
 	                  Linx.Framework.BV.Modulo.TcsModuloDoGrupoDetalhe deleteTcsModuloDoGrupoDetalhe = new Linx.Framework.BV.Modulo.TcsModuloDoGrupoDetalhe() {
 	                  IdGrupoModulo = entity.IdGrupoModulo,
 	                  IdModulo = entity.IdModulo,
 	                  IdModuloDoGrupo = entity.IdModuloDoGrupo
 	                  };
 	                  result.Add(new EntityChange() { Entity = deleteTcsModuloDoGrupoDetalhe, Original = null, Operation = ChangeOperation.Delete, Representation = null, Mark = "serviceContext3" });
 	                  break;
 	              case ChangeOperation.Insert:
 	                  //Insert TcsModuloDoGrupoDetalhe
 	                  Linx.Framework.BV.Modulo.TcsModuloDoGrupoDetalhe insertTcsModuloDoGrupoDetalhe = new Linx.Framework.BV.Modulo.TcsModuloDoGrupoDetalhe() {
 	                  IdGrupoModulo = entity.IdGrupoModulo,
 	                  IdModulo = entity.IdModulo,
 	                  IdModuloDoGrupo = entity.IdModuloDoGrupo
 	                  };
 	                  result.Add(new EntityChange() { Entity = insertTcsModuloDoGrupoDetalhe, Original = null, Operation = ChangeOperation.Insert, Representation = entity, Mark = "serviceContext3" });
 	                  foreach(var insertedEntity in result) insertedEntity.KeysForRefresh.Add("IdModuloDoGrupo", "IdModuloDoGrupo");
 	                  break;
 	              case ChangeOperation.Update:
 	                  //Update TcsModuloDoGrupoDetalhe
 	                  Linx.Framework.BV.Modulo.TcsModuloDoGrupoDetalhe updateTcsModuloDoGrupoDetalhe = new Linx.Framework.BV.Modulo.TcsModuloDoGrupoDetalhe() {
 	                  IdGrupoModulo = entity.IdGrupoModulo,
 	                  IdModulo = entity.IdModulo,
 	                  IdModuloDoGrupo = entity.IdModuloDoGrupo
 	                  };
 	                  //Original Definition
 	                  Linx.Framework.BV.Modulo.TcsModuloDoGrupoDetalhe updateTcsModuloDoGrupoDetalheOriginal = (original == null ? null : new Linx.Framework.BV.Modulo.TcsModuloDoGrupoDetalhe() {
 	                  IdGrupoModulo = original.IdGrupoModulo,
 	                  IdModulo = original.IdModulo,
 	                  IdModuloDoGrupo = original.IdModuloDoGrupo
 	                  });
 	                  result.Add(new EntityChange() { Entity = updateTcsModuloDoGrupoDetalhe, Original = updateTcsModuloDoGrupoDetalheOriginal, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext3" });
 	                  break;
 	              default:
 	                  break;
 	          }
 	          return result;
 	    }		
			
	  
 	    //Save All Representations Of Entity TcsParametroValor
 	    [Ignore]
 	    private void SaveBufferRepresentationsOfTcsParametroValor(List<EntityChange> entityChanges)
 	    {
 	      foreach (ChangeSetEntry entry in this.ChangeSet.ChangeSetEntries.Where(e => e.Entity is TcsParametroValor && e.Entity.GetType().Name == "TcsParametroValor"))
 	      {
 	          TcsParametroValor entity = (TcsParametroValor)entry.Entity;
 	          entityChanges.AddRange(this.GetRepresentations(entity, (this.ChangeSet.GetChangeOperation(entity) == ChangeOperation.Update ? this.ChangeSet.GetOriginal(entity) : null), this.ChangeSet.GetChangeOperation(entity)));
 	      }
 	    }
 
 	    //Get Representation Of TcsParametroValor
 	    [Ignore]
 	    private List<EntityChange> GetRepresentations(TcsParametroValor entity, TcsParametroValor original, ChangeOperation operation)
 	    {
 	          List<EntityChange> result = new List<EntityChange>();
 	          switch (operation)
 	          {
 	              case ChangeOperation.None:
 	                  //None TcsParametroValorP
 	                  Linx.Framework.BV.Parametro.TcsParametroValorP noneTcsParametroValorP = new Linx.Framework.BV.Parametro.TcsParametroValorP() {
 	                  IdParametro = entity.IdParametro,
 	                  IdParametroValor = entity.IdParametroValor,
 	                  ValorParametro = entity.ValorParametro
 	                  };
 	                  result.Add(new EntityChange() { Entity = noneTcsParametroValorP, Original = noneTcsParametroValorP, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext4" });
 	                  break;
 	              case ChangeOperation.Delete:
 	                  //Delete TcsParametroValorP
 	                  Linx.Framework.BV.Parametro.TcsParametroValorP deleteTcsParametroValorP = new Linx.Framework.BV.Parametro.TcsParametroValorP() {
 	                  IdParametro = entity.IdParametro,
 	                  IdParametroValor = entity.IdParametroValor,
 	                  ValorParametro = entity.ValorParametro
 	                  };
 	                  result.Add(new EntityChange() { Entity = deleteTcsParametroValorP, Original = null, Operation = ChangeOperation.Delete, Representation = null, Mark = "serviceContext4" });
 	                  break;
 	              case ChangeOperation.Insert:
 	                  //Insert TcsParametroValorP
 	                  Linx.Framework.BV.Parametro.TcsParametroValorP insertTcsParametroValorP = new Linx.Framework.BV.Parametro.TcsParametroValorP() {
 	                  IdParametro = entity.IdParametro,
 	                  IdParametroValor = entity.IdParametroValor,
 	                  ValorParametro = entity.ValorParametro
 	                  };
 	                  result.Add(new EntityChange() { Entity = insertTcsParametroValorP, Original = null, Operation = ChangeOperation.Insert, Representation = entity, Mark = "serviceContext4" });
 	                  foreach(var insertedEntity in result) insertedEntity.KeysForRefresh.Add("IdParametroValor", "IdParametroValor");
 	                  break;
 	              case ChangeOperation.Update:
 	                  //Update TcsParametroValorP
 	                  Linx.Framework.BV.Parametro.TcsParametroValorP updateTcsParametroValorP = new Linx.Framework.BV.Parametro.TcsParametroValorP() {
 	                  IdParametro = entity.IdParametro,
 	                  IdParametroValor = entity.IdParametroValor,
 	                  ValorParametro = entity.ValorParametro
 	                  };
 	                  //Original Definition
 	                  Linx.Framework.BV.Parametro.TcsParametroValorP updateTcsParametroValorPOriginal = (original == null ? null : new Linx.Framework.BV.Parametro.TcsParametroValorP() {
 	                  IdParametro = original.IdParametro,
 	                  IdParametroValor = original.IdParametroValor,
 	                  ValorParametro = original.ValorParametro
 	                  });
 	                  result.Add(new EntityChange() { Entity = updateTcsParametroValorP, Original = updateTcsParametroValorPOriginal, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext4" });
 	                  break;
 	              default:
 	                  break;
 	          }
 	          return result;
 	    }		
			
	  
 	    //Save All Representations Of Entity TcsPerfil
 	    [Ignore]
 	    private void SaveBufferRepresentationsOfTcsPerfil(List<EntityChange> entityChanges)
 	    {
 	      foreach (ChangeSetEntry entry in this.ChangeSet.ChangeSetEntries.Where(e => e.Entity is TcsPerfil && e.Entity.GetType().Name == "TcsPerfil"))
 	      {
 	          TcsPerfil entity = (TcsPerfil)entry.Entity;
 	          entityChanges.AddRange(this.GetRepresentations(entity, (this.ChangeSet.GetChangeOperation(entity) == ChangeOperation.Update ? this.ChangeSet.GetOriginal(entity) : null), this.ChangeSet.GetChangeOperation(entity)));
 	      }
 	    }
 
 	    //Get Representation Of TcsPerfil
 	    [Ignore]
 	    private List<EntityChange> GetRepresentations(TcsPerfil entity, TcsPerfil original, ChangeOperation operation)
 	    {
 	          List<EntityChange> result = new List<EntityChange>();
 	          switch (operation)
 	          {
 	              case ChangeOperation.None:
 	                  //None TcsPerfil
 	                  Linx.Framework.BV.Perfil.TcsPerfil noneTcsPerfil = new Linx.Framework.BV.Perfil.TcsPerfil() {
 	                  DescPerfil = entity.DescPerfil,
 	                  IdPerfil = entity.IdPerfil
 	                  };
 	                  result.Add(new EntityChange() { Entity = noneTcsPerfil, Original = noneTcsPerfil, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext6" });
 	                  break;
 	              case ChangeOperation.Delete:
 	                  //Delete TcsPerfil
 	                  Linx.Framework.BV.Perfil.TcsPerfil deleteTcsPerfil = new Linx.Framework.BV.Perfil.TcsPerfil() {
 	                  DescPerfil = entity.DescPerfil,
 	                  IdPerfil = entity.IdPerfil
 	                  };
 	                  result.Add(new EntityChange() { Entity = deleteTcsPerfil, Original = null, Operation = ChangeOperation.Delete, Representation = null, Mark = "serviceContext6" });
 	                  break;
 	              case ChangeOperation.Insert:
 	                  //Insert TcsPerfil
 	                  Linx.Framework.BV.Perfil.TcsPerfil insertTcsPerfil = new Linx.Framework.BV.Perfil.TcsPerfil() {
 	                  DescPerfil = entity.DescPerfil,
 	                  IdPerfil = entity.IdPerfil
 	                  };
 	                  result.Add(new EntityChange() { Entity = insertTcsPerfil, Original = null, Operation = ChangeOperation.Insert, Representation = entity, Mark = "serviceContext6" });
 	                  foreach(var insertedEntity in result) insertedEntity.KeysForRefresh.Add("IdPerfil", "IdPerfil");
 	                  break;
 	              case ChangeOperation.Update:
 	                  //Update TcsPerfil
 	                  Linx.Framework.BV.Perfil.TcsPerfil updateTcsPerfil = new Linx.Framework.BV.Perfil.TcsPerfil() {
 	                  DescPerfil = entity.DescPerfil,
 	                  IdPerfil = entity.IdPerfil
 	                  };
 	                  //Original Definition
 	                  Linx.Framework.BV.Perfil.TcsPerfil updateTcsPerfilOriginal = (original == null ? null : new Linx.Framework.BV.Perfil.TcsPerfil() {
 	                  DescPerfil = original.DescPerfil,
 	                  IdPerfil = original.IdPerfil
 	                  });
 	                  result.Add(new EntityChange() { Entity = updateTcsPerfil, Original = updateTcsPerfilOriginal, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext6" });
 	                  break;
 	              default:
 	                  break;
 	          }
 	          return result;
 	    }		
			
	  
 	    //Save All Representations Of Entity TcsPerfilRegraModulo
 	    [Ignore]
 	    private void SaveBufferRepresentationsOfTcsPerfilRegraModulo(List<EntityChange> entityChanges)
 	    {
 	      foreach (ChangeSetEntry entry in this.ChangeSet.ChangeSetEntries.Where(e => e.Entity is TcsPerfilRegraModulo && e.Entity.GetType().Name == "TcsPerfilRegraModulo"))
 	      {
 	          TcsPerfilRegraModulo entity = (TcsPerfilRegraModulo)entry.Entity;
 	          entityChanges.AddRange(this.GetRepresentations(entity, (this.ChangeSet.GetChangeOperation(entity) == ChangeOperation.Update ? this.ChangeSet.GetOriginal(entity) : null), this.ChangeSet.GetChangeOperation(entity)));
 	      }
 	    }
 
 	    //Get Representation Of TcsPerfilRegraModulo
 	    [Ignore]
 	    private List<EntityChange> GetRepresentations(TcsPerfilRegraModulo entity, TcsPerfilRegraModulo original, ChangeOperation operation)
 	    {
 	          List<EntityChange> result = new List<EntityChange>();
 	          switch (operation)
 	          {
 	              case ChangeOperation.None:
 	                  //None TcsPerfilRegraModulo
 	                  Linx.Framework.BV.Perfil.TcsPerfilRegraModulo noneTcsPerfilRegraModulo = new Linx.Framework.BV.Perfil.TcsPerfilRegraModulo() {
 	                  IdModulo = entity.IdModulo,
 	                  IdPerfil = entity.IdPerfil,
 	                  IdPerfilRegraModulo = entity.IdPerfilRegraModulo,
 	                  LxRegraAcessoModulo = entity.LxRegraAcessoModulo
 	                  };
 	                  result.Add(new EntityChange() { Entity = noneTcsPerfilRegraModulo, Original = noneTcsPerfilRegraModulo, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext6" });
 	                  break;
 	              case ChangeOperation.Delete:
 	                  //Delete TcsPerfilRegraModulo
 	                  Linx.Framework.BV.Perfil.TcsPerfilRegraModulo deleteTcsPerfilRegraModulo = new Linx.Framework.BV.Perfil.TcsPerfilRegraModulo() {
 	                  IdModulo = entity.IdModulo,
 	                  IdPerfil = entity.IdPerfil,
 	                  IdPerfilRegraModulo = entity.IdPerfilRegraModulo,
 	                  LxRegraAcessoModulo = entity.LxRegraAcessoModulo
 	                  };
 	                  result.Add(new EntityChange() { Entity = deleteTcsPerfilRegraModulo, Original = null, Operation = ChangeOperation.Delete, Representation = null, Mark = "serviceContext6" });
 	                  break;
 	              case ChangeOperation.Insert:
 	                  //Insert TcsPerfilRegraModulo
 	                  Linx.Framework.BV.Perfil.TcsPerfilRegraModulo insertTcsPerfilRegraModulo = new Linx.Framework.BV.Perfil.TcsPerfilRegraModulo() {
 	                  IdModulo = entity.IdModulo,
 	                  IdPerfil = entity.IdPerfil,
 	                  IdPerfilRegraModulo = entity.IdPerfilRegraModulo,
 	                  LxRegraAcessoModulo = entity.LxRegraAcessoModulo
 	                  };
 	                  result.Add(new EntityChange() { Entity = insertTcsPerfilRegraModulo, Original = null, Operation = ChangeOperation.Insert, Representation = entity, Mark = "serviceContext6" });
 	                  foreach(var insertedEntity in result) insertedEntity.KeysForRefresh.Add("IdPerfilRegraModulo", "IdPerfilRegraModulo");
 	                  break;
 	              case ChangeOperation.Update:
 	                  //Update TcsPerfilRegraModulo
 	                  Linx.Framework.BV.Perfil.TcsPerfilRegraModulo updateTcsPerfilRegraModulo = new Linx.Framework.BV.Perfil.TcsPerfilRegraModulo() {
 	                  IdModulo = entity.IdModulo,
 	                  IdPerfil = entity.IdPerfil,
 	                  IdPerfilRegraModulo = entity.IdPerfilRegraModulo,
 	                  LxRegraAcessoModulo = entity.LxRegraAcessoModulo
 	                  };
 	                  //Original Definition
 	                  Linx.Framework.BV.Perfil.TcsPerfilRegraModulo updateTcsPerfilRegraModuloOriginal = (original == null ? null : new Linx.Framework.BV.Perfil.TcsPerfilRegraModulo() {
 	                  IdModulo = original.IdModulo,
 	                  IdPerfil = original.IdPerfil,
 	                  IdPerfilRegraModulo = original.IdPerfilRegraModulo,
 	                  LxRegraAcessoModulo = original.LxRegraAcessoModulo
 	                  });
 	                  result.Add(new EntityChange() { Entity = updateTcsPerfilRegraModulo, Original = updateTcsPerfilRegraModuloOriginal, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext6" });
 	                  break;
 	              default:
 	                  break;
 	          }
 	          return result;
 	    }		
			
	  
 	    //Save All Representations Of Entity TcsPerfilUsuario
 	    [Ignore]
 	    private void SaveBufferRepresentationsOfTcsPerfilUsuario(List<EntityChange> entityChanges)
 	    {
 	      foreach (ChangeSetEntry entry in this.ChangeSet.ChangeSetEntries.Where(e => e.Entity is TcsPerfilUsuario && e.Entity.GetType().Name == "TcsPerfilUsuario"))
 	      {
 	          TcsPerfilUsuario entity = (TcsPerfilUsuario)entry.Entity;
 	          entityChanges.AddRange(this.GetRepresentations(entity, (this.ChangeSet.GetChangeOperation(entity) == ChangeOperation.Update ? this.ChangeSet.GetOriginal(entity) : null), this.ChangeSet.GetChangeOperation(entity)));
 	      }
 	    }
 
 	    //Get Representation Of TcsPerfilUsuario
 	    [Ignore]
 	    private List<EntityChange> GetRepresentations(TcsPerfilUsuario entity, TcsPerfilUsuario original, ChangeOperation operation)
 	    {
 	          List<EntityChange> result = new List<EntityChange>();
 	          switch (operation)
 	          {
 	              case ChangeOperation.None:
 	                  //None TcsUsuarioPerfil
 	                  Linx.Framework.BV.Perfil.TcsUsuarioPerfil noneTcsUsuarioPerfil = new Linx.Framework.BV.Perfil.TcsUsuarioPerfil() {
 	                  IdPerfil = entity.IdPerfil,
 	                  IdTcsUsuarioPerfil = entity.IdTcsUsuarioPerfil,
 	                  IdUsuario = entity.IdUsuario
 	                  };
 	                  result.Add(new EntityChange() { Entity = noneTcsUsuarioPerfil, Original = noneTcsUsuarioPerfil, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext6" });
 	                  break;
 	              case ChangeOperation.Delete:
 	                  //Delete TcsUsuarioPerfil
 	                  Linx.Framework.BV.Perfil.TcsUsuarioPerfil deleteTcsUsuarioPerfil = new Linx.Framework.BV.Perfil.TcsUsuarioPerfil() {
 	                  IdPerfil = entity.IdPerfil,
 	                  IdTcsUsuarioPerfil = entity.IdTcsUsuarioPerfil,
 	                  IdUsuario = entity.IdUsuario
 	                  };
 	                  result.Add(new EntityChange() { Entity = deleteTcsUsuarioPerfil, Original = null, Operation = ChangeOperation.Delete, Representation = null, Mark = "serviceContext6" });
 	                  break;
 	              case ChangeOperation.Insert:
 	                  //Insert TcsUsuarioPerfil
 	                  Linx.Framework.BV.Perfil.TcsUsuarioPerfil insertTcsUsuarioPerfil = new Linx.Framework.BV.Perfil.TcsUsuarioPerfil() {
 	                  IdPerfil = entity.IdPerfil,
 	                  IdTcsUsuarioPerfil = entity.IdTcsUsuarioPerfil,
 	                  IdUsuario = entity.IdUsuario
 	                  };
 	                  result.Add(new EntityChange() { Entity = insertTcsUsuarioPerfil, Original = null, Operation = ChangeOperation.Insert, Representation = entity, Mark = "serviceContext6" });
 	                  foreach(var insertedEntity in result) insertedEntity.KeysForRefresh.Add("IdTcsUsuarioPerfil", "IdTcsUsuarioPerfil");
 	                  break;
 	              case ChangeOperation.Update:
 	                  //Update TcsUsuarioPerfil
 	                  Linx.Framework.BV.Perfil.TcsUsuarioPerfil updateTcsUsuarioPerfil = new Linx.Framework.BV.Perfil.TcsUsuarioPerfil() {
 	                  IdPerfil = entity.IdPerfil,
 	                  IdTcsUsuarioPerfil = entity.IdTcsUsuarioPerfil,
 	                  IdUsuario = entity.IdUsuario
 	                  };
 	                  //Original Definition
 	                  Linx.Framework.BV.Perfil.TcsUsuarioPerfil updateTcsUsuarioPerfilOriginal = (original == null ? null : new Linx.Framework.BV.Perfil.TcsUsuarioPerfil() {
 	                  IdPerfil = original.IdPerfil,
 	                  IdTcsUsuarioPerfil = original.IdTcsUsuarioPerfil,
 	                  IdUsuario = original.IdUsuario
 	                  });
 	                  result.Add(new EntityChange() { Entity = updateTcsUsuarioPerfil, Original = updateTcsUsuarioPerfilOriginal, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext6" });
 	                  break;
 	              default:
 	                  break;
 	          }
 	          return result;
 	    }		
			
	  
 	    //Save All Representations Of Entity TcsEmpresaGpecon
 	    [Ignore]
 	    private void SaveBufferRepresentationsOfTcsEmpresaGpecon(List<EntityChange> entityChanges)
 	    {
 	      foreach (ChangeSetEntry entry in this.ChangeSet.ChangeSetEntries.Where(e => e.Entity is TcsEmpresaGpecon && e.Entity.GetType().Name == "TcsEmpresaGpecon"))
 	      {
 	          TcsEmpresaGpecon entity = (TcsEmpresaGpecon)entry.Entity;
 	          entityChanges.AddRange(this.GetRepresentations(entity, (this.ChangeSet.GetChangeOperation(entity) == ChangeOperation.Update ? this.ChangeSet.GetOriginal(entity) : null), this.ChangeSet.GetChangeOperation(entity)));
 	      }
 	    }
 
 	    //Get Representation Of TcsEmpresaGpecon
 	    [Ignore]
 	    private List<EntityChange> GetRepresentations(TcsEmpresaGpecon entity, TcsEmpresaGpecon original, ChangeOperation operation)
 	    {
 	          List<EntityChange> result = new List<EntityChange>();
 	          switch (operation)
 	          {
 	              case ChangeOperation.None:
 	                  //None TcsEmpresaGpeconP
 	                  Linx.Framework.BV.Empresa.TcsEmpresaGpeconP noneTcsEmpresaGpeconP = new Linx.Framework.BV.Empresa.TcsEmpresaGpeconP() {
 	                  IdLinx = entity.IdLinx,
 	                  IdLinxGpecon = entity.IdLinxGpecon
 	                  };
 	                  result.Add(new EntityChange() { Entity = noneTcsEmpresaGpeconP, Original = noneTcsEmpresaGpeconP, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext2" });
 	                  break;
 	              case ChangeOperation.Delete:
 	                  //Delete TcsEmpresaGpeconP
 	                  Linx.Framework.BV.Empresa.TcsEmpresaGpeconP deleteTcsEmpresaGpeconP = new Linx.Framework.BV.Empresa.TcsEmpresaGpeconP() {
 	                  IdLinx = entity.IdLinx,
 	                  IdLinxGpecon = entity.IdLinxGpecon
 	                  };
 	                  result.Add(new EntityChange() { Entity = deleteTcsEmpresaGpeconP, Original = null, Operation = ChangeOperation.Delete, Representation = null, Mark = "serviceContext2" });
 	                  break;
 	              case ChangeOperation.Insert:
 	                  //Insert TcsEmpresaGpeconP
 	                  Linx.Framework.BV.Empresa.TcsEmpresaGpeconP insertTcsEmpresaGpeconP = new Linx.Framework.BV.Empresa.TcsEmpresaGpeconP() {
 	                  IdLinx = entity.IdLinx,
 	                  IdLinxGpecon = entity.IdLinxGpecon
 	                  };
 	                  result.Add(new EntityChange() { Entity = insertTcsEmpresaGpeconP, Original = null, Operation = ChangeOperation.Insert, Representation = entity, Mark = "serviceContext2" });
 	                  foreach(var insertedEntity in result) insertedEntity.KeysForRefresh.Add("IdLinx", "IdLinx");
 	                  foreach(var insertedEntity in result) insertedEntity.KeysForRefresh.Add("IdLinxGpecon", "IdLinxGpecon");
 	                  break;
 	              case ChangeOperation.Update:
 	                  //Update TcsEmpresaGpeconP
 	                  Linx.Framework.BV.Empresa.TcsEmpresaGpeconP updateTcsEmpresaGpeconP = new Linx.Framework.BV.Empresa.TcsEmpresaGpeconP() {
 	                  IdLinx = entity.IdLinx,
 	                  IdLinxGpecon = entity.IdLinxGpecon
 	                  };
 	                  //Original Definition
 	                  Linx.Framework.BV.Empresa.TcsEmpresaGpeconP updateTcsEmpresaGpeconPOriginal = (original == null ? null : new Linx.Framework.BV.Empresa.TcsEmpresaGpeconP() {
 	                  IdLinx = original.IdLinx,
 	                  IdLinxGpecon = original.IdLinxGpecon
 	                  });
 	                  result.Add(new EntityChange() { Entity = updateTcsEmpresaGpeconP, Original = updateTcsEmpresaGpeconPOriginal, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext2" });
 	                  break;
 	              default:
 	                  break;
 	          }
 	          return result;
 	    }		
			
	  
 	    //Save All Representations Of Entity TbcFilial
 	    [Ignore]
 	    private void SaveBufferRepresentationsOfTbcFilial(List<EntityChange> entityChanges)
 	    {
 	      foreach (ChangeSetEntry entry in this.ChangeSet.ChangeSetEntries.Where(e => e.Entity is TbcFilial && e.Entity.GetType().Name == "TbcFilial"))
 	      {
 	          TbcFilial entity = (TbcFilial)entry.Entity;
 	          entityChanges.AddRange(this.GetRepresentations(entity, (this.ChangeSet.GetChangeOperation(entity) == ChangeOperation.Update ? this.ChangeSet.GetOriginal(entity) : null), this.ChangeSet.GetChangeOperation(entity)));
 	      }
 	    }
 
 	    //Get Representation Of TbcFilial
 	    [Ignore]
 	    private List<EntityChange> GetRepresentations(TbcFilial entity, TbcFilial original, ChangeOperation operation)
 	    {
 	          List<EntityChange> result = new List<EntityChange>();
 	          switch (operation)
 	          {
 	              case ChangeOperation.None:
 	                  //None TbcFilial
 	                  Linx.CadastroBase.BV.CadastroPfj.TbcFilial noneTbcFilial = new Linx.CadastroBase.BV.CadastroPfj.TbcFilial() {
 	                  Bairro = entity.Bairro,
 	                  BandeiraRede = entity.BandeiraRede,
 	                  Cep = entity.Cep,
 	                  CnpjCpf = entity.CnpjCpf,
 	                  CodDeposito = entity.CodDeposito,
 	                  CodigoFilial = entity.CodigoFilial,
 	                  CodigoPfj = entity.CodigoPfj,
 	                  Complemento = entity.Complemento,
 	                  DddCelular = entity.DddCelular,
 	                  DddFixo = entity.DddFixo,
 	                  Email = entity.Email,
 	                  FoneCelular = entity.FoneCelular,
 	                  FoneFixo = entity.FoneFixo,
 	                  IdFilialPfj = entity.IdFilialPfj,
 	                  IdGpecon = entity.IdGpecon,
 	                  IdLjvCanalVenda = entity.IdLjvCanalVenda,
 	                  IdMatrizContabil = entity.IdMatrizContabil,
 	                  IdPfj = entity.IdPfj,
 	                  IncluiDeposito = entity.IncluiDeposito,
 	                  IncluiLoja = entity.IncluiLoja,
 	                  IndicaEstrangeiro = entity.IndicaEstrangeiro,
 	                  IndicaFilial = entity.IndicaFilial,
 	                  IndicaLoja = entity.IndicaLoja,
 	                  IndicaMatrizContabil = entity.IndicaMatrizContabil,
 	                  InscrEstadual = entity.InscrEstadual,
 	                  Logradouro = entity.Logradouro,
 	                  LxPfjFisicaJuridica = entity.LxPfjFisicaJuridica,
 	                  LxTipoLogradouro = entity.LxTipoLogradouro,
 	                  Municipio = entity.Municipio,
 	                  NomeFantasiaApelido = entity.NomeFantasiaApelido,
 	                  NomeFilial = entity.NomeFilial,
 	                  Numero = entity.Numero,
 	                  ObsEndereco = entity.ObsEndereco,
 	                  Pais = entity.Pais,
 	                  RazaoSocialNomeCompleto = entity.RazaoSocialNomeCompleto,
 	                  Uf = entity.Uf
 	                  };
 	                  result.Add(new EntityChange() { Entity = noneTbcFilial, Original = noneTbcFilial, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext0" });
 	                  break;
 	              case ChangeOperation.Delete:
 	                  //Delete TbcFilial
 	                  Linx.CadastroBase.BV.CadastroPfj.TbcFilial deleteTbcFilial = new Linx.CadastroBase.BV.CadastroPfj.TbcFilial() {
 	                  Bairro = entity.Bairro,
 	                  BandeiraRede = entity.BandeiraRede,
 	                  Cep = entity.Cep,
 	                  CnpjCpf = entity.CnpjCpf,
 	                  CodDeposito = entity.CodDeposito,
 	                  CodigoFilial = entity.CodigoFilial,
 	                  CodigoPfj = entity.CodigoPfj,
 	                  Complemento = entity.Complemento,
 	                  DddCelular = entity.DddCelular,
 	                  DddFixo = entity.DddFixo,
 	                  Email = entity.Email,
 	                  FoneCelular = entity.FoneCelular,
 	                  FoneFixo = entity.FoneFixo,
 	                  IdFilialPfj = entity.IdFilialPfj,
 	                  IdGpecon = entity.IdGpecon,
 	                  IdLjvCanalVenda = entity.IdLjvCanalVenda,
 	                  IdMatrizContabil = entity.IdMatrizContabil,
 	                  IdPfj = entity.IdPfj,
 	                  IncluiDeposito = entity.IncluiDeposito,
 	                  IncluiLoja = entity.IncluiLoja,
 	                  IndicaEstrangeiro = entity.IndicaEstrangeiro,
 	                  IndicaFilial = entity.IndicaFilial,
 	                  IndicaLoja = entity.IndicaLoja,
 	                  IndicaMatrizContabil = entity.IndicaMatrizContabil,
 	                  InscrEstadual = entity.InscrEstadual,
 	                  Logradouro = entity.Logradouro,
 	                  LxPfjFisicaJuridica = entity.LxPfjFisicaJuridica,
 	                  LxTipoLogradouro = entity.LxTipoLogradouro,
 	                  Municipio = entity.Municipio,
 	                  NomeFantasiaApelido = entity.NomeFantasiaApelido,
 	                  NomeFilial = entity.NomeFilial,
 	                  Numero = entity.Numero,
 	                  ObsEndereco = entity.ObsEndereco,
 	                  Pais = entity.Pais,
 	                  RazaoSocialNomeCompleto = entity.RazaoSocialNomeCompleto,
 	                  Uf = entity.Uf
 	                  };
 	                  result.Add(new EntityChange() { Entity = deleteTbcFilial, Original = null, Operation = ChangeOperation.Delete, Representation = null, Mark = "serviceContext0" });
 	                  break;
 	              case ChangeOperation.Insert:
 	                  //Insert TbcFilial
 	                  Linx.CadastroBase.BV.CadastroPfj.TbcFilial insertTbcFilial = new Linx.CadastroBase.BV.CadastroPfj.TbcFilial() {
 	                  Bairro = entity.Bairro,
 	                  BandeiraRede = entity.BandeiraRede,
 	                  Cep = entity.Cep,
 	                  CnpjCpf = entity.CnpjCpf,
 	                  CodDeposito = entity.CodDeposito,
 	                  CodigoFilial = entity.CodigoFilial,
 	                  CodigoPfj = entity.CodigoPfj,
 	                  Complemento = entity.Complemento,
 	                  DddCelular = entity.DddCelular,
 	                  DddFixo = entity.DddFixo,
 	                  Email = entity.Email,
 	                  FoneCelular = entity.FoneCelular,
 	                  FoneFixo = entity.FoneFixo,
 	                  IdFilialPfj = entity.IdFilialPfj,
 	                  IdGpecon = entity.IdGpecon,
 	                  IdLjvCanalVenda = entity.IdLjvCanalVenda,
 	                  IdMatrizContabil = entity.IdMatrizContabil,
 	                  IdPfj = entity.IdPfj,
 	                  IncluiDeposito = entity.IncluiDeposito,
 	                  IncluiLoja = entity.IncluiLoja,
 	                  IndicaEstrangeiro = entity.IndicaEstrangeiro,
 	                  IndicaFilial = entity.IndicaFilial,
 	                  IndicaLoja = entity.IndicaLoja,
 	                  IndicaMatrizContabil = entity.IndicaMatrizContabil,
 	                  InscrEstadual = entity.InscrEstadual,
 	                  Logradouro = entity.Logradouro,
 	                  LxPfjFisicaJuridica = entity.LxPfjFisicaJuridica,
 	                  LxTipoLogradouro = entity.LxTipoLogradouro,
 	                  Municipio = entity.Municipio,
 	                  NomeFantasiaApelido = entity.NomeFantasiaApelido,
 	                  NomeFilial = entity.NomeFilial,
 	                  Numero = entity.Numero,
 	                  ObsEndereco = entity.ObsEndereco,
 	                  Pais = entity.Pais,
 	                  RazaoSocialNomeCompleto = entity.RazaoSocialNomeCompleto,
 	                  Uf = entity.Uf
 	                  };
 	                  result.Add(new EntityChange() { Entity = insertTbcFilial, Original = null, Operation = ChangeOperation.Insert, Representation = entity, Mark = "serviceContext0" });
 	                  foreach(var insertedEntity in result) insertedEntity.KeysForRefresh.Add("IdFilialPfj", "IdFilialPfj");
 	                  foreach(var insertedEntity in result) insertedEntity.KeysForRefresh.Add("IdMatrizContabil", "IdMatrizContabil");
 	                  foreach(var insertedEntity in result) insertedEntity.KeysForRefresh.Add("IdPfj", "IdPfj");
 	                  break;
 	              case ChangeOperation.Update:
 	                  //Update TbcFilial
 	                  Linx.CadastroBase.BV.CadastroPfj.TbcFilial updateTbcFilial = new Linx.CadastroBase.BV.CadastroPfj.TbcFilial() {
 	                  Bairro = entity.Bairro,
 	                  BandeiraRede = entity.BandeiraRede,
 	                  Cep = entity.Cep,
 	                  CnpjCpf = entity.CnpjCpf,
 	                  CodDeposito = entity.CodDeposito,
 	                  CodigoFilial = entity.CodigoFilial,
 	                  CodigoPfj = entity.CodigoPfj,
 	                  Complemento = entity.Complemento,
 	                  DddCelular = entity.DddCelular,
 	                  DddFixo = entity.DddFixo,
 	                  Email = entity.Email,
 	                  FoneCelular = entity.FoneCelular,
 	                  FoneFixo = entity.FoneFixo,
 	                  IdFilialPfj = entity.IdFilialPfj,
 	                  IdGpecon = entity.IdGpecon,
 	                  IdLjvCanalVenda = entity.IdLjvCanalVenda,
 	                  IdMatrizContabil = entity.IdMatrizContabil,
 	                  IdPfj = entity.IdPfj,
 	                  IncluiDeposito = entity.IncluiDeposito,
 	                  IncluiLoja = entity.IncluiLoja,
 	                  IndicaEstrangeiro = entity.IndicaEstrangeiro,
 	                  IndicaFilial = entity.IndicaFilial,
 	                  IndicaLoja = entity.IndicaLoja,
 	                  IndicaMatrizContabil = entity.IndicaMatrizContabil,
 	                  InscrEstadual = entity.InscrEstadual,
 	                  Logradouro = entity.Logradouro,
 	                  LxPfjFisicaJuridica = entity.LxPfjFisicaJuridica,
 	                  LxTipoLogradouro = entity.LxTipoLogradouro,
 	                  Municipio = entity.Municipio,
 	                  NomeFantasiaApelido = entity.NomeFantasiaApelido,
 	                  NomeFilial = entity.NomeFilial,
 	                  Numero = entity.Numero,
 	                  ObsEndereco = entity.ObsEndereco,
 	                  Pais = entity.Pais,
 	                  RazaoSocialNomeCompleto = entity.RazaoSocialNomeCompleto,
 	                  Uf = entity.Uf
 	                  };
 	                  //Original Definition
 	                  Linx.CadastroBase.BV.CadastroPfj.TbcFilial updateTbcFilialOriginal = (original == null ? null : new Linx.CadastroBase.BV.CadastroPfj.TbcFilial() {
 	                  Bairro = original.Bairro,
 	                  BandeiraRede = original.BandeiraRede,
 	                  Cep = original.Cep,
 	                  CnpjCpf = original.CnpjCpf,
 	                  CodDeposito = original.CodDeposito,
 	                  CodigoFilial = original.CodigoFilial,
 	                  CodigoPfj = original.CodigoPfj,
 	                  Complemento = original.Complemento,
 	                  DddCelular = original.DddCelular,
 	                  DddFixo = original.DddFixo,
 	                  Email = original.Email,
 	                  FoneCelular = original.FoneCelular,
 	                  FoneFixo = original.FoneFixo,
 	                  IdFilialPfj = original.IdFilialPfj,
 	                  IdGpecon = original.IdGpecon,
 	                  IdLjvCanalVenda = original.IdLjvCanalVenda,
 	                  IdMatrizContabil = original.IdMatrizContabil,
 	                  IdPfj = original.IdPfj,
 	                  IncluiDeposito = original.IncluiDeposito,
 	                  IncluiLoja = original.IncluiLoja,
 	                  IndicaEstrangeiro = original.IndicaEstrangeiro,
 	                  IndicaFilial = original.IndicaFilial,
 	                  IndicaLoja = original.IndicaLoja,
 	                  IndicaMatrizContabil = original.IndicaMatrizContabil,
 	                  InscrEstadual = original.InscrEstadual,
 	                  Logradouro = original.Logradouro,
 	                  LxPfjFisicaJuridica = original.LxPfjFisicaJuridica,
 	                  LxTipoLogradouro = original.LxTipoLogradouro,
 	                  Municipio = original.Municipio,
 	                  NomeFantasiaApelido = original.NomeFantasiaApelido,
 	                  NomeFilial = original.NomeFilial,
 	                  Numero = original.Numero,
 	                  ObsEndereco = original.ObsEndereco,
 	                  Pais = original.Pais,
 	                  RazaoSocialNomeCompleto = original.RazaoSocialNomeCompleto,
 	                  Uf = original.Uf
 	                  });
 	                  result.Add(new EntityChange() { Entity = updateTbcFilial, Original = updateTbcFilialOriginal, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext0" });
 	                  break;
 	              default:
 	                  break;
 	          }
 	          return result;
 	    }		
			
	  
 	    //Save All Representations Of Entity TbcGrupoEconomico
 	    [Ignore]
 	    private void SaveBufferRepresentationsOfTbcGrupoEconomico(List<EntityChange> entityChanges)
 	    {
 	      foreach (ChangeSetEntry entry in this.ChangeSet.ChangeSetEntries.Where(e => e.Entity is TbcGrupoEconomico && e.Entity.GetType().Name == "TbcGrupoEconomico"))
 	      {
 	          TbcGrupoEconomico entity = (TbcGrupoEconomico)entry.Entity;
 	          entityChanges.AddRange(this.GetRepresentations(entity, (this.ChangeSet.GetChangeOperation(entity) == ChangeOperation.Update ? this.ChangeSet.GetOriginal(entity) : null), this.ChangeSet.GetChangeOperation(entity)));
 	      }
 	    }
 
 	    //Get Representation Of TbcGrupoEconomico
 	    [Ignore]
 	    private List<EntityChange> GetRepresentations(TbcGrupoEconomico entity, TbcGrupoEconomico original, ChangeOperation operation)
 	    {
 	          List<EntityChange> result = new List<EntityChange>();
 	          switch (operation)
 	          {
 	              case ChangeOperation.None:
 	                  //None TbcGrupoEconomico
 	                  Linx.Operacional.CadastroBase.BV.GrupoEconomico.TbcGrupoEconomico noneTbcGrupoEconomico = new Linx.Operacional.CadastroBase.BV.GrupoEconomico.TbcGrupoEconomico() {
 	                  DescGrupoEconomico = entity.DescGrupoEconomico,
 	                  IdGpeconCadastro = entity.IdGpeconCadastro
 	                  };
 	                  result.Add(new EntityChange() { Entity = noneTbcGrupoEconomico, Original = noneTbcGrupoEconomico, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext11" });
 	                  break;
 	              case ChangeOperation.Delete:
 	                  //Delete TbcGrupoEconomico
 	                  Linx.Operacional.CadastroBase.BV.GrupoEconomico.TbcGrupoEconomico deleteTbcGrupoEconomico = new Linx.Operacional.CadastroBase.BV.GrupoEconomico.TbcGrupoEconomico() {
 	                  DescGrupoEconomico = entity.DescGrupoEconomico,
 	                  IdGpeconCadastro = entity.IdGpeconCadastro
 	                  };
 	                  result.Add(new EntityChange() { Entity = deleteTbcGrupoEconomico, Original = null, Operation = ChangeOperation.Delete, Representation = null, Mark = "serviceContext11" });
 	                  break;
 	              case ChangeOperation.Insert:
 	                  //Insert TbcGrupoEconomico
 	                  Linx.Operacional.CadastroBase.BV.GrupoEconomico.TbcGrupoEconomico insertTbcGrupoEconomico = new Linx.Operacional.CadastroBase.BV.GrupoEconomico.TbcGrupoEconomico() {
 	                  DescGrupoEconomico = entity.DescGrupoEconomico,
 	                  IdGpeconCadastro = entity.IdGpeconCadastro
 	                  };
 	                  result.Add(new EntityChange() { Entity = insertTbcGrupoEconomico, Original = null, Operation = ChangeOperation.Insert, Representation = entity, Mark = "serviceContext11" });
 	                  foreach(var insertedEntity in result) insertedEntity.KeysForRefresh.Add("IdGpeconCadastro", "IdGpeconCadastro");
 	                  break;
 	              case ChangeOperation.Update:
 	                  //Update TbcGrupoEconomico
 	                  Linx.Operacional.CadastroBase.BV.GrupoEconomico.TbcGrupoEconomico updateTbcGrupoEconomico = new Linx.Operacional.CadastroBase.BV.GrupoEconomico.TbcGrupoEconomico() {
 	                  DescGrupoEconomico = entity.DescGrupoEconomico,
 	                  IdGpeconCadastro = entity.IdGpeconCadastro
 	                  };
 	                  //Original Definition
 	                  Linx.Operacional.CadastroBase.BV.GrupoEconomico.TbcGrupoEconomico updateTbcGrupoEconomicoOriginal = (original == null ? null : new Linx.Operacional.CadastroBase.BV.GrupoEconomico.TbcGrupoEconomico() {
 	                  DescGrupoEconomico = original.DescGrupoEconomico,
 	                  IdGpeconCadastro = original.IdGpeconCadastro
 	                  });
 	                  result.Add(new EntityChange() { Entity = updateTbcGrupoEconomico, Original = updateTbcGrupoEconomicoOriginal, Operation = ChangeOperation.Update, Representation = null, Mark = "serviceContext11" });
 	                  break;
 	              default:
 	                  break;
 	          }
 	          return result;
 	    }		
	
	    #endregion Save Representations.



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
	
		

	        if (entityName.InList("Linx.Framework.Setup.LinxAutoSetup.TcsEmpresaAutenticacao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsEmpresaAutenticacao",
	        			NameSpace = "Linx.Framework.Setup.LinxAutoSetup",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsEmpresaAutenticacao",
	        			ClearMethodName = "ClearTcsEmpresaAutenticacao",
	        			QueryMethodName  = "GetPagedTcsEmpresaAutenticacao",	
	        			CountingMethodName  = "GetTcsEmpresaAutenticacao" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsEmpresaAutenticacao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsEmpresaAutenticacao"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.Setup.LinxAutoSetup.TcsEmpresaAutenticacao", "Linx.Framework.Setup.LinxAutoSetup.TcsEmpresaAutenticacaoModulo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsEmpresaAutenticacaoModulo",
	        			NameSpace = "Linx.Framework.Setup.LinxAutoSetup",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsEmpresaAutenticacao",	
	        			DisplayName = "TcsEmpresaAutenticacaoModulo",
	        			ClearMethodName = "ClearTcsEmpresaAutenticacaoModulo",
	        			QueryMethodName  = "GetPagedTcsEmpresaAutenticacaoModulo",	
	        			CountingMethodName  = "GetTcsEmpresaAutenticacaoModulo" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsEmpresaAutenticacaoModulo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsEmpresaAutenticacaoModulo"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.Setup.LinxAutoSetup.TcsUsuarioAutenticacao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsUsuarioAutenticacao",
	        			NameSpace = "Linx.Framework.Setup.LinxAutoSetup",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsUsuarioAutenticacao",
	        			ClearMethodName = "ClearTcsUsuarioAutenticacao",
	        			QueryMethodName  = "GetPagedTcsUsuarioAutenticacao",	
	        			CountingMethodName  = "GetTcsUsuarioAutenticacao" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsUsuarioAutenticacao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsUsuarioAutenticacao"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.Setup.LinxAutoSetup.TcsUsuarioAutenticacao", "Linx.Framework.Setup.LinxAutoSetup.TcsUsuarioAutenticacaoAcesso"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsUsuarioAutenticacaoAcesso",
	        			NameSpace = "Linx.Framework.Setup.LinxAutoSetup",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsUsuarioAutenticacao",	
	        			DisplayName = "TcsUsuarioAutenticacaoAcesso",
	        			ClearMethodName = "ClearTcsUsuarioAutenticacaoAcesso",
	        			QueryMethodName  = "GetPagedTcsUsuarioAutenticacaoAcesso",	
	        			CountingMethodName  = "GetTcsUsuarioAutenticacaoAcesso" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsUsuarioAutenticacaoAcesso"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsUsuarioAutenticacaoAcesso"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.Setup.LinxAutoSetup.TcsUsuarioPerfil"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsUsuarioPerfil",
	        			NameSpace = "Linx.Framework.Setup.LinxAutoSetup",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsUsuarioPerfil",
	        			ClearMethodName = "ClearTcsUsuarioPerfil",
	        			QueryMethodName  = "GetPagedTcsUsuarioPerfil",	
	        			CountingMethodName  = "GetTcsUsuarioPerfil" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsUsuarioPerfil"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsUsuarioPerfil"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.Setup.LinxAutoSetup.TcsAmbiente"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsAmbiente",
	        			NameSpace = "Linx.Framework.Setup.LinxAutoSetup",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsAmbiente",
	        			ClearMethodName = "ClearTcsAmbiente",
	        			QueryMethodName  = "GetPagedTcsAmbiente",	
	        			CountingMethodName  = "GetTcsAmbiente" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsAmbiente"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsAmbiente"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.Setup.LinxAutoSetup.TcsAmbiente", "Linx.Framework.Setup.LinxAutoSetup.TcsAmbienteConexao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsAmbienteConexao",
	        			NameSpace = "Linx.Framework.Setup.LinxAutoSetup",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsAmbiente",	
	        			DisplayName = "TcsAmbienteConexao",
	        			ClearMethodName = "ClearTcsAmbienteConexao",
	        			QueryMethodName  = "GetPagedTcsAmbienteConexao",	
	        			CountingMethodName  = "GetTcsAmbienteConexao" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsAmbienteConexao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsAmbienteConexao"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.Setup.LinxAutoSetup.TcsAmbiente", "Linx.Framework.Setup.LinxAutoSetup.TcsAmbienteUsuarioAcesso"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsAmbienteUsuarioAcesso",
	        			NameSpace = "Linx.Framework.Setup.LinxAutoSetup",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsAmbiente",	
	        			DisplayName = "TcsAmbienteUsuarioAcesso",
	        			ClearMethodName = "ClearTcsAmbienteUsuarioAcesso",
	        			QueryMethodName  = "GetPagedTcsAmbienteUsuarioAcesso",	
	        			CountingMethodName  = "GetTcsAmbienteUsuarioAcesso" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsAmbienteUsuarioAcesso"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsAmbienteUsuarioAcesso"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.Setup.LinxAutoSetup.TcsModuloGrupo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsModuloGrupo",
	        			NameSpace = "Linx.Framework.Setup.LinxAutoSetup",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsModuloGrupo",
	        			ClearMethodName = "ClearTcsModuloGrupo",
	        			QueryMethodName  = "GetPagedTcsModuloGrupo",	
	        			CountingMethodName  = "GetTcsModuloGrupo" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsModuloGrupo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsModuloGrupo"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.Setup.LinxAutoSetup.TcsModuloGrupo", "Linx.Framework.Setup.LinxAutoSetup.TcsModuloGrupoDetalhe"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsModuloGrupoDetalhe",
	        			NameSpace = "Linx.Framework.Setup.LinxAutoSetup",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsModuloGrupo",	
	        			DisplayName = "TcsModuloGrupoDetalhe",
	        			ClearMethodName = "ClearTcsModuloGrupoDetalhe",
	        			QueryMethodName  = "GetPagedTcsModuloGrupoDetalhe",	
	        			CountingMethodName  = "GetTcsModuloGrupoDetalhe" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsModuloGrupoDetalhe"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsModuloGrupoDetalhe"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.Setup.LinxAutoSetup.TcsParametroValor"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsParametroValor",
	        			NameSpace = "Linx.Framework.Setup.LinxAutoSetup",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsParametroValor",
	        			ClearMethodName = "ClearTcsParametroValor",
	        			QueryMethodName  = "GetPagedTcsParametroValor",	
	        			CountingMethodName  = "GetTcsParametroValor" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsParametroValor"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsParametroValor"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.Setup.LinxAutoSetup.TcsPerfil"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsPerfil",
	        			NameSpace = "Linx.Framework.Setup.LinxAutoSetup",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsPerfil",
	        			ClearMethodName = "ClearTcsPerfil",
	        			QueryMethodName  = "GetPagedTcsPerfil",	
	        			CountingMethodName  = "GetTcsPerfil" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsPerfil"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsPerfil"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.Setup.LinxAutoSetup.TcsPerfil", "Linx.Framework.Setup.LinxAutoSetup.TcsPerfilRegraModulo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsPerfilRegraModulo",
	        			NameSpace = "Linx.Framework.Setup.LinxAutoSetup",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsPerfil",	
	        			DisplayName = "TcsPerfilRegraModulo",
	        			ClearMethodName = "ClearTcsPerfilRegraModulo",
	        			QueryMethodName  = "GetPagedTcsPerfilRegraModulo",	
	        			CountingMethodName  = "GetTcsPerfilRegraModulo" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsPerfilRegraModulo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsPerfilRegraModulo"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.Setup.LinxAutoSetup.TcsPerfil", "Linx.Framework.Setup.LinxAutoSetup.TcsPerfilUsuario"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsPerfilUsuario",
	        			NameSpace = "Linx.Framework.Setup.LinxAutoSetup",
	        			HasQuickSearch = false,
	        			ParentClassName = "TcsPerfil",	
	        			DisplayName = "TcsPerfilUsuario",
	        			ClearMethodName = "ClearTcsPerfilUsuario",
	        			QueryMethodName  = "GetPagedTcsPerfilUsuario",	
	        			CountingMethodName  = "GetTcsPerfilUsuario" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsPerfilUsuario"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsPerfilUsuario"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.Setup.LinxAutoSetup.AmbienteInfo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "AmbienteInfo",
	        			NameSpace = "Linx.Framework.Setup.LinxAutoSetup",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "AmbienteInfo",
	        			ClearMethodName = "ClearAmbienteInfo",
	        			QueryMethodName  = "GetPagedAmbienteInfo",	
	        			CountingMethodName  = "GetAmbienteInfo" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.AmbienteInfo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.AmbienteInfo"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.Setup.LinxAutoSetup.TcsEmpresaGpecon"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsEmpresaGpecon",
	        			NameSpace = "Linx.Framework.Setup.LinxAutoSetup",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsEmpresaGpecon",
	        			ClearMethodName = "ClearTcsEmpresaGpecon",
	        			QueryMethodName  = "GetPagedTcsEmpresaGpecon",	
	        			CountingMethodName  = "GetTcsEmpresaGpecon" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsEmpresaGpecon"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsEmpresaGpecon"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.Setup.LinxAutoSetup.TcsAmbienteInfo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsAmbienteInfo",
	        			NameSpace = "Linx.Framework.Setup.LinxAutoSetup",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsAmbienteInfo",
	        			ClearMethodName = "ClearTcsAmbienteInfo",
	        			QueryMethodName  = "GetPagedTcsAmbienteInfo",	
	        			CountingMethodName  = "GetTcsAmbienteInfo" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsAmbienteInfo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsAmbienteInfo"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.Setup.LinxAutoSetup.TcsParametroAutorizacao"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TcsParametroAutorizacao",
	        			NameSpace = "Linx.Framework.Setup.LinxAutoSetup",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TcsParametroAutorizacao",
	        			ClearMethodName = "ClearTcsParametroAutorizacao",
	        			QueryMethodName  = "GetPagedTcsParametroAutorizacao",	
	        			CountingMethodName  = "GetTcsParametroAutorizacao" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsParametroAutorizacao"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TcsParametroAutorizacao"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.Setup.LinxAutoSetup.MultimarcaInfo"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "MultimarcaInfo",
	        			NameSpace = "Linx.Framework.Setup.LinxAutoSetup",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "MultimarcaInfo",
	        			ClearMethodName = "ClearMultimarcaInfo",
	        			QueryMethodName  = "GetPagedMultimarcaInfo",	
	        			CountingMethodName  = "GetMultimarcaInfo" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.MultimarcaInfo"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.MultimarcaInfo"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.Setup.LinxAutoSetup.TbcFilial"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TbcFilial",
	        			NameSpace = "Linx.Framework.Setup.LinxAutoSetup",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TbcFilial",
	        			ClearMethodName = "ClearTbcFilial",
	        			QueryMethodName  = "GetPagedTbcFilial",	
	        			CountingMethodName  = "GetTbcFilial" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TbcFilial"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TbcFilial"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.Setup.LinxAutoSetup.TbcGrupoEconomico"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TbcGrupoEconomico",
	        			NameSpace = "Linx.Framework.Setup.LinxAutoSetup",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TbcGrupoEconomico",
	        			ClearMethodName = "ClearTbcGrupoEconomico",
	        			QueryMethodName  = "GetPagedTbcGrupoEconomico",	
	        			CountingMethodName  = "GetTbcGrupoEconomico" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TbcGrupoEconomico"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TbcGrupoEconomico"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.Setup.LinxAutoSetup.TbcBandeiraRede"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "TbcBandeiraRede",
	        			NameSpace = "Linx.Framework.Setup.LinxAutoSetup",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "TbcBandeiraRede",
	        			ClearMethodName = "ClearTbcBandeiraRede",
	        			QueryMethodName  = "GetPagedTbcBandeiraRede",	
	        			CountingMethodName  = "GetTbcBandeiraRede" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TbcBandeiraRede"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.TbcBandeiraRede"), forceAll: forceAll)
	        		});
	        }
		

	        if (entityName.InList("Linx.Framework.Setup.LinxAutoSetup.LjvCanalVenda"))
	        {
	        		result.Add(new LinxEntityReferenceInfo()
	        		{
	        			ClassName = "LjvCanalVenda",
	        			NameSpace = "Linx.Framework.Setup.LinxAutoSetup",
	        			HasQuickSearch = false,
	        			ParentClassName = null,	
	        			DisplayName = "LjvCanalVenda",
	        			ClearMethodName = "ClearLjvCanalVenda",
	        			QueryMethodName  = "GetPagedLjvCanalVenda",	
	        			CountingMethodName  = "GetLjvCanalVenda" + "Counting",	
	        			EdmEntityName = ObjectExtension.GetFunctionalPointOfType(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.LjvCanalVenda"), "EdmEntityName"),
	        			Properties = ObjectExtension.GetFunctionalPoints(Type.GetType("Linx.Framework.Setup.LinxAutoSetup.LjvCanalVenda"), forceAll: forceAll)
	        		});
	        }
	
            return result;
        }
	
	    [Ignore]
	    public string[] GetClientDomains(bool erp)
        {	
	    		if (erp)
	    		{

         		    return new string[] { "FrameworkSetup_ClientErpDataDomainsFactory", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.Setup.ClientResources.ClientErpDataDomainsFactory.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
	    		}
	    		else 
	    		{

         		    return new string[] { "FrameworkSetup_MobileDataDomains", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.Setup.ClientResources.MobileDataDomains.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
	    		}

        }

	    [Ignore]
	    public string[] GetClientService(bool erp)
        {	

	    		if (erp)
	    		{

         		    return new string[] { "FrameworkSetup_LinxAutoSetupClientErpService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.Setup.ClientResources.LinxAutoSetupClientErpService.res", System.Reflection.Assembly.GetExecutingAssembly()) };		
	    		}
	    		else 
	    		{

         		    return new string[] { "FrameworkSetup_linxAutoSetupService", Linx.Tools.AssemblyHelper.ReadResourceContent("Linx.Framework.Setup.ClientResources.linxAutoSetupService.res", System.Reflection.Assembly.GetExecutingAssembly()) };	
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
	    //Clear TcsEmpresaAutenticacao.
	    public IEnumerable<TcsEmpresaAutenticacao> ClearTcsEmpresaAutenticacao()
	    {
	        List<TcsEmpresaAutenticacao> result = new List<TcsEmpresaAutenticacao>();
	        result.Add(new TcsEmpresaAutenticacao());	
			
	        result[0].TcsEmpresaAutenticacaoModuloList = new List<TcsEmpresaAutenticacaoModulo>();
	        ((List<TcsEmpresaAutenticacaoModulo>)result[0].TcsEmpresaAutenticacaoModuloList).Add(new TcsEmpresaAutenticacaoModulo());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsEmpresaAutenticacaoModulo.
	    public IEnumerable<TcsEmpresaAutenticacaoModulo> ClearTcsEmpresaAutenticacaoModulo()
	    {
	        List<TcsEmpresaAutenticacaoModulo> result = new List<TcsEmpresaAutenticacaoModulo>();
	        result.Add(new TcsEmpresaAutenticacaoModulo());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsUsuarioAutenticacao.
	    public IEnumerable<TcsUsuarioAutenticacao> ClearTcsUsuarioAutenticacao()
	    {
	        List<TcsUsuarioAutenticacao> result = new List<TcsUsuarioAutenticacao>();
	        result.Add(new TcsUsuarioAutenticacao(false));	
			
	        result[0].TcsUsuarioAutenticacaoAcessoList = new List<TcsUsuarioAutenticacaoAcesso>();
	        ((List<TcsUsuarioAutenticacaoAcesso>)result[0].TcsUsuarioAutenticacaoAcessoList).Add(new TcsUsuarioAutenticacaoAcesso());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsUsuarioAutenticacaoAcesso.
	    public IEnumerable<TcsUsuarioAutenticacaoAcesso> ClearTcsUsuarioAutenticacaoAcesso()
	    {
	        List<TcsUsuarioAutenticacaoAcesso> result = new List<TcsUsuarioAutenticacaoAcesso>();
	        result.Add(new TcsUsuarioAutenticacaoAcesso());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsUsuarioPerfil.
	    public IEnumerable<TcsUsuarioPerfil> ClearTcsUsuarioPerfil()
	    {
	        List<TcsUsuarioPerfil> result = new List<TcsUsuarioPerfil>();
	        result.Add(new TcsUsuarioPerfil());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsAmbiente.
	    public IEnumerable<TcsAmbiente> ClearTcsAmbiente()
	    {
	        List<TcsAmbiente> result = new List<TcsAmbiente>();
	        result.Add(new TcsAmbiente());	
			
	        result[0].TcsAmbienteConexaoList = new List<TcsAmbienteConexao>();
	        ((List<TcsAmbienteConexao>)result[0].TcsAmbienteConexaoList).Add(new TcsAmbienteConexao());
			
	        result[0].TcsAmbienteUsuarioAcessoList = new List<TcsAmbienteUsuarioAcesso>();
	        ((List<TcsAmbienteUsuarioAcesso>)result[0].TcsAmbienteUsuarioAcessoList).Add(new TcsAmbienteUsuarioAcesso());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsAmbienteConexao.
	    public IEnumerable<TcsAmbienteConexao> ClearTcsAmbienteConexao()
	    {
	        List<TcsAmbienteConexao> result = new List<TcsAmbienteConexao>();
	        result.Add(new TcsAmbienteConexao());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsAmbienteUsuarioAcesso.
	    public IEnumerable<TcsAmbienteUsuarioAcesso> ClearTcsAmbienteUsuarioAcesso()
	    {
	        List<TcsAmbienteUsuarioAcesso> result = new List<TcsAmbienteUsuarioAcesso>();
	        result.Add(new TcsAmbienteUsuarioAcesso());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsModuloGrupo.
	    public IEnumerable<TcsModuloGrupo> ClearTcsModuloGrupo()
	    {
	        List<TcsModuloGrupo> result = new List<TcsModuloGrupo>();
	        result.Add(new TcsModuloGrupo());	
			
	        result[0].TcsModuloGrupoDetalheList = new List<TcsModuloGrupoDetalhe>();
	        ((List<TcsModuloGrupoDetalhe>)result[0].TcsModuloGrupoDetalheList).Add(new TcsModuloGrupoDetalhe());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsModuloGrupoDetalhe.
	    public IEnumerable<TcsModuloGrupoDetalhe> ClearTcsModuloGrupoDetalhe()
	    {
	        List<TcsModuloGrupoDetalhe> result = new List<TcsModuloGrupoDetalhe>();
	        result.Add(new TcsModuloGrupoDetalhe());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsParametroValor.
	    public IEnumerable<TcsParametroValor> ClearTcsParametroValor()
	    {
	        List<TcsParametroValor> result = new List<TcsParametroValor>();
	        result.Add(new TcsParametroValor());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsPerfil.
	    public IEnumerable<TcsPerfil> ClearTcsPerfil()
	    {
	        List<TcsPerfil> result = new List<TcsPerfil>();
	        result.Add(new TcsPerfil());	
			
	        result[0].TcsPerfilRegraModuloList = new List<TcsPerfilRegraModulo>();
	        ((List<TcsPerfilRegraModulo>)result[0].TcsPerfilRegraModuloList).Add(new TcsPerfilRegraModulo());
			
	        result[0].TcsPerfilUsuarioList = new List<TcsPerfilUsuario>();
	        ((List<TcsPerfilUsuario>)result[0].TcsPerfilUsuarioList).Add(new TcsPerfilUsuario());
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsPerfilRegraModulo.
	    public IEnumerable<TcsPerfilRegraModulo> ClearTcsPerfilRegraModulo()
	    {
	        List<TcsPerfilRegraModulo> result = new List<TcsPerfilRegraModulo>();
	        result.Add(new TcsPerfilRegraModulo());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsPerfilUsuario.
	    public IEnumerable<TcsPerfilUsuario> ClearTcsPerfilUsuario()
	    {
	        List<TcsPerfilUsuario> result = new List<TcsPerfilUsuario>();
	        result.Add(new TcsPerfilUsuario());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear AmbienteInfo.
	    public IEnumerable<AmbienteInfo> ClearAmbienteInfo()
	    {
	        List<AmbienteInfo> result = new List<AmbienteInfo>();
	        result.Add(new AmbienteInfo());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsEmpresaGpecon.
	    public IEnumerable<TcsEmpresaGpecon> ClearTcsEmpresaGpecon()
	    {
	        List<TcsEmpresaGpecon> result = new List<TcsEmpresaGpecon>();
	        result.Add(new TcsEmpresaGpecon());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsAmbienteInfo.
	    public IEnumerable<TcsAmbienteInfo> ClearTcsAmbienteInfo()
	    {
	        List<TcsAmbienteInfo> result = new List<TcsAmbienteInfo>();
	        result.Add(new TcsAmbienteInfo());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TcsParametroAutorizacao.
	    public IEnumerable<TcsParametroAutorizacao> ClearTcsParametroAutorizacao()
	    {
	        List<TcsParametroAutorizacao> result = new List<TcsParametroAutorizacao>();
	        result.Add(new TcsParametroAutorizacao());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear MultimarcaInfo.
	    public IEnumerable<MultimarcaInfo> ClearMultimarcaInfo()
	    {
	        List<MultimarcaInfo> result = new List<MultimarcaInfo>();
	        result.Add(new MultimarcaInfo());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TbcFilial.
	    public IEnumerable<TbcFilial> ClearTbcFilial()
	    {
	        List<TbcFilial> result = new List<TbcFilial>();
	        result.Add(new TbcFilial(false));	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TbcGrupoEconomico.
	    public IEnumerable<TbcGrupoEconomico> ClearTbcGrupoEconomico()
	    {
	        List<TbcGrupoEconomico> result = new List<TbcGrupoEconomico>();
	        result.Add(new TbcGrupoEconomico());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear TbcBandeiraRede.
	    public IEnumerable<TbcBandeiraRede> ClearTbcBandeiraRede()
	    {
	        List<TbcBandeiraRede> result = new List<TbcBandeiraRede>();
	        result.Add(new TbcBandeiraRede());	
		
	        

	
	        return result;
	    }
		
	
	    [Ignore]
	    //Clear LjvCanalVenda.
	    public IEnumerable<LjvCanalVenda> ClearLjvCanalVenda()
	    {
	        List<LjvCanalVenda> result = new List<LjvCanalVenda>();
	        result.Add(new LjvCanalVenda());	
		
	        

	
	        return result;
	    }
		
	    #endregion Clear Methods Definitions.
	
	    #region Get Methods Definitions.
	
		
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsEmpresaAutenticacao.
	    public IQueryable<TcsEmpresaAutenticacao> GetTcsEmpresaAutenticacao()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.Empresa.EmpresaDomainService serviceContext2 = new Linx.Framework.BV.Empresa.EmpresaDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsEmpresaAutenticacao> result = 
	            (
                 from TcsEmpresaAutenticacao_Rep1 in serviceContext2.GetTcsEmpresaAutenticacaoNoAssociations()
	            
	            	
	            select new TcsEmpresaAutenticacao()		
	            {
	            
                CnpjCpf = TcsEmpresaAutenticacao_Rep1.CnpjCpf
                , IdLinx = TcsEmpresaAutenticacao_Rep1.IdLinx
                , NomeEmpresa = TcsEmpresaAutenticacao_Rep1.NomeEmpresa
                , UidEmpresa = TcsEmpresaAutenticacao_Rep1.UidEmpresa
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsEmpresaAutenticacaoModulo.
	    public IQueryable<TcsEmpresaAutenticacaoModulo> GetTcsEmpresaAutenticacaoModulo()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.Empresa.EmpresaDomainService serviceContext2 = new Linx.Framework.BV.Empresa.EmpresaDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsEmpresaAutenticacaoModulo> result = 
	            (
                 from TcsEmpresaModulo_Rep1 in serviceContext2.GetTcsEmpresaModuloNoAssociations()
	            
	            	
	            select new TcsEmpresaAutenticacaoModulo()		
	            {
	            
                IdLinx = TcsEmpresaModulo_Rep1.IdLinx
                , IdModulo = TcsEmpresaModulo_Rep1.IdModulo
                , IdTcsAplicativo = TcsEmpresaModulo_Rep1.IdTcsAplicativo
                , IdTcsEmpresaModulo = TcsEmpresaModulo_Rep1.IdTcsEmpresaModulo
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsEmpresaAutenticacaoNoAssociations.
	    public IQueryable<TcsEmpresaAutenticacao> GetTcsEmpresaAutenticacaoNoAssociations()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.Empresa.EmpresaDomainService serviceContext2 = new Linx.Framework.BV.Empresa.EmpresaDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsEmpresaAutenticacao> result = 
	            (
                 from TcsEmpresaAutenticacao_Rep1 in serviceContext2.GetTcsEmpresaAutenticacaoNoAssociations()
	            
	            	
	            select new TcsEmpresaAutenticacao()		
	            {
	            
                CnpjCpf = TcsEmpresaAutenticacao_Rep1.CnpjCpf
                , IdLinx = TcsEmpresaAutenticacao_Rep1.IdLinx
                , NomeEmpresa = TcsEmpresaAutenticacao_Rep1.NomeEmpresa
                , UidEmpresa = TcsEmpresaAutenticacao_Rep1.UidEmpresa
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsEmpresaAutenticacaoModuloNoAssociations.
	    public IQueryable<TcsEmpresaAutenticacaoModulo> GetTcsEmpresaAutenticacaoModuloNoAssociations()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.Empresa.EmpresaDomainService serviceContext2 = new Linx.Framework.BV.Empresa.EmpresaDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsEmpresaAutenticacaoModulo> result = 
	            (
                 from TcsEmpresaModulo_Rep1 in serviceContext2.GetTcsEmpresaModuloNoAssociations()
	            
	            	
	            select new TcsEmpresaAutenticacaoModulo()		
	            {
	            
                IdLinx = TcsEmpresaModulo_Rep1.IdLinx
                , IdModulo = TcsEmpresaModulo_Rep1.IdModulo
                , IdTcsAplicativo = TcsEmpresaModulo_Rep1.IdTcsAplicativo
                , IdTcsEmpresaModulo = TcsEmpresaModulo_Rep1.IdTcsEmpresaModulo
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsUsuarioAutenticacao.
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacao()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService serviceContext8 = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsUsuarioAutenticacao> result = 
	            (
                 from TcsUsuarioAutenticacao_Rep1 in serviceContext8.GetTcsUsuarioAutenticacaoNoAssociations()
	            
	            	
	            select new TcsUsuarioAutenticacao()		
	            {
	            
                AutenticacaoWindows = TcsUsuarioAutenticacao_Rep1.AutenticacaoWindows
                , CnpjCpf = TcsUsuarioAutenticacao_Rep1.CnpjCpf
                , ConfirmacaoUsuario = TcsUsuarioAutenticacao_Rep1.ConfirmacaoUsuario
                , ConfirmacaoUsuario1 = TcsUsuarioAutenticacao_Rep1.ConfirmacaoUsuario1
                , CriaUsuario = TcsUsuarioAutenticacao_Rep1.CriaUsuario
                , DataAlteracao = TcsUsuarioAutenticacao_Rep1.DataAlteracao
                , DataCadastro = TcsUsuarioAutenticacao_Rep1.DataCadastro
                , DataExpiracaoSenha = TcsUsuarioAutenticacao_Rep1.DataExpiracaoSenha
                , Email = TcsUsuarioAutenticacao_Rep1.Email
                , GeraSenhaUsuario = TcsUsuarioAutenticacao_Rep1.GeraSenhaUsuario
                , IdLinx = TcsUsuarioAutenticacao_Rep1.IdLinx
                , IdUsuario = TcsUsuarioAutenticacao_Rep1.IdUsuario
                , LxPfjFisicaJuridica = TcsUsuarioAutenticacao_Rep1.LxPfjFisicaJuridica
                , LxPfjFisicaJuridicaName = ((TcsUsuarioAutenticacao_Rep1.LxPfjFisicaJuridica) == 1 ? "Pessoa Física" : ((TcsUsuarioAutenticacao_Rep1.LxPfjFisicaJuridica) == 2 ? "Pessoa Jurídica" : ""))
                , NomeAutenticacao = TcsUsuarioAutenticacao_Rep1.NomeAutenticacao
                , NomeCurtoUsuario = TcsUsuarioAutenticacao_Rep1.NomeCurtoUsuario
                , NomeUsuario = TcsUsuarioAutenticacao_Rep1.NomeUsuario
                , UidUsuario = TcsUsuarioAutenticacao_Rep1.UidUsuario
                , VigenciaFinal = TcsUsuarioAutenticacao_Rep1.VigenciaFinal
                , VigenciaInicial = TcsUsuarioAutenticacao_Rep1.VigenciaInicial
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsUsuarioAutenticacaoAcesso.
	    public IQueryable<TcsUsuarioAutenticacaoAcesso> GetTcsUsuarioAutenticacaoAcesso()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService serviceContext8 = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsUsuarioAutenticacaoAcesso> result = 
	            (
                 from TcsUsuarioAcesso_Rep1 in serviceContext8.GetTcsUsuarioAcessoNoAssociations()
	            
	            	
	            select new TcsUsuarioAutenticacaoAcesso()		
	            {
	            
                IdTcsAmbiente = TcsUsuarioAcesso_Rep1.IdTcsAmbiente
                , IdTcsAmbienteRelacionado = TcsUsuarioAcesso_Rep1.IdTcsAmbienteRelacionado
                , IdTcsUsuarioAcesso = TcsUsuarioAcesso_Rep1.IdTcsUsuarioAcesso
                , IdUsuario = TcsUsuarioAcesso_Rep1.IdUsuario
                , IndicaAcessoPadrao = TcsUsuarioAcesso_Rep1.IndicaAcessoPadrao
                , IndicaAdministrador = TcsUsuarioAcesso_Rep1.IndicaAdministrador
                , IndicaMultiGpecon = TcsUsuarioAcesso_Rep1.IndicaMultiGpecon
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoNoAssociations.
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoNoAssociations()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService serviceContext8 = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsUsuarioAutenticacao> result = 
	            (
                 from TcsUsuarioAutenticacao_Rep1 in serviceContext8.GetTcsUsuarioAutenticacaoNoAssociations()
	            
	            	
	            select new TcsUsuarioAutenticacao()		
	            {
	            
                AutenticacaoWindows = TcsUsuarioAutenticacao_Rep1.AutenticacaoWindows
                , CnpjCpf = TcsUsuarioAutenticacao_Rep1.CnpjCpf
                , ConfirmacaoUsuario = TcsUsuarioAutenticacao_Rep1.ConfirmacaoUsuario
                , ConfirmacaoUsuario1 = TcsUsuarioAutenticacao_Rep1.ConfirmacaoUsuario1
                , CriaUsuario = TcsUsuarioAutenticacao_Rep1.CriaUsuario
                , DataAlteracao = TcsUsuarioAutenticacao_Rep1.DataAlteracao
                , DataCadastro = TcsUsuarioAutenticacao_Rep1.DataCadastro
                , DataExpiracaoSenha = TcsUsuarioAutenticacao_Rep1.DataExpiracaoSenha
                , Email = TcsUsuarioAutenticacao_Rep1.Email
                , GeraSenhaUsuario = TcsUsuarioAutenticacao_Rep1.GeraSenhaUsuario
                , IdLinx = TcsUsuarioAutenticacao_Rep1.IdLinx
                , IdUsuario = TcsUsuarioAutenticacao_Rep1.IdUsuario
                , LxPfjFisicaJuridica = TcsUsuarioAutenticacao_Rep1.LxPfjFisicaJuridica
                , LxPfjFisicaJuridicaName = ((TcsUsuarioAutenticacao_Rep1.LxPfjFisicaJuridica) == 1 ? "Pessoa Física" : ((TcsUsuarioAutenticacao_Rep1.LxPfjFisicaJuridica) == 2 ? "Pessoa Jurídica" : ""))
                , NomeAutenticacao = TcsUsuarioAutenticacao_Rep1.NomeAutenticacao
                , NomeCurtoUsuario = TcsUsuarioAutenticacao_Rep1.NomeCurtoUsuario
                , NomeUsuario = TcsUsuarioAutenticacao_Rep1.NomeUsuario
                , UidUsuario = TcsUsuarioAutenticacao_Rep1.UidUsuario
                , VigenciaFinal = TcsUsuarioAutenticacao_Rep1.VigenciaFinal
                , VigenciaInicial = TcsUsuarioAutenticacao_Rep1.VigenciaInicial
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoAcessoNoAssociations.
	    public IQueryable<TcsUsuarioAutenticacaoAcesso> GetTcsUsuarioAutenticacaoAcessoNoAssociations()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService serviceContext8 = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsUsuarioAutenticacaoAcesso> result = 
	            (
                 from TcsUsuarioAcesso_Rep1 in serviceContext8.GetTcsUsuarioAcessoNoAssociations()
	            
	            	
	            select new TcsUsuarioAutenticacaoAcesso()		
	            {
	            
                IdTcsAmbiente = TcsUsuarioAcesso_Rep1.IdTcsAmbiente
                , IdTcsAmbienteRelacionado = TcsUsuarioAcesso_Rep1.IdTcsAmbienteRelacionado
                , IdTcsUsuarioAcesso = TcsUsuarioAcesso_Rep1.IdTcsUsuarioAcesso
                , IdUsuario = TcsUsuarioAcesso_Rep1.IdUsuario
                , IndicaAcessoPadrao = TcsUsuarioAcesso_Rep1.IndicaAcessoPadrao
                , IndicaAdministrador = TcsUsuarioAcesso_Rep1.IndicaAdministrador
                , IndicaMultiGpecon = TcsUsuarioAcesso_Rep1.IndicaMultiGpecon
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsUsuarioPerfil.
	    public IQueryable<TcsUsuarioPerfil> GetTcsUsuarioPerfil()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.Usuario.UsuarioDomainService serviceContext7 = new Linx.Framework.BV.Usuario.UsuarioDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsUsuarioPerfil> result = 
	            (
                 from TcsUsuarioPerfilP_Rep1 in serviceContext7.GetTcsUsuarioPerfilPNoAssociations()
	            
	            	
	            select new TcsUsuarioPerfil()		
	            {
	            
                IdPerfil = TcsUsuarioPerfilP_Rep1.IdPerfil
                , IdTcsUsuarioPerfil = TcsUsuarioPerfilP_Rep1.IdTcsUsuarioPerfil
                , IdUsuario = TcsUsuarioPerfilP_Rep1.IdUsuario
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioPerfilNoAssociations.
	    public IQueryable<TcsUsuarioPerfil> GetTcsUsuarioPerfilNoAssociations()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.Usuario.UsuarioDomainService serviceContext7 = new Linx.Framework.BV.Usuario.UsuarioDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsUsuarioPerfil> result = 
	            (
                 from TcsUsuarioPerfilP_Rep1 in serviceContext7.GetTcsUsuarioPerfilPNoAssociations()
	            
	            	
	            select new TcsUsuarioPerfil()		
	            {
	            
                IdPerfil = TcsUsuarioPerfilP_Rep1.IdPerfil
                , IdTcsUsuarioPerfil = TcsUsuarioPerfilP_Rep1.IdTcsUsuarioPerfil
                , IdUsuario = TcsUsuarioPerfilP_Rep1.IdUsuario
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsAmbiente.
	    public IQueryable<TcsAmbiente> GetTcsAmbiente()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.Ambiente.AmbienteDomainService serviceContext1 = new Linx.Framework.BV.Ambiente.AmbienteDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsAmbiente> result = 
	            (
                 from TcsAmbiente_Rep1 in serviceContext1.GetTcsAmbienteNoAssociations()
	            
	            	
	            select new TcsAmbiente()		
	            {
	            
                DescricaoAmbiente = TcsAmbiente_Rep1.DescricaoAmbiente
                , IdAplicacao = TcsAmbiente_Rep1.IdAplicacao
                , IdLinx = TcsAmbiente_Rep1.IdLinx
                , IdTcsAmbiente = TcsAmbiente_Rep1.IdTcsAmbiente
                , UidEmpresa = TcsAmbiente_Rep1.UidEmpresa
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsAmbienteConexao.
	    public IQueryable<TcsAmbienteConexao> GetTcsAmbienteConexao()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.Ambiente.AmbienteDomainService serviceContext1 = new Linx.Framework.BV.Ambiente.AmbienteDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsAmbienteConexao> result = 
	            (
                 from TcsAmbienteConexao_Rep1 in serviceContext1.GetTcsAmbienteConexaoNoAssociations()
	            
	            	
	            select new TcsAmbienteConexao()		
	            {
	            
                IdTcsAmbiente = TcsAmbienteConexao_Rep1.IdTcsAmbiente
                , IdTcsAmbienteConexao = TcsAmbienteConexao_Rep1.IdTcsAmbienteConexao
                , IdTcsAplicativoConexao = TcsAmbienteConexao_Rep1.IdTcsAplicativoConexao
                , IdTcsBancoServidor = TcsAmbienteConexao_Rep1.IdTcsBancoServidor
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsAmbienteUsuarioAcesso.
	    public IQueryable<TcsAmbienteUsuarioAcesso> GetTcsAmbienteUsuarioAcesso()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.Ambiente.AmbienteDomainService serviceContext1 = new Linx.Framework.BV.Ambiente.AmbienteDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsAmbienteUsuarioAcesso> result = 
	            (
                 from TcsAmbienteUsuarioAcesso_Rep1 in serviceContext1.GetTcsAmbienteUsuarioAcessoNoAssociations()
	            
	            	
	            select new TcsAmbienteUsuarioAcesso()		
	            {
	            
                IdTcsAmbiente = TcsAmbienteUsuarioAcesso_Rep1.IdTcsAmbiente
                , IdTcsUsuarioAcesso = TcsAmbienteUsuarioAcesso_Rep1.IdTcsUsuarioAcesso
                , IdUsuario = TcsAmbienteUsuarioAcesso_Rep1.IdUsuario
                , IndicaAdministrador = TcsAmbienteUsuarioAcesso_Rep1.IndicaAdministrador
                , IndicaMultiGpecon = TcsAmbienteUsuarioAcesso_Rep1.IndicaMultiGpecon
                , NomeAutenticacao = TcsAmbienteUsuarioAcesso_Rep1.NomeAutenticacao
                , NomeUsuario = TcsAmbienteUsuarioAcesso_Rep1.NomeUsuario
                , UidUsuario = TcsAmbienteUsuarioAcesso_Rep1.UidUsuario
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteNoAssociations.
	    public IQueryable<TcsAmbiente> GetTcsAmbienteNoAssociations()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.Ambiente.AmbienteDomainService serviceContext1 = new Linx.Framework.BV.Ambiente.AmbienteDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsAmbiente> result = 
	            (
                 from TcsAmbiente_Rep1 in serviceContext1.GetTcsAmbienteNoAssociations()
	            
	            	
	            select new TcsAmbiente()		
	            {
	            
                DescricaoAmbiente = TcsAmbiente_Rep1.DescricaoAmbiente
                , IdAplicacao = TcsAmbiente_Rep1.IdAplicacao
                , IdLinx = TcsAmbiente_Rep1.IdLinx
                , IdTcsAmbiente = TcsAmbiente_Rep1.IdTcsAmbiente
                , UidEmpresa = TcsAmbiente_Rep1.UidEmpresa
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteConexaoNoAssociations.
	    public IQueryable<TcsAmbienteConexao> GetTcsAmbienteConexaoNoAssociations()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.Ambiente.AmbienteDomainService serviceContext1 = new Linx.Framework.BV.Ambiente.AmbienteDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsAmbienteConexao> result = 
	            (
                 from TcsAmbienteConexao_Rep1 in serviceContext1.GetTcsAmbienteConexaoNoAssociations()
	            
	            	
	            select new TcsAmbienteConexao()		
	            {
	            
                IdTcsAmbiente = TcsAmbienteConexao_Rep1.IdTcsAmbiente
                , IdTcsAmbienteConexao = TcsAmbienteConexao_Rep1.IdTcsAmbienteConexao
                , IdTcsAplicativoConexao = TcsAmbienteConexao_Rep1.IdTcsAplicativoConexao
                , IdTcsBancoServidor = TcsAmbienteConexao_Rep1.IdTcsBancoServidor
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteUsuarioAcessoNoAssociations.
	    public IQueryable<TcsAmbienteUsuarioAcesso> GetTcsAmbienteUsuarioAcessoNoAssociations()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.Ambiente.AmbienteDomainService serviceContext1 = new Linx.Framework.BV.Ambiente.AmbienteDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsAmbienteUsuarioAcesso> result = 
	            (
                 from TcsAmbienteUsuarioAcesso_Rep1 in serviceContext1.GetTcsAmbienteUsuarioAcessoNoAssociations()
	            
	            	
	            select new TcsAmbienteUsuarioAcesso()		
	            {
	            
                IdTcsAmbiente = TcsAmbienteUsuarioAcesso_Rep1.IdTcsAmbiente
                , IdTcsUsuarioAcesso = TcsAmbienteUsuarioAcesso_Rep1.IdTcsUsuarioAcesso
                , IdUsuario = TcsAmbienteUsuarioAcesso_Rep1.IdUsuario
                , IndicaAdministrador = TcsAmbienteUsuarioAcesso_Rep1.IndicaAdministrador
                , IndicaMultiGpecon = TcsAmbienteUsuarioAcesso_Rep1.IndicaMultiGpecon
                , NomeAutenticacao = TcsAmbienteUsuarioAcesso_Rep1.NomeAutenticacao
                , NomeUsuario = TcsAmbienteUsuarioAcesso_Rep1.NomeUsuario
                , UidUsuario = TcsAmbienteUsuarioAcesso_Rep1.UidUsuario
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsModuloGrupo.
	    public IQueryable<TcsModuloGrupo> GetTcsModuloGrupo()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.Modulo.ModuloDomainService serviceContext3 = new Linx.Framework.BV.Modulo.ModuloDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsModuloGrupo> result = 
	            (
                 from TcsModuloGrupo_Rep1 in serviceContext3.GetTcsModuloGrupoNoAssociations()
	            
	            	
	            select new TcsModuloGrupo()		
	            {
	            
                DescGrupoModulo = TcsModuloGrupo_Rep1.DescGrupoModulo
                , IdGrupoModulo = TcsModuloGrupo_Rep1.IdGrupoModulo
                , IdTcsAplicativo = TcsModuloGrupo_Rep1.IdTcsAplicativo
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsModuloGrupoDetalhe.
	    public IQueryable<TcsModuloGrupoDetalhe> GetTcsModuloGrupoDetalhe()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.Modulo.ModuloDomainService serviceContext3 = new Linx.Framework.BV.Modulo.ModuloDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsModuloGrupoDetalhe> result = 
	            (
                 from TcsModuloDoGrupoDetalhe_Rep1 in serviceContext3.GetTcsModuloDoGrupoDetalheNoAssociations()
	            
	            	
	            select new TcsModuloGrupoDetalhe()		
	            {
	            
                IdGrupoModulo = TcsModuloDoGrupoDetalhe_Rep1.IdGrupoModulo
                , IdModulo = TcsModuloDoGrupoDetalhe_Rep1.IdModulo
                , IdModuloDoGrupo = TcsModuloDoGrupoDetalhe_Rep1.IdModuloDoGrupo
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsModuloGrupoNoAssociations.
	    public IQueryable<TcsModuloGrupo> GetTcsModuloGrupoNoAssociations()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.Modulo.ModuloDomainService serviceContext3 = new Linx.Framework.BV.Modulo.ModuloDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsModuloGrupo> result = 
	            (
                 from TcsModuloGrupo_Rep1 in serviceContext3.GetTcsModuloGrupoNoAssociations()
	            
	            	
	            select new TcsModuloGrupo()		
	            {
	            
                DescGrupoModulo = TcsModuloGrupo_Rep1.DescGrupoModulo
                , IdGrupoModulo = TcsModuloGrupo_Rep1.IdGrupoModulo
                , IdTcsAplicativo = TcsModuloGrupo_Rep1.IdTcsAplicativo
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsModuloGrupoDetalheNoAssociations.
	    public IQueryable<TcsModuloGrupoDetalhe> GetTcsModuloGrupoDetalheNoAssociations()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.Modulo.ModuloDomainService serviceContext3 = new Linx.Framework.BV.Modulo.ModuloDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsModuloGrupoDetalhe> result = 
	            (
                 from TcsModuloDoGrupoDetalhe_Rep1 in serviceContext3.GetTcsModuloDoGrupoDetalheNoAssociations()
	            
	            	
	            select new TcsModuloGrupoDetalhe()		
	            {
	            
                IdGrupoModulo = TcsModuloDoGrupoDetalhe_Rep1.IdGrupoModulo
                , IdModulo = TcsModuloDoGrupoDetalhe_Rep1.IdModulo
                , IdModuloDoGrupo = TcsModuloDoGrupoDetalhe_Rep1.IdModuloDoGrupo
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsParametroValor.
	    public IQueryable<TcsParametroValor> GetTcsParametroValor()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.Parametro.ParametroDomainService serviceContext4 = new Linx.Framework.BV.Parametro.ParametroDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsParametroValor> result = 
	            (
                 from TcsParametroValorP_Rep1 in serviceContext4.GetTcsParametroValorPNoAssociations()
	            
	            	
	            select new TcsParametroValor()		
	            {
	            
                IdParametro = TcsParametroValorP_Rep1.IdParametro
                , IdParametroValor = TcsParametroValorP_Rep1.IdParametroValor
                , ValorParametro = TcsParametroValorP_Rep1.ValorParametro
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsParametroValorNoAssociations.
	    public IQueryable<TcsParametroValor> GetTcsParametroValorNoAssociations()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.Parametro.ParametroDomainService serviceContext4 = new Linx.Framework.BV.Parametro.ParametroDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsParametroValor> result = 
	            (
                 from TcsParametroValorP_Rep1 in serviceContext4.GetTcsParametroValorPNoAssociations()
	            
	            	
	            select new TcsParametroValor()		
	            {
	            
                IdParametro = TcsParametroValorP_Rep1.IdParametro
                , IdParametroValor = TcsParametroValorP_Rep1.IdParametroValor
                , ValorParametro = TcsParametroValorP_Rep1.ValorParametro
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsPerfil.
	    public IQueryable<TcsPerfil> GetTcsPerfil()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.Perfil.PerfilDomainService serviceContext6 = new Linx.Framework.BV.Perfil.PerfilDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsPerfil> result = 
	            (
                 from TcsPerfil_Rep1 in serviceContext6.GetTcsPerfilNoAssociations()
	            
	            	
	            select new TcsPerfil()		
	            {
	            
                DescPerfil = TcsPerfil_Rep1.DescPerfil
                , IdPerfil = TcsPerfil_Rep1.IdPerfil
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsPerfilRegraModulo.
	    public IQueryable<TcsPerfilRegraModulo> GetTcsPerfilRegraModulo()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.Perfil.PerfilDomainService serviceContext6 = new Linx.Framework.BV.Perfil.PerfilDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsPerfilRegraModulo> result = 
	            (
                 from TcsPerfilRegraModulo_Rep1 in serviceContext6.GetTcsPerfilRegraModuloNoAssociations()
	            
	            	
	            select new TcsPerfilRegraModulo()		
	            {
	            
                IdModulo = TcsPerfilRegraModulo_Rep1.IdModulo
                , IdPerfil = TcsPerfilRegraModulo_Rep1.IdPerfil
                , IdPerfilRegraModulo = TcsPerfilRegraModulo_Rep1.IdPerfilRegraModulo
                , LxRegraAcessoModulo = TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo
                , LxRegraAcessoModuloName = ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 1 ? "Acesso Bloqueado" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 2 ? "Acesso Total" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 5 ? "Alterar" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 12 ? "Criar Pesquisa" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 10 ? "Criar Relatório" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 6 ? "Excluir" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 9 ? "Exportar" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 8 ? "Imprimir" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 4 ? "Incluir" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 11 ? "Layout" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 7 ? "Pesquisa Especial" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 3 ? "Pesquisar" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 99 ? "Regra Transação" : "")))))))))))))
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsPerfilUsuario.
	    public IQueryable<TcsPerfilUsuario> GetTcsPerfilUsuario()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.Perfil.PerfilDomainService serviceContext6 = new Linx.Framework.BV.Perfil.PerfilDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsPerfilUsuario> result = 
	            (
                 from TcsUsuarioPerfil_Rep2 in serviceContext6.GetTcsUsuarioPerfilNoAssociations()
	            
	            	
	            select new TcsPerfilUsuario()		
	            {
	            
                IdPerfil = TcsUsuarioPerfil_Rep2.IdPerfil
                , IdTcsUsuarioPerfil = TcsUsuarioPerfil_Rep2.IdTcsUsuarioPerfil
                , IdUsuario = TcsUsuarioPerfil_Rep2.IdUsuario
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsPerfilNoAssociations.
	    public IQueryable<TcsPerfil> GetTcsPerfilNoAssociations()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.Perfil.PerfilDomainService serviceContext6 = new Linx.Framework.BV.Perfil.PerfilDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsPerfil> result = 
	            (
                 from TcsPerfil_Rep1 in serviceContext6.GetTcsPerfilNoAssociations()
	            
	            	
	            select new TcsPerfil()		
	            {
	            
                DescPerfil = TcsPerfil_Rep1.DescPerfil
                , IdPerfil = TcsPerfil_Rep1.IdPerfil
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsPerfilRegraModuloNoAssociations.
	    public IQueryable<TcsPerfilRegraModulo> GetTcsPerfilRegraModuloNoAssociations()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.Perfil.PerfilDomainService serviceContext6 = new Linx.Framework.BV.Perfil.PerfilDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsPerfilRegraModulo> result = 
	            (
                 from TcsPerfilRegraModulo_Rep1 in serviceContext6.GetTcsPerfilRegraModuloNoAssociations()
	            
	            	
	            select new TcsPerfilRegraModulo()		
	            {
	            
                IdModulo = TcsPerfilRegraModulo_Rep1.IdModulo
                , IdPerfil = TcsPerfilRegraModulo_Rep1.IdPerfil
                , IdPerfilRegraModulo = TcsPerfilRegraModulo_Rep1.IdPerfilRegraModulo
                , LxRegraAcessoModulo = TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo
                , LxRegraAcessoModuloName = ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 1 ? "Acesso Bloqueado" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 2 ? "Acesso Total" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 5 ? "Alterar" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 12 ? "Criar Pesquisa" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 10 ? "Criar Relatório" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 6 ? "Excluir" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 9 ? "Exportar" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 8 ? "Imprimir" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 4 ? "Incluir" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 11 ? "Layout" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 7 ? "Pesquisa Especial" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 3 ? "Pesquisar" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 99 ? "Regra Transação" : "")))))))))))))
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsPerfilUsuarioNoAssociations.
	    public IQueryable<TcsPerfilUsuario> GetTcsPerfilUsuarioNoAssociations()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.Perfil.PerfilDomainService serviceContext6 = new Linx.Framework.BV.Perfil.PerfilDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsPerfilUsuario> result = 
	            (
                 from TcsUsuarioPerfil_Rep2 in serviceContext6.GetTcsUsuarioPerfilNoAssociations()
	            
	            	
	            select new TcsPerfilUsuario()		
	            {
	            
                IdPerfil = TcsUsuarioPerfil_Rep2.IdPerfil
                , IdTcsUsuarioPerfil = TcsUsuarioPerfil_Rep2.IdTcsUsuarioPerfil
                , IdUsuario = TcsUsuarioPerfil_Rep2.IdUsuario
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get AmbienteInfo.
	    public IEnumerable<AmbienteInfo> GetAmbienteInfo()
	    {




	
	        IEnumerable<AmbienteInfo> result = new List<AmbienteInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get AmbienteInfoNoAssociations.
	    public IEnumerable<AmbienteInfo> GetAmbienteInfoNoAssociations()
	    {




	
	        IEnumerable<AmbienteInfo> result = new List<AmbienteInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsEmpresaGpecon.
	    public IQueryable<TcsEmpresaGpecon> GetTcsEmpresaGpecon()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.Empresa.EmpresaDomainService serviceContext2 = new Linx.Framework.BV.Empresa.EmpresaDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsEmpresaGpecon> result = 
	            (
                 from TcsEmpresaGpeconP_Rep1 in serviceContext2.GetTcsEmpresaGpeconPNoAssociations()
	            
	            	
	            select new TcsEmpresaGpecon()		
	            {
	            
                IdLinx = TcsEmpresaGpeconP_Rep1.IdLinx
                , IdLinxGpecon = TcsEmpresaGpeconP_Rep1.IdLinxGpecon
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsEmpresaGpeconNoAssociations.
	    public IQueryable<TcsEmpresaGpecon> GetTcsEmpresaGpeconNoAssociations()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.Empresa.EmpresaDomainService serviceContext2 = new Linx.Framework.BV.Empresa.EmpresaDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsEmpresaGpecon> result = 
	            (
                 from TcsEmpresaGpeconP_Rep1 in serviceContext2.GetTcsEmpresaGpeconPNoAssociations()
	            
	            	
	            select new TcsEmpresaGpecon()		
	            {
	            
                IdLinx = TcsEmpresaGpeconP_Rep1.IdLinx
                , IdLinxGpecon = TcsEmpresaGpeconP_Rep1.IdLinxGpecon
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsAmbienteInfo.
	    public IQueryable<TcsAmbienteInfo> GetTcsAmbienteInfo()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteInfo> result = 
	            (from entity0 in this.DbContext.TCS_AMBIENTE
                  let entity0Al1 = entity0.TCS_APLICACAO
                  let entity0Al2 = entity0.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsAmbienteInfo()		
	            {
	            
                IdAplicacao = entity0Al1.ID_APLICACAO
                , IdLinx = entity0Al2.ID_LINX
                , IdTcsAmbiente = entity0.ID_TCS_AMBIENTE
                , UidAplicacao = entity0Al1.UID_APLICACAO
                , UidEmpresa = entity0Al2.UID_EMPRESA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteInfoNoAssociations.
	    public IQueryable<TcsAmbienteInfo> GetTcsAmbienteInfoNoAssociations()
	    {




		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteInfo> result = 
	            (from entity0 in this.DbContext.TCS_AMBIENTE
                  let entity0Al1 = entity0.TCS_APLICACAO
                  let entity0Al2 = entity0.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsAmbienteInfo()		
	            {
	            
                IdAplicacao = entity0Al1.ID_APLICACAO
                , IdLinx = entity0Al2.ID_LINX
                , IdTcsAmbiente = entity0.ID_TCS_AMBIENTE
                , UidAplicacao = entity0Al1.UID_APLICACAO
                , UidEmpresa = entity0Al2.UID_EMPRESA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TcsParametroAutorizacao.
	    public IQueryable<TcsParametroAutorizacao> GetTcsParametroAutorizacao()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.ParametroAutorizacao.ParametroAutorizacaoDomainService serviceContext5 = new Linx.Framework.BV.ParametroAutorizacao.ParametroAutorizacaoDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsParametroAutorizacao> result = 
	            (
                 from TcsParametroAutorizacao_Rep1 in serviceContext5.GetTcsParametroAutorizacaoNoAssociations()
	            
	            	
	            select new TcsParametroAutorizacao()		
	            {
	            
                IdParametro = TcsParametroAutorizacao_Rep1.IdParametro
                , IdTcsAplicativo = TcsParametroAutorizacao_Rep1.IdTcsAplicativo
                , TituloParametro = TcsParametroAutorizacao_Rep1.TituloParametro
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsParametroAutorizacaoNoAssociations.
	    public IQueryable<TcsParametroAutorizacao> GetTcsParametroAutorizacaoNoAssociations()
	    {




		
	
	        
		
	        
             Linx.Framework.BV.ParametroAutorizacao.ParametroAutorizacaoDomainService serviceContext5 = new Linx.Framework.BV.ParametroAutorizacao.ParametroAutorizacaoDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsParametroAutorizacao> result = 
	            (
                 from TcsParametroAutorizacao_Rep1 in serviceContext5.GetTcsParametroAutorizacaoNoAssociations()
	            
	            	
	            select new TcsParametroAutorizacao()		
	            {
	            
                IdParametro = TcsParametroAutorizacao_Rep1.IdParametro
                , IdTcsAplicativo = TcsParametroAutorizacao_Rep1.IdTcsAplicativo
                , TituloParametro = TcsParametroAutorizacao_Rep1.TituloParametro
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get MultimarcaInfo.
	    public IEnumerable<MultimarcaInfo> GetMultimarcaInfo()
	    {




	
	        IEnumerable<MultimarcaInfo> result = new List<MultimarcaInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get MultimarcaInfoNoAssociations.
	    public IEnumerable<MultimarcaInfo> GetMultimarcaInfoNoAssociations()
	    {




	
	        IEnumerable<MultimarcaInfo> result = new List<MultimarcaInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TbcFilial.
	    public IQueryable<TbcFilial> GetTbcFilial()
	    {




		
	
	        
		
	        
             Linx.CadastroBase.BV.CadastroPfj.CadastroPfjDomainService serviceContext0 = new Linx.CadastroBase.BV.CadastroPfj.CadastroPfjDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TbcFilial> result = 
	            (
                 from TbcFilial_Rep2 in serviceContext0.GetTbcFilialNoAssociations()
	            
	            	
	            select new TbcFilial()		
	            {
	            
                Bairro = TbcFilial_Rep2.Bairro
                , BandeiraRede = TbcFilial_Rep2.BandeiraRede
                , Cep = TbcFilial_Rep2.Cep
                , CnpjCpf = TbcFilial_Rep2.CnpjCpf
                , CodDeposito = TbcFilial_Rep2.CodDeposito
                , CodigoFilial = TbcFilial_Rep2.CodigoFilial
                , CodigoPfj = TbcFilial_Rep2.CodigoPfj
                , Complemento = TbcFilial_Rep2.Complemento
                , DddCelular = TbcFilial_Rep2.DddCelular
                , DddFixo = TbcFilial_Rep2.DddFixo
                , Email = TbcFilial_Rep2.Email
                , FoneCelular = TbcFilial_Rep2.FoneCelular
                , FoneFixo = TbcFilial_Rep2.FoneFixo
                , IdFilialPfj = TbcFilial_Rep2.IdFilialPfj
                , IdGpecon = TbcFilial_Rep2.IdGpecon
                , IdLjvCanalVenda = TbcFilial_Rep2.IdLjvCanalVenda
                , IdMatrizContabil = TbcFilial_Rep2.IdMatrizContabil
                , IdPfj = TbcFilial_Rep2.IdPfj
                , IncluiDeposito = TbcFilial_Rep2.IncluiDeposito
                , IncluiLoja = TbcFilial_Rep2.IncluiLoja
                , IndicaEstrangeiro = TbcFilial_Rep2.IndicaEstrangeiro
                , IndicaFilial = TbcFilial_Rep2.IndicaFilial
                , IndicaLoja = TbcFilial_Rep2.IndicaLoja
                , IndicaMatrizContabil = TbcFilial_Rep2.IndicaMatrizContabil
                , InscrEstadual = TbcFilial_Rep2.InscrEstadual
                , Logradouro = TbcFilial_Rep2.Logradouro
                , LxPfjFisicaJuridica = TbcFilial_Rep2.LxPfjFisicaJuridica
                , LxPfjFisicaJuridicaName = ((TbcFilial_Rep2.LxPfjFisicaJuridica) == 1 ? "Pessoa Física" : ((TbcFilial_Rep2.LxPfjFisicaJuridica) == 2 ? "Pessoa Jurídica" : ""))
                , LxTipoLogradouro = TbcFilial_Rep2.LxTipoLogradouro
                , LxTipoLogradouroName = ((TbcFilial_Rep2.LxTipoLogradouro) == 1 ? "Aeroporto" : ((TbcFilial_Rep2.LxTipoLogradouro) == 2 ? "Alameda" : ((TbcFilial_Rep2.LxTipoLogradouro) == 3 ? "Apartamento" : ((TbcFilial_Rep2.LxTipoLogradouro) == 4 ? "Avenida" : ((TbcFilial_Rep2.LxTipoLogradouro) == 5 ? "Beco" : ((TbcFilial_Rep2.LxTipoLogradouro) == 6 ? "Bloco" : ((TbcFilial_Rep2.LxTipoLogradouro) == 7 ? "Caminho" : ((TbcFilial_Rep2.LxTipoLogradouro) == 8 ? "Escadinha" : ((TbcFilial_Rep2.LxTipoLogradouro) == 9 ? "Estação" : ((TbcFilial_Rep2.LxTipoLogradouro) == 10 ? "Estrada" : ((TbcFilial_Rep2.LxTipoLogradouro) == 11 ? "Fazenda" : ((TbcFilial_Rep2.LxTipoLogradouro) == 12 ? "Fortaleza" : ((TbcFilial_Rep2.LxTipoLogradouro) == 13 ? "Galeria" : ((TbcFilial_Rep2.LxTipoLogradouro) == 14 ? "Ladeira" : ((TbcFilial_Rep2.LxTipoLogradouro) == 15 ? "Largo" : ((TbcFilial_Rep2.LxTipoLogradouro) == 17 ? "Parque" : ((TbcFilial_Rep2.LxTipoLogradouro) == 16 ? "Praça" : ((TbcFilial_Rep2.LxTipoLogradouro) == 18 ? "Praia" : ((TbcFilial_Rep2.LxTipoLogradouro) == 19 ? "Quadra" : ((TbcFilial_Rep2.LxTipoLogradouro) == 20 ? "Quilômetro" : ((TbcFilial_Rep2.LxTipoLogradouro) == 21 ? "Quinta" : ((TbcFilial_Rep2.LxTipoLogradouro) == 22 ? "Rodovia" : ((TbcFilial_Rep2.LxTipoLogradouro) == 23 ? "Rua" : ((TbcFilial_Rep2.LxTipoLogradouro) == 24 ? "Super Quadra" : ((TbcFilial_Rep2.LxTipoLogradouro) == 25 ? "Travessa" : ((TbcFilial_Rep2.LxTipoLogradouro) == 26 ? "Viaduto" : ((TbcFilial_Rep2.LxTipoLogradouro) == 27 ? "Vila" : "")))))))))))))))))))))))))))
                , Municipio = TbcFilial_Rep2.Municipio
                , NomeFantasiaApelido = TbcFilial_Rep2.NomeFantasiaApelido
                , NomeFilial = TbcFilial_Rep2.NomeFilial
                , Numero = TbcFilial_Rep2.Numero
                , ObsEndereco = TbcFilial_Rep2.ObsEndereco
                , Pais = TbcFilial_Rep2.Pais
                , RazaoSocialNomeCompleto = TbcFilial_Rep2.RazaoSocialNomeCompleto
                , Uf = TbcFilial_Rep2.Uf
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TbcFilialNoAssociations.
	    public IQueryable<TbcFilial> GetTbcFilialNoAssociations()
	    {




		
	
	        
		
	        
             Linx.CadastroBase.BV.CadastroPfj.CadastroPfjDomainService serviceContext0 = new Linx.CadastroBase.BV.CadastroPfj.CadastroPfjDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TbcFilial> result = 
	            (
                 from TbcFilial_Rep2 in serviceContext0.GetTbcFilialNoAssociations()
	            
	            	
	            select new TbcFilial()		
	            {
	            
                Bairro = TbcFilial_Rep2.Bairro
                , BandeiraRede = TbcFilial_Rep2.BandeiraRede
                , Cep = TbcFilial_Rep2.Cep
                , CnpjCpf = TbcFilial_Rep2.CnpjCpf
                , CodDeposito = TbcFilial_Rep2.CodDeposito
                , CodigoFilial = TbcFilial_Rep2.CodigoFilial
                , CodigoPfj = TbcFilial_Rep2.CodigoPfj
                , Complemento = TbcFilial_Rep2.Complemento
                , DddCelular = TbcFilial_Rep2.DddCelular
                , DddFixo = TbcFilial_Rep2.DddFixo
                , Email = TbcFilial_Rep2.Email
                , FoneCelular = TbcFilial_Rep2.FoneCelular
                , FoneFixo = TbcFilial_Rep2.FoneFixo
                , IdFilialPfj = TbcFilial_Rep2.IdFilialPfj
                , IdGpecon = TbcFilial_Rep2.IdGpecon
                , IdLjvCanalVenda = TbcFilial_Rep2.IdLjvCanalVenda
                , IdMatrizContabil = TbcFilial_Rep2.IdMatrizContabil
                , IdPfj = TbcFilial_Rep2.IdPfj
                , IncluiDeposito = TbcFilial_Rep2.IncluiDeposito
                , IncluiLoja = TbcFilial_Rep2.IncluiLoja
                , IndicaEstrangeiro = TbcFilial_Rep2.IndicaEstrangeiro
                , IndicaFilial = TbcFilial_Rep2.IndicaFilial
                , IndicaLoja = TbcFilial_Rep2.IndicaLoja
                , IndicaMatrizContabil = TbcFilial_Rep2.IndicaMatrizContabil
                , InscrEstadual = TbcFilial_Rep2.InscrEstadual
                , Logradouro = TbcFilial_Rep2.Logradouro
                , LxPfjFisicaJuridica = TbcFilial_Rep2.LxPfjFisicaJuridica
                , LxPfjFisicaJuridicaName = ((TbcFilial_Rep2.LxPfjFisicaJuridica) == 1 ? "Pessoa Física" : ((TbcFilial_Rep2.LxPfjFisicaJuridica) == 2 ? "Pessoa Jurídica" : ""))
                , LxTipoLogradouro = TbcFilial_Rep2.LxTipoLogradouro
                , LxTipoLogradouroName = ((TbcFilial_Rep2.LxTipoLogradouro) == 1 ? "Aeroporto" : ((TbcFilial_Rep2.LxTipoLogradouro) == 2 ? "Alameda" : ((TbcFilial_Rep2.LxTipoLogradouro) == 3 ? "Apartamento" : ((TbcFilial_Rep2.LxTipoLogradouro) == 4 ? "Avenida" : ((TbcFilial_Rep2.LxTipoLogradouro) == 5 ? "Beco" : ((TbcFilial_Rep2.LxTipoLogradouro) == 6 ? "Bloco" : ((TbcFilial_Rep2.LxTipoLogradouro) == 7 ? "Caminho" : ((TbcFilial_Rep2.LxTipoLogradouro) == 8 ? "Escadinha" : ((TbcFilial_Rep2.LxTipoLogradouro) == 9 ? "Estação" : ((TbcFilial_Rep2.LxTipoLogradouro) == 10 ? "Estrada" : ((TbcFilial_Rep2.LxTipoLogradouro) == 11 ? "Fazenda" : ((TbcFilial_Rep2.LxTipoLogradouro) == 12 ? "Fortaleza" : ((TbcFilial_Rep2.LxTipoLogradouro) == 13 ? "Galeria" : ((TbcFilial_Rep2.LxTipoLogradouro) == 14 ? "Ladeira" : ((TbcFilial_Rep2.LxTipoLogradouro) == 15 ? "Largo" : ((TbcFilial_Rep2.LxTipoLogradouro) == 17 ? "Parque" : ((TbcFilial_Rep2.LxTipoLogradouro) == 16 ? "Praça" : ((TbcFilial_Rep2.LxTipoLogradouro) == 18 ? "Praia" : ((TbcFilial_Rep2.LxTipoLogradouro) == 19 ? "Quadra" : ((TbcFilial_Rep2.LxTipoLogradouro) == 20 ? "Quilômetro" : ((TbcFilial_Rep2.LxTipoLogradouro) == 21 ? "Quinta" : ((TbcFilial_Rep2.LxTipoLogradouro) == 22 ? "Rodovia" : ((TbcFilial_Rep2.LxTipoLogradouro) == 23 ? "Rua" : ((TbcFilial_Rep2.LxTipoLogradouro) == 24 ? "Super Quadra" : ((TbcFilial_Rep2.LxTipoLogradouro) == 25 ? "Travessa" : ((TbcFilial_Rep2.LxTipoLogradouro) == 26 ? "Viaduto" : ((TbcFilial_Rep2.LxTipoLogradouro) == 27 ? "Vila" : "")))))))))))))))))))))))))))
                , Municipio = TbcFilial_Rep2.Municipio
                , NomeFantasiaApelido = TbcFilial_Rep2.NomeFantasiaApelido
                , NomeFilial = TbcFilial_Rep2.NomeFilial
                , Numero = TbcFilial_Rep2.Numero
                , ObsEndereco = TbcFilial_Rep2.ObsEndereco
                , Pais = TbcFilial_Rep2.Pais
                , RazaoSocialNomeCompleto = TbcFilial_Rep2.RazaoSocialNomeCompleto
                , Uf = TbcFilial_Rep2.Uf
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TbcGrupoEconomico.
	    public IQueryable<TbcGrupoEconomico> GetTbcGrupoEconomico()
	    {




		
	
	        
		
	        
             Linx.Operacional.CadastroBase.BV.GrupoEconomico.GrupoEconomicoDomainService serviceContext11 = new Linx.Operacional.CadastroBase.BV.GrupoEconomico.GrupoEconomicoDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TbcGrupoEconomico> result = 
	            (
                 from TbcGrupoEconomico_Rep1 in serviceContext11.GetTbcGrupoEconomicoNoAssociations()
	            
	            	
	            select new TbcGrupoEconomico()		
	            {
	            
                DescGrupoEconomico = TbcGrupoEconomico_Rep1.DescGrupoEconomico
                , IdGpeconCadastro = TbcGrupoEconomico_Rep1.IdGpeconCadastro
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TbcGrupoEconomicoNoAssociations.
	    public IQueryable<TbcGrupoEconomico> GetTbcGrupoEconomicoNoAssociations()
	    {




		
	
	        
		
	        
             Linx.Operacional.CadastroBase.BV.GrupoEconomico.GrupoEconomicoDomainService serviceContext11 = new Linx.Operacional.CadastroBase.BV.GrupoEconomico.GrupoEconomicoDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TbcGrupoEconomico> result = 
	            (
                 from TbcGrupoEconomico_Rep1 in serviceContext11.GetTbcGrupoEconomicoNoAssociations()
	            
	            	
	            select new TbcGrupoEconomico()		
	            {
	            
                DescGrupoEconomico = TbcGrupoEconomico_Rep1.DescGrupoEconomico
                , IdGpeconCadastro = TbcGrupoEconomico_Rep1.IdGpeconCadastro
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get TbcBandeiraRede.
	    public IQueryable<TbcBandeiraRede> GetTbcBandeiraRede()
	    {




		
	
	        
		
	        
             Linx.Operacional.CadastroBase.BV.BandeiraRede.BandeiraRedeDomainService serviceContext9 = new Linx.Operacional.CadastroBase.BV.BandeiraRede.BandeiraRedeDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TbcBandeiraRede> result = 
	            (
                 from TbcBandeiraRede_Rep1 in serviceContext9.GetTbcBandeiraRedeNoAssociations()
	            
	            	
	            select new TbcBandeiraRede()		
	            {
	            
                CodBandeiraRede = TbcBandeiraRede_Rep1.CodBandeiraRede
                , IdBandeiraRedeCadastro = TbcBandeiraRede_Rep1.IdBandeiraRedeCadastro
                , IdLinx = TbcBandeiraRede_Rep1.IdLinx
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TbcBandeiraRedeNoAssociations.
	    public IQueryable<TbcBandeiraRede> GetTbcBandeiraRedeNoAssociations()
	    {




		
	
	        
		
	        
             Linx.Operacional.CadastroBase.BV.BandeiraRede.BandeiraRedeDomainService serviceContext9 = new Linx.Operacional.CadastroBase.BV.BandeiraRede.BandeiraRedeDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TbcBandeiraRede> result = 
	            (
                 from TbcBandeiraRede_Rep1 in serviceContext9.GetTbcBandeiraRedeNoAssociations()
	            
	            	
	            select new TbcBandeiraRede()		
	            {
	            
                CodBandeiraRede = TbcBandeiraRede_Rep1.CodBandeiraRede
                , IdBandeiraRedeCadastro = TbcBandeiraRede_Rep1.IdBandeiraRedeCadastro
                , IdLinx = TbcBandeiraRede_Rep1.IdLinx
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Query(HasSideEffects = false)]
	    //Get LjvCanalVenda.
	    public IQueryable<LjvCanalVenda> GetLjvCanalVenda()
	    {




		
	
	        
		
	        
             Linx.Operacional.CadastroBase.BV.CanalVenda.CanalVendaDomainService serviceContext10 = new Linx.Operacional.CadastroBase.BV.CanalVenda.CanalVendaDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<LjvCanalVenda> result = 
	            (
                 from LjvCanalVenda_Rep1 in serviceContext10.GetLjvCanalVendaNoAssociations()
	            
	            	
	            select new LjvCanalVenda()		
	            {
	            
                CodCanalVenda = LjvCanalVenda_Rep1.CodCanalVenda
                , DescCanalVenda = LjvCanalVenda_Rep1.DescCanalVenda
                , IdLjvCanalVenda = LjvCanalVenda_Rep1.IdLjvCanalVenda
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get LjvCanalVendaNoAssociations.
	    public IQueryable<LjvCanalVenda> GetLjvCanalVendaNoAssociations()
	    {




		
	
	        
		
	        
             Linx.Operacional.CadastroBase.BV.CanalVenda.CanalVendaDomainService serviceContext10 = new Linx.Operacional.CadastroBase.BV.CanalVenda.CanalVendaDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<LjvCanalVenda> result = 
	            (
                 from LjvCanalVenda_Rep1 in serviceContext10.GetLjvCanalVendaNoAssociations()
	            
	            	
	            select new LjvCanalVenda()		
	            {
	            
                CodCanalVenda = LjvCanalVenda_Rep1.CodCanalVenda
                , DescCanalVenda = LjvCanalVenda_Rep1.DescCanalVenda
                , IdLjvCanalVenda = LjvCanalVenda_Rep1.IdLjvCanalVenda
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get Methods Definitions.
	
	    #region Excluded Filters
	
	    private List<string> GetExcludedFilters()
	    {
	    	List<string> result = new List<string>();
	    	//Add filtering disabled property for TCS_AMBIENTE
	    	string[] bmDisabledTcsAmbienteInfoList = this.GetEDM().GetFilteringDisabledList("TCS_AMBIENTE");
	    	if (bmDisabledTcsAmbienteInfoList.Length > 0)
	    	{
	
	    		if (bmDisabledTcsAmbienteInfoList.Contains("TCS_AMBIENTE.ID_TCS_AMBIENTE"))
	    		{
	    			result.Add("TcsAmbienteInfo|IdTcsAmbiente");
	    			result.Add("TcsAmbienteInfo|TCS_AMBIENTE.ID_TCS_AMBIENTE");
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
	    //Get TcsEmpresaAutenticacao By EntitySearchId.
	    public IEnumerable<TcsEmpresaAutenticacao> GetTcsEmpresaAutenticacaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsEmpresaAutenticacaoByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TcsEmpresaAutenticacaoModulo By EntitySearchId.
	    public IEnumerable<TcsEmpresaAutenticacaoModulo> GetTcsEmpresaAutenticacaoModuloByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsEmpresaAutenticacaoModuloByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TcsEmpresaAutenticacao By EntitySearchId.
	    public IEnumerable<TcsEmpresaAutenticacao> GetTcsEmpresaAutenticacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsEmpresaAutenticacaoByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TcsEmpresaAutenticacaoModulo By EntitySearchId.
	    public IEnumerable<TcsEmpresaAutenticacaoModulo> GetTcsEmpresaAutenticacaoModuloByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsEmpresaAutenticacaoModuloByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioAutenticacao By EntitySearchId.
	    public IEnumerable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsUsuarioAutenticacaoByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoAcesso By EntitySearchId.
	    public IEnumerable<TcsUsuarioAutenticacaoAcesso> GetTcsUsuarioAutenticacaoAcessoByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsUsuarioAutenticacaoAcessoByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioAutenticacao By EntitySearchId.
	    public IEnumerable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoAcesso By EntitySearchId.
	    public IEnumerable<TcsUsuarioAutenticacaoAcesso> GetTcsUsuarioAutenticacaoAcessoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsUsuarioAutenticacaoAcessoByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioPerfil By EntitySearchId.
	    public IEnumerable<TcsUsuarioPerfil> GetTcsUsuarioPerfilByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsUsuarioPerfilByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TcsUsuarioPerfil By EntitySearchId.
	    public IEnumerable<TcsUsuarioPerfil> GetTcsUsuarioPerfilByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsUsuarioPerfilByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TcsAmbiente By EntitySearchId.
	    public IQueryable<TcsAmbiente> GetTcsAmbienteByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsAmbienteByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TcsAmbienteConexao By EntitySearchId.
	    public IEnumerable<TcsAmbienteConexao> GetTcsAmbienteConexaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsAmbienteConexaoByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TcsAmbienteUsuarioAcesso By EntitySearchId.
	    public IEnumerable<TcsAmbienteUsuarioAcesso> GetTcsAmbienteUsuarioAcessoByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsAmbienteUsuarioAcessoByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TcsAmbiente By EntitySearchId.
	    public IQueryable<TcsAmbiente> GetTcsAmbienteByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsAmbienteByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TcsAmbienteConexao By EntitySearchId.
	    public IEnumerable<TcsAmbienteConexao> GetTcsAmbienteConexaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsAmbienteConexaoByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TcsAmbienteUsuarioAcesso By EntitySearchId.
	    public IEnumerable<TcsAmbienteUsuarioAcesso> GetTcsAmbienteUsuarioAcessoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsAmbienteUsuarioAcessoByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TcsModuloGrupo By EntitySearchId.
	    public IEnumerable<TcsModuloGrupo> GetTcsModuloGrupoByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsModuloGrupoByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TcsModuloGrupoDetalhe By EntitySearchId.
	    public IEnumerable<TcsModuloGrupoDetalhe> GetTcsModuloGrupoDetalheByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsModuloGrupoDetalheByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TcsModuloGrupo By EntitySearchId.
	    public IEnumerable<TcsModuloGrupo> GetTcsModuloGrupoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsModuloGrupoByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TcsModuloGrupoDetalhe By EntitySearchId.
	    public IEnumerable<TcsModuloGrupoDetalhe> GetTcsModuloGrupoDetalheByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsModuloGrupoDetalheByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TcsParametroValor By EntitySearchId.
	    public IEnumerable<TcsParametroValor> GetTcsParametroValorByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsParametroValorByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TcsParametroValor By EntitySearchId.
	    public IEnumerable<TcsParametroValor> GetTcsParametroValorByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsParametroValorByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TcsPerfil By EntitySearchId.
	    public IEnumerable<TcsPerfil> GetTcsPerfilByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsPerfilByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TcsPerfilRegraModulo By EntitySearchId.
	    public IQueryable<TcsPerfilRegraModulo> GetTcsPerfilRegraModuloByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsPerfilRegraModuloByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TcsPerfilUsuario By EntitySearchId.
	    public IEnumerable<TcsPerfilUsuario> GetTcsPerfilUsuarioByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsPerfilUsuarioByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TcsPerfil By EntitySearchId.
	    public IEnumerable<TcsPerfil> GetTcsPerfilByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsPerfilByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TcsPerfilRegraModulo By EntitySearchId.
	    public IQueryable<TcsPerfilRegraModulo> GetTcsPerfilRegraModuloByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsPerfilRegraModuloByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TcsPerfilUsuario By EntitySearchId.
	    public IEnumerable<TcsPerfilUsuario> GetTcsPerfilUsuarioByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsPerfilUsuarioByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get AmbienteInfo By EntitySearchId.
	    public IEnumerable<AmbienteInfo> GetAmbienteInfoByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetAmbienteInfoByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get AmbienteInfo By EntitySearchId.
	    public IEnumerable<AmbienteInfo> GetAmbienteInfoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetAmbienteInfoByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TcsEmpresaGpecon By EntitySearchId.
	    public IEnumerable<TcsEmpresaGpecon> GetTcsEmpresaGpeconByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsEmpresaGpeconByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TcsEmpresaGpecon By EntitySearchId.
	    public IEnumerable<TcsEmpresaGpecon> GetTcsEmpresaGpeconByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsEmpresaGpeconByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TcsAmbienteInfo By EntitySearchId.
	    public IQueryable<TcsAmbienteInfo> GetTcsAmbienteInfoByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsAmbienteInfoByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TcsAmbienteInfo By EntitySearchId.
	    public IQueryable<TcsAmbienteInfo> GetTcsAmbienteInfoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsAmbienteInfoByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TcsParametroAutorizacao By EntitySearchId.
	    public IEnumerable<TcsParametroAutorizacao> GetTcsParametroAutorizacaoByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsParametroAutorizacaoByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TcsParametroAutorizacao By EntitySearchId.
	    public IEnumerable<TcsParametroAutorizacao> GetTcsParametroAutorizacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTcsParametroAutorizacaoByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get MultimarcaInfo By EntitySearchId.
	    public IEnumerable<MultimarcaInfo> GetMultimarcaInfoByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetMultimarcaInfoByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get MultimarcaInfo By EntitySearchId.
	    public IEnumerable<MultimarcaInfo> GetMultimarcaInfoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetMultimarcaInfoByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TbcFilial By EntitySearchId.
	    public IEnumerable<TbcFilial> GetTbcFilialByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTbcFilialByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TbcFilial By EntitySearchId.
	    public IEnumerable<TbcFilial> GetTbcFilialByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTbcFilialByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TbcGrupoEconomico By EntitySearchId.
	    public IEnumerable<TbcGrupoEconomico> GetTbcGrupoEconomicoByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTbcGrupoEconomicoByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TbcGrupoEconomico By EntitySearchId.
	    public IEnumerable<TbcGrupoEconomico> GetTbcGrupoEconomicoByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTbcGrupoEconomicoByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TbcBandeiraRede By EntitySearchId.
	    public IEnumerable<TbcBandeiraRede> GetTbcBandeiraRedeByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTbcBandeiraRedeByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get TbcBandeiraRede By EntitySearchId.
	    public IEnumerable<TbcBandeiraRede> GetTbcBandeiraRedeByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetTbcBandeiraRedeByEntitySearchNoAssociations(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get LjvCanalVenda By EntitySearchId.
	    public IEnumerable<LjvCanalVenda> GetLjvCanalVendaByEntitySearchId(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetLjvCanalVendaByEntitySearch(queryAnalysis);
	    }
				
	    [Ignore]
	    //Get LjvCanalVenda By EntitySearchId.
	    public IEnumerable<LjvCanalVenda> GetLjvCanalVendaByEntitySearchIdNoAssociations(Guid entitySearchId)
	    {	
	            string queryAnalysis = Linx.Tools.WebCacheHelper.GetWebCache(entitySearchId.ToString()) as string;
	            return this.GetLjvCanalVendaByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get By EntitySearchId Methods Definitions.

	    #region Get QBE Methods Definitions.
	
			
	    //Get TcsEmpresaAutenticacao By Example.
	    [Ignore]
	    public IQueryable<TcsEmpresaAutenticacao> GetTcsEmpresaAutenticacaoByExample(TcsEmpresaAutenticacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsEmpresaAutenticacaoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsEmpresaAutenticacaoModulo By Example.
	    [Ignore]
	    public IQueryable<TcsEmpresaAutenticacaoModulo> GetTcsEmpresaAutenticacaoModuloByExample(TcsEmpresaAutenticacaoModulo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsEmpresaAutenticacaoModuloByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsEmpresaAutenticacao By Example.
	    [Ignore]
	    public IQueryable<TcsEmpresaAutenticacao> GetTcsEmpresaAutenticacaoByExampleNoAssociations(TcsEmpresaAutenticacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsEmpresaAutenticacaoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsEmpresaAutenticacaoModulo By Example.
	    [Ignore]
	    public IQueryable<TcsEmpresaAutenticacaoModulo> GetTcsEmpresaAutenticacaoModuloByExampleNoAssociations(TcsEmpresaAutenticacaoModulo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsEmpresaAutenticacaoModuloByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsUsuarioAutenticacao By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoByExample(TcsUsuarioAutenticacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioAutenticacaoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsUsuarioAutenticacaoAcesso By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioAutenticacaoAcesso> GetTcsUsuarioAutenticacaoAcessoByExample(TcsUsuarioAutenticacaoAcesso entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioAutenticacaoAcessoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsUsuarioAutenticacao By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoByExampleNoAssociations(TcsUsuarioAutenticacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsUsuarioAutenticacaoAcesso By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioAutenticacaoAcesso> GetTcsUsuarioAutenticacaoAcessoByExampleNoAssociations(TcsUsuarioAutenticacaoAcesso entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioAutenticacaoAcessoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsUsuarioPerfil By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioPerfil> GetTcsUsuarioPerfilByExample(TcsUsuarioPerfil entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioPerfilByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsUsuarioPerfil By Example.
	    [Ignore]
	    public IQueryable<TcsUsuarioPerfil> GetTcsUsuarioPerfilByExampleNoAssociations(TcsUsuarioPerfil entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsUsuarioPerfilByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsAmbiente By Example.
	    [Ignore]
	    public IQueryable<TcsAmbiente> GetTcsAmbienteByExample(TcsAmbiente entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAmbienteByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsAmbienteConexao By Example.
	    [Ignore]
	    public IQueryable<TcsAmbienteConexao> GetTcsAmbienteConexaoByExample(TcsAmbienteConexao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAmbienteConexaoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsAmbienteUsuarioAcesso By Example.
	    [Ignore]
	    public IQueryable<TcsAmbienteUsuarioAcesso> GetTcsAmbienteUsuarioAcessoByExample(TcsAmbienteUsuarioAcesso entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAmbienteUsuarioAcessoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsAmbiente By Example.
	    [Ignore]
	    public IQueryable<TcsAmbiente> GetTcsAmbienteByExampleNoAssociations(TcsAmbiente entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAmbienteByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsAmbienteConexao By Example.
	    [Ignore]
	    public IQueryable<TcsAmbienteConexao> GetTcsAmbienteConexaoByExampleNoAssociations(TcsAmbienteConexao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAmbienteConexaoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsAmbienteUsuarioAcesso By Example.
	    [Ignore]
	    public IQueryable<TcsAmbienteUsuarioAcesso> GetTcsAmbienteUsuarioAcessoByExampleNoAssociations(TcsAmbienteUsuarioAcesso entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAmbienteUsuarioAcessoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsModuloGrupo By Example.
	    [Ignore]
	    public IQueryable<TcsModuloGrupo> GetTcsModuloGrupoByExample(TcsModuloGrupo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsModuloGrupoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsModuloGrupoDetalhe By Example.
	    [Ignore]
	    public IQueryable<TcsModuloGrupoDetalhe> GetTcsModuloGrupoDetalheByExample(TcsModuloGrupoDetalhe entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsModuloGrupoDetalheByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsModuloGrupo By Example.
	    [Ignore]
	    public IQueryable<TcsModuloGrupo> GetTcsModuloGrupoByExampleNoAssociations(TcsModuloGrupo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsModuloGrupoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsModuloGrupoDetalhe By Example.
	    [Ignore]
	    public IQueryable<TcsModuloGrupoDetalhe> GetTcsModuloGrupoDetalheByExampleNoAssociations(TcsModuloGrupoDetalhe entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsModuloGrupoDetalheByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsParametroValor By Example.
	    [Ignore]
	    public IQueryable<TcsParametroValor> GetTcsParametroValorByExample(TcsParametroValor entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsParametroValorByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsParametroValor By Example.
	    [Ignore]
	    public IQueryable<TcsParametroValor> GetTcsParametroValorByExampleNoAssociations(TcsParametroValor entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsParametroValorByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsPerfil By Example.
	    [Ignore]
	    public IQueryable<TcsPerfil> GetTcsPerfilByExample(TcsPerfil entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsPerfilByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsPerfilRegraModulo By Example.
	    [Ignore]
	    public IQueryable<TcsPerfilRegraModulo> GetTcsPerfilRegraModuloByExample(TcsPerfilRegraModulo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsPerfilRegraModuloByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsPerfilUsuario By Example.
	    [Ignore]
	    public IQueryable<TcsPerfilUsuario> GetTcsPerfilUsuarioByExample(TcsPerfilUsuario entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsPerfilUsuarioByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsPerfil By Example.
	    [Ignore]
	    public IQueryable<TcsPerfil> GetTcsPerfilByExampleNoAssociations(TcsPerfil entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsPerfilByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsPerfilRegraModulo By Example.
	    [Ignore]
	    public IQueryable<TcsPerfilRegraModulo> GetTcsPerfilRegraModuloByExampleNoAssociations(TcsPerfilRegraModulo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsPerfilRegraModuloByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsPerfilUsuario By Example.
	    [Ignore]
	    public IQueryable<TcsPerfilUsuario> GetTcsPerfilUsuarioByExampleNoAssociations(TcsPerfilUsuario entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsPerfilUsuarioByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get AmbienteInfo By Example.
	    [Ignore]
	    public IEnumerable<AmbienteInfo> GetAmbienteInfoByExample(AmbienteInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetAmbienteInfoByEntitySearch(queryAnalysis);
	    }
			
	    //Get AmbienteInfo By Example.
	    [Ignore]
	    public IEnumerable<AmbienteInfo> GetAmbienteInfoByExampleNoAssociations(AmbienteInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetAmbienteInfoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsEmpresaGpecon By Example.
	    [Ignore]
	    public IQueryable<TcsEmpresaGpecon> GetTcsEmpresaGpeconByExample(TcsEmpresaGpecon entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsEmpresaGpeconByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsEmpresaGpecon By Example.
	    [Ignore]
	    public IQueryable<TcsEmpresaGpecon> GetTcsEmpresaGpeconByExampleNoAssociations(TcsEmpresaGpecon entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsEmpresaGpeconByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsAmbienteInfo By Example.
	    [Ignore]
	    public IQueryable<TcsAmbienteInfo> GetTcsAmbienteInfoByExample(TcsAmbienteInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAmbienteInfoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsAmbienteInfo By Example.
	    [Ignore]
	    public IQueryable<TcsAmbienteInfo> GetTcsAmbienteInfoByExampleNoAssociations(TcsAmbienteInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsAmbienteInfoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TcsParametroAutorizacao By Example.
	    [Ignore]
	    public IQueryable<TcsParametroAutorizacao> GetTcsParametroAutorizacaoByExample(TcsParametroAutorizacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsParametroAutorizacaoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TcsParametroAutorizacao By Example.
	    [Ignore]
	    public IQueryable<TcsParametroAutorizacao> GetTcsParametroAutorizacaoByExampleNoAssociations(TcsParametroAutorizacao entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTcsParametroAutorizacaoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get MultimarcaInfo By Example.
	    [Ignore]
	    public IEnumerable<MultimarcaInfo> GetMultimarcaInfoByExample(MultimarcaInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetMultimarcaInfoByEntitySearch(queryAnalysis);
	    }
			
	    //Get MultimarcaInfo By Example.
	    [Ignore]
	    public IEnumerable<MultimarcaInfo> GetMultimarcaInfoByExampleNoAssociations(MultimarcaInfo entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetMultimarcaInfoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TbcFilial By Example.
	    [Ignore]
	    public IQueryable<TbcFilial> GetTbcFilialByExample(TbcFilial entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTbcFilialByEntitySearch(queryAnalysis);
	    }
			
	    //Get TbcFilial By Example.
	    [Ignore]
	    public IQueryable<TbcFilial> GetTbcFilialByExampleNoAssociations(TbcFilial entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTbcFilialByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TbcGrupoEconomico By Example.
	    [Ignore]
	    public IQueryable<TbcGrupoEconomico> GetTbcGrupoEconomicoByExample(TbcGrupoEconomico entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTbcGrupoEconomicoByEntitySearch(queryAnalysis);
	    }
			
	    //Get TbcGrupoEconomico By Example.
	    [Ignore]
	    public IQueryable<TbcGrupoEconomico> GetTbcGrupoEconomicoByExampleNoAssociations(TbcGrupoEconomico entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTbcGrupoEconomicoByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get TbcBandeiraRede By Example.
	    [Ignore]
	    public IQueryable<TbcBandeiraRede> GetTbcBandeiraRedeByExample(TbcBandeiraRede entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTbcBandeiraRedeByEntitySearch(queryAnalysis);
	    }
			
	    //Get TbcBandeiraRede By Example.
	    [Ignore]
	    public IQueryable<TbcBandeiraRede> GetTbcBandeiraRedeByExampleNoAssociations(TbcBandeiraRede entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetTbcBandeiraRedeByEntitySearchNoAssociations(queryAnalysis);
	    }
			
	    //Get LjvCanalVenda By Example.
	    [Ignore]
	    public IQueryable<LjvCanalVenda> GetLjvCanalVendaByExample(LjvCanalVenda entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetLjvCanalVendaByEntitySearch(queryAnalysis);
	    }
			
	    //Get LjvCanalVenda By Example.
	    [Ignore]
	    public IQueryable<LjvCanalVenda> GetLjvCanalVendaByExampleNoAssociations(LjvCanalVenda entity)
	    {	
	            List<EntitySearch> entities = EntitySearch.ReadQueryFromEntityObject(entity);	
	            string queryAnalysis = SerializationManager<List<EntitySearch>>.ObjectToString(entities);
	            return this.GetLjvCanalVendaByEntitySearchNoAssociations(queryAnalysis);
	    }
		
	    #endregion Get QBE Methods Definitions.
		
	    #region Get Entity By Key



	    [Ignore]
	    public TcsEmpresaAutenticacao GetTcsEmpresaAutenticacaoByKey(int idLinx)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsEmpresaAutenticacao");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdLinx"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idLinx));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsEmpresaAutenticacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsEmpresaAutenticacaoModulo GetTcsEmpresaAutenticacaoModuloByKey(Int32 idTcsEmpresaModulo)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsEmpresaAutenticacaoModulo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsEmpresaModulo"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsEmpresaModulo));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsEmpresaAutenticacaoModuloByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsUsuarioAutenticacao GetTcsUsuarioAutenticacaoByKey(Int64 idUsuario)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsUsuarioAutenticacao");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdUsuario"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idUsuario));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsUsuarioAutenticacaoAcesso GetTcsUsuarioAutenticacaoAcessoByKey(Int32 idTcsUsuarioAcesso)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsUsuarioAutenticacaoAcesso");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsUsuarioAcesso"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsUsuarioAcesso));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsUsuarioAutenticacaoAcessoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsUsuarioPerfil GetTcsUsuarioPerfilByKey(Int64 idTcsUsuarioPerfil)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsUsuarioPerfil");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsUsuarioPerfil"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsUsuarioPerfil));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsUsuarioPerfilByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsAmbiente GetTcsAmbienteByKey(Int32 idTcsAmbiente)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsAmbiente");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsAmbiente"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsAmbiente));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsAmbienteByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsAmbienteConexao GetTcsAmbienteConexaoByKey(Int32 idTcsAmbienteConexao)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsAmbienteConexao");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsAmbienteConexao"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsAmbienteConexao));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsAmbienteConexaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsAmbienteUsuarioAcesso GetTcsAmbienteUsuarioAcessoByKey(Int32 idTcsUsuarioAcesso)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsAmbienteUsuarioAcesso");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsUsuarioAcesso"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsUsuarioAcesso));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsAmbienteUsuarioAcessoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsModuloGrupo GetTcsModuloGrupoByKey(Int64 idGrupoModulo)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsModuloGrupo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdGrupoModulo"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idGrupoModulo));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsModuloGrupoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsModuloGrupoDetalhe GetTcsModuloGrupoDetalheByKey(Int64 idModuloDoGrupo)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsModuloGrupoDetalhe");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdModuloDoGrupo"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idModuloDoGrupo));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsModuloGrupoDetalheByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsParametroValor GetTcsParametroValorByKey(Int64 idParametroValor)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsParametroValor");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdParametroValor"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idParametroValor));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsParametroValorByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsPerfil GetTcsPerfilByKey(Int64 idPerfil)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsPerfil");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdPerfil"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idPerfil));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsPerfilByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsPerfilRegraModulo GetTcsPerfilRegraModuloByKey(Int64 idPerfilRegraModulo)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsPerfilRegraModulo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdPerfilRegraModulo"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idPerfilRegraModulo));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsPerfilRegraModuloByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsPerfilUsuario GetTcsPerfilUsuarioByKey(Int64 idTcsUsuarioPerfil)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsPerfilUsuario");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsUsuarioPerfil"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsUsuarioPerfil));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsPerfilUsuarioByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public AmbienteInfo GetAmbienteInfoByKey(int idLinx)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("AmbienteInfo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdLinx"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idLinx));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetAmbienteInfoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsEmpresaGpecon GetTcsEmpresaGpeconByKey(Int32 idLinx, Int32 idLinxGpecon)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsEmpresaGpecon");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdLinx"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idLinx));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdLinxGpecon"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idLinxGpecon));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsEmpresaGpeconByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsAmbienteInfo GetTcsAmbienteInfoByKey(int idTcsAmbiente)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsAmbienteInfo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdTcsAmbiente"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idTcsAmbiente));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsAmbienteInfoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TcsParametroAutorizacao GetTcsParametroAutorizacaoByKey(Int64 idParametro)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TcsParametroAutorizacao");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdParametro"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idParametro));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTcsParametroAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public MultimarcaInfo GetMultimarcaInfoByKey(int idLinx)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("MultimarcaInfo");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdLinx"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idLinx));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetMultimarcaInfoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TbcFilial GetTbcFilialByKey(Int32 idPfj)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TbcFilial");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdPfj"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idPfj));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTbcFilialByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TbcGrupoEconomico GetTbcGrupoEconomicoByKey(Int32 idGpeconCadastro)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TbcGrupoEconomico");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdGpeconCadastro"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idGpeconCadastro));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTbcGrupoEconomicoByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public TbcBandeiraRede GetTbcBandeiraRedeByKey(Int32 idBandeiraRedeCadastro)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("TbcBandeiraRede");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdBandeiraRedeCadastro"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idBandeiraRedeCadastro));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetTbcBandeiraRedeByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }


	    [Ignore]
	    public LjvCanalVenda GetLjvCanalVendaByKey(Int32 idLjvCanalVenda)
	    {
	         List<EntitySearch> queryFilters = new List<EntitySearch>();
	         EntitySearch search = new EntitySearch("LjvCanalVenda");
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdLjvCanalVenda"));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
	         search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, idLjvCanalVenda));
	         queryFilters.Add(search);
	         string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(queryFilters);
	         return (from r in this.GetLjvCanalVendaByEntitySearchNoAssociations(serializedEntitySearch) select r).FirstOrDefault();
	    }

	    #endregion Get Entity By Key
	
	    #region Get By Entity Search Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get TcsEmpresaAutenticacaoByEntitySearch.
	    public IQueryable<TcsEmpresaAutenticacao> GetTcsEmpresaAutenticacaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsEmpresaAutenticacao", "TcsEmpresaAutenticacao", 0, "CnpjCpf#CnpjCpf","IdLinx#IdLinx","NomeEmpresa#NomeEmpresa","UidEmpresa#UidEmpresa");
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsEmpresaAutenticacaoModulo", "TcsEmpresaModulo", 0, "IdLinx#IdLinx","IdModulo#IdModulo","IdTcsAplicativo#IdTcsAplicativo","IdTcsEmpresaModulo#IdTcsEmpresaModulo");
		
	        
             Linx.Framework.BV.Empresa.EmpresaDomainService serviceContext2 = new Linx.Framework.BV.Empresa.EmpresaDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsEmpresaAutenticacao> result = 
	            (
                 from TcsEmpresaAutenticacao_Rep1 in serviceContext2.GetTcsEmpresaAutenticacaoByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsEmpresaAutenticacao()		
	            {
	            
                CnpjCpf = TcsEmpresaAutenticacao_Rep1.CnpjCpf
                , IdLinx = TcsEmpresaAutenticacao_Rep1.IdLinx
                , NomeEmpresa = TcsEmpresaAutenticacao_Rep1.NomeEmpresa
                , UidEmpresa = TcsEmpresaAutenticacao_Rep1.UidEmpresa
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsEmpresaAutenticacaoModuloByEntitySearch.
	    public IQueryable<TcsEmpresaAutenticacaoModulo> GetTcsEmpresaAutenticacaoModuloByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsEmpresaAutenticacaoModulo", "TcsEmpresaModulo", 0, "IdLinx#IdLinx","IdModulo#IdModulo","IdTcsAplicativo#IdTcsAplicativo","IdTcsEmpresaModulo#IdTcsEmpresaModulo");
		
	        
             Linx.Framework.BV.Empresa.EmpresaDomainService serviceContext2 = new Linx.Framework.BV.Empresa.EmpresaDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsEmpresaAutenticacaoModulo> result = 
	            (
                 from TcsEmpresaModulo_Rep1 in serviceContext2.GetTcsEmpresaModuloByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsEmpresaAutenticacaoModulo()		
	            {
	            
                IdLinx = TcsEmpresaModulo_Rep1.IdLinx
                , IdModulo = TcsEmpresaModulo_Rep1.IdModulo
                , IdTcsAplicativo = TcsEmpresaModulo_Rep1.IdTcsAplicativo
                , IdTcsEmpresaModulo = TcsEmpresaModulo_Rep1.IdTcsEmpresaModulo
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsEmpresaAutenticacaoByEntitySearchNoAssociations.
	    public IQueryable<TcsEmpresaAutenticacao> GetTcsEmpresaAutenticacaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsEmpresaAutenticacao", "TcsEmpresaAutenticacao", 0, "CnpjCpf#CnpjCpf","IdLinx#IdLinx","NomeEmpresa#NomeEmpresa","UidEmpresa#UidEmpresa");
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsEmpresaAutenticacaoModulo", "TcsEmpresaModulo", 0, "IdLinx#IdLinx","IdModulo#IdModulo","IdTcsAplicativo#IdTcsAplicativo","IdTcsEmpresaModulo#IdTcsEmpresaModulo");
		
	        
             Linx.Framework.BV.Empresa.EmpresaDomainService serviceContext2 = new Linx.Framework.BV.Empresa.EmpresaDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsEmpresaAutenticacao> result = 
	            (
                 from TcsEmpresaAutenticacao_Rep1 in serviceContext2.GetTcsEmpresaAutenticacaoByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsEmpresaAutenticacao()		
	            {
	            
                CnpjCpf = TcsEmpresaAutenticacao_Rep1.CnpjCpf
                , IdLinx = TcsEmpresaAutenticacao_Rep1.IdLinx
                , NomeEmpresa = TcsEmpresaAutenticacao_Rep1.NomeEmpresa
                , UidEmpresa = TcsEmpresaAutenticacao_Rep1.UidEmpresa
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsEmpresaAutenticacaoModuloByEntitySearchNoAssociations.
	    public IQueryable<TcsEmpresaAutenticacaoModulo> GetTcsEmpresaAutenticacaoModuloByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsEmpresaAutenticacaoModulo", "TcsEmpresaModulo", 0, "IdLinx#IdLinx","IdModulo#IdModulo","IdTcsAplicativo#IdTcsAplicativo","IdTcsEmpresaModulo#IdTcsEmpresaModulo");
		
	        
             Linx.Framework.BV.Empresa.EmpresaDomainService serviceContext2 = new Linx.Framework.BV.Empresa.EmpresaDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsEmpresaAutenticacaoModulo> result = 
	            (
                 from TcsEmpresaModulo_Rep1 in serviceContext2.GetTcsEmpresaModuloByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsEmpresaAutenticacaoModulo()		
	            {
	            
                IdLinx = TcsEmpresaModulo_Rep1.IdLinx
                , IdModulo = TcsEmpresaModulo_Rep1.IdModulo
                , IdTcsAplicativo = TcsEmpresaModulo_Rep1.IdTcsAplicativo
                , IdTcsEmpresaModulo = TcsEmpresaModulo_Rep1.IdTcsEmpresaModulo
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoByEntitySearch.
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsUsuarioAutenticacao", "TcsUsuarioAutenticacao", 0, "AutenticacaoWindows#AutenticacaoWindows","CnpjCpf#CnpjCpf","ConfirmacaoUsuario#ConfirmacaoUsuario","ConfirmacaoUsuario1#ConfirmacaoUsuario1","CriaUsuario#CriaUsuario","DataAlteracao#DataAlteracao","DataCadastro#DataCadastro","DataExpiracaoSenha#DataExpiracaoSenha","Email#Email","GeraSenhaUsuario#GeraSenhaUsuario","IdLinx#IdLinx","IdUsuario#IdUsuario","LxPfjFisicaJuridica#LxPfjFisicaJuridica","NomeAutenticacao#NomeAutenticacao","NomeCurtoUsuario#NomeCurtoUsuario","NomeUsuario#NomeUsuario","UidUsuario#UidUsuario","VigenciaFinal#VigenciaFinal","VigenciaInicial#VigenciaInicial","Bairro#Bairro","Cep#Cep","Complemento#Complemento","FoneCelular#FoneCelular","FoneFixo#FoneFixo","Inativo#Inativo","IndicaAcessoSuporte#IndicaAcessoSuporte","InscrEstadualRg#InscrEstadualRg","Logradouro#Logradouro","LxTipoLogradouro#LxTipoLogradouro","Municipio#Municipio","NomeEmpresa#NomeEmpresa","Numero#Numero","ObsEndereco#ObsEndereco","Ramal#Ramal","Uf#Uf","UidEmpresa#UidEmpresa");
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsUsuarioAutenticacaoAcesso", "TcsUsuarioAcesso", 0, "IdTcsAmbiente#IdTcsAmbiente","IdTcsAmbienteRelacionado#IdTcsAmbienteRelacionado","IdTcsUsuarioAcesso#IdTcsUsuarioAcesso","IdUsuario#IdUsuario","IndicaAcessoPadrao#IndicaAcessoPadrao","IndicaAdministrador#IndicaAdministrador","IndicaMultiGpecon#IndicaMultiGpecon");
		
	        
             Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService serviceContext8 = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsUsuarioAutenticacao> result = 
	            (
                 from TcsUsuarioAutenticacao_Rep1 in serviceContext8.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsUsuarioAutenticacao()		
	            {
	            
                AutenticacaoWindows = TcsUsuarioAutenticacao_Rep1.AutenticacaoWindows
                , CnpjCpf = TcsUsuarioAutenticacao_Rep1.CnpjCpf
                , ConfirmacaoUsuario = TcsUsuarioAutenticacao_Rep1.ConfirmacaoUsuario
                , ConfirmacaoUsuario1 = TcsUsuarioAutenticacao_Rep1.ConfirmacaoUsuario1
                , CriaUsuario = TcsUsuarioAutenticacao_Rep1.CriaUsuario
                , DataAlteracao = TcsUsuarioAutenticacao_Rep1.DataAlteracao
                , DataCadastro = TcsUsuarioAutenticacao_Rep1.DataCadastro
                , DataExpiracaoSenha = TcsUsuarioAutenticacao_Rep1.DataExpiracaoSenha
                , Email = TcsUsuarioAutenticacao_Rep1.Email
                , GeraSenhaUsuario = TcsUsuarioAutenticacao_Rep1.GeraSenhaUsuario
                , IdLinx = TcsUsuarioAutenticacao_Rep1.IdLinx
                , IdUsuario = TcsUsuarioAutenticacao_Rep1.IdUsuario
                , LxPfjFisicaJuridica = TcsUsuarioAutenticacao_Rep1.LxPfjFisicaJuridica
                , LxPfjFisicaJuridicaName = ((TcsUsuarioAutenticacao_Rep1.LxPfjFisicaJuridica) == 1 ? "Pessoa Física" : ((TcsUsuarioAutenticacao_Rep1.LxPfjFisicaJuridica) == 2 ? "Pessoa Jurídica" : ""))
                , NomeAutenticacao = TcsUsuarioAutenticacao_Rep1.NomeAutenticacao
                , NomeCurtoUsuario = TcsUsuarioAutenticacao_Rep1.NomeCurtoUsuario
                , NomeUsuario = TcsUsuarioAutenticacao_Rep1.NomeUsuario
                , UidUsuario = TcsUsuarioAutenticacao_Rep1.UidUsuario
                , VigenciaFinal = TcsUsuarioAutenticacao_Rep1.VigenciaFinal
                , VigenciaInicial = TcsUsuarioAutenticacao_Rep1.VigenciaInicial
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoAcessoByEntitySearch.
	    public IQueryable<TcsUsuarioAutenticacaoAcesso> GetTcsUsuarioAutenticacaoAcessoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsUsuarioAutenticacaoAcesso", "TcsUsuarioAcesso", 0, "IdTcsAmbiente#IdTcsAmbiente","IdTcsAmbienteRelacionado#IdTcsAmbienteRelacionado","IdTcsUsuarioAcesso#IdTcsUsuarioAcesso","IdUsuario#IdUsuario","IndicaAcessoPadrao#IndicaAcessoPadrao","IndicaAdministrador#IndicaAdministrador","IndicaMultiGpecon#IndicaMultiGpecon");
		
	        
             Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService serviceContext8 = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsUsuarioAutenticacaoAcesso> result = 
	            (
                 from TcsUsuarioAcesso_Rep1 in serviceContext8.GetTcsUsuarioAcessoByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsUsuarioAutenticacaoAcesso()		
	            {
	            
                IdTcsAmbiente = TcsUsuarioAcesso_Rep1.IdTcsAmbiente
                , IdTcsAmbienteRelacionado = TcsUsuarioAcesso_Rep1.IdTcsAmbienteRelacionado
                , IdTcsUsuarioAcesso = TcsUsuarioAcesso_Rep1.IdTcsUsuarioAcesso
                , IdUsuario = TcsUsuarioAcesso_Rep1.IdUsuario
                , IndicaAcessoPadrao = TcsUsuarioAcesso_Rep1.IndicaAcessoPadrao
                , IndicaAdministrador = TcsUsuarioAcesso_Rep1.IndicaAdministrador
                , IndicaMultiGpecon = TcsUsuarioAcesso_Rep1.IndicaMultiGpecon
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsUsuarioAutenticacao", "TcsUsuarioAutenticacao", 0, "AutenticacaoWindows#AutenticacaoWindows","CnpjCpf#CnpjCpf","ConfirmacaoUsuario#ConfirmacaoUsuario","ConfirmacaoUsuario1#ConfirmacaoUsuario1","CriaUsuario#CriaUsuario","DataAlteracao#DataAlteracao","DataCadastro#DataCadastro","DataExpiracaoSenha#DataExpiracaoSenha","Email#Email","GeraSenhaUsuario#GeraSenhaUsuario","IdLinx#IdLinx","IdUsuario#IdUsuario","LxPfjFisicaJuridica#LxPfjFisicaJuridica","NomeAutenticacao#NomeAutenticacao","NomeCurtoUsuario#NomeCurtoUsuario","NomeUsuario#NomeUsuario","UidUsuario#UidUsuario","VigenciaFinal#VigenciaFinal","VigenciaInicial#VigenciaInicial","Bairro#Bairro","Cep#Cep","Complemento#Complemento","FoneCelular#FoneCelular","FoneFixo#FoneFixo","Inativo#Inativo","IndicaAcessoSuporte#IndicaAcessoSuporte","InscrEstadualRg#InscrEstadualRg","Logradouro#Logradouro","LxTipoLogradouro#LxTipoLogradouro","Municipio#Municipio","NomeEmpresa#NomeEmpresa","Numero#Numero","ObsEndereco#ObsEndereco","Ramal#Ramal","Uf#Uf","UidEmpresa#UidEmpresa");
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsUsuarioAutenticacaoAcesso", "TcsUsuarioAcesso", 0, "IdTcsAmbiente#IdTcsAmbiente","IdTcsAmbienteRelacionado#IdTcsAmbienteRelacionado","IdTcsUsuarioAcesso#IdTcsUsuarioAcesso","IdUsuario#IdUsuario","IndicaAcessoPadrao#IndicaAcessoPadrao","IndicaAdministrador#IndicaAdministrador","IndicaMultiGpecon#IndicaMultiGpecon");
		
	        
             Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService serviceContext8 = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsUsuarioAutenticacao> result = 
	            (
                 from TcsUsuarioAutenticacao_Rep1 in serviceContext8.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsUsuarioAutenticacao()		
	            {
	            
                AutenticacaoWindows = TcsUsuarioAutenticacao_Rep1.AutenticacaoWindows
                , CnpjCpf = TcsUsuarioAutenticacao_Rep1.CnpjCpf
                , ConfirmacaoUsuario = TcsUsuarioAutenticacao_Rep1.ConfirmacaoUsuario
                , ConfirmacaoUsuario1 = TcsUsuarioAutenticacao_Rep1.ConfirmacaoUsuario1
                , CriaUsuario = TcsUsuarioAutenticacao_Rep1.CriaUsuario
                , DataAlteracao = TcsUsuarioAutenticacao_Rep1.DataAlteracao
                , DataCadastro = TcsUsuarioAutenticacao_Rep1.DataCadastro
                , DataExpiracaoSenha = TcsUsuarioAutenticacao_Rep1.DataExpiracaoSenha
                , Email = TcsUsuarioAutenticacao_Rep1.Email
                , GeraSenhaUsuario = TcsUsuarioAutenticacao_Rep1.GeraSenhaUsuario
                , IdLinx = TcsUsuarioAutenticacao_Rep1.IdLinx
                , IdUsuario = TcsUsuarioAutenticacao_Rep1.IdUsuario
                , LxPfjFisicaJuridica = TcsUsuarioAutenticacao_Rep1.LxPfjFisicaJuridica
                , LxPfjFisicaJuridicaName = ((TcsUsuarioAutenticacao_Rep1.LxPfjFisicaJuridica) == 1 ? "Pessoa Física" : ((TcsUsuarioAutenticacao_Rep1.LxPfjFisicaJuridica) == 2 ? "Pessoa Jurídica" : ""))
                , NomeAutenticacao = TcsUsuarioAutenticacao_Rep1.NomeAutenticacao
                , NomeCurtoUsuario = TcsUsuarioAutenticacao_Rep1.NomeCurtoUsuario
                , NomeUsuario = TcsUsuarioAutenticacao_Rep1.NomeUsuario
                , UidUsuario = TcsUsuarioAutenticacao_Rep1.UidUsuario
                , VigenciaFinal = TcsUsuarioAutenticacao_Rep1.VigenciaFinal
                , VigenciaInicial = TcsUsuarioAutenticacao_Rep1.VigenciaInicial
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioAutenticacaoAcessoByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioAutenticacaoAcesso> GetTcsUsuarioAutenticacaoAcessoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsUsuarioAutenticacaoAcesso", "TcsUsuarioAcesso", 0, "IdTcsAmbiente#IdTcsAmbiente","IdTcsAmbienteRelacionado#IdTcsAmbienteRelacionado","IdTcsUsuarioAcesso#IdTcsUsuarioAcesso","IdUsuario#IdUsuario","IndicaAcessoPadrao#IndicaAcessoPadrao","IndicaAdministrador#IndicaAdministrador","IndicaMultiGpecon#IndicaMultiGpecon");
		
	        
             Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService serviceContext8 = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsUsuarioAutenticacaoAcesso> result = 
	            (
                 from TcsUsuarioAcesso_Rep1 in serviceContext8.GetTcsUsuarioAcessoByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsUsuarioAutenticacaoAcesso()		
	            {
	            
                IdTcsAmbiente = TcsUsuarioAcesso_Rep1.IdTcsAmbiente
                , IdTcsAmbienteRelacionado = TcsUsuarioAcesso_Rep1.IdTcsAmbienteRelacionado
                , IdTcsUsuarioAcesso = TcsUsuarioAcesso_Rep1.IdTcsUsuarioAcesso
                , IdUsuario = TcsUsuarioAcesso_Rep1.IdUsuario
                , IndicaAcessoPadrao = TcsUsuarioAcesso_Rep1.IndicaAcessoPadrao
                , IndicaAdministrador = TcsUsuarioAcesso_Rep1.IndicaAdministrador
                , IndicaMultiGpecon = TcsUsuarioAcesso_Rep1.IndicaMultiGpecon
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioPerfilByEntitySearch.
	    public IQueryable<TcsUsuarioPerfil> GetTcsUsuarioPerfilByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsUsuarioPerfil", "TcsUsuarioPerfilP", 0, "IdPerfil#IdPerfil","IdTcsUsuarioPerfil#IdTcsUsuarioPerfil","IdUsuario#IdUsuario");
		
	        
             Linx.Framework.BV.Usuario.UsuarioDomainService serviceContext7 = new Linx.Framework.BV.Usuario.UsuarioDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsUsuarioPerfil> result = 
	            (
                 from TcsUsuarioPerfilP_Rep1 in serviceContext7.GetTcsUsuarioPerfilPByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsUsuarioPerfil()		
	            {
	            
                IdPerfil = TcsUsuarioPerfilP_Rep1.IdPerfil
                , IdTcsUsuarioPerfil = TcsUsuarioPerfilP_Rep1.IdTcsUsuarioPerfil
                , IdUsuario = TcsUsuarioPerfilP_Rep1.IdUsuario
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsUsuarioPerfilByEntitySearchNoAssociations.
	    public IQueryable<TcsUsuarioPerfil> GetTcsUsuarioPerfilByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsUsuarioPerfil", "TcsUsuarioPerfilP", 0, "IdPerfil#IdPerfil","IdTcsUsuarioPerfil#IdTcsUsuarioPerfil","IdUsuario#IdUsuario");
		
	        
             Linx.Framework.BV.Usuario.UsuarioDomainService serviceContext7 = new Linx.Framework.BV.Usuario.UsuarioDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsUsuarioPerfil> result = 
	            (
                 from TcsUsuarioPerfilP_Rep1 in serviceContext7.GetTcsUsuarioPerfilPByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsUsuarioPerfil()		
	            {
	            
                IdPerfil = TcsUsuarioPerfilP_Rep1.IdPerfil
                , IdTcsUsuarioPerfil = TcsUsuarioPerfilP_Rep1.IdTcsUsuarioPerfil
                , IdUsuario = TcsUsuarioPerfilP_Rep1.IdUsuario
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteByEntitySearch.
	    public IQueryable<TcsAmbiente> GetTcsAmbienteByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsAmbiente", "TcsAmbiente", 0, "DescricaoAmbiente#DescricaoAmbiente","IdAplicacao#IdAplicacao","IdLinx#IdLinx","IdTcsAmbiente#IdTcsAmbiente","UidEmpresa#UidEmpresa","DescricaoAplicacao#DescricaoAplicacao","DescricaoAplicativo#DescricaoAplicativo","EmDesenvolvimento#EmDesenvolvimento","IdTcsAplicativo#IdTcsAplicativo","NomeEmpresa#NomeEmpresa","UidAplicacao#UidAplicacao","Url#Url","UrlWorkArea#UrlWorkArea");
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsAmbienteConexao", "TcsAmbienteConexao", 0, "IdTcsAmbiente#IdTcsAmbiente","IdTcsAmbienteConexao#IdTcsAmbienteConexao","IdTcsAplicativoConexao#IdTcsAplicativoConexao","IdTcsBancoServidor#IdTcsBancoServidor");
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsAmbienteUsuarioAcesso", "TcsAmbienteUsuarioAcesso", 0, "IdTcsAmbiente#IdTcsAmbiente","IdTcsUsuarioAcesso#IdTcsUsuarioAcesso","IdUsuario#IdUsuario","IndicaAdministrador#IndicaAdministrador","IndicaMultiGpecon#IndicaMultiGpecon","NomeAutenticacao#NomeAutenticacao","NomeUsuario#NomeUsuario","UidUsuario#UidUsuario");
		
	        
             Linx.Framework.BV.Ambiente.AmbienteDomainService serviceContext1 = new Linx.Framework.BV.Ambiente.AmbienteDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsAmbiente> result = 
	            (
                 from TcsAmbiente_Rep1 in serviceContext1.GetTcsAmbienteByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsAmbiente()		
	            {
	            
                DescricaoAmbiente = TcsAmbiente_Rep1.DescricaoAmbiente
                , IdAplicacao = TcsAmbiente_Rep1.IdAplicacao
                , IdLinx = TcsAmbiente_Rep1.IdLinx
                , IdTcsAmbiente = TcsAmbiente_Rep1.IdTcsAmbiente
                , UidEmpresa = TcsAmbiente_Rep1.UidEmpresa
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteConexaoByEntitySearch.
	    public IQueryable<TcsAmbienteConexao> GetTcsAmbienteConexaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsAmbienteConexao", "TcsAmbienteConexao", 0, "IdTcsAmbiente#IdTcsAmbiente","IdTcsAmbienteConexao#IdTcsAmbienteConexao","IdTcsAplicativoConexao#IdTcsAplicativoConexao","IdTcsBancoServidor#IdTcsBancoServidor");
		
	        
             Linx.Framework.BV.Ambiente.AmbienteDomainService serviceContext1 = new Linx.Framework.BV.Ambiente.AmbienteDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsAmbienteConexao> result = 
	            (
                 from TcsAmbienteConexao_Rep1 in serviceContext1.GetTcsAmbienteConexaoByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsAmbienteConexao()		
	            {
	            
                IdTcsAmbiente = TcsAmbienteConexao_Rep1.IdTcsAmbiente
                , IdTcsAmbienteConexao = TcsAmbienteConexao_Rep1.IdTcsAmbienteConexao
                , IdTcsAplicativoConexao = TcsAmbienteConexao_Rep1.IdTcsAplicativoConexao
                , IdTcsBancoServidor = TcsAmbienteConexao_Rep1.IdTcsBancoServidor
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteUsuarioAcessoByEntitySearch.
	    public IQueryable<TcsAmbienteUsuarioAcesso> GetTcsAmbienteUsuarioAcessoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsAmbienteUsuarioAcesso", "TcsAmbienteUsuarioAcesso", 0, "IdTcsAmbiente#IdTcsAmbiente","IdTcsUsuarioAcesso#IdTcsUsuarioAcesso","IdUsuario#IdUsuario","IndicaAdministrador#IndicaAdministrador","IndicaMultiGpecon#IndicaMultiGpecon","NomeAutenticacao#NomeAutenticacao","NomeUsuario#NomeUsuario","UidUsuario#UidUsuario");
		
	        
             Linx.Framework.BV.Ambiente.AmbienteDomainService serviceContext1 = new Linx.Framework.BV.Ambiente.AmbienteDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsAmbienteUsuarioAcesso> result = 
	            (
                 from TcsAmbienteUsuarioAcesso_Rep1 in serviceContext1.GetTcsAmbienteUsuarioAcessoByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsAmbienteUsuarioAcesso()		
	            {
	            
                IdTcsAmbiente = TcsAmbienteUsuarioAcesso_Rep1.IdTcsAmbiente
                , IdTcsUsuarioAcesso = TcsAmbienteUsuarioAcesso_Rep1.IdTcsUsuarioAcesso
                , IdUsuario = TcsAmbienteUsuarioAcesso_Rep1.IdUsuario
                , IndicaAdministrador = TcsAmbienteUsuarioAcesso_Rep1.IndicaAdministrador
                , IndicaMultiGpecon = TcsAmbienteUsuarioAcesso_Rep1.IndicaMultiGpecon
                , NomeAutenticacao = TcsAmbienteUsuarioAcesso_Rep1.NomeAutenticacao
                , NomeUsuario = TcsAmbienteUsuarioAcesso_Rep1.NomeUsuario
                , UidUsuario = TcsAmbienteUsuarioAcesso_Rep1.UidUsuario
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteByEntitySearchNoAssociations.
	    public IQueryable<TcsAmbiente> GetTcsAmbienteByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsAmbiente", "TcsAmbiente", 0, "DescricaoAmbiente#DescricaoAmbiente","IdAplicacao#IdAplicacao","IdLinx#IdLinx","IdTcsAmbiente#IdTcsAmbiente","UidEmpresa#UidEmpresa","DescricaoAplicacao#DescricaoAplicacao","DescricaoAplicativo#DescricaoAplicativo","EmDesenvolvimento#EmDesenvolvimento","IdTcsAplicativo#IdTcsAplicativo","NomeEmpresa#NomeEmpresa","UidAplicacao#UidAplicacao","Url#Url","UrlWorkArea#UrlWorkArea");
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsAmbienteConexao", "TcsAmbienteConexao", 0, "IdTcsAmbiente#IdTcsAmbiente","IdTcsAmbienteConexao#IdTcsAmbienteConexao","IdTcsAplicativoConexao#IdTcsAplicativoConexao","IdTcsBancoServidor#IdTcsBancoServidor");
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsAmbienteUsuarioAcesso", "TcsAmbienteUsuarioAcesso", 0, "IdTcsAmbiente#IdTcsAmbiente","IdTcsUsuarioAcesso#IdTcsUsuarioAcesso","IdUsuario#IdUsuario","IndicaAdministrador#IndicaAdministrador","IndicaMultiGpecon#IndicaMultiGpecon","NomeAutenticacao#NomeAutenticacao","NomeUsuario#NomeUsuario","UidUsuario#UidUsuario");
		
	        
             Linx.Framework.BV.Ambiente.AmbienteDomainService serviceContext1 = new Linx.Framework.BV.Ambiente.AmbienteDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsAmbiente> result = 
	            (
                 from TcsAmbiente_Rep1 in serviceContext1.GetTcsAmbienteByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsAmbiente()		
	            {
	            
                DescricaoAmbiente = TcsAmbiente_Rep1.DescricaoAmbiente
                , IdAplicacao = TcsAmbiente_Rep1.IdAplicacao
                , IdLinx = TcsAmbiente_Rep1.IdLinx
                , IdTcsAmbiente = TcsAmbiente_Rep1.IdTcsAmbiente
                , UidEmpresa = TcsAmbiente_Rep1.UidEmpresa
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteConexaoByEntitySearchNoAssociations.
	    public IQueryable<TcsAmbienteConexao> GetTcsAmbienteConexaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsAmbienteConexao", "TcsAmbienteConexao", 0, "IdTcsAmbiente#IdTcsAmbiente","IdTcsAmbienteConexao#IdTcsAmbienteConexao","IdTcsAplicativoConexao#IdTcsAplicativoConexao","IdTcsBancoServidor#IdTcsBancoServidor");
		
	        
             Linx.Framework.BV.Ambiente.AmbienteDomainService serviceContext1 = new Linx.Framework.BV.Ambiente.AmbienteDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsAmbienteConexao> result = 
	            (
                 from TcsAmbienteConexao_Rep1 in serviceContext1.GetTcsAmbienteConexaoByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsAmbienteConexao()		
	            {
	            
                IdTcsAmbiente = TcsAmbienteConexao_Rep1.IdTcsAmbiente
                , IdTcsAmbienteConexao = TcsAmbienteConexao_Rep1.IdTcsAmbienteConexao
                , IdTcsAplicativoConexao = TcsAmbienteConexao_Rep1.IdTcsAplicativoConexao
                , IdTcsBancoServidor = TcsAmbienteConexao_Rep1.IdTcsBancoServidor
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteUsuarioAcessoByEntitySearchNoAssociations.
	    public IQueryable<TcsAmbienteUsuarioAcesso> GetTcsAmbienteUsuarioAcessoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsAmbienteUsuarioAcesso", "TcsAmbienteUsuarioAcesso", 0, "IdTcsAmbiente#IdTcsAmbiente","IdTcsUsuarioAcesso#IdTcsUsuarioAcesso","IdUsuario#IdUsuario","IndicaAdministrador#IndicaAdministrador","IndicaMultiGpecon#IndicaMultiGpecon","NomeAutenticacao#NomeAutenticacao","NomeUsuario#NomeUsuario","UidUsuario#UidUsuario");
		
	        
             Linx.Framework.BV.Ambiente.AmbienteDomainService serviceContext1 = new Linx.Framework.BV.Ambiente.AmbienteDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsAmbienteUsuarioAcesso> result = 
	            (
                 from TcsAmbienteUsuarioAcesso_Rep1 in serviceContext1.GetTcsAmbienteUsuarioAcessoByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsAmbienteUsuarioAcesso()		
	            {
	            
                IdTcsAmbiente = TcsAmbienteUsuarioAcesso_Rep1.IdTcsAmbiente
                , IdTcsUsuarioAcesso = TcsAmbienteUsuarioAcesso_Rep1.IdTcsUsuarioAcesso
                , IdUsuario = TcsAmbienteUsuarioAcesso_Rep1.IdUsuario
                , IndicaAdministrador = TcsAmbienteUsuarioAcesso_Rep1.IndicaAdministrador
                , IndicaMultiGpecon = TcsAmbienteUsuarioAcesso_Rep1.IndicaMultiGpecon
                , NomeAutenticacao = TcsAmbienteUsuarioAcesso_Rep1.NomeAutenticacao
                , NomeUsuario = TcsAmbienteUsuarioAcesso_Rep1.NomeUsuario
                , UidUsuario = TcsAmbienteUsuarioAcesso_Rep1.UidUsuario
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsModuloGrupoByEntitySearch.
	    public IQueryable<TcsModuloGrupo> GetTcsModuloGrupoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsModuloGrupo", "TcsModuloGrupo", 0, "DescGrupoModulo#DescGrupoModulo","IdGrupoModulo#IdGrupoModulo","IdTcsAplicativo#IdTcsAplicativo","DescricaoAplicativo#DescricaoAplicativo");
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsModuloGrupoDetalhe", "TcsModuloDoGrupoDetalhe", 0, "IdGrupoModulo#IdGrupoModulo","IdModulo#IdModulo","IdModuloDoGrupo#IdModuloDoGrupo");
		
	        
             Linx.Framework.BV.Modulo.ModuloDomainService serviceContext3 = new Linx.Framework.BV.Modulo.ModuloDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsModuloGrupo> result = 
	            (
                 from TcsModuloGrupo_Rep1 in serviceContext3.GetTcsModuloGrupoByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsModuloGrupo()		
	            {
	            
                DescGrupoModulo = TcsModuloGrupo_Rep1.DescGrupoModulo
                , IdGrupoModulo = TcsModuloGrupo_Rep1.IdGrupoModulo
                , IdTcsAplicativo = TcsModuloGrupo_Rep1.IdTcsAplicativo
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsModuloGrupoDetalheByEntitySearch.
	    public IQueryable<TcsModuloGrupoDetalhe> GetTcsModuloGrupoDetalheByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsModuloGrupoDetalhe", "TcsModuloDoGrupoDetalhe", 0, "IdGrupoModulo#IdGrupoModulo","IdModulo#IdModulo","IdModuloDoGrupo#IdModuloDoGrupo");
		
	        
             Linx.Framework.BV.Modulo.ModuloDomainService serviceContext3 = new Linx.Framework.BV.Modulo.ModuloDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsModuloGrupoDetalhe> result = 
	            (
                 from TcsModuloDoGrupoDetalhe_Rep1 in serviceContext3.GetTcsModuloDoGrupoDetalheByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsModuloGrupoDetalhe()		
	            {
	            
                IdGrupoModulo = TcsModuloDoGrupoDetalhe_Rep1.IdGrupoModulo
                , IdModulo = TcsModuloDoGrupoDetalhe_Rep1.IdModulo
                , IdModuloDoGrupo = TcsModuloDoGrupoDetalhe_Rep1.IdModuloDoGrupo
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsModuloGrupoByEntitySearchNoAssociations.
	    public IQueryable<TcsModuloGrupo> GetTcsModuloGrupoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsModuloGrupo", "TcsModuloGrupo", 0, "DescGrupoModulo#DescGrupoModulo","IdGrupoModulo#IdGrupoModulo","IdTcsAplicativo#IdTcsAplicativo","DescricaoAplicativo#DescricaoAplicativo");
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsModuloGrupoDetalhe", "TcsModuloDoGrupoDetalhe", 0, "IdGrupoModulo#IdGrupoModulo","IdModulo#IdModulo","IdModuloDoGrupo#IdModuloDoGrupo");
		
	        
             Linx.Framework.BV.Modulo.ModuloDomainService serviceContext3 = new Linx.Framework.BV.Modulo.ModuloDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsModuloGrupo> result = 
	            (
                 from TcsModuloGrupo_Rep1 in serviceContext3.GetTcsModuloGrupoByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsModuloGrupo()		
	            {
	            
                DescGrupoModulo = TcsModuloGrupo_Rep1.DescGrupoModulo
                , IdGrupoModulo = TcsModuloGrupo_Rep1.IdGrupoModulo
                , IdTcsAplicativo = TcsModuloGrupo_Rep1.IdTcsAplicativo
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsModuloGrupoDetalheByEntitySearchNoAssociations.
	    public IQueryable<TcsModuloGrupoDetalhe> GetTcsModuloGrupoDetalheByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsModuloGrupoDetalhe", "TcsModuloDoGrupoDetalhe", 0, "IdGrupoModulo#IdGrupoModulo","IdModulo#IdModulo","IdModuloDoGrupo#IdModuloDoGrupo");
		
	        
             Linx.Framework.BV.Modulo.ModuloDomainService serviceContext3 = new Linx.Framework.BV.Modulo.ModuloDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsModuloGrupoDetalhe> result = 
	            (
                 from TcsModuloDoGrupoDetalhe_Rep1 in serviceContext3.GetTcsModuloDoGrupoDetalheByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsModuloGrupoDetalhe()		
	            {
	            
                IdGrupoModulo = TcsModuloDoGrupoDetalhe_Rep1.IdGrupoModulo
                , IdModulo = TcsModuloDoGrupoDetalhe_Rep1.IdModulo
                , IdModuloDoGrupo = TcsModuloDoGrupoDetalhe_Rep1.IdModuloDoGrupo
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsParametroValorByEntitySearch.
	    public IQueryable<TcsParametroValor> GetTcsParametroValorByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsParametroValor", "TcsParametroValorP", 0, "IdParametro#IdParametro","IdParametroValor#IdParametroValor","ValorParametro#ValorParametro","LxDatatypeParametro#LxDatatypeParametro","PossuiVariacao#PossuiVariacao","ValorParametroBool#ValorParametroBool","ValorParametroData#ValorParametroData");
		
	        
             Linx.Framework.BV.Parametro.ParametroDomainService serviceContext4 = new Linx.Framework.BV.Parametro.ParametroDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsParametroValor> result = 
	            (
                 from TcsParametroValorP_Rep1 in serviceContext4.GetTcsParametroValorPByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsParametroValor()		
	            {
	            
                IdParametro = TcsParametroValorP_Rep1.IdParametro
                , IdParametroValor = TcsParametroValorP_Rep1.IdParametroValor
                , ValorParametro = TcsParametroValorP_Rep1.ValorParametro
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsParametroValorByEntitySearchNoAssociations.
	    public IQueryable<TcsParametroValor> GetTcsParametroValorByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsParametroValor", "TcsParametroValorP", 0, "IdParametro#IdParametro","IdParametroValor#IdParametroValor","ValorParametro#ValorParametro","LxDatatypeParametro#LxDatatypeParametro","PossuiVariacao#PossuiVariacao","ValorParametroBool#ValorParametroBool","ValorParametroData#ValorParametroData");
		
	        
             Linx.Framework.BV.Parametro.ParametroDomainService serviceContext4 = new Linx.Framework.BV.Parametro.ParametroDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsParametroValor> result = 
	            (
                 from TcsParametroValorP_Rep1 in serviceContext4.GetTcsParametroValorPByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsParametroValor()		
	            {
	            
                IdParametro = TcsParametroValorP_Rep1.IdParametro
                , IdParametroValor = TcsParametroValorP_Rep1.IdParametroValor
                , ValorParametro = TcsParametroValorP_Rep1.ValorParametro
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsPerfilByEntitySearch.
	    public IQueryable<TcsPerfil> GetTcsPerfilByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsPerfil", "TcsPerfil", 0, "DescPerfil#DescPerfil","IdPerfil#IdPerfil","Inativo#Inativo","IndicaPerfilLinx#IndicaPerfilLinx","PerfilAutenticacao#PerfilAutenticacao");
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsPerfilRegraModulo", "TcsPerfilRegraModulo", 0, "IdModulo#IdModulo","IdPerfil#IdPerfil","IdPerfilRegraModulo#IdPerfilRegraModulo","LxRegraAcessoModulo#LxRegraAcessoModulo");
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsPerfilUsuario", "TcsUsuarioPerfil", 0, "IdPerfil#IdPerfil","IdTcsUsuarioPerfil#IdTcsUsuarioPerfil","IdUsuario#IdUsuario");
		
	        
             Linx.Framework.BV.Perfil.PerfilDomainService serviceContext6 = new Linx.Framework.BV.Perfil.PerfilDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsPerfil> result = 
	            (
                 from TcsPerfil_Rep1 in serviceContext6.GetTcsPerfilByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsPerfil()		
	            {
	            
                DescPerfil = TcsPerfil_Rep1.DescPerfil
                , IdPerfil = TcsPerfil_Rep1.IdPerfil
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsPerfilRegraModuloByEntitySearch.
	    public IQueryable<TcsPerfilRegraModulo> GetTcsPerfilRegraModuloByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsPerfilRegraModulo", "TcsPerfilRegraModulo", 0, "IdModulo#IdModulo","IdPerfil#IdPerfil","IdPerfilRegraModulo#IdPerfilRegraModulo","LxRegraAcessoModulo#LxRegraAcessoModulo");
		
	        
             Linx.Framework.BV.Perfil.PerfilDomainService serviceContext6 = new Linx.Framework.BV.Perfil.PerfilDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsPerfilRegraModulo> result = 
	            (
                 from TcsPerfilRegraModulo_Rep1 in serviceContext6.GetTcsPerfilRegraModuloByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsPerfilRegraModulo()		
	            {
	            
                IdModulo = TcsPerfilRegraModulo_Rep1.IdModulo
                , IdPerfil = TcsPerfilRegraModulo_Rep1.IdPerfil
                , IdPerfilRegraModulo = TcsPerfilRegraModulo_Rep1.IdPerfilRegraModulo
                , LxRegraAcessoModulo = TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo
                , LxRegraAcessoModuloName = ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 1 ? "Acesso Bloqueado" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 2 ? "Acesso Total" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 5 ? "Alterar" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 12 ? "Criar Pesquisa" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 10 ? "Criar Relatório" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 6 ? "Excluir" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 9 ? "Exportar" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 8 ? "Imprimir" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 4 ? "Incluir" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 11 ? "Layout" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 7 ? "Pesquisa Especial" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 3 ? "Pesquisar" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 99 ? "Regra Transação" : "")))))))))))))
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsPerfilUsuarioByEntitySearch.
	    public IQueryable<TcsPerfilUsuario> GetTcsPerfilUsuarioByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsPerfilUsuario", "TcsUsuarioPerfil", 0, "IdPerfil#IdPerfil","IdTcsUsuarioPerfil#IdTcsUsuarioPerfil","IdUsuario#IdUsuario");
		
	        
             Linx.Framework.BV.Perfil.PerfilDomainService serviceContext6 = new Linx.Framework.BV.Perfil.PerfilDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsPerfilUsuario> result = 
	            (
                 from TcsUsuarioPerfil_Rep2 in serviceContext6.GetTcsUsuarioPerfilByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsPerfilUsuario()		
	            {
	            
                IdPerfil = TcsUsuarioPerfil_Rep2.IdPerfil
                , IdTcsUsuarioPerfil = TcsUsuarioPerfil_Rep2.IdTcsUsuarioPerfil
                , IdUsuario = TcsUsuarioPerfil_Rep2.IdUsuario
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsPerfilByEntitySearchNoAssociations.
	    public IQueryable<TcsPerfil> GetTcsPerfilByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsPerfil", "TcsPerfil", 0, "DescPerfil#DescPerfil","IdPerfil#IdPerfil","Inativo#Inativo","IndicaPerfilLinx#IndicaPerfilLinx","PerfilAutenticacao#PerfilAutenticacao");
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsPerfilRegraModulo", "TcsPerfilRegraModulo", 0, "IdModulo#IdModulo","IdPerfil#IdPerfil","IdPerfilRegraModulo#IdPerfilRegraModulo","LxRegraAcessoModulo#LxRegraAcessoModulo");
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsPerfilUsuario", "TcsUsuarioPerfil", 0, "IdPerfil#IdPerfil","IdTcsUsuarioPerfil#IdTcsUsuarioPerfil","IdUsuario#IdUsuario");
		
	        
             Linx.Framework.BV.Perfil.PerfilDomainService serviceContext6 = new Linx.Framework.BV.Perfil.PerfilDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsPerfil> result = 
	            (
                 from TcsPerfil_Rep1 in serviceContext6.GetTcsPerfilByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsPerfil()		
	            {
	            
                DescPerfil = TcsPerfil_Rep1.DescPerfil
                , IdPerfil = TcsPerfil_Rep1.IdPerfil
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsPerfilRegraModuloByEntitySearchNoAssociations.
	    public IQueryable<TcsPerfilRegraModulo> GetTcsPerfilRegraModuloByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsPerfilRegraModulo", "TcsPerfilRegraModulo", 0, "IdModulo#IdModulo","IdPerfil#IdPerfil","IdPerfilRegraModulo#IdPerfilRegraModulo","LxRegraAcessoModulo#LxRegraAcessoModulo");
		
	        
             Linx.Framework.BV.Perfil.PerfilDomainService serviceContext6 = new Linx.Framework.BV.Perfil.PerfilDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsPerfilRegraModulo> result = 
	            (
                 from TcsPerfilRegraModulo_Rep1 in serviceContext6.GetTcsPerfilRegraModuloByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsPerfilRegraModulo()		
	            {
	            
                IdModulo = TcsPerfilRegraModulo_Rep1.IdModulo
                , IdPerfil = TcsPerfilRegraModulo_Rep1.IdPerfil
                , IdPerfilRegraModulo = TcsPerfilRegraModulo_Rep1.IdPerfilRegraModulo
                , LxRegraAcessoModulo = TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo
                , LxRegraAcessoModuloName = ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 1 ? "Acesso Bloqueado" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 2 ? "Acesso Total" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 5 ? "Alterar" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 12 ? "Criar Pesquisa" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 10 ? "Criar Relatório" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 6 ? "Excluir" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 9 ? "Exportar" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 8 ? "Imprimir" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 4 ? "Incluir" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 11 ? "Layout" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 7 ? "Pesquisa Especial" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 3 ? "Pesquisar" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 99 ? "Regra Transação" : "")))))))))))))
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsPerfilUsuarioByEntitySearchNoAssociations.
	    public IQueryable<TcsPerfilUsuario> GetTcsPerfilUsuarioByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsPerfilUsuario", "TcsUsuarioPerfil", 0, "IdPerfil#IdPerfil","IdTcsUsuarioPerfil#IdTcsUsuarioPerfil","IdUsuario#IdUsuario");
		
	        
             Linx.Framework.BV.Perfil.PerfilDomainService serviceContext6 = new Linx.Framework.BV.Perfil.PerfilDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsPerfilUsuario> result = 
	            (
                 from TcsUsuarioPerfil_Rep2 in serviceContext6.GetTcsUsuarioPerfilByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsPerfilUsuario()		
	            {
	            
                IdPerfil = TcsUsuarioPerfil_Rep2.IdPerfil
                , IdTcsUsuarioPerfil = TcsUsuarioPerfil_Rep2.IdTcsUsuarioPerfil
                , IdUsuario = TcsUsuarioPerfil_Rep2.IdUsuario
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get AmbienteInfoByEntitySearch.
	    public IEnumerable<AmbienteInfo> GetAmbienteInfoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<AmbienteInfo> result = new List<AmbienteInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get AmbienteInfoByEntitySearchNoAssociations.
	    public IEnumerable<AmbienteInfo> GetAmbienteInfoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<AmbienteInfo> result = new List<AmbienteInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsEmpresaGpeconByEntitySearch.
	    public IQueryable<TcsEmpresaGpecon> GetTcsEmpresaGpeconByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsEmpresaGpecon", "TcsEmpresaGpeconP", 0, "IdLinx#IdLinx","IdLinxGpecon#IdLinxGpecon");
		
	        
             Linx.Framework.BV.Empresa.EmpresaDomainService serviceContext2 = new Linx.Framework.BV.Empresa.EmpresaDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsEmpresaGpecon> result = 
	            (
                 from TcsEmpresaGpeconP_Rep1 in serviceContext2.GetTcsEmpresaGpeconPByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsEmpresaGpecon()		
	            {
	            
                IdLinx = TcsEmpresaGpeconP_Rep1.IdLinx
                , IdLinxGpecon = TcsEmpresaGpeconP_Rep1.IdLinxGpecon
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsEmpresaGpeconByEntitySearchNoAssociations.
	    public IQueryable<TcsEmpresaGpecon> GetTcsEmpresaGpeconByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsEmpresaGpecon", "TcsEmpresaGpeconP", 0, "IdLinx#IdLinx","IdLinxGpecon#IdLinxGpecon");
		
	        
             Linx.Framework.BV.Empresa.EmpresaDomainService serviceContext2 = new Linx.Framework.BV.Empresa.EmpresaDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsEmpresaGpecon> result = 
	            (
                 from TcsEmpresaGpeconP_Rep1 in serviceContext2.GetTcsEmpresaGpeconPByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsEmpresaGpecon()		
	            {
	            
                IdLinx = TcsEmpresaGpeconP_Rep1.IdLinx
                , IdLinxGpecon = TcsEmpresaGpeconP_Rep1.IdLinxGpecon
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteInfoByEntitySearch.
	    public IQueryable<TcsAmbienteInfo> GetTcsAmbienteInfoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAmbienteInfo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteInfo> result = 
	            (from entity0 in this.DbContext.TCS_AMBIENTE.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_APLICACAO
                  let entity0Al2 = entity0.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsAmbienteInfo()		
	            {
	            
                IdAplicacao = entity0Al1.ID_APLICACAO
                , IdLinx = entity0Al2.ID_LINX
                , IdTcsAmbiente = entity0.ID_TCS_AMBIENTE
                , UidAplicacao = entity0Al1.UID_APLICACAO
                , UidEmpresa = entity0Al2.UID_EMPRESA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsAmbienteInfoByEntitySearchNoAssociations.
	    public IQueryable<TcsAmbienteInfo> GetTcsAmbienteInfoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAmbienteInfo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteInfo> result = 
	            (from entity0 in this.DbContext.TCS_AMBIENTE.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_APLICACAO
                  let entity0Al2 = entity0.TCS_EMPRESA_AUTENTICACAO
	            
	            	
	            select new TcsAmbienteInfo()		
	            {
	            
                IdAplicacao = entity0Al1.ID_APLICACAO
                , IdLinx = entity0Al2.ID_LINX
                , IdTcsAmbiente = entity0.ID_TCS_AMBIENTE
                , UidAplicacao = entity0Al1.UID_APLICACAO
                , UidEmpresa = entity0Al2.UID_EMPRESA
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsParametroAutorizacaoByEntitySearch.
	    public IQueryable<TcsParametroAutorizacao> GetTcsParametroAutorizacaoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsParametroAutorizacao", "TcsParametroAutorizacao", 0, "IdParametro#IdParametro","IdTcsAplicativo#IdTcsAplicativo","TituloParametro#TituloParametro","ColunaCodValida#ColunaCodValida","ColunaDescValida#ColunaDescValida","DescGrupoParametro#DescGrupoParametro","DescParametro#DescParametro","DescricaoAplicativo#DescricaoAplicativo","DescTabela#DescTabela","FaixaFinal#FaixaFinal","FaixaInicial#FaixaInicial","IdGrupoParametro#IdGrupoParametro","IndicaEnviaPdv#IndicaEnviaPdv","IndicaParametroLinx#IndicaParametroLinx","LxDatatypeParametro#LxDatatypeParametro","LxTipoValidacaoParametro#LxTipoValidacaoParametro","NivelAcesso#NivelAcesso","NivelAcessoEdicao#NivelAcessoEdicao","ObsParametro#ObsParametro","PermiteVariacaoPorEntidade#PermiteVariacaoPorEntidade","UidTabela#UidTabela");
		
	        
             Linx.Framework.BV.ParametroAutorizacao.ParametroAutorizacaoDomainService serviceContext5 = new Linx.Framework.BV.ParametroAutorizacao.ParametroAutorizacaoDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsParametroAutorizacao> result = 
	            (
                 from TcsParametroAutorizacao_Rep1 in serviceContext5.GetTcsParametroAutorizacaoByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsParametroAutorizacao()		
	            {
	            
                IdParametro = TcsParametroAutorizacao_Rep1.IdParametro
                , IdTcsAplicativo = TcsParametroAutorizacao_Rep1.IdTcsAplicativo
                , TituloParametro = TcsParametroAutorizacao_Rep1.TituloParametro
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TcsParametroAutorizacaoByEntitySearchNoAssociations.
	    public IQueryable<TcsParametroAutorizacao> GetTcsParametroAutorizacaoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsParametroAutorizacao", "TcsParametroAutorizacao", 0, "IdParametro#IdParametro","IdTcsAplicativo#IdTcsAplicativo","TituloParametro#TituloParametro","ColunaCodValida#ColunaCodValida","ColunaDescValida#ColunaDescValida","DescGrupoParametro#DescGrupoParametro","DescParametro#DescParametro","DescricaoAplicativo#DescricaoAplicativo","DescTabela#DescTabela","FaixaFinal#FaixaFinal","FaixaInicial#FaixaInicial","IdGrupoParametro#IdGrupoParametro","IndicaEnviaPdv#IndicaEnviaPdv","IndicaParametroLinx#IndicaParametroLinx","LxDatatypeParametro#LxDatatypeParametro","LxTipoValidacaoParametro#LxTipoValidacaoParametro","NivelAcesso#NivelAcesso","NivelAcessoEdicao#NivelAcessoEdicao","ObsParametro#ObsParametro","PermiteVariacaoPorEntidade#PermiteVariacaoPorEntidade","UidTabela#UidTabela");
		
	        
             Linx.Framework.BV.ParametroAutorizacao.ParametroAutorizacaoDomainService serviceContext5 = new Linx.Framework.BV.ParametroAutorizacao.ParametroAutorizacaoDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsParametroAutorizacao> result = 
	            (
                 from TcsParametroAutorizacao_Rep1 in serviceContext5.GetTcsParametroAutorizacaoByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TcsParametroAutorizacao()		
	            {
	            
                IdParametro = TcsParametroAutorizacao_Rep1.IdParametro
                , IdTcsAplicativo = TcsParametroAutorizacao_Rep1.IdTcsAplicativo
                , TituloParametro = TcsParametroAutorizacao_Rep1.TituloParametro
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get MultimarcaInfoByEntitySearch.
	    public IEnumerable<MultimarcaInfo> GetMultimarcaInfoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<MultimarcaInfo> result = new List<MultimarcaInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get MultimarcaInfoByEntitySearchNoAssociations.
	    public IEnumerable<MultimarcaInfo> GetMultimarcaInfoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<MultimarcaInfo> result = new List<MultimarcaInfo>();
	  	
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TbcFilialByEntitySearch.
	    public IQueryable<TbcFilial> GetTbcFilialByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TbcFilial", "TbcFilial", 0, "Bairro#Bairro","BandeiraRede#BandeiraRede","Cep#Cep","CnpjCpf#CnpjCpf","CodDeposito#CodDeposito","CodigoFilial#CodigoFilial","CodigoPfj#CodigoPfj","Complemento#Complemento","DddCelular#DddCelular","DddFixo#DddFixo","Email#Email","FoneCelular#FoneCelular","FoneFixo#FoneFixo","IdFilialPfj#IdFilialPfj","IdGpecon#IdGpecon","IdLjvCanalVenda#IdLjvCanalVenda","IdMatrizContabil#IdMatrizContabil","IdPfj#IdPfj","IncluiDeposito#IncluiDeposito","IncluiLoja#IncluiLoja","IndicaEstrangeiro#IndicaEstrangeiro","IndicaFilial#IndicaFilial","IndicaLoja#IndicaLoja","IndicaMatrizContabil#IndicaMatrizContabil","InscrEstadual#InscrEstadual","Logradouro#Logradouro","LxPfjFisicaJuridica#LxPfjFisicaJuridica","LxTipoLogradouro#LxTipoLogradouro","Municipio#Municipio","NomeFantasiaApelido#NomeFantasiaApelido","NomeFilial#NomeFilial","Numero#Numero","ObsEndereco#ObsEndereco","Pais#Pais","RazaoSocialNomeCompleto#RazaoSocialNomeCompleto","Uf#Uf","CodAgrupadorRegraFilial#CodAgrupadorRegraFilial","CodBandeiraRede#CodBandeiraRede","CodCanalVenda#CodCanalVenda","CodigoMatrizContabil#CodigoMatrizContabil","CodRegiaoComercial#CodRegiaoComercial","CtrlStkLocalizacao#CtrlStkLocalizacao","CtrlStkLote#CtrlStkLote","DescAgrupadorRegraFilial#DescAgrupadorRegraFilial","DescBandeiraRede#DescBandeiraRede","DescCanalVenda#DescCanalVenda","DescGrupoEconomico#DescGrupoEconomico","DescRegiaoComercial#DescRegiaoComercial","IdAgrupadorRegraFilial#IdAgrupadorRegraFilial","IdRegiaoComercial#IdRegiaoComercial","Inativo#Inativo","IndicaEcommerce#IndicaEcommerce","IndicaFranquia#IndicaFranquia","IndicaHubfiscal#IndicaHubfiscal","IndicaHubfiscalAtualizado#IndicaHubfiscalAtualizado","NomeMatrizContabil#NomeMatrizContabil");
		
	        
             Linx.CadastroBase.BV.CadastroPfj.CadastroPfjDomainService serviceContext0 = new Linx.CadastroBase.BV.CadastroPfj.CadastroPfjDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TbcFilial> result = 
	            (
                 from TbcFilial_Rep2 in serviceContext0.GetTbcFilialByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TbcFilial()		
	            {
	            
                Bairro = TbcFilial_Rep2.Bairro
                , BandeiraRede = TbcFilial_Rep2.BandeiraRede
                , Cep = TbcFilial_Rep2.Cep
                , CnpjCpf = TbcFilial_Rep2.CnpjCpf
                , CodDeposito = TbcFilial_Rep2.CodDeposito
                , CodigoFilial = TbcFilial_Rep2.CodigoFilial
                , CodigoPfj = TbcFilial_Rep2.CodigoPfj
                , Complemento = TbcFilial_Rep2.Complemento
                , DddCelular = TbcFilial_Rep2.DddCelular
                , DddFixo = TbcFilial_Rep2.DddFixo
                , Email = TbcFilial_Rep2.Email
                , FoneCelular = TbcFilial_Rep2.FoneCelular
                , FoneFixo = TbcFilial_Rep2.FoneFixo
                , IdFilialPfj = TbcFilial_Rep2.IdFilialPfj
                , IdGpecon = TbcFilial_Rep2.IdGpecon
                , IdLjvCanalVenda = TbcFilial_Rep2.IdLjvCanalVenda
                , IdMatrizContabil = TbcFilial_Rep2.IdMatrizContabil
                , IdPfj = TbcFilial_Rep2.IdPfj
                , IncluiDeposito = TbcFilial_Rep2.IncluiDeposito
                , IncluiLoja = TbcFilial_Rep2.IncluiLoja
                , IndicaEstrangeiro = TbcFilial_Rep2.IndicaEstrangeiro
                , IndicaFilial = TbcFilial_Rep2.IndicaFilial
                , IndicaLoja = TbcFilial_Rep2.IndicaLoja
                , IndicaMatrizContabil = TbcFilial_Rep2.IndicaMatrizContabil
                , InscrEstadual = TbcFilial_Rep2.InscrEstadual
                , Logradouro = TbcFilial_Rep2.Logradouro
                , LxPfjFisicaJuridica = TbcFilial_Rep2.LxPfjFisicaJuridica
                , LxPfjFisicaJuridicaName = ((TbcFilial_Rep2.LxPfjFisicaJuridica) == 1 ? "Pessoa Física" : ((TbcFilial_Rep2.LxPfjFisicaJuridica) == 2 ? "Pessoa Jurídica" : ""))
                , LxTipoLogradouro = TbcFilial_Rep2.LxTipoLogradouro
                , LxTipoLogradouroName = ((TbcFilial_Rep2.LxTipoLogradouro) == 1 ? "Aeroporto" : ((TbcFilial_Rep2.LxTipoLogradouro) == 2 ? "Alameda" : ((TbcFilial_Rep2.LxTipoLogradouro) == 3 ? "Apartamento" : ((TbcFilial_Rep2.LxTipoLogradouro) == 4 ? "Avenida" : ((TbcFilial_Rep2.LxTipoLogradouro) == 5 ? "Beco" : ((TbcFilial_Rep2.LxTipoLogradouro) == 6 ? "Bloco" : ((TbcFilial_Rep2.LxTipoLogradouro) == 7 ? "Caminho" : ((TbcFilial_Rep2.LxTipoLogradouro) == 8 ? "Escadinha" : ((TbcFilial_Rep2.LxTipoLogradouro) == 9 ? "Estação" : ((TbcFilial_Rep2.LxTipoLogradouro) == 10 ? "Estrada" : ((TbcFilial_Rep2.LxTipoLogradouro) == 11 ? "Fazenda" : ((TbcFilial_Rep2.LxTipoLogradouro) == 12 ? "Fortaleza" : ((TbcFilial_Rep2.LxTipoLogradouro) == 13 ? "Galeria" : ((TbcFilial_Rep2.LxTipoLogradouro) == 14 ? "Ladeira" : ((TbcFilial_Rep2.LxTipoLogradouro) == 15 ? "Largo" : ((TbcFilial_Rep2.LxTipoLogradouro) == 17 ? "Parque" : ((TbcFilial_Rep2.LxTipoLogradouro) == 16 ? "Praça" : ((TbcFilial_Rep2.LxTipoLogradouro) == 18 ? "Praia" : ((TbcFilial_Rep2.LxTipoLogradouro) == 19 ? "Quadra" : ((TbcFilial_Rep2.LxTipoLogradouro) == 20 ? "Quilômetro" : ((TbcFilial_Rep2.LxTipoLogradouro) == 21 ? "Quinta" : ((TbcFilial_Rep2.LxTipoLogradouro) == 22 ? "Rodovia" : ((TbcFilial_Rep2.LxTipoLogradouro) == 23 ? "Rua" : ((TbcFilial_Rep2.LxTipoLogradouro) == 24 ? "Super Quadra" : ((TbcFilial_Rep2.LxTipoLogradouro) == 25 ? "Travessa" : ((TbcFilial_Rep2.LxTipoLogradouro) == 26 ? "Viaduto" : ((TbcFilial_Rep2.LxTipoLogradouro) == 27 ? "Vila" : "")))))))))))))))))))))))))))
                , Municipio = TbcFilial_Rep2.Municipio
                , NomeFantasiaApelido = TbcFilial_Rep2.NomeFantasiaApelido
                , NomeFilial = TbcFilial_Rep2.NomeFilial
                , Numero = TbcFilial_Rep2.Numero
                , ObsEndereco = TbcFilial_Rep2.ObsEndereco
                , Pais = TbcFilial_Rep2.Pais
                , RazaoSocialNomeCompleto = TbcFilial_Rep2.RazaoSocialNomeCompleto
                , Uf = TbcFilial_Rep2.Uf
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TbcFilialByEntitySearchNoAssociations.
	    public IQueryable<TbcFilial> GetTbcFilialByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TbcFilial", "TbcFilial", 0, "Bairro#Bairro","BandeiraRede#BandeiraRede","Cep#Cep","CnpjCpf#CnpjCpf","CodDeposito#CodDeposito","CodigoFilial#CodigoFilial","CodigoPfj#CodigoPfj","Complemento#Complemento","DddCelular#DddCelular","DddFixo#DddFixo","Email#Email","FoneCelular#FoneCelular","FoneFixo#FoneFixo","IdFilialPfj#IdFilialPfj","IdGpecon#IdGpecon","IdLjvCanalVenda#IdLjvCanalVenda","IdMatrizContabil#IdMatrizContabil","IdPfj#IdPfj","IncluiDeposito#IncluiDeposito","IncluiLoja#IncluiLoja","IndicaEstrangeiro#IndicaEstrangeiro","IndicaFilial#IndicaFilial","IndicaLoja#IndicaLoja","IndicaMatrizContabil#IndicaMatrizContabil","InscrEstadual#InscrEstadual","Logradouro#Logradouro","LxPfjFisicaJuridica#LxPfjFisicaJuridica","LxTipoLogradouro#LxTipoLogradouro","Municipio#Municipio","NomeFantasiaApelido#NomeFantasiaApelido","NomeFilial#NomeFilial","Numero#Numero","ObsEndereco#ObsEndereco","Pais#Pais","RazaoSocialNomeCompleto#RazaoSocialNomeCompleto","Uf#Uf","CodAgrupadorRegraFilial#CodAgrupadorRegraFilial","CodBandeiraRede#CodBandeiraRede","CodCanalVenda#CodCanalVenda","CodigoMatrizContabil#CodigoMatrizContabil","CodRegiaoComercial#CodRegiaoComercial","CtrlStkLocalizacao#CtrlStkLocalizacao","CtrlStkLote#CtrlStkLote","DescAgrupadorRegraFilial#DescAgrupadorRegraFilial","DescBandeiraRede#DescBandeiraRede","DescCanalVenda#DescCanalVenda","DescGrupoEconomico#DescGrupoEconomico","DescRegiaoComercial#DescRegiaoComercial","IdAgrupadorRegraFilial#IdAgrupadorRegraFilial","IdRegiaoComercial#IdRegiaoComercial","Inativo#Inativo","IndicaEcommerce#IndicaEcommerce","IndicaFranquia#IndicaFranquia","IndicaHubfiscal#IndicaHubfiscal","IndicaHubfiscalAtualizado#IndicaHubfiscalAtualizado","NomeMatrizContabil#NomeMatrizContabil");
		
	        
             Linx.CadastroBase.BV.CadastroPfj.CadastroPfjDomainService serviceContext0 = new Linx.CadastroBase.BV.CadastroPfj.CadastroPfjDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TbcFilial> result = 
	            (
                 from TbcFilial_Rep2 in serviceContext0.GetTbcFilialByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TbcFilial()		
	            {
	            
                Bairro = TbcFilial_Rep2.Bairro
                , BandeiraRede = TbcFilial_Rep2.BandeiraRede
                , Cep = TbcFilial_Rep2.Cep
                , CnpjCpf = TbcFilial_Rep2.CnpjCpf
                , CodDeposito = TbcFilial_Rep2.CodDeposito
                , CodigoFilial = TbcFilial_Rep2.CodigoFilial
                , CodigoPfj = TbcFilial_Rep2.CodigoPfj
                , Complemento = TbcFilial_Rep2.Complemento
                , DddCelular = TbcFilial_Rep2.DddCelular
                , DddFixo = TbcFilial_Rep2.DddFixo
                , Email = TbcFilial_Rep2.Email
                , FoneCelular = TbcFilial_Rep2.FoneCelular
                , FoneFixo = TbcFilial_Rep2.FoneFixo
                , IdFilialPfj = TbcFilial_Rep2.IdFilialPfj
                , IdGpecon = TbcFilial_Rep2.IdGpecon
                , IdLjvCanalVenda = TbcFilial_Rep2.IdLjvCanalVenda
                , IdMatrizContabil = TbcFilial_Rep2.IdMatrizContabil
                , IdPfj = TbcFilial_Rep2.IdPfj
                , IncluiDeposito = TbcFilial_Rep2.IncluiDeposito
                , IncluiLoja = TbcFilial_Rep2.IncluiLoja
                , IndicaEstrangeiro = TbcFilial_Rep2.IndicaEstrangeiro
                , IndicaFilial = TbcFilial_Rep2.IndicaFilial
                , IndicaLoja = TbcFilial_Rep2.IndicaLoja
                , IndicaMatrizContabil = TbcFilial_Rep2.IndicaMatrizContabil
                , InscrEstadual = TbcFilial_Rep2.InscrEstadual
                , Logradouro = TbcFilial_Rep2.Logradouro
                , LxPfjFisicaJuridica = TbcFilial_Rep2.LxPfjFisicaJuridica
                , LxPfjFisicaJuridicaName = ((TbcFilial_Rep2.LxPfjFisicaJuridica) == 1 ? "Pessoa Física" : ((TbcFilial_Rep2.LxPfjFisicaJuridica) == 2 ? "Pessoa Jurídica" : ""))
                , LxTipoLogradouro = TbcFilial_Rep2.LxTipoLogradouro
                , LxTipoLogradouroName = ((TbcFilial_Rep2.LxTipoLogradouro) == 1 ? "Aeroporto" : ((TbcFilial_Rep2.LxTipoLogradouro) == 2 ? "Alameda" : ((TbcFilial_Rep2.LxTipoLogradouro) == 3 ? "Apartamento" : ((TbcFilial_Rep2.LxTipoLogradouro) == 4 ? "Avenida" : ((TbcFilial_Rep2.LxTipoLogradouro) == 5 ? "Beco" : ((TbcFilial_Rep2.LxTipoLogradouro) == 6 ? "Bloco" : ((TbcFilial_Rep2.LxTipoLogradouro) == 7 ? "Caminho" : ((TbcFilial_Rep2.LxTipoLogradouro) == 8 ? "Escadinha" : ((TbcFilial_Rep2.LxTipoLogradouro) == 9 ? "Estação" : ((TbcFilial_Rep2.LxTipoLogradouro) == 10 ? "Estrada" : ((TbcFilial_Rep2.LxTipoLogradouro) == 11 ? "Fazenda" : ((TbcFilial_Rep2.LxTipoLogradouro) == 12 ? "Fortaleza" : ((TbcFilial_Rep2.LxTipoLogradouro) == 13 ? "Galeria" : ((TbcFilial_Rep2.LxTipoLogradouro) == 14 ? "Ladeira" : ((TbcFilial_Rep2.LxTipoLogradouro) == 15 ? "Largo" : ((TbcFilial_Rep2.LxTipoLogradouro) == 17 ? "Parque" : ((TbcFilial_Rep2.LxTipoLogradouro) == 16 ? "Praça" : ((TbcFilial_Rep2.LxTipoLogradouro) == 18 ? "Praia" : ((TbcFilial_Rep2.LxTipoLogradouro) == 19 ? "Quadra" : ((TbcFilial_Rep2.LxTipoLogradouro) == 20 ? "Quilômetro" : ((TbcFilial_Rep2.LxTipoLogradouro) == 21 ? "Quinta" : ((TbcFilial_Rep2.LxTipoLogradouro) == 22 ? "Rodovia" : ((TbcFilial_Rep2.LxTipoLogradouro) == 23 ? "Rua" : ((TbcFilial_Rep2.LxTipoLogradouro) == 24 ? "Super Quadra" : ((TbcFilial_Rep2.LxTipoLogradouro) == 25 ? "Travessa" : ((TbcFilial_Rep2.LxTipoLogradouro) == 26 ? "Viaduto" : ((TbcFilial_Rep2.LxTipoLogradouro) == 27 ? "Vila" : "")))))))))))))))))))))))))))
                , Municipio = TbcFilial_Rep2.Municipio
                , NomeFantasiaApelido = TbcFilial_Rep2.NomeFantasiaApelido
                , NomeFilial = TbcFilial_Rep2.NomeFilial
                , Numero = TbcFilial_Rep2.Numero
                , ObsEndereco = TbcFilial_Rep2.ObsEndereco
                , Pais = TbcFilial_Rep2.Pais
                , RazaoSocialNomeCompleto = TbcFilial_Rep2.RazaoSocialNomeCompleto
                , Uf = TbcFilial_Rep2.Uf
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TbcGrupoEconomicoByEntitySearch.
	    public IQueryable<TbcGrupoEconomico> GetTbcGrupoEconomicoByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TbcGrupoEconomico", "TbcGrupoEconomico", 0, "DescGrupoEconomico#DescGrupoEconomico","IdGpeconCadastro#IdGpeconCadastro","FatorCambio#FatorCambio","IndicaGpeconMaster#IndicaGpeconMaster","IndicaMoedaForte#IndicaMoedaForte");
		
	        
             Linx.Operacional.CadastroBase.BV.GrupoEconomico.GrupoEconomicoDomainService serviceContext11 = new Linx.Operacional.CadastroBase.BV.GrupoEconomico.GrupoEconomicoDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TbcGrupoEconomico> result = 
	            (
                 from TbcGrupoEconomico_Rep1 in serviceContext11.GetTbcGrupoEconomicoByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TbcGrupoEconomico()		
	            {
	            
                DescGrupoEconomico = TbcGrupoEconomico_Rep1.DescGrupoEconomico
                , IdGpeconCadastro = TbcGrupoEconomico_Rep1.IdGpeconCadastro
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TbcGrupoEconomicoByEntitySearchNoAssociations.
	    public IQueryable<TbcGrupoEconomico> GetTbcGrupoEconomicoByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TbcGrupoEconomico", "TbcGrupoEconomico", 0, "DescGrupoEconomico#DescGrupoEconomico","IdGpeconCadastro#IdGpeconCadastro","FatorCambio#FatorCambio","IndicaGpeconMaster#IndicaGpeconMaster","IndicaMoedaForte#IndicaMoedaForte");
		
	        
             Linx.Operacional.CadastroBase.BV.GrupoEconomico.GrupoEconomicoDomainService serviceContext11 = new Linx.Operacional.CadastroBase.BV.GrupoEconomico.GrupoEconomicoDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TbcGrupoEconomico> result = 
	            (
                 from TbcGrupoEconomico_Rep1 in serviceContext11.GetTbcGrupoEconomicoByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TbcGrupoEconomico()		
	            {
	            
                DescGrupoEconomico = TbcGrupoEconomico_Rep1.DescGrupoEconomico
                , IdGpeconCadastro = TbcGrupoEconomico_Rep1.IdGpeconCadastro
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TbcBandeiraRedeByEntitySearch.
	    public IQueryable<TbcBandeiraRede> GetTbcBandeiraRedeByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TbcBandeiraRede", "TbcBandeiraRede", 0, "CodBandeiraRede#CodBandeiraRede","IdBandeiraRedeCadastro#IdBandeiraRedeCadastro","IdLinx#IdLinx","DataAtualizacao#DataAtualizacao","DataCadastro#DataCadastro","DescBandeiraRede#DescBandeiraRede");
		
	        
             Linx.Operacional.CadastroBase.BV.BandeiraRede.BandeiraRedeDomainService serviceContext9 = new Linx.Operacional.CadastroBase.BV.BandeiraRede.BandeiraRedeDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TbcBandeiraRede> result = 
	            (
                 from TbcBandeiraRede_Rep1 in serviceContext9.GetTbcBandeiraRedeByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TbcBandeiraRede()		
	            {
	            
                CodBandeiraRede = TbcBandeiraRede_Rep1.CodBandeiraRede
                , IdBandeiraRedeCadastro = TbcBandeiraRede_Rep1.IdBandeiraRedeCadastro
                , IdLinx = TbcBandeiraRede_Rep1.IdLinx
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get TbcBandeiraRedeByEntitySearchNoAssociations.
	    public IQueryable<TbcBandeiraRede> GetTbcBandeiraRedeByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TbcBandeiraRede", "TbcBandeiraRede", 0, "CodBandeiraRede#CodBandeiraRede","IdBandeiraRedeCadastro#IdBandeiraRedeCadastro","IdLinx#IdLinx","DataAtualizacao#DataAtualizacao","DataCadastro#DataCadastro","DescBandeiraRede#DescBandeiraRede");
		
	        
             Linx.Operacional.CadastroBase.BV.BandeiraRede.BandeiraRedeDomainService serviceContext9 = new Linx.Operacional.CadastroBase.BV.BandeiraRede.BandeiraRedeDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TbcBandeiraRede> result = 
	            (
                 from TbcBandeiraRede_Rep1 in serviceContext9.GetTbcBandeiraRedeByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new TbcBandeiraRede()		
	            {
	            
                CodBandeiraRede = TbcBandeiraRede_Rep1.CodBandeiraRede
                , IdBandeiraRedeCadastro = TbcBandeiraRede_Rep1.IdBandeiraRedeCadastro
                , IdLinx = TbcBandeiraRede_Rep1.IdLinx
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get LjvCanalVendaByEntitySearch.
	    public IQueryable<LjvCanalVenda> GetLjvCanalVendaByEntitySearch(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"LjvCanalVenda", "LjvCanalVenda", 0, "CodCanalVenda#CodCanalVenda","DescCanalVenda#DescCanalVenda","IdLjvCanalVenda#IdLjvCanalVenda");
		
	        
             Linx.Operacional.CadastroBase.BV.CanalVenda.CanalVendaDomainService serviceContext10 = new Linx.Operacional.CadastroBase.BV.CanalVenda.CanalVendaDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<LjvCanalVenda> result = 
	            (
                 from LjvCanalVenda_Rep1 in serviceContext10.GetLjvCanalVendaByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new LjvCanalVenda()		
	            {
	            
                CodCanalVenda = LjvCanalVenda_Rep1.CodCanalVenda
                , DescCanalVenda = LjvCanalVenda_Rep1.DescCanalVenda
                , IdLjvCanalVenda = LjvCanalVenda_Rep1.IdLjvCanalVenda
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get LjvCanalVendaByEntitySearchNoAssociations.
	    public IQueryable<LjvCanalVenda> GetLjvCanalVendaByEntitySearchNoAssociations(string serializedEntitySearch, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"LjvCanalVenda", "LjvCanalVenda", 0, "CodCanalVenda#CodCanalVenda","DescCanalVenda#DescCanalVenda","IdLjvCanalVenda#IdLjvCanalVenda");
		
	        
             Linx.Operacional.CadastroBase.BV.CanalVenda.CanalVendaDomainService serviceContext10 = new Linx.Operacional.CadastroBase.BV.CanalVenda.CanalVendaDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<LjvCanalVenda> result = 
	            (
                 from LjvCanalVenda_Rep1 in serviceContext10.GetLjvCanalVendaByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            	
	            select new LjvCanalVenda()		
	            {
	            
                CodCanalVenda = LjvCanalVenda_Rep1.CodCanalVenda
                , DescCanalVenda = LjvCanalVenda_Rep1.DescCanalVenda
                , IdLjvCanalVenda = LjvCanalVenda_Rep1.IdLjvCanalVenda
		
	            }
	            );
		
	
	        	

	
	        return result;
	    }
		
	    #endregion Get By Entity Search Methods Definitions.
	

	    #region Paging Methods Definitions.
	
			
	
	    
	    [Ignore]
	    //Get PagedTcsEmpresaAutenticacao.
	    public IQueryable<TcsEmpresaAutenticacao> GetPagedTcsEmpresaAutenticacao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsEmpresaAutenticacao", "TcsEmpresaAutenticacao", 0, "CnpjCpf#CnpjCpf","IdLinx#IdLinx","NomeEmpresa#NomeEmpresa","UidEmpresa#UidEmpresa");
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsEmpresaAutenticacaoModulo", "TcsEmpresaModulo", 0, "IdLinx#IdLinx","IdModulo#IdModulo","IdTcsAplicativo#IdTcsAplicativo","IdTcsEmpresaModulo#IdTcsEmpresaModulo");
		
	        
             Linx.Framework.BV.Empresa.EmpresaDomainService serviceContext2 = new Linx.Framework.BV.Empresa.EmpresaDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsEmpresaAutenticacao> result = 
	            (
                 from TcsEmpresaAutenticacao_Rep1 in serviceContext2.GetTcsEmpresaAutenticacaoByEntitySearchNoAssociations(repSerializedEntitySearch)
                orderby TcsEmpresaAutenticacao_Rep1.IdLinx ascending
	            
	            	
	            select new TcsEmpresaAutenticacao()		
	            {
	            
                CnpjCpf = TcsEmpresaAutenticacao_Rep1.CnpjCpf
                , IdLinx = TcsEmpresaAutenticacao_Rep1.IdLinx
                , NomeEmpresa = TcsEmpresaAutenticacao_Rep1.NomeEmpresa
                , UidEmpresa = TcsEmpresaAutenticacao_Rep1.UidEmpresa
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsEmpresaAutenticacaoModulo.
	    public IQueryable<TcsEmpresaAutenticacaoModulo> GetPagedTcsEmpresaAutenticacaoModulo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsEmpresaAutenticacaoModulo", "TcsEmpresaModulo", 0, "IdLinx#IdLinx","IdModulo#IdModulo","IdTcsAplicativo#IdTcsAplicativo","IdTcsEmpresaModulo#IdTcsEmpresaModulo");
		
	        
             Linx.Framework.BV.Empresa.EmpresaDomainService serviceContext2 = new Linx.Framework.BV.Empresa.EmpresaDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsEmpresaAutenticacaoModulo> result = 
	            (
                 from TcsEmpresaModulo_Rep1 in serviceContext2.GetTcsEmpresaModuloByEntitySearchNoAssociations(repSerializedEntitySearch)
                orderby TcsEmpresaModulo_Rep1.IdTcsEmpresaModulo ascending
	            
	            	
	            select new TcsEmpresaAutenticacaoModulo()		
	            {
	            
                IdLinx = TcsEmpresaModulo_Rep1.IdLinx
                , IdModulo = TcsEmpresaModulo_Rep1.IdModulo
                , IdTcsAplicativo = TcsEmpresaModulo_Rep1.IdTcsAplicativo
                , IdTcsEmpresaModulo = TcsEmpresaModulo_Rep1.IdTcsEmpresaModulo
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsEmpresaAutenticacaoCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
		
	    [Ignore]
	    public int GetTcsEmpresaAutenticacaoModuloCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsUsuarioAutenticacao.
	    public IQueryable<TcsUsuarioAutenticacao> GetPagedTcsUsuarioAutenticacao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsUsuarioAutenticacao", "TcsUsuarioAutenticacao", 0, "AutenticacaoWindows#AutenticacaoWindows","CnpjCpf#CnpjCpf","ConfirmacaoUsuario#ConfirmacaoUsuario","ConfirmacaoUsuario1#ConfirmacaoUsuario1","CriaUsuario#CriaUsuario","DataAlteracao#DataAlteracao","DataCadastro#DataCadastro","DataExpiracaoSenha#DataExpiracaoSenha","Email#Email","GeraSenhaUsuario#GeraSenhaUsuario","IdLinx#IdLinx","IdUsuario#IdUsuario","LxPfjFisicaJuridica#LxPfjFisicaJuridica","NomeAutenticacao#NomeAutenticacao","NomeCurtoUsuario#NomeCurtoUsuario","NomeUsuario#NomeUsuario","UidUsuario#UidUsuario","VigenciaFinal#VigenciaFinal","VigenciaInicial#VigenciaInicial","Bairro#Bairro","Cep#Cep","Complemento#Complemento","FoneCelular#FoneCelular","FoneFixo#FoneFixo","Inativo#Inativo","IndicaAcessoSuporte#IndicaAcessoSuporte","InscrEstadualRg#InscrEstadualRg","Logradouro#Logradouro","LxTipoLogradouro#LxTipoLogradouro","Municipio#Municipio","NomeEmpresa#NomeEmpresa","Numero#Numero","ObsEndereco#ObsEndereco","Ramal#Ramal","Uf#Uf","UidEmpresa#UidEmpresa");
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsUsuarioAutenticacaoAcesso", "TcsUsuarioAcesso", 0, "IdTcsAmbiente#IdTcsAmbiente","IdTcsAmbienteRelacionado#IdTcsAmbienteRelacionado","IdTcsUsuarioAcesso#IdTcsUsuarioAcesso","IdUsuario#IdUsuario","IndicaAcessoPadrao#IndicaAcessoPadrao","IndicaAdministrador#IndicaAdministrador","IndicaMultiGpecon#IndicaMultiGpecon");
		
	        
             Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService serviceContext8 = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsUsuarioAutenticacao> result = 
	            (
                 from TcsUsuarioAutenticacao_Rep1 in serviceContext8.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(repSerializedEntitySearch)
                orderby TcsUsuarioAutenticacao_Rep1.IdUsuario ascending
	            
	            	
	            select new TcsUsuarioAutenticacao()		
	            {
	            
                AutenticacaoWindows = TcsUsuarioAutenticacao_Rep1.AutenticacaoWindows
                , CnpjCpf = TcsUsuarioAutenticacao_Rep1.CnpjCpf
                , ConfirmacaoUsuario = TcsUsuarioAutenticacao_Rep1.ConfirmacaoUsuario
                , ConfirmacaoUsuario1 = TcsUsuarioAutenticacao_Rep1.ConfirmacaoUsuario1
                , CriaUsuario = TcsUsuarioAutenticacao_Rep1.CriaUsuario
                , DataAlteracao = TcsUsuarioAutenticacao_Rep1.DataAlteracao
                , DataCadastro = TcsUsuarioAutenticacao_Rep1.DataCadastro
                , DataExpiracaoSenha = TcsUsuarioAutenticacao_Rep1.DataExpiracaoSenha
                , Email = TcsUsuarioAutenticacao_Rep1.Email
                , GeraSenhaUsuario = TcsUsuarioAutenticacao_Rep1.GeraSenhaUsuario
                , IdLinx = TcsUsuarioAutenticacao_Rep1.IdLinx
                , IdUsuario = TcsUsuarioAutenticacao_Rep1.IdUsuario
                , LxPfjFisicaJuridica = TcsUsuarioAutenticacao_Rep1.LxPfjFisicaJuridica
                , LxPfjFisicaJuridicaName = ((TcsUsuarioAutenticacao_Rep1.LxPfjFisicaJuridica) == 1 ? "Pessoa Física" : ((TcsUsuarioAutenticacao_Rep1.LxPfjFisicaJuridica) == 2 ? "Pessoa Jurídica" : ""))
                , NomeAutenticacao = TcsUsuarioAutenticacao_Rep1.NomeAutenticacao
                , NomeCurtoUsuario = TcsUsuarioAutenticacao_Rep1.NomeCurtoUsuario
                , NomeUsuario = TcsUsuarioAutenticacao_Rep1.NomeUsuario
                , UidUsuario = TcsUsuarioAutenticacao_Rep1.UidUsuario
                , VigenciaFinal = TcsUsuarioAutenticacao_Rep1.VigenciaFinal
                , VigenciaInicial = TcsUsuarioAutenticacao_Rep1.VigenciaInicial
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsUsuarioAutenticacaoAcesso.
	    public IQueryable<TcsUsuarioAutenticacaoAcesso> GetPagedTcsUsuarioAutenticacaoAcesso(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsUsuarioAutenticacaoAcesso", "TcsUsuarioAcesso", 0, "IdTcsAmbiente#IdTcsAmbiente","IdTcsAmbienteRelacionado#IdTcsAmbienteRelacionado","IdTcsUsuarioAcesso#IdTcsUsuarioAcesso","IdUsuario#IdUsuario","IndicaAcessoPadrao#IndicaAcessoPadrao","IndicaAdministrador#IndicaAdministrador","IndicaMultiGpecon#IndicaMultiGpecon");
		
	        
             Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService serviceContext8 = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsUsuarioAutenticacaoAcesso> result = 
	            (
                 from TcsUsuarioAcesso_Rep1 in serviceContext8.GetTcsUsuarioAcessoByEntitySearchNoAssociations(repSerializedEntitySearch)
                orderby TcsUsuarioAcesso_Rep1.IdTcsUsuarioAcesso ascending
	            
	            	
	            select new TcsUsuarioAutenticacaoAcesso()		
	            {
	            
                IdTcsAmbiente = TcsUsuarioAcesso_Rep1.IdTcsAmbiente
                , IdTcsAmbienteRelacionado = TcsUsuarioAcesso_Rep1.IdTcsAmbienteRelacionado
                , IdTcsUsuarioAcesso = TcsUsuarioAcesso_Rep1.IdTcsUsuarioAcesso
                , IdUsuario = TcsUsuarioAcesso_Rep1.IdUsuario
                , IndicaAcessoPadrao = TcsUsuarioAcesso_Rep1.IndicaAcessoPadrao
                , IndicaAdministrador = TcsUsuarioAcesso_Rep1.IndicaAdministrador
                , IndicaMultiGpecon = TcsUsuarioAcesso_Rep1.IndicaMultiGpecon
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsUsuarioAutenticacaoCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
		
	    [Ignore]
	    public int GetTcsUsuarioAutenticacaoAcessoCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsUsuarioPerfil.
	    public IQueryable<TcsUsuarioPerfil> GetPagedTcsUsuarioPerfil(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsUsuarioPerfil", "TcsUsuarioPerfilP", 0, "IdPerfil#IdPerfil","IdTcsUsuarioPerfil#IdTcsUsuarioPerfil","IdUsuario#IdUsuario");
		
	        
             Linx.Framework.BV.Usuario.UsuarioDomainService serviceContext7 = new Linx.Framework.BV.Usuario.UsuarioDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsUsuarioPerfil> result = 
	            (
                 from TcsUsuarioPerfilP_Rep1 in serviceContext7.GetTcsUsuarioPerfilPByEntitySearchNoAssociations(repSerializedEntitySearch)
                orderby TcsUsuarioPerfilP_Rep1.IdTcsUsuarioPerfil ascending
	            
	            	
	            select new TcsUsuarioPerfil()		
	            {
	            
                IdPerfil = TcsUsuarioPerfilP_Rep1.IdPerfil
                , IdTcsUsuarioPerfil = TcsUsuarioPerfilP_Rep1.IdTcsUsuarioPerfil
                , IdUsuario = TcsUsuarioPerfilP_Rep1.IdUsuario
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsUsuarioPerfilCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsAmbiente.
	    public IQueryable<TcsAmbiente> GetPagedTcsAmbiente(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsAmbiente", "TcsAmbiente", 0, "DescricaoAmbiente#DescricaoAmbiente","IdAplicacao#IdAplicacao","IdLinx#IdLinx","IdTcsAmbiente#IdTcsAmbiente","UidEmpresa#UidEmpresa","DescricaoAplicacao#DescricaoAplicacao","DescricaoAplicativo#DescricaoAplicativo","EmDesenvolvimento#EmDesenvolvimento","IdTcsAplicativo#IdTcsAplicativo","NomeEmpresa#NomeEmpresa","UidAplicacao#UidAplicacao","Url#Url","UrlWorkArea#UrlWorkArea");
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsAmbienteConexao", "TcsAmbienteConexao", 0, "IdTcsAmbiente#IdTcsAmbiente","IdTcsAmbienteConexao#IdTcsAmbienteConexao","IdTcsAplicativoConexao#IdTcsAplicativoConexao","IdTcsBancoServidor#IdTcsBancoServidor");
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsAmbienteUsuarioAcesso", "TcsAmbienteUsuarioAcesso", 0, "IdTcsAmbiente#IdTcsAmbiente","IdTcsUsuarioAcesso#IdTcsUsuarioAcesso","IdUsuario#IdUsuario","IndicaAdministrador#IndicaAdministrador","IndicaMultiGpecon#IndicaMultiGpecon","NomeAutenticacao#NomeAutenticacao","NomeUsuario#NomeUsuario","UidUsuario#UidUsuario");
		
	        
             Linx.Framework.BV.Ambiente.AmbienteDomainService serviceContext1 = new Linx.Framework.BV.Ambiente.AmbienteDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsAmbiente> result = 
	            (
                 from TcsAmbiente_Rep1 in serviceContext1.GetTcsAmbienteByEntitySearchNoAssociations(repSerializedEntitySearch)
                orderby TcsAmbiente_Rep1.IdTcsAmbiente ascending
	            
	            	
	            select new TcsAmbiente()		
	            {
	            
                DescricaoAmbiente = TcsAmbiente_Rep1.DescricaoAmbiente
                , IdAplicacao = TcsAmbiente_Rep1.IdAplicacao
                , IdLinx = TcsAmbiente_Rep1.IdLinx
                , IdTcsAmbiente = TcsAmbiente_Rep1.IdTcsAmbiente
                , UidEmpresa = TcsAmbiente_Rep1.UidEmpresa
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsAmbienteConexao.
	    public IQueryable<TcsAmbienteConexao> GetPagedTcsAmbienteConexao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsAmbienteConexao", "TcsAmbienteConexao", 0, "IdTcsAmbiente#IdTcsAmbiente","IdTcsAmbienteConexao#IdTcsAmbienteConexao","IdTcsAplicativoConexao#IdTcsAplicativoConexao","IdTcsBancoServidor#IdTcsBancoServidor");
		
	        
             Linx.Framework.BV.Ambiente.AmbienteDomainService serviceContext1 = new Linx.Framework.BV.Ambiente.AmbienteDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsAmbienteConexao> result = 
	            (
                 from TcsAmbienteConexao_Rep1 in serviceContext1.GetTcsAmbienteConexaoByEntitySearchNoAssociations(repSerializedEntitySearch)
                orderby TcsAmbienteConexao_Rep1.IdTcsAmbienteConexao ascending
	            
	            	
	            select new TcsAmbienteConexao()		
	            {
	            
                IdTcsAmbiente = TcsAmbienteConexao_Rep1.IdTcsAmbiente
                , IdTcsAmbienteConexao = TcsAmbienteConexao_Rep1.IdTcsAmbienteConexao
                , IdTcsAplicativoConexao = TcsAmbienteConexao_Rep1.IdTcsAplicativoConexao
                , IdTcsBancoServidor = TcsAmbienteConexao_Rep1.IdTcsBancoServidor
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsAmbienteUsuarioAcesso.
	    public IQueryable<TcsAmbienteUsuarioAcesso> GetPagedTcsAmbienteUsuarioAcesso(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsAmbienteUsuarioAcesso", "TcsAmbienteUsuarioAcesso", 0, "IdTcsAmbiente#IdTcsAmbiente","IdTcsUsuarioAcesso#IdTcsUsuarioAcesso","IdUsuario#IdUsuario","IndicaAdministrador#IndicaAdministrador","IndicaMultiGpecon#IndicaMultiGpecon","NomeAutenticacao#NomeAutenticacao","NomeUsuario#NomeUsuario","UidUsuario#UidUsuario");
		
	        
             Linx.Framework.BV.Ambiente.AmbienteDomainService serviceContext1 = new Linx.Framework.BV.Ambiente.AmbienteDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsAmbienteUsuarioAcesso> result = 
	            (
                 from TcsAmbienteUsuarioAcesso_Rep1 in serviceContext1.GetTcsAmbienteUsuarioAcessoByEntitySearchNoAssociations(repSerializedEntitySearch)
                orderby TcsAmbienteUsuarioAcesso_Rep1.IdTcsUsuarioAcesso ascending
	            
	            	
	            select new TcsAmbienteUsuarioAcesso()		
	            {
	            
                IdTcsAmbiente = TcsAmbienteUsuarioAcesso_Rep1.IdTcsAmbiente
                , IdTcsUsuarioAcesso = TcsAmbienteUsuarioAcesso_Rep1.IdTcsUsuarioAcesso
                , IdUsuario = TcsAmbienteUsuarioAcesso_Rep1.IdUsuario
                , IndicaAdministrador = TcsAmbienteUsuarioAcesso_Rep1.IndicaAdministrador
                , IndicaMultiGpecon = TcsAmbienteUsuarioAcesso_Rep1.IndicaMultiGpecon
                , NomeAutenticacao = TcsAmbienteUsuarioAcesso_Rep1.NomeAutenticacao
                , NomeUsuario = TcsAmbienteUsuarioAcesso_Rep1.NomeUsuario
                , UidUsuario = TcsAmbienteUsuarioAcesso_Rep1.UidUsuario
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsAmbienteCounting(string serializedEntitySearch)
	    {	
		 
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsAmbiente", "TcsAmbiente", 0, "DescricaoAmbiente#DescricaoAmbiente","IdAplicacao#IdAplicacao","IdLinx#IdLinx","IdTcsAmbiente#IdTcsAmbiente","UidEmpresa#UidEmpresa","DescricaoAplicacao#DescricaoAplicacao","DescricaoAplicativo#DescricaoAplicativo","EmDesenvolvimento#EmDesenvolvimento","IdTcsAplicativo#IdTcsAplicativo","NomeEmpresa#NomeEmpresa","UidAplicacao#UidAplicacao","Url#Url","UrlWorkArea#UrlWorkArea");
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsAmbienteConexao", "TcsAmbienteConexao", 0, "IdTcsAmbiente#IdTcsAmbiente","IdTcsAmbienteConexao#IdTcsAmbienteConexao","IdTcsAplicativoConexao#IdTcsAplicativoConexao","IdTcsBancoServidor#IdTcsBancoServidor");
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsAmbienteUsuarioAcesso", "TcsAmbienteUsuarioAcesso", 0, "IdTcsAmbiente#IdTcsAmbiente","IdTcsUsuarioAcesso#IdTcsUsuarioAcesso","IdUsuario#IdUsuario","IndicaAdministrador#IndicaAdministrador","IndicaMultiGpecon#IndicaMultiGpecon","NomeAutenticacao#NomeAutenticacao","NomeUsuario#NomeUsuario","UidUsuario#UidUsuario");

	        
             Linx.Framework.BV.Ambiente.AmbienteDomainService serviceContext1 = new Linx.Framework.BV.Ambiente.AmbienteDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };

	        return
	            (
                 from TcsAmbiente_Rep1 in serviceContext1.GetTcsAmbienteByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsAmbienteConexaoCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
		
	    [Ignore]
	    public int GetTcsAmbienteUsuarioAcessoCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsModuloGrupo.
	    public IQueryable<TcsModuloGrupo> GetPagedTcsModuloGrupo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsModuloGrupo", "TcsModuloGrupo", 0, "DescGrupoModulo#DescGrupoModulo","IdGrupoModulo#IdGrupoModulo","IdTcsAplicativo#IdTcsAplicativo","DescricaoAplicativo#DescricaoAplicativo");
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsModuloGrupoDetalhe", "TcsModuloDoGrupoDetalhe", 0, "IdGrupoModulo#IdGrupoModulo","IdModulo#IdModulo","IdModuloDoGrupo#IdModuloDoGrupo");
		
	        
             Linx.Framework.BV.Modulo.ModuloDomainService serviceContext3 = new Linx.Framework.BV.Modulo.ModuloDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsModuloGrupo> result = 
	            (
                 from TcsModuloGrupo_Rep1 in serviceContext3.GetTcsModuloGrupoByEntitySearchNoAssociations(repSerializedEntitySearch)
                orderby TcsModuloGrupo_Rep1.IdGrupoModulo ascending
	            
	            	
	            select new TcsModuloGrupo()		
	            {
	            
                DescGrupoModulo = TcsModuloGrupo_Rep1.DescGrupoModulo
                , IdGrupoModulo = TcsModuloGrupo_Rep1.IdGrupoModulo
                , IdTcsAplicativo = TcsModuloGrupo_Rep1.IdTcsAplicativo
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsModuloGrupoDetalhe.
	    public IQueryable<TcsModuloGrupoDetalhe> GetPagedTcsModuloGrupoDetalhe(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsModuloGrupoDetalhe", "TcsModuloDoGrupoDetalhe", 0, "IdGrupoModulo#IdGrupoModulo","IdModulo#IdModulo","IdModuloDoGrupo#IdModuloDoGrupo");
		
	        
             Linx.Framework.BV.Modulo.ModuloDomainService serviceContext3 = new Linx.Framework.BV.Modulo.ModuloDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsModuloGrupoDetalhe> result = 
	            (
                 from TcsModuloDoGrupoDetalhe_Rep1 in serviceContext3.GetTcsModuloDoGrupoDetalheByEntitySearchNoAssociations(repSerializedEntitySearch)
                orderby TcsModuloDoGrupoDetalhe_Rep1.IdModuloDoGrupo ascending
	            
	            	
	            select new TcsModuloGrupoDetalhe()		
	            {
	            
                IdGrupoModulo = TcsModuloDoGrupoDetalhe_Rep1.IdGrupoModulo
                , IdModulo = TcsModuloDoGrupoDetalhe_Rep1.IdModulo
                , IdModuloDoGrupo = TcsModuloDoGrupoDetalhe_Rep1.IdModuloDoGrupo
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsModuloGrupoCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
		
	    [Ignore]
	    public int GetTcsModuloGrupoDetalheCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsParametroValor.
	    public IQueryable<TcsParametroValor> GetPagedTcsParametroValor(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsParametroValor", "TcsParametroValorP", 0, "IdParametro#IdParametro","IdParametroValor#IdParametroValor","ValorParametro#ValorParametro","LxDatatypeParametro#LxDatatypeParametro","PossuiVariacao#PossuiVariacao","ValorParametroBool#ValorParametroBool","ValorParametroData#ValorParametroData");
		
	        
             Linx.Framework.BV.Parametro.ParametroDomainService serviceContext4 = new Linx.Framework.BV.Parametro.ParametroDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsParametroValor> result = 
	            (
                 from TcsParametroValorP_Rep1 in serviceContext4.GetTcsParametroValorPByEntitySearchNoAssociations(repSerializedEntitySearch)
                orderby TcsParametroValorP_Rep1.IdParametroValor ascending
	            
	            	
	            select new TcsParametroValor()		
	            {
	            
                IdParametro = TcsParametroValorP_Rep1.IdParametro
                , IdParametroValor = TcsParametroValorP_Rep1.IdParametroValor
                , ValorParametro = TcsParametroValorP_Rep1.ValorParametro
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsParametroValorCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsPerfil.
	    public IQueryable<TcsPerfil> GetPagedTcsPerfil(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsPerfil", "TcsPerfil", 0, "DescPerfil#DescPerfil","IdPerfil#IdPerfil","Inativo#Inativo","IndicaPerfilLinx#IndicaPerfilLinx","PerfilAutenticacao#PerfilAutenticacao");
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsPerfilRegraModulo", "TcsPerfilRegraModulo", 0, "IdModulo#IdModulo","IdPerfil#IdPerfil","IdPerfilRegraModulo#IdPerfilRegraModulo","LxRegraAcessoModulo#LxRegraAcessoModulo");
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsPerfilUsuario", "TcsUsuarioPerfil", 0, "IdPerfil#IdPerfil","IdTcsUsuarioPerfil#IdTcsUsuarioPerfil","IdUsuario#IdUsuario");
		
	        
             Linx.Framework.BV.Perfil.PerfilDomainService serviceContext6 = new Linx.Framework.BV.Perfil.PerfilDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsPerfil> result = 
	            (
                 from TcsPerfil_Rep1 in serviceContext6.GetTcsPerfilByEntitySearchNoAssociations(repSerializedEntitySearch)
                orderby TcsPerfil_Rep1.IdPerfil ascending
	            
	            	
	            select new TcsPerfil()		
	            {
	            
                DescPerfil = TcsPerfil_Rep1.DescPerfil
                , IdPerfil = TcsPerfil_Rep1.IdPerfil
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsPerfilRegraModulo.
	    public IQueryable<TcsPerfilRegraModulo> GetPagedTcsPerfilRegraModulo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsPerfilRegraModulo", "TcsPerfilRegraModulo", 0, "IdModulo#IdModulo","IdPerfil#IdPerfil","IdPerfilRegraModulo#IdPerfilRegraModulo","LxRegraAcessoModulo#LxRegraAcessoModulo");
		
	        
             Linx.Framework.BV.Perfil.PerfilDomainService serviceContext6 = new Linx.Framework.BV.Perfil.PerfilDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsPerfilRegraModulo> result = 
	            (
                 from TcsPerfilRegraModulo_Rep1 in serviceContext6.GetTcsPerfilRegraModuloByEntitySearchNoAssociations(repSerializedEntitySearch)
                orderby TcsPerfilRegraModulo_Rep1.IdPerfilRegraModulo ascending
	            
	            	
	            select new TcsPerfilRegraModulo()		
	            {
	            
                IdModulo = TcsPerfilRegraModulo_Rep1.IdModulo
                , IdPerfil = TcsPerfilRegraModulo_Rep1.IdPerfil
                , IdPerfilRegraModulo = TcsPerfilRegraModulo_Rep1.IdPerfilRegraModulo
                , LxRegraAcessoModulo = TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo
                , LxRegraAcessoModuloName = ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 1 ? "Acesso Bloqueado" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 2 ? "Acesso Total" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 5 ? "Alterar" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 12 ? "Criar Pesquisa" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 10 ? "Criar Relatório" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 6 ? "Excluir" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 9 ? "Exportar" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 8 ? "Imprimir" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 4 ? "Incluir" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 11 ? "Layout" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 7 ? "Pesquisa Especial" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 3 ? "Pesquisar" : ((TcsPerfilRegraModulo_Rep1.LxRegraAcessoModulo) == 99 ? "Regra Transação" : "")))))))))))))
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsPerfilUsuario.
	    public IQueryable<TcsPerfilUsuario> GetPagedTcsPerfilUsuario(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsPerfilUsuario", "TcsUsuarioPerfil", 0, "IdPerfil#IdPerfil","IdTcsUsuarioPerfil#IdTcsUsuarioPerfil","IdUsuario#IdUsuario");
		
	        
             Linx.Framework.BV.Perfil.PerfilDomainService serviceContext6 = new Linx.Framework.BV.Perfil.PerfilDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsPerfilUsuario> result = 
	            (
                 from TcsUsuarioPerfil_Rep2 in serviceContext6.GetTcsUsuarioPerfilByEntitySearchNoAssociations(repSerializedEntitySearch)
                orderby TcsUsuarioPerfil_Rep2.IdTcsUsuarioPerfil ascending
	            
	            	
	            select new TcsPerfilUsuario()		
	            {
	            
                IdPerfil = TcsUsuarioPerfil_Rep2.IdPerfil
                , IdTcsUsuarioPerfil = TcsUsuarioPerfil_Rep2.IdTcsUsuarioPerfil
                , IdUsuario = TcsUsuarioPerfil_Rep2.IdUsuario
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsPerfilCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
		
	    [Ignore]
	    public int GetTcsPerfilRegraModuloCounting(string serializedEntitySearch)
	    {	
		 
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsPerfilRegraModulo", "TcsPerfilRegraModulo", 0, "IdModulo#IdModulo","IdPerfil#IdPerfil","IdPerfilRegraModulo#IdPerfilRegraModulo","LxRegraAcessoModulo#LxRegraAcessoModulo");

	        
             Linx.Framework.BV.Perfil.PerfilDomainService serviceContext6 = new Linx.Framework.BV.Perfil.PerfilDomainService(this.Headers) { IsSecure = this.IsSecure };

	        return
	            (
                 from TcsPerfilRegraModulo_Rep1 in serviceContext6.GetTcsPerfilRegraModuloByEntitySearchNoAssociations(repSerializedEntitySearch)
	            
	            select 1
	            ).Count();	
		
	    }
		
	    [Ignore]
	    public int GetTcsPerfilUsuarioCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedAmbienteInfo.
	    public IEnumerable<AmbienteInfo> GetPagedAmbienteInfo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<AmbienteInfo> result = new List<AmbienteInfo>();
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetAmbienteInfoCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsEmpresaGpecon.
	    public IQueryable<TcsEmpresaGpecon> GetPagedTcsEmpresaGpecon(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsEmpresaGpecon", "TcsEmpresaGpeconP", 0, "IdLinx#IdLinx","IdLinxGpecon#IdLinxGpecon");
		
	        
             Linx.Framework.BV.Empresa.EmpresaDomainService serviceContext2 = new Linx.Framework.BV.Empresa.EmpresaDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsEmpresaGpecon> result = 
	            (
                 from TcsEmpresaGpeconP_Rep1 in serviceContext2.GetTcsEmpresaGpeconPByEntitySearchNoAssociations(repSerializedEntitySearch)
                orderby TcsEmpresaGpeconP_Rep1.IdLinx ascending, TcsEmpresaGpeconP_Rep1.IdLinxGpecon ascending
	            
	            	
	            select new TcsEmpresaGpecon()		
	            {
	            
                IdLinx = TcsEmpresaGpeconP_Rep1.IdLinx
                , IdLinxGpecon = TcsEmpresaGpeconP_Rep1.IdLinxGpecon
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsEmpresaGpeconCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsAmbienteInfo.
	    public IQueryable<TcsAmbienteInfo> GetPagedTcsAmbienteInfo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        //Get filter expression	  
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAmbienteInfo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	        if (!jEntitySearch.IsNullOrEmpty()) 
	        { 
	            string bmSearch = this.GetEDM().JExpressionToEntitySql(jEntitySearch, parameters);
	            if (!bmSearch.IsNullOrEmpty()) dynQuery += (dynQuery.IsNullOrEmpty() ? "" : " And ") + bmSearch;
	        }

	        if (dynQuery.IsNullOrEmpty())
	        		dynQuery = "true";

	        

		
	
	        
		
	        
	
	        IQueryable<TcsAmbienteInfo> result = 
	            (from entity0 in this.DbContext.TCS_AMBIENTE.Where(dynQuery, parameters.ToArray())
                  let entity0Al1 = entity0.TCS_APLICACAO
                  let entity0Al2 = entity0.TCS_EMPRESA_AUTENTICACAO
                orderby entity0.ID_TCS_AMBIENTE ascending
	            
	            	
	            select new TcsAmbienteInfo()		
	            {
	            
                IdAplicacao = entity0Al1.ID_APLICACAO
                , IdLinx = entity0Al2.ID_LINX
                , IdTcsAmbiente = entity0.ID_TCS_AMBIENTE
                , UidAplicacao = entity0Al1.UID_APLICACAO
                , UidEmpresa = entity0Al2.UID_EMPRESA
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsAmbienteInfoCounting(string serializedEntitySearch)
	    {	
		 
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));
	        string dynQuery = String.Empty;
	        List<ObjectParameter> parameters = new List<ObjectParameter>();
	        EntitySearch.AdjustExcludedFilters(entitySearchList, GetExcludedFilters());
	        List<EntitySearch> replacedEntitySearchList = EntitySearch.ReplaceFieldToFilterDataKey(entitySearchList, typeof(TcsAmbienteInfo));
	        replacedEntitySearchList.GetEntityQueryExpression(ref dynQuery, parameters);
	
	        if (dynQuery.IsNullOrEmpty())
	            dynQuery = "true";

	        

	        

	        return
	            (from entity in this.DbContext.TCS_AMBIENTE.Where(dynQuery, parameters.ToArray())
                  let entityAl1 = entity.TCS_APLICACAO
                  let entityAl2 = entity.TCS_EMPRESA_AUTENTICACAO
	            
	            select 1
	            ).Count();	
		
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTcsParametroAutorizacao.
	    public IQueryable<TcsParametroAutorizacao> GetPagedTcsParametroAutorizacao(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TcsParametroAutorizacao", "TcsParametroAutorizacao", 0, "IdParametro#IdParametro","IdTcsAplicativo#IdTcsAplicativo","TituloParametro#TituloParametro","ColunaCodValida#ColunaCodValida","ColunaDescValida#ColunaDescValida","DescGrupoParametro#DescGrupoParametro","DescParametro#DescParametro","DescricaoAplicativo#DescricaoAplicativo","DescTabela#DescTabela","FaixaFinal#FaixaFinal","FaixaInicial#FaixaInicial","IdGrupoParametro#IdGrupoParametro","IndicaEnviaPdv#IndicaEnviaPdv","IndicaParametroLinx#IndicaParametroLinx","LxDatatypeParametro#LxDatatypeParametro","LxTipoValidacaoParametro#LxTipoValidacaoParametro","NivelAcesso#NivelAcesso","NivelAcessoEdicao#NivelAcessoEdicao","ObsParametro#ObsParametro","PermiteVariacaoPorEntidade#PermiteVariacaoPorEntidade","UidTabela#UidTabela");
		
	        
             Linx.Framework.BV.ParametroAutorizacao.ParametroAutorizacaoDomainService serviceContext5 = new Linx.Framework.BV.ParametroAutorizacao.ParametroAutorizacaoDomainService(this.GetEDM(), this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TcsParametroAutorizacao> result = 
	            (
                 from TcsParametroAutorizacao_Rep1 in serviceContext5.GetTcsParametroAutorizacaoByEntitySearchNoAssociations(repSerializedEntitySearch)
                orderby TcsParametroAutorizacao_Rep1.IdParametro ascending
	            
	            	
	            select new TcsParametroAutorizacao()		
	            {
	            
                IdParametro = TcsParametroAutorizacao_Rep1.IdParametro
                , IdTcsAplicativo = TcsParametroAutorizacao_Rep1.IdTcsAplicativo
                , TituloParametro = TcsParametroAutorizacao_Rep1.TituloParametro
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTcsParametroAutorizacaoCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedMultimarcaInfo.
	    public IEnumerable<MultimarcaInfo> GetPagedMultimarcaInfo(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

	
	        IEnumerable<MultimarcaInfo> result = new List<MultimarcaInfo>();
	  	
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetMultimarcaInfoCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTbcFilial.
	    public IQueryable<TbcFilial> GetPagedTbcFilial(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TbcFilial", "TbcFilial", 0, "Bairro#Bairro","BandeiraRede#BandeiraRede","Cep#Cep","CnpjCpf#CnpjCpf","CodDeposito#CodDeposito","CodigoFilial#CodigoFilial","CodigoPfj#CodigoPfj","Complemento#Complemento","DddCelular#DddCelular","DddFixo#DddFixo","Email#Email","FoneCelular#FoneCelular","FoneFixo#FoneFixo","IdFilialPfj#IdFilialPfj","IdGpecon#IdGpecon","IdLjvCanalVenda#IdLjvCanalVenda","IdMatrizContabil#IdMatrizContabil","IdPfj#IdPfj","IncluiDeposito#IncluiDeposito","IncluiLoja#IncluiLoja","IndicaEstrangeiro#IndicaEstrangeiro","IndicaFilial#IndicaFilial","IndicaLoja#IndicaLoja","IndicaMatrizContabil#IndicaMatrizContabil","InscrEstadual#InscrEstadual","Logradouro#Logradouro","LxPfjFisicaJuridica#LxPfjFisicaJuridica","LxTipoLogradouro#LxTipoLogradouro","Municipio#Municipio","NomeFantasiaApelido#NomeFantasiaApelido","NomeFilial#NomeFilial","Numero#Numero","ObsEndereco#ObsEndereco","Pais#Pais","RazaoSocialNomeCompleto#RazaoSocialNomeCompleto","Uf#Uf","CodAgrupadorRegraFilial#CodAgrupadorRegraFilial","CodBandeiraRede#CodBandeiraRede","CodCanalVenda#CodCanalVenda","CodigoMatrizContabil#CodigoMatrizContabil","CodRegiaoComercial#CodRegiaoComercial","CtrlStkLocalizacao#CtrlStkLocalizacao","CtrlStkLote#CtrlStkLote","DescAgrupadorRegraFilial#DescAgrupadorRegraFilial","DescBandeiraRede#DescBandeiraRede","DescCanalVenda#DescCanalVenda","DescGrupoEconomico#DescGrupoEconomico","DescRegiaoComercial#DescRegiaoComercial","IdAgrupadorRegraFilial#IdAgrupadorRegraFilial","IdRegiaoComercial#IdRegiaoComercial","Inativo#Inativo","IndicaEcommerce#IndicaEcommerce","IndicaFranquia#IndicaFranquia","IndicaHubfiscal#IndicaHubfiscal","IndicaHubfiscalAtualizado#IndicaHubfiscalAtualizado","NomeMatrizContabil#NomeMatrizContabil");
		
	        
             Linx.CadastroBase.BV.CadastroPfj.CadastroPfjDomainService serviceContext0 = new Linx.CadastroBase.BV.CadastroPfj.CadastroPfjDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TbcFilial> result = 
	            (
                 from TbcFilial_Rep2 in serviceContext0.GetTbcFilialByEntitySearchNoAssociations(repSerializedEntitySearch)
                orderby TbcFilial_Rep2.IdPfj ascending
	            
	            	
	            select new TbcFilial()		
	            {
	            
                Bairro = TbcFilial_Rep2.Bairro
                , BandeiraRede = TbcFilial_Rep2.BandeiraRede
                , Cep = TbcFilial_Rep2.Cep
                , CnpjCpf = TbcFilial_Rep2.CnpjCpf
                , CodDeposito = TbcFilial_Rep2.CodDeposito
                , CodigoFilial = TbcFilial_Rep2.CodigoFilial
                , CodigoPfj = TbcFilial_Rep2.CodigoPfj
                , Complemento = TbcFilial_Rep2.Complemento
                , DddCelular = TbcFilial_Rep2.DddCelular
                , DddFixo = TbcFilial_Rep2.DddFixo
                , Email = TbcFilial_Rep2.Email
                , FoneCelular = TbcFilial_Rep2.FoneCelular
                , FoneFixo = TbcFilial_Rep2.FoneFixo
                , IdFilialPfj = TbcFilial_Rep2.IdFilialPfj
                , IdGpecon = TbcFilial_Rep2.IdGpecon
                , IdLjvCanalVenda = TbcFilial_Rep2.IdLjvCanalVenda
                , IdMatrizContabil = TbcFilial_Rep2.IdMatrizContabil
                , IdPfj = TbcFilial_Rep2.IdPfj
                , IncluiDeposito = TbcFilial_Rep2.IncluiDeposito
                , IncluiLoja = TbcFilial_Rep2.IncluiLoja
                , IndicaEstrangeiro = TbcFilial_Rep2.IndicaEstrangeiro
                , IndicaFilial = TbcFilial_Rep2.IndicaFilial
                , IndicaLoja = TbcFilial_Rep2.IndicaLoja
                , IndicaMatrizContabil = TbcFilial_Rep2.IndicaMatrizContabil
                , InscrEstadual = TbcFilial_Rep2.InscrEstadual
                , Logradouro = TbcFilial_Rep2.Logradouro
                , LxPfjFisicaJuridica = TbcFilial_Rep2.LxPfjFisicaJuridica
                , LxPfjFisicaJuridicaName = ((TbcFilial_Rep2.LxPfjFisicaJuridica) == 1 ? "Pessoa Física" : ((TbcFilial_Rep2.LxPfjFisicaJuridica) == 2 ? "Pessoa Jurídica" : ""))
                , LxTipoLogradouro = TbcFilial_Rep2.LxTipoLogradouro
                , LxTipoLogradouroName = ((TbcFilial_Rep2.LxTipoLogradouro) == 1 ? "Aeroporto" : ((TbcFilial_Rep2.LxTipoLogradouro) == 2 ? "Alameda" : ((TbcFilial_Rep2.LxTipoLogradouro) == 3 ? "Apartamento" : ((TbcFilial_Rep2.LxTipoLogradouro) == 4 ? "Avenida" : ((TbcFilial_Rep2.LxTipoLogradouro) == 5 ? "Beco" : ((TbcFilial_Rep2.LxTipoLogradouro) == 6 ? "Bloco" : ((TbcFilial_Rep2.LxTipoLogradouro) == 7 ? "Caminho" : ((TbcFilial_Rep2.LxTipoLogradouro) == 8 ? "Escadinha" : ((TbcFilial_Rep2.LxTipoLogradouro) == 9 ? "Estação" : ((TbcFilial_Rep2.LxTipoLogradouro) == 10 ? "Estrada" : ((TbcFilial_Rep2.LxTipoLogradouro) == 11 ? "Fazenda" : ((TbcFilial_Rep2.LxTipoLogradouro) == 12 ? "Fortaleza" : ((TbcFilial_Rep2.LxTipoLogradouro) == 13 ? "Galeria" : ((TbcFilial_Rep2.LxTipoLogradouro) == 14 ? "Ladeira" : ((TbcFilial_Rep2.LxTipoLogradouro) == 15 ? "Largo" : ((TbcFilial_Rep2.LxTipoLogradouro) == 17 ? "Parque" : ((TbcFilial_Rep2.LxTipoLogradouro) == 16 ? "Praça" : ((TbcFilial_Rep2.LxTipoLogradouro) == 18 ? "Praia" : ((TbcFilial_Rep2.LxTipoLogradouro) == 19 ? "Quadra" : ((TbcFilial_Rep2.LxTipoLogradouro) == 20 ? "Quilômetro" : ((TbcFilial_Rep2.LxTipoLogradouro) == 21 ? "Quinta" : ((TbcFilial_Rep2.LxTipoLogradouro) == 22 ? "Rodovia" : ((TbcFilial_Rep2.LxTipoLogradouro) == 23 ? "Rua" : ((TbcFilial_Rep2.LxTipoLogradouro) == 24 ? "Super Quadra" : ((TbcFilial_Rep2.LxTipoLogradouro) == 25 ? "Travessa" : ((TbcFilial_Rep2.LxTipoLogradouro) == 26 ? "Viaduto" : ((TbcFilial_Rep2.LxTipoLogradouro) == 27 ? "Vila" : "")))))))))))))))))))))))))))
                , Municipio = TbcFilial_Rep2.Municipio
                , NomeFantasiaApelido = TbcFilial_Rep2.NomeFantasiaApelido
                , NomeFilial = TbcFilial_Rep2.NomeFilial
                , Numero = TbcFilial_Rep2.Numero
                , ObsEndereco = TbcFilial_Rep2.ObsEndereco
                , Pais = TbcFilial_Rep2.Pais
                , RazaoSocialNomeCompleto = TbcFilial_Rep2.RazaoSocialNomeCompleto
                , Uf = TbcFilial_Rep2.Uf
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTbcFilialCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTbcGrupoEconomico.
	    public IQueryable<TbcGrupoEconomico> GetPagedTbcGrupoEconomico(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TbcGrupoEconomico", "TbcGrupoEconomico", 0, "DescGrupoEconomico#DescGrupoEconomico","IdGpeconCadastro#IdGpeconCadastro","FatorCambio#FatorCambio","IndicaGpeconMaster#IndicaGpeconMaster","IndicaMoedaForte#IndicaMoedaForte");
		
	        
             Linx.Operacional.CadastroBase.BV.GrupoEconomico.GrupoEconomicoDomainService serviceContext11 = new Linx.Operacional.CadastroBase.BV.GrupoEconomico.GrupoEconomicoDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TbcGrupoEconomico> result = 
	            (
                 from TbcGrupoEconomico_Rep1 in serviceContext11.GetTbcGrupoEconomicoByEntitySearchNoAssociations(repSerializedEntitySearch)
                orderby TbcGrupoEconomico_Rep1.IdGpeconCadastro ascending
	            
	            	
	            select new TbcGrupoEconomico()		
	            {
	            
                DescGrupoEconomico = TbcGrupoEconomico_Rep1.DescGrupoEconomico
                , IdGpeconCadastro = TbcGrupoEconomico_Rep1.IdGpeconCadastro
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTbcGrupoEconomicoCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedTbcBandeiraRede.
	    public IQueryable<TbcBandeiraRede> GetPagedTbcBandeiraRede(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"TbcBandeiraRede", "TbcBandeiraRede", 0, "CodBandeiraRede#CodBandeiraRede","IdBandeiraRedeCadastro#IdBandeiraRedeCadastro","IdLinx#IdLinx","DataAtualizacao#DataAtualizacao","DataCadastro#DataCadastro","DescBandeiraRede#DescBandeiraRede");
		
	        
             Linx.Operacional.CadastroBase.BV.BandeiraRede.BandeiraRedeDomainService serviceContext9 = new Linx.Operacional.CadastroBase.BV.BandeiraRede.BandeiraRedeDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<TbcBandeiraRede> result = 
	            (
                 from TbcBandeiraRede_Rep1 in serviceContext9.GetTbcBandeiraRedeByEntitySearchNoAssociations(repSerializedEntitySearch)
                orderby TbcBandeiraRede_Rep1.IdBandeiraRedeCadastro ascending
	            
	            	
	            select new TbcBandeiraRede()		
	            {
	            
                CodBandeiraRede = TbcBandeiraRede_Rep1.CodBandeiraRede
                , IdBandeiraRedeCadastro = TbcBandeiraRede_Rep1.IdBandeiraRedeCadastro
                , IdLinx = TbcBandeiraRede_Rep1.IdLinx
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetTbcBandeiraRedeCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
			
	
	    
	    [Ignore]
	    //Get PagedLjvCanalVenda.
	    public IQueryable<LjvCanalVenda> GetPagedLjvCanalVenda(string serializedEntitySearch, int skip, int take, string jEntitySearch = "")
	    {




	
	        List<EntitySearch> entitySearchList = (serializedEntitySearch.IsNullOrEmpty() ? new List<EntitySearch>() : SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch));

		
	
	        
             string repSerializedEntitySearch = serializedEntitySearch;
             repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch,"LjvCanalVenda", "LjvCanalVenda", 0, "CodCanalVenda#CodCanalVenda","DescCanalVenda#DescCanalVenda","IdLjvCanalVenda#IdLjvCanalVenda");
		
	        
             Linx.Operacional.CadastroBase.BV.CanalVenda.CanalVendaDomainService serviceContext10 = new Linx.Operacional.CadastroBase.BV.CanalVenda.CanalVendaDomainService(this.Headers) { IsSecure = this.IsSecure };
	
	        IQueryable<LjvCanalVenda> result = 
	            (
                 from LjvCanalVenda_Rep1 in serviceContext10.GetLjvCanalVendaByEntitySearchNoAssociations(repSerializedEntitySearch)
                orderby LjvCanalVenda_Rep1.IdLjvCanalVenda ascending
	            
	            	
	            select new LjvCanalVenda()		
	            {
	            
                CodCanalVenda = LjvCanalVenda_Rep1.CodCanalVenda
                , DescCanalVenda = LjvCanalVenda_Rep1.DescCanalVenda
                , IdLjvCanalVenda = LjvCanalVenda_Rep1.IdLjvCanalVenda
		
	            }
	            ).Skip(skip).Take(take);
		
	
	        	

	
	        return result;
	    }
		
	    [Ignore]
	    public int GetLjvCanalVendaCounting(string serializedEntitySearch)
	    {	
		 
		         return 1;

		 
	    }
		
	    #endregion Paging Methods Definitions.


	    #region Update Methods Definitions.
	
			
	    
	    [Update()]	
	    //Update TcsEmpresaAutenticacao.
	    public void UpdateTcsEmpresaAutenticacao(TcsEmpresaAutenticacao entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert TcsEmpresaAutenticacao.
	    public void InsertTcsEmpresaAutenticacao(TcsEmpresaAutenticacao entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete TcsEmpresaAutenticacao.
	    public void DeleteTcsEmpresaAutenticacao(TcsEmpresaAutenticacao entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsEmpresaAutenticacaoModulo.
	    public void UpdateTcsEmpresaAutenticacaoModulo(TcsEmpresaAutenticacaoModulo entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert TcsEmpresaAutenticacaoModulo.
	    public void InsertTcsEmpresaAutenticacaoModulo(TcsEmpresaAutenticacaoModulo entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete TcsEmpresaAutenticacaoModulo.
	    public void DeleteTcsEmpresaAutenticacaoModulo(TcsEmpresaAutenticacaoModulo entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsUsuarioAutenticacao.
	    public void UpdateTcsUsuarioAutenticacao(TcsUsuarioAutenticacao entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert TcsUsuarioAutenticacao.
	    public void InsertTcsUsuarioAutenticacao(TcsUsuarioAutenticacao entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete TcsUsuarioAutenticacao.
	    public void DeleteTcsUsuarioAutenticacao(TcsUsuarioAutenticacao entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsUsuarioAutenticacaoAcesso.
	    public void UpdateTcsUsuarioAutenticacaoAcesso(TcsUsuarioAutenticacaoAcesso entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert TcsUsuarioAutenticacaoAcesso.
	    public void InsertTcsUsuarioAutenticacaoAcesso(TcsUsuarioAutenticacaoAcesso entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete TcsUsuarioAutenticacaoAcesso.
	    public void DeleteTcsUsuarioAutenticacaoAcesso(TcsUsuarioAutenticacaoAcesso entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsUsuarioPerfil.
	    public void UpdateTcsUsuarioPerfil(TcsUsuarioPerfil entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert TcsUsuarioPerfil.
	    public void InsertTcsUsuarioPerfil(TcsUsuarioPerfil entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete TcsUsuarioPerfil.
	    public void DeleteTcsUsuarioPerfil(TcsUsuarioPerfil entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsAmbiente.
	    public void UpdateTcsAmbiente(TcsAmbiente entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert TcsAmbiente.
	    public void InsertTcsAmbiente(TcsAmbiente entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete TcsAmbiente.
	    public void DeleteTcsAmbiente(TcsAmbiente entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsAmbienteConexao.
	    public void UpdateTcsAmbienteConexao(TcsAmbienteConexao entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert TcsAmbienteConexao.
	    public void InsertTcsAmbienteConexao(TcsAmbienteConexao entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete TcsAmbienteConexao.
	    public void DeleteTcsAmbienteConexao(TcsAmbienteConexao entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsAmbienteUsuarioAcesso.
	    public void UpdateTcsAmbienteUsuarioAcesso(TcsAmbienteUsuarioAcesso entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert TcsAmbienteUsuarioAcesso.
	    public void InsertTcsAmbienteUsuarioAcesso(TcsAmbienteUsuarioAcesso entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete TcsAmbienteUsuarioAcesso.
	    public void DeleteTcsAmbienteUsuarioAcesso(TcsAmbienteUsuarioAcesso entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsModuloGrupo.
	    public void UpdateTcsModuloGrupo(TcsModuloGrupo entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert TcsModuloGrupo.
	    public void InsertTcsModuloGrupo(TcsModuloGrupo entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete TcsModuloGrupo.
	    public void DeleteTcsModuloGrupo(TcsModuloGrupo entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsModuloGrupoDetalhe.
	    public void UpdateTcsModuloGrupoDetalhe(TcsModuloGrupoDetalhe entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert TcsModuloGrupoDetalhe.
	    public void InsertTcsModuloGrupoDetalhe(TcsModuloGrupoDetalhe entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete TcsModuloGrupoDetalhe.
	    public void DeleteTcsModuloGrupoDetalhe(TcsModuloGrupoDetalhe entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsParametroValor.
	    public void UpdateTcsParametroValor(TcsParametroValor entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert TcsParametroValor.
	    public void InsertTcsParametroValor(TcsParametroValor entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete TcsParametroValor.
	    public void DeleteTcsParametroValor(TcsParametroValor entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsPerfil.
	    public void UpdateTcsPerfil(TcsPerfil entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert TcsPerfil.
	    public void InsertTcsPerfil(TcsPerfil entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete TcsPerfil.
	    public void DeleteTcsPerfil(TcsPerfil entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsPerfilRegraModulo.
	    public void UpdateTcsPerfilRegraModulo(TcsPerfilRegraModulo entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert TcsPerfilRegraModulo.
	    public void InsertTcsPerfilRegraModulo(TcsPerfilRegraModulo entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete TcsPerfilRegraModulo.
	    public void DeleteTcsPerfilRegraModulo(TcsPerfilRegraModulo entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsPerfilUsuario.
	    public void UpdateTcsPerfilUsuario(TcsPerfilUsuario entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert TcsPerfilUsuario.
	    public void InsertTcsPerfilUsuario(TcsPerfilUsuario entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete TcsPerfilUsuario.
	    public void DeleteTcsPerfilUsuario(TcsPerfilUsuario entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update AmbienteInfo.
	    public void UpdateAmbienteInfo(AmbienteInfo entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert AmbienteInfo.
	    public void InsertAmbienteInfo(AmbienteInfo entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete AmbienteInfo.
	    public void DeleteAmbienteInfo(AmbienteInfo entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsEmpresaGpecon.
	    public void UpdateTcsEmpresaGpecon(TcsEmpresaGpecon entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert TcsEmpresaGpecon.
	    public void InsertTcsEmpresaGpecon(TcsEmpresaGpecon entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete TcsEmpresaGpecon.
	    public void DeleteTcsEmpresaGpecon(TcsEmpresaGpecon entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsAmbienteInfo.
	    public void UpdateTcsAmbienteInfo(TcsAmbienteInfo entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Update, null); 	
	            

	
	    }

	    
	    [Insert()]
	    //Insert TcsAmbienteInfo.
	    public void InsertTcsAmbienteInfo(TcsAmbienteInfo entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Insert, null);
	            

	
	    }

	    
	    [Delete()]
	    //Delete TcsAmbienteInfo.
	    public void DeleteTcsAmbienteInfo(TcsAmbienteInfo entity)
	    {



	
	            entity.ApplyChanges(this.DbContext, null, ChangeOperation.Delete, null);
	            

	
	    }
		
			
	    
	    [Update()]	
	    //Update TcsParametroAutorizacao.
	    public void UpdateTcsParametroAutorizacao(TcsParametroAutorizacao entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert TcsParametroAutorizacao.
	    public void InsertTcsParametroAutorizacao(TcsParametroAutorizacao entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete TcsParametroAutorizacao.
	    public void DeleteTcsParametroAutorizacao(TcsParametroAutorizacao entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update MultimarcaInfo.
	    public void UpdateMultimarcaInfo(MultimarcaInfo entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert MultimarcaInfo.
	    public void InsertMultimarcaInfo(MultimarcaInfo entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete MultimarcaInfo.
	    public void DeleteMultimarcaInfo(MultimarcaInfo entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TbcFilial.
	    public void UpdateTbcFilial(TbcFilial entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert TbcFilial.
	    public void InsertTbcFilial(TbcFilial entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete TbcFilial.
	    public void DeleteTbcFilial(TbcFilial entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TbcGrupoEconomico.
	    public void UpdateTbcGrupoEconomico(TbcGrupoEconomico entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert TbcGrupoEconomico.
	    public void InsertTbcGrupoEconomico(TbcGrupoEconomico entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete TbcGrupoEconomico.
	    public void DeleteTbcGrupoEconomico(TbcGrupoEconomico entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update TbcBandeiraRede.
	    public void UpdateTbcBandeiraRede(TbcBandeiraRede entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert TbcBandeiraRede.
	    public void InsertTbcBandeiraRede(TbcBandeiraRede entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete TbcBandeiraRede.
	    public void DeleteTbcBandeiraRede(TbcBandeiraRede entity)
	    {



	
	    }
		
			
	    
	    [Update()]	
	    //Update LjvCanalVenda.
	    public void UpdateLjvCanalVenda(LjvCanalVenda entity)
	    {



	
	    }

	    
	    [Insert()]
	    //Insert LjvCanalVenda.
	    public void InsertLjvCanalVenda(LjvCanalVenda entity)
	    {



	
	    }

	    
	    [Delete()]
	    //Delete LjvCanalVenda.
	    public void DeleteLjvCanalVenda(LjvCanalVenda entity)
	    {



	
	    }
		
		
	    #endregion Update Methods Definitions.
		

	
	    		
	}
		
	
}