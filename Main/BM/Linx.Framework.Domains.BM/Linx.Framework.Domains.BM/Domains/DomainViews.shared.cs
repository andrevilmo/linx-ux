	

using System;
using System.IO;
using System.Collections.Generic;
using Linx.Tools; 
    
namespace Linx.Framework.Domains.BM.Domains
{

			
    public partial class TipoTransacao
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Todos"); 
				    
					result.Add("2", "ERP"); 
				    
					result.Add("3", "Loja"); 
				    
					result.Add("4", "Excel"); 
				    
					result.Add("5", "Mobile"); 
				    
					result.Add("6", "ERP App"); 
				    
					result.Add("7", "Assistente"); 
				    
					result.Add("8", "Dashboard"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Todos"); 
				    
					result.Add("2", "ERP"); 
				    
					result.Add("3", "Loja"); 
				    
					result.Add("4", "Excel"); 
				    
					result.Add("5", "Mobile"); 
				    
					result.Add("6", "ERPAPP"); 
				    
					result.Add("7", "Assistente"); 
				    
					result.Add("8", "Dashboard"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Todos = new DomainKeyPair() { Value = "1", DisplayName = "Todos" };
					[FunctionalPoint("Value[1];DisplayName[Todos]")]
					public static DomainKeyPair Todos { get { return _Todos; } }
				    
					private static DomainKeyPair _ERP = new DomainKeyPair() { Value = "2", DisplayName = "ERP" };
					[FunctionalPoint("Value[2];DisplayName[ERP]")]
					public static DomainKeyPair ERP { get { return _ERP; } }
				    
					private static DomainKeyPair _Loja = new DomainKeyPair() { Value = "3", DisplayName = "Loja" };
					[FunctionalPoint("Value[3];DisplayName[Loja]")]
					public static DomainKeyPair Loja { get { return _Loja; } }
				    
					private static DomainKeyPair _Excel = new DomainKeyPair() { Value = "4", DisplayName = "Excel" };
					[FunctionalPoint("Value[4];DisplayName[Excel]")]
					public static DomainKeyPair Excel { get { return _Excel; } }
				    
					private static DomainKeyPair _Mobile = new DomainKeyPair() { Value = "5", DisplayName = "Mobile" };
					[FunctionalPoint("Value[5];DisplayName[Mobile]")]
					public static DomainKeyPair Mobile { get { return _Mobile; } }
				    
					private static DomainKeyPair _ERPAPP = new DomainKeyPair() { Value = "6", DisplayName = "ERP App" };
					[FunctionalPoint("Value[6];DisplayName[ERP App]")]
					public static DomainKeyPair ERPAPP { get { return _ERPAPP; } }
				    
					private static DomainKeyPair _Assistente = new DomainKeyPair() { Value = "7", DisplayName = "Assistente" };
					[FunctionalPoint("Value[7];DisplayName[Assistente]")]
					public static DomainKeyPair Assistente { get { return _Assistente; } }
				    
					private static DomainKeyPair _Dashboard = new DomainKeyPair() { Value = "8", DisplayName = "Dashboard" };
					[FunctionalPoint("Value[8];DisplayName[Dashboard]")]
					public static DomainKeyPair Dashboard { get { return _Dashboard; } }
				    
			#endregion properties

		

	}    
			
    public partial class RegraAcesso
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Acesso Bloqueado"); 
				    
					result.Add("2", "Acesso Total"); 
				    
					result.Add("3", "Pesquisar"); 
				    
					result.Add("4", "Incluir"); 
				    
					result.Add("5", "Alterar"); 
				    
					result.Add("6", "Excluir"); 
				    
					result.Add("7", "Pesquisa Especial"); 
				    
					result.Add("8", "Imprimir"); 
				    
					result.Add("9", "Exportar"); 
				    
					result.Add("10", "Criar Relatório"); 
				    
					result.Add("99", "Regra Transação"); 
				    
					result.Add("11", "Layout"); 
				    
					result.Add("12", "Criar Pesquisa"); 
				    
					result.Add("13", "Acesso por Transação"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "AcessoBloqueado"); 
				    
					result.Add("2", "AcessoTotal"); 
				    
					result.Add("3", "Pesquisar"); 
				    
					result.Add("4", "Incluir"); 
				    
					result.Add("5", "Alterar"); 
				    
					result.Add("6", "Excluir"); 
				    
					result.Add("7", "PesquisaEspecial"); 
				    
					result.Add("8", "Imprimir"); 
				    
					result.Add("9", "Exportar"); 
				    
					result.Add("10", "CriarRelatorio"); 
				    
					result.Add("99", "RegraTransacao"); 
				    
					result.Add("11", "Layout"); 
				    
					result.Add("12", "CriarPesquisa"); 
				    
					result.Add("13", "AcessoTransacao"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _AcessoBloqueado = new DomainKeyPair() { Value = "1", DisplayName = "Acesso Bloqueado" };
					[FunctionalPoint("Value[1];DisplayName[Acesso Bloqueado]")]
					public static DomainKeyPair AcessoBloqueado { get { return _AcessoBloqueado; } }
				    
					private static DomainKeyPair _AcessoTotal = new DomainKeyPair() { Value = "2", DisplayName = "Acesso Total" };
					[FunctionalPoint("Value[2];DisplayName[Acesso Total]")]
					public static DomainKeyPair AcessoTotal { get { return _AcessoTotal; } }
				    
					private static DomainKeyPair _Pesquisar = new DomainKeyPair() { Value = "3", DisplayName = "Pesquisar" };
					[FunctionalPoint("Value[3];DisplayName[Pesquisar]")]
					public static DomainKeyPair Pesquisar { get { return _Pesquisar; } }
				    
					private static DomainKeyPair _Incluir = new DomainKeyPair() { Value = "4", DisplayName = "Incluir" };
					[FunctionalPoint("Value[4];DisplayName[Incluir]")]
					public static DomainKeyPair Incluir { get { return _Incluir; } }
				    
					private static DomainKeyPair _Alterar = new DomainKeyPair() { Value = "5", DisplayName = "Alterar" };
					[FunctionalPoint("Value[5];DisplayName[Alterar]")]
					public static DomainKeyPair Alterar { get { return _Alterar; } }
				    
					private static DomainKeyPair _Excluir = new DomainKeyPair() { Value = "6", DisplayName = "Excluir" };
					[FunctionalPoint("Value[6];DisplayName[Excluir]")]
					public static DomainKeyPair Excluir { get { return _Excluir; } }
				    
					private static DomainKeyPair _PesquisaEspecial = new DomainKeyPair() { Value = "7", DisplayName = "Pesquisa Especial" };
					[FunctionalPoint("Value[7];DisplayName[Pesquisa Especial]")]
					public static DomainKeyPair PesquisaEspecial { get { return _PesquisaEspecial; } }
				    
					private static DomainKeyPair _Imprimir = new DomainKeyPair() { Value = "8", DisplayName = "Imprimir" };
					[FunctionalPoint("Value[8];DisplayName[Imprimir]")]
					public static DomainKeyPair Imprimir { get { return _Imprimir; } }
				    
					private static DomainKeyPair _Exportar = new DomainKeyPair() { Value = "9", DisplayName = "Exportar" };
					[FunctionalPoint("Value[9];DisplayName[Exportar]")]
					public static DomainKeyPair Exportar { get { return _Exportar; } }
				    
					private static DomainKeyPair _CriarRelatorio = new DomainKeyPair() { Value = "10", DisplayName = "Criar Relatório" };
					[FunctionalPoint("Value[10];DisplayName[Criar Relatório]")]
					public static DomainKeyPair CriarRelatorio { get { return _CriarRelatorio; } }
				    
					private static DomainKeyPair _RegraTransacao = new DomainKeyPair() { Value = "99", DisplayName = "Regra Transação" };
					[FunctionalPoint("Value[99];DisplayName[Regra Transação]")]
					public static DomainKeyPair RegraTransacao { get { return _RegraTransacao; } }
				    
					private static DomainKeyPair _Layout = new DomainKeyPair() { Value = "11", DisplayName = "Layout" };
					[FunctionalPoint("Value[11];DisplayName[Layout]")]
					public static DomainKeyPair Layout { get { return _Layout; } }
				    
					private static DomainKeyPair _CriarPesquisa = new DomainKeyPair() { Value = "12", DisplayName = "Criar Pesquisa" };
					[FunctionalPoint("Value[12];DisplayName[Criar Pesquisa]")]
					public static DomainKeyPair CriarPesquisa { get { return _CriarPesquisa; } }
				    
					private static DomainKeyPair _AcessoTransacao = new DomainKeyPair() { Value = "13", DisplayName = "Acesso por Transação" };
					[FunctionalPoint("Value[13];DisplayName[Acesso por Transação]")]
					public static DomainKeyPair AcessoTransacao { get { return _AcessoTransacao; } }
				    
			#endregion properties

		

	}    
			
    public partial class RegraAcessoColuna
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Acesso Bloqueado"); 
				    
					result.Add("2", "Acesso Total"); 
				    
					result.Add("3", "Visualizar"); 
				    
					result.Add("4", "Alterar"); 
				    
					result.Add("5", "Pesquisar"); 
				    
					result.Add("99", "Regra Transação"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "AcessoBloqueado"); 
				    
					result.Add("2", "AcessoTotal"); 
				    
					result.Add("3", "Visualizar"); 
				    
					result.Add("4", "Alterar"); 
				    
					result.Add("5", "Pesquisar"); 
				    
					result.Add("99", "RegraTransacao"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _AcessoBloqueado = new DomainKeyPair() { Value = "1", DisplayName = "Acesso Bloqueado" };
					[FunctionalPoint("Value[1];DisplayName[Acesso Bloqueado]")]
					public static DomainKeyPair AcessoBloqueado { get { return _AcessoBloqueado; } }
				    
					private static DomainKeyPair _AcessoTotal = new DomainKeyPair() { Value = "2", DisplayName = "Acesso Total" };
					[FunctionalPoint("Value[2];DisplayName[Acesso Total]")]
					public static DomainKeyPair AcessoTotal { get { return _AcessoTotal; } }
				    
					private static DomainKeyPair _Visualizar = new DomainKeyPair() { Value = "3", DisplayName = "Visualizar" };
					[FunctionalPoint("Value[3];DisplayName[Visualizar]")]
					public static DomainKeyPair Visualizar { get { return _Visualizar; } }
				    
					private static DomainKeyPair _Alterar = new DomainKeyPair() { Value = "4", DisplayName = "Alterar" };
					[FunctionalPoint("Value[4];DisplayName[Alterar]")]
					public static DomainKeyPair Alterar { get { return _Alterar; } }
				    
					private static DomainKeyPair _Pesquisar = new DomainKeyPair() { Value = "5", DisplayName = "Pesquisar" };
					[FunctionalPoint("Value[5];DisplayName[Pesquisar]")]
					public static DomainKeyPair Pesquisar { get { return _Pesquisar; } }
				    
					private static DomainKeyPair _RegraTransacao = new DomainKeyPair() { Value = "99", DisplayName = "Regra Transação" };
					[FunctionalPoint("Value[99];DisplayName[Regra Transação]")]
					public static DomainKeyPair RegraTransacao { get { return _RegraTransacao; } }
				    
			#endregion properties

		

	}    
			
    public partial class TipoObjeto
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "BO"); 
				    
