#define _WIN32_DCOM
#include <comdef.h>
#include <iostream>
#include <exception>
#include "ATLComTime.h"
#include "Linx.License.Agent.h"
#include "Database.h"
#include "sqlite3.h"
#include "WinHttpClient.h"
#include <clocale>
#include <windows.h>
#include "base64.h"
#include <wbemidl.h>

#pragma comment(lib, "wbemuuid.lib")
#pragma comment(lib, "sqlite3.lib")

#define _FUNC_KEY  "LINX174557892000"
#define REMOTE_SERVER_VALIDATE L"https://svc-licensing.linxsaas.com.br/LinxLicenseServerLicenciamento/ValidateLicense"
#define REMOTE_SERVER_REMOVE L"https://svc-licensing.linxsaas.com.br/LinxLicenseServerLicenciamento/RemoveLicense"
#define REMOTE_SERVER_LOG L"https://svc-licensing.linxsaas.com.br/LinxLicenseServerLicenciamento/LogUpdate"


using namespace std;
using namespace LocalStorage;

const string uniqueMachineKey = "";//new Identity().Value();
string licenseServer = "";

namespace LicenseAgent
{	

	#pragma region LogInfo implementations
	string LogInfo::ToString()
	{
		std::stringstream strm;
		strm << "{\"IdProduto\":\"" << this->IdProduto << "\",\"IdCliente\":\"" << this->IdCliente << "\",\"Chave\":\"" << this->Chave << "\",\"Usuario\":\"" << this->Usuario << "\",\"Terminal\":\"" << this->Terminal << "\",\"Data\":\"" << LicenseDB::ConvertJson(this->Data) << "\",\"Detalhes\":" << this->Detalhes << ",\"IdUsuario\":" << this->IdUsuario << ",\"NomeAutenticacao\":\"" << this->NomeAutenticacao << "\",\"IdLinx\":" << this->IdLinx << ",\"CodigoFilial\":\"" << this->CodigoFilial << "\",\"NomeFilial\":\"" << this->NomeFilial << "\",\"IndicaLoja\":" << (this->IndicaLoja ? "true" : "false") << "}";

		return strm.str();
	}
	void LogInfo::SetData(string data)
	{	
		string key = Licensing::GetPropertyValue(data, "IdProduto");
		if (key.length() > 0)
		{
			this->IdProduto = std::atoi(key.c_str());
			this->IdCliente = Licensing::GetPropertyValue(data, "IdCliente");
			this->Chave = Licensing::GetPropertyValue(data, "Chave");
			this->Usuario = Licensing::GetPropertyValue(data, "Usuario");
			this->Terminal = Licensing::GetPropertyValue(data, "Terminal");
			this->Detalhes = Licensing::GetPropertyValue(data, "Detalhes");
			this->IdUsuario = std::atol(Licensing::GetPropertyValue(data, "IdUsuario").c_str());
			string idLinx = Licensing::GetPropertyValue(data, "IdLinx");
			if (idLinx.length() > 0)
			{
				this->IdLinx = std::atoi(idLinx.c_str());
			}
			this->NomeAutenticacao = Licensing::GetPropertyValue(data, "NomeAutenticacao");
			this->CodigoFilial = Licensing::GetPropertyValue(data, "CodigoFilial");
			this->NomeFilial = Licensing::GetPropertyValue(data, "NomeFilial");
			this->IndicaLoja = (Licensing::GetPropertyValue(data, "IndicaLoja") == "true");
		}

	}
	#pragma endregion

	#pragma region LicencaInfo implementations
	string LicencaInfo::ToString()
	{
		std::stringstream strm;
		strm << "{\"IdLicenca\":\"" << this->IdLicenca << "\",\"IdCliente\":\"" << this->IdCliente << "\",\"Chave\":\"" << this->Chave << "\",\"Usuario\":\"" << this->Usuario << "\",\"Terminal\":\"" << this->Terminal << "\"}";

		return strm.str();
	}
	void LicencaInfo::SetData(string data)
	{	
		string key = Licensing::GetPropertyValue(data, "IdLicenca");
		if (key.length() > 0)
		{
			this->IdLicenca = std::atol(key.c_str());
			this->IdCliente = Licensing::GetPropertyValue(data, "IdCliente");
			this->Chave = Licensing::GetPropertyValue(data, "Chave");
			this->Usuario = Licensing::GetPropertyValue(data, "Usuario");
			this->Terminal = Licensing::GetPropertyValue(data, "Terminal");
		}

	}
	#pragma endregion

