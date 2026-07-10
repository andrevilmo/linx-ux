																																																																																																			

using System;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using Linx.Tools; 
    
namespace Linx.Operacional.BM.Domains
{

			
    public partial class LX_TIPO_CARTAO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Cartão de Crédito"); 
				    
					result.Add("2", "Cartão de Débito"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "CartaoCredito"); 
				    
					result.Add("2", "CartaoDebito"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _CartaoCredito = new DomainKeyPair() { Value = "1", DisplayName = "Cartão de Crédito" };
					[FunctionalPoint("Value[1];DisplayName[Cartão de Crédito]")]
					public static DomainKeyPair CartaoCredito { get { return _CartaoCredito; } }
				    
					private static DomainKeyPair _CartaoDebito = new DomainKeyPair() { Value = "2", DisplayName = "Cartão de Débito" };
					[FunctionalPoint("Value[2];DisplayName[Cartão de Débito]")]
					public static DomainKeyPair CartaoDebito { get { return _CartaoDebito; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_STATUS_ARTIGO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Inativo"); 
				    
					result.Add("2", "Em Desenvolvimento"); 
				    
					result.Add("3", "Aguardando Liberação"); 
				    
					result.Add("4", "Liberado"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Inativo"); 
				    
					result.Add("2", "EmDesenvolvimento"); 
				    
					result.Add("3", "AguardandoLiberacao"); 
				    
					result.Add("4", "Liberado"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Inativo = new DomainKeyPair() { Value = "1", DisplayName = "Inativo" };
					[FunctionalPoint("Value[1];DisplayName[Inativo]")]
					public static DomainKeyPair Inativo { get { return _Inativo; } }
				    
					private static DomainKeyPair _EmDesenvolvimento = new DomainKeyPair() { Value = "2", DisplayName = "Em Desenvolvimento" };
					[FunctionalPoint("Value[2];DisplayName[Em Desenvolvimento]")]
					public static DomainKeyPair EmDesenvolvimento { get { return _EmDesenvolvimento; } }
				    
					private static DomainKeyPair _AguardandoLiberacao = new DomainKeyPair() { Value = "3", DisplayName = "Aguardando Liberação" };
					[FunctionalPoint("Value[3];DisplayName[Aguardando Liberação]")]
					public static DomainKeyPair AguardandoLiberacao { get { return _AguardandoLiberacao; } }
				    
					private static DomainKeyPair _Liberado = new DomainKeyPair() { Value = "4", DisplayName = "Liberado" };
					[FunctionalPoint("Value[4];DisplayName[Liberado]")]
					public static DomainKeyPair Liberado { get { return _Liberado; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_CODIGO_ITEM_FISCAL
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Por Artigo"); 
				    
					result.Add("2", "Por Sku"); 
				    
					result.Add("3", "Por GTIN"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Artigo"); 
				    
					result.Add("2", "Sku"); 
				    
					result.Add("3", "GTIN"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Artigo = new DomainKeyPair() { Value = "1", DisplayName = "Por Artigo" };
					[FunctionalPoint("Value[1];DisplayName[Por Artigo]")]
					public static DomainKeyPair Artigo { get { return _Artigo; } }
				    
					private static DomainKeyPair _Sku = new DomainKeyPair() { Value = "2", DisplayName = "Por Sku" };
					[FunctionalPoint("Value[2];DisplayName[Por Sku]")]
					public static DomainKeyPair Sku { get { return _Sku; } }
				    
					private static DomainKeyPair _GTIN = new DomainKeyPair() { Value = "3", DisplayName = "Por GTIN" };
					[FunctionalPoint("Value[3];DisplayName[Por GTIN]")]
					public static DomainKeyPair GTIN { get { return _GTIN; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_GRUPO_RELACAO_TIPO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("10", "Relação Pessoal"); 
				    
					result.Add("15", "Relação Funcional"); 
				    
					result.Add("20", "Relação Empresarial"); 
				    
					result.Add("25", "Relação Financeira"); 
				    
					result.Add("30", "Relação Logística "); 
				    
					result.Add("35", "Relação Transporte"); 
				    
					result.Add("40", "Comercial Venda"); 
				    
					result.Add("45", "Comercial Compra"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("10", "Pessoal"); 
				    
					result.Add("15", "Funcional"); 
				    
					result.Add("20", "Empresarial"); 
				    
					result.Add("25", "Financeira"); 
				    
					result.Add("30", "Logistica"); 
				    
					result.Add("35", "Transportador"); 
				    
					result.Add("40", "ComercialSaida"); 
				    
					result.Add("45", "ComercialEntrada"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Pessoal = new DomainKeyPair() { Value = "10", DisplayName = "Relação Pessoal" };
					[FunctionalPoint("Value[10];DisplayName[Relação Pessoal]")]
					public static DomainKeyPair Pessoal { get { return _Pessoal; } }
				    
					private static DomainKeyPair _Funcional = new DomainKeyPair() { Value = "15", DisplayName = "Relação Funcional" };
					[FunctionalPoint("Value[15];DisplayName[Relação Funcional]")]
					public static DomainKeyPair Funcional { get { return _Funcional; } }
				    
					private static DomainKeyPair _Empresarial = new DomainKeyPair() { Value = "20", DisplayName = "Relação Empresarial" };
					[FunctionalPoint("Value[20];DisplayName[Relação Empresarial]")]
					public static DomainKeyPair Empresarial { get { return _Empresarial; } }
				    
					private static DomainKeyPair _Financeira = new DomainKeyPair() { Value = "25", DisplayName = "Relação Financeira" };
					[FunctionalPoint("Value[25];DisplayName[Relação Financeira]")]
					public static DomainKeyPair Financeira { get { return _Financeira; } }
				    
					private static DomainKeyPair _Logistica = new DomainKeyPair() { Value = "30", DisplayName = "Relação Logística " };
					[FunctionalPoint("Value[30];DisplayName[Relação Logística ]")]
					public static DomainKeyPair Logistica { get { return _Logistica; } }
				    
					private static DomainKeyPair _Transportador = new DomainKeyPair() { Value = "35", DisplayName = "Relação Transporte" };
					[FunctionalPoint("Value[35];DisplayName[Relação Transporte]")]
					public static DomainKeyPair Transportador { get { return _Transportador; } }
				    
					private static DomainKeyPair _ComercialSaida = new DomainKeyPair() { Value = "40", DisplayName = "Comercial Venda" };
					[FunctionalPoint("Value[40];DisplayName[Comercial Venda]")]
					public static DomainKeyPair ComercialSaida { get { return _ComercialSaida; } }
				    
					private static DomainKeyPair _ComercialEntrada = new DomainKeyPair() { Value = "45", DisplayName = "Comercial Compra" };
					[FunctionalPoint("Value[45];DisplayName[Comercial Compra]")]
					public static DomainKeyPair ComercialEntrada { get { return _ComercialEntrada; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_STATUS_LOJA
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Em fase de Abertura"); 
				    
					result.Add("2", "Loja Aberta"); 
				    
					result.Add("3", "Fechamento Temporário"); 
				    
					result.Add("4", "Loja Fechada"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "FaseAbertura"); 
				    
					result.Add("2", "LojaAberta"); 
				    
					result.Add("3", "FechamentoTemporario"); 
				    
					result.Add("4", "LojaFechada"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _FaseAbertura = new DomainKeyPair() { Value = "1", DisplayName = "Em fase de Abertura" };
					[FunctionalPoint("Value[1];DisplayName[Em fase de Abertura]")]
					public static DomainKeyPair FaseAbertura { get { return _FaseAbertura; } }
				    
					private static DomainKeyPair _LojaAberta = new DomainKeyPair() { Value = "2", DisplayName = "Loja Aberta" };
					[FunctionalPoint("Value[2];DisplayName[Loja Aberta]")]
					public static DomainKeyPair LojaAberta { get { return _LojaAberta; } }
				    
					private static DomainKeyPair _FechamentoTemporario = new DomainKeyPair() { Value = "3", DisplayName = "Fechamento Temporário" };
					[FunctionalPoint("Value[3];DisplayName[Fechamento Temporário]")]
					public static DomainKeyPair FechamentoTemporario { get { return _FechamentoTemporario; } }
				    
					private static DomainKeyPair _LojaFechada = new DomainKeyPair() { Value = "4", DisplayName = "Loja Fechada" };
					[FunctionalPoint("Value[4];DisplayName[Loja Fechada]")]
					public static DomainKeyPair LojaFechada { get { return _LojaFechada; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_STATUS_ACAO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Em Planejamento"); 
				    
					result.Add("2", "Aguardando Data de Execução"); 
				    
					result.Add("3", "Em Execução"); 
				    
					result.Add("4", "Encerrada"); 
				    
					result.Add("5", "Cancelada"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Em_Planejamento"); 
				    
					result.Add("2", "Aguardando_Data_Execucao"); 
				    
					result.Add("3", "Em_Execucao"); 
				    
					result.Add("4", "Encerrada"); 
				    
					result.Add("5", "Cancelada"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Em_Planejamento = new DomainKeyPair() { Value = "1", DisplayName = "Em Planejamento" };
					[FunctionalPoint("Value[1];DisplayName[Em Planejamento]")]
					public static DomainKeyPair Em_Planejamento { get { return _Em_Planejamento; } }
				    
					private static DomainKeyPair _Aguardando_Data_Execucao = new DomainKeyPair() { Value = "2", DisplayName = "Aguardando Data de Execução" };
					[FunctionalPoint("Value[2];DisplayName[Aguardando Data de Execução]")]
					public static DomainKeyPair Aguardando_Data_Execucao { get { return _Aguardando_Data_Execucao; } }
				    
					private static DomainKeyPair _Em_Execucao = new DomainKeyPair() { Value = "3", DisplayName = "Em Execução" };
					[FunctionalPoint("Value[3];DisplayName[Em Execução]")]
					public static DomainKeyPair Em_Execucao { get { return _Em_Execucao; } }
				    
					private static DomainKeyPair _Encerrada = new DomainKeyPair() { Value = "4", DisplayName = "Encerrada" };
					[FunctionalPoint("Value[4];DisplayName[Encerrada]")]
					public static DomainKeyPair Encerrada { get { return _Encerrada; } }
				    
					private static DomainKeyPair _Cancelada = new DomainKeyPair() { Value = "5", DisplayName = "Cancelada" };
					[FunctionalPoint("Value[5];DisplayName[Cancelada]")]
					public static DomainKeyPair Cancelada { get { return _Cancelada; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_STATUS_COLETA
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Coleta em Andamento"); 
				    
					result.Add("2", "Coleta Finalizada"); 
				    
					result.Add("4", "Coleta Eleita"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "EmAndamento"); 
				    
					result.Add("2", "ColetaFinalizada"); 
				    
					result.Add("4", "ColetaEleita"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _EmAndamento = new DomainKeyPair() { Value = "1", DisplayName = "Coleta em Andamento" };
					[FunctionalPoint("Value[1];DisplayName[Coleta em Andamento]")]
					public static DomainKeyPair EmAndamento { get { return _EmAndamento; } }
				    
					private static DomainKeyPair _ColetaFinalizada = new DomainKeyPair() { Value = "2", DisplayName = "Coleta Finalizada" };
					[FunctionalPoint("Value[2];DisplayName[Coleta Finalizada]")]
					public static DomainKeyPair ColetaFinalizada { get { return _ColetaFinalizada; } }
				    
					private static DomainKeyPair _ColetaEleita = new DomainKeyPair() { Value = "4", DisplayName = "Coleta Eleita" };
					[FunctionalPoint("Value[4];DisplayName[Coleta Eleita]")]
					public static DomainKeyPair ColetaEleita { get { return _ColetaEleita; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_STATUS_COLETA_ITEM
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Coleta em Andamento"); 
				    
					result.Add("2", "Coleta Finalizada"); 
				    
					result.Add("3", "Coleta Rejeitada"); 
				    
					result.Add("9", "Coleta Processada"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "AguardandoComparacao"); 
				    
					result.Add("2", "ColetaEleita"); 
				    
					result.Add("3", "ColetaRejeitada"); 
				    
					result.Add("9", "ColetaProcessada"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _AguardandoComparacao = new DomainKeyPair() { Value = "1", DisplayName = "Coleta em Andamento" };
					[FunctionalPoint("Value[1];DisplayName[Coleta em Andamento]")]
					public static DomainKeyPair AguardandoComparacao { get { return _AguardandoComparacao; } }
				    
					private static DomainKeyPair _ColetaEleita = new DomainKeyPair() { Value = "2", DisplayName = "Coleta Finalizada" };
					[FunctionalPoint("Value[2];DisplayName[Coleta Finalizada]")]
					public static DomainKeyPair ColetaEleita { get { return _ColetaEleita; } }
				    
					private static DomainKeyPair _ColetaRejeitada = new DomainKeyPair() { Value = "3", DisplayName = "Coleta Rejeitada" };
					[FunctionalPoint("Value[3];DisplayName[Coleta Rejeitada]")]
					public static DomainKeyPair ColetaRejeitada { get { return _ColetaRejeitada; } }
				    
					private static DomainKeyPair _ColetaProcessada = new DomainKeyPair() { Value = "9", DisplayName = "Coleta Processada" };
					[FunctionalPoint("Value[9];DisplayName[Coleta Processada]")]
					public static DomainKeyPair ColetaProcessada { get { return _ColetaProcessada; } }
				    
			#endregion properties

		

	}    
			
    public partial class dmvDias
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Domingo"); 
				    
					result.Add("2", "Segunda"); 
				    
					result.Add("4", "Terça"); 
				    
					result.Add("8", "Quarta"); 
				    
					result.Add("16", "Quinta"); 
				    
					result.Add("32", "Sexta"); 
				    
					result.Add("64", "Sábado"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Domingo"); 
				    
					result.Add("2", "Segunda"); 
				    
					result.Add("4", "Terca"); 
				    
					result.Add("8", "Quarta"); 
				    
					result.Add("16", "Quinta"); 
				    
					result.Add("32", "Sexta"); 
				    
					result.Add("64", "Sabado"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Domingo = new DomainKeyPair() { Value = "1", DisplayName = "Domingo" };
					[FunctionalPoint("Value[1];DisplayName[Domingo]")]
					public static DomainKeyPair Domingo { get { return _Domingo; } }
				    
					private static DomainKeyPair _Segunda = new DomainKeyPair() { Value = "2", DisplayName = "Segunda" };
					[FunctionalPoint("Value[2];DisplayName[Segunda]")]
					public static DomainKeyPair Segunda { get { return _Segunda; } }
				    
					private static DomainKeyPair _Terca = new DomainKeyPair() { Value = "4", DisplayName = "Terça" };
					[FunctionalPoint("Value[4];DisplayName[Terça]")]
					public static DomainKeyPair Terca { get { return _Terca; } }
				    
					private static DomainKeyPair _Quarta = new DomainKeyPair() { Value = "8", DisplayName = "Quarta" };
					[FunctionalPoint("Value[8];DisplayName[Quarta]")]
					public static DomainKeyPair Quarta { get { return _Quarta; } }
				    
					private static DomainKeyPair _Quinta = new DomainKeyPair() { Value = "16", DisplayName = "Quinta" };
					[FunctionalPoint("Value[16];DisplayName[Quinta]")]
					public static DomainKeyPair Quinta { get { return _Quinta; } }
				    
					private static DomainKeyPair _Sexta = new DomainKeyPair() { Value = "32", DisplayName = "Sexta" };
					[FunctionalPoint("Value[32];DisplayName[Sexta]")]
					public static DomainKeyPair Sexta { get { return _Sexta; } }
				    
					private static DomainKeyPair _Sabado = new DomainKeyPair() { Value = "64", DisplayName = "Sábado" };
					[FunctionalPoint("Value[64];DisplayName[Sábado]")]
					public static DomainKeyPair Sabado { get { return _Sabado; } }
				    
			#endregion properties

		

	}    
			
    public partial class dmvLxTipoCartao
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Cartão de Crédito"); 
				    
					result.Add("2", "Cartão de débito"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Cartaodecredito"); 
				    
					result.Add("2", "Cartaodedebito"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Cartaodecredito = new DomainKeyPair() { Value = "1", DisplayName = "Cartão de Crédito" };
					[FunctionalPoint("Value[1];DisplayName[Cartão de Crédito]")]
					public static DomainKeyPair Cartaodecredito { get { return _Cartaodecredito; } }
				    
					private static DomainKeyPair _Cartaodedebito = new DomainKeyPair() { Value = "2", DisplayName = "Cartão de débito" };
					[FunctionalPoint("Value[2];DisplayName[Cartão de débito]")]
					public static DomainKeyPair Cartaodedebito { get { return _Cartaodedebito; } }
				    
			#endregion properties

		

	}    
			
    public partial class GiftTipoRequisicao
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Saldo"); 
				    
					result.Add("2", "Carga"); 
				    
					result.Add("3", "Resgate"); 
				    
					result.Add("4", "Confirmacao"); 
				    
					result.Add("5", "Desfazimento"); 
				    
					result.Add("6", "Estorno"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Saldo"); 
				    
					result.Add("2", "Carga"); 
				    
					result.Add("3", "Resgate"); 
				    
					result.Add("4", "Confirmacao"); 
				    
					result.Add("5", "Desfazimento"); 
				    
					result.Add("6", "Estorno"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Saldo = new DomainKeyPair() { Value = "1", DisplayName = "Saldo" };
					[FunctionalPoint("Value[1];DisplayName[Saldo]")]
					public static DomainKeyPair Saldo { get { return _Saldo; } }
				    
					private static DomainKeyPair _Carga = new DomainKeyPair() { Value = "2", DisplayName = "Carga" };
					[FunctionalPoint("Value[2];DisplayName[Carga]")]
					public static DomainKeyPair Carga { get { return _Carga; } }
				    
					private static DomainKeyPair _Resgate = new DomainKeyPair() { Value = "3", DisplayName = "Resgate" };
					[FunctionalPoint("Value[3];DisplayName[Resgate]")]
					public static DomainKeyPair Resgate { get { return _Resgate; } }
				    
					private static DomainKeyPair _Confirmacao = new DomainKeyPair() { Value = "4", DisplayName = "Confirmacao" };
					[FunctionalPoint("Value[4];DisplayName[Confirmacao]")]
					public static DomainKeyPair Confirmacao { get { return _Confirmacao; } }
				    
					private static DomainKeyPair _Desfazimento = new DomainKeyPair() { Value = "5", DisplayName = "Desfazimento" };
					[FunctionalPoint("Value[5];DisplayName[Desfazimento]")]
					public static DomainKeyPair Desfazimento { get { return _Desfazimento; } }
				    
					private static DomainKeyPair _Estorno = new DomainKeyPair() { Value = "6", DisplayName = "Estorno" };
					[FunctionalPoint("Value[6];DisplayName[Estorno]")]
					public static DomainKeyPair Estorno { get { return _Estorno; } }
				    
			#endregion properties

		

	}    
			
    public partial class ProvedorGiftCard
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Peela"); 
				    
					result.Add("2", "Unik"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Peela"); 
				    
					result.Add("2", "Unik"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Peela = new DomainKeyPair() { Value = "1", DisplayName = "Peela" };
					[FunctionalPoint("Value[1];DisplayName[Peela]")]
					public static DomainKeyPair Peela { get { return _Peela; } }
				    
					private static DomainKeyPair _Unik = new DomainKeyPair() { Value = "2", DisplayName = "Unik" };
					[FunctionalPoint("Value[2];DisplayName[Unik]")]
					public static DomainKeyPair Unik { get { return _Unik; } }
				    
			#endregion properties

		

	}    
			
    public partial class DmvPjpf
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("0", "Pessoa Física"); 
				    
					result.Add("1", "Pesoa Jurídica"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("0", "PF"); 
				    
					result.Add("1", "PJ"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _PF = new DomainKeyPair() { Value = "0", DisplayName = "Pessoa Física" };
					[FunctionalPoint("Value[0];DisplayName[Pessoa Física]")]
					public static DomainKeyPair PF { get { return _PF; } }
				    
					private static DomainKeyPair _PJ = new DomainKeyPair() { Value = "1", DisplayName = "Pesoa Jurídica" };
					[FunctionalPoint("Value[1];DisplayName[Pesoa Jurídica]")]
					public static DomainKeyPair PJ { get { return _PJ; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_TABELA_ATRIBUTO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Artigo"); 
				    
					result.Add("2", "Cliente"); 
				    
					result.Add("3", "Variante Sku"); 
				    
					result.Add("4", "LOJA"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "ARTIGO"); 
				    
					result.Add("2", "CLIENTE"); 
				    
					result.Add("3", "VARIANTE_SKU"); 
				    
					result.Add("4", "LOJA"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _ARTIGO = new DomainKeyPair() { Value = "1", DisplayName = "Artigo" };
					[FunctionalPoint("Value[1];DisplayName[Artigo]")]
					public static DomainKeyPair ARTIGO { get { return _ARTIGO; } }
				    
					private static DomainKeyPair _CLIENTE = new DomainKeyPair() { Value = "2", DisplayName = "Cliente" };
					[FunctionalPoint("Value[2];DisplayName[Cliente]")]
					public static DomainKeyPair CLIENTE { get { return _CLIENTE; } }
				    
					private static DomainKeyPair _VARIANTE_SKU = new DomainKeyPair() { Value = "3", DisplayName = "Variante Sku" };
					[FunctionalPoint("Value[3];DisplayName[Variante Sku]")]
					public static DomainKeyPair VARIANTE_SKU { get { return _VARIANTE_SKU; } }
				    
					private static DomainKeyPair _LOJA = new DomainKeyPair() { Value = "4", DisplayName = "LOJA" };
					[FunctionalPoint("Value[4];DisplayName[LOJA]")]
					public static DomainKeyPair LOJA { get { return _LOJA; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_TIPO_LOGRADOURO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Rua"); 
				    
					result.Add("2", "Avenida"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "RUA"); 
				    
					result.Add("2", "AVENIDA"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _RUA = new DomainKeyPair() { Value = "1", DisplayName = "Rua" };
					[FunctionalPoint("Value[1];DisplayName[Rua]")]
					public static DomainKeyPair RUA { get { return _RUA; } }
				    
					private static DomainKeyPair _AVENIDA = new DomainKeyPair() { Value = "2", DisplayName = "Avenida" };
					[FunctionalPoint("Value[2];DisplayName[Avenida]")]
					public static DomainKeyPair AVENIDA { get { return _AVENIDA; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_TIPO_TELEFONE
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Residencial"); 
				    
					result.Add("2", "Comercial"); 
				    
					result.Add("3", "Celular"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "RESIDENCIAL"); 
				    
					result.Add("2", "COMERCIAL"); 
				    
					result.Add("3", "CELULAR"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _RESIDENCIAL = new DomainKeyPair() { Value = "1", DisplayName = "Residencial" };
					[FunctionalPoint("Value[1];DisplayName[Residencial]")]
					public static DomainKeyPair RESIDENCIAL { get { return _RESIDENCIAL; } }
				    
					private static DomainKeyPair _COMERCIAL = new DomainKeyPair() { Value = "2", DisplayName = "Comercial" };
					[FunctionalPoint("Value[2];DisplayName[Comercial]")]
					public static DomainKeyPair COMERCIAL { get { return _COMERCIAL; } }
				    
					private static DomainKeyPair _CELULAR = new DomainKeyPair() { Value = "3", DisplayName = "Celular" };
					[FunctionalPoint("Value[3];DisplayName[Celular]")]
					public static DomainKeyPair CELULAR { get { return _CELULAR; } }
				    
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
			
    public partial class LX_SEXO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Masculino"); 
				    
					result.Add("2", "Feminino"); 
				    
					result.Add("3", "Não Informado"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Masculino"); 
				    
					result.Add("2", "Feminino"); 
				    
					result.Add("3", "NaoInformado"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Masculino = new DomainKeyPair() { Value = "1", DisplayName = "Masculino" };
					[FunctionalPoint("Value[1];DisplayName[Masculino]")]
					public static DomainKeyPair Masculino { get { return _Masculino; } }
				    
					private static DomainKeyPair _Feminino = new DomainKeyPair() { Value = "2", DisplayName = "Feminino" };
					[FunctionalPoint("Value[2];DisplayName[Feminino]")]
					public static DomainKeyPair Feminino { get { return _Feminino; } }
				    
					private static DomainKeyPair _NaoInformado = new DomainKeyPair() { Value = "3", DisplayName = "Não Informado" };
					[FunctionalPoint("Value[3];DisplayName[Não Informado]")]
					public static DomainKeyPair NaoInformado { get { return _NaoInformado; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_TIPO_PRECO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Custo Reposição"); 
				    
					result.Add("2", "Custo Médio"); 
				    
					result.Add("3", "Preço de Venda Varejo"); 
				    
					result.Add("4", "Preço de Venda Atacado"); 
				    
					result.Add("5", "Concorrência"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Custo_Reposicao"); 
				    
					result.Add("2", "Custo_Medio"); 
				    
					result.Add("3", "Preco_Venda_Varejo"); 
				    
					result.Add("4", "Preco_Venda_Atacado"); 
				    
					result.Add("5", "Concorrencia"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Custo_Reposicao = new DomainKeyPair() { Value = "1", DisplayName = "Custo Reposição" };
					[FunctionalPoint("Value[1];DisplayName[Custo Reposição]")]
					public static DomainKeyPair Custo_Reposicao { get { return _Custo_Reposicao; } }
				    
					private static DomainKeyPair _Custo_Medio = new DomainKeyPair() { Value = "2", DisplayName = "Custo Médio" };
					[FunctionalPoint("Value[2];DisplayName[Custo Médio]")]
					public static DomainKeyPair Custo_Medio { get { return _Custo_Medio; } }
				    
					private static DomainKeyPair _Preco_Venda_Varejo = new DomainKeyPair() { Value = "3", DisplayName = "Preço de Venda Varejo" };
					[FunctionalPoint("Value[3];DisplayName[Preço de Venda Varejo]")]
					public static DomainKeyPair Preco_Venda_Varejo { get { return _Preco_Venda_Varejo; } }
				    
					private static DomainKeyPair _Preco_Venda_Atacado = new DomainKeyPair() { Value = "4", DisplayName = "Preço de Venda Atacado" };
					[FunctionalPoint("Value[4];DisplayName[Preço de Venda Atacado]")]
					public static DomainKeyPair Preco_Venda_Atacado { get { return _Preco_Venda_Atacado; } }
				    
					private static DomainKeyPair _Concorrencia = new DomainKeyPair() { Value = "5", DisplayName = "Concorrência" };
					[FunctionalPoint("Value[5];DisplayName[Concorrência]")]
					public static DomainKeyPair Concorrencia { get { return _Concorrencia; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_VIA_TRANSPORTE
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Marítima"); 
				    
					result.Add("2", "Fluvial"); 
				    
					result.Add("3", "Lacustre"); 
				    
					result.Add("4", "Aérea"); 
				    
					result.Add("5", "Postal"); 
				    
					result.Add("6", "Ferroviária"); 
				    
					result.Add("7", "Rodoviária"); 
				    
					result.Add("8", "Conduto rede transmissão"); 
				    
					result.Add("9", "Meios Próprios"); 
				    
					result.Add("10", "Entrada e Saida ficta"); 
				    
					result.Add("11", "Courier"); 
				    
					result.Add("12", "Hand Carry"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Maritima"); 
				    
					result.Add("2", "Fluvial"); 
				    
					result.Add("3", "Lacustre"); 
				    
					result.Add("4", "Aerea"); 
				    
					result.Add("5", "Postal"); 
				    
					result.Add("6", "Ferroviaria"); 
				    
					result.Add("7", "Rodoviaria"); 
				    
					result.Add("8", "Conduto_rede_transmissao"); 
				    
					result.Add("9", "MeiosProprios"); 
				    
					result.Add("10", "EntradaSaidaficta"); 
				    
					result.Add("11", "Courier"); 
				    
					result.Add("12", "HandCarry"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Maritima = new DomainKeyPair() { Value = "1", DisplayName = "Marítima" };
					[FunctionalPoint("Value[1];DisplayName[Marítima]")]
					public static DomainKeyPair Maritima { get { return _Maritima; } }
				    
					private static DomainKeyPair _Fluvial = new DomainKeyPair() { Value = "2", DisplayName = "Fluvial" };
					[FunctionalPoint("Value[2];DisplayName[Fluvial]")]
					public static DomainKeyPair Fluvial { get { return _Fluvial; } }
				    
					private static DomainKeyPair _Lacustre = new DomainKeyPair() { Value = "3", DisplayName = "Lacustre" };
					[FunctionalPoint("Value[3];DisplayName[Lacustre]")]
					public static DomainKeyPair Lacustre { get { return _Lacustre; } }
				    
					private static DomainKeyPair _Aerea = new DomainKeyPair() { Value = "4", DisplayName = "Aérea" };
					[FunctionalPoint("Value[4];DisplayName[Aérea]")]
					public static DomainKeyPair Aerea { get { return _Aerea; } }
				    
					private static DomainKeyPair _Postal = new DomainKeyPair() { Value = "5", DisplayName = "Postal" };
					[FunctionalPoint("Value[5];DisplayName[Postal]")]
					public static DomainKeyPair Postal { get { return _Postal; } }
				    
					private static DomainKeyPair _Ferroviaria = new DomainKeyPair() { Value = "6", DisplayName = "Ferroviária" };
					[FunctionalPoint("Value[6];DisplayName[Ferroviária]")]
					public static DomainKeyPair Ferroviaria { get { return _Ferroviaria; } }
				    
					private static DomainKeyPair _Rodoviaria = new DomainKeyPair() { Value = "7", DisplayName = "Rodoviária" };
					[FunctionalPoint("Value[7];DisplayName[Rodoviária]")]
					public static DomainKeyPair Rodoviaria { get { return _Rodoviaria; } }
				    
					private static DomainKeyPair _Conduto_rede_transmissao = new DomainKeyPair() { Value = "8", DisplayName = "Conduto rede transmissão" };
					[FunctionalPoint("Value[8];DisplayName[Conduto rede transmissão]")]
					public static DomainKeyPair Conduto_rede_transmissao { get { return _Conduto_rede_transmissao; } }
				    
					private static DomainKeyPair _MeiosProprios = new DomainKeyPair() { Value = "9", DisplayName = "Meios Próprios" };
					[FunctionalPoint("Value[9];DisplayName[Meios Próprios]")]
					public static DomainKeyPair MeiosProprios { get { return _MeiosProprios; } }
				    
					private static DomainKeyPair _EntradaSaidaficta = new DomainKeyPair() { Value = "10", DisplayName = "Entrada e Saida ficta" };
					[FunctionalPoint("Value[10];DisplayName[Entrada e Saida ficta]")]
					public static DomainKeyPair EntradaSaidaficta { get { return _EntradaSaidaficta; } }
				    
					private static DomainKeyPair _Courier = new DomainKeyPair() { Value = "11", DisplayName = "Courier" };
					[FunctionalPoint("Value[11];DisplayName[Courier]")]
					public static DomainKeyPair Courier { get { return _Courier; } }
				    
					private static DomainKeyPair _HandCarry = new DomainKeyPair() { Value = "12", DisplayName = "Hand Carry" };
					[FunctionalPoint("Value[12];DisplayName[Hand Carry]")]
					public static DomainKeyPair HandCarry { get { return _HandCarry; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_TIPO_END_ELETRONICO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "E-mail"); 
				    
					result.Add("3", "FaceBook"); 
				    
					result.Add("4", "LinkedIn"); 
				    
					result.Add("2", "Site"); 
				    
					result.Add("5", "Skype"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "EMAIL"); 
				    
					result.Add("3", "FACEBOOK"); 
				    
					result.Add("4", "LINKEDIN"); 
				    
					result.Add("2", "SITE"); 
				    
					result.Add("5", "SKYPE"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _EMAIL = new DomainKeyPair() { Value = "1", DisplayName = "E-mail" };
					[FunctionalPoint("Value[1];DisplayName[E-mail]")]
					public static DomainKeyPair EMAIL { get { return _EMAIL; } }
				    
					private static DomainKeyPair _FACEBOOK = new DomainKeyPair() { Value = "3", DisplayName = "FaceBook" };
					[FunctionalPoint("Value[3];DisplayName[FaceBook]")]
					public static DomainKeyPair FACEBOOK { get { return _FACEBOOK; } }
				    
					private static DomainKeyPair _LINKEDIN = new DomainKeyPair() { Value = "4", DisplayName = "LinkedIn" };
					[FunctionalPoint("Value[4];DisplayName[LinkedIn]")]
					public static DomainKeyPair LINKEDIN { get { return _LINKEDIN; } }
				    
					private static DomainKeyPair _SITE = new DomainKeyPair() { Value = "2", DisplayName = "Site" };
					[FunctionalPoint("Value[2];DisplayName[Site]")]
					public static DomainKeyPair SITE { get { return _SITE; } }
				    
					private static DomainKeyPair _SKYPE = new DomainKeyPair() { Value = "5", DisplayName = "Skype" };
					[FunctionalPoint("Value[5];DisplayName[Skype]")]
					public static DomainKeyPair SKYPE { get { return _SKYPE; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_TIPO_ATENDIMENTO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Venda"); 
				    
					result.Add("2", "Devolução"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Venda"); 
				    
					result.Add("2", "Devolucao"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Venda = new DomainKeyPair() { Value = "1", DisplayName = "Venda" };
					[FunctionalPoint("Value[1];DisplayName[Venda]")]
					public static DomainKeyPair Venda { get { return _Venda; } }
				    
					private static DomainKeyPair _Devolucao = new DomainKeyPair() { Value = "2", DisplayName = "Devolução" };
					[FunctionalPoint("Value[2];DisplayName[Devolução]")]
					public static DomainKeyPair Devolucao { get { return _Devolucao; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_STATUS_REGISTRO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Pendente"); 
				    
					result.Add("2", "Enviando"); 
				    
					result.Add("3", "Enviado"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Pendente"); 
				    
					result.Add("2", "Enviando"); 
				    
					result.Add("3", "Enviado"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Pendente = new DomainKeyPair() { Value = "1", DisplayName = "Pendente" };
					[FunctionalPoint("Value[1];DisplayName[Pendente]")]
					public static DomainKeyPair Pendente { get { return _Pendente; } }
				    
					private static DomainKeyPair _Enviando = new DomainKeyPair() { Value = "2", DisplayName = "Enviando" };
					[FunctionalPoint("Value[2];DisplayName[Enviando]")]
					public static DomainKeyPair Enviando { get { return _Enviando; } }
				    
					private static DomainKeyPair _Enviado = new DomainKeyPair() { Value = "3", DisplayName = "Enviado" };
					[FunctionalPoint("Value[3];DisplayName[Enviado]")]
					public static DomainKeyPair Enviado { get { return _Enviado; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_STATUS_OPERACAO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Loja Aberta"); 
				    
					result.Add("2", "Venda Encerrada"); 
				    
					result.Add("3", "Loja Fechada"); 
				    
					result.Add("4", "Movimento Integrado"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "LojaAberta"); 
				    
					result.Add("2", "VendaEncerrada"); 
				    
					result.Add("3", "LojaFechada"); 
				    
					result.Add("4", "MovimentoIntegrado"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _LojaAberta = new DomainKeyPair() { Value = "1", DisplayName = "Loja Aberta" };
					[FunctionalPoint("Value[1];DisplayName[Loja Aberta]")]
					public static DomainKeyPair LojaAberta { get { return _LojaAberta; } }
				    
					private static DomainKeyPair _VendaEncerrada = new DomainKeyPair() { Value = "2", DisplayName = "Venda Encerrada" };
					[FunctionalPoint("Value[2];DisplayName[Venda Encerrada]")]
					public static DomainKeyPair VendaEncerrada { get { return _VendaEncerrada; } }
				    
					private static DomainKeyPair _LojaFechada = new DomainKeyPair() { Value = "3", DisplayName = "Loja Fechada" };
					[FunctionalPoint("Value[3];DisplayName[Loja Fechada]")]
					public static DomainKeyPair LojaFechada { get { return _LojaFechada; } }
				    
					private static DomainKeyPair _MovimentoIntegrado = new DomainKeyPair() { Value = "4", DisplayName = "Movimento Integrado" };
					[FunctionalPoint("Value[4];DisplayName[Movimento Integrado]")]
					public static DomainKeyPair MovimentoIntegrado { get { return _MovimentoIntegrado; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_TIPO_ITEM
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Mercadoria"); 
				    
					result.Add("2", "Serviço"); 
				    
					result.Add("3", "Pedido"); 
				    
					result.Add("4", "Vale Presente"); 
				    
					result.Add("5", "Recarga Celular"); 
				    
					result.Add("6", "Correspondente Bancário"); 
				    
					result.Add("7", "Outros Recebimentos Financeiros"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Mercadoria"); 
				    
					result.Add("2", "Servico"); 
				    
					result.Add("3", "Pedido"); 
				    
					result.Add("4", "ValePresente"); 
				    
					result.Add("5", "RecargaCelular"); 
				    
					result.Add("6", "CorrespondenteBancario"); 
				    
					result.Add("7", "OutrosRecebimentosFinanceiros"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Mercadoria = new DomainKeyPair() { Value = "1", DisplayName = "Mercadoria" };
					[FunctionalPoint("Value[1];DisplayName[Mercadoria]")]
					public static DomainKeyPair Mercadoria { get { return _Mercadoria; } }
				    
					private static DomainKeyPair _Servico = new DomainKeyPair() { Value = "2", DisplayName = "Serviço" };
					[FunctionalPoint("Value[2];DisplayName[Serviço]")]
					public static DomainKeyPair Servico { get { return _Servico; } }
				    
					private static DomainKeyPair _Pedido = new DomainKeyPair() { Value = "3", DisplayName = "Pedido" };
					[FunctionalPoint("Value[3];DisplayName[Pedido]")]
					public static DomainKeyPair Pedido { get { return _Pedido; } }
				    
					private static DomainKeyPair _ValePresente = new DomainKeyPair() { Value = "4", DisplayName = "Vale Presente" };
					[FunctionalPoint("Value[4];DisplayName[Vale Presente]")]
					public static DomainKeyPair ValePresente { get { return _ValePresente; } }
				    
					private static DomainKeyPair _RecargaCelular = new DomainKeyPair() { Value = "5", DisplayName = "Recarga Celular" };
					[FunctionalPoint("Value[5];DisplayName[Recarga Celular]")]
					public static DomainKeyPair RecargaCelular { get { return _RecargaCelular; } }
				    
					private static DomainKeyPair _CorrespondenteBancario = new DomainKeyPair() { Value = "6", DisplayName = "Correspondente Bancário" };
					[FunctionalPoint("Value[6];DisplayName[Correspondente Bancário]")]
					public static DomainKeyPair CorrespondenteBancario { get { return _CorrespondenteBancario; } }
				    
					private static DomainKeyPair _OutrosRecebimentosFinanceiros = new DomainKeyPair() { Value = "7", DisplayName = "Outros Recebimentos Financeiros" };
					[FunctionalPoint("Value[7];DisplayName[Outros Recebimentos Financeiros]")]
					public static DomainKeyPair OutrosRecebimentosFinanceiros { get { return _OutrosRecebimentosFinanceiros; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_TIPO_RESGATE
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Desconto"); 
				    
					result.Add("2", "Pagamento"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Desconto"); 
				    
					result.Add("2", "Pagamento"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Desconto = new DomainKeyPair() { Value = "1", DisplayName = "Desconto" };
					[FunctionalPoint("Value[1];DisplayName[Desconto]")]
					public static DomainKeyPair Desconto { get { return _Desconto; } }
				    
					private static DomainKeyPair _Pagamento = new DomainKeyPair() { Value = "2", DisplayName = "Pagamento" };
					[FunctionalPoint("Value[2];DisplayName[Pagamento]")]
					public static DomainKeyPair Pagamento { get { return _Pagamento; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_FILTRO_TIPO_LOJA
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Lojas participantes"); 
				    
					result.Add("2", "Lojas não participantes"); 
				    
					result.Add("3", "Todas as lojas"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "LojasSelecionadas"); 
				    
					result.Add("2", "LojasNaoSelecionadas"); 
				    
					result.Add("3", "TodasLojas"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _LojasSelecionadas = new DomainKeyPair() { Value = "1", DisplayName = "Lojas participantes" };
					[FunctionalPoint("Value[1];DisplayName[Lojas participantes]")]
					public static DomainKeyPair LojasSelecionadas { get { return _LojasSelecionadas; } }
				    
					private static DomainKeyPair _LojasNaoSelecionadas = new DomainKeyPair() { Value = "2", DisplayName = "Lojas não participantes" };
					[FunctionalPoint("Value[2];DisplayName[Lojas não participantes]")]
					public static DomainKeyPair LojasNaoSelecionadas { get { return _LojasNaoSelecionadas; } }
				    
					private static DomainKeyPair _TodasLojas = new DomainKeyPair() { Value = "3", DisplayName = "Todas as lojas" };
					[FunctionalPoint("Value[3];DisplayName[Todas as lojas]")]
					public static DomainKeyPair TodasLojas { get { return _TodasLojas; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_FILTRO_TIPO_OPERACAO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Operações participantes"); 
				    
					result.Add("2", "Operações não participantes"); 
				    
					result.Add("3", "Todas as operações"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "OperacoesSelecionadas"); 
				    
					result.Add("2", "OperacoesNaoSelecionadas"); 
				    
					result.Add("3", "TodasOperacoes"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _OperacoesSelecionadas = new DomainKeyPair() { Value = "1", DisplayName = "Operações participantes" };
					[FunctionalPoint("Value[1];DisplayName[Operações participantes]")]
					public static DomainKeyPair OperacoesSelecionadas { get { return _OperacoesSelecionadas; } }
				    
					private static DomainKeyPair _OperacoesNaoSelecionadas = new DomainKeyPair() { Value = "2", DisplayName = "Operações não participantes" };
					[FunctionalPoint("Value[2];DisplayName[Operações não participantes]")]
					public static DomainKeyPair OperacoesNaoSelecionadas { get { return _OperacoesNaoSelecionadas; } }
				    
					private static DomainKeyPair _TodasOperacoes = new DomainKeyPair() { Value = "3", DisplayName = "Todas as operações" };
					[FunctionalPoint("Value[3];DisplayName[Todas as operações]")]
					public static DomainKeyPair TodasOperacoes { get { return _TodasOperacoes; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_FILTRO_TIPO_PGTO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Tipos participantes"); 
				    
					result.Add("2", "Tipos não participantes"); 
				    
					result.Add("3", "Todos os tipos"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "TiposSelecionados"); 
				    
					result.Add("2", "TiposNaoSelecionados"); 
				    
					result.Add("3", "TodoTipos"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _TiposSelecionados = new DomainKeyPair() { Value = "1", DisplayName = "Tipos participantes" };
					[FunctionalPoint("Value[1];DisplayName[Tipos participantes]")]
					public static DomainKeyPair TiposSelecionados { get { return _TiposSelecionados; } }
				    
					private static DomainKeyPair _TiposNaoSelecionados = new DomainKeyPair() { Value = "2", DisplayName = "Tipos não participantes" };
					[FunctionalPoint("Value[2];DisplayName[Tipos não participantes]")]
					public static DomainKeyPair TiposNaoSelecionados { get { return _TiposNaoSelecionados; } }
				    
					private static DomainKeyPair _TodoTipos = new DomainKeyPair() { Value = "3", DisplayName = "Todos os tipos" };
					[FunctionalPoint("Value[3];DisplayName[Todos os tipos]")]
					public static DomainKeyPair TodoTipos { get { return _TodoTipos; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_STATUS_PEDIDO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Pedido em confecção"); 
				    
					result.Add("2", "Pedido publicado"); 
				    
					result.Add("3", "Pedido cancelado"); 
				    
					result.Add("4", "Pedido encerrado"); 
				    
					result.Add("5", "Faturamento iniciado"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "PedidoConfeccao"); 
				    
					result.Add("2", "PedidoPublicado"); 
				    
					result.Add("3", "PedidoCancelado"); 
				    
					result.Add("4", "PedidoEncerrado"); 
				    
					result.Add("5", "FaturamentoIniciado"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _PedidoConfeccao = new DomainKeyPair() { Value = "1", DisplayName = "Pedido em confecção" };
					[FunctionalPoint("Value[1];DisplayName[Pedido em confecção]")]
					public static DomainKeyPair PedidoConfeccao { get { return _PedidoConfeccao; } }
				    
					private static DomainKeyPair _PedidoPublicado = new DomainKeyPair() { Value = "2", DisplayName = "Pedido publicado" };
					[FunctionalPoint("Value[2];DisplayName[Pedido publicado]")]
					public static DomainKeyPair PedidoPublicado { get { return _PedidoPublicado; } }
				    
					private static DomainKeyPair _PedidoCancelado = new DomainKeyPair() { Value = "3", DisplayName = "Pedido cancelado" };
					[FunctionalPoint("Value[3];DisplayName[Pedido cancelado]")]
					public static DomainKeyPair PedidoCancelado { get { return _PedidoCancelado; } }
				    
					private static DomainKeyPair _PedidoEncerrado = new DomainKeyPair() { Value = "4", DisplayName = "Pedido encerrado" };
					[FunctionalPoint("Value[4];DisplayName[Pedido encerrado]")]
					public static DomainKeyPair PedidoEncerrado { get { return _PedidoEncerrado; } }
				    
					private static DomainKeyPair _FaturamentoIniciado = new DomainKeyPair() { Value = "5", DisplayName = "Faturamento iniciado" };
					[FunctionalPoint("Value[5];DisplayName[Faturamento iniciado]")]
					public static DomainKeyPair FaturamentoIniciado { get { return _FaturamentoIniciado; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_TIPO_PEDIDO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Pré venda"); 
				    
					result.Add("2", "Pedido ecommerce"); 
				    
					result.Add("3", "Lista de presente"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "PreVenda"); 
				    
					result.Add("2", "PedidoEcommerce"); 
				    
					result.Add("3", "ListaPresente"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _PreVenda = new DomainKeyPair() { Value = "1", DisplayName = "Pré venda" };
					[FunctionalPoint("Value[1];DisplayName[Pré venda]")]
					public static DomainKeyPair PreVenda { get { return _PreVenda; } }
				    
					private static DomainKeyPair _PedidoEcommerce = new DomainKeyPair() { Value = "2", DisplayName = "Pedido ecommerce" };
					[FunctionalPoint("Value[2];DisplayName[Pedido ecommerce]")]
					public static DomainKeyPair PedidoEcommerce { get { return _PedidoEcommerce; } }
				    
					private static DomainKeyPair _ListaPresente = new DomainKeyPair() { Value = "3", DisplayName = "Lista de presente" };
					[FunctionalPoint("Value[3];DisplayName[Lista de presente]")]
					public static DomainKeyPair ListaPresente { get { return _ListaPresente; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_TIPO_LISTA
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Presente"); 
				    
					result.Add("2", "Casamento"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Presente"); 
				    
					result.Add("2", "Casamento"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Presente = new DomainKeyPair() { Value = "1", DisplayName = "Presente" };
					[FunctionalPoint("Value[1];DisplayName[Presente]")]
					public static DomainKeyPair Presente { get { return _Presente; } }
				    
					private static DomainKeyPair _Casamento = new DomainKeyPair() { Value = "2", DisplayName = "Casamento" };
					[FunctionalPoint("Value[2];DisplayName[Casamento]")]
					public static DomainKeyPair Casamento { get { return _Casamento; } }
				    
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
			
    public partial class LX_ESTADO_CIVIL
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Solteiro(a)"); 
				    
					result.Add("2", "Casado(a)"); 
				    
					result.Add("3", "Separado(a)"); 
				    
					result.Add("4", "Divorciado(a)"); 
				    
					result.Add("5", "Viúvo(a)"); 
				    
					result.Add("6", "Outros"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Solteiro"); 
				    
					result.Add("2", "Casado"); 
				    
					result.Add("3", "Separado"); 
				    
					result.Add("4", "Divorciado"); 
				    
					result.Add("5", "Viuvo"); 
				    
					result.Add("6", "Outros"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Solteiro = new DomainKeyPair() { Value = "1", DisplayName = "Solteiro(a)" };
					[FunctionalPoint("Value[1];DisplayName[Solteiro(a)]")]
					public static DomainKeyPair Solteiro { get { return _Solteiro; } }
				    
					private static DomainKeyPair _Casado = new DomainKeyPair() { Value = "2", DisplayName = "Casado(a)" };
					[FunctionalPoint("Value[2];DisplayName[Casado(a)]")]
					public static DomainKeyPair Casado { get { return _Casado; } }
				    
					private static DomainKeyPair _Separado = new DomainKeyPair() { Value = "3", DisplayName = "Separado(a)" };
					[FunctionalPoint("Value[3];DisplayName[Separado(a)]")]
					public static DomainKeyPair Separado { get { return _Separado; } }
				    
					private static DomainKeyPair _Divorciado = new DomainKeyPair() { Value = "4", DisplayName = "Divorciado(a)" };
					[FunctionalPoint("Value[4];DisplayName[Divorciado(a)]")]
					public static DomainKeyPair Divorciado { get { return _Divorciado; } }
				    
					private static DomainKeyPair _Viuvo = new DomainKeyPair() { Value = "5", DisplayName = "Viúvo(a)" };
					[FunctionalPoint("Value[5];DisplayName[Viúvo(a)]")]
					public static DomainKeyPair Viuvo { get { return _Viuvo; } }
				    
					private static DomainKeyPair _Outros = new DomainKeyPair() { Value = "6", DisplayName = "Outros" };
					[FunctionalPoint("Value[6];DisplayName[Outros]")]
					public static DomainKeyPair Outros { get { return _Outros; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_TIPO_VALIDACAO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Atributos"); 
				    
					result.Add("2", "Faixa de Valores"); 
				    
					result.Add("3", "Tabelas de Sistemas"); 
				    
					result.Add("4", "Livre"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Atributos"); 
				    
					result.Add("2", "FaixaValores"); 
				    
					result.Add("3", "TabelasSistemas"); 
				    
					result.Add("4", "Livre"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Atributos = new DomainKeyPair() { Value = "1", DisplayName = "Atributos" };
					[FunctionalPoint("Value[1];DisplayName[Atributos]")]
					public static DomainKeyPair Atributos { get { return _Atributos; } }
				    
					private static DomainKeyPair _FaixaValores = new DomainKeyPair() { Value = "2", DisplayName = "Faixa de Valores" };
					[FunctionalPoint("Value[2];DisplayName[Faixa de Valores]")]
					public static DomainKeyPair FaixaValores { get { return _FaixaValores; } }
				    
					private static DomainKeyPair _TabelasSistemas = new DomainKeyPair() { Value = "3", DisplayName = "Tabelas de Sistemas" };
					[FunctionalPoint("Value[3];DisplayName[Tabelas de Sistemas]")]
					public static DomainKeyPair TabelasSistemas { get { return _TabelasSistemas; } }
				    
					private static DomainKeyPair _Livre = new DomainKeyPair() { Value = "4", DisplayName = "Livre" };
					[FunctionalPoint("Value[4];DisplayName[Livre]")]
					public static DomainKeyPair Livre { get { return _Livre; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_PRIORIDADE_PROMOCAO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Muito Baixa"); 
				    
					result.Add("2", "Baixa"); 
				    
					result.Add("3", "Média"); 
				    
					result.Add("4", "Alta"); 
				    
					result.Add("5", "Muito Alta"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "MuitoBaixa"); 
				    
					result.Add("2", "Baixa"); 
				    
					result.Add("3", "Media"); 
				    
					result.Add("4", "Alta"); 
				    
					result.Add("5", "MuitoAlta"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _MuitoBaixa = new DomainKeyPair() { Value = "1", DisplayName = "Muito Baixa" };
					[FunctionalPoint("Value[1];DisplayName[Muito Baixa]")]
					public static DomainKeyPair MuitoBaixa { get { return _MuitoBaixa; } }
				    
					private static DomainKeyPair _Baixa = new DomainKeyPair() { Value = "2", DisplayName = "Baixa" };
					[FunctionalPoint("Value[2];DisplayName[Baixa]")]
					public static DomainKeyPair Baixa { get { return _Baixa; } }
				    
					private static DomainKeyPair _Media = new DomainKeyPair() { Value = "3", DisplayName = "Média" };
					[FunctionalPoint("Value[3];DisplayName[Média]")]
					public static DomainKeyPair Media { get { return _Media; } }
				    
					private static DomainKeyPair _Alta = new DomainKeyPair() { Value = "4", DisplayName = "Alta" };
					[FunctionalPoint("Value[4];DisplayName[Alta]")]
					public static DomainKeyPair Alta { get { return _Alta; } }
				    
					private static DomainKeyPair _MuitoAlta = new DomainKeyPair() { Value = "5", DisplayName = "Muito Alta" };
					[FunctionalPoint("Value[5];DisplayName[Muito Alta]")]
					public static DomainKeyPair MuitoAlta { get { return _MuitoAlta; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_CODIGO_IDIOMA
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Português"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Portugues"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Portugues = new DomainKeyPair() { Value = "1", DisplayName = "Português" };
					[FunctionalPoint("Value[1];DisplayName[Português]")]
					public static DomainKeyPair Portugues { get { return _Portugues; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_TIPO_OFERTA
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Desconto Subtotal"); 
				    
					result.Add("2", "Desconto Item"); 
				    
					result.Add("3", "Desconto Subtotal Cupom"); 
				    
					result.Add("4", "Desconto Subtotal Gift"); 
				    
					result.Add("5", "Vale Produto Venda Atual"); 
				    
					result.Add("6", "Vale Produto Outra Venda"); 
				    
					result.Add("7", "Brinde Próprio"); 
				    
					result.Add("8", "Brinde Terceiro"); 
				    
					result.Add("9", "Gift Terceiro"); 
				    
					result.Add("10", "Campanha Frete"); 
				    
					result.Add("11", "Campanha Estacionamento"); 
				    
					result.Add("12", "Pontuação Fidelidade"); 
				    
					result.Add("13", "Pontuação Vendedor"); 
				    
					result.Add("14", "Comissão Extra"); 
				    
					result.Add("15", "Outras Ofertas"); 
				    
					result.Add("16", "Sem Benefício"); 
				    
					result.Add("17", "Cupom Promocional"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "DescontoSubtotal"); 
				    
					result.Add("2", "DescontoItem"); 
				    
					result.Add("3", "DescontoSubtotalCupom"); 
				    
					result.Add("4", "DescontoSubtotalGift"); 
				    
					result.Add("5", "ValeProdutoVendaAtual"); 
				    
					result.Add("6", "ValeProdutoOutraVenda"); 
				    
					result.Add("7", "BrindeProprio"); 
				    
					result.Add("8", "BrindeTerceiro"); 
				    
					result.Add("9", "GiftTerceiro"); 
				    
					result.Add("10", "CampanhaFrete"); 
				    
					result.Add("11", "CampanhaEstacionamento"); 
				    
					result.Add("12", "PontuacaoFidelidade"); 
				    
					result.Add("13", "PontuacaoVendedor"); 
				    
					result.Add("14", "ComissaoExtra"); 
				    
					result.Add("15", "OutrasOfertas"); 
				    
					result.Add("16", "SemBeneficio"); 
				    
					result.Add("17", "CupomPromocional"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _DescontoSubtotal = new DomainKeyPair() { Value = "1", DisplayName = "Desconto Subtotal" };
					[FunctionalPoint("Value[1];DisplayName[Desconto Subtotal]")]
					public static DomainKeyPair DescontoSubtotal { get { return _DescontoSubtotal; } }
				    
					private static DomainKeyPair _DescontoItem = new DomainKeyPair() { Value = "2", DisplayName = "Desconto Item" };
					[FunctionalPoint("Value[2];DisplayName[Desconto Item]")]
					public static DomainKeyPair DescontoItem { get { return _DescontoItem; } }
				    
					private static DomainKeyPair _DescontoSubtotalCupom = new DomainKeyPair() { Value = "3", DisplayName = "Desconto Subtotal Cupom" };
					[FunctionalPoint("Value[3];DisplayName[Desconto Subtotal Cupom]")]
					public static DomainKeyPair DescontoSubtotalCupom { get { return _DescontoSubtotalCupom; } }
				    
					private static DomainKeyPair _DescontoSubtotalGift = new DomainKeyPair() { Value = "4", DisplayName = "Desconto Subtotal Gift" };
					[FunctionalPoint("Value[4];DisplayName[Desconto Subtotal Gift]")]
					public static DomainKeyPair DescontoSubtotalGift { get { return _DescontoSubtotalGift; } }
				    
					private static DomainKeyPair _ValeProdutoVendaAtual = new DomainKeyPair() { Value = "5", DisplayName = "Vale Produto Venda Atual" };
					[FunctionalPoint("Value[5];DisplayName[Vale Produto Venda Atual]")]
					public static DomainKeyPair ValeProdutoVendaAtual { get { return _ValeProdutoVendaAtual; } }
				    
					private static DomainKeyPair _ValeProdutoOutraVenda = new DomainKeyPair() { Value = "6", DisplayName = "Vale Produto Outra Venda" };
					[FunctionalPoint("Value[6];DisplayName[Vale Produto Outra Venda]")]
					public static DomainKeyPair ValeProdutoOutraVenda { get { return _ValeProdutoOutraVenda; } }
				    
					private static DomainKeyPair _BrindeProprio = new DomainKeyPair() { Value = "7", DisplayName = "Brinde Próprio" };
					[FunctionalPoint("Value[7];DisplayName[Brinde Próprio]")]
					public static DomainKeyPair BrindeProprio { get { return _BrindeProprio; } }
				    
					private static DomainKeyPair _BrindeTerceiro = new DomainKeyPair() { Value = "8", DisplayName = "Brinde Terceiro" };
					[FunctionalPoint("Value[8];DisplayName[Brinde Terceiro]")]
					public static DomainKeyPair BrindeTerceiro { get { return _BrindeTerceiro; } }
				    
					private static DomainKeyPair _GiftTerceiro = new DomainKeyPair() { Value = "9", DisplayName = "Gift Terceiro" };
					[FunctionalPoint("Value[9];DisplayName[Gift Terceiro]")]
					public static DomainKeyPair GiftTerceiro { get { return _GiftTerceiro; } }
				    
					private static DomainKeyPair _CampanhaFrete = new DomainKeyPair() { Value = "10", DisplayName = "Campanha Frete" };
					[FunctionalPoint("Value[10];DisplayName[Campanha Frete]")]
					public static DomainKeyPair CampanhaFrete { get { return _CampanhaFrete; } }
				    
					private static DomainKeyPair _CampanhaEstacionamento = new DomainKeyPair() { Value = "11", DisplayName = "Campanha Estacionamento" };
					[FunctionalPoint("Value[11];DisplayName[Campanha Estacionamento]")]
					public static DomainKeyPair CampanhaEstacionamento { get { return _CampanhaEstacionamento; } }
				    
					private static DomainKeyPair _PontuacaoFidelidade = new DomainKeyPair() { Value = "12", DisplayName = "Pontuação Fidelidade" };
					[FunctionalPoint("Value[12];DisplayName[Pontuação Fidelidade]")]
					public static DomainKeyPair PontuacaoFidelidade { get { return _PontuacaoFidelidade; } }
				    
					private static DomainKeyPair _PontuacaoVendedor = new DomainKeyPair() { Value = "13", DisplayName = "Pontuação Vendedor" };
					[FunctionalPoint("Value[13];DisplayName[Pontuação Vendedor]")]
					public static DomainKeyPair PontuacaoVendedor { get { return _PontuacaoVendedor; } }
				    
					private static DomainKeyPair _ComissaoExtra = new DomainKeyPair() { Value = "14", DisplayName = "Comissão Extra" };
					[FunctionalPoint("Value[14];DisplayName[Comissão Extra]")]
					public static DomainKeyPair ComissaoExtra { get { return _ComissaoExtra; } }
				    
					private static DomainKeyPair _OutrasOfertas = new DomainKeyPair() { Value = "15", DisplayName = "Outras Ofertas" };
					[FunctionalPoint("Value[15];DisplayName[Outras Ofertas]")]
					public static DomainKeyPair OutrasOfertas { get { return _OutrasOfertas; } }
				    
					private static DomainKeyPair _SemBeneficio = new DomainKeyPair() { Value = "16", DisplayName = "Sem Benefício" };
					[FunctionalPoint("Value[16];DisplayName[Sem Benefício]")]
					public static DomainKeyPair SemBeneficio { get { return _SemBeneficio; } }
				    
					private static DomainKeyPair _CupomPromocional = new DomainKeyPair() { Value = "17", DisplayName = "Cupom Promocional" };
					[FunctionalPoint("Value[17];DisplayName[Cupom Promocional]")]
					public static DomainKeyPair CupomPromocional { get { return _CupomPromocional; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_STATUS_LGE_PEDIDO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("4", "Aguardando Recebimento"); 
				    
					result.Add("5", "Já Faturado e em trânsito"); 
				    
					result.Add("6", "Encerrado"); 
				    
					result.Add("9", "Cancelado"); 
				    
					result.Add("1", "Em Elaboração"); 
				    
					result.Add("2", "Aprovação Interna"); 
				    
					result.Add("3", "Aprovação Externa"); 
				    
					result.Add("7", "Não Aprovado Internamente"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("4", "AguardandoRecebimento"); 
				    
					result.Add("5", "EmTransito"); 
				    
					result.Add("6", "Encerrado"); 
				    
					result.Add("9", "Cancelado"); 
				    
					result.Add("1", "EmElaboracao"); 
				    
					result.Add("2", "AprovacaoInterna"); 
				    
					result.Add("3", "AprovacaoExterna"); 
				    
					result.Add("7", "NaoAprovadoInternamente"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _AguardandoRecebimento = new DomainKeyPair() { Value = "4", DisplayName = "Aguardando Recebimento" };
					[FunctionalPoint("Value[4];DisplayName[Aguardando Recebimento]")]
					public static DomainKeyPair AguardandoRecebimento { get { return _AguardandoRecebimento; } }
				    
					private static DomainKeyPair _EmTransito = new DomainKeyPair() { Value = "5", DisplayName = "Já Faturado e em trânsito" };
					[FunctionalPoint("Value[5];DisplayName[Já Faturado e em trânsito]")]
					public static DomainKeyPair EmTransito { get { return _EmTransito; } }
				    
					private static DomainKeyPair _Encerrado = new DomainKeyPair() { Value = "6", DisplayName = "Encerrado" };
					[FunctionalPoint("Value[6];DisplayName[Encerrado]")]
					public static DomainKeyPair Encerrado { get { return _Encerrado; } }
				    
					private static DomainKeyPair _Cancelado = new DomainKeyPair() { Value = "9", DisplayName = "Cancelado" };
					[FunctionalPoint("Value[9];DisplayName[Cancelado]")]
					public static DomainKeyPair Cancelado { get { return _Cancelado; } }
				    
					private static DomainKeyPair _EmElaboracao = new DomainKeyPair() { Value = "1", DisplayName = "Em Elaboração" };
					[FunctionalPoint("Value[1];DisplayName[Em Elaboração]")]
					public static DomainKeyPair EmElaboracao { get { return _EmElaboracao; } }
				    
					private static DomainKeyPair _AprovacaoInterna = new DomainKeyPair() { Value = "2", DisplayName = "Aprovação Interna" };
					[FunctionalPoint("Value[2];DisplayName[Aprovação Interna]")]
					public static DomainKeyPair AprovacaoInterna { get { return _AprovacaoInterna; } }
				    
					private static DomainKeyPair _AprovacaoExterna = new DomainKeyPair() { Value = "3", DisplayName = "Aprovação Externa" };
					[FunctionalPoint("Value[3];DisplayName[Aprovação Externa]")]
					public static DomainKeyPair AprovacaoExterna { get { return _AprovacaoExterna; } }
				    
					private static DomainKeyPair _NaoAprovadoInternamente = new DomainKeyPair() { Value = "7", DisplayName = "Não Aprovado Internamente" };
					[FunctionalPoint("Value[7];DisplayName[Não Aprovado Internamente]")]
					public static DomainKeyPair NaoAprovadoInternamente { get { return _NaoAprovadoInternamente; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_STATUS_ATENDIMENTO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("0", "Indefinido"); 
				    
					result.Add("1", "Atendimento em Andamento"); 
				    
					result.Add("2", "Atendimento Suspenso"); 
				    
					result.Add("3", "Atendimento Concluído"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("0", "Indefinido"); 
				    
					result.Add("1", "AtendimentoEmAndamento"); 
				    
					result.Add("2", "AtendimentoSuspenso"); 
				    
					result.Add("3", "AtendimentoConcluido"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Indefinido = new DomainKeyPair() { Value = "0", DisplayName = "Indefinido" };
					[FunctionalPoint("Value[0];DisplayName[Indefinido]")]
					public static DomainKeyPair Indefinido { get { return _Indefinido; } }
				    
					private static DomainKeyPair _AtendimentoEmAndamento = new DomainKeyPair() { Value = "1", DisplayName = "Atendimento em Andamento" };
					[FunctionalPoint("Value[1];DisplayName[Atendimento em Andamento]")]
					public static DomainKeyPair AtendimentoEmAndamento { get { return _AtendimentoEmAndamento; } }
				    
					private static DomainKeyPair _AtendimentoSuspenso = new DomainKeyPair() { Value = "2", DisplayName = "Atendimento Suspenso" };
					[FunctionalPoint("Value[2];DisplayName[Atendimento Suspenso]")]
					public static DomainKeyPair AtendimentoSuspenso { get { return _AtendimentoSuspenso; } }
				    
					private static DomainKeyPair _AtendimentoConcluido = new DomainKeyPair() { Value = "3", DisplayName = "Atendimento Concluído" };
					[FunctionalPoint("Value[3];DisplayName[Atendimento Concluído]")]
					public static DomainKeyPair AtendimentoConcluido { get { return _AtendimentoConcluido; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_FATOR_STK_MOV
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Entrada"); 
				    
					result.Add("-1", "Saída"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Entrada"); 
				    
					result.Add("-1", "Saida"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Entrada = new DomainKeyPair() { Value = "1", DisplayName = "Entrada" };
					[FunctionalPoint("Value[1];DisplayName[Entrada]")]
					public static DomainKeyPair Entrada { get { return _Entrada; } }
				    
					private static DomainKeyPair _Saida = new DomainKeyPair() { Value = "-1", DisplayName = "Saída" };
					[FunctionalPoint("Value[-1];DisplayName[Saída]")]
					public static DomainKeyPair Saida { get { return _Saida; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_TIPO_OPERACAO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Entrada de Nota Fiscal"); 
				    
					result.Add("2", "Saida de Nota Fiscal"); 
				    
					result.Add("3", "Entrada no Estoque"); 
				    
					result.Add("4", "Saída do Estoque"); 
				    
					result.Add("50", "Financeiro a Pagar"); 
				    
					result.Add("60", "Financeiro a Receber"); 
				    
					result.Add("5", "Ajuste de Estoque"); 
				    
					result.Add("6", "Loja Venda"); 
				    
					result.Add("7", "Loja Devolução"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "EntradaNotaFiscal"); 
				    
					result.Add("2", "SaidaNotaFiscal"); 
				    
					result.Add("3", "EntradaEstoque"); 
				    
					result.Add("4", "SaidaEstoque"); 
				    
					result.Add("50", "FinanceiroPagar"); 
				    
					result.Add("60", "FinanceiroReceber"); 
				    
					result.Add("5", "AjusteEstoque"); 
				    
					result.Add("6", "VendaVarejo"); 
				    
					result.Add("7", "DevoluçãoVarejo"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _EntradaNotaFiscal = new DomainKeyPair() { Value = "1", DisplayName = "Entrada de Nota Fiscal" };
					[FunctionalPoint("Value[1];DisplayName[Entrada de Nota Fiscal]")]
					public static DomainKeyPair EntradaNotaFiscal { get { return _EntradaNotaFiscal; } }
				    
					private static DomainKeyPair _SaidaNotaFiscal = new DomainKeyPair() { Value = "2", DisplayName = "Saida de Nota Fiscal" };
					[FunctionalPoint("Value[2];DisplayName[Saida de Nota Fiscal]")]
					public static DomainKeyPair SaidaNotaFiscal { get { return _SaidaNotaFiscal; } }
				    
					private static DomainKeyPair _EntradaEstoque = new DomainKeyPair() { Value = "3", DisplayName = "Entrada no Estoque" };
					[FunctionalPoint("Value[3];DisplayName[Entrada no Estoque]")]
					public static DomainKeyPair EntradaEstoque { get { return _EntradaEstoque; } }
				    
					private static DomainKeyPair _SaidaEstoque = new DomainKeyPair() { Value = "4", DisplayName = "Saída do Estoque" };
					[FunctionalPoint("Value[4];DisplayName[Saída do Estoque]")]
					public static DomainKeyPair SaidaEstoque { get { return _SaidaEstoque; } }
				    
					private static DomainKeyPair _FinanceiroPagar = new DomainKeyPair() { Value = "50", DisplayName = "Financeiro a Pagar" };
					[FunctionalPoint("Value[50];DisplayName[Financeiro a Pagar]")]
					public static DomainKeyPair FinanceiroPagar { get { return _FinanceiroPagar; } }
				    
					private static DomainKeyPair _FinanceiroReceber = new DomainKeyPair() { Value = "60", DisplayName = "Financeiro a Receber" };
					[FunctionalPoint("Value[60];DisplayName[Financeiro a Receber]")]
					public static DomainKeyPair FinanceiroReceber { get { return _FinanceiroReceber; } }
				    
					private static DomainKeyPair _AjusteEstoque = new DomainKeyPair() { Value = "5", DisplayName = "Ajuste de Estoque" };
					[FunctionalPoint("Value[5];DisplayName[Ajuste de Estoque]")]
					public static DomainKeyPair AjusteEstoque { get { return _AjusteEstoque; } }
				    
					private static DomainKeyPair _VendaVarejo = new DomainKeyPair() { Value = "6", DisplayName = "Loja Venda" };
					[FunctionalPoint("Value[6];DisplayName[Loja Venda]")]
					public static DomainKeyPair VendaVarejo { get { return _VendaVarejo; } }
				    
					private static DomainKeyPair _DevoluçãoVarejo = new DomainKeyPair() { Value = "7", DisplayName = "Loja Devolução" };
					[FunctionalPoint("Value[7];DisplayName[Loja Devolução]")]
					public static DomainKeyPair DevoluçãoVarejo { get { return _DevoluçãoVarejo; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_COND_PAGTO_STATUS
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Em Elaboração"); 
				    
					result.Add("2", "Em Aprovação Interna"); 
				    
					result.Add("3", "Em Aprovação Externa"); 
				    
					result.Add("4", "Aprovada"); 
				    
					result.Add("5", "Reprovada"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "EmElaboracao"); 
				    
					result.Add("2", "EmAprovacaoInterna"); 
				    
					result.Add("3", "EmAprovacaoExterna"); 
				    
					result.Add("4", "Aprovada"); 
				    
					result.Add("5", "Reprovada"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _EmElaboracao = new DomainKeyPair() { Value = "1", DisplayName = "Em Elaboração" };
					[FunctionalPoint("Value[1];DisplayName[Em Elaboração]")]
					public static DomainKeyPair EmElaboracao { get { return _EmElaboracao; } }
				    
					private static DomainKeyPair _EmAprovacaoInterna = new DomainKeyPair() { Value = "2", DisplayName = "Em Aprovação Interna" };
					[FunctionalPoint("Value[2];DisplayName[Em Aprovação Interna]")]
					public static DomainKeyPair EmAprovacaoInterna { get { return _EmAprovacaoInterna; } }
				    
					private static DomainKeyPair _EmAprovacaoExterna = new DomainKeyPair() { Value = "3", DisplayName = "Em Aprovação Externa" };
					[FunctionalPoint("Value[3];DisplayName[Em Aprovação Externa]")]
					public static DomainKeyPair EmAprovacaoExterna { get { return _EmAprovacaoExterna; } }
				    
					private static DomainKeyPair _Aprovada = new DomainKeyPair() { Value = "4", DisplayName = "Aprovada" };
					[FunctionalPoint("Value[4];DisplayName[Aprovada]")]
					public static DomainKeyPair Aprovada { get { return _Aprovada; } }
				    
					private static DomainKeyPair _Reprovada = new DomainKeyPair() { Value = "5", DisplayName = "Reprovada" };
					[FunctionalPoint("Value[5];DisplayName[Reprovada]")]
					public static DomainKeyPair Reprovada { get { return _Reprovada; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_TIPO_COND_PAGTO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "À Vista"); 
				    
					result.Add("2", "Parcela Fixa"); 
				    
					result.Add("3", "Parcela Variável"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "AVista"); 
				    
					result.Add("2", "ParcelaFixa"); 
				    
					result.Add("3", "ParcelaVariavel"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _AVista = new DomainKeyPair() { Value = "1", DisplayName = "À Vista" };
					[FunctionalPoint("Value[1];DisplayName[À Vista]")]
					public static DomainKeyPair AVista { get { return _AVista; } }
				    
					private static DomainKeyPair _ParcelaFixa = new DomainKeyPair() { Value = "2", DisplayName = "Parcela Fixa" };
					[FunctionalPoint("Value[2];DisplayName[Parcela Fixa]")]
					public static DomainKeyPair ParcelaFixa { get { return _ParcelaFixa; } }
				    
					private static DomainKeyPair _ParcelaVariavel = new DomainKeyPair() { Value = "3", DisplayName = "Parcela Variável" };
					[FunctionalPoint("Value[3];DisplayName[Parcela Variável]")]
					public static DomainKeyPair ParcelaVariavel { get { return _ParcelaVariavel; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_STATUS_NFE
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Solicitar XML"); 
				    
					result.Add("2", "Erro na Solicitação do XML"); 
				    
					result.Add("3", "Solicitando XML"); 
				    
					result.Add("4", "Pendente de Autorização"); 
				    
					result.Add("5", "Autorizando"); 
				    
					result.Add("6", "Pendente de Consulta"); 
				    
					result.Add("7", "Consultando"); 
				    
					result.Add("8", "Autorizado"); 
				    
					result.Add("9", "Erro na Comunicação com o MID-e"); 
				    
					result.Add("10", "Erro da SEFAZ"); 
				    
					result.Add("11", "Cancelado"); 
				    
					result.Add("12", "Inutilizado"); 
				    
					result.Add("13", "Denegado"); 
				    
					result.Add("14", "Aguardando Informações"); 
				    
					result.Add("15", "Recebimento de NF-e de Entrada"); 
				    
					result.Add("16", "NF não Eletrônica"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "SolicitarXML"); 
				    
					result.Add("2", "ErroSolicitarXML"); 
				    
					result.Add("3", "SolicitandoXML"); 
				    
					result.Add("4", "PendenteAutorizacao"); 
				    
					result.Add("5", "Autorizando"); 
				    
					result.Add("6", "PendenteConsulta"); 
				    
					result.Add("7", "Consultando"); 
				    
					result.Add("8", "Autorizado"); 
				    
					result.Add("9", "ErroComunicacaoMID"); 
				    
					result.Add("10", "ErroSEFAZ"); 
				    
					result.Add("11", "Cancelado"); 
				    
					result.Add("12", "Inutilizado"); 
				    
					result.Add("13", "Denegado"); 
				    
					result.Add("14", "AguardandoInformacoes"); 
				    
					result.Add("15", "RecebimentoNFeEntrada"); 
				    
					result.Add("16", "NfNaoEletronica"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _SolicitarXML = new DomainKeyPair() { Value = "1", DisplayName = "Solicitar XML" };
					[FunctionalPoint("Value[1];DisplayName[Solicitar XML]")]
					public static DomainKeyPair SolicitarXML { get { return _SolicitarXML; } }
				    
					private static DomainKeyPair _ErroSolicitarXML = new DomainKeyPair() { Value = "2", DisplayName = "Erro na Solicitação do XML" };
					[FunctionalPoint("Value[2];DisplayName[Erro na Solicitação do XML]")]
					public static DomainKeyPair ErroSolicitarXML { get { return _ErroSolicitarXML; } }
				    
					private static DomainKeyPair _SolicitandoXML = new DomainKeyPair() { Value = "3", DisplayName = "Solicitando XML" };
					[FunctionalPoint("Value[3];DisplayName[Solicitando XML]")]
					public static DomainKeyPair SolicitandoXML { get { return _SolicitandoXML; } }
				    
					private static DomainKeyPair _PendenteAutorizacao = new DomainKeyPair() { Value = "4", DisplayName = "Pendente de Autorização" };
					[FunctionalPoint("Value[4];DisplayName[Pendente de Autorização]")]
					public static DomainKeyPair PendenteAutorizacao { get { return _PendenteAutorizacao; } }
				    
					private static DomainKeyPair _Autorizando = new DomainKeyPair() { Value = "5", DisplayName = "Autorizando" };
					[FunctionalPoint("Value[5];DisplayName[Autorizando]")]
					public static DomainKeyPair Autorizando { get { return _Autorizando; } }
				    
					private static DomainKeyPair _PendenteConsulta = new DomainKeyPair() { Value = "6", DisplayName = "Pendente de Consulta" };
					[FunctionalPoint("Value[6];DisplayName[Pendente de Consulta]")]
					public static DomainKeyPair PendenteConsulta { get { return _PendenteConsulta; } }
				    
					private static DomainKeyPair _Consultando = new DomainKeyPair() { Value = "7", DisplayName = "Consultando" };
					[FunctionalPoint("Value[7];DisplayName[Consultando]")]
					public static DomainKeyPair Consultando { get { return _Consultando; } }
				    
					private static DomainKeyPair _Autorizado = new DomainKeyPair() { Value = "8", DisplayName = "Autorizado" };
					[FunctionalPoint("Value[8];DisplayName[Autorizado]")]
					public static DomainKeyPair Autorizado { get { return _Autorizado; } }
				    
					private static DomainKeyPair _ErroComunicacaoMID = new DomainKeyPair() { Value = "9", DisplayName = "Erro na Comunicação com o MID-e" };
					[FunctionalPoint("Value[9];DisplayName[Erro na Comunicação com o MID-e]")]
					public static DomainKeyPair ErroComunicacaoMID { get { return _ErroComunicacaoMID; } }
				    
					private static DomainKeyPair _ErroSEFAZ = new DomainKeyPair() { Value = "10", DisplayName = "Erro da SEFAZ" };
					[FunctionalPoint("Value[10];DisplayName[Erro da SEFAZ]")]
					public static DomainKeyPair ErroSEFAZ { get { return _ErroSEFAZ; } }
				    
					private static DomainKeyPair _Cancelado = new DomainKeyPair() { Value = "11", DisplayName = "Cancelado" };
					[FunctionalPoint("Value[11];DisplayName[Cancelado]")]
					public static DomainKeyPair Cancelado { get { return _Cancelado; } }
				    
					private static DomainKeyPair _Inutilizado = new DomainKeyPair() { Value = "12", DisplayName = "Inutilizado" };
					[FunctionalPoint("Value[12];DisplayName[Inutilizado]")]
					public static DomainKeyPair Inutilizado { get { return _Inutilizado; } }
				    
					private static DomainKeyPair _Denegado = new DomainKeyPair() { Value = "13", DisplayName = "Denegado" };
					[FunctionalPoint("Value[13];DisplayName[Denegado]")]
					public static DomainKeyPair Denegado { get { return _Denegado; } }
				    
					private static DomainKeyPair _AguardandoInformacoes = new DomainKeyPair() { Value = "14", DisplayName = "Aguardando Informações" };
					[FunctionalPoint("Value[14];DisplayName[Aguardando Informações]")]
					public static DomainKeyPair AguardandoInformacoes { get { return _AguardandoInformacoes; } }
				    
					private static DomainKeyPair _RecebimentoNFeEntrada = new DomainKeyPair() { Value = "15", DisplayName = "Recebimento de NF-e de Entrada" };
					[FunctionalPoint("Value[15];DisplayName[Recebimento de NF-e de Entrada]")]
					public static DomainKeyPair RecebimentoNFeEntrada { get { return _RecebimentoNFeEntrada; } }
				    
					private static DomainKeyPair _NfNaoEletronica = new DomainKeyPair() { Value = "16", DisplayName = "NF não Eletrônica" };
					[FunctionalPoint("Value[16];DisplayName[NF não Eletrônica]")]
					public static DomainKeyPair NfNaoEletronica { get { return _NfNaoEletronica; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_TIPO_PARCEIRO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Envio de e-mails"); 
				    
					result.Add("2", "Envio de SMS"); 
				    
					result.Add("3", "Envio de mala direta"); 
				    
					result.Add("4", "Fornecedor de  Gift Card de conteúdo"); 
				    
					result.Add("5", "Limpeza de dados"); 
				    
					result.Add("6", "Fornecedor de Brinde"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "EnvioEmail"); 
				    
					result.Add("2", "EnvioSMS"); 
				    
					result.Add("3", "EnvioMalaDireta"); 
				    
					result.Add("4", "FornecedorGiftCard"); 
				    
					result.Add("5", "LimpezaDados"); 
				    
					result.Add("6", "FornecedorBrinde"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _EnvioEmail = new DomainKeyPair() { Value = "1", DisplayName = "Envio de e-mails" };
					[FunctionalPoint("Value[1];DisplayName[Envio de e-mails]")]
					public static DomainKeyPair EnvioEmail { get { return _EnvioEmail; } }
				    
					private static DomainKeyPair _EnvioSMS = new DomainKeyPair() { Value = "2", DisplayName = "Envio de SMS" };
					[FunctionalPoint("Value[2];DisplayName[Envio de SMS]")]
					public static DomainKeyPair EnvioSMS { get { return _EnvioSMS; } }
				    
					private static DomainKeyPair _EnvioMalaDireta = new DomainKeyPair() { Value = "3", DisplayName = "Envio de mala direta" };
					[FunctionalPoint("Value[3];DisplayName[Envio de mala direta]")]
					public static DomainKeyPair EnvioMalaDireta { get { return _EnvioMalaDireta; } }
				    
					private static DomainKeyPair _FornecedorGiftCard = new DomainKeyPair() { Value = "4", DisplayName = "Fornecedor de  Gift Card de conteúdo" };
					[FunctionalPoint("Value[4];DisplayName[Fornecedor de  Gift Card de conteúdo]")]
					public static DomainKeyPair FornecedorGiftCard { get { return _FornecedorGiftCard; } }
				    
					private static DomainKeyPair _LimpezaDados = new DomainKeyPair() { Value = "5", DisplayName = "Limpeza de dados" };
					[FunctionalPoint("Value[5];DisplayName[Limpeza de dados]")]
					public static DomainKeyPair LimpezaDados { get { return _LimpezaDados; } }
				    
					private static DomainKeyPair _FornecedorBrinde = new DomainKeyPair() { Value = "6", DisplayName = "Fornecedor de Brinde" };
					[FunctionalPoint("Value[6];DisplayName[Fornecedor de Brinde]")]
					public static DomainKeyPair FornecedorBrinde { get { return _FornecedorBrinde; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_STATUS_INTEGRACAO_FISCAL
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Não Integrado"); 
				    
					result.Add("2", "Integrado"); 
				    
					result.Add("9", "Trânsito Integração"); 
				    
					result.Add("3", "Reintegrar"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "NaoIntegrado"); 
				    
					result.Add("2", "Integrado"); 
				    
					result.Add("9", "TransitoIntegracao"); 
				    
					result.Add("3", "Reintegrar"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _NaoIntegrado = new DomainKeyPair() { Value = "1", DisplayName = "Não Integrado" };
					[FunctionalPoint("Value[1];DisplayName[Não Integrado]")]
					public static DomainKeyPair NaoIntegrado { get { return _NaoIntegrado; } }
				    
					private static DomainKeyPair _Integrado = new DomainKeyPair() { Value = "2", DisplayName = "Integrado" };
					[FunctionalPoint("Value[2];DisplayName[Integrado]")]
					public static DomainKeyPair Integrado { get { return _Integrado; } }
				    
					private static DomainKeyPair _TransitoIntegracao = new DomainKeyPair() { Value = "9", DisplayName = "Trânsito Integração" };
					[FunctionalPoint("Value[9];DisplayName[Trânsito Integração]")]
					public static DomainKeyPair TransitoIntegracao { get { return _TransitoIntegracao; } }
				    
					private static DomainKeyPair _Reintegrar = new DomainKeyPair() { Value = "3", DisplayName = "Reintegrar" };
					[FunctionalPoint("Value[3];DisplayName[Reintegrar]")]
					public static DomainKeyPair Reintegrar { get { return _Reintegrar; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_ORIGEM_DESCONTO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Pagamento"); 
				    
					result.Add("2", "Promoção"); 
				    
					result.Add("3", "Promoção Brinde"); 
				    
					result.Add("4", "Manual"); 
				    
					result.Add("5", "Operação"); 
				    
					result.Add("6", "Fidelidade"); 
				    
					result.Add("7", "Tabela de Preço"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Pagamento"); 
				    
					result.Add("2", "Promocao"); 
				    
					result.Add("3", "PromocaoBrinde"); 
				    
					result.Add("4", "Manual"); 
				    
					result.Add("5", "Operacao"); 
				    
					result.Add("6", "Fidelidade"); 
				    
					result.Add("7", "TabelaPreco"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Pagamento = new DomainKeyPair() { Value = "1", DisplayName = "Pagamento" };
					[FunctionalPoint("Value[1];DisplayName[Pagamento]")]
					public static DomainKeyPair Pagamento { get { return _Pagamento; } }
				    
					private static DomainKeyPair _Promocao = new DomainKeyPair() { Value = "2", DisplayName = "Promoção" };
					[FunctionalPoint("Value[2];DisplayName[Promoção]")]
					public static DomainKeyPair Promocao { get { return _Promocao; } }
				    
					private static DomainKeyPair _PromocaoBrinde = new DomainKeyPair() { Value = "3", DisplayName = "Promoção Brinde" };
					[FunctionalPoint("Value[3];DisplayName[Promoção Brinde]")]
					public static DomainKeyPair PromocaoBrinde { get { return _PromocaoBrinde; } }
				    
					private static DomainKeyPair _Manual = new DomainKeyPair() { Value = "4", DisplayName = "Manual" };
					[FunctionalPoint("Value[4];DisplayName[Manual]")]
					public static DomainKeyPair Manual { get { return _Manual; } }
				    
					private static DomainKeyPair _Operacao = new DomainKeyPair() { Value = "5", DisplayName = "Operação" };
					[FunctionalPoint("Value[5];DisplayName[Operação]")]
					public static DomainKeyPair Operacao { get { return _Operacao; } }
				    
					private static DomainKeyPair _Fidelidade = new DomainKeyPair() { Value = "6", DisplayName = "Fidelidade" };
					[FunctionalPoint("Value[6];DisplayName[Fidelidade]")]
					public static DomainKeyPair Fidelidade { get { return _Fidelidade; } }
				    
					private static DomainKeyPair _TabelaPreco = new DomainKeyPair() { Value = "7", DisplayName = "Tabela de Preço" };
					[FunctionalPoint("Value[7];DisplayName[Tabela de Preço]")]
					public static DomainKeyPair TabelaPreco { get { return _TabelaPreco; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_OPERADOR
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "E"); 
				    
					result.Add("2", "OU"); 
				    
					result.Add("3", "("); 
				    
					result.Add("4", "E ("); 
				    
					result.Add("5", "OU ("); 
				    
					result.Add("6", ")"); 
				    
					result.Add("7", ") E"); 
				    
					result.Add("8", ") OU"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "OperadorE"); 
				    
					result.Add("2", "OperadorOU"); 
				    
					result.Add("3", "OperadorAbreParentese"); 
				    
					result.Add("4", "OperadorE_AbreParentese"); 
				    
					result.Add("5", "OperadorOU_AbreParentese"); 
				    
					result.Add("6", "OperadorFechaParentese"); 
				    
					result.Add("7", "OperadorFechaParentese_E"); 
				    
					result.Add("8", "OperadorFechaParentese_OU"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _OperadorE = new DomainKeyPair() { Value = "1", DisplayName = "E" };
					[FunctionalPoint("Value[1];DisplayName[E]")]
					public static DomainKeyPair OperadorE { get { return _OperadorE; } }
				    
					private static DomainKeyPair _OperadorOU = new DomainKeyPair() { Value = "2", DisplayName = "OU" };
					[FunctionalPoint("Value[2];DisplayName[OU]")]
					public static DomainKeyPair OperadorOU { get { return _OperadorOU; } }
				    
					private static DomainKeyPair _OperadorAbreParentese = new DomainKeyPair() { Value = "3", DisplayName = "(" };
					[FunctionalPoint("Value[3];DisplayName[(]")]
					public static DomainKeyPair OperadorAbreParentese { get { return _OperadorAbreParentese; } }
				    
					private static DomainKeyPair _OperadorE_AbreParentese = new DomainKeyPair() { Value = "4", DisplayName = "E (" };
					[FunctionalPoint("Value[4];DisplayName[E (]")]
					public static DomainKeyPair OperadorE_AbreParentese { get { return _OperadorE_AbreParentese; } }
				    
					private static DomainKeyPair _OperadorOU_AbreParentese = new DomainKeyPair() { Value = "5", DisplayName = "OU (" };
					[FunctionalPoint("Value[5];DisplayName[OU (]")]
					public static DomainKeyPair OperadorOU_AbreParentese { get { return _OperadorOU_AbreParentese; } }
				    
					private static DomainKeyPair _OperadorFechaParentese = new DomainKeyPair() { Value = "6", DisplayName = ")" };
					[FunctionalPoint("Value[6];DisplayName[)]")]
					public static DomainKeyPair OperadorFechaParentese { get { return _OperadorFechaParentese; } }
				    
					private static DomainKeyPair _OperadorFechaParentese_E = new DomainKeyPair() { Value = "7", DisplayName = ") E" };
					[FunctionalPoint("Value[7];DisplayName[) E]")]
					public static DomainKeyPair OperadorFechaParentese_E { get { return _OperadorFechaParentese_E; } }
				    
					private static DomainKeyPair _OperadorFechaParentese_OU = new DomainKeyPair() { Value = "8", DisplayName = ") OU" };
					[FunctionalPoint("Value[8];DisplayName[) OU]")]
					public static DomainKeyPair OperadorFechaParentese_OU { get { return _OperadorFechaParentese_OU; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_TIPO_CODIGO_BARRA
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "GTIN-8"); 
				    
					result.Add("2", "GTIN-12"); 
				    
					result.Add("3", "GTIN-13"); 
				    
					result.Add("4", "GTIN-14"); 
				    
					result.Add("9", "Próprio"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "GTIN8"); 
				    
					result.Add("2", "GTIN12"); 
				    
					result.Add("3", "GTIN13"); 
				    
					result.Add("4", "GTIN14"); 
				    
					result.Add("9", "Proprio"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _GTIN8 = new DomainKeyPair() { Value = "1", DisplayName = "GTIN-8" };
					[FunctionalPoint("Value[1];DisplayName[GTIN-8]")]
					public static DomainKeyPair GTIN8 { get { return _GTIN8; } }
				    
					private static DomainKeyPair _GTIN12 = new DomainKeyPair() { Value = "2", DisplayName = "GTIN-12" };
					[FunctionalPoint("Value[2];DisplayName[GTIN-12]")]
					public static DomainKeyPair GTIN12 { get { return _GTIN12; } }
				    
					private static DomainKeyPair _GTIN13 = new DomainKeyPair() { Value = "3", DisplayName = "GTIN-13" };
					[FunctionalPoint("Value[3];DisplayName[GTIN-13]")]
					public static DomainKeyPair GTIN13 { get { return _GTIN13; } }
				    
					private static DomainKeyPair _GTIN14 = new DomainKeyPair() { Value = "4", DisplayName = "GTIN-14" };
					[FunctionalPoint("Value[4];DisplayName[GTIN-14]")]
					public static DomainKeyPair GTIN14 { get { return _GTIN14; } }
				    
					private static DomainKeyPair _Proprio = new DomainKeyPair() { Value = "9", DisplayName = "Próprio" };
					[FunctionalPoint("Value[9];DisplayName[Próprio]")]
					public static DomainKeyPair Proprio { get { return _Proprio; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_TIPO_FISCAL
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("100", "ECF"); 
				    
					result.Add("101", "NFCe"); 
				    
					result.Add("102", "NFe"); 
				    
					result.Add("103", "Pré Venda"); 
				    
					result.Add("104", "Desenvolvimento"); 
				    
					result.Add("105", "SAT"); 
				    
					result.Add("99", "Não se Aplica"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("100", "ECF"); 
				    
					result.Add("101", "NFCe"); 
				    
					result.Add("102", "NFe"); 
				    
					result.Add("103", "PreVenda"); 
				    
					result.Add("104", "Desenvolvimento"); 
				    
					result.Add("105", "SAT"); 
				    
					result.Add("99", "NaoSeAplica"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _ECF = new DomainKeyPair() { Value = "100", DisplayName = "ECF" };
					[FunctionalPoint("Value[100];DisplayName[ECF]")]
					public static DomainKeyPair ECF { get { return _ECF; } }
				    
					private static DomainKeyPair _NFCe = new DomainKeyPair() { Value = "101", DisplayName = "NFCe" };
					[FunctionalPoint("Value[101];DisplayName[NFCe]")]
					public static DomainKeyPair NFCe { get { return _NFCe; } }
				    
					private static DomainKeyPair _NFe = new DomainKeyPair() { Value = "102", DisplayName = "NFe" };
					[FunctionalPoint("Value[102];DisplayName[NFe]")]
					public static DomainKeyPair NFe { get { return _NFe; } }
				    
					private static DomainKeyPair _PreVenda = new DomainKeyPair() { Value = "103", DisplayName = "Pré Venda" };
					[FunctionalPoint("Value[103];DisplayName[Pré Venda]")]
					public static DomainKeyPair PreVenda { get { return _PreVenda; } }
				    
					private static DomainKeyPair _Desenvolvimento = new DomainKeyPair() { Value = "104", DisplayName = "Desenvolvimento" };
					[FunctionalPoint("Value[104];DisplayName[Desenvolvimento]")]
					public static DomainKeyPair Desenvolvimento { get { return _Desenvolvimento; } }
				    
					private static DomainKeyPair _SAT = new DomainKeyPair() { Value = "105", DisplayName = "SAT" };
					[FunctionalPoint("Value[105];DisplayName[SAT]")]
					public static DomainKeyPair SAT { get { return _SAT; } }
				    
					private static DomainKeyPair _NaoSeAplica = new DomainKeyPair() { Value = "99", DisplayName = "Não se Aplica" };
					[FunctionalPoint("Value[99];DisplayName[Não se Aplica]")]
					public static DomainKeyPair NaoSeAplica { get { return _NaoSeAplica; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_TIPO_ENDERECO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Residencial"); 
				    
					result.Add("2", "Comercial"); 
				    
					result.Add("3", "Coleta"); 
				    
					result.Add("4", "Entrega"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Residencial"); 
				    
					result.Add("2", "Comercial"); 
				    
					result.Add("3", "Coleta"); 
				    
					result.Add("4", "Entrega"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Residencial = new DomainKeyPair() { Value = "1", DisplayName = "Residencial" };
					[FunctionalPoint("Value[1];DisplayName[Residencial]")]
					public static DomainKeyPair Residencial { get { return _Residencial; } }
				    
					private static DomainKeyPair _Comercial = new DomainKeyPair() { Value = "2", DisplayName = "Comercial" };
					[FunctionalPoint("Value[2];DisplayName[Comercial]")]
					public static DomainKeyPair Comercial { get { return _Comercial; } }
				    
					private static DomainKeyPair _Coleta = new DomainKeyPair() { Value = "3", DisplayName = "Coleta" };
					[FunctionalPoint("Value[3];DisplayName[Coleta]")]
					public static DomainKeyPair Coleta { get { return _Coleta; } }
				    
					private static DomainKeyPair _Entrega = new DomainKeyPair() { Value = "4", DisplayName = "Entrega" };
					[FunctionalPoint("Value[4];DisplayName[Entrega]")]
					public static DomainKeyPair Entrega { get { return _Entrega; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_PEDIDO_ORIGEM
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("0", "Não Identificado"); 
				    
					result.Add("1", "Caixa"); 
				    
					result.Add("2", "Microterminal"); 
				    
					result.Add("3", "Mobile"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("0", "INDEFINIDO"); 
				    
					result.Add("1", "CAIXA"); 
				    
					result.Add("2", "MICROTERMINAL"); 
				    
					result.Add("3", "MOBILE"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _INDEFINIDO = new DomainKeyPair() { Value = "0", DisplayName = "Não Identificado" };
					[FunctionalPoint("Value[0];DisplayName[Não Identificado]")]
					public static DomainKeyPair INDEFINIDO { get { return _INDEFINIDO; } }
				    
					private static DomainKeyPair _CAIXA = new DomainKeyPair() { Value = "1", DisplayName = "Caixa" };
					[FunctionalPoint("Value[1];DisplayName[Caixa]")]
					public static DomainKeyPair CAIXA { get { return _CAIXA; } }
				    
					private static DomainKeyPair _MICROTERMINAL = new DomainKeyPair() { Value = "2", DisplayName = "Microterminal" };
					[FunctionalPoint("Value[2];DisplayName[Microterminal]")]
					public static DomainKeyPair MICROTERMINAL { get { return _MICROTERMINAL; } }
				    
					private static DomainKeyPair _MOBILE = new DomainKeyPair() { Value = "3", DisplayName = "Mobile" };
					[FunctionalPoint("Value[3];DisplayName[Mobile]")]
					public static DomainKeyPair MOBILE { get { return _MOBILE; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_ATENDIMENTO_ORIGEM
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("0", "Não Identificado"); 
				    
					result.Add("1", "Caixa"); 
				    
					result.Add("2", "Mobile"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("0", "INDEFINIDO"); 
				    
					result.Add("1", "CAIXA"); 
				    
					result.Add("2", "MOBILE"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _INDEFINIDO = new DomainKeyPair() { Value = "0", DisplayName = "Não Identificado" };
					[FunctionalPoint("Value[0];DisplayName[Não Identificado]")]
					public static DomainKeyPair INDEFINIDO { get { return _INDEFINIDO; } }
				    
					private static DomainKeyPair _CAIXA = new DomainKeyPair() { Value = "1", DisplayName = "Caixa" };
					[FunctionalPoint("Value[1];DisplayName[Caixa]")]
					public static DomainKeyPair CAIXA { get { return _CAIXA; } }
				    
					private static DomainKeyPair _MOBILE = new DomainKeyPair() { Value = "2", DisplayName = "Mobile" };
					[FunctionalPoint("Value[2];DisplayName[Mobile]")]
					public static DomainKeyPair MOBILE { get { return _MOBILE; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_MODALIDADE_FRETE
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("100", "Contratação do Frete por conta do Remetente (CIF)"); 
				    
					result.Add("1", "Contratação do Frete por conta do Destinatário (FOB)"); 
				    
					result.Add("2", "Contratação do Frete por conta do Remetente (FOB)"); 
				    
					result.Add("9", "Sem Ocorrência de Transporte"); 
				    
					result.Add("3", "Transporte Próprio por conta de Terceiro"); 
				    
					result.Add("4", "Transporte Próprio por conta do Destinatário"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("100", "PorContaEmitente"); 
				    
					result.Add("1", "PorContaDestinatário"); 
				    
					result.Add("2", "PorContaTerceiros"); 
				    
					result.Add("9", "SemFrete"); 
				    
					result.Add("3", "TranspPropPorContaTerceiro"); 
				    
					result.Add("4", "TranspPropPorContaDestinatario"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _PorContaEmitente = new DomainKeyPair() { Value = "100", DisplayName = "Contratação do Frete por conta do Remetente (CIF)" };
					[FunctionalPoint("Value[100];DisplayName[Contratação do Frete por conta do Remetente (CIF)]")]
					public static DomainKeyPair PorContaEmitente { get { return _PorContaEmitente; } }
				    
					private static DomainKeyPair _PorContaDestinatário = new DomainKeyPair() { Value = "1", DisplayName = "Contratação do Frete por conta do Destinatário (FOB)" };
					[FunctionalPoint("Value[1];DisplayName[Contratação do Frete por conta do Destinatário (FOB)]")]
					public static DomainKeyPair PorContaDestinatário { get { return _PorContaDestinatário; } }
				    
					private static DomainKeyPair _PorContaTerceiros = new DomainKeyPair() { Value = "2", DisplayName = "Contratação do Frete por conta do Remetente (FOB)" };
					[FunctionalPoint("Value[2];DisplayName[Contratação do Frete por conta do Remetente (FOB)]")]
					public static DomainKeyPair PorContaTerceiros { get { return _PorContaTerceiros; } }
				    
					private static DomainKeyPair _SemFrete = new DomainKeyPair() { Value = "9", DisplayName = "Sem Ocorrência de Transporte" };
					[FunctionalPoint("Value[9];DisplayName[Sem Ocorrência de Transporte]")]
					public static DomainKeyPair SemFrete { get { return _SemFrete; } }
				    
					private static DomainKeyPair _TranspPropPorContaTerceiro = new DomainKeyPair() { Value = "3", DisplayName = "Transporte Próprio por conta de Terceiro" };
					[FunctionalPoint("Value[3];DisplayName[Transporte Próprio por conta de Terceiro]")]
					public static DomainKeyPair TranspPropPorContaTerceiro { get { return _TranspPropPorContaTerceiro; } }
				    
					private static DomainKeyPair _TranspPropPorContaDestinatario = new DomainKeyPair() { Value = "4", DisplayName = "Transporte Próprio por conta do Destinatário" };
					[FunctionalPoint("Value[4];DisplayName[Transporte Próprio por conta do Destinatário]")]
					public static DomainKeyPair TranspPropPorContaDestinatario { get { return _TranspPropPorContaDestinatario; } }
				    
			#endregion properties

		

	}    
			
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
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Numerico"); 
				    
					result.Add("2", "Caractere"); 
				    
					result.Add("3", "Data"); 
				    
					result.Add("4", "Logico"); 
				    
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
			
    public partial class TipoConteudoObjeto
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Layout"); 
				    
					result.Add("2", "Mídia"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Layout"); 
				    
					result.Add("2", "Media"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Layout = new DomainKeyPair() { Value = "1", DisplayName = "Layout" };
					[FunctionalPoint("Value[1];DisplayName[Layout]")]
					public static DomainKeyPair Layout { get { return _Layout; } }
				    
					private static DomainKeyPair _Media = new DomainKeyPair() { Value = "2", DisplayName = "Mídia" };
					[FunctionalPoint("Value[2];DisplayName[Mídia]")]
					public static DomainKeyPair Media { get { return _Media; } }
				    
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
				
					result.Add("1", "Filtro BO"); 
				    
					result.Add("2", "Filtro EDM (Entity SQL)"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "FiltroBO"); 
				    
					result.Add("2", "FiltroEdm"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _FiltroBO = new DomainKeyPair() { Value = "1", DisplayName = "Filtro BO" };
					[FunctionalPoint("Value[1];DisplayName[Filtro BO]")]
					public static DomainKeyPair FiltroBO { get { return _FiltroBO; } }
				    
					private static DomainKeyPair _FiltroEdm = new DomainKeyPair() { Value = "2", DisplayName = "Filtro EDM (Entity SQL)" };
					[FunctionalPoint("Value[2];DisplayName[Filtro EDM (Entity SQL)]")]
					public static DomainKeyPair FiltroEdm { get { return _FiltroEdm; } }
				    
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
			
    public partial class LX_INDICADOR_MERCADORIA
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("P", "Produto"); 
				    
					result.Add("L", "Look"); 
				    
					result.Add("S", "Serviço"); 
				    
					result.Add("K", "Kit / Embalagem / Lista"); 
				    
					result.Add("V", "Veículo"); 
				    
					result.Add("M", "Medicamento"); 
				    
					result.Add("A", "Armamento"); 
				    
					result.Add("C", "Combustível e Lubrificante"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("P", "Produto"); 
				    
					result.Add("L", "Look"); 
				    
					result.Add("S", "Servico"); 
				    
					result.Add("K", "KitLista"); 
				    
					result.Add("V", "Veiculo"); 
				    
					result.Add("M", "Medicamento"); 
				    
					result.Add("A", "Armamento"); 
				    
					result.Add("C", "Combustivel"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Produto = new DomainKeyPair() { Value = "P", DisplayName = "Produto" };
					[FunctionalPoint("Value[P];DisplayName[Produto]")]
					public static DomainKeyPair Produto { get { return _Produto; } }
				    
					private static DomainKeyPair _Look = new DomainKeyPair() { Value = "L", DisplayName = "Look" };
					[FunctionalPoint("Value[L];DisplayName[Look]")]
					public static DomainKeyPair Look { get { return _Look; } }
				    
					private static DomainKeyPair _Servico = new DomainKeyPair() { Value = "S", DisplayName = "Serviço" };
					[FunctionalPoint("Value[S];DisplayName[Serviço]")]
					public static DomainKeyPair Servico { get { return _Servico; } }
				    
					private static DomainKeyPair _KitLista = new DomainKeyPair() { Value = "K", DisplayName = "Kit / Embalagem / Lista" };
					[FunctionalPoint("Value[K];DisplayName[Kit / Embalagem / Lista]")]
					public static DomainKeyPair KitLista { get { return _KitLista; } }
				    
					private static DomainKeyPair _Veiculo = new DomainKeyPair() { Value = "V", DisplayName = "Veículo" };
					[FunctionalPoint("Value[V];DisplayName[Veículo]")]
					public static DomainKeyPair Veiculo { get { return _Veiculo; } }
				    
					private static DomainKeyPair _Medicamento = new DomainKeyPair() { Value = "M", DisplayName = "Medicamento" };
					[FunctionalPoint("Value[M];DisplayName[Medicamento]")]
					public static DomainKeyPair Medicamento { get { return _Medicamento; } }
				    
					private static DomainKeyPair _Armamento = new DomainKeyPair() { Value = "A", DisplayName = "Armamento" };
					[FunctionalPoint("Value[A];DisplayName[Armamento]")]
					public static DomainKeyPair Armamento { get { return _Armamento; } }
				    
					private static DomainKeyPair _Combustivel = new DomainKeyPair() { Value = "C", DisplayName = "Combustível e Lubrificante" };
					[FunctionalPoint("Value[C];DisplayName[Combustível e Lubrificante]")]
					public static DomainKeyPair Combustivel { get { return _Combustivel; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_EMBALAGEM
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Único"); 
				    
					result.Add("2", "Embalagem"); 
				    
					result.Add("3", "Kit"); 
				    
					result.Add("4", "Lista de Produtos"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Unico"); 
				    
					result.Add("2", "Embalagem"); 
				    
					result.Add("3", "Kit"); 
				    
					result.Add("4", "Lista"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Unico = new DomainKeyPair() { Value = "1", DisplayName = "Único" };
					[FunctionalPoint("Value[1];DisplayName[Único]")]
					public static DomainKeyPair Unico { get { return _Unico; } }
				    
					private static DomainKeyPair _Embalagem = new DomainKeyPair() { Value = "2", DisplayName = "Embalagem" };
					[FunctionalPoint("Value[2];DisplayName[Embalagem]")]
					public static DomainKeyPair Embalagem { get { return _Embalagem; } }
				    
					private static DomainKeyPair _Kit = new DomainKeyPair() { Value = "3", DisplayName = "Kit" };
					[FunctionalPoint("Value[3];DisplayName[Kit]")]
					public static DomainKeyPair Kit { get { return _Kit; } }
				    
					private static DomainKeyPair _Lista = new DomainKeyPair() { Value = "4", DisplayName = "Lista de Produtos" };
					[FunctionalPoint("Value[4];DisplayName[Lista de Produtos]")]
					public static DomainKeyPair Lista { get { return _Lista; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_TIPO_MENSAGEM
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Cliente Fidelidade que pontuou na venda"); 
				    
					result.Add("2", "Cliente Fidelidade que resgatou pontos na venda"); 
				    
					result.Add("3", "Cliente não Fidelidade"); 
				    
					result.Add("4", "Cliente não identificado na venda"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "PontuouVenda"); 
				    
					result.Add("2", "ResgatePontos"); 
				    
					result.Add("3", "ClienteNaoFidelizado"); 
				    
					result.Add("4", "ClienteNaoIdentificado"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _PontuouVenda = new DomainKeyPair() { Value = "1", DisplayName = "Cliente Fidelidade que pontuou na venda" };
					[FunctionalPoint("Value[1];DisplayName[Cliente Fidelidade que pontuou na venda]")]
					public static DomainKeyPair PontuouVenda { get { return _PontuouVenda; } }
				    
					private static DomainKeyPair _ResgatePontos = new DomainKeyPair() { Value = "2", DisplayName = "Cliente Fidelidade que resgatou pontos na venda" };
					[FunctionalPoint("Value[2];DisplayName[Cliente Fidelidade que resgatou pontos na venda]")]
					public static DomainKeyPair ResgatePontos { get { return _ResgatePontos; } }
				    
					private static DomainKeyPair _ClienteNaoFidelizado = new DomainKeyPair() { Value = "3", DisplayName = "Cliente não Fidelidade" };
					[FunctionalPoint("Value[3];DisplayName[Cliente não Fidelidade]")]
					public static DomainKeyPair ClienteNaoFidelizado { get { return _ClienteNaoFidelizado; } }
				    
					private static DomainKeyPair _ClienteNaoIdentificado = new DomainKeyPair() { Value = "4", DisplayName = "Cliente não identificado na venda" };
					[FunctionalPoint("Value[4];DisplayName[Cliente não identificado na venda]")]
					public static DomainKeyPair ClienteNaoIdentificado { get { return _ClienteNaoIdentificado; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_TIPO_CTRL_TIPO_PGTO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Venda"); 
				    
					result.Add("2", "Caixa"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Venda"); 
				    
					result.Add("2", "Caixa"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Venda = new DomainKeyPair() { Value = "1", DisplayName = "Venda" };
					[FunctionalPoint("Value[1];DisplayName[Venda]")]
					public static DomainKeyPair Venda { get { return _Venda; } }
				    
					private static DomainKeyPair _Caixa = new DomainKeyPair() { Value = "2", DisplayName = "Caixa" };
					[FunctionalPoint("Value[2];DisplayName[Caixa]")]
					public static DomainKeyPair Caixa { get { return _Caixa; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_STATUS_CONFERENCIA_CTRL
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Não Conferido"); 
				    
					result.Add("2", "Conferido"); 
				    
					result.Add("3", "Com Divergência"); 
				    
					result.Add("4", "Financeiro Gerado"); 
				    
					result.Add("9", "Não Gera Financeiro"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "NaoAplicaNaoConferido"); 
				    
					result.Add("2", "Conferido"); 
				    
					result.Add("3", "Divergencia"); 
				    
					result.Add("4", "Integrado"); 
				    
					result.Add("9", "NaoGeraFinanceiro"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _NaoAplicaNaoConferido = new DomainKeyPair() { Value = "1", DisplayName = "Não Conferido" };
					[FunctionalPoint("Value[1];DisplayName[Não Conferido]")]
					public static DomainKeyPair NaoAplicaNaoConferido { get { return _NaoAplicaNaoConferido; } }
				    
					private static DomainKeyPair _Conferido = new DomainKeyPair() { Value = "2", DisplayName = "Conferido" };
					[FunctionalPoint("Value[2];DisplayName[Conferido]")]
					public static DomainKeyPair Conferido { get { return _Conferido; } }
				    
					private static DomainKeyPair _Divergencia = new DomainKeyPair() { Value = "3", DisplayName = "Com Divergência" };
					[FunctionalPoint("Value[3];DisplayName[Com Divergência]")]
					public static DomainKeyPair Divergencia { get { return _Divergencia; } }
				    
					private static DomainKeyPair _Integrado = new DomainKeyPair() { Value = "4", DisplayName = "Financeiro Gerado" };
					[FunctionalPoint("Value[4];DisplayName[Financeiro Gerado]")]
					public static DomainKeyPair Integrado { get { return _Integrado; } }
				    
					private static DomainKeyPair _NaoGeraFinanceiro = new DomainKeyPair() { Value = "9", DisplayName = "Não Gera Financeiro" };
					[FunctionalPoint("Value[9];DisplayName[Não Gera Financeiro]")]
					public static DomainKeyPair NaoGeraFinanceiro { get { return _NaoGeraFinanceiro; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_TIPO_CONFERENCIA
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Tipo de Pagamento"); 
				    
					result.Add("2", "Terminal"); 
				    
					result.Add("3", "Período"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "TipoPgto"); 
				    
					result.Add("2", "Terminal"); 
				    
					result.Add("3", "Periodo"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _TipoPgto = new DomainKeyPair() { Value = "1", DisplayName = "Tipo de Pagamento" };
					[FunctionalPoint("Value[1];DisplayName[Tipo de Pagamento]")]
					public static DomainKeyPair TipoPgto { get { return _TipoPgto; } }
				    
					private static DomainKeyPair _Terminal = new DomainKeyPair() { Value = "2", DisplayName = "Terminal" };
					[FunctionalPoint("Value[2];DisplayName[Terminal]")]
					public static DomainKeyPair Terminal { get { return _Terminal; } }
				    
					private static DomainKeyPair _Periodo = new DomainKeyPair() { Value = "3", DisplayName = "Período" };
					[FunctionalPoint("Value[3];DisplayName[Período]")]
					public static DomainKeyPair Periodo { get { return _Periodo; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_TIPO_OCORRENCIA
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Inclusão"); 
				    
					result.Add("2", "Manutenção"); 
				    
					result.Add("3", "Exclusão"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Inclusao"); 
				    
					result.Add("2", "Manutencao"); 
				    
					result.Add("3", "ExclusaoInativacao"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Inclusao = new DomainKeyPair() { Value = "1", DisplayName = "Inclusão" };
					[FunctionalPoint("Value[1];DisplayName[Inclusão]")]
					public static DomainKeyPair Inclusao { get { return _Inclusao; } }
				    
					private static DomainKeyPair _Manutencao = new DomainKeyPair() { Value = "2", DisplayName = "Manutenção" };
					[FunctionalPoint("Value[2];DisplayName[Manutenção]")]
					public static DomainKeyPair Manutencao { get { return _Manutencao; } }
				    
					private static DomainKeyPair _ExclusaoInativacao = new DomainKeyPair() { Value = "3", DisplayName = "Exclusão" };
					[FunctionalPoint("Value[3];DisplayName[Exclusão]")]
					public static DomainKeyPair ExclusaoInativacao { get { return _ExclusaoInativacao; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_LOJA_TIPO_AGRUPAMENTO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Agrupamento Sortimento"); 
				    
					result.Add("2", "Agrupamento Comercial"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Sortimento"); 
				    
					result.Add("2", "Comercial"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Sortimento = new DomainKeyPair() { Value = "1", DisplayName = "Agrupamento Sortimento" };
					[FunctionalPoint("Value[1];DisplayName[Agrupamento Sortimento]")]
					public static DomainKeyPair Sortimento { get { return _Sortimento; } }
				    
					private static DomainKeyPair _Comercial = new DomainKeyPair() { Value = "2", DisplayName = "Agrupamento Comercial" };
					[FunctionalPoint("Value[2];DisplayName[Agrupamento Comercial]")]
					public static DomainKeyPair Comercial { get { return _Comercial; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_STATUS_CENARIO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Em Elaboração"); 
				    
					result.Add("2", "Ativo"); 
				    
					result.Add("3", "Inativo"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "EmElaboracao"); 
				    
					result.Add("2", "Ativo"); 
				    
					result.Add("3", "Inativo"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _EmElaboracao = new DomainKeyPair() { Value = "1", DisplayName = "Em Elaboração" };
					[FunctionalPoint("Value[1];DisplayName[Em Elaboração]")]
					public static DomainKeyPair EmElaboracao { get { return _EmElaboracao; } }
				    
					private static DomainKeyPair _Ativo = new DomainKeyPair() { Value = "2", DisplayName = "Ativo" };
					[FunctionalPoint("Value[2];DisplayName[Ativo]")]
					public static DomainKeyPair Ativo { get { return _Ativo; } }
				    
					private static DomainKeyPair _Inativo = new DomainKeyPair() { Value = "3", DisplayName = "Inativo" };
					[FunctionalPoint("Value[3];DisplayName[Inativo]")]
					public static DomainKeyPair Inativo { get { return _Inativo; } }
				    
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
			
    public partial class LX_STATUS_PROCESSO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Não Processado"); 
				    
					result.Add("2", "Processado"); 
				    
					result.Add("3", "Erro"); 
				    
					result.Add("4", "Em Processamento"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "NaoProcessado"); 
				    
					result.Add("2", "Processado"); 
				    
					result.Add("3", "Erro"); 
				    
					result.Add("4", "EmProcessamento"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _NaoProcessado = new DomainKeyPair() { Value = "1", DisplayName = "Não Processado" };
					[FunctionalPoint("Value[1];DisplayName[Não Processado]")]
					public static DomainKeyPair NaoProcessado { get { return _NaoProcessado; } }
				    
					private static DomainKeyPair _Processado = new DomainKeyPair() { Value = "2", DisplayName = "Processado" };
					[FunctionalPoint("Value[2];DisplayName[Processado]")]
					public static DomainKeyPair Processado { get { return _Processado; } }
				    
					private static DomainKeyPair _Erro = new DomainKeyPair() { Value = "3", DisplayName = "Erro" };
					[FunctionalPoint("Value[3];DisplayName[Erro]")]
					public static DomainKeyPair Erro { get { return _Erro; } }
				    
					private static DomainKeyPair _EmProcessamento = new DomainKeyPair() { Value = "4", DisplayName = "Em Processamento" };
					[FunctionalPoint("Value[4];DisplayName[Em Processamento]")]
					public static DomainKeyPair EmProcessamento { get { return _EmProcessamento; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_TIPO_REMARCACAO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Remarcação"); 
				    
					result.Add("2", "Promoção"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Remarcacao"); 
				    
					result.Add("2", "Promocao"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Remarcacao = new DomainKeyPair() { Value = "1", DisplayName = "Remarcação" };
					[FunctionalPoint("Value[1];DisplayName[Remarcação]")]
					public static DomainKeyPair Remarcacao { get { return _Remarcacao; } }
				    
					private static DomainKeyPair _Promocao = new DomainKeyPair() { Value = "2", DisplayName = "Promoção" };
					[FunctionalPoint("Value[2];DisplayName[Promoção]")]
					public static DomainKeyPair Promocao { get { return _Promocao; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_ORIGEM_MOVIMENTO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Capturada pelo Sistema"); 
				    
					result.Add("2", "Digitada na Loja"); 
				    
					result.Add("3", "Digitada na Retaguarda"); 
				    
					result.Add("4", "Capturada do Resumo da NFCe"); 
				    
					result.Add("5", "Capturada do Resumo do SAT"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "CapturadaSistema"); 
				    
					result.Add("2", "DigitadaLoja"); 
				    
					result.Add("3", "DigitadaRetaguarda"); 
				    
					result.Add("4", "CapturadaNFCe"); 
				    
					result.Add("5", "CapturadaSAT"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _CapturadaSistema = new DomainKeyPair() { Value = "1", DisplayName = "Capturada pelo Sistema" };
					[FunctionalPoint("Value[1];DisplayName[Capturada pelo Sistema]")]
					public static DomainKeyPair CapturadaSistema { get { return _CapturadaSistema; } }
				    
					private static DomainKeyPair _DigitadaLoja = new DomainKeyPair() { Value = "2", DisplayName = "Digitada na Loja" };
					[FunctionalPoint("Value[2];DisplayName[Digitada na Loja]")]
					public static DomainKeyPair DigitadaLoja { get { return _DigitadaLoja; } }
				    
					private static DomainKeyPair _DigitadaRetaguarda = new DomainKeyPair() { Value = "3", DisplayName = "Digitada na Retaguarda" };
					[FunctionalPoint("Value[3];DisplayName[Digitada na Retaguarda]")]
					public static DomainKeyPair DigitadaRetaguarda { get { return _DigitadaRetaguarda; } }
				    
					private static DomainKeyPair _CapturadaNFCe = new DomainKeyPair() { Value = "4", DisplayName = "Capturada do Resumo da NFCe" };
					[FunctionalPoint("Value[4];DisplayName[Capturada do Resumo da NFCe]")]
					public static DomainKeyPair CapturadaNFCe { get { return _CapturadaNFCe; } }
				    
					private static DomainKeyPair _CapturadaSAT = new DomainKeyPair() { Value = "5", DisplayName = "Capturada do Resumo do SAT" };
					[FunctionalPoint("Value[5];DisplayName[Capturada do Resumo do SAT]")]
					public static DomainKeyPair CapturadaSAT { get { return _CapturadaSAT; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_STATUS_REDUCAO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Não Processado"); 
				    
					result.Add("2", "Validado"); 
				    
					result.Add("3", "Ausência de Redução Anterior"); 
				    
					result.Add("9", "Erro"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "NaoProcessado"); 
				    
					result.Add("2", "Validado"); 
				    
					result.Add("3", "AusenciaReducaoAnterior"); 
				    
					result.Add("9", "Erro"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _NaoProcessado = new DomainKeyPair() { Value = "1", DisplayName = "Não Processado" };
					[FunctionalPoint("Value[1];DisplayName[Não Processado]")]
					public static DomainKeyPair NaoProcessado { get { return _NaoProcessado; } }
				    
					private static DomainKeyPair _Validado = new DomainKeyPair() { Value = "2", DisplayName = "Validado" };
					[FunctionalPoint("Value[2];DisplayName[Validado]")]
					public static DomainKeyPair Validado { get { return _Validado; } }
				    
					private static DomainKeyPair _AusenciaReducaoAnterior = new DomainKeyPair() { Value = "3", DisplayName = "Ausência de Redução Anterior" };
					[FunctionalPoint("Value[3];DisplayName[Ausência de Redução Anterior]")]
					public static DomainKeyPair AusenciaReducaoAnterior { get { return _AusenciaReducaoAnterior; } }
				    
					private static DomainKeyPair _Erro = new DomainKeyPair() { Value = "9", DisplayName = "Erro" };
					[FunctionalPoint("Value[9];DisplayName[Erro]")]
					public static DomainKeyPair Erro { get { return _Erro; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_STATUS_REQUISICAO_ITEM
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Aguardando Pedido"); 
				    
					result.Add("2", "Pedido Gerado"); 
				    
					result.Add("3", "Cancelado"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "AGUARDANDO_PEDIDO"); 
				    
					result.Add("2", "PEDIDO_GERADO"); 
				    
					result.Add("3", "CANCELADO"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _AGUARDANDO_PEDIDO = new DomainKeyPair() { Value = "1", DisplayName = "Aguardando Pedido" };
					[FunctionalPoint("Value[1];DisplayName[Aguardando Pedido]")]
					public static DomainKeyPair AGUARDANDO_PEDIDO { get { return _AGUARDANDO_PEDIDO; } }
				    
					private static DomainKeyPair _PEDIDO_GERADO = new DomainKeyPair() { Value = "2", DisplayName = "Pedido Gerado" };
					[FunctionalPoint("Value[2];DisplayName[Pedido Gerado]")]
					public static DomainKeyPair PEDIDO_GERADO { get { return _PEDIDO_GERADO; } }
				    
					private static DomainKeyPair _CANCELADO = new DomainKeyPair() { Value = "3", DisplayName = "Cancelado" };
					[FunctionalPoint("Value[3];DisplayName[Cancelado]")]
					public static DomainKeyPair CANCELADO { get { return _CANCELADO; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_STATUS_REQUISICAO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Aguardando Pedido"); 
				    
					result.Add("2", "Finalizado"); 
				    
					result.Add("3", "Cancelado"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "AGUARDANDO_PEDIDO"); 
				    
					result.Add("2", "FINALIZADO"); 
				    
					result.Add("3", "CANCELADO"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _AGUARDANDO_PEDIDO = new DomainKeyPair() { Value = "1", DisplayName = "Aguardando Pedido" };
					[FunctionalPoint("Value[1];DisplayName[Aguardando Pedido]")]
					public static DomainKeyPair AGUARDANDO_PEDIDO { get { return _AGUARDANDO_PEDIDO; } }
				    
					private static DomainKeyPair _FINALIZADO = new DomainKeyPair() { Value = "2", DisplayName = "Finalizado" };
					[FunctionalPoint("Value[2];DisplayName[Finalizado]")]
					public static DomainKeyPair FINALIZADO { get { return _FINALIZADO; } }
				    
					private static DomainKeyPair _CANCELADO = new DomainKeyPair() { Value = "3", DisplayName = "Cancelado" };
					[FunctionalPoint("Value[3];DisplayName[Cancelado]")]
					public static DomainKeyPair CANCELADO { get { return _CANCELADO; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_TIPO_AJUSTE
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Completo"); 
				    
					result.Add("2", "Parcial"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "COMPLETO"); 
				    
					result.Add("2", "PARCIAL"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _COMPLETO = new DomainKeyPair() { Value = "1", DisplayName = "Completo" };
					[FunctionalPoint("Value[1];DisplayName[Completo]")]
					public static DomainKeyPair COMPLETO { get { return _COMPLETO; } }
				    
					private static DomainKeyPair _PARCIAL = new DomainKeyPair() { Value = "2", DisplayName = "Parcial" };
					[FunctionalPoint("Value[2];DisplayName[Parcial]")]
					public static DomainKeyPair PARCIAL { get { return _PARCIAL; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_METODO_RECONTAGEM
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Todos os Itens do Setor"); 
				    
					result.Add("2", "Somente com Divergência"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "TodosOsItens"); 
				    
					result.Add("2", "SomenteComDivergência"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _TodosOsItens = new DomainKeyPair() { Value = "1", DisplayName = "Todos os Itens do Setor" };
					[FunctionalPoint("Value[1];DisplayName[Todos os Itens do Setor]")]
					public static DomainKeyPair TodosOsItens { get { return _TodosOsItens; } }
				    
					private static DomainKeyPair _SomenteComDivergência = new DomainKeyPair() { Value = "2", DisplayName = "Somente com Divergência" };
					[FunctionalPoint("Value[2];DisplayName[Somente com Divergência]")]
					public static DomainKeyPair SomenteComDivergência { get { return _SomenteComDivergência; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_STATUS_INVENTARIO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Em Definição"); 
				    
					result.Add("2", "Aguardando Coleta"); 
				    
					result.Add("3", "Aguardando Ajuste"); 
				    
					result.Add("4", "Finalizado"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "EmSetorizacao"); 
				    
					result.Add("2", "AguardandoColeta"); 
				    
					result.Add("3", "AguardandoAjuste"); 
				    
					result.Add("4", "InventarioFinalizado"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _EmSetorizacao = new DomainKeyPair() { Value = "1", DisplayName = "Em Definição" };
					[FunctionalPoint("Value[1];DisplayName[Em Definição]")]
					public static DomainKeyPair EmSetorizacao { get { return _EmSetorizacao; } }
				    
					private static DomainKeyPair _AguardandoColeta = new DomainKeyPair() { Value = "2", DisplayName = "Aguardando Coleta" };
					[FunctionalPoint("Value[2];DisplayName[Aguardando Coleta]")]
					public static DomainKeyPair AguardandoColeta { get { return _AguardandoColeta; } }
				    
					private static DomainKeyPair _AguardandoAjuste = new DomainKeyPair() { Value = "3", DisplayName = "Aguardando Ajuste" };
					[FunctionalPoint("Value[3];DisplayName[Aguardando Ajuste]")]
					public static DomainKeyPair AguardandoAjuste { get { return _AguardandoAjuste; } }
				    
					private static DomainKeyPair _InventarioFinalizado = new DomainKeyPair() { Value = "4", DisplayName = "Finalizado" };
					[FunctionalPoint("Value[4];DisplayName[Finalizado]")]
					public static DomainKeyPair InventarioFinalizado { get { return _InventarioFinalizado; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_STATUS_INVENTARIO_SETOR
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Aguardando Coleta"); 
				    
					result.Add("2", "Coleta Finalizada"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "AguardandoColeta"); 
				    
					result.Add("2", "ColetaFinalizada"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _AguardandoColeta = new DomainKeyPair() { Value = "1", DisplayName = "Aguardando Coleta" };
					[FunctionalPoint("Value[1];DisplayName[Aguardando Coleta]")]
					public static DomainKeyPair AguardandoColeta { get { return _AguardandoColeta; } }
				    
					private static DomainKeyPair _ColetaFinalizada = new DomainKeyPair() { Value = "2", DisplayName = "Coleta Finalizada" };
					[FunctionalPoint("Value[2];DisplayName[Coleta Finalizada]")]
					public static DomainKeyPair ColetaFinalizada { get { return _ColetaFinalizada; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_STATUS_CONFERENCIA
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Em Contagem"); 
				    
					result.Add("2", "Aguardando Confronto"); 
				    
					result.Add("3", "Conferência em Análise"); 
				    
					result.Add("4", "Conferência Finalizada"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "EmContagem"); 
				    
					result.Add("2", "AguardandoConfronto"); 
				    
					result.Add("3", "EmConfronto"); 
				    
					result.Add("4", "Finalizada"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _EmContagem = new DomainKeyPair() { Value = "1", DisplayName = "Em Contagem" };
					[FunctionalPoint("Value[1];DisplayName[Em Contagem]")]
					public static DomainKeyPair EmContagem { get { return _EmContagem; } }
				    
					private static DomainKeyPair _AguardandoConfronto = new DomainKeyPair() { Value = "2", DisplayName = "Aguardando Confronto" };
					[FunctionalPoint("Value[2];DisplayName[Aguardando Confronto]")]
					public static DomainKeyPair AguardandoConfronto { get { return _AguardandoConfronto; } }
				    
					private static DomainKeyPair _EmConfronto = new DomainKeyPair() { Value = "3", DisplayName = "Conferência em Análise" };
					[FunctionalPoint("Value[3];DisplayName[Conferência em Análise]")]
					public static DomainKeyPair EmConfronto { get { return _EmConfronto; } }
				    
					private static DomainKeyPair _Finalizada = new DomainKeyPair() { Value = "4", DisplayName = "Conferência Finalizada" };
					[FunctionalPoint("Value[4];DisplayName[Conferência Finalizada]")]
					public static DomainKeyPair Finalizada { get { return _Finalizada; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_STATUS_CONFRONTO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Conferência = NF"); 
				    
					result.Add("5", "Conferência > NF - Op1-Devolução"); 
				    
					result.Add("6", "Conferência > NF - Op2-Complementar"); 
				    
					result.Add("7", "Conferência < NF - Op1-Dev.Simbolica"); 
				    
					result.Add("1", "Aguarda Confronto"); 
				    
					result.Add("3", "Conferência > NF "); 
				    
					result.Add("4", "Conferência < NF "); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "ConfIgualNf"); 
				    
					result.Add("5", "ConfMaiorQueNfOp1"); 
				    
					result.Add("6", "ConfMaiorQueNfOp2"); 
				    
					result.Add("7", "ConfMenorQueNfOp1"); 
				    
					result.Add("1", "AguardaConfronto"); 
				    
					result.Add("3", "ConfMaiorQueNF"); 
				    
					result.Add("4", "ConfMenorQueNF"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _ConfIgualNf = new DomainKeyPair() { Value = "2", DisplayName = "Conferência = NF" };
					[FunctionalPoint("Value[2];DisplayName[Conferência = NF]")]
					public static DomainKeyPair ConfIgualNf { get { return _ConfIgualNf; } }
				    
					private static DomainKeyPair _ConfMaiorQueNfOp1 = new DomainKeyPair() { Value = "5", DisplayName = "Conferência > NF - Op1-Devolução" };
					[FunctionalPoint("Value[5];DisplayName[Conferência > NF - Op1-Devolução]")]
					public static DomainKeyPair ConfMaiorQueNfOp1 { get { return _ConfMaiorQueNfOp1; } }
				    
					private static DomainKeyPair _ConfMaiorQueNfOp2 = new DomainKeyPair() { Value = "6", DisplayName = "Conferência > NF - Op2-Complementar" };
					[FunctionalPoint("Value[6];DisplayName[Conferência > NF - Op2-Complementar]")]
					public static DomainKeyPair ConfMaiorQueNfOp2 { get { return _ConfMaiorQueNfOp2; } }
				    
					private static DomainKeyPair _ConfMenorQueNfOp1 = new DomainKeyPair() { Value = "7", DisplayName = "Conferência < NF - Op1-Dev.Simbolica" };
					[FunctionalPoint("Value[7];DisplayName[Conferência < NF - Op1-Dev.Simbolica]")]
					public static DomainKeyPair ConfMenorQueNfOp1 { get { return _ConfMenorQueNfOp1; } }
				    
					private static DomainKeyPair _AguardaConfronto = new DomainKeyPair() { Value = "1", DisplayName = "Aguarda Confronto" };
					[FunctionalPoint("Value[1];DisplayName[Aguarda Confronto]")]
					public static DomainKeyPair AguardaConfronto { get { return _AguardaConfronto; } }
				    
					private static DomainKeyPair _ConfMaiorQueNF = new DomainKeyPair() { Value = "3", DisplayName = "Conferência > NF " };
					[FunctionalPoint("Value[3];DisplayName[Conferência > NF ]")]
					public static DomainKeyPair ConfMaiorQueNF { get { return _ConfMaiorQueNF; } }
				    
					private static DomainKeyPair _ConfMenorQueNF = new DomainKeyPair() { Value = "4", DisplayName = "Conferência < NF " };
					[FunctionalPoint("Value[4];DisplayName[Conferência < NF ]")]
					public static DomainKeyPair ConfMenorQueNF { get { return _ConfMenorQueNF; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_STATUS_ROMANEIO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Não Considera"); 
				    
					result.Add("2", "Pendente"); 
				    
					result.Add("3", "Finalizado"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "NaoConsideraEstoque"); 
				    
					result.Add("2", "EstoquePendente"); 
				    
					result.Add("3", "EstoqueFinalizado"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _NaoConsideraEstoque = new DomainKeyPair() { Value = "1", DisplayName = "Não Considera" };
					[FunctionalPoint("Value[1];DisplayName[Não Considera]")]
					public static DomainKeyPair NaoConsideraEstoque { get { return _NaoConsideraEstoque; } }
				    
					private static DomainKeyPair _EstoquePendente = new DomainKeyPair() { Value = "2", DisplayName = "Pendente" };
					[FunctionalPoint("Value[2];DisplayName[Pendente]")]
					public static DomainKeyPair EstoquePendente { get { return _EstoquePendente; } }
				    
					private static DomainKeyPair _EstoqueFinalizado = new DomainKeyPair() { Value = "3", DisplayName = "Finalizado" };
					[FunctionalPoint("Value[3];DisplayName[Finalizado]")]
					public static DomainKeyPair EstoqueFinalizado { get { return _EstoqueFinalizado; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_TIPO_EMISSAO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Própria"); 
				    
					result.Add("2", "Terceiro"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Propria"); 
				    
					result.Add("2", "Terceiro"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Propria = new DomainKeyPair() { Value = "1", DisplayName = "Própria" };
					[FunctionalPoint("Value[1];DisplayName[Própria]")]
					public static DomainKeyPair Propria { get { return _Propria; } }
				    
					private static DomainKeyPair _Terceiro = new DomainKeyPair() { Value = "2", DisplayName = "Terceiro" };
					[FunctionalPoint("Value[2];DisplayName[Terceiro]")]
					public static DomainKeyPair Terceiro { get { return _Terceiro; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_OCORRENCIA_CUSTO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Processado"); 
				    
					result.Add("2", "Saldo Negativo"); 
				    
					result.Add("3", "Saldo sem composição "); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Processado"); 
				    
					result.Add("2", "SaldoNegativo"); 
				    
					result.Add("3", "SaldoSemComposicao"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Processado = new DomainKeyPair() { Value = "1", DisplayName = "Processado" };
					[FunctionalPoint("Value[1];DisplayName[Processado]")]
					public static DomainKeyPair Processado { get { return _Processado; } }
				    
					private static DomainKeyPair _SaldoNegativo = new DomainKeyPair() { Value = "2", DisplayName = "Saldo Negativo" };
					[FunctionalPoint("Value[2];DisplayName[Saldo Negativo]")]
					public static DomainKeyPair SaldoNegativo { get { return _SaldoNegativo; } }
				    
					private static DomainKeyPair _SaldoSemComposicao = new DomainKeyPair() { Value = "3", DisplayName = "Saldo sem composição " };
					[FunctionalPoint("Value[3];DisplayName[Saldo sem composição ]")]
					public static DomainKeyPair SaldoSemComposicao { get { return _SaldoSemComposicao; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_STATUS_DOCUMENTO_ROMANEIO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Em Elaboração"); 
				    
					result.Add("2", "Aguardando Nota Fiscal"); 
				    
					result.Add("3", "Aguardando Mercadoria"); 
				    
					result.Add("4", "Aguardando Autorização da NF"); 
				    
					result.Add("5", "Finalizado"); 
				    
					result.Add("9", "Cancelado"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "EmElaboracao"); 
				    
					result.Add("2", "AguardandoNF"); 
				    
					result.Add("3", "AguardandoMercadoria"); 
				    
					result.Add("4", "AguardandoAutorizacao"); 
				    
					result.Add("5", "Finalizado"); 
				    
					result.Add("9", "Cancelado"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _EmElaboracao = new DomainKeyPair() { Value = "1", DisplayName = "Em Elaboração" };
					[FunctionalPoint("Value[1];DisplayName[Em Elaboração]")]
					public static DomainKeyPair EmElaboracao { get { return _EmElaboracao; } }
				    
					private static DomainKeyPair _AguardandoNF = new DomainKeyPair() { Value = "2", DisplayName = "Aguardando Nota Fiscal" };
					[FunctionalPoint("Value[2];DisplayName[Aguardando Nota Fiscal]")]
					public static DomainKeyPair AguardandoNF { get { return _AguardandoNF; } }
				    
					private static DomainKeyPair _AguardandoMercadoria = new DomainKeyPair() { Value = "3", DisplayName = "Aguardando Mercadoria" };
					[FunctionalPoint("Value[3];DisplayName[Aguardando Mercadoria]")]
					public static DomainKeyPair AguardandoMercadoria { get { return _AguardandoMercadoria; } }
				    
					private static DomainKeyPair _AguardandoAutorizacao = new DomainKeyPair() { Value = "4", DisplayName = "Aguardando Autorização da NF" };
					[FunctionalPoint("Value[4];DisplayName[Aguardando Autorização da NF]")]
					public static DomainKeyPair AguardandoAutorizacao { get { return _AguardandoAutorizacao; } }
				    
					private static DomainKeyPair _Finalizado = new DomainKeyPair() { Value = "5", DisplayName = "Finalizado" };
					[FunctionalPoint("Value[5];DisplayName[Finalizado]")]
					public static DomainKeyPair Finalizado { get { return _Finalizado; } }
				    
					private static DomainKeyPair _Cancelado = new DomainKeyPair() { Value = "9", DisplayName = "Cancelado" };
					[FunctionalPoint("Value[9];DisplayName[Cancelado]")]
					public static DomainKeyPair Cancelado { get { return _Cancelado; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_INDICADOR_PRESENCA
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("100", "Nao se aplica"); 
				    
					result.Add("1", "Operação Presencial"); 
				    
					result.Add("2", "Operação pela Internet"); 
				    
					result.Add("3", "Operação por Teleatendimento"); 
				    
					result.Add("4", "NFC-e com Entrega em Domicílio"); 
				    
					result.Add("9", "Operação não presencial / Outros"); 
				    
					result.Add("5", "Operação presencial fora do estabelecimento"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("100", "NaoSeAplica"); 
				    
					result.Add("1", "OperacaoPresencial"); 
				    
					result.Add("2", "OperacaoInternet"); 
				    
					result.Add("3", "OperacaoTeleatendimento"); 
				    
					result.Add("4", "OperacaoEntegraDomicilio"); 
				    
					result.Add("9", "OperacaoNaoPresencial"); 
				    
					result.Add("5", "OperacaoPresencialForaEstabelecimento"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _NaoSeAplica = new DomainKeyPair() { Value = "100", DisplayName = "Nao se aplica" };
					[FunctionalPoint("Value[100];DisplayName[Nao se aplica]")]
					public static DomainKeyPair NaoSeAplica { get { return _NaoSeAplica; } }
				    
					private static DomainKeyPair _OperacaoPresencial = new DomainKeyPair() { Value = "1", DisplayName = "Operação Presencial" };
					[FunctionalPoint("Value[1];DisplayName[Operação Presencial]")]
					public static DomainKeyPair OperacaoPresencial { get { return _OperacaoPresencial; } }
				    
					private static DomainKeyPair _OperacaoInternet = new DomainKeyPair() { Value = "2", DisplayName = "Operação pela Internet" };
					[FunctionalPoint("Value[2];DisplayName[Operação pela Internet]")]
					public static DomainKeyPair OperacaoInternet { get { return _OperacaoInternet; } }
				    
					private static DomainKeyPair _OperacaoTeleatendimento = new DomainKeyPair() { Value = "3", DisplayName = "Operação por Teleatendimento" };
					[FunctionalPoint("Value[3];DisplayName[Operação por Teleatendimento]")]
					public static DomainKeyPair OperacaoTeleatendimento { get { return _OperacaoTeleatendimento; } }
				    
					private static DomainKeyPair _OperacaoEntegraDomicilio = new DomainKeyPair() { Value = "4", DisplayName = "NFC-e com Entrega em Domicílio" };
					[FunctionalPoint("Value[4];DisplayName[NFC-e com Entrega em Domicílio]")]
					public static DomainKeyPair OperacaoEntegraDomicilio { get { return _OperacaoEntegraDomicilio; } }
				    
					private static DomainKeyPair _OperacaoNaoPresencial = new DomainKeyPair() { Value = "9", DisplayName = "Operação não presencial / Outros" };
					[FunctionalPoint("Value[9];DisplayName[Operação não presencial / Outros]")]
					public static DomainKeyPair OperacaoNaoPresencial { get { return _OperacaoNaoPresencial; } }
				    
					private static DomainKeyPair _OperacaoPresencialForaEstabelecimento = new DomainKeyPair() { Value = "5", DisplayName = "Operação presencial fora do estabelecimento" };
					[FunctionalPoint("Value[5];DisplayName[Operação presencial fora do estabelecimento]")]
					public static DomainKeyPair OperacaoPresencialForaEstabelecimento { get { return _OperacaoPresencialForaEstabelecimento; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_PROPRIEDADE_STK
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Estoque Próprio"); 
				    
					result.Add("2", "Estoque de Terceiro"); 
				    
					result.Add("3", "Estoque com Terceiro"); 
				    
					result.Add("4", "Estoque de Terceiro com Terceiro"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Proprio"); 
				    
					result.Add("2", "DeTerceiro"); 
				    
					result.Add("3", "ComTerceiro"); 
				    
					result.Add("4", "DeTerceiroComTerceiro"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Proprio = new DomainKeyPair() { Value = "1", DisplayName = "Estoque Próprio" };
					[FunctionalPoint("Value[1];DisplayName[Estoque Próprio]")]
					public static DomainKeyPair Proprio { get { return _Proprio; } }
				    
					private static DomainKeyPair _DeTerceiro = new DomainKeyPair() { Value = "2", DisplayName = "Estoque de Terceiro" };
					[FunctionalPoint("Value[2];DisplayName[Estoque de Terceiro]")]
					public static DomainKeyPair DeTerceiro { get { return _DeTerceiro; } }
				    
					private static DomainKeyPair _ComTerceiro = new DomainKeyPair() { Value = "3", DisplayName = "Estoque com Terceiro" };
					[FunctionalPoint("Value[3];DisplayName[Estoque com Terceiro]")]
					public static DomainKeyPair ComTerceiro { get { return _ComTerceiro; } }
				    
					private static DomainKeyPair _DeTerceiroComTerceiro = new DomainKeyPair() { Value = "4", DisplayName = "Estoque de Terceiro com Terceiro" };
					[FunctionalPoint("Value[4];DisplayName[Estoque de Terceiro com Terceiro]")]
					public static DomainKeyPair DeTerceiroComTerceiro { get { return _DeTerceiroComTerceiro; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_LOJA_SORTIMENTO_METODO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Venda Período"); 
				    
					result.Add("2", "Estoque Mínimo"); 
				    
					result.Add("3", "Estoque Ideal"); 
				    
					result.Add("4", "Calcular Estoque Ideal"); 
				    
					result.Add("8", "Importação"); 
				    
					result.Add("9", "Digitação Mínimo, Máximo e Ideal"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "VendaPeriodo"); 
				    
					result.Add("2", "EstoqueMinimo"); 
				    
					result.Add("3", "EstoqueIdeal"); 
				    
					result.Add("4", "EstoqueIdealCalculado"); 
				    
					result.Add("8", "Importacao"); 
				    
					result.Add("9", "Digitacao"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _VendaPeriodo = new DomainKeyPair() { Value = "1", DisplayName = "Venda Período" };
					[FunctionalPoint("Value[1];DisplayName[Venda Período]")]
					public static DomainKeyPair VendaPeriodo { get { return _VendaPeriodo; } }
				    
					private static DomainKeyPair _EstoqueMinimo = new DomainKeyPair() { Value = "2", DisplayName = "Estoque Mínimo" };
					[FunctionalPoint("Value[2];DisplayName[Estoque Mínimo]")]
					public static DomainKeyPair EstoqueMinimo { get { return _EstoqueMinimo; } }
				    
					private static DomainKeyPair _EstoqueIdeal = new DomainKeyPair() { Value = "3", DisplayName = "Estoque Ideal" };
					[FunctionalPoint("Value[3];DisplayName[Estoque Ideal]")]
					public static DomainKeyPair EstoqueIdeal { get { return _EstoqueIdeal; } }
				    
					private static DomainKeyPair _EstoqueIdealCalculado = new DomainKeyPair() { Value = "4", DisplayName = "Calcular Estoque Ideal" };
					[FunctionalPoint("Value[4];DisplayName[Calcular Estoque Ideal]")]
					public static DomainKeyPair EstoqueIdealCalculado { get { return _EstoqueIdealCalculado; } }
				    
					private static DomainKeyPair _Importacao = new DomainKeyPair() { Value = "8", DisplayName = "Importação" };
					[FunctionalPoint("Value[8];DisplayName[Importação]")]
					public static DomainKeyPair Importacao { get { return _Importacao; } }
				    
					private static DomainKeyPair _Digitacao = new DomainKeyPair() { Value = "9", DisplayName = "Digitação Mínimo, Máximo e Ideal" };
					[FunctionalPoint("Value[9];DisplayName[Digitação Mínimo, Máximo e Ideal]")]
					public static DomainKeyPair Digitacao { get { return _Digitacao; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_STATUS_GERACAO_COMPRA
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Gerar Compra"); 
				    
					result.Add("2", "Não Gerar Compra"); 
				    
					result.Add("3", "Compra Gerada"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "GerarCompra"); 
				    
					result.Add("2", "NaoGerarCompra"); 
				    
					result.Add("3", "CompraGerada"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _GerarCompra = new DomainKeyPair() { Value = "1", DisplayName = "Gerar Compra" };
					[FunctionalPoint("Value[1];DisplayName[Gerar Compra]")]
					public static DomainKeyPair GerarCompra { get { return _GerarCompra; } }
				    
					private static DomainKeyPair _NaoGerarCompra = new DomainKeyPair() { Value = "2", DisplayName = "Não Gerar Compra" };
					[FunctionalPoint("Value[2];DisplayName[Não Gerar Compra]")]
					public static DomainKeyPair NaoGerarCompra { get { return _NaoGerarCompra; } }
				    
					private static DomainKeyPair _CompraGerada = new DomainKeyPair() { Value = "3", DisplayName = "Compra Gerada" };
					[FunctionalPoint("Value[3];DisplayName[Compra Gerada]")]
					public static DomainKeyPair CompraGerada { get { return _CompraGerada; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_STATUS_NF_DOC_FISCAL
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Pendente"); 
				    
					result.Add("2", "Autorizado"); 
				    
					result.Add("3", "Cancelado"); 
				    
					result.Add("4", "Inutilizado"); 
				    
					result.Add("5", "Denegado"); 
				    
					result.Add("6", "Contingência Offline"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Pendente"); 
				    
					result.Add("2", "Autorizado"); 
				    
					result.Add("3", "Cancelado"); 
				    
					result.Add("4", "Inutilizado"); 
				    
					result.Add("5", "Denegado"); 
				    
					result.Add("6", "ContingenciaOffLine"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Pendente = new DomainKeyPair() { Value = "1", DisplayName = "Pendente" };
					[FunctionalPoint("Value[1];DisplayName[Pendente]")]
					public static DomainKeyPair Pendente { get { return _Pendente; } }
				    
					private static DomainKeyPair _Autorizado = new DomainKeyPair() { Value = "2", DisplayName = "Autorizado" };
					[FunctionalPoint("Value[2];DisplayName[Autorizado]")]
					public static DomainKeyPair Autorizado { get { return _Autorizado; } }
				    
					private static DomainKeyPair _Cancelado = new DomainKeyPair() { Value = "3", DisplayName = "Cancelado" };
					[FunctionalPoint("Value[3];DisplayName[Cancelado]")]
					public static DomainKeyPair Cancelado { get { return _Cancelado; } }
				    
					private static DomainKeyPair _Inutilizado = new DomainKeyPair() { Value = "4", DisplayName = "Inutilizado" };
					[FunctionalPoint("Value[4];DisplayName[Inutilizado]")]
					public static DomainKeyPair Inutilizado { get { return _Inutilizado; } }
				    
					private static DomainKeyPair _Denegado = new DomainKeyPair() { Value = "5", DisplayName = "Denegado" };
					[FunctionalPoint("Value[5];DisplayName[Denegado]")]
					public static DomainKeyPair Denegado { get { return _Denegado; } }
				    
					private static DomainKeyPair _ContingenciaOffLine = new DomainKeyPair() { Value = "6", DisplayName = "Contingência Offline" };
					[FunctionalPoint("Value[6];DisplayName[Contingência Offline]")]
					public static DomainKeyPair ContingenciaOffLine { get { return _ContingenciaOffLine; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_TIPO_VALOR_ATENDIMENTO_ATRIBUTO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Saldo de Pontos"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "SaldoPontos"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _SaldoPontos = new DomainKeyPair() { Value = "1", DisplayName = "Saldo de Pontos" };
					[FunctionalPoint("Value[1];DisplayName[Saldo de Pontos]")]
					public static DomainKeyPair SaldoPontos { get { return _SaldoPontos; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_ORIGEM_ATENDIMENTO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Venda Loja Física"); 
				    
					result.Add("2", "Venda Direta"); 
				    
					result.Add("3", "Ecommerce"); 
				    
					result.Add("4", "Televenda"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "VendaLojaFisica"); 
				    
					result.Add("2", "VendaDireta"); 
				    
					result.Add("3", "Ecommerce"); 
				    
					result.Add("4", "Televenda"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _VendaLojaFisica = new DomainKeyPair() { Value = "1", DisplayName = "Venda Loja Física" };
					[FunctionalPoint("Value[1];DisplayName[Venda Loja Física]")]
					public static DomainKeyPair VendaLojaFisica { get { return _VendaLojaFisica; } }
				    
					private static DomainKeyPair _VendaDireta = new DomainKeyPair() { Value = "2", DisplayName = "Venda Direta" };
					[FunctionalPoint("Value[2];DisplayName[Venda Direta]")]
					public static DomainKeyPair VendaDireta { get { return _VendaDireta; } }
				    
					private static DomainKeyPair _Ecommerce = new DomainKeyPair() { Value = "3", DisplayName = "Ecommerce" };
					[FunctionalPoint("Value[3];DisplayName[Ecommerce]")]
					public static DomainKeyPair Ecommerce { get { return _Ecommerce; } }
				    
					private static DomainKeyPair _Televenda = new DomainKeyPair() { Value = "4", DisplayName = "Televenda" };
					[FunctionalPoint("Value[4];DisplayName[Televenda]")]
					public static DomainKeyPair Televenda { get { return _Televenda; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_STATUS_COMISSAO_PERIODO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Em preparação"); 
				    
					result.Add("2", "Aguardando Processamento"); 
				    
					result.Add("3", "Processado"); 
				    
					result.Add("9", "Finalizado"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "EmPreparacao"); 
				    
					result.Add("2", "AguardandoProcessamento"); 
				    
					result.Add("3", "Processado"); 
				    
					result.Add("9", "Finalizado"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _EmPreparacao = new DomainKeyPair() { Value = "1", DisplayName = "Em preparação" };
					[FunctionalPoint("Value[1];DisplayName[Em preparação]")]
					public static DomainKeyPair EmPreparacao { get { return _EmPreparacao; } }
				    
					private static DomainKeyPair _AguardandoProcessamento = new DomainKeyPair() { Value = "2", DisplayName = "Aguardando Processamento" };
					[FunctionalPoint("Value[2];DisplayName[Aguardando Processamento]")]
					public static DomainKeyPair AguardandoProcessamento { get { return _AguardandoProcessamento; } }
				    
					private static DomainKeyPair _Processado = new DomainKeyPair() { Value = "3", DisplayName = "Processado" };
					[FunctionalPoint("Value[3];DisplayName[Processado]")]
					public static DomainKeyPair Processado { get { return _Processado; } }
				    
					private static DomainKeyPair _Finalizado = new DomainKeyPair() { Value = "9", DisplayName = "Finalizado" };
					[FunctionalPoint("Value[9];DisplayName[Finalizado]")]
					public static DomainKeyPair Finalizado { get { return _Finalizado; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_COMISSAO_PROCESSO_TIPO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Grupo Econômico - Comissão Base"); 
				    
					result.Add("2", "Loja - Comissão Base"); 
				    
					result.Add("3", "Funcionário - Comissão Base"); 
				    
					result.Add("4", "Grupo Econômico - Comissão Base - Operação"); 
				    
					result.Add("5", "Loja - Comissão Base - Operação"); 
				    
					result.Add("6", "Grupo Econômico - Comissão Base - Promoção"); 
				    
					result.Add("7", "Loja - Comissão Base - Promoção"); 
				    
					result.Add("8", "Grupo Econômico - Prêmio - Meta Loja"); 
				    
					result.Add("9", "Loja - Prêmio - Meta Loja"); 
				    
					result.Add("10", "Funcionário - Prêmio - Meta Loja"); 
				    
					result.Add("11", "Grupo Econômico - Prêmio - Meta Vendedor"); 
				    
					result.Add("12", "Loja - Prêmio - Meta Vendedor"); 
				    
					result.Add("13", "Funcionário - Prêmio - Meta Vendedor"); 
				    
					result.Add("14", "Grupo Econômico - Prêmio - Ticket Médio"); 
				    
					result.Add("15", "Loja - Prêmio - Ticket Médio"); 
				    
					result.Add("16", "Funcionário - Prêmio - Ticket Médio"); 
				    
					result.Add("17", "Grupo Econômico - Prêmio - Venda Cartão Presente"); 
				    
					result.Add("18", "Loja - Prêmio - Venda Cartão Presente"); 
				    
					result.Add("19", "Funcionário - Prêmio - Venda Cartão Presente"); 
				    
					result.Add("20", "Grupo Econômico - Prêmio - Venda Produto"); 
				    
					result.Add("21", "Loja - Prêmio - Venda Produto"); 
				    
					result.Add("23", "Grupo Econômico - Prêmio - Quantidade de Cupons"); 
				    
					result.Add("24", "Loja - Prêmio - Quantidade de Cupons"); 
				    
					result.Add("25", "Funcionário - Prêmio - Quantidade de Cupons"); 
				    
					result.Add("26", "Grupo Econômico - Prêmio - Valor de Cupom"); 
				    
					result.Add("27", "Loja - Prêmio - Valor de Cupom"); 
				    
					result.Add("28", "Funcionário - Prêmio - Valor de Cupom"); 
				    
					result.Add("29", "Grupo Econômico - Prêmio - Tipo Pagamento"); 
				    
					result.Add("30", "Loja - Prêmio - Tipo Pagamento"); 
				    
					result.Add("31", "Grupo Econômico - Prêmio - Superação de Meta"); 
				    
					result.Add("32", "Loja - Prêmio - Superação de Meta"); 
				    
					result.Add("33", "Funcionário - Prêmio - Superação de Meta"); 
				    
					result.Add("99", "Não Elegível"); 
				    
					result.Add("98", "Fonte Externa de Valores"); 
				    
					result.Add("34", "Grupo Econômico - Adicional - Governo"); 
				    
					result.Add("35", "Loja - Adicional - Governo"); 
				    
					result.Add("36", "Funcionário - Adicional - Governo"); 
				    
					result.Add("37", "Funcionário - Prêmio - Venda Produto"); 
				    
					result.Add("38", "Grupo Econômico - Prêmio - Adicional de Superação de Meta"); 
				    
					result.Add("39", "Loja - Prêmio - Adicional de Superação de Meta"); 
				    
					result.Add("40", "Funcionário - Prêmio - Adicional de Superação de Meta"); 
				    
					result.Add("41", "UF - Comissão Base"); 
				    
					result.Add("42", "UF - Comissão Base - Operação"); 
				    
					result.Add("43", "UF - Comissão Base - Promoção"); 
				    
					result.Add("44", "UF - Prêmio - Meta Loja"); 
				    
					result.Add("45", "UF - Prêmio - Meta Vendedor"); 
				    
					result.Add("46", "UF - Prêmio - Ticket Médio"); 
				    
					result.Add("47", "UF - Prêmio - Venda Cartão Presente"); 
				    
					result.Add("48", "UF - Prêmio - Venda Produto"); 
				    
					result.Add("49", "UF - Prêmio - Quantidade de Cupons"); 
				    
					result.Add("50", "UF - Prêmio - Valor de Cupom"); 
				    
					result.Add("51", "UF - Prêmio - Tipo Pagamento"); 
				    
					result.Add("52", "UF - Prêmio - Superação de Meta"); 
				    
					result.Add("53", "UF - Adicional - Governo"); 
				    
					result.Add("54", "UF - Prêmio - Adicional de Superação de Meta"); 
				    
					result.Add("55", "Grupo Econômico - Prêmio - Venda Fidelidade"); 
				    
					result.Add("56", "Loja - Prêmio - Venda Fidelidade"); 
				    
					result.Add("57", "Funcionário - Prêmio - Venda Fidelidade"); 
				    
					result.Add("58", "UF - Prêmio - Venda Fidelidade"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "ComissaoBaseGrupoEconomico"); 
				    
					result.Add("2", "ComissaoBaseLoja"); 
				    
					result.Add("3", "ComissaoBaseFuncionario"); 
				    
					result.Add("4", "ComissaoBaseOperacaoGrupoEconomico"); 
				    
					result.Add("5", "ComissaoBaseOperacaoLoja"); 
				    
					result.Add("6", "ComissaoBasePromocaoGrupoEconomico"); 
				    
					result.Add("7", "ComissaoBasePromocaoLoja"); 
				    
					result.Add("8", "PremioMetaLojaGrupoEconomico"); 
				    
					result.Add("9", "PremioMetaLojaLoja"); 
				    
					result.Add("10", "PremioMetaLojaFuncionario"); 
				    
					result.Add("11", "PremioMetaVendedorGrupoEconomico"); 
				    
					result.Add("12", "PremioMetaVendedorLoja"); 
				    
					result.Add("13", "PremioMetaVendedorFuncionario"); 
				    
					result.Add("14", "PremioTicketMedioGrupoEconomico"); 
				    
					result.Add("15", "PremioTicketMedioLoja"); 
				    
					result.Add("16", "PremioTicketMedioFuncionario"); 
				    
					result.Add("17", "PremioVendaCartaoPresenteGrupoEconomico"); 
				    
					result.Add("18", "PremioVendaCartaoPresenteLoja"); 
				    
					result.Add("19", "PremioVendaCartaoPresenteFuncionario"); 
				    
					result.Add("20", "PremioVendaProdutoGrupoEconomico"); 
				    
					result.Add("21", "PremioVendaProdutoLoja"); 
				    
					result.Add("23", "PremioMinimoQtdeCupomGrupoEconomico"); 
				    
					result.Add("24", "PremioMinimoQtdeCupomLoja"); 
				    
					result.Add("25", "PremioMinimoQtdeCupomFuncionario"); 
				    
					result.Add("26", "PremioMinimoValorCupomGrupoEconomico"); 
				    
					result.Add("27", "PremioMinimoValorCupomLoja"); 
				    
					result.Add("28", "PremioMinimoValorCupomFuncionario"); 
				    
					result.Add("29", "PremioTipoPagamentoGrupoEconomico"); 
				    
					result.Add("30", "PremioTipoPagamentoLoja"); 
				    
					result.Add("31", "PremioMetaVendedorSuperarGrupoEconomico"); 
				    
					result.Add("32", "PremioMetaVendedorSuperarLoja"); 
				    
					result.Add("33", "PremioMetaVendedorSuperarFuncionario"); 
				    
					result.Add("99", "NaoElegivel"); 
				    
					result.Add("98", "FonteExternaValores"); 
				    
					result.Add("34", "AdicionalGovernoGrupoEconomico"); 
				    
					result.Add("35", "AdicionalGovernoLoja"); 
				    
					result.Add("36", "AdicionalGovernoFuncionario"); 
				    
					result.Add("37", "PremioVendaProdutoFuncionario"); 
				    
					result.Add("38", "PremioMetaVendedorSuperarAdicionalGrupoEconomico"); 
				    
					result.Add("39", "PremioMetaVendedorSuperarAdicionalLoja"); 
				    
					result.Add("40", "PremioMetaVendedorSuperarAdicionalFuncionario"); 
				    
					result.Add("41", "ComissaoBaseUF"); 
				    
					result.Add("42", "ComissaoBaseOperacaoUF"); 
				    
					result.Add("43", "ComissaoBasePromocaoUF"); 
				    
					result.Add("44", "PremioMetaLojaUF"); 
				    
					result.Add("45", "PremioMetaVendedorUF"); 
				    
					result.Add("46", "PremioTicketMedioUF"); 
				    
					result.Add("47", "PremioVendaCartaoPresenteUF"); 
				    
					result.Add("48", "PremioVendaProdutoUF"); 
				    
					result.Add("49", "PremioMinimoQtdeCupomUF"); 
				    
					result.Add("50", "PremioMinimoValorCupomUF"); 
				    
					result.Add("51", "PremioTipoPagamentoUF"); 
				    
					result.Add("52", "PremioMetaVendedorSuperarUF"); 
				    
					result.Add("53", "AdicionalGovernoUF"); 
				    
					result.Add("54", "PremioMetaVendedorSuperarAdicionalUF"); 
				    
					result.Add("55", "PremioVendaFidelidadeGrupoEconomico"); 
				    
					result.Add("56", "PremioVendaFidelidadeLoja"); 
				    
					result.Add("57", "PremioVendaFidelidadeFuncionario"); 
				    
					result.Add("58", "PremioVendaFidelidadeUF"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _ComissaoBaseGrupoEconomico = new DomainKeyPair() { Value = "1", DisplayName = "Grupo Econômico - Comissão Base" };
					[FunctionalPoint("Value[1];DisplayName[Grupo Econômico - Comissão Base]")]
					public static DomainKeyPair ComissaoBaseGrupoEconomico { get { return _ComissaoBaseGrupoEconomico; } }
				    
					private static DomainKeyPair _ComissaoBaseLoja = new DomainKeyPair() { Value = "2", DisplayName = "Loja - Comissão Base" };
					[FunctionalPoint("Value[2];DisplayName[Loja - Comissão Base]")]
					public static DomainKeyPair ComissaoBaseLoja { get { return _ComissaoBaseLoja; } }
				    
					private static DomainKeyPair _ComissaoBaseFuncionario = new DomainKeyPair() { Value = "3", DisplayName = "Funcionário - Comissão Base" };
					[FunctionalPoint("Value[3];DisplayName[Funcionário - Comissão Base]")]
					public static DomainKeyPair ComissaoBaseFuncionario { get { return _ComissaoBaseFuncionario; } }
				    
					private static DomainKeyPair _ComissaoBaseOperacaoGrupoEconomico = new DomainKeyPair() { Value = "4", DisplayName = "Grupo Econômico - Comissão Base - Operação" };
					[FunctionalPoint("Value[4];DisplayName[Grupo Econômico - Comissão Base - Operação]")]
					public static DomainKeyPair ComissaoBaseOperacaoGrupoEconomico { get { return _ComissaoBaseOperacaoGrupoEconomico; } }
				    
					private static DomainKeyPair _ComissaoBaseOperacaoLoja = new DomainKeyPair() { Value = "5", DisplayName = "Loja - Comissão Base - Operação" };
					[FunctionalPoint("Value[5];DisplayName[Loja - Comissão Base - Operação]")]
					public static DomainKeyPair ComissaoBaseOperacaoLoja { get { return _ComissaoBaseOperacaoLoja; } }
				    
					private static DomainKeyPair _ComissaoBasePromocaoGrupoEconomico = new DomainKeyPair() { Value = "6", DisplayName = "Grupo Econômico - Comissão Base - Promoção" };
					[FunctionalPoint("Value[6];DisplayName[Grupo Econômico - Comissão Base - Promoção]")]
					public static DomainKeyPair ComissaoBasePromocaoGrupoEconomico { get { return _ComissaoBasePromocaoGrupoEconomico; } }
				    
					private static DomainKeyPair _ComissaoBasePromocaoLoja = new DomainKeyPair() { Value = "7", DisplayName = "Loja - Comissão Base - Promoção" };
					[FunctionalPoint("Value[7];DisplayName[Loja - Comissão Base - Promoção]")]
					public static DomainKeyPair ComissaoBasePromocaoLoja { get { return _ComissaoBasePromocaoLoja; } }
				    
					private static DomainKeyPair _PremioMetaLojaGrupoEconomico = new DomainKeyPair() { Value = "8", DisplayName = "Grupo Econômico - Prêmio - Meta Loja" };
					[FunctionalPoint("Value[8];DisplayName[Grupo Econômico - Prêmio - Meta Loja]")]
					public static DomainKeyPair PremioMetaLojaGrupoEconomico { get { return _PremioMetaLojaGrupoEconomico; } }
				    
					private static DomainKeyPair _PremioMetaLojaLoja = new DomainKeyPair() { Value = "9", DisplayName = "Loja - Prêmio - Meta Loja" };
					[FunctionalPoint("Value[9];DisplayName[Loja - Prêmio - Meta Loja]")]
					public static DomainKeyPair PremioMetaLojaLoja { get { return _PremioMetaLojaLoja; } }
				    
					private static DomainKeyPair _PremioMetaLojaFuncionario = new DomainKeyPair() { Value = "10", DisplayName = "Funcionário - Prêmio - Meta Loja" };
					[FunctionalPoint("Value[10];DisplayName[Funcionário - Prêmio - Meta Loja]")]
					public static DomainKeyPair PremioMetaLojaFuncionario { get { return _PremioMetaLojaFuncionario; } }
				    
					private static DomainKeyPair _PremioMetaVendedorGrupoEconomico = new DomainKeyPair() { Value = "11", DisplayName = "Grupo Econômico - Prêmio - Meta Vendedor" };
					[FunctionalPoint("Value[11];DisplayName[Grupo Econômico - Prêmio - Meta Vendedor]")]
					public static DomainKeyPair PremioMetaVendedorGrupoEconomico { get { return _PremioMetaVendedorGrupoEconomico; } }
				    
					private static DomainKeyPair _PremioMetaVendedorLoja = new DomainKeyPair() { Value = "12", DisplayName = "Loja - Prêmio - Meta Vendedor" };
					[FunctionalPoint("Value[12];DisplayName[Loja - Prêmio - Meta Vendedor]")]
					public static DomainKeyPair PremioMetaVendedorLoja { get { return _PremioMetaVendedorLoja; } }
				    
					private static DomainKeyPair _PremioMetaVendedorFuncionario = new DomainKeyPair() { Value = "13", DisplayName = "Funcionário - Prêmio - Meta Vendedor" };
					[FunctionalPoint("Value[13];DisplayName[Funcionário - Prêmio - Meta Vendedor]")]
					public static DomainKeyPair PremioMetaVendedorFuncionario { get { return _PremioMetaVendedorFuncionario; } }
				    
					private static DomainKeyPair _PremioTicketMedioGrupoEconomico = new DomainKeyPair() { Value = "14", DisplayName = "Grupo Econômico - Prêmio - Ticket Médio" };
					[FunctionalPoint("Value[14];DisplayName[Grupo Econômico - Prêmio - Ticket Médio]")]
					public static DomainKeyPair PremioTicketMedioGrupoEconomico { get { return _PremioTicketMedioGrupoEconomico; } }
				    
					private static DomainKeyPair _PremioTicketMedioLoja = new DomainKeyPair() { Value = "15", DisplayName = "Loja - Prêmio - Ticket Médio" };
					[FunctionalPoint("Value[15];DisplayName[Loja - Prêmio - Ticket Médio]")]
					public static DomainKeyPair PremioTicketMedioLoja { get { return _PremioTicketMedioLoja; } }
				    
					private static DomainKeyPair _PremioTicketMedioFuncionario = new DomainKeyPair() { Value = "16", DisplayName = "Funcionário - Prêmio - Ticket Médio" };
					[FunctionalPoint("Value[16];DisplayName[Funcionário - Prêmio - Ticket Médio]")]
					public static DomainKeyPair PremioTicketMedioFuncionario { get { return _PremioTicketMedioFuncionario; } }
				    
					private static DomainKeyPair _PremioVendaCartaoPresenteGrupoEconomico = new DomainKeyPair() { Value = "17", DisplayName = "Grupo Econômico - Prêmio - Venda Cartão Presente" };
					[FunctionalPoint("Value[17];DisplayName[Grupo Econômico - Prêmio - Venda Cartão Presente]")]
					public static DomainKeyPair PremioVendaCartaoPresenteGrupoEconomico { get { return _PremioVendaCartaoPresenteGrupoEconomico; } }
				    
					private static DomainKeyPair _PremioVendaCartaoPresenteLoja = new DomainKeyPair() { Value = "18", DisplayName = "Loja - Prêmio - Venda Cartão Presente" };
					[FunctionalPoint("Value[18];DisplayName[Loja - Prêmio - Venda Cartão Presente]")]
					public static DomainKeyPair PremioVendaCartaoPresenteLoja { get { return _PremioVendaCartaoPresenteLoja; } }
				    
					private static DomainKeyPair _PremioVendaCartaoPresenteFuncionario = new DomainKeyPair() { Value = "19", DisplayName = "Funcionário - Prêmio - Venda Cartão Presente" };
					[FunctionalPoint("Value[19];DisplayName[Funcionário - Prêmio - Venda Cartão Presente]")]
					public static DomainKeyPair PremioVendaCartaoPresenteFuncionario { get { return _PremioVendaCartaoPresenteFuncionario; } }
				    
					private static DomainKeyPair _PremioVendaProdutoGrupoEconomico = new DomainKeyPair() { Value = "20", DisplayName = "Grupo Econômico - Prêmio - Venda Produto" };
					[FunctionalPoint("Value[20];DisplayName[Grupo Econômico - Prêmio - Venda Produto]")]
					public static DomainKeyPair PremioVendaProdutoGrupoEconomico { get { return _PremioVendaProdutoGrupoEconomico; } }
				    
					private static DomainKeyPair _PremioVendaProdutoLoja = new DomainKeyPair() { Value = "21", DisplayName = "Loja - Prêmio - Venda Produto" };
					[FunctionalPoint("Value[21];DisplayName[Loja - Prêmio - Venda Produto]")]
					public static DomainKeyPair PremioVendaProdutoLoja { get { return _PremioVendaProdutoLoja; } }
				    
					private static DomainKeyPair _PremioMinimoQtdeCupomGrupoEconomico = new DomainKeyPair() { Value = "23", DisplayName = "Grupo Econômico - Prêmio - Quantidade de Cupons" };
					[FunctionalPoint("Value[23];DisplayName[Grupo Econômico - Prêmio - Quantidade de Cupons]")]
					public static DomainKeyPair PremioMinimoQtdeCupomGrupoEconomico { get { return _PremioMinimoQtdeCupomGrupoEconomico; } }
				    
					private static DomainKeyPair _PremioMinimoQtdeCupomLoja = new DomainKeyPair() { Value = "24", DisplayName = "Loja - Prêmio - Quantidade de Cupons" };
					[FunctionalPoint("Value[24];DisplayName[Loja - Prêmio - Quantidade de Cupons]")]
					public static DomainKeyPair PremioMinimoQtdeCupomLoja { get { return _PremioMinimoQtdeCupomLoja; } }
				    
					private static DomainKeyPair _PremioMinimoQtdeCupomFuncionario = new DomainKeyPair() { Value = "25", DisplayName = "Funcionário - Prêmio - Quantidade de Cupons" };
					[FunctionalPoint("Value[25];DisplayName[Funcionário - Prêmio - Quantidade de Cupons]")]
					public static DomainKeyPair PremioMinimoQtdeCupomFuncionario { get { return _PremioMinimoQtdeCupomFuncionario; } }
				    
					private static DomainKeyPair _PremioMinimoValorCupomGrupoEconomico = new DomainKeyPair() { Value = "26", DisplayName = "Grupo Econômico - Prêmio - Valor de Cupom" };
					[FunctionalPoint("Value[26];DisplayName[Grupo Econômico - Prêmio - Valor de Cupom]")]
					public static DomainKeyPair PremioMinimoValorCupomGrupoEconomico { get { return _PremioMinimoValorCupomGrupoEconomico; } }
				    
					private static DomainKeyPair _PremioMinimoValorCupomLoja = new DomainKeyPair() { Value = "27", DisplayName = "Loja - Prêmio - Valor de Cupom" };
					[FunctionalPoint("Value[27];DisplayName[Loja - Prêmio - Valor de Cupom]")]
					public static DomainKeyPair PremioMinimoValorCupomLoja { get { return _PremioMinimoValorCupomLoja; } }
				    
					private static DomainKeyPair _PremioMinimoValorCupomFuncionario = new DomainKeyPair() { Value = "28", DisplayName = "Funcionário - Prêmio - Valor de Cupom" };
					[FunctionalPoint("Value[28];DisplayName[Funcionário - Prêmio - Valor de Cupom]")]
					public static DomainKeyPair PremioMinimoValorCupomFuncionario { get { return _PremioMinimoValorCupomFuncionario; } }
				    
					private static DomainKeyPair _PremioTipoPagamentoGrupoEconomico = new DomainKeyPair() { Value = "29", DisplayName = "Grupo Econômico - Prêmio - Tipo Pagamento" };
					[FunctionalPoint("Value[29];DisplayName[Grupo Econômico - Prêmio - Tipo Pagamento]")]
					public static DomainKeyPair PremioTipoPagamentoGrupoEconomico { get { return _PremioTipoPagamentoGrupoEconomico; } }
				    
					private static DomainKeyPair _PremioTipoPagamentoLoja = new DomainKeyPair() { Value = "30", DisplayName = "Loja - Prêmio - Tipo Pagamento" };
					[FunctionalPoint("Value[30];DisplayName[Loja - Prêmio - Tipo Pagamento]")]
					public static DomainKeyPair PremioTipoPagamentoLoja { get { return _PremioTipoPagamentoLoja; } }
				    
					private static DomainKeyPair _PremioMetaVendedorSuperarGrupoEconomico = new DomainKeyPair() { Value = "31", DisplayName = "Grupo Econômico - Prêmio - Superação de Meta" };
					[FunctionalPoint("Value[31];DisplayName[Grupo Econômico - Prêmio - Superação de Meta]")]
					public static DomainKeyPair PremioMetaVendedorSuperarGrupoEconomico { get { return _PremioMetaVendedorSuperarGrupoEconomico; } }
				    
					private static DomainKeyPair _PremioMetaVendedorSuperarLoja = new DomainKeyPair() { Value = "32", DisplayName = "Loja - Prêmio - Superação de Meta" };
					[FunctionalPoint("Value[32];DisplayName[Loja - Prêmio - Superação de Meta]")]
					public static DomainKeyPair PremioMetaVendedorSuperarLoja { get { return _PremioMetaVendedorSuperarLoja; } }
				    
					private static DomainKeyPair _PremioMetaVendedorSuperarFuncionario = new DomainKeyPair() { Value = "33", DisplayName = "Funcionário - Prêmio - Superação de Meta" };
					[FunctionalPoint("Value[33];DisplayName[Funcionário - Prêmio - Superação de Meta]")]
					public static DomainKeyPair PremioMetaVendedorSuperarFuncionario { get { return _PremioMetaVendedorSuperarFuncionario; } }
				    
					private static DomainKeyPair _NaoElegivel = new DomainKeyPair() { Value = "99", DisplayName = "Não Elegível" };
					[FunctionalPoint("Value[99];DisplayName[Não Elegível]")]
					public static DomainKeyPair NaoElegivel { get { return _NaoElegivel; } }
				    
					private static DomainKeyPair _FonteExternaValores = new DomainKeyPair() { Value = "98", DisplayName = "Fonte Externa de Valores" };
					[FunctionalPoint("Value[98];DisplayName[Fonte Externa de Valores]")]
					public static DomainKeyPair FonteExternaValores { get { return _FonteExternaValores; } }
				    
					private static DomainKeyPair _AdicionalGovernoGrupoEconomico = new DomainKeyPair() { Value = "34", DisplayName = "Grupo Econômico - Adicional - Governo" };
					[FunctionalPoint("Value[34];DisplayName[Grupo Econômico - Adicional - Governo]")]
					public static DomainKeyPair AdicionalGovernoGrupoEconomico { get { return _AdicionalGovernoGrupoEconomico; } }
				    
					private static DomainKeyPair _AdicionalGovernoLoja = new DomainKeyPair() { Value = "35", DisplayName = "Loja - Adicional - Governo" };
					[FunctionalPoint("Value[35];DisplayName[Loja - Adicional - Governo]")]
					public static DomainKeyPair AdicionalGovernoLoja { get { return _AdicionalGovernoLoja; } }
				    
					private static DomainKeyPair _AdicionalGovernoFuncionario = new DomainKeyPair() { Value = "36", DisplayName = "Funcionário - Adicional - Governo" };
					[FunctionalPoint("Value[36];DisplayName[Funcionário - Adicional - Governo]")]
					public static DomainKeyPair AdicionalGovernoFuncionario { get { return _AdicionalGovernoFuncionario; } }
				    
					private static DomainKeyPair _PremioVendaProdutoFuncionario = new DomainKeyPair() { Value = "37", DisplayName = "Funcionário - Prêmio - Venda Produto" };
					[FunctionalPoint("Value[37];DisplayName[Funcionário - Prêmio - Venda Produto]")]
					public static DomainKeyPair PremioVendaProdutoFuncionario { get { return _PremioVendaProdutoFuncionario; } }
				    
					private static DomainKeyPair _PremioMetaVendedorSuperarAdicionalGrupoEconomico = new DomainKeyPair() { Value = "38", DisplayName = "Grupo Econômico - Prêmio - Adicional de Superação de Meta" };
					[FunctionalPoint("Value[38];DisplayName[Grupo Econômico - Prêmio - Adicional de Superação de Meta]")]
					public static DomainKeyPair PremioMetaVendedorSuperarAdicionalGrupoEconomico { get { return _PremioMetaVendedorSuperarAdicionalGrupoEconomico; } }
				    
					private static DomainKeyPair _PremioMetaVendedorSuperarAdicionalLoja = new DomainKeyPair() { Value = "39", DisplayName = "Loja - Prêmio - Adicional de Superação de Meta" };
					[FunctionalPoint("Value[39];DisplayName[Loja - Prêmio - Adicional de Superação de Meta]")]
					public static DomainKeyPair PremioMetaVendedorSuperarAdicionalLoja { get { return _PremioMetaVendedorSuperarAdicionalLoja; } }
				    
					private static DomainKeyPair _PremioMetaVendedorSuperarAdicionalFuncionario = new DomainKeyPair() { Value = "40", DisplayName = "Funcionário - Prêmio - Adicional de Superação de Meta" };
					[FunctionalPoint("Value[40];DisplayName[Funcionário - Prêmio - Adicional de Superação de Meta]")]
					public static DomainKeyPair PremioMetaVendedorSuperarAdicionalFuncionario { get { return _PremioMetaVendedorSuperarAdicionalFuncionario; } }
				    
					private static DomainKeyPair _ComissaoBaseUF = new DomainKeyPair() { Value = "41", DisplayName = "UF - Comissão Base" };
					[FunctionalPoint("Value[41];DisplayName[UF - Comissão Base]")]
					public static DomainKeyPair ComissaoBaseUF { get { return _ComissaoBaseUF; } }
				    
					private static DomainKeyPair _ComissaoBaseOperacaoUF = new DomainKeyPair() { Value = "42", DisplayName = "UF - Comissão Base - Operação" };
					[FunctionalPoint("Value[42];DisplayName[UF - Comissão Base - Operação]")]
					public static DomainKeyPair ComissaoBaseOperacaoUF { get { return _ComissaoBaseOperacaoUF; } }
				    
					private static DomainKeyPair _ComissaoBasePromocaoUF = new DomainKeyPair() { Value = "43", DisplayName = "UF - Comissão Base - Promoção" };
					[FunctionalPoint("Value[43];DisplayName[UF - Comissão Base - Promoção]")]
					public static DomainKeyPair ComissaoBasePromocaoUF { get { return _ComissaoBasePromocaoUF; } }
				    
					private static DomainKeyPair _PremioMetaLojaUF = new DomainKeyPair() { Value = "44", DisplayName = "UF - Prêmio - Meta Loja" };
					[FunctionalPoint("Value[44];DisplayName[UF - Prêmio - Meta Loja]")]
					public static DomainKeyPair PremioMetaLojaUF { get { return _PremioMetaLojaUF; } }
				    
					private static DomainKeyPair _PremioMetaVendedorUF = new DomainKeyPair() { Value = "45", DisplayName = "UF - Prêmio - Meta Vendedor" };
					[FunctionalPoint("Value[45];DisplayName[UF - Prêmio - Meta Vendedor]")]
					public static DomainKeyPair PremioMetaVendedorUF { get { return _PremioMetaVendedorUF; } }
				    
					private static DomainKeyPair _PremioTicketMedioUF = new DomainKeyPair() { Value = "46", DisplayName = "UF - Prêmio - Ticket Médio" };
					[FunctionalPoint("Value[46];DisplayName[UF - Prêmio - Ticket Médio]")]
					public static DomainKeyPair PremioTicketMedioUF { get { return _PremioTicketMedioUF; } }
				    
					private static DomainKeyPair _PremioVendaCartaoPresenteUF = new DomainKeyPair() { Value = "47", DisplayName = "UF - Prêmio - Venda Cartão Presente" };
					[FunctionalPoint("Value[47];DisplayName[UF - Prêmio - Venda Cartão Presente]")]
					public static DomainKeyPair PremioVendaCartaoPresenteUF { get { return _PremioVendaCartaoPresenteUF; } }
				    
					private static DomainKeyPair _PremioVendaProdutoUF = new DomainKeyPair() { Value = "48", DisplayName = "UF - Prêmio - Venda Produto" };
					[FunctionalPoint("Value[48];DisplayName[UF - Prêmio - Venda Produto]")]
					public static DomainKeyPair PremioVendaProdutoUF { get { return _PremioVendaProdutoUF; } }
				    
					private static DomainKeyPair _PremioMinimoQtdeCupomUF = new DomainKeyPair() { Value = "49", DisplayName = "UF - Prêmio - Quantidade de Cupons" };
					[FunctionalPoint("Value[49];DisplayName[UF - Prêmio - Quantidade de Cupons]")]
					public static DomainKeyPair PremioMinimoQtdeCupomUF { get { return _PremioMinimoQtdeCupomUF; } }
				    
					private static DomainKeyPair _PremioMinimoValorCupomUF = new DomainKeyPair() { Value = "50", DisplayName = "UF - Prêmio - Valor de Cupom" };
					[FunctionalPoint("Value[50];DisplayName[UF - Prêmio - Valor de Cupom]")]
					public static DomainKeyPair PremioMinimoValorCupomUF { get { return _PremioMinimoValorCupomUF; } }
				    
					private static DomainKeyPair _PremioTipoPagamentoUF = new DomainKeyPair() { Value = "51", DisplayName = "UF - Prêmio - Tipo Pagamento" };
					[FunctionalPoint("Value[51];DisplayName[UF - Prêmio - Tipo Pagamento]")]
					public static DomainKeyPair PremioTipoPagamentoUF { get { return _PremioTipoPagamentoUF; } }
				    
					private static DomainKeyPair _PremioMetaVendedorSuperarUF = new DomainKeyPair() { Value = "52", DisplayName = "UF - Prêmio - Superação de Meta" };
					[FunctionalPoint("Value[52];DisplayName[UF - Prêmio - Superação de Meta]")]
					public static DomainKeyPair PremioMetaVendedorSuperarUF { get { return _PremioMetaVendedorSuperarUF; } }
				    
					private static DomainKeyPair _AdicionalGovernoUF = new DomainKeyPair() { Value = "53", DisplayName = "UF - Adicional - Governo" };
					[FunctionalPoint("Value[53];DisplayName[UF - Adicional - Governo]")]
					public static DomainKeyPair AdicionalGovernoUF { get { return _AdicionalGovernoUF; } }
				    
					private static DomainKeyPair _PremioMetaVendedorSuperarAdicionalUF = new DomainKeyPair() { Value = "54", DisplayName = "UF - Prêmio - Adicional de Superação de Meta" };
					[FunctionalPoint("Value[54];DisplayName[UF - Prêmio - Adicional de Superação de Meta]")]
					public static DomainKeyPair PremioMetaVendedorSuperarAdicionalUF { get { return _PremioMetaVendedorSuperarAdicionalUF; } }
				    
					private static DomainKeyPair _PremioVendaFidelidadeGrupoEconomico = new DomainKeyPair() { Value = "55", DisplayName = "Grupo Econômico - Prêmio - Venda Fidelidade" };
					[FunctionalPoint("Value[55];DisplayName[Grupo Econômico - Prêmio - Venda Fidelidade]")]
					public static DomainKeyPair PremioVendaFidelidadeGrupoEconomico { get { return _PremioVendaFidelidadeGrupoEconomico; } }
				    
					private static DomainKeyPair _PremioVendaFidelidadeLoja = new DomainKeyPair() { Value = "56", DisplayName = "Loja - Prêmio - Venda Fidelidade" };
					[FunctionalPoint("Value[56];DisplayName[Loja - Prêmio - Venda Fidelidade]")]
					public static DomainKeyPair PremioVendaFidelidadeLoja { get { return _PremioVendaFidelidadeLoja; } }
				    
					private static DomainKeyPair _PremioVendaFidelidadeFuncionario = new DomainKeyPair() { Value = "57", DisplayName = "Funcionário - Prêmio - Venda Fidelidade" };
					[FunctionalPoint("Value[57];DisplayName[Funcionário - Prêmio - Venda Fidelidade]")]
					public static DomainKeyPair PremioVendaFidelidadeFuncionario { get { return _PremioVendaFidelidadeFuncionario; } }
				    
					private static DomainKeyPair _PremioVendaFidelidadeUF = new DomainKeyPair() { Value = "58", DisplayName = "UF - Prêmio - Venda Fidelidade" };
					[FunctionalPoint("Value[58];DisplayName[UF - Prêmio - Venda Fidelidade]")]
					public static DomainKeyPair PremioVendaFidelidadeUF { get { return _PremioVendaFidelidadeUF; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_FUNCAO_VENDEDOR
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Vendedor"); 
				    
					result.Add("2", "Gerente"); 
				    
					result.Add("3", "Caixa"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Vendedor"); 
				    
					result.Add("2", "Gerente"); 
				    
					result.Add("3", "Caixa"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Vendedor = new DomainKeyPair() { Value = "1", DisplayName = "Vendedor" };
					[FunctionalPoint("Value[1];DisplayName[Vendedor]")]
					public static DomainKeyPair Vendedor { get { return _Vendedor; } }
				    
					private static DomainKeyPair _Gerente = new DomainKeyPair() { Value = "2", DisplayName = "Gerente" };
					[FunctionalPoint("Value[2];DisplayName[Gerente]")]
					public static DomainKeyPair Gerente { get { return _Gerente; } }
				    
					private static DomainKeyPair _Caixa = new DomainKeyPair() { Value = "3", DisplayName = "Caixa" };
					[FunctionalPoint("Value[3];DisplayName[Caixa]")]
					public static DomainKeyPair Caixa { get { return _Caixa; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_TIPO_COMISSAO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Oficial"); 
				    
					result.Add("2", "Simulação"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Oficial"); 
				    
					result.Add("2", "Simulação"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Oficial = new DomainKeyPair() { Value = "1", DisplayName = "Oficial" };
					[FunctionalPoint("Value[1];DisplayName[Oficial]")]
					public static DomainKeyPair Oficial { get { return _Oficial; } }
				    
					private static DomainKeyPair _Simulação = new DomainKeyPair() { Value = "2", DisplayName = "Simulação" };
					[FunctionalPoint("Value[2];DisplayName[Simulação]")]
					public static DomainKeyPair Simulação { get { return _Simulação; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_TIPO_NF_RELACAO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("10", "Não Relacionada"); 
				    
					result.Add("20", "Devolução"); 
				    
					result.Add("30", "Retorno Físico"); 
				    
					result.Add("31", "Retorno Simbólico"); 
				    
					result.Add("91", "À Retornar"); 
				    
					result.Add("80", "Simples Faturamento"); 
				    
					result.Add("60", "À Ordem"); 
				    
					result.Add("70", "CTRC de NF"); 
				    
					result.Add("71", "Fatura Frete"); 
				    
					result.Add("81", "Entrega Futura"); 
				    
					result.Add("90", "NF de Cupom Fiscal"); 
				    
					result.Add("50", "Transferência"); 
				    
					result.Add("51", "Devolução de Transferência"); 
				    
					result.Add("82", "Recebimento Futuro"); 
				    
					result.Add("99", "Complementar"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("10", "NaoRelacionada"); 
				    
					result.Add("20", "Devolucao"); 
				    
					result.Add("30", "RetornoFisico"); 
				    
					result.Add("31", "RetornoSimbolico"); 
				    
					result.Add("91", "aRetornar"); 
				    
					result.Add("80", "SimplesFaturamento"); 
				    
					result.Add("60", "aOrdem"); 
				    
					result.Add("70", "CTRCdeNF"); 
				    
					result.Add("71", "FaturaFrete"); 
				    
					result.Add("81", "EntregaFutura"); 
				    
					result.Add("90", "NFdeCupomFiscal"); 
				    
					result.Add("50", "Transferencia"); 
				    
					result.Add("51", "DevolucaoTransferencia"); 
				    
					result.Add("82", "RecebimentoFuturo"); 
				    
					result.Add("99", "Complementar"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _NaoRelacionada = new DomainKeyPair() { Value = "10", DisplayName = "Não Relacionada" };
					[FunctionalPoint("Value[10];DisplayName[Não Relacionada]")]
					public static DomainKeyPair NaoRelacionada { get { return _NaoRelacionada; } }
				    
					private static DomainKeyPair _Devolucao = new DomainKeyPair() { Value = "20", DisplayName = "Devolução" };
					[FunctionalPoint("Value[20];DisplayName[Devolução]")]
					public static DomainKeyPair Devolucao { get { return _Devolucao; } }
				    
					private static DomainKeyPair _RetornoFisico = new DomainKeyPair() { Value = "30", DisplayName = "Retorno Físico" };
					[FunctionalPoint("Value[30];DisplayName[Retorno Físico]")]
					public static DomainKeyPair RetornoFisico { get { return _RetornoFisico; } }
				    
					private static DomainKeyPair _RetornoSimbolico = new DomainKeyPair() { Value = "31", DisplayName = "Retorno Simbólico" };
					[FunctionalPoint("Value[31];DisplayName[Retorno Simbólico]")]
					public static DomainKeyPair RetornoSimbolico { get { return _RetornoSimbolico; } }
				    
					private static DomainKeyPair _aRetornar = new DomainKeyPair() { Value = "91", DisplayName = "À Retornar" };
					[FunctionalPoint("Value[91];DisplayName[À Retornar]")]
					public static DomainKeyPair aRetornar { get { return _aRetornar; } }
				    
					private static DomainKeyPair _SimplesFaturamento = new DomainKeyPair() { Value = "80", DisplayName = "Simples Faturamento" };
					[FunctionalPoint("Value[80];DisplayName[Simples Faturamento]")]
					public static DomainKeyPair SimplesFaturamento { get { return _SimplesFaturamento; } }
				    
					private static DomainKeyPair _aOrdem = new DomainKeyPair() { Value = "60", DisplayName = "À Ordem" };
					[FunctionalPoint("Value[60];DisplayName[À Ordem]")]
					public static DomainKeyPair aOrdem { get { return _aOrdem; } }
				    
					private static DomainKeyPair _CTRCdeNF = new DomainKeyPair() { Value = "70", DisplayName = "CTRC de NF" };
					[FunctionalPoint("Value[70];DisplayName[CTRC de NF]")]
					public static DomainKeyPair CTRCdeNF { get { return _CTRCdeNF; } }
				    
					private static DomainKeyPair _FaturaFrete = new DomainKeyPair() { Value = "71", DisplayName = "Fatura Frete" };
					[FunctionalPoint("Value[71];DisplayName[Fatura Frete]")]
					public static DomainKeyPair FaturaFrete { get { return _FaturaFrete; } }
				    
					private static DomainKeyPair _EntregaFutura = new DomainKeyPair() { Value = "81", DisplayName = "Entrega Futura" };
					[FunctionalPoint("Value[81];DisplayName[Entrega Futura]")]
					public static DomainKeyPair EntregaFutura { get { return _EntregaFutura; } }
				    
					private static DomainKeyPair _NFdeCupomFiscal = new DomainKeyPair() { Value = "90", DisplayName = "NF de Cupom Fiscal" };
					[FunctionalPoint("Value[90];DisplayName[NF de Cupom Fiscal]")]
					public static DomainKeyPair NFdeCupomFiscal { get { return _NFdeCupomFiscal; } }
				    
					private static DomainKeyPair _Transferencia = new DomainKeyPair() { Value = "50", DisplayName = "Transferência" };
					[FunctionalPoint("Value[50];DisplayName[Transferência]")]
					public static DomainKeyPair Transferencia { get { return _Transferencia; } }
				    
					private static DomainKeyPair _DevolucaoTransferencia = new DomainKeyPair() { Value = "51", DisplayName = "Devolução de Transferência" };
					[FunctionalPoint("Value[51];DisplayName[Devolução de Transferência]")]
					public static DomainKeyPair DevolucaoTransferencia { get { return _DevolucaoTransferencia; } }
				    
					private static DomainKeyPair _RecebimentoFuturo = new DomainKeyPair() { Value = "82", DisplayName = "Recebimento Futuro" };
					[FunctionalPoint("Value[82];DisplayName[Recebimento Futuro]")]
					public static DomainKeyPair RecebimentoFuturo { get { return _RecebimentoFuturo; } }
				    
					private static DomainKeyPair _Complementar = new DomainKeyPair() { Value = "99", DisplayName = "Complementar" };
					[FunctionalPoint("Value[99];DisplayName[Complementar]")]
					public static DomainKeyPair Complementar { get { return _Complementar; } }
				    
			#endregion properties

		

	}    

}