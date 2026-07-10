#pragma once

#include "ATLComTime.h"
#include <iostream>
#include <exception>

using namespace std;

namespace LicenseAgent
{

	#pragma region API Comunication Classes

			enum RETORNO_TIPO
			{
				Ok = 1, Alerta = 2, Erro = 3
			};
			
			class LicencaInfo
			{
				public:
					long IdLicenca;
					string IdCliente;
					string Chave;
					string Usuario;
					string Terminal;
					string ToString();
					void SetData(string data);
			};

			class LicencaRetorno
			{
				public:
					LicencaRetorno();
					int IdLR;
					bool Valor;
					RETORNO_TIPO Tipo;
					string Descricao;
					string Mensagem;
					string ToString();
					void SetData(string data);
			};

			class LogInfo
			{
				public:
					int IdProduto;
					string IdCliente;
					string Chave;
					string Usuario;
					string Terminal;
					COleDateTime Data; 
					string Detalhes;
					long IdUsuario;
					string NomeAutenticacao;
					int IdLinx;
					string CodigoFilial;
					string NomeFilial;
					bool IndicaLoja;
					string ToString();
					void SetData(string);


			};
			
	#pragma endregion

	#pragma region Offline Store Classes
			enum STATUS_CHAVE 
			{
				ATIVO = 1, PENDENTE = 2, REVOGADO = 3
			};

			class LicencaUso 
			{
				public:
					long IdLU;
					long IdLR; //FK
					string IdLicencaUso;
					STATUS_CHAVE LxStatusChave;
					string Periodicidade;
					string DiasOffline;
					string Mensagem;
					string TemporaryIdLicencaUso;
					COleDateTime Data; 
					COleDateTime DataProcesso;
					void EncryptData();
					void DecryptData();
					string ToString();
					void SetData(string);
			};

			class LicencaRequisicao 
			{
				public:
					long IdLR;
					string IdLicenca;
					string IdCliente;
					string Chave;
					string Usuario;
					string Terminal;
					string ToString();

			};
		
			
	#pragma endregion

	#pragma region Licensing Class

			class Licensing
			{
				friend class LicencaRequisicao;
				friend class LicencaUso;
				friend class LicencaRetorno;
				friend class LicencaInfo;
				friend class LogInfo;
				friend class LicencaUso;				
				private:
					string machineKey;
					/// <summary>
					/// HDD Serial Number.
					/// </summary>
					string GetHardwareSerialNumber();					
					/// <summary>
					/// Http post.
					/// </summary>
					string HttpPost(const wstring, string);
					/// <summary>
					/// Value part of a token.
					/// </summary>
					static string GetValuePart(string, string);					
					/// <summary>
					/// Verificar se existe uma licença para o produto.
					/// </summary>
					/// <param name="info"></param>
					/// <returns></returns>
					LicencaRetorno ValidarLicenca(LicencaInfo);
					/// <summary>
					/// Remover licença.
					/// </summary>
					/// <param name="info"></param>
					/// <returns></returns>
					LicencaRetorno RemoverLicenca(LicencaInfo);
					/// <summary>
					/// Saqlvar Log de produto licenciado.
					/// </summary>
					/// <param name="logContent"></param>
					/// <returns></returns>
					LicencaRetorno SalvarLicencaLog(LogInfo);
					/// <summary>
					/// Adjust message result.
					/// </summary>
					/// <param name="result"></param>
					/// <param name="licencaUso"></param>
					/// <param name="exp"></param>
					void SetMessageResult(LicencaRetorno &, LicencaUso, exception);					
					/// <summary>
					/// Dias antes de expirar.
					/// </summary>
					/// <param name="licencaUso"></param>
					/// <returns></returns>
					int DiasAExpirar(LicencaUso);
					/// <summary>
					/// Licença está expirada?
					/// </summary>
					/// <param name="licencaUso"></param>
					/// <returns></returns>
					bool LicencaExpirada(LicencaUso);															
					/// <summary>
					/// Test token.
					/// </summary>
					bool IsValidToken(string);
					/// <summary>
					/// Encrypt Data.
					/// </summary>
					/// <param name="data"></param>
					/// <returns></returns>
					static string Encrypt(string);	
					/// <summary>
					/// Undefined Data Function.
					/// </summary>
					static string Undefined(string);
					/// <summary>
					/// Undefined Data Function.
					/// </summary>
					static string Undefined2(string);
					/// <summary>
					/// Decrypt Data.
					/// </summary>
					/// <param name="data"></param>
					/// <returns></returns>
					static string Decrypt(string);	
					/// <summary>
					/// Get machine key.
					/// </summary>
					/// <returns></returns>
					string GetKey();			
					/// <summary>
					/// Convert wstring TO string.
					/// </summary>
					/// <returns></returns>
					static string Convert(wstring);
					/// <summary>
					/// Get a property value.
					/// </summary>
					static string GetPropertyValue(string, string);
				public:	
					/// <summary>
					/// Save Log.
					/// </summary>
					void SaveLog(string);
					/// <summary>
					/// String Trim.
					/// </summary>
					string Trim(const string&);
					/// <summary>
					/// Extract Json value.
					/// </summary>
					string GetJsonPropertyValue(string, string);
					/// <summary>
					/// Convert wstring to string.
					/// </summary>
					string ConvertWsToS(wstring);					
					/// <summary>
					/// Get current computer name.
					/// </summary>
					static string GetCurrentHostName();					
					/// <summary>
					/// Validar uma licença.
					/// Body:
					/// {
					///     "IdLicenca" : 4,
					///     "IdCliente": "65161419000170",
					///     "Usuario" : "usuarioTeste1"
					/// }     
					/// </summary>
					/// <param name="info"></param>
					/// <returns></returns>      
					string Validar(string);
					/// <summary>
					/// Enviar log para o servidor remoto de licenças.
					/// Body:
					/// {
					///    "IdProduto" : 3,
					///    "IdCliente": "65161419000170",
					///    "Usuario" : "usuarioTeste1",
					///    "IdUsuario" : null,
					///    "NomeAutenticacao" : null,    
					///    "CodigoFilial" : "",
					///    "NomeFilial" : "",
					///    "IndicaLoja" : false,
					///    "IdLinx" : null,
					///     "Detalhes" : [ "Teste1", "Teste2" ]
					/// }     
					/// </summary>
					/// <param name="logContent"></param>
					/// <returns></returns>        
					string SalvarLog(string);
					/// <summary>
					/// Remover uma licença.
					/// Body:
					/// {
					///     "IdLicenca" : 4,
					///     "IdCliente": "65161419000170",
					///     "Usuario" : "usuarioTeste1"
					/// }     
					/// </summary>
					/// <param name="info"></param>
					/// <returns></returns>       
					string Remover(string);
					/// <summary>
					/// This method should be passed to the business area, for generating the token key.
					/// </summary>
					/// <returns></returns>
					string GenerateToken(string);
					/// <summary>
					/// Encript Data;
					/// </summary>
					/// <param name="data"></param>
					/// <param name="token"></param>
					/// <returns></returns>
					string Encrypt(string, string);
					/// <summary>
					/// Decrypt Data.
					/// </summary>
					/// <param name="data"></param>
					/// <param name="token"></param>
					/// <returns></returns>
					string Decrypt(string, string);
								

			};
	#pragma endregion


}