	#pragma region LicencaRetorno implementations

	LicencaRetorno::LicencaRetorno()
	{
		this->IdLR = 1;
	}

	string LicencaRetorno::ToString()
	{
		std::stringstream strm;
		strm << "{\"IdLR\":" << this->IdLR << ",\"Valor\":" << this->Valor << ",\"Tipo\":" << this->Tipo << ",\"Descricao\":\"" << this->Descricao << "\",\"Mensagem\":\"" << this->Mensagem << "\"}";

		return strm.str();
	}
	void LicencaRetorno::SetData(string data)
	{		
		this->IdLR = 0;
		this->Valor = (Licensing::GetPropertyValue(data, "Valor") == "true");
		this->Tipo = (RETORNO_TIPO)std::atoi(Licensing::GetPropertyValue(data, "Tipo").c_str());
		this->Descricao = Licensing::GetPropertyValue(data, "Descricao");
		this->Mensagem = Licensing::GetPropertyValue(data, "Mensagem");		
	}
	#pragma endregion


	#pragma region LicencaUso implementations
	void LicencaUso::EncryptData()
	{
			this->IdLicencaUso = Licensing::Encrypt(this->IdLicencaUso);
            this->Periodicidade = Licensing::Encrypt(this->Periodicidade);
            this->DiasOffline = Licensing::Encrypt(this->DiasOffline);
            this->Mensagem = Licensing::Encrypt(this->Mensagem);
            this->TemporaryIdLicencaUso = Licensing::Encrypt(this->TemporaryIdLicencaUso);
	};

	void LicencaUso::DecryptData() 
	{
			this->IdLicencaUso = Licensing::Decrypt(this->IdLicencaUso);
            this->Periodicidade = Licensing::Decrypt(this->Periodicidade);
            this->DiasOffline = Licensing::Decrypt(this->DiasOffline);
            this->Mensagem = Licensing::Decrypt(this->Mensagem);
            this->TemporaryIdLicencaUso = Licensing::Decrypt(this->TemporaryIdLicencaUso);
	};

	string LicencaUso::ToString()
	{
		std::stringstream strm;
		strm << "{\"IdLU\":" << this->IdLU << ",\"IdLR\":" << this->IdLR << ",\"IdLicencaUso\":\"" << this->IdLicencaUso << "\",\"LxStatusChave\":" << this->LxStatusChave << ",\"Periodicidade\":\"" << this->Periodicidade << "\",\"DiasOffline\":\"" << this->DiasOffline << "\",\"Mensagem\":\"" << this->Mensagem << "\",\"TemporaryIdLicencaUso\":\"" << this->TemporaryIdLicencaUso << "\",\"Data\":\"" << LicenseDB::Convert(this->Data) << "\",\"DataProcesso\":\"" << LicenseDB::Convert(this->DataProcesso) << "\"}";

		return strm.str();
	}

	void LicencaUso::SetData(string data)
	{	
		string key = Licensing::GetPropertyValue(data, "IdLicencaUso");
		if (key.length() > 0)
		{
			this->IdLicencaUso = key;
			this->LxStatusChave = (STATUS_CHAVE)atoi(Licensing::GetPropertyValue(data, "LxStatusChave").c_str());
			this->Periodicidade = Licensing::GetPropertyValue(data, "Periodicidade");
			this->DiasOffline = Licensing::GetPropertyValue(data, "DiasOffline");
			this->Mensagem = Licensing::GetPropertyValue(data, "Mensagem");
			this->TemporaryIdLicencaUso = Licensing::GetPropertyValue(data, "TemporaryIdLicencaUso");
		}

	}
	#pragma endregion

	#pragma region LicencaRequisicao implementations
	string LicencaRequisicao::ToString()
	{
		std::stringstream strm;
		strm << "{\"IdLR\":" << this->IdLR << ",\"IdLicenca\":\"" << this->IdLicenca << "\",\"IdCliente\":\"" << this->IdCliente << "\",\"Chave\":\"" << this->Chave << "\",\"Usuario\":\"" << this->Usuario << "\",\"Terminal\":\"" << this->Terminal << "\"}";

		return strm.str();
	}
	#pragma endregion

