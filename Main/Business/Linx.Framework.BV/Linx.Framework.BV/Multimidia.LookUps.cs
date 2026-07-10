

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

using Linx.Framework.ControleSistema.BM;

namespace Linx.Framework.BV.Multimidia
{
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up DOC_MULTIMIDIA];DisplayName[Look Up DOC_MULTIMIDIA];Height[0];Width[0];EdmEntityName[DOC_MULTIMIDIA]")]	

	public partial class LookUpDocMultimidia 
	{
		
	    #region Data Properties	
	 


	    private Byte[] _Conteudo;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Conteudo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA.CONTEUDO]")]
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

	    private System.String _DescDocClassificador;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "DescDocClassificador", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(60)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA.DOC_CLASSIFICADOR.DESC_DOC_CLASSIFICADOR]")]
	    public System.String DescDocClassificador
	    {
	    	    get
	    	    {
	    	          return _DescDocClassificador;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescDocClassificador != value)
	    	          {
	    	              this._DescDocClassificador = value;
	    	          }
	    	    }
	    }

	    private System.String _DescDocumento;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "DescDocumento", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(60)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA.DESC_DOCUMENTO]")]
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

	    private Int64 _IdDocClassificador;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "IdDocClassificador", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA.DOC_CLASSIFICADOR.ID_DOC_CLASSIFICADOR]")]
	    public Int64 IdDocClassificador
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

	    private Byte _LxTipoDocumento;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "LxTipoDocumento", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA.LX_TIPO_DOCUMENTO]")]
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
	    [Display(Name = "LxTipoExtensao", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA.LX_TIPO_EXTENSAO]")]
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

	    private System.String _Obs;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Obs", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA.OBS]")]
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

	    private Byte[] _Thumbnail;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Thumbnail", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA.THUMBNAIL]")]
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

	    private System.Guid _UidDocumento;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "UidDocumento", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA.UID_DOCUMENTO]")]
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
	    [Display(Name = "Url", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(500)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA.URL]")]
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
	    [Display(Name = "XmlMapeamento", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA.XML_MAPEAMENTO]")]
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

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up DOC_MULTIMIDIA];DisplayName[Look Up DOC_MULTIMIDIA];Height[0];Width[0];Entities[DOC_MULTIMIDIA:UidDocumento];EdmEntityName[DOC_MULTIMIDIA]")]	

	public partial class LookUpDocMultimidiaCompact 
	{
		
	    #region Data Properties	
	 


	    private Byte[] _Conteudo;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Conteudo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA.CONTEUDO]")]
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

	    private System.String _DescDocumento;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "DescDocumento", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(60)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA.DESC_DOCUMENTO]")]
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

	    private Byte[] _Thumbnail;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Thumbnail", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA.THUMBNAIL]")]
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

	    private System.Guid _UidDocumento;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "UidDocumento", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA.UID_DOCUMENTO]")]
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
	    [Display(Name = "Url", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(500)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA.URL]")]
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

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up DOC_MULTIMIDIA];DisplayName[Look Up DOC_MULTIMIDIA];Height[0];Width[0];EdmEntityName[DOC_MULTIMIDIA]")]	

	public partial class LookUpDocMultimidiaCompact2 
	{
		
	    #region Data Properties	
	 


	    private Byte[] _Conteudo;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Conteudo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA.CONTEUDO]")]
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

	    private System.String _DescDocumento;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "DescDocumento", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(60)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA.DESC_DOCUMENTO]")]
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

	    private Int64 _IdDocClassificador;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Doc Classificador", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA.DOC_CLASSIFICADOR.ID_DOC_CLASSIFICADOR]")]
	    public Int64 IdDocClassificador
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

	    private Byte _LxTipoDocumento;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Lx Tipo Documento", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA.LX_TIPO_DOCUMENTO]")]
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
	    [Display(Name = "Lx Tipo Extensao", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA.LX_TIPO_EXTENSAO]")]
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

	    private System.String _Obs;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Obs", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA.OBS]")]
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

	    private Byte[] _Thumbnail;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Thumbnail", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA.THUMBNAIL]")]
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

	    private System.Guid _UidDocumento;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Uid Documento", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA.UID_DOCUMENTO]")]
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
	    [Display(Name = "Url", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(500)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA.URL]")]
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
	    [Display(Name = "XmlMapeamento", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_MULTIMIDIA.XML_MAPEAMENTO]")]
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

	    #endregion Data Properties	

	    #region Special Enums	
	 
	

	    #endregion Special Enums
	
	}	
	
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up DOC_CLASSIFICADOR];DisplayName[Look Up DOC_CLASSIFICADOR];Height[0];Width[0];EdmEntityName[DOC_CLASSIFICADOR]")]	

	public partial class LookUpDocClassificador 
	{
		
	    #region Data Properties	
	 


	    private Int64 _IdDocClassificador;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Doc Classificador", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_CLASSIFICADOR.ID_DOC_CLASSIFICADOR]")]
	    public Int64 IdDocClassificador
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
	
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[Look Up DOC_CLASSIFICADOR];DisplayName[Look Up DOC_CLASSIFICADOR];Height[0];Width[0];EdmEntityName[DOC_CLASSIFICADOR]")]	

	public partial class LookUpDocClassificador1 
	{
		
	    #region Data Properties	
	 


	    private System.String _DescDocClassificador;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Desc Doc Classificador", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(60)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_CLASSIFICADOR.DESC_DOC_CLASSIFICADOR]")]
	    public System.String DescDocClassificador
	    {
	    	    get
	    	    {
	    	          return _DescDocClassificador;
	    	    }
	    	    set
	    	    {
	    	          if (this._DescDocClassificador != value)
	    	          {
	    	              this._DescDocClassificador = value;
	    	          }
	    	    }
	    }

	    private Int64 _IdDocClassificador;
	    [DataMember()]
	    [Key()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Id Doc Classificador", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[DOC_CLASSIFICADOR.ID_DOC_CLASSIFICADOR]")]
	    public Int64 IdDocClassificador
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
	
	
	///////////////////////////////////////////////////////////////////////
	////////////////////////// LookUp Class Definition ////////////////////
	///////////////////////////////////////////////////////////////////////
	[DataContract(IsReference = false)]
	[Serializable()]
	[FunctionalPoint("ClassDescription[];DisplayName[];Height[0];Width[0];EdmEntityName[]")]	

	public partial class LookUpTcsAplicativo 
	{
		
	    #region Data Properties	
	 


	    private int _IdTcsAplicativo;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public int IdTcsAplicativo
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

	    private string _DescricaoAplicativo;
	    [DataMember()]
	    [XmlAttribute()]
	    [Editable(true)]
	    [Display(Name = "Aplicativo", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
	    [StringLength(250)]
	    [FunctionalPoint("IsEditable[false];ObjectClass[TextBox];FilterDataKey[]")]
	    public string DescricaoAplicativo
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
	

}