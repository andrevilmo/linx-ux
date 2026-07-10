

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
using System.Linq.Expressions;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

using Linx.Framework.Autorizacao.BM;

namespace Linx.Framework.BV.ParametroAutorizacao
{
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up TCS_PARAMETRO_GRUPO_AUTORIZACAO];DisplayName[Look Up TCS_PARAMETRO_GRUPO_AUTORIZACAO];Height[0];Width[0];EdmEntityName[TCS_PARAMETRO_GRUPO_AUTORIZACAO]")]	

	public partial class LookUpTcsParametroGrupoAutorizacao 
	{
		
	    #region Data Properties	
	 


	    private System.String _DescGrupoParametro;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição Grupo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(60)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_PARAMETRO_GRUPO_AUTORIZACAO.DESC_GRUPO_PARAMETRO]")]
	    public System.String DescGrupoParametro
	    {
	    	    get
	    	    {
	    	          return _DescGrupoParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescGrupoParametro != value)
	    	          {
	    	              this._DescGrupoParametro = value;
	    	          }
	    	    }
	    }

	    private Int16 _IdGrupoParametro;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Grupo", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_PARAMETRO_GRUPO_AUTORIZACAO.ID_GRUPO_PARAMETRO]")]
	    public Int16 IdGrupoParametro
	    {
	    	    get
	    	    {
	    	          return _IdGrupoParametro;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdGrupoParametro != value)
	    	          {
	    	              this._IdGrupoParametro = value;
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
	[FunctionalPoint("ClassDescription[Look Up TCS_APLICATIVO];DisplayName[Look Up TCS_APLICATIVO];Height[0];Width[0];Entities[TCS_APLICATIVO:IdTcsAplicativo];EdmEntityName[TCS_APLICATIVO]")]	

	public partial class LookUpTcsAplicativo 
	{
		
	    #region Data Properties	
	 


	    private Int32 _IdTcsAplicativo;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicativo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_APLICATIVO.ID_TCS_APLICATIVO]")]
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
	    	              this._IdTcsAplicativo = value;
	    	          }
	    	    }
	    }

	    private System.String _DescricaoAplicativo;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_APLICATIVO.DESCRICAO_APLICATIVO]")]
	    public System.String DescricaoAplicativo
	    {
	    	    get
	    	    {
	    	          return _DescricaoAplicativo;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescricaoAplicativo != value)
	    	          {
	    	              this._DescricaoAplicativo = value;
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
	[FunctionalPoint("ClassDescription[Look Up TCS_TABELA_AUTORIZACAO];DisplayName[Look Up TCS_TABELA_AUTORIZACAO];Height[0];Width[0];Entities[TCS_TABELA_AUTORIZACAO:UidTabela];EdmEntityName[TCS_TABELA_AUTORIZACAO]")]	

	public partial class LookUpTcsTabelaAutorizacaoSelecao 
	{
		
	    #region Data Properties	
	 


	    private System.String _NomeTabela;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Tabela", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_TABELA_AUTORIZACAO.NOME_TABELA]")]
	    public System.String NomeTabela
	    {
	    	    get
	    	    {
	    	          return _NomeTabela;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeTabela != value)
	    	          {
	    	              this._NomeTabela = value;
	    	          }
	    	    }
	    }

	    private System.String _DescTabela;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Descrição", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(80)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_TABELA_AUTORIZACAO.DESC_TABELA]")]
	    public System.String DescTabela
	    {
	    	    get
	    	    {
	    	          return _DescTabela;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescTabela != value)
	    	          {
	    	              this._DescTabela = value;
	    	          }
	    	    }
	    }

	    private System.Guid _UidTabela;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Tabela", Description="", Order = 2, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_TABELA_AUTORIZACAO.UID_TABELA]")]
	    public System.Guid UidTabela
	    {
	    	    get
	    	    {
	    	          return _UidTabela;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidTabela != value)
	    	          {
	    	              this._UidTabela = value;
	    	          }
	    	    }
	    }	

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	

}