	#pragma region Licensing implementations

	string Licensing::GetValuePart(string token, string innerDelimiter)
	{
		size_t innerPos = token.find(innerDelimiter);
		if (innerPos != string::npos)
		{			
			token.erase(0, innerPos + innerDelimiter.length());
			if (token[0] == '"')
				token = token.substr(1);
			if (token[token.length() - 1] == '"')
				token = token.substr(0, token.length() - 1);
			return token;
		}

		return "";
	}

	string Licensing::GetJsonPropertyValue(string s, string propertyName)
	{
		return Licensing::GetPropertyValue(s, propertyName);
	}

	string Licensing::ConvertWsToS(wstring ws)
	{
		return Licensing::Convert(ws);
	}

	string Licensing::GetPropertyValue(string s, string propertyName)
	{
		string delimiter = ",", innerDelimiter = propertyName + "\":";

		size_t pos = 0;
		string token;

		//Inner Data List
		if ((pos = s.find(innerDelimiter + "[")) != string::npos) {
			s.erase(0, pos + (innerDelimiter).length());
			pos = s.find("]");
			token = s.substr(0, pos+1);
			return token;
		}


		while ((pos = s.find(delimiter)) != string::npos) {
			token = s.substr(0, pos);

			token = GetValuePart(token, innerDelimiter);
			if (token != "")
				return token;

			s.erase(0, pos + delimiter.length());
		}

		token = GetValuePart(s.substr(0, s.length()-1), innerDelimiter);
		if (token != "")
			return token;

		return "";
	}

	string Licensing::Convert(wstring ws) 
	{
	  std::setlocale(LC_ALL, "");
	  const std::locale locale("");
	  typedef std::codecvt<wchar_t, char, std::mbstate_t> converter_type;
	  const converter_type& converter = std::use_facet<converter_type>(locale);
	  std::vector<char> to(ws.length() * converter.max_length());
	  std::mbstate_t state;
	  const wchar_t* from_next;
	  char* to_next;
	  const converter_type::result result = converter.out(state, ws.data(), ws.data() + ws.length(), from_next, &to[0], &to[0] + to.size(), to_next);
	  
	  std::string s(&to[0], to_next);
	  return s;
	  
	}

	string Licensing::HttpPost(const wstring endpoint, string data)
	{
		//REMOTE_SERVER
		WinHttpClient client(endpoint);
		// Set post data.		
		client.SetAdditionalDataToSend((BYTE *)data.c_str(), data.size());

		// Set request headers.
		wchar_t szSize[50] = L"";
		swprintf_s(szSize, L"%d", data.size());
		wstring headers = L"Content-Length: ";
		headers += szSize;
		headers += L"\r\nContent-Type: application/json\r\n";
		client.SetAdditionalRequestHeaders(headers);

		// Send http post request.

		client.SendHttpRequest(L"POST");

		wstring httpResponseHeader = client.GetResponseHeader();
		wstring httpResponseContent = client.GetResponseContent();
		
		if (httpResponseContent.length() == 0)
		{
			return "{\"ExceptionMessage\":\"Sem conexão com o servidor.\"}";
		}
		
		return Convert(httpResponseContent);
	}

	string Licensing::GetCurrentHostName()
	{
		TCHAR computerName[MAX_COMPUTERNAME_LENGTH + 1];
		DWORD size = sizeof(computerName) / sizeof(computerName[0]);
		GetComputerName(computerName, &size);
		CString strCN(computerName);
		CT2CA pszConvertedAnsiString (strCN);
		string str(pszConvertedAnsiString);

		return str;
	}

	void Licensing::SaveLog(string msg)
	{
		std::ofstream outfile;
	    outfile.open("C:\\Temp\\LinxLicenseAgentDebug.log", std::ios_base::app);

		CString cstr = COleDateTime::GetCurrentTime().Format(_T("%Y-%m-%d %H:%M:%S"));
		CT2CA pszConvertedAnsiString (cstr);
		string str(pszConvertedAnsiString);

	    outfile << str << " - " << msg << endl; 
	} 

