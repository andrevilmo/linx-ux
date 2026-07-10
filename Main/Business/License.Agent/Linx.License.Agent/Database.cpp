#include <iostream>
#include <fstream>
#include <vector>
#include <string>
#include <sstream>
#include "sqlite3.h"
#include "Database.h"
#include <stdlib.h>

namespace LocalStorage
{

	string LicenseDB::GetDatabasePath()
	{		
		if (databasePath.length() == 0)
		{
			Licensing licensing;
			TCHAR lpTempPathBuffer[MAX_PATH];
			GetTempPath(MAX_PATH,          // length of the buffer
						lpTempPathBuffer); // buffer for path 
			databasePath = licensing.ConvertWsToS(lpTempPathBuffer);
			databasePath += "RLST1218891720035678.db";
		}

		return databasePath;
	}

	bool LicenseDB::ConnectDB ()
	{
		string dbPath = GetDatabasePath();
		existsDB = FileExists(dbPath);

		if ( sqlite3_open(dbPath.c_str(), &dbfile) == SQLITE_OK )
		{
			isOpenDB = true;
			CheckDataBase();
			return true;
		}
		
		return false;
	}


	bool LicenseDB::FileExists (string & name) {
		ifstream f(name.c_str());
		return f.good();
	}

	void LicenseDB::CheckDataBase()
	{
		if (!existsDB)
		{
			existsDB = true;

			const char *script = "CREATE TABLE [LicencaRequisicao] ("\
				"   [IdLR] INTEGER PRIMARY KEY AUTOINCREMENT,"\
				"   [IdLicenca] [varchar](250) NOT NULL,"\
				"   [IdCliente] [varchar](250) NOT NULL,"\
				"   [Usuario] [varchar](250) NOT NULL,"\
				"   [Chave] [varchar](1000) NOT NULL,"\
				"   [Terminal] [varchar](250) NOT NULL"\
				");"\
				"CREATE TABLE [LicencaUso] ("\
				"   [IdLU] INTEGER PRIMARY KEY AUTOINCREMENT,"\
				"   [IdLicencaUso] [varchar](250) NOT NULL,"\
				"   [LxStatusChave] [varchar](250) NOT NULL,"\
				"   [Periodicidade] [varchar](250) NOT NULL,"\
				"   [DiasOffline] [varchar](250) NOT NULL,"\
				"   [Mensagem] [varchar](2000) NULL,"\
				"   [TemporaryIdLicencaUso] [varchar](250) NOT NULL,"\
				"   [Data] [datetime] NOT NULL,"\
				"   [IdLR] [bigint] NOT NULL,"\
				"   [DataProcesso] [datetime] NOT NULL,"\
				"   FOREIGN KEY (IdLR) REFERENCES LicencaRequisicao(IdLR)"\
				");"\
				"PRAGMA foreign_keys = ON";

			char *zErrMsg = 0;
			int rc;	
			rc = sqlite3_exec(dbfile,script, NULL, 0, &zErrMsg);

			if( rc != SQLITE_OK ){
				sqlite3_free(zErrMsg);
		    } 
		}
	}

	void LicenseDB::DisonnectDB ()
	{
		if ( isOpenDB == true ) 
		{
			sqlite3_close(dbfile);
		}
	}	

	long LicenseDB::GetLastId(string tableName, string columnName)
	{
		std::stringstream strm;
		strm << "select max(" << columnName << ") from " << tableName;

		string s = strm.str();
		char *str = &s[0];
				
		sqlite3_stmt *statement;
		long result;
		char *query = str;
		{
			if(sqlite3_prepare(dbfile,query,-1,&statement,0)==SQLITE_OK)
			{
				int res=sqlite3_step(statement);
				if ( res == SQLITE_ROW ) 
				{					 
					result = (long)sqlite3_column_int64(statement, 0);
				}
				sqlite3_finalize(statement);
			}			
		}

		return result;
	}

	string LicenseDB::Convert(COleDateTime dt)
	{
		CString cstr = dt.Format(_T("%Y-%m-%d %H:%M:%S"));
		CT2CA pszConvertedAnsiString (cstr);
		string str(pszConvertedAnsiString);
		return str;
	}

	string LicenseDB::ConvertJson(COleDateTime dt)
	{
		CString cstr = dt.Format(_T("%Y-%m-%dT%H:%M:%S"));
		CT2CA pszConvertedAnsiString (cstr);
		string str(pszConvertedAnsiString);
		return str;
	}

