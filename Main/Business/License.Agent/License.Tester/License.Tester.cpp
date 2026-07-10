// License.Tester.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include "Database.h"
#include <Windows.h>
#include <atlstr.h>

using namespace std;
using namespace LocalStorage;


int main()
{
	string publicKey = "LINX174557892000";
    Licensing instance;

	
	#pragma region Check License
            
            cout << "Checking license..." << endl;
            //Data Definition            
            char *data = "{"
             "\"IdLicenca\":4,"
             "\"IdCliente\":\"65161419000170\","
             "\"Usuario\":\"usuarioTeste1\""
            "}";
            //Get new token (Expires in two minutes)
            string token = instance.GenerateToken(publicKey);
			cout << "Token: " << token << endl;
            //Encrypt data
            string encryptData = instance.Encrypt(data, token);
            //Verify license
            string result = instance.Validar(encryptData);
            //Decrypt license result
            string dataResult = instance.Decrypt(result, token);  
			cout << "Data Result: " << dataResult << endl;
			cout << "End Checking License!" << endl;
		
			//How to get property value of the result		
			cout << "Valor: " << instance.GetJsonPropertyValue(dataResult, "Valor" ) << endl;
			cout << "Descricao: " << instance.GetJsonPropertyValue(dataResult, "Descricao" ) << endl;
            

     #pragma endregion

	 #pragma region Remove License
            
			cout << "Removing license..." << endl;            
            //Get new token (Expires in two minutes)
            token = instance.GenerateToken(publicKey);
            //Encrypt data
            encryptData = instance.Encrypt(data, token);
            //Verify license
            result = instance.Remover(encryptData);
            //Decrypt license result
            dataResult = instance.Decrypt(result, token);
            cout << "Data Result: " << dataResult << endl;
			cout << "End Removing license!" << endl;

     #pragma endregion


	 #pragma region Save LOG
            
			cout << "Saving log..." << endl; 
            //Data Definition            
            data = "{"
                   "\"IdProduto\":3,"
                   "\"IdCliente\":\"65161419000170\","
                   "\"Usuario\":\"usuarioTeste1\","
                   "\"IdUsuario\":null,"
                   "\"NomeAutenticacao\":null,"
                   "\"CodigoFilial\":\"\","
                   "\"NomeFilial\":\"\","
                   "\"IndicaLoja\":false,"
                   "\"IdLinx\":null,"
                   "\"Detalhes\":[ \"Test1\", \"Test2\" ]"
                  "}";
            //Get new token (Expires in two minutes)
            token = instance.GenerateToken(publicKey);			
            //Encrypt data
            encryptData = instance.Encrypt(data, token);
            //Verify license
            result = instance.SalvarLog(encryptData);
            //Decrypt license result
            dataResult = instance.Decrypt(result, token);
            cout << "Data Result: " << dataResult << endl;
			cout << "End Saving log!" << endl;

            #pragma endregion


    return 0;
}