	string Licensing::Trim(const string& str)
	{
		size_t first = str.find_first_not_of(' ');
		if (string::npos == first)
		{
			return str;
		}
		size_t last = str.find_last_not_of(' ');
		return str.substr(first, (last - first + 1));
	}

	string Licensing::GetHardwareSerialNumber()
	{
		HRESULT hres;

		//SaveLog("GetHardwareSerialNumber - Start");

		// Step 1: --------------------------------------------------
		// Initialize COM. ------------------------------------------

		//SaveLog("Initializing COM library");
		hres =  CoInitializeEx(0, COINIT_APARTMENTTHREADED); //COINIT_MULTITHREADED or COINIT_APARTMENTTHREADED
		if (FAILED(hres))
		{
			//SaveLog("Failed to initialize COM library");
			return "1";                  // Program has failed.
		}
	    
		// Step 2: ---------------------------------------------------
		// Obtain the initial locator to WMI -------------------------

		IWbemLocator *pLoc = NULL;

		//SaveLog("Obtain the initial locator to WMI");
		hres = CoCreateInstance(
			CLSID_WbemLocator,             
			0, 
			CLSCTX_INPROC_SERVER, 
			IID_IWbemLocator, (LPVOID *) &pLoc);
	 
		if (FAILED(hres))
		{
			//SaveLog("Failed to create IWbemLocator object.");
			CoUninitialize();
			return "1";                 // Program has failed.
		}

		// Step 3: -----------------------------------------------------
		// Connect to WMI through the IWbemLocator::ConnectServer method

		//SaveLog("Connect to WMI through the IWbemLocator::ConnectServer method");
		IWbemServices *pSvc = NULL;
	 
		// Connect to the root\cimv2 namespace with
		// the current user and obtain pointer pSvc
		// to make IWbemServices calls.
		hres = pLoc->ConnectServer(
			 _bstr_t(L"ROOT\\CIMV2"), // Object path of WMI namespace
			 NULL,                    // User name. NULL = current user
			 NULL,                    // User password. NULL = current
			 0,                       // Locale. NULL indicates current
			 NULL,                    // Security flags.
			 0,                       // Authority (for example, Kerberos)
			 0,                       // Context object 
			 &pSvc                    // pointer to IWbemServices proxy
			 );
	    
		if (FAILED(hres))
		{
			//SaveLog("Could not connect.");
			pLoc->Release();     
			CoUninitialize();
			return "1";                // Program has failed.
		}

		// Step 4: --------------------------------------------------
		// Set security levels on the proxy -------------------------

		//SaveLog("Set security levels on the proxy");
		hres = CoSetProxyBlanket(
		   pSvc,                        // Indicates the proxy to set
		   RPC_C_AUTHN_WINNT,           // RPC_C_AUTHN_xxx
		   RPC_C_AUTHZ_NONE,            // RPC_C_AUTHZ_xxx
		   NULL,                        // Server principal name 
		   RPC_C_AUTHN_LEVEL_CALL,      // RPC_C_AUTHN_LEVEL_xxx 
		   RPC_C_IMP_LEVEL_IMPERSONATE, // RPC_C_IMP_LEVEL_xxx
		   NULL,                        // client identity
		   EOAC_NONE                    // proxy capabilities 
		);

		if (FAILED(hres))
		{
			//SaveLog("Could not set proxy blanket.");
			pSvc->Release();
			pLoc->Release();     
			CoUninitialize();
			return "1";               // Program has failed.
		}

		// Step 5: --------------------------------------------------
		// Use the IWbemServices pointer to make requests of WMI ----

		//SaveLog("Use the IWbemServices pointer to make requests of WMI");

		string serialResult = "";
		for (int i = 0; i < 2; i++)
		{			
			CStringW strQuery;
			if (i == 0)
			{
				CStringW strDrivePath;
				strDrivePath.Format(_T("\\\\\\\\.\\\\PhysicalDrive%u"), 0);

				strQuery.Format(L"SELECT SerialNumber FROM Win32_PhysicalMedia WHERE Tag=\"%s\"", 
					strDrivePath);
			}
			else 
			{
				strQuery.Format(L"SELECT SerialNumber FROM Win32_BaseBoard");
			}


			// For example, get the name of the operating system

			//SaveLog("Executing Query:");
			CT2CA queryConvertedAnsiString (strQuery);
			std::string hdwQuery(queryConvertedAnsiString);
			//SaveLog(hdwQuery);

			IEnumWbemClassObject* pEnumerator = NULL;
			hres = pSvc->ExecQuery(
				bstr_t("WQL"), 
				bstr_t(strQuery),
				WBEM_FLAG_FORWARD_ONLY | WBEM_FLAG_RETURN_IMMEDIATELY, 
				NULL,
				&pEnumerator);
		    
			if (FAILED(hres))
			{
				//SaveLog("Query for operating system name failed:");
				pSvc->Release();
				pLoc->Release();
				CoUninitialize();
				return "1";               // Program has failed.
			}

			// Step 6: -------------------------------------------------
			// Get the data from the query in step 5 -------------------
		 
			IWbemClassObject *pclsObj = NULL;
			ULONG uReturn = 0;
		   
			while (pEnumerator)
			{
				HRESULT hr = pEnumerator->Next(WBEM_INFINITE, 1, 
					&pclsObj, &uReturn);

				if(0 == uReturn)
				{
					break;
				}

				VARIANT vtProp;

				// Get the value of the Name property
				hr = pclsObj->Get(L"SerialNumber", 0, &vtProp, 0, 0);

				CString strSerialNumber = vtProp.bstrVal; // assign serial number to output parameter

				CT2CA hddConvertedAnsiString (strSerialNumber);
				std::string hdwSerialNumber(hddConvertedAnsiString);
				if (serialResult.length() > 0)
					serialResult += "|";
				serialResult += Trim(hdwSerialNumber);			

				VariantClear(&vtProp);

				pclsObj->Release();
			}

			pEnumerator->Release();
		}

		// Cleanup
		// ========    
		pSvc->Release();
		pLoc->Release();    
		CoUninitialize();

		//SaveLog("GetHardwareSerialNumber - End");
		//SaveLog(serialResult);

		return serialResult;
	}


