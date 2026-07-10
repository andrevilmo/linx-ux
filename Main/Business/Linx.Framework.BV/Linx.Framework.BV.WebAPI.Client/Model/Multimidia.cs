using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linx.Tools;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Linx.Framework.BV.Multimidia
{
    
    [DataContract(IsReference = false, Name = "DocMultimidiaTabela")]
    [Serializable()]
    public partial class DocMultimidiaTabela
    {
    

        private Byte[] _Conteudo;

        [DataMember(Name = "Conteudo", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Conteudo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="Byte[]#Conteudo#false##30##Conteudo#0#true##::LookUpDocMultimidia##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#DescDocClassificador,IdDocClassificador[DescDocClassificador,IdDocClassificador]#Conteudo[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];DescDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoExtensao[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Obs[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Thumbnail[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];UidDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Url[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];XmlMapeamento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.CONTEUDO")]
        public Byte[] Conteudo
        {
                get
                {
                      return _Conteudo;
                }
                set
                {
                      this._Conteudo = value;
                }
        }

        private System.DateTime _DataCriacao;

        [DataMember(Name = "DataCriacao", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Data Criacao", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.DATA_CRIACAO")]
        public System.DateTime DataCriacao
        {
                get
                {
                      return _DataCriacao;
                }
                set
                {
                      this._DataCriacao = value;
                }
        }

        private System.String _DescDocClassificador;

        [DataMember(Name = "DescDocClassificador", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "DescDocClassificador", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescDocClassificador#false##600##DescDocClassificador#1#true##::LookUpDocMultimidia##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#DescDocClassificador,IdDocClassificador[DescDocClassificador,IdDocClassificador]#Conteudo[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];DescDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoExtensao[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Obs[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Thumbnail[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];UidDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Url[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];XmlMapeamento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.DOC_CLASSIFICADOR.DESC_DOC_CLASSIFICADOR")]
        public System.String DescDocClassificador
        {
                get
                {
                      return _DescDocClassificador;
                }
                set
                {
                      this._DescDocClassificador = value;
                }
        }

        private System.String _DescDocumento;

        [DataMember(Name = "DescDocumento", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "DescDocumento", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescDocumento#false##600##DescDocumento#2#true##::LookUpDocMultimidia##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#DescDocClassificador,IdDocClassificador[DescDocClassificador,IdDocClassificador]#Conteudo[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];DescDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoExtensao[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Obs[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Thumbnail[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];UidDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Url[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];XmlMapeamento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.DESC_DOCUMENTO")]
        public System.String DescDocumento
        {
                get
                {
                      return _DescDocumento;
                }
                set
                {
                      this._DescDocumento = value;
                }
        }

        private Int64 _IdChave;

        [DataMember(Name = "IdChave", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "IdChave", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [Key()]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.ID_CHAVE")]
        public Int64 IdChave
        {
                get
                {
                      return _IdChave;
                }
                set
                {
                      this._IdChave = value;
                }
        }

        private Int64 _IdDocClassificador;

        [DataMember(Name = "IdDocClassificador", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "IdDocClassificador", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdDocClassificador#true##24:0##IdDocClassificador#3#true##::LookUpDocMultimidia##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#DescDocClassificador,IdDocClassificador[DescDocClassificador,IdDocClassificador]#Conteudo[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];DescDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoExtensao[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Obs[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Thumbnail[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];UidDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Url[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];XmlMapeamento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.DOC_CLASSIFICADOR.ID_DOC_CLASSIFICADOR")]
        public Int64 IdDocClassificador
        {
                get
                {
                      return _IdDocClassificador;
                }
                set
                {
                      this._IdDocClassificador = value;
                }
        }

        private Byte _LxTipoDocumento;

        [DataMember(Name = "LxTipoDocumento", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "LxTipoDocumento", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="Byte#LxTipoDocumento#false##30##LxTipoDocumento#4#true##::LookUpDocMultimidia##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#DescDocClassificador,IdDocClassificador[DescDocClassificador,IdDocClassificador]#Conteudo[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];DescDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoExtensao[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Obs[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Thumbnail[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];UidDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Url[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];XmlMapeamento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.LX_TIPO_DOCUMENTO")]
        public Byte LxTipoDocumento
        {
                get
                {
                      return _LxTipoDocumento;
                }
                set
                {
                      this._LxTipoDocumento = value;
                }
        }

        private Byte _LxTipoExtensao;

        [DataMember(Name = "LxTipoExtensao", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "LxTipoExtensao", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="Byte#LxTipoExtensao#false##30##LxTipoExtensao#5#true##::LookUpDocMultimidia##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#DescDocClassificador,IdDocClassificador[DescDocClassificador,IdDocClassificador]#Conteudo[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];DescDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoExtensao[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Obs[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Thumbnail[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];UidDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Url[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];XmlMapeamento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.LX_TIPO_EXTENSAO")]
        public Byte LxTipoExtensao
        {
                get
                {
                      return _LxTipoExtensao;
                }
                set
                {
                      this._LxTipoExtensao = value;
                }
        }

        private Byte _LxTipoMidia;

        [DataMember(Name = "LxTipoMidia", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Lx Tipo Midia", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.LX_TIPO_MIDIA")]
        public Byte LxTipoMidia
        {
                get
                {
                      return _LxTipoMidia;
                }
                set
                {
                      this._LxTipoMidia = value;
                }
        }

        private System.String _NomeArquivo;

        [DataMember(Name = "NomeArquivo", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Nome Arquivo", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.NOME_ARQUIVO")]
        public System.String NomeArquivo
        {
                get
                {
                      return _NomeArquivo;
                }
                set
                {
                      this._NomeArquivo = value;
                }
        }

        private System.String _Obs;

        [DataMember(Name = "Obs", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Obs", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#Obs#false##0##Obs#6#true##::LookUpDocMultimidia##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#DescDocClassificador,IdDocClassificador[DescDocClassificador,IdDocClassificador]#Conteudo[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];DescDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoExtensao[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Obs[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Thumbnail[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];UidDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Url[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];XmlMapeamento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.OBS")]
        public System.String Obs
        {
                get
                {
                      return _Obs;
                }
                set
                {
                      this._Obs = value;
                }
        }

        private Int16 _OrdemApresentacao;

        [DataMember(Name = "OrdemApresentacao", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "OrdemApresentacao", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.ORDEM_APRESENTACAO")]
        public Int16 OrdemApresentacao
        {
                get
                {
                      return _OrdemApresentacao;
                }
                set
                {
                      this._OrdemApresentacao = value;
                }
        }

        private System.Nullable<System.Int32> _TamanhoMidia;

        [DataMember(Name = "TamanhoMidia", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Tamanho Midia", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.TAMANHO_MIDIA")]
        public System.Nullable<System.Int32> TamanhoMidia
        {
                get
                {
                      return _TamanhoMidia;
                }
                set
                {
                      this._TamanhoMidia = value;
                }
        }

        private Byte[] _Thumbnail;

        [DataMember(Name = "Thumbnail", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Thumbnail", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="Byte[]#Thumbnail#false##30##Thumbnail#7#true##::LookUpDocMultimidia##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#DescDocClassificador,IdDocClassificador[DescDocClassificador,IdDocClassificador]#Conteudo[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];DescDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoExtensao[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Obs[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Thumbnail[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];UidDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Url[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];XmlMapeamento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.THUMBNAIL")]
        public Byte[] Thumbnail
        {
                get
                {
                      return _Thumbnail;
                }
                set
                {
                      this._Thumbnail = value;
                }
        }

        private System.String _TipoConteudoHttp;

        [DataMember(Name = "TipoConteudoHttp", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Tipo Conteudo Http", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.TIPO_CONTEUDO_HTTP")]
        public System.String TipoConteudoHttp
        {
                get
                {
                      return _TipoConteudoHttp;
                }
                set
                {
                      this._TipoConteudoHttp = value;
                }
        }

        private System.Guid _UidChave;

        [DataMember(Name = "UidChave", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "UidChave", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
        [Key()]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.UID_CHAVE")]
        public System.Guid UidChave
        {
                get
                {
                      return _UidChave;
                }
                set
                {
                      this._UidChave = value;
                }
        }

        private System.Guid _UidDocumento;

        [DataMember(Name = "UidDocumento", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "UidDocumento", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
        [Key()]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Guid#UidDocumento#true##12:0##UidDocumento#8#true##::LookUpDocMultimidia##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#DescDocClassificador,IdDocClassificador[DescDocClassificador,IdDocClassificador]#Conteudo[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];DescDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoExtensao[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Obs[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Thumbnail[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];UidDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Url[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];XmlMapeamento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.UID_DOCUMENTO")]
        public System.Guid UidDocumento
        {
                get
                {
                      return _UidDocumento;
                }
                set
                {
                      this._UidDocumento = value;
                }
        }

        private System.Guid _UidTabela;

        [DataMember(Name = "UidTabela", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Uid Tabela", Description="", Order = 5, AutoGenerateField = false, GroupName="", ResourceType= null)]
        [Key()]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.UID_TABELA")]
        public System.Guid UidTabela
        {
                get
                {
                      return _UidTabela;
                }
                set
                {
                      this._UidTabela = value;
                }
        }

        private System.String _Url;

        [DataMember(Name = "Url", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Url", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#Url#false##5000##Url#9#true##::LookUpDocMultimidia##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#DescDocClassificador,IdDocClassificador[DescDocClassificador,IdDocClassificador]#Conteudo[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];DescDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoExtensao[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Obs[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Thumbnail[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];UidDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Url[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];XmlMapeamento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.URL")]
        public System.String Url
        {
                get
                {
                      return _Url;
                }
                set
                {
                      this._Url = value;
                }
        }

        private System.String _XmlMapeamento;

        [DataMember(Name = "XmlMapeamento", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "XmlMapeamento", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#XmlMapeamento#false##0##XmlMapeamento#10#true##::LookUpDocMultimidia##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#DescDocClassificador,IdDocClassificador[DescDocClassificador,IdDocClassificador]#Conteudo[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];DescDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];LxTipoExtensao[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Obs[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Thumbnail[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];UidDocumento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];Url[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador];XmlMapeamento[DescDocClassificador=DescDocClassificador,IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.XML_MAPEAMENTO")]
        public System.String XmlMapeamento
        {
                get
                {
                      return _XmlMapeamento;
                }
                set
                {
                      this._XmlMapeamento = value;
                }
        }
    }
    
    [DataContract(IsReference = false, Name = "DocMultimidiaCompact")]
    [Serializable()]
    public partial class DocMultimidiaCompact
    {
    

        private Byte[] _Conteudo;

        [DataMember(Name = "Conteudo", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Conteudo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="Byte[]#Conteudo#false##30##Conteudo#0#true##::LookUpDocMultimidiaCompact##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable###true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.CONTEUDO")]
        public Byte[] Conteudo
        {
                get
                {
                      return _Conteudo;
                }
                set
                {
                      this._Conteudo = value;
                }
        }

        private System.String _DescDocumento;

        [DataMember(Name = "DescDocumento", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "DescDocumento", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescDocumento#false##600##DescDocumento#1#true##::LookUpDocMultimidiaCompact##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable###true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.DESC_DOCUMENTO")]
        public System.String DescDocumento
        {
                get
                {
                      return _DescDocumento;
                }
                set
                {
                      this._DescDocumento = value;
                }
        }

        private Int64 _IdChave;

        [DataMember(Name = "IdChave", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "IdChave", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [Key()]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.ID_CHAVE")]
        public Int64 IdChave
        {
                get
                {
                      return _IdChave;
                }
                set
                {
                      this._IdChave = value;
                }
        }

        private Byte[] _Thumbnail;

        [DataMember(Name = "Thumbnail", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Thumbnail", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="Byte[]#Thumbnail#false##30##Thumbnail#2#true##::LookUpDocMultimidiaCompact##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable###true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.THUMBNAIL")]
        public Byte[] Thumbnail
        {
                get
                {
                      return _Thumbnail;
                }
                set
                {
                      this._Thumbnail = value;
                }
        }

        private System.Guid _UidChave;

        [DataMember(Name = "UidChave", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "UidChave", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
        [Key()]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.UID_CHAVE")]
        public System.Guid UidChave
        {
                get
                {
                      return _UidChave;
                }
                set
                {
                      this._UidChave = value;
                }
        }

        private System.Guid _UidDocumento;

        [DataMember(Name = "UidDocumento", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "UidDocumento", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
        [Key()]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Guid#UidDocumento#true##12:0##UidDocumento#3#true##::LookUpDocMultimidiaCompact##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable###true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.UID_DOCUMENTO")]
        public System.Guid UidDocumento
        {
                get
                {
                      return _UidDocumento;
                }
                set
                {
                      this._UidDocumento = value;
                }
        }

        private System.Guid _UidTabela;

        [DataMember(Name = "UidTabela", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Uid Tabela", Description="", Order = 5, AutoGenerateField = false, GroupName="", ResourceType= null)]
        [Key()]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.UID_TABELA")]
        public System.Guid UidTabela
        {
                get
                {
                      return _UidTabela;
                }
                set
                {
                      this._UidTabela = value;
                }
        }

        private System.String _Url;

        [DataMember(Name = "Url", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Url", Description="", Order = 13, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#Url#false##5000##Url#4#true##::LookUpDocMultimidiaCompact##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable###true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.URL")]
        public System.String Url
        {
                get
                {
                      return _Url;
                }
                set
                {
                      this._Url = value;
                }
        }
    }
    
    [DataContract(IsReference = false, Name = "MultimidiaCompact2BO")]
    [Serializable()]
    public partial class MultimidiaCompact2BO
    {
    

        private Byte[] _Conteudo;

        [DataMember(Name = "Conteudo", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Conteudo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="Byte[]#Conteudo#false##30##Conteudo#0#true##::LookUpDocMultimidiaCompact2##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#IdDocClassificador[IdDocClassificador]#Conteudo[IdDocClassificador=IdDocClassificador];DescDocumento[IdDocClassificador=IdDocClassificador];LxTipoDocumento[IdDocClassificador=IdDocClassificador];LxTipoExtensao[IdDocClassificador=IdDocClassificador];Obs[IdDocClassificador=IdDocClassificador];Thumbnail[IdDocClassificador=IdDocClassificador];UidDocumento[IdDocClassificador=IdDocClassificador];Url[IdDocClassificador=IdDocClassificador];XmlMapeamento[IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.CONTEUDO")]
        public Byte[] Conteudo
        {
                get
                {
                      return _Conteudo;
                }
                set
                {
                      this._Conteudo = value;
                }
        }

        private System.String _DescDocumento;

        [DataMember(Name = "DescDocumento", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "DescDocumento", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescDocumento#false##600##DescDocumento#1#true##::LookUpDocMultimidiaCompact2##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#IdDocClassificador[IdDocClassificador]#Conteudo[IdDocClassificador=IdDocClassificador];DescDocumento[IdDocClassificador=IdDocClassificador];LxTipoDocumento[IdDocClassificador=IdDocClassificador];LxTipoExtensao[IdDocClassificador=IdDocClassificador];Obs[IdDocClassificador=IdDocClassificador];Thumbnail[IdDocClassificador=IdDocClassificador];UidDocumento[IdDocClassificador=IdDocClassificador];Url[IdDocClassificador=IdDocClassificador];XmlMapeamento[IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.DESC_DOCUMENTO")]
        public System.String DescDocumento
        {
                get
                {
                      return _DescDocumento;
                }
                set
                {
                      this._DescDocumento = value;
                }
        }

        private Int64 _IdChave;

        [DataMember(Name = "IdChave", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "IdChave", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [Key()]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.ID_CHAVE")]
        public Int64 IdChave
        {
                get
                {
                      return _IdChave;
                }
                set
                {
                      this._IdChave = value;
                }
        }

        private Int64 _IdDocClassificador;

        [DataMember(Name = "IdDocClassificador", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Id Doc Classificador", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdDocClassificador#true##24:0##Id Doc Classificador#2#true##::LookUpDocMultimidiaCompact2##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#IdDocClassificador[IdDocClassificador]#Conteudo[IdDocClassificador=IdDocClassificador];DescDocumento[IdDocClassificador=IdDocClassificador];LxTipoDocumento[IdDocClassificador=IdDocClassificador];LxTipoExtensao[IdDocClassificador=IdDocClassificador];Obs[IdDocClassificador=IdDocClassificador];Thumbnail[IdDocClassificador=IdDocClassificador];UidDocumento[IdDocClassificador=IdDocClassificador];Url[IdDocClassificador=IdDocClassificador];XmlMapeamento[IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.DOC_CLASSIFICADOR.ID_DOC_CLASSIFICADOR")]
        public Int64 IdDocClassificador
        {
                get
                {
                      return _IdDocClassificador;
                }
                set
                {
                      this._IdDocClassificador = value;
                }
        }

        private Byte _LxTipoDocumento;

        [DataMember(Name = "LxTipoDocumento", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Lx Tipo Documento", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="Byte#LxTipoDocumento#false##30##Lx Tipo Documento#3#true##::LookUpDocMultimidiaCompact2##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#IdDocClassificador[IdDocClassificador]#Conteudo[IdDocClassificador=IdDocClassificador];DescDocumento[IdDocClassificador=IdDocClassificador];LxTipoDocumento[IdDocClassificador=IdDocClassificador];LxTipoExtensao[IdDocClassificador=IdDocClassificador];Obs[IdDocClassificador=IdDocClassificador];Thumbnail[IdDocClassificador=IdDocClassificador];UidDocumento[IdDocClassificador=IdDocClassificador];Url[IdDocClassificador=IdDocClassificador];XmlMapeamento[IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.LX_TIPO_DOCUMENTO")]
        public Byte LxTipoDocumento
        {
                get
                {
                      return _LxTipoDocumento;
                }
                set
                {
                      this._LxTipoDocumento = value;
                }
        }

        private Byte _LxTipoExtensao;

        [DataMember(Name = "LxTipoExtensao", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Lx Tipo Extensao", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="Byte#LxTipoExtensao#false##30##Lx Tipo Extensao#4#true##::LookUpDocMultimidiaCompact2##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#IdDocClassificador[IdDocClassificador]#Conteudo[IdDocClassificador=IdDocClassificador];DescDocumento[IdDocClassificador=IdDocClassificador];LxTipoDocumento[IdDocClassificador=IdDocClassificador];LxTipoExtensao[IdDocClassificador=IdDocClassificador];Obs[IdDocClassificador=IdDocClassificador];Thumbnail[IdDocClassificador=IdDocClassificador];UidDocumento[IdDocClassificador=IdDocClassificador];Url[IdDocClassificador=IdDocClassificador];XmlMapeamento[IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.LX_TIPO_EXTENSAO")]
        public Byte LxTipoExtensao
        {
                get
                {
                      return _LxTipoExtensao;
                }
                set
                {
                      this._LxTipoExtensao = value;
                }
        }

        private System.String _Obs;

        [DataMember(Name = "Obs", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Obs", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#Obs#false##0##Obs#5#true##::LookUpDocMultimidiaCompact2##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#IdDocClassificador[IdDocClassificador]#Conteudo[IdDocClassificador=IdDocClassificador];DescDocumento[IdDocClassificador=IdDocClassificador];LxTipoDocumento[IdDocClassificador=IdDocClassificador];LxTipoExtensao[IdDocClassificador=IdDocClassificador];Obs[IdDocClassificador=IdDocClassificador];Thumbnail[IdDocClassificador=IdDocClassificador];UidDocumento[IdDocClassificador=IdDocClassificador];Url[IdDocClassificador=IdDocClassificador];XmlMapeamento[IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.OBS")]
        public System.String Obs
        {
                get
                {
                      return _Obs;
                }
                set
                {
                      this._Obs = value;
                }
        }

        private Int16 _OrdemApresentacao;

        [DataMember(Name = "OrdemApresentacao", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "OrdemApresentacao", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.ORDEM_APRESENTACAO")]
        public Int16 OrdemApresentacao
        {
                get
                {
                      return _OrdemApresentacao;
                }
                set
                {
                      this._OrdemApresentacao = value;
                }
        }

        private Byte[] _Thumbnail;

        [DataMember(Name = "Thumbnail", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Thumbnail", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="Byte[]#Thumbnail#false##30##Thumbnail#6#true##::LookUpDocMultimidiaCompact2##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#IdDocClassificador[IdDocClassificador]#Conteudo[IdDocClassificador=IdDocClassificador];DescDocumento[IdDocClassificador=IdDocClassificador];LxTipoDocumento[IdDocClassificador=IdDocClassificador];LxTipoExtensao[IdDocClassificador=IdDocClassificador];Obs[IdDocClassificador=IdDocClassificador];Thumbnail[IdDocClassificador=IdDocClassificador];UidDocumento[IdDocClassificador=IdDocClassificador];Url[IdDocClassificador=IdDocClassificador];XmlMapeamento[IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.THUMBNAIL")]
        public Byte[] Thumbnail
        {
                get
                {
                      return _Thumbnail;
                }
                set
                {
                      this._Thumbnail = value;
                }
        }

        private System.Guid _UidChave;

        [DataMember(Name = "UidChave", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "UidChave", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
        [Key()]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.UID_CHAVE")]
        public System.Guid UidChave
        {
                get
                {
                      return _UidChave;
                }
                set
                {
                      this._UidChave = value;
                }
        }

        private System.Guid _UidDocumento;

        [DataMember(Name = "UidDocumento", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Uid Documento", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
        [Key()]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.Guid#UidDocumento#true##12:0##Uid Documento#7#true##::LookUpDocMultimidiaCompact2##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#IdDocClassificador[IdDocClassificador]#Conteudo[IdDocClassificador=IdDocClassificador];DescDocumento[IdDocClassificador=IdDocClassificador];LxTipoDocumento[IdDocClassificador=IdDocClassificador];LxTipoExtensao[IdDocClassificador=IdDocClassificador];Obs[IdDocClassificador=IdDocClassificador];Thumbnail[IdDocClassificador=IdDocClassificador];UidDocumento[IdDocClassificador=IdDocClassificador];Url[IdDocClassificador=IdDocClassificador];XmlMapeamento[IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.UID_DOCUMENTO")]
        public System.Guid UidDocumento
        {
                get
                {
                      return _UidDocumento;
                }
                set
                {
                      this._UidDocumento = value;
                }
        }

        private System.Guid _UidTabela;

        [DataMember(Name = "UidTabela", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Uid Tabela", Description="", Order = 5, AutoGenerateField = false, GroupName="", ResourceType= null)]
        [Key()]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.UID_TABELA")]
        public System.Guid UidTabela
        {
                get
                {
                      return _UidTabela;
                }
                set
                {
                      this._UidTabela = value;
                }
        }

        private System.String _Url;

        [DataMember(Name = "Url", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Url", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#Url#false##5000##Url#8#true##::LookUpDocMultimidiaCompact2##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#IdDocClassificador[IdDocClassificador]#Conteudo[IdDocClassificador=IdDocClassificador];DescDocumento[IdDocClassificador=IdDocClassificador];LxTipoDocumento[IdDocClassificador=IdDocClassificador];LxTipoExtensao[IdDocClassificador=IdDocClassificador];Obs[IdDocClassificador=IdDocClassificador];Thumbnail[IdDocClassificador=IdDocClassificador];UidDocumento[IdDocClassificador=IdDocClassificador];Url[IdDocClassificador=IdDocClassificador];XmlMapeamento[IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.URL")]
        public System.String Url
        {
                get
                {
                      return _Url;
                }
                set
                {
                      this._Url = value;
                }
        }

        private System.String _XmlMapeamento;

        [DataMember(Name = "XmlMapeamento", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "XmlMapeamento", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#XmlMapeamento#false##0##XmlMapeamento#9#true##::LookUpDocMultimidiaCompact2##false#false#DOC_MULTIMIDIA#DOC_MULTIMIDIA#Linx.Framework.BV.Multimidia#IQueryable#IdDocClassificador[IdDocClassificador]#Conteudo[IdDocClassificador=IdDocClassificador];DescDocumento[IdDocClassificador=IdDocClassificador];LxTipoDocumento[IdDocClassificador=IdDocClassificador];LxTipoExtensao[IdDocClassificador=IdDocClassificador];Obs[IdDocClassificador=IdDocClassificador];Thumbnail[IdDocClassificador=IdDocClassificador];UidDocumento[IdDocClassificador=IdDocClassificador];Url[IdDocClassificador=IdDocClassificador];XmlMapeamento[IdDocClassificador=IdDocClassificador]#true#false", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.XML_MAPEAMENTO")]
        public System.String XmlMapeamento
        {
                get
                {
                      return _XmlMapeamento;
                }
                set
                {
                      this._XmlMapeamento = value;
                }
        }

        [DataMember(Name = "TipoExtensao", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
        public System.String TipoExtensao
        {
                get; set;
        }

        [DataMember(Name = "DescTabela", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="\"\"")]
        public string DescTabela
        {
                get; set;
        }

        [DataMember(Name = "NomeTabela", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="\"\"")]
        public string NomeTabela
        {
                get; set;
        }
    }
    
    [DataContract(IsReference = false, Name = "DocMultimidiaUid")]
    [Serializable()]
    public partial class DocMultimidiaUid
    {
    

        private System.Guid _UidDocumento;

        [DataMember(Name = "UidDocumento", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "UidDocumento", Description="", Order = 0, AutoGenerateField = false, GroupName="", ResourceType= null)]
        [Key()]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.UID_DOCUMENTO")]
        public System.Guid UidDocumento
        {
                get
                {
                      return _UidDocumento;
                }
                set
                {
                      this._UidDocumento = value;
                }
        }
    }
    
    [DataContract(IsReference = false, Name = "DocMultimidiaInfo")]
    [Serializable()]
    public partial class DocMultimidiaInfo
    {
    

        private Guid _UidDocumento;

        [DataMember(Name = "UidDocumento", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [Key()]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
        public Guid UidDocumento
        {
                get
                {
                      return _UidDocumento;
                }
                set
                {
                      this._UidDocumento = value;
                }
        }

        private string _Url;

        [DataMember(Name = "Url", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
        public string Url
        {
                get
                {
                      return _Url;
                }
                set
                {
                      this._Url = value;
                }
        }

        private int _OrdemApresentacao;

        [DataMember(Name = "OrdemApresentacao", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
        public int OrdemApresentacao
        {
                get
                {
                      return _OrdemApresentacao;
                }
                set
                {
                      this._OrdemApresentacao = value;
                }
        }

        private byte _TipoDocumento;

        [DataMember(Name = "TipoDocumento", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
        public byte TipoDocumento
        {
                get
                {
                      return _TipoDocumento;
                }
                set
                {
                      this._TipoDocumento = value;
                }
        }

        private string _DescricaoTipoDocumento;

        [DataMember(Name = "DescricaoTipoDocumento", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
        public string DescricaoTipoDocumento
        {
                get
                {
                      return _DescricaoTipoDocumento;
                }
                set
                {
                      this._DescricaoTipoDocumento = value;
                }
        }

        private byte _TipoMidia;

        [DataMember(Name = "TipoMidia", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
        public byte TipoMidia
        {
                get
                {
                      return _TipoMidia;
                }
                set
                {
                      this._TipoMidia = value;
                }
        }

        private string _DescricaoTipoMidia;

        [DataMember(Name = "DescricaoTipoMidia", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
        public string DescricaoTipoMidia
        {
                get
                {
                      return _DescricaoTipoMidia;
                }
                set
                {
                      this._DescricaoTipoMidia = value;
                }
        }

        private string _NomeArquivo;

        [DataMember(Name = "NomeArquivo", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
        public string NomeArquivo
        {
                get
                {
                      return _NomeArquivo;
                }
                set
                {
                      this._NomeArquivo = value;
                }
        }

        private string _TipoConteudoHttp;

        [DataMember(Name = "TipoConteudoHttp", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
        public string TipoConteudoHttp
        {
                get
                {
                      return _TipoConteudoHttp;
                }
                set
                {
                      this._TipoConteudoHttp = value;
                }
        }

        private int? _TamanhoMidia;

        [DataMember(Name = "TamanhoMidia", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
        public int? TamanhoMidia
        {
                get
                {
                      return _TamanhoMidia;
                }
                set
                {
                      this._TamanhoMidia = value;
                }
        }

        private string _UrlThumbnail;

        [DataMember(Name = "UrlThumbnail", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
        public string UrlThumbnail
        {
                get
                {
                      return _UrlThumbnail;
                }
                set
                {
                      this._UrlThumbnail = value;
                }
        }

        private string _UrlServiceBus;

        [DataMember(Name = "UrlServiceBus", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
        public string UrlServiceBus
        {
                get
                {
                      return _UrlServiceBus;
                }
                set
                {
                      this._UrlServiceBus = value;
                }
        }
    }
    
    [DataContract(IsReference = false, Name = "DocMultimidia")]
    [Serializable()]
    public partial class DocMultimidia
    {
    

        private Byte[] _Conteudo;

        [DataMember(Name = "Conteudo", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Conteudo", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.CONTEUDO")]
        public Byte[] Conteudo
        {
                get
                {
                      return _Conteudo;
                }
                set
                {
                      this._Conteudo = value;
                }
        }

        private System.DateTime _DataCriacao;

        [DataMember(Name = "DataCriacao", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Data Criacao", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.DATA_CRIACAO")]
        public System.DateTime DataCriacao
        {
                get
                {
                      return _DataCriacao;
                }
                set
                {
                      this._DataCriacao = value;
                }
        }

        private System.String _DescDocClassificador;

        [DataMember(Name = "DescDocClassificador", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Desc Doc Classificador", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="System.String#DescDocClassificador#false##60:0##Desc Doc Classificador#0#true##::LookUpDocClassificador1##false#false#DOC_CLASSIFICADOR#DOC_CLASSIFICADOR#Linx.Framework.BV.Multimidia#IQueryable###true#false", EdmKey="DOC_MULTIMIDIA.DOC_CLASSIFICADOR.DESC_DOC_CLASSIFICADOR")]
        public System.String DescDocClassificador
        {
                get
                {
                      return _DescDocClassificador;
                }
                set
                {
                      this._DescDocClassificador = value;
                }
        }

        private System.String _DescDocumento;

        [DataMember(Name = "DescDocumento", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Desc Documento", Description="", Order = 11, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.DESC_DOCUMENTO")]
        public System.String DescDocumento
        {
                get
                {
                      return _DescDocumento;
                }
                set
                {
                      this._DescDocumento = value;
                }
        }

        private Int64 _IdDocClassificador;

        [DataMember(Name = "IdDocClassificador", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Id Doc Classificador", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="Int64#IdDocClassificador#true##24:0##Id Doc Classificador#1#true##::LookUpDocClassificador1##false#false#DOC_CLASSIFICADOR#DOC_CLASSIFICADOR#Linx.Framework.BV.Multimidia#IQueryable###true#false", EdmKey="DOC_MULTIMIDIA.DOC_CLASSIFICADOR.ID_DOC_CLASSIFICADOR")]
        public Int64 IdDocClassificador
        {
                get
                {
                      return _IdDocClassificador;
                }
                set
                {
                      this._IdDocClassificador = value;
                }
        }

        private Byte _LxTipoDocumento;

        [DataMember(Name = "LxTipoDocumento", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Lx Tipo Documento", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.LX_TIPO_DOCUMENTO")]
        public Byte LxTipoDocumento
        {
                get
                {
                      return _LxTipoDocumento;
                }
                set
                {
                      this._LxTipoDocumento = value;
                }
        }

        private Byte _LxTipoExtensao;

        [DataMember(Name = "LxTipoExtensao", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Lx Tipo Extensao", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.LX_TIPO_EXTENSAO")]
        public Byte LxTipoExtensao
        {
                get
                {
                      return _LxTipoExtensao;
                }
                set
                {
                      this._LxTipoExtensao = value;
                }
        }

        private Byte _LxTipoMidia;

        [DataMember(Name = "LxTipoMidia", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Lx Tipo Midia", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.LX_TIPO_MIDIA")]
        public Byte LxTipoMidia
        {
                get
                {
                      return _LxTipoMidia;
                }
                set
                {
                      this._LxTipoMidia = value;
                }
        }

        private System.String _NomeArquivo;

        [DataMember(Name = "NomeArquivo", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Nome Arquivo", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.NOME_ARQUIVO")]
        public System.String NomeArquivo
        {
                get
                {
                      return _NomeArquivo;
                }
                set
                {
                      this._NomeArquivo = value;
                }
        }

        private System.String _Obs;

        [DataMember(Name = "Obs", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Obs", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.OBS")]
        public System.String Obs
        {
                get
                {
                      return _Obs;
                }
                set
                {
                      this._Obs = value;
                }
        }

        private System.Nullable<System.Int32> _TamanhoMidia;

        [DataMember(Name = "TamanhoMidia", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Tamanho Midia", Description="", Order = 9, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.TAMANHO_MIDIA")]
        public System.Nullable<System.Int32> TamanhoMidia
        {
                get
                {
                      return _TamanhoMidia;
                }
                set
                {
                      this._TamanhoMidia = value;
                }
        }

        private Byte[] _Thumbnail;

        [DataMember(Name = "Thumbnail", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Thumbnail", Description="", Order = 8, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.THUMBNAIL")]
        public Byte[] Thumbnail
        {
                get
                {
                      return _Thumbnail;
                }
                set
                {
                      this._Thumbnail = value;
                }
        }

        private System.String _TipoConteudoHttp;

        [DataMember(Name = "TipoConteudoHttp", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Tipo Conteudo Http", Description="", Order = 12, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.TIPO_CONTEUDO_HTTP")]
        public System.String TipoConteudoHttp
        {
                get
                {
                      return _TipoConteudoHttp;
                }
                set
                {
                      this._TipoConteudoHttp = value;
                }
        }

        private System.Guid _UidDocumento;

        [DataMember(Name = "UidDocumento", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Uid Documento", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
        [Key()]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.UID_DOCUMENTO")]
        public System.Guid UidDocumento
        {
                get
                {
                      return _UidDocumento;
                }
                set
                {
                      this._UidDocumento = value;
                }
        }

        private System.String _Url;

        [DataMember(Name = "Url", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Url", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.URL")]
        public System.String Url
        {
                get
                {
                      return _Url;
                }
                set
                {
                      this._Url = value;
                }
        }

        private System.String _XmlMapeamento;

        [DataMember(Name = "XmlMapeamento", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Xml Mapeamento", Description="", Order = 10, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA.XML_MAPEAMENTO")]
        public System.String XmlMapeamento
        {
                get
                {
                      return _XmlMapeamento;
                }
                set
                {
                      this._XmlMapeamento = value;
                }
        }
 
         //Detail Collections
 
         private IEnumerable<DocMultimidiaTabelaChild> _DocMultimidiaTabelaChildList;
         [DataMember(Name = "DocMultimidiaTabelaChildList", EmitDefaultValue = true)]
         public IEnumerable<DocMultimidiaTabelaChild> DocMultimidiaTabelaChildList
         {
               get { return _DocMultimidiaTabelaChildList;}
               set { _DocMultimidiaTabelaChildList = value;}
         }
    }
    
    [DataContract(IsReference = false, Name = "DocMultimidiaTabelaChild")]
    [Serializable()]
    public partial class DocMultimidiaTabelaChild
    {
    

        private Int64 _IdChave;

        [DataMember(Name = "IdChave", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Id Chave", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [Key()]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.ID_CHAVE")]
        public Int64 IdChave
        {
                get
                {
                      return _IdChave;
                }
                set
                {
                      this._IdChave = value;
                }
        }

        private Int16 _OrdemApresentacao;

        [DataMember(Name = "OrdemApresentacao", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Ordem Apresentacao", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.ORDEM_APRESENTACAO")]
        public Int16 OrdemApresentacao
        {
                get
                {
                      return _OrdemApresentacao;
                }
                set
                {
                      this._OrdemApresentacao = value;
                }
        }

        private System.Guid _UidChave;

        [DataMember(Name = "UidChave", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Uid Chave", Description="", Order = 5, AutoGenerateField = false, GroupName="", ResourceType= null)]
        [Key()]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.UID_CHAVE")]
        public System.Guid UidChave
        {
                get
                {
                      return _UidChave;
                }
                set
                {
                      this._UidChave = value;
                }
        }

        private System.Guid _UidDocumento;

        [DataMember(Name = "UidDocumento", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Uid Documento", Description="", Order = 1, AutoGenerateField = false, GroupName="", ResourceType= null)]
        [Key()]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.DOC_MULTIMIDIA.UID_DOCUMENTO")]
        public System.Guid UidDocumento
        {
                get
                {
                      return _UidDocumento;
                }
                set
                {
                      this._UidDocumento = value;
                }
        }

        private System.Guid _UidTabela;

        [DataMember(Name = "UidTabela", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Uid Tabela", Description="", Order = 5, AutoGenerateField = false, GroupName="", ResourceType= null)]
        [Key()]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_TABELA.UID_TABELA")]
        public System.Guid UidTabela
        {
                get
                {
                      return _UidTabela;
                }
                set
                {
                      this._UidTabela = value;
                }
        }
    }
    
    [DataContract(IsReference = false, Name = "DocMultimidiaConfig")]
    [Serializable()]
    public partial class DocMultimidiaConfig
    {
    

        private System.Nullable<System.Int32> _DocAltura;

        [DataMember(Name = "DocAltura", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Altura", Description="", Order = 1, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_CONFIG.DOC_ALTURA")]
        public System.Nullable<System.Int32> DocAltura
        {
                get
                {
                      return _DocAltura;
                }
                set
                {
                      this._DocAltura = value;
                }
        }

        private System.String _DocDuracao;

        [DataMember(Name = "DocDuracao", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Duração", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_CONFIG.DOC_DURACAO")]
        public System.String DocDuracao
        {
                get
                {
                      return _DocDuracao;
                }
                set
                {
                      this._DocDuracao = value;
                }
        }

        private System.String _DocFormatoVisualizacao;

        [DataMember(Name = "DocFormatoVisualizacao", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Formato da Visualização", Description="", Order = 3, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_CONFIG.DOC_FORMATO_VISUALIZACAO")]
        public System.String DocFormatoVisualizacao
        {
                get
                {
                      return _DocFormatoVisualizacao;
                }
                set
                {
                      this._DocFormatoVisualizacao = value;
                }
        }

        private System.Nullable<System.Int32> _DocLargura;

        [DataMember(Name = "DocLargura", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Largura", Description="", Order = 4, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_CONFIG.DOC_LARGURA")]
        public System.Nullable<System.Int32> DocLargura
        {
                get
                {
                      return _DocLargura;
                }
                set
                {
                      this._DocLargura = value;
                }
        }

        private System.String _DocTamanho;

        [DataMember(Name = "DocTamanho", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Tamanho", Description="", Order = 5, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_CONFIG.DOC_TAMANHO")]
        public System.String DocTamanho
        {
                get
                {
                      return _DocTamanho;
                }
                set
                {
                      this._DocTamanho = value;
                }
        }

        private Int32 _IdTcsAplicativo;

        [DataMember(Name = "IdTcsAplicativo", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Id Tcs Aplicativo", Description="", Order = 6, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [Key()]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="int#IdTcsAplicativo#false##12:0###0#false##::LookUpTcsAplicativo##false#false###Linx.Framework.BV.Multimidia#IQueryable###true#false", EdmKey="DOC_MULTIMIDIA_CONFIG.ID_TCS_APLICATIVO")]
        public Int32 IdTcsAplicativo
        {
                get
                {
                      return _IdTcsAplicativo;
                }
                set
                {
                      this._IdTcsAplicativo = value;
                }
        }

        private Byte _LxUsoMultimidia;

        [DataMember(Name = "LxUsoMultimidia", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Tipo de Uso", Description="", Order = 7, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [Key()]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="DOC_MULTIMIDIA_CONFIG.LX_USO_MULTIMIDIA")]
        public Byte LxUsoMultimidia
        {
                get
                {
                      return _LxUsoMultimidia;
                }
                set
                {
                      this._LxUsoMultimidia = value;
                }
        }

        [DataMember(Name = "DescricaoAplicativo", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Aplicativo", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="string#DescricaoAplicativo#false##250:0##Aplicativo#1#true##::LookUpTcsAplicativo##false#false###Linx.Framework.BV.Multimidia#IQueryable###true#false", EdmKey="")]
        public string DescricaoAplicativo
        {
                get; set;
        }
    }
    
    [DataContract(IsReference = false, Name = "MediaElement")]
    [Serializable()]
    public partial class MediaElement
    {
    

        private Guid _Id;

        [DataMember(Name = "Id", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [Key()]
        public Guid Id
        {
                get
                {
                      return _Id;
                }
                set
                {
                      this._Id = value;
                }
        }

        private string _Url;

        [DataMember(Name = "Url", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        public string Url
        {
                get
                {
                      return _Url;
                }
                set
                {
                      this._Url = value;
                }
        }

        private Byte _LxTipoDocumento;

        [DataMember(Name = "LxTipoDocumento", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "Lx Tipo Documento", Description="", Order = 2, AutoGenerateField = true, GroupName="", ResourceType= null)]
        public Byte LxTipoDocumento
        {
                get
                {
                      return _LxTipoDocumento;
                }
                set
                {
                      this._LxTipoDocumento = value;
                }
        }

        private bool _KeepContent;

        [DataMember(Name = "KeepContent", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        public bool KeepContent
        {
                get
                {
                      return _KeepContent;
                }
                set
                {
                      this._KeepContent = value;
                }
        }

        [DataMember(Name = "Extension", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        public string Extension
        {
                get; set;
        }

        [DataMember(Name = "TipoDocumento", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        public string TipoDocumento
        {
                get; set;
        }
    }
    
    [DataContract(IsReference = false, Name = "MediaConfigLength")]
    [Serializable()]
    public partial class MediaConfigLength
    {
    

        private int _IdApp;

        [DataMember(Name = "IdApp", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [Key()]
        public int IdApp
        {
                get
                {
                      return _IdApp;
                }
                set
                {
                      this._IdApp = value;
                }
        }

        private int _IdUse;

        [DataMember(Name = "IdUse", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [Key()]
        public int IdUse
        {
                get
                {
                      return _IdUse;
                }
                set
                {
                      this._IdUse = value;
                }
        }

        private int _Height;

        [DataMember(Name = "Height", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        public int Height
        {
                get
                {
                      return _Height;
                }
                set
                {
                      this._Height = value;
                }
        }

        private int _Width;

        [DataMember(Name = "Width", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        public int Width
        {
                get
                {
                      return _Width;
                }
                set
                {
                      this._Width = value;
                }
        }

        [DataMember(Name = "AppName", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        public string AppName
        {
                get; set;
        }

        [DataMember(Name = "UseName", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        public string UseName
        {
                get; set;
        }
    }
    
    [DataContract(IsReference = false, Name = "DocMultimidiaUpload")]
    [Serializable()]
    public partial class DocMultimidiaUpload
    {
    

        private byte _TipoDocumento;

        [DataMember(Name = "TipoDocumento", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [Key()]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
        public byte TipoDocumento
        {
                get
                {
                      return _TipoDocumento;
                }
                set
                {
                      this._TipoDocumento = value;
                }
        }

        private string _Conteudo;

        [DataMember(Name = "Conteudo", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
        public string Conteudo
        {
                get
                {
                      return _Conteudo;
                }
                set
                {
                      this._Conteudo = value;
                }
        }

        private string _NomeArquivo;

        [DataMember(Name = "NomeArquivo", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
        public string NomeArquivo
        {
                get
                {
                      return _NomeArquivo;
                }
                set
                {
                      this._NomeArquivo = value;
                }
        }

        private string _TipoConteudoHttp;

        [DataMember(Name = "TipoConteudoHttp", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
        public string TipoConteudoHttp
        {
                get
                {
                      return _TipoConteudoHttp;
                }
                set
                {
                      this._TipoConteudoHttp = value;
                }
        }

        private int _Tamanho;

        [DataMember(Name = "Tamanho", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
        public int Tamanho
        {
                get
                {
                      return _Tamanho;
                }
                set
                {
                      this._Tamanho = value;
                }
        }

        private string _JExpression;

        [DataMember(Name = "JExpression", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
        public string JExpression
        {
                get
                {
                      return _JExpression;
                }
                set
                {
                      this._JExpression = value;
                }
        }

        private string _NomeTabela;

        [DataMember(Name = "NomeTabela", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
        public string NomeTabela
        {
                get
                {
                      return _NomeTabela;
                }
                set
                {
                      this._NomeTabela = value;
                }
        }
    }
    
    [DataContract(IsReference = false, Name = "DocTabelaSync")]
    [Serializable()]
    public partial class DocTabelaSync
    {
    

        private string _NomeTabela;

        [DataMember(Name = "NomeTabela", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [Key()]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
        public string NomeTabela
        {
                get
                {
                      if (_NomeTabela.IsNullOrEmpty())
                         _NomeTabela =  String.Empty;
                      return _NomeTabela;
                }
                set
                {
                      this._NomeTabela = value;
                }
        }

        private Int64? _IdChave;

        [DataMember(Name = "IdChave", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
        public Int64? IdChave
        {
                get
                {
                      return _IdChave;
                }
                set
                {
                      this._IdChave = value;
                }
        }

        private Guid? _UidChave;

        [DataMember(Name = "UidChave", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
        public Guid? UidChave
        {
                get
                {
                      return _UidChave;
                }
                set
                {
                      this._UidChave = value;
                }
        }

        private List<Guid> _Midias;

        [DataMember(Name = "Midias", EmitDefaultValue = true)]
        [XmlAttribute()]
        [Editable(true)]
        [Display(Name = "", Description="", Order = 0, AutoGenerateField = true, GroupName="", ResourceType= null)]
        [LinxPublicationField(IsSuggestion=false, LookUpInfo="", EdmKey="")]
        public List<Guid> Midias
        {
                get
                {
                      return _Midias;
                }
                set
                {
                      this._Midias = value;
                }
        }
    }
    
}
