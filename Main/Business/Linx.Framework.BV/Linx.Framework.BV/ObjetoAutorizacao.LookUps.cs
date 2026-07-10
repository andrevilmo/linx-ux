

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

namespace Linx.Framework.BV.ObjetoAutorizacao
{
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up TCS_LAYOUT_AUTORIZACAO];DisplayName[Look Up TCS_LAYOUT_AUTORIZACAO];Height[0];Width[0];EdmEntityName[TCS_LAYOUT_AUTORIZACAO]")]	

	public partial class LookUpTcsLayoutAutorizacaoLista 
	{
		
	    #region Data Properties	
	 


	    private System.String _DescLayout;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Desc Layout", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(60)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_LAYOUT_AUTORIZACAO.DESC_LAYOUT]")]
	    public System.String DescLayout
	    {
	    	    get
	    	    {
	    	          return _DescLayout;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescLayout != value)
	    	          {
	    	              this._DescLayout = value;
	    	          }
	    	    }
	    }

	    private System.String _Detalhes;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Detalhes", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(500)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_LAYOUT_AUTORIZACAO.DETALHES]")]
	    public System.String Detalhes
	    {
	    	    get
	    	    {
	    	          return _Detalhes;
	    	    }
	    	    set
	    	    {
	    	          if (this._Detalhes != value)
	    	          {
	    	              this._Detalhes = value;
	    	          }
	    	    }
	    }

	    private System.String _Idioma;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Idioma", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(18)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_LAYOUT_AUTORIZACAO.IDIOMA]")]
	    public System.String Idioma
	    {
	    	    get
	    	    {
	    	          return _Idioma;
	    	    }
	    	    set
	    	    {
	    	          if (this._Idioma != value)
	    	          {
	    	              this._Idioma = value;
	    	          }
	    	    }
	    }

	    private System.Nullable<Boolean> _Inativo;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Inativo", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[CheckBox];FilterDataKey[TCS_LAYOUT_AUTORIZACAO.INATIVO]")]
	    public System.Nullable<Boolean> Inativo
	    {
	    	    get
	    	    {
	    	          return _Inativo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Inativo != value)
	    	          {
	    	              this._Inativo = value;
	    	          }
	    	    }
	    }

	    private System.Nullable<Boolean> _LayoutPadrao;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Layout Padrao", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[CheckBox];FilterDataKey[TCS_LAYOUT_AUTORIZACAO.LAYOUT_PADRAO]")]
	    public System.Nullable<Boolean> LayoutPadrao
	    {
	    	    get
	    	    {
	    	          return _LayoutPadrao;
	    	    }
	    	    set
	    	    {
	    	          if (this._LayoutPadrao != value)
	    	          {
	    	              this._LayoutPadrao = value;
	    	          }
	    	    }
	    }

	    private System.Nullable<Byte> _LxTipoLayout;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lx Tipo Layout", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_LAYOUT_AUTORIZACAO.LX_TIPO_LAYOUT]")]
	    public System.Nullable<Byte> LxTipoLayout
	    {
	    	    get
	    	    {
	    	          return _LxTipoLayout;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoLayout != value)
	    	          {
	    	              this._LxTipoLayout = value;
	    	          }
	    	    }
	    }

	    private System.Nullable<Boolean> _PossuiFiltro;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Possui Filtro", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[CheckBox];FilterDataKey[TCS_LAYOUT_AUTORIZACAO.POSSUI_FILTRO]")]
	    public System.Nullable<Boolean> PossuiFiltro
	    {
	    	    get
	    	    {
	    	          return _PossuiFiltro;
	    	    }
	    	    set
	    	    {
	    	          if (this._PossuiFiltro != value)
	    	          {
	    	              this._PossuiFiltro = value;
	    	          }
	    	    }
	    }

	    private System.Nullable<Int64> _IdObjetoConteudo;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Objeto Conteudo", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_LAYOUT_AUTORIZACAO.ID_OBJETO_CONTEUDO]")]
	    public System.Nullable<Int64> IdObjetoConteudo
	    {
	    	    get
	    	    {
	    	          return _IdObjetoConteudo;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdObjetoConteudo != value)
	    	          {
	    	              this._IdObjetoConteudo = value;
	    	          }
	    	    }
	    }

	    private System.Nullable<System.DateTime> _UltAtualizacao;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Ult Atualizacao", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_LAYOUT_AUTORIZACAO.ULT_ATUALIZACAO]")]
	    public System.Nullable<System.DateTime> UltAtualizacao
	    {
	    	    get
	    	    {
	    	          return _UltAtualizacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._UltAtualizacao != value)
	    	          {
	    	              this._UltAtualizacao = value;
	    	          }
	    	    }
	    }

	    private System.Nullable<Int64> _IdLayout;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Objeto Conteudo", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[TCS_LAYOUT_AUTORIZACAO.ID_OBJETO_CONTEUDO]")]
	    public System.Nullable<Int64> IdLayout
	    {
	    	    get
	    	    {
	    	          return _IdLayout;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdLayout != value)
	    	          {
	    	              this._IdLayout = value;
	    	          }
	    	    }
	    }	

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	

}