	string Licensing::GetKey()
	{
		if (machineKey.length() > 0)
			return machineKey;
	    
	    machineKey = GetHardwareSerialNumber();

		return machineKey;
	}

	LicencaRetorno Licensing::ValidarLicenca(LicencaInfo info)
	{
		LicencaRetorno result;	
		exception error("0");
		LicenseDB db;
		COleDateTime currentDate = COleDateTime::GetCurrentTime();
		currentDate.SetDate(currentDate.GetYear(), currentDate.GetMonth(), currentDate.GetDay());

		db.ConnectDB();
	
		info.Terminal = Licensing::GetCurrentHostName();
        info.Chave = GetKey();
		

		//Verificar/Ajustar o controle de requisição a licença local
		string idLicenca;
		stringstream strstream;
		strstream << info.IdLicenca;
		idLicenca = strstream.str();
        idLicenca = Licensing::Encrypt(idLicenca);
        string idChave = Licensing::Encrypt(info.Chave);
        string idCliente = Licensing::Encrypt(info.IdCliente);
        string usuario = Licensing::Encrypt(info.Usuario);
        string terminal = Licensing::Encrypt(info.Terminal);

		LicencaRequisicao licencaReq = db.Query(idCliente, idLicenca, idChave);
		if (licencaReq.IdLR <= 0) //Does not exists
		{
			licencaReq.IdCliente = idCliente;
            licencaReq.IdLicenca = idLicenca;
            licencaReq.Usuario = usuario;
            licencaReq.Chave = idChave;
            licencaReq.Terminal = terminal;

			db.Add(&licencaReq);
		}
		else 
		{
			if (licencaReq.Terminal != terminal || licencaReq.Usuario != usuario)
            {
				licencaReq.Terminal = terminal;
                licencaReq.Usuario = usuario;
				db.Update(licencaReq);
			}
		}

		//Obter a licença local
		LicencaUso licencaUso = db.Query(licencaReq);
		if (licencaUso.IdLU > 0)
		{
			licencaUso.DecryptData();
			if (!LicencaExpirada(licencaUso) && licencaUso.Data.GetYear() == currentDate.GetYear() && licencaUso.Data.GetMonth() == currentDate.GetMonth() && licencaUso.Data.GetDay() == currentDate.GetDay())
            {
                SetMessageResult(result, licencaUso, error);
                return result;
            }
		}

		try
		{

			string content = HttpPost(REMOTE_SERVER_VALIDATE, info.ToString());

			//Verify http exception
			string exceptionMessage = this->GetJsonPropertyValue(content, "ExceptionMessage");
			if (exceptionMessage.length() > 0)
			{
				throw exception(exceptionMessage.c_str());
			}

			LicencaUso lUso;
			lUso.SetData(content);
						
			if (licencaUso.IdLU <= 0)
			{
				licencaUso = lUso;
			}
			else 
			{
				licencaUso.IdLicencaUso = lUso.IdLicencaUso;
                licencaUso.LxStatusChave = lUso.LxStatusChave;
                licencaUso.Periodicidade = lUso.Periodicidade;
                licencaUso.DiasOffline = lUso.DiasOffline;
                licencaUso.Mensagem = lUso.Mensagem;
                licencaUso.TemporaryIdLicencaUso = lUso.TemporaryIdLicencaUso;
			}

			licencaUso.IdLR = licencaReq.IdLR;			
			licencaUso.Data = currentDate;
			licencaUso.DataProcesso = currentDate;
		
		}
		catch(const std::runtime_error & re)
		{
			error = exception(re.what());
		}
		catch (exception & e)
		{
			error = e;
		}

		//Adjust DataProcesso
		if (licencaUso.IdLU > 0 && error.what() != "0")
        {   
            licencaUso.DataProcesso = currentDate;
        }
				
		SetMessageResult(result, licencaUso, error);

		//Salvar a licença localmente
        if (result.Valor)
        {
            licencaUso.EncryptData();
            if (licencaUso.IdLU <= 0)
                db.Add(&licencaUso);
            else
				db.Update(licencaUso);
        }

		db.DisonnectDB();
		return result;
	}

