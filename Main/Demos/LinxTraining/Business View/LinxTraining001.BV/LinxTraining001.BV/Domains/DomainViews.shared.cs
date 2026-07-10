											

using System;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using Linx.Tools; 
    
namespace LinxTraining001.BV.Domains
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
            values = GetDomainValues("LXOrigem");
            if (values.Count > 0)
            {
            	result.Add("LXOrigem", values);                    
            }
            values = GetDomainValues("LXTipoClientes");
            if (values.Count > 0)
            {
            	result.Add("LXTipoClientes", values);                    
            }
            values = GetDomainValues("TstDomainString");
            if (values.Count > 0)
            {
            	result.Add("TstDomainString", values);                    
            }
            values = GetDomainValues("ProdutoDomain");
            if (values.Count > 0)
            {
            	result.Add("ProdutoDomain", values);                    
            }
            values = GetDomainValues("DomainString");
            if (values.Count > 0)
            {
            	result.Add("DomainString", values);                    
            }
            values = GetDomainValues("tstCombo");
            if (values.Count > 0)
            {
            	result.Add("tstCombo", values);                    
            }
            return result;
        }

        public static Dictionary<string, string> GetDomainValues(string domainName)
        {
            Dictionary<string, string> result;
            switch (domainName)
            {


                case "LXOrigem":
                    result = LXOrigem.GetValues();
                    break;

                case "LXTipoClientes":
                    result = LXTipoClientes.GetValues();
                    break;

                case "TstDomainString":
                    result = TstDomainString.GetValues();
                    break;

                case "ProdutoDomain":
                    result = ProdutoDomain.GetValues();
                    break;

                case "DomainString":
                    result = DomainString.GetValues();
                    break;

                case "tstCombo":
                    result = tstCombo.GetValues();
                    break;

                default:
                    result = new Dictionary<string, string>();
                    break;
            }

            return result;
        }
    }

	//<LXOrigem>((#LxExpr#) == [-1-] ? "Internet" : ((#LxExpr#) == [-2-] ? "Loja Física" : ""))</LXOrigem>	
    public partial class LXOrigem
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Internet"); 
				    
					result.Add("2", "Loja Física"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Internet"); 
				    
					result.Add("2", "Lojafisica"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Internet = new DomainKeyPair() { Value = "1", DisplayName = "Internet" };
					[FunctionalPoint("Value[1];DisplayName[Internet]")]
					public static DomainKeyPair Internet { get { return _Internet; } }
				    
					private static DomainKeyPair _Lojafisica = new DomainKeyPair() { Value = "2", DisplayName = "Loja Física" };
					[FunctionalPoint("Value[2];DisplayName[Loja Física]")]
					public static DomainKeyPair Lojafisica { get { return _Lojafisica; } }
				    
			#endregion properties

			

	}    
	//<LXTipoClientes>((#LxExpr#) == [-3-] ? "Fornecedor" : ((#LxExpr#) == [-1-] ? "Pessoa Física" : ((#LxExpr#) == [-2-] ? "Pessoa Jurídica" : "")))</LXTipoClientes>	
    public partial class LXTipoClientes
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "Fornecedor"); 
				    
					result.Add("1", "Pessoa Física"); 
				    
					result.Add("2", "Pessoa Jurídica"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "Fornecedor"); 
				    
					result.Add("1", "Pessoafisica"); 
				    
					result.Add("2", "Pessoajuridica"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Fornecedor = new DomainKeyPair() { Value = "3", DisplayName = "Fornecedor" };
					[FunctionalPoint("Value[3];DisplayName[Fornecedor]")]
					public static DomainKeyPair Fornecedor { get { return _Fornecedor; } }
				    
					private static DomainKeyPair _Pessoafisica = new DomainKeyPair() { Value = "1", DisplayName = "Pessoa Física" };
					[FunctionalPoint("Value[1];DisplayName[Pessoa Física]")]
					public static DomainKeyPair Pessoafisica { get { return _Pessoafisica; } }
				    
					private static DomainKeyPair _Pessoajuridica = new DomainKeyPair() { Value = "2", DisplayName = "Pessoa Jurídica" };
					[FunctionalPoint("Value[2];DisplayName[Pessoa Jurídica]")]
					public static DomainKeyPair Pessoajuridica { get { return _Pessoajuridica; } }
				    
			#endregion properties

			

	}    
	//<TstDomainString>((#LxExpr#) == [-01-] ? "String 01" : ((#LxExpr#) == [-01A-] ? "String 01A" : ((#LxExpr#) == [-02-] ? "String 02" : ((#LxExpr#) == [-A-] ? "String A" : ((#LxExpr#) == [-ststdd-] ? "NewString" : ((#LxExpr#) == [-sttst-] ? "String Teste" : ((#LxExpr#) == [-ValString-] ? "ValString" : "")))))))</TstDomainString>	
    public partial class TstDomainString
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("01", "String 01"); 
				    
					result.Add("01A", "String 01A"); 
				    
					result.Add("02", "String 02"); 
				    
					result.Add("A", "String A"); 
				    
					result.Add("ststdd", "NewString"); 
				    
					result.Add("sttst", "String Teste"); 
				    
					result.Add("ValString", "ValString"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("01", "st01"); 
				    
					result.Add("01A", "st01A"); 
				    
					result.Add("02", "st02"); 
				    
					result.Add("A", "stA"); 
				    
					result.Add("ststdd", "ststdd"); 
				    
					result.Add("sttst", "sttst"); 
				    
					result.Add("ValString", "ValString"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _st01 = new DomainKeyPair() { Value = "01", DisplayName = "String 01" };
					[FunctionalPoint("Value['01'];DisplayName[String 01]")]
					public static DomainKeyPair st01 { get { return _st01; } }
				    
					private static DomainKeyPair _st01A = new DomainKeyPair() { Value = "01A", DisplayName = "String 01A" };
					[FunctionalPoint("Value['01A'];DisplayName[String 01A]")]
					public static DomainKeyPair st01A { get { return _st01A; } }
				    
					private static DomainKeyPair _st02 = new DomainKeyPair() { Value = "02", DisplayName = "String 02" };
					[FunctionalPoint("Value['02'];DisplayName[String 02]")]
					public static DomainKeyPair st02 { get { return _st02; } }
				    
					private static DomainKeyPair _stA = new DomainKeyPair() { Value = "A", DisplayName = "String A" };
					[FunctionalPoint("Value[\"A\"];DisplayName[String A]")]
					public static DomainKeyPair stA { get { return _stA; } }
				    
					private static DomainKeyPair _ststdd = new DomainKeyPair() { Value = "ststdd", DisplayName = "NewString" };
					[FunctionalPoint("Value[\"ststdd\"];DisplayName[NewString]")]
					public static DomainKeyPair ststdd { get { return _ststdd; } }
				    
					private static DomainKeyPair _sttst = new DomainKeyPair() { Value = "sttst", DisplayName = "String Teste" };
					[FunctionalPoint("Value[\"sttst\"];DisplayName[String Teste]")]
					public static DomainKeyPair sttst { get { return _sttst; } }
				    
					private static DomainKeyPair _ValString = new DomainKeyPair() { Value = "ValString", DisplayName = "ValString" };
					[FunctionalPoint("Value[\"ValString\"];DisplayName[ValString]")]
					public static DomainKeyPair ValString { get { return _ValString; } }
				    
			#endregion properties

			

	}    
	//<ProdutoDomain>((#LxExpr#) == [-Item1-] ? "PRODUTO A" : ((#LxExpr#) == [-Item2-] ? "PRODUTO B" : ((#LxExpr#) == [-Item3-] ? "PRODUTO C" : ((#LxExpr#) == [-Item4-] ? "PRODUTO D" : ""))))</ProdutoDomain>	
    public partial class ProdutoDomain
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("Item1", "PRODUTO A"); 
				    
					result.Add("Item2", "PRODUTO B"); 
				    
					result.Add("Item3", "PRODUTO C"); 
				    
					result.Add("Item4", "PRODUTO D"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("Item1", "PRODUTOA"); 
				    
					result.Add("Item2", "PRODUTOB"); 
				    
					result.Add("Item3", "PRODUTOC"); 
				    
					result.Add("Item4", "PRODUTOD"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _PRODUTOA = new DomainKeyPair() { Value = "Item1", DisplayName = "PRODUTO A" };
					[FunctionalPoint("Value[\"Item1\"];DisplayName[PRODUTO A]")]
					public static DomainKeyPair PRODUTOA { get { return _PRODUTOA; } }
				    
					private static DomainKeyPair _PRODUTOB = new DomainKeyPair() { Value = "Item2", DisplayName = "PRODUTO B" };
					[FunctionalPoint("Value[\"Item2\"];DisplayName[PRODUTO B]")]
					public static DomainKeyPair PRODUTOB { get { return _PRODUTOB; } }
				    
					private static DomainKeyPair _PRODUTOC = new DomainKeyPair() { Value = "Item3", DisplayName = "PRODUTO C" };
					[FunctionalPoint("Value[\"Item3\"];DisplayName[PRODUTO C]")]
					public static DomainKeyPair PRODUTOC { get { return _PRODUTOC; } }
				    
					private static DomainKeyPair _PRODUTOD = new DomainKeyPair() { Value = "Item4", DisplayName = "PRODUTO D" };
					[FunctionalPoint("Value[\"Item4\"];DisplayName[PRODUTO D]")]
					public static DomainKeyPair PRODUTOD { get { return _PRODUTOD; } }
				    
			#endregion properties

			

	}    
	//<DomainString>((#LxExpr#) == [-05-] ? "String 05" : ((#LxExpr#) == [-06-] ? "String 06" : ((#LxExpr#) == [-B-] ? "String B" : ((#LxExpr#) == [-01B-] ? "String 01B" : ((#LxExpr#) == [-ststt-] ? "Teste String" : "")))))</DomainString>	
    public partial class DomainString
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("05", "String 05"); 
				    
					result.Add("06", "String 06"); 
				    
					result.Add("B", "String B"); 
				    
					result.Add("01B", "String 01B"); 
				    
					result.Add("ststt", "Teste String"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("05", "st05"); 
				    
					result.Add("06", "st06"); 
				    
					result.Add("B", "stB"); 
				    
					result.Add("01B", "st01B"); 
				    
					result.Add("ststt", "ststt"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _st05 = new DomainKeyPair() { Value = "05", DisplayName = "String 05" };
					[FunctionalPoint("Value['05'];DisplayName[String 05]")]
					public static DomainKeyPair st05 { get { return _st05; } }
				    
					private static DomainKeyPair _st06 = new DomainKeyPair() { Value = "06", DisplayName = "String 06" };
					[FunctionalPoint("Value['06'];DisplayName[String 06]")]
					public static DomainKeyPair st06 { get { return _st06; } }
				    
					private static DomainKeyPair _stB = new DomainKeyPair() { Value = "B", DisplayName = "String B" };
					[FunctionalPoint("Value[\"B\"];DisplayName[String B]")]
					public static DomainKeyPair stB { get { return _stB; } }
				    
					private static DomainKeyPair _st01B = new DomainKeyPair() { Value = "01B", DisplayName = "String 01B" };
					[FunctionalPoint("Value['01B'];DisplayName[String 01B]")]
					public static DomainKeyPair st01B { get { return _st01B; } }
				    
					private static DomainKeyPair _ststt = new DomainKeyPair() { Value = "ststt", DisplayName = "Teste String" };
					[FunctionalPoint("Value[\"ststt\"];DisplayName[Teste String]")]
					public static DomainKeyPair ststt { get { return _ststt; } }
				    
			#endregion properties

			

	}    
	//<tstCombo>((#LxExpr#) == [-1-] ? "Teste1" : ((#LxExpr#) == [-2-] ? "Teste2" : ((#LxExpr#) == [-3-] ? "Teste3" : "")))</tstCombo>	
    public partial class tstCombo
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Teste1"); 
				    
					result.Add("2", "Teste2"); 
				    
					result.Add("3", "Teste3"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "teste1"); 
				    
					result.Add("2", "teste2"); 
				    
					result.Add("3", "teste3"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _teste1 = new DomainKeyPair() { Value = "1", DisplayName = "Teste1" };
					[FunctionalPoint("Value[1];DisplayName[Teste1]")]
					public static DomainKeyPair teste1 { get { return _teste1; } }
				    
					private static DomainKeyPair _teste2 = new DomainKeyPair() { Value = "2", DisplayName = "Teste2" };
					[FunctionalPoint("Value[2];DisplayName[Teste2]")]
					public static DomainKeyPair teste2 { get { return _teste2; } }
				    
					private static DomainKeyPair _teste3 = new DomainKeyPair() { Value = "3", DisplayName = "Teste3" };
					[FunctionalPoint("Value[3];DisplayName[Teste3]")]
					public static DomainKeyPair teste3 { get { return _teste3; } }
				    
			#endregion properties

			

	}    

}