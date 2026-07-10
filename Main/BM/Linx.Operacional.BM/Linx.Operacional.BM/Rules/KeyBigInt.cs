using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Operacional.BM.Rules
{
    public class KeyBigInt
    {
        public static Int64 GetBigIntFromGuid(string tableName, string columnName, Guid id)
        {
            Guid valorAntigo = Guid.Parse(id.ToString());
            long valorNovo = 0;

            System.Data.SqlClient.SqlParameter[] param = new System.Data.SqlClient.SqlParameter[] {
                        new System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.UniqueIdentifier) { Value = valorAntigo },
                        new System.Data.SqlClient.SqlParameter("@TABELA", System.Data.SqlDbType.VarChar) { Value = tableName },
                        new System.Data.SqlClient.SqlParameter("@COLUNA", System.Data.SqlDbType.VarChar) { Value = columnName },
                        new System.Data.SqlClient.SqlParameter("@VALOR_NOVO", System.Data.SqlDbType.BigInt) { Value = valorNovo, Direction = System.Data.ParameterDirection.InputOutput }
                    };

            Linx.Operacional.BM.LinxOperacional bm = new BM.LinxOperacional();
            bm.Database.ExecuteSqlCommand("EXEC LX_LJV.LJV_CONTROLE_GUID_BIGINT @ID, @TABELA, @COLUNA, @VALOR_NOVO OUTPUT", param);
                    
            if (DBNull.Value.Equals(param[3].Value))
                throw new Exception(String.Format("Não foi possível converter o valor '{0}' de Guid para BigInt", valorAntigo));

            valorNovo = Convert.ToInt64(param[3].Value);

            return valorNovo; 
        }
        
        //public static long GetNewKeyByTerminal(int terminalId)
        //{
        //    long timePart = (long)(DateTime.Now.Subtract(new DateTime(2000, 1, 1)).TotalMilliseconds / 10);

        //    if (timePart > 8796093022206)
        //        throw new Exception("O controle de tempo excedeu o tamanho máximo (8.796.093.022.206).");

        //    if (terminalId > 16777214)
        //        throw new Exception("O id do terminal excedeu o tamanho máximo (16.777.214).");

        //    long newKey = ((long)terminalId << 40) + timePart;

        //    return newKey;
        //}

        //public static long GetNewKeyByLoja(int lojaId)
        //{
        //    long timePart = (long)(DateTime.Now.Subtract(new DateTime(2000, 1, 1)).TotalMilliseconds / 10);

        //    if (timePart > 8796093022206)
        //        throw new Exception("O controle de tempo excedeu o tamanho máximo (8.796.093.022.206).");

        //    if (lojaId > 16777214)
        //        throw new Exception("O id do terminal excedeu o tamanho máximo (16.777.214).");

        //    long newKey = ((long)lojaId << 40) + timePart;

        //    return newKey;
        //}
    }
}