	string Licensing::Validar(string licenseInfo)
	{
		LicencaRetorno lr;
		
		try
		{
			string decInfo = Decrypt(licenseInfo);
			LicencaInfo li;
			li.SetData(decInfo);
			lr = ValidarLicenca(li);
		}
		catch(const std::runtime_error & re)
		{
			lr.Tipo = RETORNO_TIPO::Erro;
			lr.Descricao = "Erro";
			lr.Mensagem = re.what();
			lr.Valor = false;
		}
		catch (exception & e)
		{
			lr.Tipo = RETORNO_TIPO::Erro;
			lr.Descricao = "Erro";
			lr.Mensagem = e.what();
			lr.Valor = false;
		}
		

		return Encrypt(lr.ToString());
	}

	/// <summary>
	/// Remover licença.
	/// </summary>
	/// <param name="info"></param>
	/// <returns></returns>
	LicencaRetorno Licensing::RemoverLicenca(LicencaInfo info)
	{
		LicencaRetorno result;

		//Ajustar propriedades com informações locais
        info.Terminal = Licensing::GetCurrentHostName();
        info.Chave = GetKey();
		
        exception error("0");
        try
        {
			string content = HttpPost(REMOTE_SERVER_REMOVE, info.ToString());
			//Verify http exception
			string exceptionMessage = this->GetJsonPropertyValue(content, "ExceptionMessage");
			if (exceptionMessage.length() > 0)
			{
				throw exception(exceptionMessage.c_str());
			}
			result.Valor = true;
		}
		catch(const std::runtime_error & re)
		{
			error = exception(re.what());
		}	
		catch (exception &exp)
        {
            error = exp;
        }
		
		LicencaUso lu;
		SetMessageResult(result, lu, error);

		return result;
	}

