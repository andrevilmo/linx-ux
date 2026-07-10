			

using System;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using Linx.Tools; 
    
namespace Linx.Demo.BM.Domains
{

			
    public partial class LX_COMBOBOX_LOJA
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "LOJA1"); 
				    
					result.Add("2", "LOJA2"); 
				    
					result.Add("3", "LOJA3"); 
				    
					result.Add("4", "LOJA4"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "LOJA1"); 
				    
					result.Add("2", "LOJA2"); 
				    
					result.Add("3", "LOJA3"); 
				    
					result.Add("4", "LOJA4"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _LOJA1 = new DomainKeyPair() { Value = "1", DisplayName = "LOJA1" };
					[FunctionalPoint("Value[1];DisplayName[LOJA1]")]
					public static DomainKeyPair LOJA1 { get { return _LOJA1; } }
				    
					private static DomainKeyPair _LOJA2 = new DomainKeyPair() { Value = "2", DisplayName = "LOJA2" };
					[FunctionalPoint("Value[2];DisplayName[LOJA2]")]
					public static DomainKeyPair LOJA2 { get { return _LOJA2; } }
				    
					private static DomainKeyPair _LOJA3 = new DomainKeyPair() { Value = "3", DisplayName = "LOJA3" };
					[FunctionalPoint("Value[3];DisplayName[LOJA3]")]
					public static DomainKeyPair LOJA3 { get { return _LOJA3; } }
				    
					private static DomainKeyPair _LOJA4 = new DomainKeyPair() { Value = "4", DisplayName = "LOJA4" };
					[FunctionalPoint("Value[4];DisplayName[LOJA4]")]
					public static DomainKeyPair LOJA4 { get { return _LOJA4; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_COMBOBOX_VENDA_ITEM
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "VENDA_ITEM1"); 
				    
					result.Add("2", "VENDA_ITEM2"); 
				    
					result.Add("3", "VENDA_ITEM3"); 
				    
					result.Add("4", "VENDA_ITEM4"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "VENDA_ITEM1"); 
				    
					result.Add("2", "VENDA_ITEM2"); 
				    
					result.Add("3", "VENDA_ITEM3"); 
				    
					result.Add("4", "VENDA_ITEM4"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _VENDA_ITEM1 = new DomainKeyPair() { Value = "1", DisplayName = "VENDA_ITEM1" };
					[FunctionalPoint("Value[1];DisplayName[VENDA_ITEM1]")]
					public static DomainKeyPair VENDA_ITEM1 { get { return _VENDA_ITEM1; } }
				    
					private static DomainKeyPair _VENDA_ITEM2 = new DomainKeyPair() { Value = "2", DisplayName = "VENDA_ITEM2" };
					[FunctionalPoint("Value[2];DisplayName[VENDA_ITEM2]")]
					public static DomainKeyPair VENDA_ITEM2 { get { return _VENDA_ITEM2; } }
				    
					private static DomainKeyPair _VENDA_ITEM3 = new DomainKeyPair() { Value = "3", DisplayName = "VENDA_ITEM3" };
					[FunctionalPoint("Value[3];DisplayName[VENDA_ITEM3]")]
					public static DomainKeyPair VENDA_ITEM3 { get { return _VENDA_ITEM3; } }
				    
					private static DomainKeyPair _VENDA_ITEM4 = new DomainKeyPair() { Value = "4", DisplayName = "VENDA_ITEM4" };
					[FunctionalPoint("Value[4];DisplayName[VENDA_ITEM4]")]
					public static DomainKeyPair VENDA_ITEM4 { get { return _VENDA_ITEM4; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_COMBOBOX_CIDADE
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "CIDADE1"); 
				    
					result.Add("2", "CIDADE2"); 
				    
					result.Add("3", "CIDADE3"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "CIDADE1"); 
				    
					result.Add("2", "CIDADE2"); 
				    
					result.Add("3", "CIDADE3"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _CIDADE1 = new DomainKeyPair() { Value = "1", DisplayName = "CIDADE1" };
					[FunctionalPoint("Value[1];DisplayName[CIDADE1]")]
					public static DomainKeyPair CIDADE1 { get { return _CIDADE1; } }
				    
					private static DomainKeyPair _CIDADE2 = new DomainKeyPair() { Value = "2", DisplayName = "CIDADE2" };
					[FunctionalPoint("Value[2];DisplayName[CIDADE2]")]
					public static DomainKeyPair CIDADE2 { get { return _CIDADE2; } }
				    
					private static DomainKeyPair _CIDADE3 = new DomainKeyPair() { Value = "3", DisplayName = "CIDADE3" };
					[FunctionalPoint("Value[3];DisplayName[CIDADE3]")]
					public static DomainKeyPair CIDADE3 { get { return _CIDADE3; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_COMBOBOX_ESTADO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "ESTADO1"); 
				    
					result.Add("2", "ESTADO2"); 
				    
					result.Add("3", "ESTADO3"); 
				    
					result.Add("4", "ESTADO4"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "ESTADO1"); 
				    
					result.Add("2", "ESTADO2"); 
				    
					result.Add("3", "ESTADO3"); 
				    
					result.Add("4", "ESTADO4"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _ESTADO1 = new DomainKeyPair() { Value = "1", DisplayName = "ESTADO1" };
					[FunctionalPoint("Value[1];DisplayName[ESTADO1]")]
					public static DomainKeyPair ESTADO1 { get { return _ESTADO1; } }
				    
					private static DomainKeyPair _ESTADO2 = new DomainKeyPair() { Value = "2", DisplayName = "ESTADO2" };
					[FunctionalPoint("Value[2];DisplayName[ESTADO2]")]
					public static DomainKeyPair ESTADO2 { get { return _ESTADO2; } }
				    
					private static DomainKeyPair _ESTADO3 = new DomainKeyPair() { Value = "3", DisplayName = "ESTADO3" };
					[FunctionalPoint("Value[3];DisplayName[ESTADO3]")]
					public static DomainKeyPair ESTADO3 { get { return _ESTADO3; } }
				    
					private static DomainKeyPair _ESTADO4 = new DomainKeyPair() { Value = "4", DisplayName = "ESTADO4" };
					[FunctionalPoint("Value[4];DisplayName[ESTADO4]")]
					public static DomainKeyPair ESTADO4 { get { return _ESTADO4; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_COMBOBOX_PAIS
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "PAIS1"); 
				    
					result.Add("2", "PAIS2"); 
				    
					result.Add("3", "PAIS3"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "PAIS1"); 
				    
					result.Add("2", "PAIS2"); 
				    
					result.Add("3", "PAIS3"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _PAIS1 = new DomainKeyPair() { Value = "1", DisplayName = "PAIS1" };
					[FunctionalPoint("Value[1];DisplayName[PAIS1]")]
					public static DomainKeyPair PAIS1 { get { return _PAIS1; } }
				    
					private static DomainKeyPair _PAIS2 = new DomainKeyPair() { Value = "2", DisplayName = "PAIS2" };
					[FunctionalPoint("Value[2];DisplayName[PAIS2]")]
					public static DomainKeyPair PAIS2 { get { return _PAIS2; } }
				    
					private static DomainKeyPair _PAIS3 = new DomainKeyPair() { Value = "3", DisplayName = "PAIS3" };
					[FunctionalPoint("Value[3];DisplayName[PAIS3]")]
					public static DomainKeyPair PAIS3 { get { return _PAIS3; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_COMBOBOX_MARCA
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "MARCA1"); 
				    
					result.Add("2", "MARCA2"); 
				    
					result.Add("3", "MARCA3"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "MARCA1"); 
				    
					result.Add("2", "MARCA2"); 
				    
					result.Add("3", "MARCA3"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _MARCA1 = new DomainKeyPair() { Value = "1", DisplayName = "MARCA1" };
					[FunctionalPoint("Value[1];DisplayName[MARCA1]")]
					public static DomainKeyPair MARCA1 { get { return _MARCA1; } }
				    
					private static DomainKeyPair _MARCA2 = new DomainKeyPair() { Value = "2", DisplayName = "MARCA2" };
					[FunctionalPoint("Value[2];DisplayName[MARCA2]")]
					public static DomainKeyPair MARCA2 { get { return _MARCA2; } }
				    
					private static DomainKeyPair _MARCA3 = new DomainKeyPair() { Value = "3", DisplayName = "MARCA3" };
					[FunctionalPoint("Value[3];DisplayName[MARCA3]")]
					public static DomainKeyPair MARCA3 { get { return _MARCA3; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_REPRESENTANTE
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "REPRESENTANTE1"); 
				    
					result.Add("2", "REPRESENTANTE2"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "REPRESENTANTE1"); 
				    
					result.Add("2", "REPRESENTANTE2"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _REPRESENTANTE1 = new DomainKeyPair() { Value = "1", DisplayName = "REPRESENTANTE1" };
					[FunctionalPoint("Value[1];DisplayName[REPRESENTANTE1]")]
					public static DomainKeyPair REPRESENTANTE1 { get { return _REPRESENTANTE1; } }
				    
					private static DomainKeyPair _REPRESENTANTE2 = new DomainKeyPair() { Value = "2", DisplayName = "REPRESENTANTE2" };
					[FunctionalPoint("Value[2];DisplayName[REPRESENTANTE2]")]
					public static DomainKeyPair REPRESENTANTE2 { get { return _REPRESENTANTE2; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_REGIAO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "REGIAO1"); 
				    
					result.Add("2", "REGIAO2"); 
				    
					result.Add("3", "REGIAO3"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "REGIAO1"); 
				    
					result.Add("2", "REGIAO2"); 
				    
					result.Add("3", "REGIAO3"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _REGIAO1 = new DomainKeyPair() { Value = "1", DisplayName = "REGIAO1" };
					[FunctionalPoint("Value[1];DisplayName[REGIAO1]")]
					public static DomainKeyPair REGIAO1 { get { return _REGIAO1; } }
				    
					private static DomainKeyPair _REGIAO2 = new DomainKeyPair() { Value = "2", DisplayName = "REGIAO2" };
					[FunctionalPoint("Value[2];DisplayName[REGIAO2]")]
					public static DomainKeyPair REGIAO2 { get { return _REGIAO2; } }
				    
					private static DomainKeyPair _REGIAO3 = new DomainKeyPair() { Value = "3", DisplayName = "REGIAO3" };
					[FunctionalPoint("Value[3];DisplayName[REGIAO3]")]
					public static DomainKeyPair REGIAO3 { get { return _REGIAO3; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_VENDA
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "VENDA1"); 
				    
					result.Add("2", "VENDA2"); 
				    
					result.Add("3", "VENDA3"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "VENDA1"); 
				    
					result.Add("2", "VENDA2"); 
				    
					result.Add("3", "VENDA3"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _VENDA1 = new DomainKeyPair() { Value = "1", DisplayName = "VENDA1" };
					[FunctionalPoint("Value[1];DisplayName[VENDA1]")]
					public static DomainKeyPair VENDA1 { get { return _VENDA1; } }
				    
					private static DomainKeyPair _VENDA2 = new DomainKeyPair() { Value = "2", DisplayName = "VENDA2" };
					[FunctionalPoint("Value[2];DisplayName[VENDA2]")]
					public static DomainKeyPair VENDA2 { get { return _VENDA2; } }
				    
					private static DomainKeyPair _VENDA3 = new DomainKeyPair() { Value = "3", DisplayName = "VENDA3" };
					[FunctionalPoint("Value[3];DisplayName[VENDA3]")]
					public static DomainKeyPair VENDA3 { get { return _VENDA3; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_FORMA_PAGAMENTO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "PAGAMENTO1"); 
				    
					result.Add("2", "PAGAMENTO2"); 
				    
					result.Add("3", "PAGAMENTO3"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "PAGAMENTO1"); 
				    
					result.Add("2", "PAGAMENTO2"); 
				    
					result.Add("3", "PAGAMENTO3"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _PAGAMENTO1 = new DomainKeyPair() { Value = "1", DisplayName = "PAGAMENTO1" };
					[FunctionalPoint("Value[1];DisplayName[PAGAMENTO1]")]
					public static DomainKeyPair PAGAMENTO1 { get { return _PAGAMENTO1; } }
				    
					private static DomainKeyPair _PAGAMENTO2 = new DomainKeyPair() { Value = "2", DisplayName = "PAGAMENTO2" };
					[FunctionalPoint("Value[2];DisplayName[PAGAMENTO2]")]
					public static DomainKeyPair PAGAMENTO2 { get { return _PAGAMENTO2; } }
				    
					private static DomainKeyPair _PAGAMENTO3 = new DomainKeyPair() { Value = "3", DisplayName = "PAGAMENTO3" };
					[FunctionalPoint("Value[3];DisplayName[PAGAMENTO3]")]
					public static DomainKeyPair PAGAMENTO3 { get { return _PAGAMENTO3; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_VENDEDOR
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "VENDEDOR1"); 
				    
					result.Add("2", "VENDEDOR2"); 
				    
					result.Add("3", "VENDEDOR3"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "VENDEDOR1"); 
				    
					result.Add("2", "VENDEDOR2"); 
				    
					result.Add("3", "VENDEDOR3"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _VENDEDOR1 = new DomainKeyPair() { Value = "1", DisplayName = "VENDEDOR1" };
					[FunctionalPoint("Value[1];DisplayName[VENDEDOR1]")]
					public static DomainKeyPair VENDEDOR1 { get { return _VENDEDOR1; } }
				    
					private static DomainKeyPair _VENDEDOR2 = new DomainKeyPair() { Value = "2", DisplayName = "VENDEDOR2" };
					[FunctionalPoint("Value[2];DisplayName[VENDEDOR2]")]
					public static DomainKeyPair VENDEDOR2 { get { return _VENDEDOR2; } }
				    
					private static DomainKeyPair _VENDEDOR3 = new DomainKeyPair() { Value = "3", DisplayName = "VENDEDOR3" };
					[FunctionalPoint("Value[3];DisplayName[VENDEDOR3]")]
					public static DomainKeyPair VENDEDOR3 { get { return _VENDEDOR3; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_CODIGO_FISCAL
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "FISCAL1"); 
				    
					result.Add("2", "FISCAL2"); 
				    
					result.Add("3", "FISCAL3"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "FISCAL1"); 
				    
					result.Add("2", "FISCAL2"); 
				    
					result.Add("3", "FISCAL3"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _FISCAL1 = new DomainKeyPair() { Value = "1", DisplayName = "FISCAL1" };
					[FunctionalPoint("Value[1];DisplayName[FISCAL1]")]
					public static DomainKeyPair FISCAL1 { get { return _FISCAL1; } }
				    
					private static DomainKeyPair _FISCAL2 = new DomainKeyPair() { Value = "2", DisplayName = "FISCAL2" };
					[FunctionalPoint("Value[2];DisplayName[FISCAL2]")]
					public static DomainKeyPair FISCAL2 { get { return _FISCAL2; } }
				    
					private static DomainKeyPair _FISCAL3 = new DomainKeyPair() { Value = "3", DisplayName = "FISCAL3" };
					[FunctionalPoint("Value[3];DisplayName[FISCAL3]")]
					public static DomainKeyPair FISCAL3 { get { return _FISCAL3; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_PRODUTO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "PRODUTO1"); 
				    
					result.Add("2", "PRODUTO2"); 
				    
					result.Add("3", "PRODUTO3"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "PRODUTO1"); 
				    
					result.Add("2", "PRODUTO2"); 
				    
					result.Add("3", "PRODUTO3"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _PRODUTO1 = new DomainKeyPair() { Value = "1", DisplayName = "PRODUTO1" };
					[FunctionalPoint("Value[1];DisplayName[PRODUTO1]")]
					public static DomainKeyPair PRODUTO1 { get { return _PRODUTO1; } }
				    
					private static DomainKeyPair _PRODUTO2 = new DomainKeyPair() { Value = "2", DisplayName = "PRODUTO2" };
					[FunctionalPoint("Value[2];DisplayName[PRODUTO2]")]
					public static DomainKeyPair PRODUTO2 { get { return _PRODUTO2; } }
				    
					private static DomainKeyPair _PRODUTO3 = new DomainKeyPair() { Value = "3", DisplayName = "PRODUTO3" };
					[FunctionalPoint("Value[3];DisplayName[PRODUTO3]")]
					public static DomainKeyPair PRODUTO3 { get { return _PRODUTO3; } }
				    
			#endregion properties

		

	}    

}