					result.Add("2", "Transação"); 
				    
					result.Add("3", "Campo"); 
				    
					result.Add("4", "Trigger"); 
				    
					result.Add("5", "Stored Procedure"); 
				    
					result.Add("6", "Relatório"); 
				    
					result.Add("7", "Workflow"); 
				    
					result.Add("8", "Template de ação de Workflow"); 
				    
					result.Add("9", "Layout"); 
				    
					result.Add("10", "Filtro"); 
				    
					result.Add("11", "Extensão (Objeto de entrada)"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "BO"); 
				    
					result.Add("2", "Transacao"); 
				    
					result.Add("3", "Campo"); 
				    
					result.Add("4", "Trigger"); 
				    
					result.Add("5", "StoredProcedure"); 
				    
					result.Add("6", "Relatorio"); 
				    
					result.Add("7", "Workflow"); 
				    
					result.Add("8", "TemplateAcaoWF"); 
				    
					result.Add("9", "Layout"); 
				    
					result.Add("10", "Filtro"); 
				    
					result.Add("11", "UserExtension"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _BO = new DomainKeyPair() { Value = "1", DisplayName = "BO" };
					[FunctionalPoint("Value[1];DisplayName[BO]")]
					public static DomainKeyPair BO { get { return _BO; } }
				    
					private static DomainKeyPair _Transacao = new DomainKeyPair() { Value = "2", DisplayName = "Transação" };
					[FunctionalPoint("Value[2];DisplayName[Transação]")]
					public static DomainKeyPair Transacao { get { return _Transacao; } }
				    
					private static DomainKeyPair _Campo = new DomainKeyPair() { Value = "3", DisplayName = "Campo" };
					[FunctionalPoint("Value[3];DisplayName[Campo]")]
					public static DomainKeyPair Campo { get { return _Campo; } }
				    
					private static DomainKeyPair _Trigger = new DomainKeyPair() { Value = "4", DisplayName = "Trigger" };
					[FunctionalPoint("Value[4];DisplayName[Trigger]")]
					public static DomainKeyPair Trigger { get { return _Trigger; } }
				    
					private static DomainKeyPair _StoredProcedure = new DomainKeyPair() { Value = "5", DisplayName = "Stored Procedure" };
					[FunctionalPoint("Value[5];DisplayName[Stored Procedure]")]
					public static DomainKeyPair StoredProcedure { get { return _StoredProcedure; } }
				    
					private static DomainKeyPair _Relatorio = new DomainKeyPair() { Value = "6", DisplayName = "Relatório" };
					[FunctionalPoint("Value[6];DisplayName[Relatório]")]
					public static DomainKeyPair Relatorio { get { return _Relatorio; } }
				    
					private static DomainKeyPair _Workflow = new DomainKeyPair() { Value = "7", DisplayName = "Workflow" };
					[FunctionalPoint("Value[7];DisplayName[Workflow]")]
					public static DomainKeyPair Workflow { get { return _Workflow; } }
				    
					private static DomainKeyPair _TemplateAcaoWF = new DomainKeyPair() { Value = "8", DisplayName = "Template de ação de Workflow" };
					[FunctionalPoint("Value[8];DisplayName[Template de ação de Workflow]")]
					public static DomainKeyPair TemplateAcaoWF { get { return _TemplateAcaoWF; } }
				    
					private static DomainKeyPair _Layout = new DomainKeyPair() { Value = "9", DisplayName = "Layout" };
					[FunctionalPoint("Value[9];DisplayName[Layout]")]
					public static DomainKeyPair Layout { get { return _Layout; } }
				    
					private static DomainKeyPair _Filtro = new DomainKeyPair() { Value = "10", DisplayName = "Filtro" };
					[FunctionalPoint("Value[10];DisplayName[Filtro]")]
					public static DomainKeyPair Filtro { get { return _Filtro; } }
				    
					private static DomainKeyPair _UserExtension = new DomainKeyPair() { Value = "11", DisplayName = "Extensão (Objeto de entrada)" };
					[FunctionalPoint("Value[11];DisplayName[Extensão (Objeto de entrada)]")]
					public static DomainKeyPair UserExtension { get { return _UserExtension; } }
				    
			#endregion properties

		

	}    
			
    public partial class TipoValidacaoParametro
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Validação Contra Tabela (Valida)"); 
				    
					result.Add("2", "Validação Contra Tabela (Combo)"); 
				    
					result.Add("3", "Validação Contra Faixa"); 
				    
					result.Add("4", "Validação Contra Objeto CRM"); 
				    
					result.Add("8", "Sem Validação"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "ValidacaoTabela"); 
				    
					result.Add("2", "ValidacaoCombo"); 
				    
					result.Add("3", "ValidacaoFaixa"); 
				    
					result.Add("4", "ValidacaoObjetoCRM"); 
				    
					result.Add("8", "SemValidacao"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _ValidacaoTabela = new DomainKeyPair() { Value = "1", DisplayName = "Validação Contra Tabela (Valida)" };
					[FunctionalPoint("Value[1];DisplayName[Validação Contra Tabela (Valida)]")]
					public static DomainKeyPair ValidacaoTabela { get { return _ValidacaoTabela; } }
				    
					private static DomainKeyPair _ValidacaoCombo = new DomainKeyPair() { Value = "2", DisplayName = "Validação Contra Tabela (Combo)" };
					[FunctionalPoint("Value[2];DisplayName[Validação Contra Tabela (Combo)]")]
					public static DomainKeyPair ValidacaoCombo { get { return _ValidacaoCombo; } }
				    
					private static DomainKeyPair _ValidacaoFaixa = new DomainKeyPair() { Value = "3", DisplayName = "Validação Contra Faixa" };
					[FunctionalPoint("Value[3];DisplayName[Validação Contra Faixa]")]
					public static DomainKeyPair ValidacaoFaixa { get { return _ValidacaoFaixa; } }
				    
					private static DomainKeyPair _ValidacaoObjetoCRM = new DomainKeyPair() { Value = "4", DisplayName = "Validação Contra Objeto CRM" };
					[FunctionalPoint("Value[4];DisplayName[Validação Contra Objeto CRM]")]
					public static DomainKeyPair ValidacaoObjetoCRM { get { return _ValidacaoObjetoCRM; } }
				    
					private static DomainKeyPair _SemValidacao = new DomainKeyPair() { Value = "8", DisplayName = "Sem Validação" };
					[FunctionalPoint("Value[8];DisplayName[Sem Validação]")]
					public static DomainKeyPair SemValidacao { get { return _SemValidacao; } }
				    
			#endregion properties

		

	}    
			
    public partial class TipoValorParametro
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Numérico"); 
				    
					result.Add("2", "Caractere"); 
				    
					result.Add("3", "Data"); 
				    
					result.Add("4", "Lógico"); 
				    
