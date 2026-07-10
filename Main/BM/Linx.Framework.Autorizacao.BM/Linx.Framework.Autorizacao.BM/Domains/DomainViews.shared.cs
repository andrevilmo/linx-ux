								

using System;
using System.IO;
using System.Collections.Generic;
using Linx.Tools; 
    
namespace Linx.Framework.Autorizacao.BM.Domains
{

			
    public partial class TipoMensagem
    {
				
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Informação"); 
				    
					result.Add("2", "Alerta"); 
				    
					result.Add("3", "Erro"); 
				    
					result.Add("4", "Sucesso"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Info"); 
				    
					result.Add("2", "Warning"); 
				    
					result.Add("3", "Error"); 
				    
					result.Add("4", "Success"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Info = new DomainKeyPair() { Value = "1", DisplayName = "Informação" };
					[FunctionalPoint("Value[1];DisplayName[Informação]")]
					public static DomainKeyPair Info { get { return _Info; } }
				    
					private static DomainKeyPair _Warning = new DomainKeyPair() { Value = "2", DisplayName = "Alerta" };
					[FunctionalPoint("Value[2];DisplayName[Alerta]")]
					public static DomainKeyPair Warning { get { return _Warning; } }
				    
					private static DomainKeyPair _Error = new DomainKeyPair() { Value = "3", DisplayName = "Erro" };
					[FunctionalPoint("Value[3];DisplayName[Erro]")]
					public static DomainKeyPair Error { get { return _Error; } }
				    
					private static DomainKeyPair _Success = new DomainKeyPair() { Value = "4", DisplayName = "Sucesso" };
					[FunctionalPoint("Value[4];DisplayName[Sucesso]")]
					public static DomainKeyPair Success { get { return _Success; } }
				    
			#endregion properties

		

	}    

}