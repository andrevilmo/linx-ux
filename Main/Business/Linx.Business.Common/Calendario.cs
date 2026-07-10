using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Business.Common
{
    public class Calendario
    {
        /// <summary>
        /// Método para retornar o proximo dia util 
        /// </summary>
        /// <param name="data">Data para retorno do próximo dia util</param>        
        /// <returns>Nova data com o proximo dia util</returns>
        public DateTime GetProximoDiaUtil(DateTime data)
        {
            //Verifico se é sabado
            if (data.DayOfWeek == DayOfWeek.Saturday)
                data = data.AddDays(2);

            //Verifico se é domingo
            if (data.DayOfWeek == DayOfWeek.Sunday)
                data = data.AddDays(1);

            return data;
        }

        /// <summary>
        /// Método para retornar o dia da semana
        /// </summary>
        /// <param name="diaSemana">Int dia da semana
        /// OBS: 1 - Domingo
        ///      2 - Segunda
        ///      3 - Terça
        ///      4 - Quarta
        ///      5 - Quinta
        ///      6 - Sexta
        ///      7 - Sabado
        /// </param>
        /// <returns>Objeto do tipo DayOfWeek</returns>
        public DayOfWeek GetDiaDaSemana(int diaSemana)
        {
            DayOfWeek retorno = new DayOfWeek();
            switch (diaSemana)
            {
                case 1:
                    retorno = DayOfWeek.Sunday;
                    break;
                case 2:
                    retorno = DayOfWeek.Monday;
                    break;
                case 3:
                    retorno = DayOfWeek.Tuesday;
                    break;
                case 4:
                    retorno = DayOfWeek.Wednesday;
                    break;
                case 5:
                    retorno = DayOfWeek.Thursday;
                    break;
                case 6:
                    retorno = DayOfWeek.Friday;
                    break;
                case 7:
                    retorno = DayOfWeek.Saturday;
                    break;
            }

            return retorno;
        }

        /// <summary>
        /// Método para retornar o numero do dia da semana
        /// OBS: 1 - Domingo
        ///      2 - Segunda
        ///      3 - Terça
        ///      4 - Quarta
        ///      5 - Quinta
        ///      6 - Sexta
        ///      7 - Sabado
        /// </summary>
        /// <param name="diaSemana">Dia da semana</param>
        /// <returns>Numero do dia da semana</returns>
        public int GetDiaDaSemanaNumero(DayOfWeek diaSemana)
        {
            int retorno = 0;
            switch (diaSemana)
            {
                case DayOfWeek.Sunday:
                    retorno = 1;
                    break;
                case DayOfWeek.Monday:
                    retorno = 2;
                    break;
                case DayOfWeek.Tuesday:
                    retorno = 3;
                    break;
                case DayOfWeek.Wednesday:
                    retorno = 4;
                    break;
                case DayOfWeek.Thursday:
                    retorno = 5;
                    break;
                case DayOfWeek.Friday:
                    retorno = 6;
                    break;
                case DayOfWeek.Saturday:
                    retorno = 7;
                    break;
            }

            return retorno;
        }

        public bool IsDiaValido(int dia, int mes, int ano)
        {
            int ultimoDiaMes = DateTime.DaysInMonth(ano, mes);
            if (dia > ultimoDiaMes || dia < 1)
                return false;
            else
                return true;
        }

        public bool IsDiaValido(DateTime data)
        {
            int ultimoDiaMes = DateTime.DaysInMonth(data.Year, data.Month);
            if (data.Day > ultimoDiaMes || data.Day < 1)
                return false;
            else
                return true;
        }
    }
}