					result.Add("5", "Senha"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Numerico"); 
				    
					result.Add("2", "Caractere"); 
				    
					result.Add("3", "Data"); 
				    
					result.Add("4", "Logico"); 
				    
					result.Add("5", "Senha"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Numerico = new DomainKeyPair() { Value = "1", DisplayName = "Numérico" };
					[FunctionalPoint("Value[1];DisplayName[Numérico]")]
					public static DomainKeyPair Numerico { get { return _Numerico; } }
				    
					private static DomainKeyPair _Caractere = new DomainKeyPair() { Value = "2", DisplayName = "Caractere" };
					[FunctionalPoint("Value[2];DisplayName[Caractere]")]
					public static DomainKeyPair Caractere { get { return _Caractere; } }
				    
					private static DomainKeyPair _Data = new DomainKeyPair() { Value = "3", DisplayName = "Data" };
					[FunctionalPoint("Value[3];DisplayName[Data]")]
					public static DomainKeyPair Data { get { return _Data; } }
				    
					private static DomainKeyPair _Logico = new DomainKeyPair() { Value = "4", DisplayName = "Lógico" };
					[FunctionalPoint("Value[4];DisplayName[Lógico]")]
					public static DomainKeyPair Logico { get { return _Logico; } }
				    
					private static DomainKeyPair _Senha = new DomainKeyPair() { Value = "5", DisplayName = "Senha" };
					[FunctionalPoint("Value[5];DisplayName[Senha]")]
					public static DomainKeyPair Senha { get { return _Senha; } }
				    
			#endregion properties

		

	}    
			
    public partial class TipoDocumento
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Normal"); 
				    
					result.Add("2", "Matriz Para Transformação"); 
				    
					result.Add("3", "Detalhe/Estampa"); 
				    
					result.Add("4", "360°"); 
				    
					result.Add("5", "Vídeos"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Normal"); 
				    
					result.Add("2", "Matriz_Transformacao"); 
				    
					result.Add("3", "Detalhe_Estampa"); 
				    
					result.Add("4", "Imagem_360"); 
				    
					result.Add("5", "Vídeos"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Normal = new DomainKeyPair() { Value = "1", DisplayName = "Normal" };
					[FunctionalPoint("Value[1];DisplayName[Normal]")]
					public static DomainKeyPair Normal { get { return _Normal; } }
				    
					private static DomainKeyPair _Matriz_Transformacao = new DomainKeyPair() { Value = "2", DisplayName = "Matriz Para Transformação" };
					[FunctionalPoint("Value[2];DisplayName[Matriz Para Transformação]")]
					public static DomainKeyPair Matriz_Transformacao { get { return _Matriz_Transformacao; } }
				    
					private static DomainKeyPair _Detalhe_Estampa = new DomainKeyPair() { Value = "3", DisplayName = "Detalhe/Estampa" };
					[FunctionalPoint("Value[3];DisplayName[Detalhe/Estampa]")]
					public static DomainKeyPair Detalhe_Estampa { get { return _Detalhe_Estampa; } }
				    
					private static DomainKeyPair _Imagem_360 = new DomainKeyPair() { Value = "4", DisplayName = "360°" };
					[FunctionalPoint("Value[4];DisplayName[360°]")]
					public static DomainKeyPair Imagem_360 { get { return _Imagem_360; } }
				    
					private static DomainKeyPair _Vídeos = new DomainKeyPair() { Value = "5", DisplayName = "Vídeos" };
					[FunctionalPoint("Value[5];DisplayName[Vídeos]")]
					public static DomainKeyPair Vídeos { get { return _Vídeos; } }
				    
			#endregion properties

		

	}    
			
    public partial class TipoExtensao
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "JPEG"); 
				    
					result.Add("2", "JPG"); 
				    
					result.Add("3", "PNG"); 
				    
					result.Add("4", "WMV"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "JPEG"); 
				    
					result.Add("2", "JPG"); 
				    
					result.Add("3", "PNG"); 
				    
					result.Add("4", "WMV"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _JPEG = new DomainKeyPair() { Value = "1", DisplayName = "JPEG" };
					[FunctionalPoint("Value[1];DisplayName[JPEG]")]
					public static DomainKeyPair JPEG { get { return _JPEG; } }
				    
					private static DomainKeyPair _JPG = new DomainKeyPair() { Value = "2", DisplayName = "JPG" };
					[FunctionalPoint("Value[2];DisplayName[JPG]")]
					public static DomainKeyPair JPG { get { return _JPG; } }
				    
					private static DomainKeyPair _PNG = new DomainKeyPair() { Value = "3", DisplayName = "PNG" };
					[FunctionalPoint("Value[3];DisplayName[PNG]")]
					public static DomainKeyPair PNG { get { return _PNG; } }
				    
					private static DomainKeyPair _WMV = new DomainKeyPair() { Value = "4", DisplayName = "WMV" };
					[FunctionalPoint("Value[4];DisplayName[WMV]")]
					public static DomainKeyPair WMV { get { return _WMV; } }
				    
			#endregion properties

		

	}    
			
    public partial class TipoArquivo
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("G", "Todos"); 
				    
					result.Add("X", "XML"); 
				    
					result.Add("E", "Excel"); 
				    
					result.Add("T", "Text"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("G", "Todos"); 
				    
					result.Add("X", "Xml"); 
				    
					result.Add("E", "Excel"); 
				    
					result.Add("T", "Text"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Todos = new DomainKeyPair() { Value = "G", DisplayName = "Todos" };
					[FunctionalPoint("Value[G];DisplayName[Todos]")]
					public static DomainKeyPair Todos { get { return _Todos; } }
				    
					private static DomainKeyPair _Xml = new DomainKeyPair() { Value = "X", DisplayName = "XML" };
					[FunctionalPoint("Value[X];DisplayName[XML]")]
					public static DomainKeyPair Xml { get { return _Xml; } }
				    
					private static DomainKeyPair _Excel = new DomainKeyPair() { Value = "E", DisplayName = "Excel" };
					[FunctionalPoint("Value[E];DisplayName[Excel]")]
					public static DomainKeyPair Excel { get { return _Excel; } }
				    
					private static DomainKeyPair _Text = new DomainKeyPair() { Value = "T", DisplayName = "Text" };
					[FunctionalPoint("Value[T];DisplayName[Text]")]
					public static DomainKeyPair Text { get { return _Text; } }
				    
			#endregion properties

		

	}    
			
    public partial class TipoDado
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("STR", "String"); 
				    
					result.Add("INT", "Integer"); 
				    
					result.Add("BLN", "Boolean"); 
				    
					result.Add("DEC", "Decimal"); 
				    
					result.Add("BYT", "Byte"); 
				    
					result.Add("LNG", "Long"); 
				    
					result.Add("POS", "PositiveInteger"); 
				    
					result.Add("DTE", "Date"); 
				    
					result.Add("TME", "Time"); 
				    
					result.Add("DBL", "Double"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("STR", "STRING"); 
				    
					result.Add("INT", "INTEGER"); 
				    
					result.Add("BLN", "BOOLEAN"); 
				    
					result.Add("DEC", "DECIMAL"); 
				    
					result.Add("BYT", "BYTE"); 
				    
					result.Add("LNG", "LONG"); 
				    
					result.Add("POS", "POSITIVEINTEGER"); 
				    
					result.Add("DTE", "DATE"); 
				    
					result.Add("TME", "TIME"); 
				    
					result.Add("DBL", "DOUBLE"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _STRING = new DomainKeyPair() { Value = "STR", DisplayName = "String" };
					[FunctionalPoint("Value[STR];DisplayName[String]")]
					public static DomainKeyPair STRING { get { return _STRING; } }
				    
					private static DomainKeyPair _INTEGER = new DomainKeyPair() { Value = "INT", DisplayName = "Integer" };
					[FunctionalPoint("Value[INT];DisplayName[Integer]")]
					public static DomainKeyPair INTEGER { get { return _INTEGER; } }
				    
					private static DomainKeyPair _BOOLEAN = new DomainKeyPair() { Value = "BLN", DisplayName = "Boolean" };
					[FunctionalPoint("Value[BLN];DisplayName[Boolean]")]
					public static DomainKeyPair BOOLEAN { get { return _BOOLEAN; } }
				    
					private static DomainKeyPair _DECIMAL = new DomainKeyPair() { Value = "DEC", DisplayName = "Decimal" };
					[FunctionalPoint("Value[DEC];DisplayName[Decimal]")]
					public static DomainKeyPair DECIMAL { get { return _DECIMAL; } }
				    
					private static DomainKeyPair _BYTE = new DomainKeyPair() { Value = "BYT", DisplayName = "Byte" };
					[FunctionalPoint("Value[BYT];DisplayName[Byte]")]
					public static DomainKeyPair BYTE { get { return _BYTE; } }
				    
					private static DomainKeyPair _LONG = new DomainKeyPair() { Value = "LNG", DisplayName = "Long" };
					[FunctionalPoint("Value[LNG];DisplayName[Long]")]
					public static DomainKeyPair LONG { get { return _LONG; } }
				    
					private static DomainKeyPair _POSITIVEINTEGER = new DomainKeyPair() { Value = "POS", DisplayName = "PositiveInteger" };
					[FunctionalPoint("Value[POS];DisplayName[PositiveInteger]")]
					public static DomainKeyPair POSITIVEINTEGER { get { return _POSITIVEINTEGER; } }
				    
					private static DomainKeyPair _DATE = new DomainKeyPair() { Value = "DTE", DisplayName = "Date" };
					[FunctionalPoint("Value[DTE];DisplayName[Date]")]
					public static DomainKeyPair DATE { get { return _DATE; } }
				    
					private static DomainKeyPair _TIME = new DomainKeyPair() { Value = "TME", DisplayName = "Time" };
					[FunctionalPoint("Value[TME];DisplayName[Time]")]
					public static DomainKeyPair TIME { get { return _TIME; } }
				    
					private static DomainKeyPair _DOUBLE = new DomainKeyPair() { Value = "DBL", DisplayName = "Double" };
					[FunctionalPoint("Value[DBL];DisplayName[Double]")]
					public static DomainKeyPair DOUBLE { get { return _DOUBLE; } }
				    
			#endregion properties

		

	}    
			
    public partial class TipoLog
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Leitura de Arquivo"); 
				    
					result.Add("2", "Geração de Arquivo"); 
				    
					result.Add("3", "Importação de Layout"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "LeituraArquivo"); 
				    
					result.Add("2", "GeracaoArquivo"); 
				    
					result.Add("3", "ImportacaoLayout"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _LeituraArquivo = new DomainKeyPair() { Value = "1", DisplayName = "Leitura de Arquivo" };
					[FunctionalPoint("Value[1];DisplayName[Leitura de Arquivo]")]
					public static DomainKeyPair LeituraArquivo { get { return _LeituraArquivo; } }
				    
					private static DomainKeyPair _GeracaoArquivo = new DomainKeyPair() { Value = "2", DisplayName = "Geração de Arquivo" };
					[FunctionalPoint("Value[2];DisplayName[Geração de Arquivo]")]
					public static DomainKeyPair GeracaoArquivo { get { return _GeracaoArquivo; } }
				    
					private static DomainKeyPair _ImportacaoLayout = new DomainKeyPair() { Value = "3", DisplayName = "Importação de Layout" };
					[FunctionalPoint("Value[3];DisplayName[Importação de Layout]")]
					public static DomainKeyPair ImportacaoLayout { get { return _ImportacaoLayout; } }
				    
			#endregion properties

		

	}    
			
    public partial class FormatoData
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "AAAAMMDD"); 
				    
					result.Add("2", "DDMMAAAA"); 
				    
					result.Add("3", "MMDDAAAA"); 
				    
					result.Add("4", "AAMMDD"); 
				    
					result.Add("5", "DDMMAA"); 
				    
					result.Add("6", "MMDDAA"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "AAAAMMDD"); 
				    
					result.Add("2", "DDMMAAAA"); 
				    
					result.Add("3", "MMDDAAAA"); 
				    
					result.Add("4", "AAMMDD"); 
				    
					result.Add("5", "DDMMAA"); 
				    
					result.Add("6", "MMDDAA"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _AAAAMMDD = new DomainKeyPair() { Value = "1", DisplayName = "AAAAMMDD" };
					[FunctionalPoint("Value[1];DisplayName[AAAAMMDD]")]
					public static DomainKeyPair AAAAMMDD { get { return _AAAAMMDD; } }
				    
					private static DomainKeyPair _DDMMAAAA = new DomainKeyPair() { Value = "2", DisplayName = "DDMMAAAA" };
					[FunctionalPoint("Value[2];DisplayName[DDMMAAAA]")]
					public static DomainKeyPair DDMMAAAA { get { return _DDMMAAAA; } }
				    
					private static DomainKeyPair _MMDDAAAA = new DomainKeyPair() { Value = "3", DisplayName = "MMDDAAAA" };
					[FunctionalPoint("Value[3];DisplayName[MMDDAAAA]")]
					public static DomainKeyPair MMDDAAAA { get { return _MMDDAAAA; } }
				    
					private static DomainKeyPair _AAMMDD = new DomainKeyPair() { Value = "4", DisplayName = "AAMMDD" };
					[FunctionalPoint("Value[4];DisplayName[AAMMDD]")]
					public static DomainKeyPair AAMMDD { get { return _AAMMDD; } }
				    
					private static DomainKeyPair _DDMMAA = new DomainKeyPair() { Value = "5", DisplayName = "DDMMAA" };
					[FunctionalPoint("Value[5];DisplayName[DDMMAA]")]
					public static DomainKeyPair DDMMAA { get { return _DDMMAA; } }
				    
					private static DomainKeyPair _MMDDAA = new DomainKeyPair() { Value = "6", DisplayName = "MMDDAA" };
					[FunctionalPoint("Value[6];DisplayName[MMDDAA]")]
					public static DomainKeyPair MMDDAA { get { return _MMDDAA; } }
				    
			#endregion properties

		

	}    
			
    public partial class TipoLayout
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Layout do Sistema"); 
				    
					result.Add("2", "Layout do Usuário"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "SystemLayout"); 
				    
					result.Add("2", "UserLayout"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _SystemLayout = new DomainKeyPair() { Value = "1", DisplayName = "Layout do Sistema" };
					[FunctionalPoint("Value[1];DisplayName[Layout do Sistema]")]
					public static DomainKeyPair SystemLayout { get { return _SystemLayout; } }
				    
					private static DomainKeyPair _UserLayout = new DomainKeyPair() { Value = "2", DisplayName = "Layout do Usuário" };
					[FunctionalPoint("Value[2];DisplayName[Layout do Usuário]")]
					public static DomainKeyPair UserLayout { get { return _UserLayout; } }
				    
			#endregion properties

		

	}    
			
    public partial class PosicaoDaTransacao
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Página"); 
				    
					result.Add("2", "Painel à Esquerda"); 
				    
					result.Add("3", "Painel Superior"); 
				    
					result.Add("4", "Painel à Direita"); 
				    
					result.Add("5", "Painel Inferior"); 
				    
					result.Add("6", "Painel Flutuante"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "None"); 
				    
					result.Add("2", "Left"); 
				    
					result.Add("3", "Top"); 
				    
					result.Add("4", "Right"); 
				    
					result.Add("5", "Bottom"); 
				    
					result.Add("6", "Floating"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _None = new DomainKeyPair() { Value = "1", DisplayName = "Página" };
					[FunctionalPoint("Value[1];DisplayName[Página]")]
					public static DomainKeyPair None { get { return _None; } }
				    
					private static DomainKeyPair _Left = new DomainKeyPair() { Value = "2", DisplayName = "Painel à Esquerda" };
					[FunctionalPoint("Value[2];DisplayName[Painel à Esquerda]")]
					public static DomainKeyPair Left { get { return _Left; } }
				    
					private static DomainKeyPair _Top = new DomainKeyPair() { Value = "3", DisplayName = "Painel Superior" };
					[FunctionalPoint("Value[3];DisplayName[Painel Superior]")]
					public static DomainKeyPair Top { get { return _Top; } }
				    
					private static DomainKeyPair _Right = new DomainKeyPair() { Value = "4", DisplayName = "Painel à Direita" };
					[FunctionalPoint("Value[4];DisplayName[Painel à Direita]")]
					public static DomainKeyPair Right { get { return _Right; } }
				    
					private static DomainKeyPair _Bottom = new DomainKeyPair() { Value = "5", DisplayName = "Painel Inferior" };
					[FunctionalPoint("Value[5];DisplayName[Painel Inferior]")]
					public static DomainKeyPair Bottom { get { return _Bottom; } }
				    
					private static DomainKeyPair _Floating = new DomainKeyPair() { Value = "6", DisplayName = "Painel Flutuante" };
					[FunctionalPoint("Value[6];DisplayName[Painel Flutuante]")]
					public static DomainKeyPair Floating { get { return _Floating; } }
				    
			#endregion properties

		

	}    
			
    public partial class TipoLayoutDependente
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Grade de Dados"); 
				    
					result.Add("2", "Formulário"); 
				    
					result.Add("3", "Grade de Dados à Esquerda/Formulário à Direita"); 
				    
					result.Add("4", "Grade de Dados em Cima/Formulário em Baixo"); 
				    
					result.Add("5", "Grade de Dados à Direita/Formulário à Esquerda"); 
				    
					result.Add("6", "Grade de Dados em Baixo/Formulário em Cima"); 
				    
					result.Add("7", "Padrão"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "GridLayout"); 
				    
					result.Add("2", "ColumnsLayout"); 
				    
					result.Add("3", "LeftGridLayout_RightColumnsLayout"); 
				    
					result.Add("4", "TopGridLayout_BottomColumnsLayout"); 
				    
					result.Add("5", "RightGridLayout_LeftColumnsLayout"); 
				    
					result.Add("6", "BottomGridLayout_TopColumnsLayout"); 
				    
					result.Add("7", "Default"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _GridLayout = new DomainKeyPair() { Value = "1", DisplayName = "Grade de Dados" };
					[FunctionalPoint("Value[1];DisplayName[Grade de Dados]")]
					public static DomainKeyPair GridLayout { get { return _GridLayout; } }
				    
					private static DomainKeyPair _ColumnsLayout = new DomainKeyPair() { Value = "2", DisplayName = "Formulário" };
					[FunctionalPoint("Value[2];DisplayName[Formulário]")]
					public static DomainKeyPair ColumnsLayout { get { return _ColumnsLayout; } }
				    
					private static DomainKeyPair _LeftGridLayout_RightColumnsLayout = new DomainKeyPair() { Value = "3", DisplayName = "Grade de Dados à Esquerda/Formulário à Direita" };
					[FunctionalPoint("Value[3];DisplayName[Grade de Dados à Esquerda/Formulário à Direita]")]
					public static DomainKeyPair LeftGridLayout_RightColumnsLayout { get { return _LeftGridLayout_RightColumnsLayout; } }
				    
					private static DomainKeyPair _TopGridLayout_BottomColumnsLayout = new DomainKeyPair() { Value = "4", DisplayName = "Grade de Dados em Cima/Formulário em Baixo" };
					[FunctionalPoint("Value[4];DisplayName[Grade de Dados em Cima/Formulário em Baixo]")]
					public static DomainKeyPair TopGridLayout_BottomColumnsLayout { get { return _TopGridLayout_BottomColumnsLayout; } }
				    
					private static DomainKeyPair _RightGridLayout_LeftColumnsLayout = new DomainKeyPair() { Value = "5", DisplayName = "Grade de Dados à Direita/Formulário à Esquerda" };
					[FunctionalPoint("Value[5];DisplayName[Grade de Dados à Direita/Formulário à Esquerda]")]
					public static DomainKeyPair RightGridLayout_LeftColumnsLayout { get { return _RightGridLayout_LeftColumnsLayout; } }
				    
					private static DomainKeyPair _BottomGridLayout_TopColumnsLayout = new DomainKeyPair() { Value = "6", DisplayName = "Grade de Dados em Baixo/Formulário em Cima" };
					[FunctionalPoint("Value[6];DisplayName[Grade de Dados em Baixo/Formulário em Cima]")]
					public static DomainKeyPair BottomGridLayout_TopColumnsLayout { get { return _BottomGridLayout_TopColumnsLayout; } }
				    
					private static DomainKeyPair _Default = new DomainKeyPair() { Value = "7", DisplayName = "Padrão" };
					[FunctionalPoint("Value[7];DisplayName[Padrão]")]
					public static DomainKeyPair Default { get { return _Default; } }
				    
			#endregion properties

		

	}    
			
    public partial class IdAplicativo
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "UX"); 
				    
					result.Add("3", "POS"); 
				    
					result.Add("5", "CRM Mobile"); 
				    
					result.Add("6", "ETL"); 
				    
					result.Add("7", "Mobile"); 
				    
					result.Add("8", "Excel"); 
				    
					result.Add("9", "Sites Loyalty"); 
				    
					result.Add("10", "Carga Dados CRM"); 
				    
					result.Add("11", "MID"); 
				    
					result.Add("12", "Serviço de Mídias"); 
				    
					result.Add("13", "Linx Shop"); 
				    
					result.Add("14", "Ensemble"); 
				    
					result.Add("15", "Linx Services"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "LINXUX"); 
				    
					result.Add("3", "LINXPOS"); 
				    
					result.Add("5", "LINXCRMMOBILE"); 
				    
					result.Add("6", "LINXETL"); 
				    
					result.Add("7", "LinxMobile"); 
				    
					result.Add("8", "LinxExcel"); 
				    
					result.Add("9", "Loyalty"); 
				    
					result.Add("10", "CargaDados"); 
				    
					result.Add("11", "MID"); 
				    
					result.Add("12", "MediaService"); 
				    
					result.Add("13", "LinxShop"); 
				    
					result.Add("14", "Ensemble"); 
				    
					result.Add("15", "MIDServices"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _LINXUX = new DomainKeyPair() { Value = "1", DisplayName = "UX" };
					[FunctionalPoint("Value[1];DisplayName[UX]")]
					public static DomainKeyPair LINXUX { get { return _LINXUX; } }
				    
					private static DomainKeyPair _LINXPOS = new DomainKeyPair() { Value = "3", DisplayName = "POS" };
					[FunctionalPoint("Value[3];DisplayName[POS]")]
					public static DomainKeyPair LINXPOS { get { return _LINXPOS; } }
				    
					private static DomainKeyPair _LINXCRMMOBILE = new DomainKeyPair() { Value = "5", DisplayName = "CRM Mobile" };
					[FunctionalPoint("Value[5];DisplayName[CRM Mobile]")]
					public static DomainKeyPair LINXCRMMOBILE { get { return _LINXCRMMOBILE; } }
				    
					private static DomainKeyPair _LINXETL = new DomainKeyPair() { Value = "6", DisplayName = "ETL" };
					[FunctionalPoint("Value[6];DisplayName[ETL]")]
					public static DomainKeyPair LINXETL { get { return _LINXETL; } }
				    
					private static DomainKeyPair _LinxMobile = new DomainKeyPair() { Value = "7", DisplayName = "Mobile" };
					[FunctionalPoint("Value[7];DisplayName[Mobile]")]
					public static DomainKeyPair LinxMobile { get { return _LinxMobile; } }
				    
					private static DomainKeyPair _LinxExcel = new DomainKeyPair() { Value = "8", DisplayName = "Excel" };
					[FunctionalPoint("Value[8];DisplayName[Excel]")]
					public static DomainKeyPair LinxExcel { get { return _LinxExcel; } }
				    
					private static DomainKeyPair _Loyalty = new DomainKeyPair() { Value = "9", DisplayName = "Sites Loyalty" };
					[FunctionalPoint("Value[9];DisplayName[Sites Loyalty]")]
					public static DomainKeyPair Loyalty { get { return _Loyalty; } }
				    
					private static DomainKeyPair _CargaDados = new DomainKeyPair() { Value = "10", DisplayName = "Carga Dados CRM" };
					[FunctionalPoint("Value[10];DisplayName[Carga Dados CRM]")]
					public static DomainKeyPair CargaDados { get { return _CargaDados; } }
				    
					private static DomainKeyPair _MID = new DomainKeyPair() { Value = "11", DisplayName = "MID" };
					[FunctionalPoint("Value[11];DisplayName[MID]")]
					public static DomainKeyPair MID { get { return _MID; } }
				    
					private static DomainKeyPair _MediaService = new DomainKeyPair() { Value = "12", DisplayName = "Serviço de Mídias" };
					[FunctionalPoint("Value[12];DisplayName[Serviço de Mídias]")]
					public static DomainKeyPair MediaService { get { return _MediaService; } }
				    
					private static DomainKeyPair _LinxShop = new DomainKeyPair() { Value = "13", DisplayName = "Linx Shop" };
					[FunctionalPoint("Value[13];DisplayName[Linx Shop]")]
					public static DomainKeyPair LinxShop { get { return _LinxShop; } }
				    
					private static DomainKeyPair _Ensemble = new DomainKeyPair() { Value = "14", DisplayName = "Ensemble" };
					[FunctionalPoint("Value[14];DisplayName[Ensemble]")]
					public static DomainKeyPair Ensemble { get { return _Ensemble; } }
				    
					private static DomainKeyPair _MIDServices = new DomainKeyPair() { Value = "15", DisplayName = "Linx Services" };
					[FunctionalPoint("Value[15];DisplayName[Linx Services]")]
					public static DomainKeyPair MIDServices { get { return _MIDServices; } }
				    
			#endregion properties

		

	}    
			
    public partial class UsoMultimidia
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Catálogo"); 
				    
					result.Add("2", "Detalhe"); 
				    
					result.Add("3", "Miniatura"); 
				    
					result.Add("4", "Zoom de Lente"); 
				    
					result.Add("5", "Zoom Ampliado"); 
				    
					result.Add("8", "Matriz Mínima"); 
				    
					result.Add("9", "Look View"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Catalogo"); 
				    
					result.Add("2", "Detalhe"); 
				    
					result.Add("3", "Miniatura"); 
				    
					result.Add("4", "ZoomLente"); 
				    
					result.Add("5", "ZoomAmpliado"); 
				    
					result.Add("8", "MatrizMinima"); 
				    
					result.Add("9", "LookView"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Catalogo = new DomainKeyPair() { Value = "1", DisplayName = "Catálogo" };
					[FunctionalPoint("Value[1];DisplayName[Catálogo]")]
					public static DomainKeyPair Catalogo { get { return _Catalogo; } }
				    
					private static DomainKeyPair _Detalhe = new DomainKeyPair() { Value = "2", DisplayName = "Detalhe" };
					[FunctionalPoint("Value[2];DisplayName[Detalhe]")]
					public static DomainKeyPair Detalhe { get { return _Detalhe; } }
				    
					private static DomainKeyPair _Miniatura = new DomainKeyPair() { Value = "3", DisplayName = "Miniatura" };
					[FunctionalPoint("Value[3];DisplayName[Miniatura]")]
					public static DomainKeyPair Miniatura { get { return _Miniatura; } }
				    
					private static DomainKeyPair _ZoomLente = new DomainKeyPair() { Value = "4", DisplayName = "Zoom de Lente" };
					[FunctionalPoint("Value[4];DisplayName[Zoom de Lente]")]
					public static DomainKeyPair ZoomLente { get { return _ZoomLente; } }
				    
					private static DomainKeyPair _ZoomAmpliado = new DomainKeyPair() { Value = "5", DisplayName = "Zoom Ampliado" };
					[FunctionalPoint("Value[5];DisplayName[Zoom Ampliado]")]
					public static DomainKeyPair ZoomAmpliado { get { return _ZoomAmpliado; } }
				    
					private static DomainKeyPair _MatrizMinima = new DomainKeyPair() { Value = "8", DisplayName = "Matriz Mínima" };
					[FunctionalPoint("Value[8];DisplayName[Matriz Mínima]")]
					public static DomainKeyPair MatrizMinima { get { return _MatrizMinima; } }
				    
					private static DomainKeyPair _LookView = new DomainKeyPair() { Value = "9", DisplayName = "Look View" };
					[FunctionalPoint("Value[9];DisplayName[Look View]")]
					public static DomainKeyPair LookView { get { return _LookView; } }
				    
			#endregion properties

		

	}    
			
    public partial class TipoFiltro
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Filtro BV"); 
				    
					result.Add("2", "Filtro BM"); 
				    
					result.Add("3", "Filtro UI"); 
				    
					result.Add("4", "Filtro Temporário"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "FiltroBV"); 
				    
					result.Add("2", "FiltroBm"); 
				    
					result.Add("3", "FiltroUI"); 
				    
					result.Add("4", "FiltroTemporario"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _FiltroBV = new DomainKeyPair() { Value = "1", DisplayName = "Filtro BV" };
					[FunctionalPoint("Value[1];DisplayName[Filtro BV]")]
					public static DomainKeyPair FiltroBV { get { return _FiltroBV; } }
				    
					private static DomainKeyPair _FiltroBm = new DomainKeyPair() { Value = "2", DisplayName = "Filtro BM" };
					[FunctionalPoint("Value[2];DisplayName[Filtro BM]")]
					public static DomainKeyPair FiltroBm { get { return _FiltroBm; } }
				    
					private static DomainKeyPair _FiltroUI = new DomainKeyPair() { Value = "3", DisplayName = "Filtro UI" };
					[FunctionalPoint("Value[3];DisplayName[Filtro UI]")]
					public static DomainKeyPair FiltroUI { get { return _FiltroUI; } }
				    
					private static DomainKeyPair _FiltroTemporario = new DomainKeyPair() { Value = "4", DisplayName = "Filtro Temporário" };
					[FunctionalPoint("Value[4];DisplayName[Filtro Temporário]")]
					public static DomainKeyPair FiltroTemporario { get { return _FiltroTemporario; } }
				    
			#endregion properties

		

	}    
			
    public partial class FilterOperator
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("NOT IN", "Not In"); 
				    
					result.Add("IN", "In"); 
				    
					result.Add("NOT LIKE", "Not Like"); 
				    
					result.Add("LIKE", "Like"); 
				    
					result.Add("!=", "!="); 
				    
					result.Add("NOT BETWEEN", "Not Between"); 
				    
					result.Add("BETWEEN", "Between"); 
				    
					result.Add("IS NOT NULL", "Not Null"); 
				    
					result.Add("IS NULL", "Null"); 
				    
					result.Add("<=", "<="); 
				    
					result.Add("<", "<"); 
				    
					result.Add(">=", ">="); 
				    
					result.Add(">", ">"); 
				    
					result.Add("=", "="); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("NOT IN", "NotIn"); 
				    
					result.Add("IN", "InList"); 
				    
					result.Add("NOT LIKE", "NotLike"); 
				    
					result.Add("LIKE", "Like"); 
				    
					result.Add("!=", "NotEqual"); 
				    
					result.Add("NOT BETWEEN", "NotBetween"); 
				    
					result.Add("BETWEEN", "Between"); 
				    
					result.Add("IS NOT NULL", "IsNotNull"); 
				    
					result.Add("IS NULL", "IsNull"); 
				    
					result.Add("<=", "LessThenOrEqualTo"); 
				    
					result.Add("<", "LessThen"); 
				    
					result.Add(">=", "GreaterThenOrEqualTo"); 
				    
					result.Add(">", "GreaterThen"); 
				    
					result.Add("=", "IsEquals"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _NotIn = new DomainKeyPair() { Value = "NOT IN", DisplayName = "Not In" };
					[FunctionalPoint("Value[NOT IN];DisplayName[Not In]")]
					public static DomainKeyPair NotIn { get { return _NotIn; } }
				    
					private static DomainKeyPair _InList = new DomainKeyPair() { Value = "IN", DisplayName = "In" };
					[FunctionalPoint("Value[IN];DisplayName[In]")]
					public static DomainKeyPair InList { get { return _InList; } }
				    
					private static DomainKeyPair _NotLike = new DomainKeyPair() { Value = "NOT LIKE", DisplayName = "Not Like" };
					[FunctionalPoint("Value[NOT LIKE];DisplayName[Not Like]")]
					public static DomainKeyPair NotLike { get { return _NotLike; } }
				    
					private static DomainKeyPair _Like = new DomainKeyPair() { Value = "LIKE", DisplayName = "Like" };
					[FunctionalPoint("Value[LIKE];DisplayName[Like]")]
					public static DomainKeyPair Like { get { return _Like; } }
				    
					private static DomainKeyPair _NotEqual = new DomainKeyPair() { Value = "!=", DisplayName = "!=" };
					[FunctionalPoint("Value[!=];DisplayName[!=]")]
					public static DomainKeyPair NotEqual { get { return _NotEqual; } }
				    
					private static DomainKeyPair _NotBetween = new DomainKeyPair() { Value = "NOT BETWEEN", DisplayName = "Not Between" };
					[FunctionalPoint("Value[NOT BETWEEN];DisplayName[Not Between]")]
					public static DomainKeyPair NotBetween { get { return _NotBetween; } }
				    
					private static DomainKeyPair _Between = new DomainKeyPair() { Value = "BETWEEN", DisplayName = "Between" };
					[FunctionalPoint("Value[BETWEEN];DisplayName[Between]")]
					public static DomainKeyPair Between { get { return _Between; } }
				    
					private static DomainKeyPair _IsNotNull = new DomainKeyPair() { Value = "IS NOT NULL", DisplayName = "Not Null" };
					[FunctionalPoint("Value[IS NOT NULL];DisplayName[Not Null]")]
					public static DomainKeyPair IsNotNull { get { return _IsNotNull; } }
				    
					private static DomainKeyPair _IsNull = new DomainKeyPair() { Value = "IS NULL", DisplayName = "Null" };
					[FunctionalPoint("Value[IS NULL];DisplayName[Null]")]
					public static DomainKeyPair IsNull { get { return _IsNull; } }
				    
					private static DomainKeyPair _LessThenOrEqualTo = new DomainKeyPair() { Value = "<=", DisplayName = "<=" };
					[FunctionalPoint("Value[<=];DisplayName[<=]")]
					public static DomainKeyPair LessThenOrEqualTo { get { return _LessThenOrEqualTo; } }
				    
					private static DomainKeyPair _LessThen = new DomainKeyPair() { Value = "<", DisplayName = "<" };
					[FunctionalPoint("Value[<];DisplayName[<]")]
					public static DomainKeyPair LessThen { get { return _LessThen; } }
				    
					private static DomainKeyPair _GreaterThenOrEqualTo = new DomainKeyPair() { Value = ">=", DisplayName = ">=" };
					[FunctionalPoint("Value[>=];DisplayName[>=]")]
					public static DomainKeyPair GreaterThenOrEqualTo { get { return _GreaterThenOrEqualTo; } }
				    
					private static DomainKeyPair _GreaterThen = new DomainKeyPair() { Value = ">", DisplayName = ">" };
					[FunctionalPoint("Value[>];DisplayName[>]")]
					public static DomainKeyPair GreaterThen { get { return _GreaterThen; } }
				    
					private static DomainKeyPair _IsEquals = new DomainKeyPair() { Value = "=", DisplayName = "=" };
					[FunctionalPoint("Value[=];DisplayName[=]")]
					public static DomainKeyPair IsEquals { get { return _IsEquals; } }
				    
			#endregion properties

		

	}    
			
    public partial class FilterCondition
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("!", "Not"); 
				    
					result.Add("||", "Or"); 
				    
					result.Add("&&", "And"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("!", "Not"); 
				    
					result.Add("||", "Or"); 
				    
					result.Add("&&", "And"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Not = new DomainKeyPair() { Value = "!", DisplayName = "Not" };
					[FunctionalPoint("Value[!];DisplayName[Not]")]
					public static DomainKeyPair Not { get { return _Not; } }
				    
					private static DomainKeyPair _Or = new DomainKeyPair() { Value = "||", DisplayName = "Or" };
					[FunctionalPoint("Value[||];DisplayName[Or]")]
					public static DomainKeyPair Or { get { return _Or; } }
				    
					private static DomainKeyPair _And = new DomainKeyPair() { Value = "&&", DisplayName = "And" };
					[FunctionalPoint("Value[&&];DisplayName[And]")]
					public static DomainKeyPair And { get { return _And; } }
				    
			#endregion properties

		

	}    
			
    public partial class TipoVerboHttp
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Get"); 
				    
					result.Add("2", "Post"); 
				    
					result.Add("3", "Put"); 
				    
					result.Add("4", "Patch"); 
				    
					result.Add("5", "Delete"); 
				    
					result.Add("6", "Copy"); 
				    
					result.Add("7", "Head"); 
				    
					result.Add("8", "Options"); 
				    
					result.Add("9", "Link"); 
				    
					result.Add("10", "Unlink"); 
				    
					result.Add("11", "Purge"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "HttpGet"); 
				    
					result.Add("2", "HttpPost"); 
				    
					result.Add("3", "HttpPut"); 
				    
					result.Add("4", "HttpPatch"); 
				    
					result.Add("5", "HttpDelete"); 
				    
					result.Add("6", "HttpCopy"); 
				    
					result.Add("7", "HttpHead"); 
				    
					result.Add("8", "HttpOptions"); 
				    
					result.Add("9", "HttpLink"); 
				    
					result.Add("10", "HttpUnlink"); 
				    
					result.Add("11", "HttpPurge"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _HttpGet = new DomainKeyPair() { Value = "1", DisplayName = "Get" };
					[FunctionalPoint("Value[1];DisplayName[Get]")]
					public static DomainKeyPair HttpGet { get { return _HttpGet; } }
				    
					private static DomainKeyPair _HttpPost = new DomainKeyPair() { Value = "2", DisplayName = "Post" };
					[FunctionalPoint("Value[2];DisplayName[Post]")]
					public static DomainKeyPair HttpPost { get { return _HttpPost; } }
				    
					private static DomainKeyPair _HttpPut = new DomainKeyPair() { Value = "3", DisplayName = "Put" };
					[FunctionalPoint("Value[3];DisplayName[Put]")]
					public static DomainKeyPair HttpPut { get { return _HttpPut; } }
				    
					private static DomainKeyPair _HttpPatch = new DomainKeyPair() { Value = "4", DisplayName = "Patch" };
					[FunctionalPoint("Value[4];DisplayName[Patch]")]
					public static DomainKeyPair HttpPatch { get { return _HttpPatch; } }
				    
					private static DomainKeyPair _HttpDelete = new DomainKeyPair() { Value = "5", DisplayName = "Delete" };
					[FunctionalPoint("Value[5];DisplayName[Delete]")]
					public static DomainKeyPair HttpDelete { get { return _HttpDelete; } }
				    
					private static DomainKeyPair _HttpCopy = new DomainKeyPair() { Value = "6", DisplayName = "Copy" };
					[FunctionalPoint("Value[6];DisplayName[Copy]")]
					public static DomainKeyPair HttpCopy { get { return _HttpCopy; } }
				    
					private static DomainKeyPair _HttpHead = new DomainKeyPair() { Value = "7", DisplayName = "Head" };
					[FunctionalPoint("Value[7];DisplayName[Head]")]
					public static DomainKeyPair HttpHead { get { return _HttpHead; } }
				    
					private static DomainKeyPair _HttpOptions = new DomainKeyPair() { Value = "8", DisplayName = "Options" };
					[FunctionalPoint("Value[8];DisplayName[Options]")]
					public static DomainKeyPair HttpOptions { get { return _HttpOptions; } }
				    
					private static DomainKeyPair _HttpLink = new DomainKeyPair() { Value = "9", DisplayName = "Link" };
					[FunctionalPoint("Value[9];DisplayName[Link]")]
					public static DomainKeyPair HttpLink { get { return _HttpLink; } }
				    
					private static DomainKeyPair _HttpUnlink = new DomainKeyPair() { Value = "10", DisplayName = "Unlink" };
					[FunctionalPoint("Value[10];DisplayName[Unlink]")]
					public static DomainKeyPair HttpUnlink { get { return _HttpUnlink; } }
				    
					private static DomainKeyPair _HttpPurge = new DomainKeyPair() { Value = "11", DisplayName = "Purge" };
					[FunctionalPoint("Value[11];DisplayName[Purge]")]
					public static DomainKeyPair HttpPurge { get { return _HttpPurge; } }
				    
			#endregion properties

		

	}    
			
    public partial class TipoProcedimento
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Procedure"); 
				    
					result.Add("2", "Função"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "StoredProcedure"); 
				    
					result.Add("2", "Function"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _StoredProcedure = new DomainKeyPair() { Value = "1", DisplayName = "Procedure" };
					[FunctionalPoint("Value[1];DisplayName[Procedure]")]
					public static DomainKeyPair StoredProcedure { get { return _StoredProcedure; } }
				    
					private static DomainKeyPair _Function = new DomainKeyPair() { Value = "2", DisplayName = "Função" };
					[FunctionalPoint("Value[2];DisplayName[Função]")]
					public static DomainKeyPair Function { get { return _Function; } }
				    
			#endregion properties

		

	}    
			
    public partial class OrigemValorParametro
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Informação da Origem"); 
				    
					result.Add("2", "Parâmetro do Sistema"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Origem"); 
				    
					result.Add("2", "Parametro"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Origem = new DomainKeyPair() { Value = "1", DisplayName = "Informação da Origem" };
					[FunctionalPoint("Value[1];DisplayName[Informação da Origem]")]
					public static DomainKeyPair Origem { get { return _Origem; } }
				    
					private static DomainKeyPair _Parametro = new DomainKeyPair() { Value = "2", DisplayName = "Parâmetro do Sistema" };
					[FunctionalPoint("Value[2];DisplayName[Parâmetro do Sistema]")]
					public static DomainKeyPair Parametro { get { return _Parametro; } }
				    
			#endregion properties

		

	}    
			
    public partial class TamanhoApresentacao
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Normal"); 
				    
					result.Add("2", "Double"); 
				    
					result.Add("3", "Double-Down"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "SizeRegular"); 
				    
					result.Add("2", "SizeDouble"); 
				    
					result.Add("3", "SizeDoubleDown"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _SizeRegular = new DomainKeyPair() { Value = "1", DisplayName = "Normal" };
					[FunctionalPoint("Value[1];DisplayName[Normal]")]
					public static DomainKeyPair SizeRegular { get { return _SizeRegular; } }
				    
					private static DomainKeyPair _SizeDouble = new DomainKeyPair() { Value = "2", DisplayName = "Double" };
					[FunctionalPoint("Value[2];DisplayName[Double]")]
					public static DomainKeyPair SizeDouble { get { return _SizeDouble; } }
				    
					private static DomainKeyPair _SizeDoubleDown = new DomainKeyPair() { Value = "3", DisplayName = "Double-Down" };
					[FunctionalPoint("Value[3];DisplayName[Double-Down]")]
					public static DomainKeyPair SizeDoubleDown { get { return _SizeDoubleDown; } }
				    
			#endregion properties

		

	}    
			
    public partial class CorFundo
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("7", "Laranja"); 
				    
					result.Add("8", "Fundo Laranja"); 
				    
					result.Add("9", "Roxo"); 
				    
					result.Add("10", "Fundo Roxo"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("7", "laranja"); 
				    
					result.Add("8", "fundo_laranja"); 
				    
					result.Add("9", "roxo"); 
				    
					result.Add("10", "fundo_roxo"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _laranja = new DomainKeyPair() { Value = "7", DisplayName = "Laranja" };
					[FunctionalPoint("Value[7];DisplayName[Laranja]")]
					public static DomainKeyPair laranja { get { return _laranja; } }
				    
					private static DomainKeyPair _fundo_laranja = new DomainKeyPair() { Value = "8", DisplayName = "Fundo Laranja" };
					[FunctionalPoint("Value[8];DisplayName[Fundo Laranja]")]
					public static DomainKeyPair fundo_laranja { get { return _fundo_laranja; } }
				    
					private static DomainKeyPair _roxo = new DomainKeyPair() { Value = "9", DisplayName = "Roxo" };
					[FunctionalPoint("Value[9];DisplayName[Roxo]")]
					public static DomainKeyPair roxo { get { return _roxo; } }
				    
					private static DomainKeyPair _fundo_roxo = new DomainKeyPair() { Value = "10", DisplayName = "Fundo Roxo" };
					[FunctionalPoint("Value[10];DisplayName[Fundo Roxo]")]
					public static DomainKeyPair fundo_roxo { get { return _fundo_roxo; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_PFJ_FISICA_JURIDICA
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Pessoa Física"); 
				    
					result.Add("2", "Pessoa Jurídica"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "FISICA"); 
				    
					result.Add("2", "JURIDICA"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _FISICA = new DomainKeyPair() { Value = "1", DisplayName = "Pessoa Física" };
					[FunctionalPoint("Value[1];DisplayName[Pessoa Física]")]
					public static DomainKeyPair FISICA { get { return _FISICA; } }
				    
					private static DomainKeyPair _JURIDICA = new DomainKeyPair() { Value = "2", DisplayName = "Pessoa Jurídica" };
					[FunctionalPoint("Value[2];DisplayName[Pessoa Jurídica]")]
					public static DomainKeyPair JURIDICA { get { return _JURIDICA; } }
				    
			#endregion properties

		

	}    
			
    public partial class LxTipoLogradouro
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Aeroporto"); 
				    
					result.Add("2", "Alameda"); 
				    
					result.Add("3", "Apartamento"); 
				    
					result.Add("4", "Avenida"); 
				    
					result.Add("5", "Beco"); 
				    
					result.Add("6", "Bloco"); 
				    
					result.Add("7", "Caminho"); 
				    
					result.Add("8", "Escadinha"); 
				    
					result.Add("9", "Estação"); 
				    
					result.Add("10", "Estrada"); 
				    
					result.Add("11", "Fazenda"); 
				    
					result.Add("12", "Fortaleza"); 
				    
					result.Add("13", "Galeria"); 
				    
					result.Add("14", "Ladeira"); 
				    
					result.Add("15", "Largo"); 
				    
					result.Add("16", "Praça"); 
				    
					result.Add("17", "Parque"); 
				    
					result.Add("18", "Praia"); 
				    
					result.Add("19", "Quadra"); 
				    
					result.Add("20", "Quilômetro"); 
				    
					result.Add("21", "Quinta"); 
				    
					result.Add("22", "Rodovia"); 
				    
					result.Add("23", "Rua"); 
				    
					result.Add("24", "Super Quadra"); 
				    
					result.Add("25", "Travessa"); 
				    
					result.Add("26", "Viaduto"); 
				    
					result.Add("27", "Vila"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Aeroporto"); 
				    
					result.Add("2", "Alameda"); 
				    
					result.Add("3", "Apartamento"); 
				    
					result.Add("4", "Avenida"); 
				    
					result.Add("5", "Beco"); 
				    
					result.Add("6", "Bloco"); 
				    
					result.Add("7", "Caminho"); 
				    
					result.Add("8", "Escadinha"); 
				    
					result.Add("9", "Estacao"); 
				    
					result.Add("10", "Estrada"); 
				    
					result.Add("11", "Fazenda"); 
				    
					result.Add("12", "Fortaleza"); 
				    
					result.Add("13", "Galeria"); 
				    
					result.Add("14", "Ladeira"); 
				    
					result.Add("15", "Largo"); 
				    
					result.Add("16", "Praca"); 
				    
					result.Add("17", "Parque"); 
				    
					result.Add("18", "Praia"); 
				    
					result.Add("19", "Quadra"); 
				    
					result.Add("20", "Quilometro"); 
				    
					result.Add("21", "Quinta"); 
				    
					result.Add("22", "Rodovia"); 
				    
					result.Add("23", "Rua"); 
				    
					result.Add("24", "SuperQuadra"); 
				    
					result.Add("25", "Travessa"); 
				    
					result.Add("26", "Viaduto"); 
				    
					result.Add("27", "Vila"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Aeroporto = new DomainKeyPair() { Value = "1", DisplayName = "Aeroporto" };
					[FunctionalPoint("Value[1];DisplayName[Aeroporto]")]
					public static DomainKeyPair Aeroporto { get { return _Aeroporto; } }
				    
					private static DomainKeyPair _Alameda = new DomainKeyPair() { Value = "2", DisplayName = "Alameda" };
					[FunctionalPoint("Value[2];DisplayName[Alameda]")]
					public static DomainKeyPair Alameda { get { return _Alameda; } }
				    
					private static DomainKeyPair _Apartamento = new DomainKeyPair() { Value = "3", DisplayName = "Apartamento" };
					[FunctionalPoint("Value[3];DisplayName[Apartamento]")]
					public static DomainKeyPair Apartamento { get { return _Apartamento; } }
				    
					private static DomainKeyPair _Avenida = new DomainKeyPair() { Value = "4", DisplayName = "Avenida" };
					[FunctionalPoint("Value[4];DisplayName[Avenida]")]
					public static DomainKeyPair Avenida { get { return _Avenida; } }
				    
					private static DomainKeyPair _Beco = new DomainKeyPair() { Value = "5", DisplayName = "Beco" };
					[FunctionalPoint("Value[5];DisplayName[Beco]")]
					public static DomainKeyPair Beco { get { return _Beco; } }
				    
					private static DomainKeyPair _Bloco = new DomainKeyPair() { Value = "6", DisplayName = "Bloco" };
					[FunctionalPoint("Value[6];DisplayName[Bloco]")]
					public static DomainKeyPair Bloco { get { return _Bloco; } }
				    
					private static DomainKeyPair _Caminho = new DomainKeyPair() { Value = "7", DisplayName = "Caminho" };
					[FunctionalPoint("Value[7];DisplayName[Caminho]")]
					public static DomainKeyPair Caminho { get { return _Caminho; } }
				    
					private static DomainKeyPair _Escadinha = new DomainKeyPair() { Value = "8", DisplayName = "Escadinha" };
					[FunctionalPoint("Value[8];DisplayName[Escadinha]")]
					public static DomainKeyPair Escadinha { get { return _Escadinha; } }
				    
					private static DomainKeyPair _Estacao = new DomainKeyPair() { Value = "9", DisplayName = "Estação" };
					[FunctionalPoint("Value[9];DisplayName[Estação]")]
					public static DomainKeyPair Estacao { get { return _Estacao; } }
				    
					private static DomainKeyPair _Estrada = new DomainKeyPair() { Value = "10", DisplayName = "Estrada" };
					[FunctionalPoint("Value[10];DisplayName[Estrada]")]
					public static DomainKeyPair Estrada { get { return _Estrada; } }
				    
					private static DomainKeyPair _Fazenda = new DomainKeyPair() { Value = "11", DisplayName = "Fazenda" };
					[FunctionalPoint("Value[11];DisplayName[Fazenda]")]
					public static DomainKeyPair Fazenda { get { return _Fazenda; } }
				    
					private static DomainKeyPair _Fortaleza = new DomainKeyPair() { Value = "12", DisplayName = "Fortaleza" };
					[FunctionalPoint("Value[12];DisplayName[Fortaleza]")]
					public static DomainKeyPair Fortaleza { get { return _Fortaleza; } }
				    
					private static DomainKeyPair _Galeria = new DomainKeyPair() { Value = "13", DisplayName = "Galeria" };
					[FunctionalPoint("Value[13];DisplayName[Galeria]")]
					public static DomainKeyPair Galeria { get { return _Galeria; } }
				    
					private static DomainKeyPair _Ladeira = new DomainKeyPair() { Value = "14", DisplayName = "Ladeira" };
					[FunctionalPoint("Value[14];DisplayName[Ladeira]")]
					public static DomainKeyPair Ladeira { get { return _Ladeira; } }
				    
					private static DomainKeyPair _Largo = new DomainKeyPair() { Value = "15", DisplayName = "Largo" };
					[FunctionalPoint("Value[15];DisplayName[Largo]")]
					public static DomainKeyPair Largo { get { return _Largo; } }
				    
					private static DomainKeyPair _Praca = new DomainKeyPair() { Value = "16", DisplayName = "Praça" };
					[FunctionalPoint("Value[16];DisplayName[Praça]")]
					public static DomainKeyPair Praca { get { return _Praca; } }
				    
					private static DomainKeyPair _Parque = new DomainKeyPair() { Value = "17", DisplayName = "Parque" };
					[FunctionalPoint("Value[17];DisplayName[Parque]")]
					public static DomainKeyPair Parque { get { return _Parque; } }
				    
					private static DomainKeyPair _Praia = new DomainKeyPair() { Value = "18", DisplayName = "Praia" };
					[FunctionalPoint("Value[18];DisplayName[Praia]")]
					public static DomainKeyPair Praia { get { return _Praia; } }
				    
					private static DomainKeyPair _Quadra = new DomainKeyPair() { Value = "19", DisplayName = "Quadra" };
					[FunctionalPoint("Value[19];DisplayName[Quadra]")]
					public static DomainKeyPair Quadra { get { return _Quadra; } }
				    
					private static DomainKeyPair _Quilometro = new DomainKeyPair() { Value = "20", DisplayName = "Quilômetro" };
					[FunctionalPoint("Value[20];DisplayName[Quilômetro]")]
					public static DomainKeyPair Quilometro { get { return _Quilometro; } }
				    
					private static DomainKeyPair _Quinta = new DomainKeyPair() { Value = "21", DisplayName = "Quinta" };
					[FunctionalPoint("Value[21];DisplayName[Quinta]")]
					public static DomainKeyPair Quinta { get { return _Quinta; } }
				    
					private static DomainKeyPair _Rodovia = new DomainKeyPair() { Value = "22", DisplayName = "Rodovia" };
					[FunctionalPoint("Value[22];DisplayName[Rodovia]")]
					public static DomainKeyPair Rodovia { get { return _Rodovia; } }
				    
					private static DomainKeyPair _Rua = new DomainKeyPair() { Value = "23", DisplayName = "Rua" };
					[FunctionalPoint("Value[23];DisplayName[Rua]")]
					public static DomainKeyPair Rua { get { return _Rua; } }
				    
					private static DomainKeyPair _SuperQuadra = new DomainKeyPair() { Value = "24", DisplayName = "Super Quadra" };
					[FunctionalPoint("Value[24];DisplayName[Super Quadra]")]
					public static DomainKeyPair SuperQuadra { get { return _SuperQuadra; } }
				    
					private static DomainKeyPair _Travessa = new DomainKeyPair() { Value = "25", DisplayName = "Travessa" };
					[FunctionalPoint("Value[25];DisplayName[Travessa]")]
					public static DomainKeyPair Travessa { get { return _Travessa; } }
				    
					private static DomainKeyPair _Viaduto = new DomainKeyPair() { Value = "26", DisplayName = "Viaduto" };
					[FunctionalPoint("Value[26];DisplayName[Viaduto]")]
					public static DomainKeyPair Viaduto { get { return _Viaduto; } }
				    
					private static DomainKeyPair _Vila = new DomainKeyPair() { Value = "27", DisplayName = "Vila" };
					[FunctionalPoint("Value[27];DisplayName[Vila]")]
					public static DomainKeyPair Vila { get { return _Vila; } }
				    
			#endregion properties

		

	}    
			
    public partial class TipoMidia
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Imagem"); 
				    
					result.Add("2", "Vídeo"); 
				    
					result.Add("3", "Documento"); 
				    
					result.Add("4", "Outros"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Imagem"); 
				    
					result.Add("2", "Video"); 
				    
					result.Add("3", "Documento"); 
				    
					result.Add("4", "Outros"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Imagem = new DomainKeyPair() { Value = "1", DisplayName = "Imagem" };
					[FunctionalPoint("Value[1];DisplayName[Imagem]")]
					public static DomainKeyPair Imagem { get { return _Imagem; } }
				    
					private static DomainKeyPair _Video = new DomainKeyPair() { Value = "2", DisplayName = "Vídeo" };
					[FunctionalPoint("Value[2];DisplayName[Vídeo]")]
					public static DomainKeyPair Video { get { return _Video; } }
				    
					private static DomainKeyPair _Documento = new DomainKeyPair() { Value = "3", DisplayName = "Documento" };
					[FunctionalPoint("Value[3];DisplayName[Documento]")]
					public static DomainKeyPair Documento { get { return _Documento; } }
				    
					private static DomainKeyPair _Outros = new DomainKeyPair() { Value = "4", DisplayName = "Outros" };
					[FunctionalPoint("Value[4];DisplayName[Outros]")]
					public static DomainKeyPair Outros { get { return _Outros; } }
				    
			#endregion properties

		

	}    
			
    public partial class ParametroHierarquia
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("100", "Obrigatório"); 
				    
					result.Add("1", "Variação Nível 1"); 
				    
					result.Add("2", "Variação Nível 2"); 
				    
					result.Add("3", "Variação Nível 3"); 
				    
					result.Add("4", "Variação Nível 4"); 
				    
					result.Add("5", "Variação Nível 5"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("100", "Obrigatorio"); 
				    
					result.Add("1", "VariacaoNivel1"); 
				    
					result.Add("2", "VariacaoNivel2"); 
				    
					result.Add("3", "VariacaoNivel3"); 
				    
					result.Add("4", "VariacaoNivel4"); 
				    
					result.Add("5", "VariacaoNivel5"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Obrigatorio = new DomainKeyPair() { Value = "100", DisplayName = "Obrigatório" };
					[FunctionalPoint("Value[100];DisplayName[Obrigatório]")]
					public static DomainKeyPair Obrigatorio { get { return _Obrigatorio; } }
				    
					private static DomainKeyPair _VariacaoNivel1 = new DomainKeyPair() { Value = "1", DisplayName = "Variação Nível 1" };
					[FunctionalPoint("Value[1];DisplayName[Variação Nível 1]")]
					public static DomainKeyPair VariacaoNivel1 { get { return _VariacaoNivel1; } }
				    
					private static DomainKeyPair _VariacaoNivel2 = new DomainKeyPair() { Value = "2", DisplayName = "Variação Nível 2" };
					[FunctionalPoint("Value[2];DisplayName[Variação Nível 2]")]
					public static DomainKeyPair VariacaoNivel2 { get { return _VariacaoNivel2; } }
				    
					private static DomainKeyPair _VariacaoNivel3 = new DomainKeyPair() { Value = "3", DisplayName = "Variação Nível 3" };
					[FunctionalPoint("Value[3];DisplayName[Variação Nível 3]")]
					public static DomainKeyPair VariacaoNivel3 { get { return _VariacaoNivel3; } }
				    
					private static DomainKeyPair _VariacaoNivel4 = new DomainKeyPair() { Value = "4", DisplayName = "Variação Nível 4" };
					[FunctionalPoint("Value[4];DisplayName[Variação Nível 4]")]
					public static DomainKeyPair VariacaoNivel4 { get { return _VariacaoNivel4; } }
				    
					private static DomainKeyPair _VariacaoNivel5 = new DomainKeyPair() { Value = "5", DisplayName = "Variação Nível 5" };
					[FunctionalPoint("Value[5];DisplayName[Variação Nível 5]")]
					public static DomainKeyPair VariacaoNivel5 { get { return _VariacaoNivel5; } }
				    
			#endregion properties

		

	}    
			
    public partial class TipoAutenticador
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Facebook"); 
				    
					result.Add("2", "Google+"); 
				    
					result.Add("3", "Microsoft Sign In"); 
				    
					result.Add("4", "Linx"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "FACEBOOK"); 
				    
					result.Add("2", "GOOGLE"); 
				    
					result.Add("3", "MICROSOFT"); 
				    
					result.Add("4", "LINX"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _FACEBOOK = new DomainKeyPair() { Value = "1", DisplayName = "Facebook" };
					[FunctionalPoint("Value[1];DisplayName[Facebook]")]
					public static DomainKeyPair FACEBOOK { get { return _FACEBOOK; } }
				    
					private static DomainKeyPair _GOOGLE = new DomainKeyPair() { Value = "2", DisplayName = "Google+" };
					[FunctionalPoint("Value[2];DisplayName[Google+]")]
					public static DomainKeyPair GOOGLE { get { return _GOOGLE; } }
				    
					private static DomainKeyPair _MICROSOFT = new DomainKeyPair() { Value = "3", DisplayName = "Microsoft Sign In" };
					[FunctionalPoint("Value[3];DisplayName[Microsoft Sign In]")]
					public static DomainKeyPair MICROSOFT { get { return _MICROSOFT; } }
				    
					private static DomainKeyPair _LINX = new DomainKeyPair() { Value = "4", DisplayName = "Linx" };
					[FunctionalPoint("Value[4];DisplayName[Linx]")]
					public static DomainKeyPair LINX { get { return _LINX; } }
				    
			#endregion properties

		

	}    
			
    public partial class TipoServidor
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "SQL Server"); 
				    
					result.Add("2", "Oracle"); 
				    
					result.Add("3", "SQLite"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "SQLSERVER"); 
				    
					result.Add("2", "ORACLE"); 
				    
					result.Add("3", "SQLITE"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _SQLSERVER = new DomainKeyPair() { Value = "1", DisplayName = "SQL Server" };
					[FunctionalPoint("Value[1];DisplayName[SQL Server]")]
					public static DomainKeyPair SQLSERVER { get { return _SQLSERVER; } }
				    
					private static DomainKeyPair _ORACLE = new DomainKeyPair() { Value = "2", DisplayName = "Oracle" };
					[FunctionalPoint("Value[2];DisplayName[Oracle]")]
					public static DomainKeyPair ORACLE { get { return _ORACLE; } }
				    
					private static DomainKeyPair _SQLITE = new DomainKeyPair() { Value = "3", DisplayName = "SQLite" };
					[FunctionalPoint("Value[3];DisplayName[SQLite]")]
					public static DomainKeyPair SQLITE { get { return _SQLITE; } }
				    
			#endregion properties

		

	}    
			
    public partial class TipoConteudoObjeto
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Layout"); 
				    
					result.Add("2", "Mídia"); 
				    
					result.Add("3", "Configuração de Exportação para Excel"); 
				    
					result.Add("4", "Configuração de Exportação para Report"); 
				    
					result.Add("5", "Gravação de Layout para Pivot Table"); 
				    
					result.Add("6", "Gravação de Layout para Grid"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Layout"); 
				    
					result.Add("2", "Media"); 
				    
					result.Add("3", "ConfigExportExcel"); 
				    
					result.Add("4", "ConfigExportReport"); 
				    
					result.Add("5", "SaveLayoutPivotTable"); 
				    
					result.Add("6", "GridLayout"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Layout = new DomainKeyPair() { Value = "1", DisplayName = "Layout" };
					[FunctionalPoint("Value[1];DisplayName[Layout]")]
					public static DomainKeyPair Layout { get { return _Layout; } }
				    
					private static DomainKeyPair _Media = new DomainKeyPair() { Value = "2", DisplayName = "Mídia" };
					[FunctionalPoint("Value[2];DisplayName[Mídia]")]
					public static DomainKeyPair Media { get { return _Media; } }
				    
					private static DomainKeyPair _ConfigExportExcel = new DomainKeyPair() { Value = "3", DisplayName = "Configuração de Exportação para Excel" };
					[FunctionalPoint("Value[3];DisplayName[Configuração de Exportação para Excel]")]
					public static DomainKeyPair ConfigExportExcel { get { return _ConfigExportExcel; } }
				    
					private static DomainKeyPair _ConfigExportReport = new DomainKeyPair() { Value = "4", DisplayName = "Configuração de Exportação para Report" };
					[FunctionalPoint("Value[4];DisplayName[Configuração de Exportação para Report]")]
					public static DomainKeyPair ConfigExportReport { get { return _ConfigExportReport; } }
				    
					private static DomainKeyPair _SaveLayoutPivotTable = new DomainKeyPair() { Value = "5", DisplayName = "Gravação de Layout para Pivot Table" };
					[FunctionalPoint("Value[5];DisplayName[Gravação de Layout para Pivot Table]")]
					public static DomainKeyPair SaveLayoutPivotTable { get { return _SaveLayoutPivotTable; } }
				    
					private static DomainKeyPair _GridLayout = new DomainKeyPair() { Value = "6", DisplayName = "Gravação de Layout para Grid" };
					[FunctionalPoint("Value[6];DisplayName[Gravação de Layout para Grid]")]
					public static DomainKeyPair GridLayout { get { return _GridLayout; } }
				    
			#endregion properties

		

	}    

}