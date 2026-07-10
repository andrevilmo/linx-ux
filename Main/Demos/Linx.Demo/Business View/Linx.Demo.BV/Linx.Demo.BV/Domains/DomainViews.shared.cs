							

using System;
using System.IO;
using System.Collections.Generic;
using Linx.Tools; 
    
namespace Linx.Demo.BV.Domains
{

	public partial class DomainHelper
    {
		public static string[] GetDomainsInfo(string domainNames)
        {
            List<string> result = new List<string>();

            foreach (string domainName in domainNames.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var values = GetDomainValues(domainName);
                if (values.Count > 0)
                {
                    foreach(var value in values)
                    {
                        result.Add(domainName + "#" + value.Key + "#" + value.Value.Replace("\"", "").Replace("'", ""));
                    }
                }
            }

            return result.ToArray();
        }

		public static Dictionary<string, Dictionary<string, string>> GetAllDomainsInfo()
        {
            Dictionary<string, Dictionary<string, string>> result = new Dictionary<string, Dictionary<string, string>>();
			Dictionary<string, string> values;
            values = GetDomainValues("LX_COMBOBOX_LOJA");
            if (values.Count > 0)
            {
            	result.Add("LX_COMBOBOX_LOJA", values);                    
            }
            values = GetDomainValues("LX_VENDA_ITEM");
            if (values.Count > 0)
            {
            	result.Add("LX_VENDA_ITEM", values);                    
            }
            values = GetDomainValues("LX_COMBOBOX_CIDADE");
            if (values.Count > 0)
            {
            	result.Add("LX_COMBOBOX_CIDADE", values);                    
            }
            values = GetDomainValues("LX_COMBOBOX_ESTADO");
            if (values.Count > 0)
            {
            	result.Add("LX_COMBOBOX_ESTADO", values);                    
            }
            values = GetDomainValues("LX_COMBOBOX_PAIS");
            if (values.Count > 0)
            {
            	result.Add("LX_COMBOBOX_PAIS", values);                    
            }
            values = GetDomainValues("LX_COMBOBOX_MARCA");
            if (values.Count > 0)
            {
            	result.Add("LX_COMBOBOX_MARCA", values);                    
            }
            values = GetDomainValues("LX_REPRESENTANTE");
            if (values.Count > 0)
            {
            	result.Add("LX_REPRESENTANTE", values);                    
            }
            values = GetDomainValues("LX_REGIAO");
            if (values.Count > 0)
            {
            	result.Add("LX_REGIAO", values);                    
            }
            values = GetDomainValues("LX_VENDA");
            if (values.Count > 0)
            {
            	result.Add("LX_VENDA", values);                    
            }
            values = GetDomainValues("LX_FORMA_PAGAMENTO");
            if (values.Count > 0)
            {
            	result.Add("LX_FORMA_PAGAMENTO", values);                    
            }
            values = GetDomainValues("LX_VENDEDOR");
            if (values.Count > 0)
            {
            	result.Add("LX_VENDEDOR", values);                    
            }
            values = GetDomainValues("LX_CODIGO_FISCAL");
            if (values.Count > 0)
            {
            	result.Add("LX_CODIGO_FISCAL", values);                    
            }
            values = GetDomainValues("LX_PRODUTO");
            if (values.Count > 0)
            {
            	result.Add("LX_PRODUTO", values);                    
            }
            return result;
        }

        public static Dictionary<string, string> GetDomainValues(string domainName)
        {
            Dictionary<string, string> result;
            switch (domainName)
            {


                case "LX_COMBOBOX_LOJA":
                    result = LX_COMBOBOX_LOJA.GetValues();
                    break;

                case "LX_VENDA_ITEM":
                    result = LX_VENDA_ITEM.GetValues();
                    break;

                case "LX_COMBOBOX_CIDADE":
                    result = LX_COMBOBOX_CIDADE.GetValues();
                    break;

                case "LX_COMBOBOX_ESTADO":
                    result = LX_COMBOBOX_ESTADO.GetValues();
                    break;

                case "LX_COMBOBOX_PAIS":
                    result = LX_COMBOBOX_PAIS.GetValues();
                    break;

                case "LX_COMBOBOX_MARCA":
                    result = LX_COMBOBOX_MARCA.GetValues();
                    break;

                case "LX_REPRESENTANTE":
                    result = LX_REPRESENTANTE.GetValues();
                    break;

                case "LX_REGIAO":
                    result = LX_REGIAO.GetValues();
                    break;

                case "LX_VENDA":
                    result = LX_VENDA.GetValues();
                    break;

                case "LX_FORMA_PAGAMENTO":
                    result = LX_FORMA_PAGAMENTO.GetValues();
                    break;

                case "LX_VENDEDOR":
                    result = LX_VENDEDOR.GetValues();
                    break;

                case "LX_CODIGO_FISCAL":
                    result = LX_CODIGO_FISCAL.GetValues();
                    break;

                case "LX_PRODUTO":
                    result = LX_PRODUTO.GetValues();
                    break;

                default:
                    result = new Dictionary<string, string>();
                    break;
            }

            return result;
        }
    }

