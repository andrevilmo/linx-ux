

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linx.Data;
using Linx.Tools;
using System.Data.Entity.Core.Objects;
using System.ComponentModel;
using System.Data.Common;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Linq.Expressions;
using Linx.LinqExtensions.Query;
using Linx.LinqExtensions.Functional;
using Linx.LinqExtensions.Expressions;
using System.Data.Linq.SqlClient;
using System.Reflection;
using System.Data.Entity.Core.Objects.DataClasses;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

using Linx.Operacional.BM;

namespace Linx.Dashboard.DashboardFinanceiro
{
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up LJV_LOJA];DisplayName[Look Up LJV_LOJA];Height[0];Width[0];Entities[LJV_LOJA:IdLoja];EdmEntityName[LJV_LOJA]")]	

	public partial class LookUpLjvLoja 
	{
		
	    #region Data Properties	
	 


	    private System.String _CodLoja;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cod Loja", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(20)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[LJV_LOJA.COD_LOJA]")]
	    public System.String CodLoja
	    {
	    	    get
	    	    {
	    	          return _CodLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodLoja != value)
	    	          {
	    	              this._CodLoja = value;
	    	          }
	    	    }
	    }

	    private System.String _DescLoja;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Desc Loja", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(60)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[LJV_LOJA.DESC_LOJA]")]
	    public System.String DescLoja
	    {
	    	    get
	    	    {
	    	          return _DescLoja;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescLoja != value)
	    	          {
	    	              this._DescLoja = value;
	    	          }
	    	    }
	    }

	    private Int32 _IdLoja;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Loja", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[LJV_LOJA.ID_LOJA]")]
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
	    	              this._IdLoja = value;
	    	          }
	    	    }
	    }

	    private System.String _CodBandeiraRede;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cod Bandeira Rede", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(25)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[LJV_LOJA.TBC_BANDEIRA_REDE.COD_BANDEIRA_REDE]")]
	    public System.String CodBandeiraRede
	    {
	    	    get
	    	    {
	    	          return _CodBandeiraRede;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodBandeiraRede != value)
	    	          {
	    	              this._CodBandeiraRede = value;
	    	          }
	    	    }
	    }

	    private System.String _DescBandeiraRede;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Desc Bandeira Rede", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(60)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[LJV_LOJA.TBC_BANDEIRA_REDE.DESC_BANDEIRA_REDE]")]
	    public System.String DescBandeiraRede
	    {
	    	    get
	    	    {
	    	          return _DescBandeiraRede;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescBandeiraRede != value)
	    	          {
	    	              this._DescBandeiraRede = value;
	    	          }
	    	    }
	    }

	    private System.Nullable<Int32> _IdBandeiraRede;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Bandeira Rede", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[LJV_LOJA.TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE]")]
	    public System.Nullable<Int32> IdBandeiraRede
	    {
	    	    get
	    	    {
	    	          return _IdBandeiraRede;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdBandeiraRede != value)
	    	          {
	    	              this._IdBandeiraRede = value;
	    	          }
	    	    }
	    }	

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up TBC_FILIAL];DisplayName[Look Up TBC_FILIAL];Height[0];Width[0];EdmEntityName[TBC_FILIAL]")]	

	public partial class LookUpTbcFilial 
	{
		
	    #region Data Properties	
	 


	    private System.Nullable<Int32> _IdFilialPfj;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Filial Pfj", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TBC_FILIAL.ID_FILIAL_PFJ]")]
	    public System.Nullable<Int32> IdFilialPfj
	    {
	    	    get
	    	    {
	    	          return _IdFilialPfj;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdFilialPfj != value)
	    	          {
	    	              this._IdFilialPfj = value;
	    	          }
	    	    }
	    }

	    private System.String _CodigoFilial;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Codigo Filial", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(18)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TBC_FILIAL.CODIGO_FILIAL]")]
	    public System.String CodigoFilial
	    {
	    	    get
	    	    {
	    	          return _CodigoFilial;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodigoFilial != value)
	    	          {
	    	              this._CodigoFilial = value;
	    	          }
	    	    }
	    }	

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up TBC_GRUPO_ECONOMICO];DisplayName[Look Up TBC_GRUPO_ECONOMICO];Height[0];Width[0];Entities[TBC_GRUPO_ECONOMICO:IdGpecon];EdmEntityName[TBC_GRUPO_ECONOMICO]")]	

	public partial class LookUpTbcGrupoEconomico 
	{
		
	    #region Data Properties	
	 


	    private Int32 _IdGpecon;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Gpecon", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TBC_GRUPO_ECONOMICO.ID_GPECON]")]
	    public Int32 IdGpecon
	    {
	    	    get
	    	    {
	    	          return _IdGpecon;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdGpecon != value)
	    	          {
	    	              this._IdGpecon = value;
	    	          }
	    	    }
	    }	

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up LJV_ATENDIMENTO];DisplayName[Look Up LJV_ATENDIMENTO];Height[0];Width[0];EdmEntityName[LJV_ATENDIMENTO]")]	

	public partial class LookUpLjvAtendimento 
	{
		
	    #region Data Properties	
	 


	    private System.DateTime _DataAtendimento;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Data Atendimento", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[LJV_ATENDIMENTO.DATA_ATENDIMENTO]")]
	    public System.DateTime DataAtendimento
	    {
	    	    get
	    	    {
	    	          return _DataAtendimento;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataAtendimento != value)
	    	          {
	    	              this._DataAtendimento = value;
	    	          }
	    	    }
	    }

	    private Int64 _IdAtendimento;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Qtde. Atendimento", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[LJV_ATENDIMENTO.ID_ATENDIMENTO]")]
	    public Int64 IdAtendimento
	    {
	    	    get
	    	    {
	    	          return _IdAtendimento;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdAtendimento != value)
	    	          {
	    	              this._IdAtendimento = value;
	    	          }
	    	    }
	    }

	    private System.String _CodBandeiraRede;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Cod Bandeira Rede", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(25)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[LJV_ATENDIMENTO.LJV_LOJA.TBC_BANDEIRA_REDE.COD_BANDEIRA_REDE]")]
	    public System.String CodBandeiraRede
	    {
	    	    get
	    	    {
	    	          return _CodBandeiraRede;
	    	    }
	    	    set
	    	    {
	    	          if (this._CodBandeiraRede != value)
	    	          {
	    	              this._CodBandeiraRede = value;
	    	          }
	    	    }
	    }

	    private System.String _DescBandeiraRede;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Desc Bandeira Rede", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(60)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[LJV_ATENDIMENTO.LJV_LOJA.TBC_BANDEIRA_REDE.DESC_BANDEIRA_REDE]")]
	    public System.String DescBandeiraRede
	    {
	    	    get
	    	    {
	    	          return _DescBandeiraRede;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescBandeiraRede != value)
	    	          {
	    	              this._DescBandeiraRede = value;
	    	          }
	    	    }
	    }

	    private System.Nullable<Int32> _IdBandeiraRede;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Bandeira Rede", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[LJV_ATENDIMENTO.LJV_LOJA.TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE]")]
	    public System.Nullable<Int32> IdBandeiraRede
	    {
	    	    get
	    	    {
	    	          return _IdBandeiraRede;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdBandeiraRede != value)
	    	          {
	    	              this._IdBandeiraRede = value;
	    	          }
	    	    }
	    }

	    private System.Nullable<System.Decimal> _ValorCupomFiscal;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Valor Cupom Fiscal", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[LJV_ATENDIMENTO.VALOR_CUPOM_FISCAL]")]
	    public System.Nullable<System.Decimal> ValorCupomFiscal
	    {
	    	    get
	    	    {
	    	          return _ValorCupomFiscal;
	    	    }
	    	    set
	    	    {
	    	          if (this._ValorCupomFiscal != value)
	    	          {
	    	              this._ValorCupomFiscal = value;
	    	          }
	    	    }
	    }	

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up LJV_VENDEDOR];DisplayName[Look Up LJV_VENDEDOR];Height[0];Width[0];EdmEntityName[LJV_VENDEDOR]")]	

	public partial class LookUpLjvVendedor 
	{
		
	    #region Data Properties	
	 


	    private System.String _NomeVendedor;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Vendedor", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(120)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[LJV_VENDEDOR.NOME_VENDEDOR]")]
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
	    	              this._NomeVendedor = value;
	    	          }
	    	    }
	    }

	    private System.String _VendedorApelido;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Vendedor Apelido", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(40)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[LJV_VENDEDOR.VENDEDOR_APELIDO]")]
	    public System.String VendedorApelido
	    {
	    	    get
	    	    {
	    	          return _VendedorApelido;
	    	    }
	    	    set
	    	    {
	    	          if (this._VendedorApelido != value)
	    	          {
	    	              this._VendedorApelido = value;
	    	          }
	    	    }
	    }	

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	

}