	/// <summary>
	/// Saqlvar Log de produto licenciado.
	/// </summary>
	/// <param name="logContent"></param>
	/// <returns></returns>
	LicencaRetorno Licensing::SalvarLicencaLog(LogInfo info)
	{
		LicencaRetorno result;

		//Ajustar propriedades com informações locais
        info.Terminal = Licensing::GetCurrentHostName();
        info.Chave = GetKey();
		info.Data = COleDateTime::GetCurrentTime();
		
        exception error("0");
        try
        {
			string content = HttpPost(REMOTE_SERVER_LOG, info.ToString());
			//Verify http exception
			string exceptionMessage = this->GetJsonPropertyValue(content, "ExceptionMessage");
			if (exceptionMessage.length() > 0)
			{
				throw exception(exceptionMessage.c_str());
			}
			result.Valor = true;
		}
		catch(const std::runtime_error & re)
		{
			error = exception(re.what());
		}	
		catch (exception &exp)
        {
            error = exp;
        }
		
		LicencaUso lu;
		SetMessageResult(result, lu, error);

		return result;
	}

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
	string Licensing::SalvarLog(string logContent)
	{
		LicencaRetorno lr;
		
		try
		{
			string decInfo = Decrypt(logContent);
			LogInfo log;
			log.SetData(decInfo);
            lr = SalvarLicencaLog(log);
		}
		catch(const std::runtime_error & re)
		{
			lr.Tipo = RETORNO_TIPO::Erro;
			lr.Descricao = "Erro";
			lr.Mensagem = re.what();
			lr.Valor = false;
		}	
		catch (exception & e)
		{
			lr.Tipo = RETORNO_TIPO::Erro;
			lr.Descricao = "Erro";
			lr.Mensagem = e.what();
			lr.Valor = false;
		}
		

		return Encrypt(lr.ToString());
	}

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
	string Licensing::Remover(string licenseInfo)
	{
		LicencaRetorno lr;
		
		try
		{
			string decInfo = Decrypt(licenseInfo);

            LicencaInfo info;
			info.SetData(decInfo);

            lr = RemoverLicenca(info);
		}
		catch(const std::runtime_error & re)
		{
			lr.Tipo = RETORNO_TIPO::Erro;
			lr.Descricao = "Erro";
			lr.Mensagem = re.what();
			lr.Valor = false;
		}	
		catch (exception & e)
		{
			lr.Tipo = RETORNO_TIPO::Erro;
			lr.Descricao = "Erro";
			lr.Mensagem = e.what();
			lr.Valor = false;
		}
		

		return Encrypt(lr.ToString());
	}

	/// <summary>
	/// Encript Data;
	/// </summary>
	/// <param name="data"></param>
	/// <param name="token"></param>
	/// <returns></returns>
	string Licensing::Encrypt(string data, string token)
	{
		if (!IsValidToken(token))
        {
            return "BAD TOKEN";
        }

        return Licensing::Encrypt(data);
	}
	/// <summary>
	/// Encrypt Data. (static)
	/// </summary>
	/// <param name="data"></param>
	/// <returns></returns>
	string Licensing::Encrypt(string data)
	{
		return Undefined(data);
	}
	/// <summary>
	/// Decrypt Data.
	/// </summary>
	/// <param name="data"></param>
	/// <param name="token"></param>
	/// <returns></returns>
	string Licensing::Decrypt(string data, string token)
	{
		if (!IsValidToken(token))
        {
            return "BAD TOKEN";
        }

        return Licensing::Decrypt(data);
	}
	/// <summary>
	/// Decrypt Data. (Static)
	/// </summary>
	/// <param name="data"></param>
	/// <returns></returns>
	string Licensing::Decrypt(string data)
	{
		return Undefined2(data);
	}

	/// <summary>
	/// Test token.
	/// </summary>
	bool Licensing::IsValidToken(string token)
	{
		try
		{
			COleDateTime startTime = LicenseDB::Convert(Licensing::Decrypt(token));        
			COleDateTime endTime = COleDateTime::GetCurrentTime();
			COleDateTimeSpan span = endTime - startTime;
			if (span.GetMinutes() <= 2)
				return true;
		}
		catch(const std::runtime_error & re) {}
		catch (exception &e) {}
        
        return false;
	}

	/// <summary>
	/// This method should be passed to the business area, for generating the token key.
	/// </summary>
	/// <returns></returns>
	string Licensing::GenerateToken(string publicKey)
	{
		COleDateTime dt = COleDateTime::GetCurrentTime();
		return Undefined(LicenseDB::Convert(dt));
	}

