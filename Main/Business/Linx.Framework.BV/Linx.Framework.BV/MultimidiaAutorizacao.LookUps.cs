

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

namespace Linx.Framework.BV.MultimidiaAutorizacao
{
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up DOC_MULTIMIDIA_AUTORIZACAO];DisplayName[Look Up DOC_MULTIMIDIA_AUTORIZACAO];Height[0];Width[0];Entities[DOC_MULTIMIDIA_AUTORIZACAO:UidDocumento];EdmEntityName[DOC_MULTIMIDIA_AUTORIZACAO]")]	

	public partial class LookUpDocMultimidiaAutorizacao 
	{
		
	    #region Data Properties	
	 


	    private Byte[] _Conteudo;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Conteudo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA_AUTORIZACAO.CONTEUDO]")]
	    public Byte[] Conteudo
	    {
	    	    get
	    	    {
	    	          return _Conteudo;
	    	    }
	    	    set
	    	    {
	    	          if (this._Conteudo != value)
	    	          {
	    	              this._Conteudo = value;
	    	          }
	    	    }
	    }

	    private System.DateTime _DataCriacao;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Data Criacao", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA_AUTORIZACAO.DATA_CRIACAO]")]
	    public System.DateTime DataCriacao
	    {
	    	    get
	    	    {
	    	          return _DataCriacao;
	    	    }
	    	    set
	    	    {
	    	          if (this._DataCriacao != value)
	    	          {
	    	              this._DataCriacao = value;
	    	          }
	    	    }
	    }

	    private System.String _DescDocumento;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Desc Documento", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(60)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA_AUTORIZACAO.DESC_DOCUMENTO]")]
	    public System.String DescDocumento
	    {
	    	    get
	    	    {
	    	          return _DescDocumento;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescDocumento != value)
	    	          {
	    	              this._DescDocumento = value;
	    	          }
	    	    }
	    }

	    private Int32 _IdDocClassificadorFk;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Doc Classificador Fk", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA_AUTORIZACAO.ID_DOC_CLASSIFICADOR_FK]")]
	    public Int32 IdDocClassificadorFk
	    {
	    	    get
	    	    {
	    	          return _IdDocClassificadorFk;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdDocClassificadorFk != value)
	    	          {
	    	              this._IdDocClassificadorFk = value;
	    	          }
	    	    }
	    }

	    private Byte _LxTipoDocumento;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lx Tipo Documento", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA_AUTORIZACAO.LX_TIPO_DOCUMENTO]")]
	    public Byte LxTipoDocumento
	    {
	    	    get
	    	    {
	    	          return _LxTipoDocumento;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoDocumento != value)
	    	          {
	    	              this._LxTipoDocumento = value;
	    	          }
	    	    }
	    }

	    private Byte _LxTipoExtensao;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lx Tipo Extensao", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA_AUTORIZACAO.LX_TIPO_EXTENSAO]")]
	    public Byte LxTipoExtensao
	    {
	    	    get
	    	    {
	    	          return _LxTipoExtensao;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoExtensao != value)
	    	          {
	    	              this._LxTipoExtensao = value;
	    	          }
	    	    }
	    }

	    private Byte _LxTipoMidia;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lx Tipo Midia", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA_AUTORIZACAO.LX_TIPO_MIDIA]")]
	    public Byte LxTipoMidia
	    {
	    	    get
	    	    {
	    	          return _LxTipoMidia;
	    	    }
	    	    set
	    	    {
	    	          if (this._LxTipoMidia != value)
	    	          {
	    	              this._LxTipoMidia = value;
	    	          }
	    	    }
	    }

	    private System.String _NomeArquivo;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Nome Arquivo", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(150)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA_AUTORIZACAO.NOME_ARQUIVO]")]
	    public System.String NomeArquivo
	    {
	    	    get
	    	    {
	    	          return _NomeArquivo;
	    	    }
	    	    set
	    	    {
	    	          if (this._NomeArquivo != value)
	    	          {
	    	              this._NomeArquivo = value;
	    	          }
	    	    }
	    }

	    private System.String _Obs;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Obs", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA_AUTORIZACAO.OBS]")]
	    public System.String Obs
	    {
	    	    get
	    	    {
	    	          return _Obs;
	    	    }
	    	    set
	    	    {
	    	          if (this._Obs != value)
	    	          {
	    	              this._Obs = value;
	    	          }
	    	    }
	    }

	    private System.Nullable<System.Int32> _TamanhoMidia;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tamanho Midia", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA_AUTORIZACAO.TAMANHO_MIDIA]")]
	    public System.Nullable<System.Int32> TamanhoMidia
	    {
	    	    get
	    	    {
	    	          return _TamanhoMidia;
	    	    }
	    	    set
	    	    {
	    	          if (this._TamanhoMidia != value)
	    	          {
	    	              this._TamanhoMidia = value;
	    	          }
	    	    }
	    }

	    private Byte[] _Thumbnail;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Thumbnail", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA_AUTORIZACAO.THUMBNAIL]")]
	    public Byte[] Thumbnail
	    {
	    	    get
	    	    {
	    	          return _Thumbnail;
	    	    }
	    	    set
	    	    {
	    	          if (this._Thumbnail != value)
	    	          {
	    	              this._Thumbnail = value;
	    	          }
	    	    }
	    }

	    private System.String _TipoConteudoHttp;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Tipo Conteudo Http", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(100)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA_AUTORIZACAO.TIPO_CONTEUDO_HTTP]")]
	    public System.String TipoConteudoHttp
	    {
	    	    get
	    	    {
	    	          return _TipoConteudoHttp;
	    	    }
	    	    set
	    	    {
	    	          if (this._TipoConteudoHttp != value)
	    	          {
	    	              this._TipoConteudoHttp = value;
	    	          }
	    	    }
	    }

	    private System.Guid _UidDocumento;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Documento", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA_AUTORIZACAO.UID_DOCUMENTO]")]
	    public System.Guid UidDocumento
	    {
	    	    get
	    	    {
	    	          return _UidDocumento;
	    	    }
	    	    set
	    	    {
	    	          if (this._UidDocumento != value)
	    	          {
	    	              this._UidDocumento = value;
	    	          }
	    	    }
	    }

	    private System.String _Url;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Url", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(500)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA_AUTORIZACAO.URL]")]
	    public System.String Url
	    {
	    	    get
	    	    {
	    	          return _Url;
	    	    }
	    	    set
	    	    {
	    	          if (this._Url != value)
	    	          {
	    	              this._Url = value;
	    	          }
	    	    }
	    }

	    private System.String _XmlMapeamento;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Xml Mapeamento", Description="", Order = 14, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA_AUTORIZACAO.XML_MAPEAMENTO]")]
	    public System.String XmlMapeamento
	    {
	    	    get
	    	    {
	    	          return _XmlMapeamento;
	    	    }
	    	    set
	    	    {
	    	          if (this._XmlMapeamento != value)
	    	          {
	    	              this._XmlMapeamento = value;
	    	          }
	    	    }
	    }

	    private Int32 _IdDocClassificador;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Doc Classificador Fk", Description="", Order = 15, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA_AUTORIZACAO.ID_DOC_CLASSIFICADOR_FK]")]
	    public Int32 IdDocClassificador
	    {
	    	    get
	    	    {
	    	          return _IdDocClassificador;
	    	    }
	    	    set
	    	    {
	    	          if (this._IdDocClassificador != value)
	    	          {
	    	              this._IdDocClassificador = value;
	    	          }
	    	    }
	    }	

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	

}