	COleDateTime LicenseDB::Convert(string strDT)
	{
		int year=-1, month=-1, day=-1, hour=-1, minute=-1, second=-1;
		
		string value = "";
		for (int i = 0; i < strDT.length(); i++)
		{
			char chr = strDT[i];
			if (chr == '-' || chr == ' ' || chr == ':' || chr == 'T')
			{
				if (year == -1)
					year = std::atoi(value.c_str());
				else if (month == -1)
					month = std::atoi(value.c_str());
				else if (day == -1)
					day = std::atoi(value.c_str());
				else if (hour == -1)
					hour = std::atoi(value.c_str());
				else if (minute == -1)
					minute = std::atoi(value.c_str());
				
				value = "";
				continue;
			}
			value += chr;
		}
		if (second == -1)
			second = std::atoi(value.c_str());

		return COleDateTime(year, month, day, hour, minute, second);
	}

	int LicenseDB::Add(LicencaUso *lu)
	{
		std::stringstream strm;
		strm << "insert into LicencaUso(IdLR,IdLicencaUso,LxStatusChave,Periodicidade,DiasOffline,Mensagem,TemporaryIdLicencaUso,Data,DataProcesso) values(" << lu->IdLR << ",'" << lu->IdLicencaUso << "'," << lu->LxStatusChave << ",'" << lu->Periodicidade << "','" << lu->DiasOffline << "','" << lu->Mensagem << "','" << lu->TemporaryIdLicencaUso << "','" << LicenseDB::Convert(lu->Data) << "','" << LicenseDB::Convert(lu->DataProcesso) << "')";

		string s = strm.str();
		char *str = &s[0];
				
		sqlite3_stmt *statement;
		int result=0;
		char *query = str;
		{
			if(sqlite3_prepare(dbfile,query,-1,&statement,0)==SQLITE_OK)
			{
				int res=sqlite3_step(statement);
				result=res;
				lu->IdLU = GetLastId("LicencaUso", "IdLU");
				sqlite3_finalize(statement);
			}
		}

		return result;
	}

	int LicenseDB::Add(LicencaRequisicao *lr)
	{
		std::stringstream strm;
		strm << "insert into LicencaRequisicao(IdLicenca,IdCliente,Chave,Usuario,Terminal) values('" << lr->IdLicenca << "','" << lr->IdCliente << "','" << lr->Chave << "','" << lr->Usuario << "','" << lr->Terminal << "')";

		string s = strm.str();
		char *str = &s[0];
				
		sqlite3_stmt *statement;
		int result = 0;
		char *query = str;
		{
			if(sqlite3_prepare(dbfile,query,-1,&statement,0)==SQLITE_OK)
			{
				int res=sqlite3_step(statement);
				result=res;				
				
				lr->IdLR = GetLastId("LicencaRequisicao", "IdLR");

				sqlite3_finalize(statement);
			}
		}

		return result;
	}

	int LicenseDB::Delete(LicencaUso lu)
	{	
		std::stringstream strm;
		strm << "delete from LicencaUso " << " where IdLU=" << lu.IdLU ;

		string s = strm.str();
		char *str = &s[0];

		sqlite3_stmt *statement;
		int result = 0;
		char *query = str;
		{
			if(sqlite3_prepare(dbfile,query,-1,&statement,0)==SQLITE_OK)
			{
				int res=sqlite3_step(statement);
				result=res;
				sqlite3_finalize(statement);
			}
			
		}
		
		return result;
	}

	int LicenseDB::Delete(LicencaRequisicao lr)
	{
		std::stringstream strm;
		strm << "delete from LicencaRequisicao " << " where IdLR=" << lr.IdLR;

		string s = strm.str();
		char *str = &s[0];

		sqlite3_stmt *statement;
		int result = 0;
		char *query = str;
		{
			if(sqlite3_prepare(dbfile,query,-1,&statement,0)==SQLITE_OK)
			{
				int res=sqlite3_step(statement);
				result=res;
				sqlite3_finalize(statement);
			}
			
		}
		
		return result;
	}

	int LicenseDB::Update(LicencaUso lu)
	{	
		std::stringstream strm;
		strm << "update LicencaUso set LxStatusChave=" << lu.LxStatusChave << ", Periodicidade='" << lu.Periodicidade << "', DiasOffline='" << lu.DiasOffline << "', Mensagem='" << lu.Mensagem << "', IdLicencaUso='" << lu.IdLicencaUso << "', TemporaryIdLicencaUso='" << lu.TemporaryIdLicencaUso << "', Data='" << LicenseDB::Convert(lu.Data) << "', DataProcesso='" << LicenseDB::Convert(lu.DataProcesso) << "' where IdLU=" << lu.IdLU ;

		string s = strm.str();
		char *str = &s[0];
		
		sqlite3_stmt *statement;
		int result = 0;
		char *query = str;	
		
		{
			if(sqlite3_prepare(dbfile,query,-1,&statement,0)==SQLITE_OK)
			{
				int res=sqlite3_step(statement);
				result=res;
				sqlite3_finalize(statement);
			}
			
		}
		
		return result;
	}