	//<LX_COMBOBOX_LOJA>((#LxExpr#) == [-1-] ? "LOJA1" : ((#LxExpr#) == [-2-] ? "LOJA2" : ((#LxExpr#) == [-3-] ? "LOJA3" : ((#LxExpr#) == [-4-] ? "LOJA4" : ""))))</LX_COMBOBOX_LOJA>	
    public partial class LX_COMBOBOX_LOJA
    {
					
			private static Dictionary<string, string> domainValues;
			public static Dictionary<string, string> GetValues()
			{
					if (domainValues == null)
					{

						domainValues = new Dictionary<string, string>();				
				
						domainValues.Add("1", "LOJA1"); 
						
						domainValues.Add("2", "LOJA2"); 
						
						domainValues.Add("3", "LOJA3"); 
						
						domainValues.Add("4", "LOJA4"); 
						
					}
					return domainValues;
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
	//<LX_VENDA_ITEM>((#LxExpr#) == [-1-] ? "VENDA_ITEM1" : ((#LxExpr#) == [-2-] ? "VENDA_ITEM2" : ((#LxExpr#) == [-3-] ? "VENDA_ITEM3" : ((#LxExpr#) == [-4-] ? "VENDA_ITEM4" : ""))))</LX_VENDA_ITEM>	
    public partial class LX_VENDA_ITEM
    {
					
			private static Dictionary<string, string> domainValues;
			public static Dictionary<string, string> GetValues()
			{
					if (domainValues == null)
					{

						domainValues = new Dictionary<string, string>();				
				
						domainValues.Add("1", "VENDA_ITEM1"); 
						
						domainValues.Add("2", "VENDA_ITEM2"); 
						
						domainValues.Add("3", "VENDA_ITEM3"); 
						
						domainValues.Add("4", "VENDA_ITEM4"); 
						
					}
					return domainValues;
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
	//<LX_COMBOBOX_CIDADE>((#LxExpr#) == [-1-] ? "CIDADE1" : ((#LxExpr#) == [-2-] ? "CIDADE2" : ((#LxExpr#) == [-3-] ? "CIDADE3" : "")))</LX_COMBOBOX_CIDADE>	
    public partial class LX_COMBOBOX_CIDADE
    {
					
			private static Dictionary<string, string> domainValues;
			public static Dictionary<string, string> GetValues()
			{
					if (domainValues == null)
					{

						domainValues = new Dictionary<string, string>();				
				
						domainValues.Add("1", "CIDADE1"); 
						
						domainValues.Add("2", "CIDADE2"); 
						
						domainValues.Add("3", "CIDADE3"); 
						
					}
					return domainValues;
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
	//<LX_COMBOBOX_ESTADO>((#LxExpr#) == [-1-] ? "ESTADO1" : ((#LxExpr#) == [-2-] ? "ESTADO2" : ((#LxExpr#) == [-3-] ? "ESTADO3" : ((#LxExpr#) == [-4-] ? "ESTADO4" : ""))))</LX_COMBOBOX_ESTADO>	
    public partial class LX_COMBOBOX_ESTADO
    {
					
			private static Dictionary<string, string> domainValues;
			public static Dictionary<string, string> GetValues()
			{
					if (domainValues == null)
					{

						domainValues = new Dictionary<string, string>();				
				
						domainValues.Add("1", "ESTADO1"); 
						
						domainValues.Add("2", "ESTADO2"); 
						
						domainValues.Add("3", "ESTADO3"); 
						
						domainValues.Add("4", "ESTADO4"); 
						
					}
					return domainValues;
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
	//<LX_COMBOBOX_PAIS>((#LxExpr#) == [-1-] ? "PAIS1" : ((#LxExpr#) == [-2-] ? "PAIS2" : ((#LxExpr#) == [-3-] ? "PAIS3" : "")))</LX_COMBOBOX_PAIS>	
    public partial class LX_COMBOBOX_PAIS
    {
					
			private static Dictionary<string, string> domainValues;
			public static Dictionary<string, string> GetValues()
			{
					if (domainValues == null)
					{

						domainValues = new Dictionary<string, string>();				
				
						domainValues.Add("1", "PAIS1"); 
						
						domainValues.Add("2", "PAIS2"); 
						
						domainValues.Add("3", "PAIS3"); 
						
					}
					return domainValues;
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
	//<LX_COMBOBOX_MARCA>((#LxExpr#) == [-1-] ? "MARCA1" : ((#LxExpr#) == [-2-] ? "MARCA2" : ((#LxExpr#) == [-3-] ? "MARCA3" : "")))</LX_COMBOBOX_MARCA>	
    public partial class LX_COMBOBOX_MARCA
    {
					
			private static Dictionary<string, string> domainValues;
			public static Dictionary<string, string> GetValues()
			{
					if (domainValues == null)
					{

						domainValues = new Dictionary<string, string>();				
				
						domainValues.Add("1", "MARCA1"); 
						
						domainValues.Add("2", "MARCA2"); 
						
						domainValues.Add("3", "MARCA3"); 
						
					}
					return domainValues;
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
	//<LX_REPRESENTANTE>((#LxExpr#) == [-1-] ? "REPRESENTANTE1" : ((#LxExpr#) == [-2-] ? "REPRESENTANTE2" : ""))</LX_REPRESENTANTE>	
    public partial class LX_REPRESENTANTE
    {
					
			private static Dictionary<string, string> domainValues;
			public static Dictionary<string, string> GetValues()
			{
					if (domainValues == null)
					{

						domainValues = new Dictionary<string, string>();				
				
						domainValues.Add("1", "REPRESENTANTE1"); 
						
						domainValues.Add("2", "REPRESENTANTE2"); 
						
					}
					return domainValues;
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
	//<LX_REGIAO>((#LxExpr#) == [-1-] ? "REGIAO1" : ((#LxExpr#) == [-2-] ? "REGIAO2" : ((#LxExpr#) == [-3-] ? "REGIAO3" : "")))</LX_REGIAO>	
    public partial class LX_REGIAO
    {
					
			private static Dictionary<string, string> domainValues;
			public static Dictionary<string, string> GetValues()
			{
					if (domainValues == null)
					{

						domainValues = new Dictionary<string, string>();				
				
						domainValues.Add("1", "REGIAO1"); 
						
						domainValues.Add("2", "REGIAO2"); 
						
						domainValues.Add("3", "REGIAO3"); 
						
					}
					return domainValues;
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
	//<LX_VENDA>((#LxExpr#) == [-1-] ? "VENDA1" : ((#LxExpr#) == [-2-] ? "VENDA2" : ((#LxExpr#) == [-3-] ? "VENDA3" : "")))</LX_VENDA>	
    public partial class LX_VENDA
    {
					
			private static Dictionary<string, string> domainValues;
			public static Dictionary<string, string> GetValues()
			{
					if (domainValues == null)
					{

						domainValues = new Dictionary<string, string>();				
				
						domainValues.Add("1", "VENDA1"); 
						
						domainValues.Add("2", "VENDA2"); 
						
						domainValues.Add("3", "VENDA3"); 
						
					}
					return domainValues;
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
	//<LX_FORMA_PAGAMENTO>((#LxExpr#) == [-1-] ? "PAGAMENTO1" : ((#LxExpr#) == [-2-] ? "PAGAMENTO2" : ((#LxExpr#) == [-3-] ? "PAGAMENTO3" : "")))</LX_FORMA_PAGAMENTO>	
    public partial class LX_FORMA_PAGAMENTO
    {
					
			private static Dictionary<string, string> domainValues;
			public static Dictionary<string, string> GetValues()
			{
					if (domainValues == null)
					{

						domainValues = new Dictionary<string, string>();				
				
						domainValues.Add("1", "PAGAMENTO1"); 
						
						domainValues.Add("2", "PAGAMENTO2"); 
						
						domainValues.Add("3", "PAGAMENTO3"); 
						
					}
					return domainValues;
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
	//<LX_VENDEDOR>((#LxExpr#) == [-1-] ? "VENDEDOR1" : ((#LxExpr#) == [-2-] ? "VENDEDOR2" : ((#LxExpr#) == [-3-] ? "VENDEDOR3" : "")))</LX_VENDEDOR>	
    public partial class LX_VENDEDOR
    {
					
			private static Dictionary<string, string> domainValues;
			public static Dictionary<string, string> GetValues()
			{
					if (domainValues == null)
					{

						domainValues = new Dictionary<string, string>();				
				
						domainValues.Add("1", "VENDEDOR1"); 
						
						domainValues.Add("2", "VENDEDOR2"); 
						
						domainValues.Add("3", "VENDEDOR3"); 
						
					}
					return domainValues;
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
	//<LX_CODIGO_FISCAL>((#LxExpr#) == [-1-] ? "FISCAL1" : ((#LxExpr#) == [-2-] ? "FISCAL2" : ((#LxExpr#) == [-3-] ? "FISCAL3" : "")))</LX_CODIGO_FISCAL>	
    public partial class LX_CODIGO_FISCAL
    {
					
			private static Dictionary<string, string> domainValues;
			public static Dictionary<string, string> GetValues()
			{
					if (domainValues == null)
					{

						domainValues = new Dictionary<string, string>();				
				
						domainValues.Add("1", "FISCAL1"); 
						
						domainValues.Add("2", "FISCAL2"); 
						
						domainValues.Add("3", "FISCAL3"); 
						
					}
					return domainValues;
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
	//<LX_PRODUTO>((#LxExpr#) == [-1-] ? "PRODUTO1" : ((#LxExpr#) == [-2-] ? "PRODUTO2" : ((#LxExpr#) == [-3-] ? "PRODUTO3" : "")))</LX_PRODUTO>	
    public partial class LX_PRODUTO
    {
					
			private static Dictionary<string, string> domainValues;
			public static Dictionary<string, string> GetValues()
			{
					if (domainValues == null)
					{

						domainValues = new Dictionary<string, string>();				
				
						domainValues.Add("1", "PRODUTO1"); 
						
						domainValues.Add("2", "PRODUTO2"); 
						
						domainValues.Add("3", "PRODUTO3"); 
						
					}
					return domainValues;
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