			

using System;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using Linx.Tools; 
    
namespace LinxTraining002.BM.Domains
{

			
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
			
    public partial class LXTipoClientes
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Pessoa Física"); 
				    
					result.Add("2", "Pessoa Jurídica"); 
				    
					result.Add("3", "Fornecedor"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Pessoafisica"); 
				    
					result.Add("2", "Pessoajuridica"); 
				    
					result.Add("3", "Fornecedor"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Pessoafisica = new DomainKeyPair() { Value = "1", DisplayName = "Pessoa Física" };
					[FunctionalPoint("Value[1];DisplayName[Pessoa Física]")]
					public static DomainKeyPair Pessoafisica { get { return _Pessoafisica; } }
				    
					private static DomainKeyPair _Pessoajuridica = new DomainKeyPair() { Value = "2", DisplayName = "Pessoa Jurídica" };
					[FunctionalPoint("Value[2];DisplayName[Pessoa Jurídica]")]
					public static DomainKeyPair Pessoajuridica { get { return _Pessoajuridica; } }
				    
					private static DomainKeyPair _Fornecedor = new DomainKeyPair() { Value = "3", DisplayName = "Fornecedor" };
					[FunctionalPoint("Value[3];DisplayName[Fornecedor]")]
					public static DomainKeyPair Fornecedor { get { return _Fornecedor; } }
				    
			#endregion properties

		

	}    
			
    public partial class TstDomainString
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("01", "String 01"); 
				    
					result.Add("02", "String 02"); 
				    
					result.Add("A", "String A"); 
				    
					result.Add("01A", "String 01A"); 
				    
					result.Add("sttst", "String Teste"); 
				    
					result.Add("ststdd", "NewString"); 
				    
					result.Add("ValString", "ValString"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("01", "st01"); 
				    
					result.Add("02", "st02"); 
				    
					result.Add("A", "stA"); 
				    
					result.Add("01A", "st01A"); 
				    
					result.Add("sttst", "sttst"); 
				    
					result.Add("ststdd", "ststdd"); 
				    
					result.Add("ValString", "ValString"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _st01 = new DomainKeyPair() { Value = "01", DisplayName = "String 01" };
					[FunctionalPoint("Value['01'];DisplayName[String 01]")]
					public static DomainKeyPair st01 { get { return _st01; } }
				    
					private static DomainKeyPair _st02 = new DomainKeyPair() { Value = "02", DisplayName = "String 02" };
					[FunctionalPoint("Value['02'];DisplayName[String 02]")]
					public static DomainKeyPair st02 { get { return _st02; } }
				    
					private static DomainKeyPair _stA = new DomainKeyPair() { Value = "A", DisplayName = "String A" };
					[FunctionalPoint("Value[\"A\"];DisplayName[String A]")]
					public static DomainKeyPair stA { get { return _stA; } }
				    
					private static DomainKeyPair _st01A = new DomainKeyPair() { Value = "01A", DisplayName = "String 01A" };
					[FunctionalPoint("Value['01A'];DisplayName[String 01A]")]
					public static DomainKeyPair st01A { get { return _st01A; } }
				    
					private static DomainKeyPair _sttst = new DomainKeyPair() { Value = "sttst", DisplayName = "String Teste" };
					[FunctionalPoint("Value[\"sttst\"];DisplayName[String Teste]")]
					public static DomainKeyPair sttst { get { return _sttst; } }
				    
					private static DomainKeyPair _ststdd = new DomainKeyPair() { Value = "ststdd", DisplayName = "NewString" };
					[FunctionalPoint("Value[\"ststdd\"];DisplayName[NewString]")]
					public static DomainKeyPair ststdd { get { return _ststdd; } }
				    
					private static DomainKeyPair _ValString = new DomainKeyPair() { Value = "ValString", DisplayName = "ValString" };
					[FunctionalPoint("Value[\"ValString\"];DisplayName[ValString]")]
					public static DomainKeyPair ValString { get { return _ValString; } }
				    
			#endregion properties

		

	}    

}