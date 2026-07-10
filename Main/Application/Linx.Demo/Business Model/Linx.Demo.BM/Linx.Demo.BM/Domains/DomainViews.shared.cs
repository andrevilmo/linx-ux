			

using System;
using System.IO;
using System.Collections.Generic;
using Linx.Tools; 
    
namespace Linx.Demo.BM.Domains
{

			
    public partial class LX_LOJA
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "LOJA 1"); 
				    
					result.Add("2", "LOJA 2"); 
				    
					result.Add("3", "LOJA 3"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "LOJA_1"); 
				    
					result.Add("2", "LOJA_2"); 
				    
					result.Add("3", "LOJA_3"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _LOJA_1 = new DomainKeyPair() { Value = "1", DisplayName = "LOJA 1" };
					[FunctionalPoint("Value[1];DisplayName[LOJA 1]")]
					public static DomainKeyPair LOJA_1 { get { return _LOJA_1; } }
				    
					private static DomainKeyPair _LOJA_2 = new DomainKeyPair() { Value = "2", DisplayName = "LOJA 2" };
					[FunctionalPoint("Value[2];DisplayName[LOJA 2]")]
					public static DomainKeyPair LOJA_2 { get { return _LOJA_2; } }
				    
					private static DomainKeyPair _LOJA_3 = new DomainKeyPair() { Value = "3", DisplayName = "LOJA 3" };
					[FunctionalPoint("Value[3];DisplayName[LOJA 3]")]
					public static DomainKeyPair LOJA_3 { get { return _LOJA_3; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_ESTADO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "ESTADO 1"); 
				    
					result.Add("2", "ESTADO 2"); 
				    
					result.Add("3", "ESTADO 3"); 
				    
					result.Add("4", "ESTADO 4"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "ESTADO_1"); 
				    
					result.Add("2", "ESTADO_2"); 
				    
					result.Add("3", "ESTADO_3"); 
				    
					result.Add("4", "ESTADO_4"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _ESTADO_1 = new DomainKeyPair() { Value = "1", DisplayName = "ESTADO 1" };
					[FunctionalPoint("Value[1];DisplayName[ESTADO 1]")]
					public static DomainKeyPair ESTADO_1 { get { return _ESTADO_1; } }
				    
					private static DomainKeyPair _ESTADO_2 = new DomainKeyPair() { Value = "2", DisplayName = "ESTADO 2" };
					[FunctionalPoint("Value[2];DisplayName[ESTADO 2]")]
					public static DomainKeyPair ESTADO_2 { get { return _ESTADO_2; } }
				    
					private static DomainKeyPair _ESTADO_3 = new DomainKeyPair() { Value = "3", DisplayName = "ESTADO 3" };
					[FunctionalPoint("Value[3];DisplayName[ESTADO 3]")]
					public static DomainKeyPair ESTADO_3 { get { return _ESTADO_3; } }
				    
					private static DomainKeyPair _ESTADO_4 = new DomainKeyPair() { Value = "4", DisplayName = "ESTADO 4" };
					[FunctionalPoint("Value[4];DisplayName[ESTADO 4]")]
					public static DomainKeyPair ESTADO_4 { get { return _ESTADO_4; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_PAIS
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "PAIS 1"); 
				    
					result.Add("2", "PAIS 2"); 
				    
					result.Add("3", "PAIS 3"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "PAIS_1"); 
				    
					result.Add("2", "PAIS_2"); 
				    
					result.Add("3", "PAIS_3"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _PAIS_1 = new DomainKeyPair() { Value = "1", DisplayName = "PAIS 1" };
					[FunctionalPoint("Value[1];DisplayName[PAIS 1]")]
					public static DomainKeyPair PAIS_1 { get { return _PAIS_1; } }
				    
					private static DomainKeyPair _PAIS_2 = new DomainKeyPair() { Value = "2", DisplayName = "PAIS 2" };
					[FunctionalPoint("Value[2];DisplayName[PAIS 2]")]
					public static DomainKeyPair PAIS_2 { get { return _PAIS_2; } }
				    
					private static DomainKeyPair _PAIS_3 = new DomainKeyPair() { Value = "3", DisplayName = "PAIS 3" };
					[FunctionalPoint("Value[3];DisplayName[PAIS 3]")]
					public static DomainKeyPair PAIS_3 { get { return _PAIS_3; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_VENDA
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "VENDA 1"); 
				    
					result.Add("2", "VENDA 2"); 
				    
					result.Add("3", "VENDA 3"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "VENDA_1"); 
				    
					result.Add("2", "VENDA_2"); 
				    
					result.Add("3", "VENDA_3"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _VENDA_1 = new DomainKeyPair() { Value = "1", DisplayName = "VENDA 1" };
					[FunctionalPoint("Value[1];DisplayName[VENDA 1]")]
					public static DomainKeyPair VENDA_1 { get { return _VENDA_1; } }
				    
					private static DomainKeyPair _VENDA_2 = new DomainKeyPair() { Value = "2", DisplayName = "VENDA 2" };
					[FunctionalPoint("Value[2];DisplayName[VENDA 2]")]
					public static DomainKeyPair VENDA_2 { get { return _VENDA_2; } }
				    
					private static DomainKeyPair _VENDA_3 = new DomainKeyPair() { Value = "3", DisplayName = "VENDA 3" };
					[FunctionalPoint("Value[3];DisplayName[VENDA 3]")]
					public static DomainKeyPair VENDA_3 { get { return _VENDA_3; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_VENDEDOR
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "VENDEDOR 1"); 
				    
					result.Add("2", "VENDEDOR 2"); 
				    
					result.Add("3", "VENDEDOR 3"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "VENDEDOR_1"); 
				    
					result.Add("2", "VENDEDOR_2"); 
				    
					result.Add("3", "VENDEDOR_3"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _VENDEDOR_1 = new DomainKeyPair() { Value = "1", DisplayName = "VENDEDOR 1" };
					[FunctionalPoint("Value[1];DisplayName[VENDEDOR 1]")]
					public static DomainKeyPair VENDEDOR_1 { get { return _VENDEDOR_1; } }
				    
					private static DomainKeyPair _VENDEDOR_2 = new DomainKeyPair() { Value = "2", DisplayName = "VENDEDOR 2" };
					[FunctionalPoint("Value[2];DisplayName[VENDEDOR 2]")]
					public static DomainKeyPair VENDEDOR_2 { get { return _VENDEDOR_2; } }
				    
					private static DomainKeyPair _VENDEDOR_3 = new DomainKeyPair() { Value = "3", DisplayName = "VENDEDOR 3" };
					[FunctionalPoint("Value[3];DisplayName[VENDEDOR 3]")]
					public static DomainKeyPair VENDEDOR_3 { get { return _VENDEDOR_3; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_FORMA_PAGAMENTO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "FORMA PAGAMENTO 1"); 
				    
					result.Add("2", "FORMA PAGAMENTO 2"); 
				    
					result.Add("3", "FORMA PAGAMENTO 3"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "FORMA_PAGAMENTO_1"); 
				    
					result.Add("2", "FORMA_PAGAMENTO_2"); 
				    
					result.Add("3", "FORMA_PAGAMENTO_3"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _FORMA_PAGAMENTO_1 = new DomainKeyPair() { Value = "1", DisplayName = "FORMA PAGAMENTO 1" };
					[FunctionalPoint("Value[1];DisplayName[FORMA PAGAMENTO 1]")]
					public static DomainKeyPair FORMA_PAGAMENTO_1 { get { return _FORMA_PAGAMENTO_1; } }
				    
					private static DomainKeyPair _FORMA_PAGAMENTO_2 = new DomainKeyPair() { Value = "2", DisplayName = "FORMA PAGAMENTO 2" };
					[FunctionalPoint("Value[2];DisplayName[FORMA PAGAMENTO 2]")]
					public static DomainKeyPair FORMA_PAGAMENTO_2 { get { return _FORMA_PAGAMENTO_2; } }
				    
					private static DomainKeyPair _FORMA_PAGAMENTO_3 = new DomainKeyPair() { Value = "3", DisplayName = "FORMA PAGAMENTO 3" };
					[FunctionalPoint("Value[3];DisplayName[FORMA PAGAMENTO 3]")]
					public static DomainKeyPair FORMA_PAGAMENTO_3 { get { return _FORMA_PAGAMENTO_3; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_CLIENTE
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "CLIENTE 1"); 
				    
					result.Add("2", "CLIENTE 2"); 
				    
					result.Add("3", "CLIENTE 3"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "CLIENTE_1"); 
				    
					result.Add("2", "CLIENTE_2"); 
				    
					result.Add("3", "CLIENTE_3"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _CLIENTE_1 = new DomainKeyPair() { Value = "1", DisplayName = "CLIENTE 1" };
					[FunctionalPoint("Value[1];DisplayName[CLIENTE 1]")]
					public static DomainKeyPair CLIENTE_1 { get { return _CLIENTE_1; } }
				    
					private static DomainKeyPair _CLIENTE_2 = new DomainKeyPair() { Value = "2", DisplayName = "CLIENTE 2" };
					[FunctionalPoint("Value[2];DisplayName[CLIENTE 2]")]
					public static DomainKeyPair CLIENTE_2 { get { return _CLIENTE_2; } }
				    
					private static DomainKeyPair _CLIENTE_3 = new DomainKeyPair() { Value = "3", DisplayName = "CLIENTE 3" };
					[FunctionalPoint("Value[3];DisplayName[CLIENTE 3]")]
					public static DomainKeyPair CLIENTE_3 { get { return _CLIENTE_3; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_VENDA_ITEM
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "VENDA ITEM 1"); 
				    
					result.Add("2", "VENDA ITEM 2"); 
				    
					result.Add("3", "VENDA ITEM 3"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "VENDA_ITEM_1"); 
				    
					result.Add("2", "VENDA_ITEM_2"); 
				    
					result.Add("3", "VENDA_ITEM_3"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _VENDA_ITEM_1 = new DomainKeyPair() { Value = "1", DisplayName = "VENDA ITEM 1" };
					[FunctionalPoint("Value[1];DisplayName[VENDA ITEM 1]")]
					public static DomainKeyPair VENDA_ITEM_1 { get { return _VENDA_ITEM_1; } }
				    
					private static DomainKeyPair _VENDA_ITEM_2 = new DomainKeyPair() { Value = "2", DisplayName = "VENDA ITEM 2" };
					[FunctionalPoint("Value[2];DisplayName[VENDA ITEM 2]")]
					public static DomainKeyPair VENDA_ITEM_2 { get { return _VENDA_ITEM_2; } }
				    
					private static DomainKeyPair _VENDA_ITEM_3 = new DomainKeyPair() { Value = "3", DisplayName = "VENDA ITEM 3" };
					[FunctionalPoint("Value[3];DisplayName[VENDA ITEM 3]")]
					public static DomainKeyPair VENDA_ITEM_3 { get { return _VENDA_ITEM_3; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_VENDA_PAI
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("A", "VENDA 1"); 
				    
					result.Add("B", "VENDA 2"); 
				    
					result.Add("C", "VENDA 3"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("A", "VENDA_1"); 
				    
					result.Add("B", "VENDA_2"); 
				    
					result.Add("C", "VENDA_3"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _VENDA_1 = new DomainKeyPair() { Value = "A", DisplayName = "VENDA 1" };
					[FunctionalPoint("Value[\"A\"];DisplayName[VENDA 1]")]
					public static DomainKeyPair VENDA_1 { get { return _VENDA_1; } }
				    
					private static DomainKeyPair _VENDA_2 = new DomainKeyPair() { Value = "B", DisplayName = "VENDA 2" };
					[FunctionalPoint("Value[\"B\"];DisplayName[VENDA 2]")]
					public static DomainKeyPair VENDA_2 { get { return _VENDA_2; } }
				    
					private static DomainKeyPair _VENDA_3 = new DomainKeyPair() { Value = "C", DisplayName = "VENDA 3" };
					[FunctionalPoint("Value[\"C\"];DisplayName[VENDA 3]")]
					public static DomainKeyPair VENDA_3 { get { return _VENDA_3; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_VENDA_FILHA
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("A", "VENDA ITEM 1"); 
				    
					result.Add("B", "VENDA ITEM 2"); 
				    
					result.Add("C", "VENDA ITEM 3"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("A", "VENDA_ITEM_1"); 
				    
					result.Add("B", "VENDA_ITEM_2"); 
				    
					result.Add("C", "VENDA_ITEM_3"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _VENDA_ITEM_1 = new DomainKeyPair() { Value = "A", DisplayName = "VENDA ITEM 1" };
					[FunctionalPoint("Value['A'];DisplayName[VENDA ITEM 1]")]
					public static DomainKeyPair VENDA_ITEM_1 { get { return _VENDA_ITEM_1; } }
				    
					private static DomainKeyPair _VENDA_ITEM_2 = new DomainKeyPair() { Value = "B", DisplayName = "VENDA ITEM 2" };
					[FunctionalPoint("Value['B'];DisplayName[VENDA ITEM 2]")]
					public static DomainKeyPair VENDA_ITEM_2 { get { return _VENDA_ITEM_2; } }
				    
					private static DomainKeyPair _VENDA_ITEM_3 = new DomainKeyPair() { Value = "C", DisplayName = "VENDA ITEM 3" };
					[FunctionalPoint("Value['C'];DisplayName[VENDA ITEM 3]")]
					public static DomainKeyPair VENDA_ITEM_3 { get { return _VENDA_ITEM_3; } }
				    
			#endregion properties

		

	}    
			
    public partial class LX_VENDA_ATACADO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "VENDA 1"); 
				    
					result.Add("2", "VENDA 2"); 
				    
					result.Add("3", "VENDA 3"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "VENDA_1"); 
				    
					result.Add("2", "VENDA_2"); 
				    
					result.Add("3", "VENDA_3"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _VENDA_1 = new DomainKeyPair() { Value = "1", DisplayName = "VENDA 1" };
					[FunctionalPoint("Value[1];DisplayName[VENDA 1]")]
					public static DomainKeyPair VENDA_1 { get { return _VENDA_1; } }
				    
					private static DomainKeyPair _VENDA_2 = new DomainKeyPair() { Value = "2", DisplayName = "VENDA 2" };
					[FunctionalPoint("Value[2];DisplayName[VENDA 2]")]
					public static DomainKeyPair VENDA_2 { get { return _VENDA_2; } }
				    
					private static DomainKeyPair _VENDA_3 = new DomainKeyPair() { Value = "3", DisplayName = "VENDA 3" };
					[FunctionalPoint("Value[3];DisplayName[VENDA 3]")]
					public static DomainKeyPair VENDA_3 { get { return _VENDA_3; } }
				    
			#endregion properties

		

	}    

}