	int LicenseDB::Update(LicencaRequisicao lr)
	{
		std::stringstream strm;
		strm << "update LicencaRequisicao set IdLicenca='" << lr.IdLicenca << "', IdCliente='" << lr.IdCliente << "', Chave='" << lr.Chave << "', Usuario='" << lr.Usuario << "', Terminal='" << lr.Terminal << "' where IdLR=" << lr.IdLR;

		string s = strm.str();
		char *str = &s[0];
		
		sqlite3_stmt *statement;
		int result = 0;
		char *query = str;	
		
		{
			if(sqlite3_prepare(dbfile,query,-1,&statement,0)==SQLITE_OK)
			{
				int res=sqlite3_step(statement);
				result=res;
				sqlite3_finalize(statement);
			}
		}
		
		return result;
	}

	//Remember: Deallocate memory result like "delete[] pointer;"
	LicencaUso LicenseDB::Query(LicencaRequisicao lr)
	{
		LicencaUso lu;
		sqlite3_stmt *statement;	
		std::stringstream strm;
		strm << "select * from LicencaUso where IdLR = " << lr.IdLR << " and LxStatusChave == " << STATUS_CHAVE::ATIVO << " order by Data desc limit 1";

		string s = strm.str();
		char *str = &s[0];
		
		char *query = str;

		if ( sqlite3_prepare(dbfile, query, -1, &statement, 0 ) == SQLITE_OK ) 
		{
			int ctotal = sqlite3_column_count(statement);
			int res = 0;

			while ( 1 )
			{
				res = sqlite3_step(statement);

				if ( res == SQLITE_ROW ) 
				{
					for ( int i = 0; i < ctotal; i++ ) 
					{
						string columnName = (char*)sqlite3_column_name(statement, i);							
						
						if (columnName == "IdLR")
								lu.IdLR = (long)sqlite3_column_int64(statement, i);
						if (columnName == "IdLU")
								lu.IdLU = (long)sqlite3_column_int64(statement, i);
						else if (columnName == "LxStatusChave")
								lu.LxStatusChave = (STATUS_CHAVE)sqlite3_column_int(statement, i);
						else if (columnName == "IdLicencaUso")
								lu.IdLicencaUso = (char*)sqlite3_column_text(statement, i);
						else if (columnName == "Periodicidade")
								lu.Periodicidade = (char*)sqlite3_column_text(statement, i);
						else if (columnName == "DiasOffline")
								lu.DiasOffline = (char*)sqlite3_column_text(statement, i);
						else if (columnName == "Mensagem")
								lu.Mensagem = (char*)sqlite3_column_text(statement, i);
						else if (columnName == "TemporaryIdLicencaUso")
								lu.TemporaryIdLicencaUso = (char*)sqlite3_column_text(statement, i);
						else if (columnName == "Data")
								lu.Data = LicenseDB::Convert((char*)sqlite3_column_text(statement, i));					
						else if (columnName == "DataProcesso")
								lu.DataProcesso = LicenseDB::Convert((char*)sqlite3_column_text(statement, i));			
								

					}
				}

				if ( res == SQLITE_DONE )	
				{
					break;
				}				
			}

		}


		return lu;
	}

	LicencaRequisicao LicenseDB::Query(string idCliente, string idLicenca, string idChave)
	{
		LicencaRequisicao lr;

		sqlite3_stmt *statement;	
		std::stringstream strm;
		strm << "select * from LicencaRequisicao where IdCliente = '" << idCliente << "' and IdLicenca = '" << idLicenca << "' and Chave = '" << idChave << "' order by IdLR desc limit 1";

		string s = strm.str();
		char *str = &s[0];
		
		char *query = str;

		if ( sqlite3_prepare(dbfile, query, -1, &statement, 0 ) == SQLITE_OK ) 
		{
			int ctotal = sqlite3_column_count(statement);
			int res = 0;

			while ( 1 )
			{
				res = sqlite3_step(statement);

				if ( res == SQLITE_ROW ) 
				{
					for ( int i = 0; i < ctotal; i++ ) 
					{
						string columnName = (char*)sqlite3_column_name(statement, i);							
						
						if (columnName == "IdLR")
								lr.IdLR = (long)sqlite3_column_int64(statement, i);						
						else if (columnName == "IdLicenca")
								lr.IdLicenca = (char*)sqlite3_column_text(statement, i);
						else if (columnName == "IdCliente")
								lr.IdCliente = (char*)sqlite3_column_text(statement, i);
						else if (columnName == "Chave")
								lr.Chave = (char*)sqlite3_column_text(statement, i);
						else if (columnName == "Usuario")
								lr.Usuario = (char*)sqlite3_column_text(statement, i);
						else if (columnName == "Terminal")
								lr.Terminal = (char*)sqlite3_column_text(statement, i);
							

					}
				}

				if ( res == SQLITE_DONE )	
				{
					break;
				}
			}
		}

		return lr;
	}



}