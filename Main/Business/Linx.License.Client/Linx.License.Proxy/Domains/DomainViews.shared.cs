	

using System;
using System.IO;
using System.Collections.Generic;
using Linx.Tools; 
    
namespace Linx.License.Client.Domains
{

			
    public partial class STATUS_CHAVE
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Licença ativa"); 
				    
					result.Add("2", "Licença pendente"); 
				    
					result.Add("3", "Licença revogada"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "ATIVO"); 
				    
					result.Add("2", "PENDENTE"); 
				    
					result.Add("3", "REVOGADO"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _ATIVO = new DomainKeyPair() { Value = "1", DisplayName = "Licença ativa" };
					[FunctionalPoint("Value[1];DisplayName[Licença ativa]")]
					public static DomainKeyPair ATIVO { get { return _ATIVO; } }
				    
					private static DomainKeyPair _PENDENTE = new DomainKeyPair() { Value = "2", DisplayName = "Licença pendente" };
					[FunctionalPoint("Value[2];DisplayName[Licença pendente]")]
					public static DomainKeyPair PENDENTE { get { return _PENDENTE; } }
				    
					private static DomainKeyPair _REVOGADO = new DomainKeyPair() { Value = "3", DisplayName = "Licença revogada" };
					[FunctionalPoint("Value[3];DisplayName[Licença revogada]")]
					public static DomainKeyPair REVOGADO { get { return _REVOGADO; } }
				    
			#endregion properties

		

	}    
			
    public partial class RETORNO_TIPO
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Ok"); 
				    
					result.Add("2", "Alerta"); 
				    
					result.Add("3", "Erro"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Ok"); 
				    
					result.Add("2", "Alerta"); 
				    
					result.Add("3", "Erro"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Ok = new DomainKeyPair() { Value = "1", DisplayName = "Ok" };
					[FunctionalPoint("Value[1];DisplayName[Ok]")]
					public static DomainKeyPair Ok { get { return _Ok; } }
				    
					private static DomainKeyPair _Alerta = new DomainKeyPair() { Value = "2", DisplayName = "Alerta" };
					[FunctionalPoint("Value[2];DisplayName[Alerta]")]
					public static DomainKeyPair Alerta { get { return _Alerta; } }
				    
					private static DomainKeyPair _Erro = new DomainKeyPair() { Value = "3", DisplayName = "Erro" };
					[FunctionalPoint("Value[3];DisplayName[Erro]")]
					public static DomainKeyPair Erro { get { return _Erro; } }
				    
			#endregion properties

		

	}    

}