	/// <summary>
	/// Dias antes de expirar.
	/// </summary>
	/// <param name="licencaUso"></param>
	/// <returns></returns>
	int Licensing::DiasAExpirar(LicencaUso licencaUso)
	{
		COleDateTime dt = licencaUso.Data + COleDateTimeSpan(atol(licencaUso.DiasOffline.c_str()), 0, 0, 0 );
		COleDateTimeSpan span = dt - COleDateTime::GetCurrentTime();
        return (span.GetDays() + 1);
	}
	/// <summary>
	/// Licença está expirada?
	/// </summary>
	/// <param name="licencaUso"></param>
	/// <returns></returns>
	bool Licensing::LicencaExpirada(LicencaUso licencaUso)
	{
		COleDateTime dt = licencaUso.Data + COleDateTimeSpan(atol(licencaUso.DiasOffline.c_str()), 0, 0, 0 );
		dt.SetDateTime(dt.GetYear(), dt.GetMonth(), dt.GetDay(), 23,59,59);
		return (dt < COleDateTime::GetCurrentTime());
	}

	/// <summary>
	/// Adjust message result.
	/// </summary>
	/// <param name="result"></param>
	/// <param name="licencaUso"></param>
	/// <param name="exp"></param>
	void Licensing::SetMessageResult(LicencaRetorno &result, LicencaUso licencaUso, exception exp)
	{
		string errorMsg = exp.what();
		if (errorMsg != "0")
        {
			if (licencaUso.IdLU <= 0)
            {
                result.Valor = false;
				result.Tipo = RETORNO_TIPO::Erro;
                result.Descricao = "Erro";
                result.Mensagem = errorMsg;
            }
            else
            {
                bool lExpirada = LicencaExpirada(licencaUso);
				result.Valor = (licencaUso.LxStatusChave == STATUS_CHAVE::ATIVO && !lExpirada);

                if (lExpirada)
                {
                    result.Tipo = RETORNO_TIPO::Erro;
                    result.Descricao = "Erro";
                    result.Mensagem = "Licença Expirada.";
                }
                else if (licencaUso.Data < licencaUso.DataProcesso)
                {
					result.Tipo = RETORNO_TIPO::Alerta;
                    result.Descricao = "Alerta";
					stringstream streamStr;
					streamStr << "A sua licença vai expirar em " << DiasAExpirar(licencaUso) << " dia(s).";
					result.Mensagem = streamStr.str();
                }
                else
                {
                    result.Valor = false;
					result.Tipo = RETORNO_TIPO::Erro;
                    result.Descricao = "Erro";
                    result.Mensagem = errorMsg;
                }
            }
        }
		else
		{
			if (licencaUso.IdLicencaUso.length() == 0)
            {
				result.Tipo = RETORNO_TIPO::Ok;
                result.Descricao = "Ok";
                result.Mensagem = "";
            }
            else
            {

                bool lExpirada = LicencaExpirada(licencaUso);
                result.Valor = (licencaUso.LxStatusChave == RETORNO_TIPO::Ok && !lExpirada);

				if (licencaUso.LxStatusChave != STATUS_CHAVE::ATIVO)
                {
					result.Tipo = RETORNO_TIPO::Erro;
                    result.Descricao = "Erro";
					result.Mensagem = (licencaUso.LxStatusChave == STATUS_CHAVE::PENDENTE ? "PENDENTE" : "REVOGADO");					
                }
                else if (lExpirada)
                {
                    result.Tipo = RETORNO_TIPO::Erro;
                    result.Descricao = "Erro";
                    result.Mensagem = "Licença Expirada.";
                }
                else if (licencaUso.Data < licencaUso.DataProcesso)
                {
                    result.Tipo = RETORNO_TIPO::Alerta;
                    result.Descricao = "Alerta";
					stringstream streamStr;
					streamStr << "A sua licença vai expirar em " << DiasAExpirar(licencaUso) << " dia(s).";
					result.Mensagem = streamStr.str();
                }
                else
                {
					result.Tipo = RETORNO_TIPO::Ok;
                    result.Descricao = "Ok";
                    result.Mensagem = "Licença Ativa.";
                }
            }
		}

	}
	
	string Licensing::Undefined(string data) {
		//char *key = &alert[0]; //Any chars will work
		//string output = data;
	 //   
		//for (int i = 0; i < data.size(); i++)
		//	output[i] = data[i] ^ key[i % (sizeof(key) / sizeof(char))];
	 //   
		//return output;

		return base64_encode(reinterpret_cast<const unsigned char*>(data.c_str()), data.length());;
	}

	string Licensing::Undefined2(string data) {
		return base64_decode(data);		
	}

	#pragma endregion
	
};