#include <iostream>
#include <fstream>
#include <vector>
#include <string>
#include <sstream>
#include "sqlite3.h"
#include "Linx.License.Agent.h"

using namespace LicenseAgent;

namespace LocalStorage
{


	class LicenseDB
	{
		private:
			bool isOpenDB;
			bool existsDB;
			sqlite3 *dbfile;
			long GetLastId(string, string);
			void CheckDataBase();
			bool FileExists (string &);
			string databasePath;
		public:
			static string Convert(COleDateTime);
			static string ConvertJson(COleDateTime);
			static COleDateTime Convert(string);
			bool ConnectDB ();
			void DisonnectDB ();
			int Add(LicencaUso *);
			int Add(LicencaRequisicao *);
			int Delete(LicencaUso);
			int Delete(LicencaRequisicao);
			int Update(LicencaUso);
			int Update(LicencaRequisicao);
			LicencaUso Query(LicencaRequisicao);
			LicencaRequisicao Query(string, string, string);
			string GetDatabasePath();

	};

}