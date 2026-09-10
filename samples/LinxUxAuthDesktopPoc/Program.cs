using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Linx.Ux.AuthDesktopPoc
{
    public static class Program
    {
        public static async Task<int> Main(string[] args)
        {
            Dictionary<string, string> flags = Parse(args);
            if (flags.ContainsKey("help") || flags.ContainsKey("h") || args.Length == 0)
            {
                PrintHelp();
                return 0;
            }

            if (flags.ContainsKey("libs") || flags.ContainsKey("dry-run"))
            {
                PrintLibraries();
                PrintFlow();
                return 0;
            }

            string service = Get(flags, "service", Environment.GetEnvironmentVariable("LINX_SERVICE_URL") ?? "http://localhost:1710/");
            string user = Get(flags, "user", Environment.GetEnvironmentVariable("LINX_USER"));
            string password = Get(flags, "password", Environment.GetEnvironmentVariable("LINX_PASSWORD"));
            bool useSso = flags.ContainsKey("sso");
            bool? acessoLocal = null;
            if (flags.ContainsKey("local"))
                acessoLocal = true;
            else if (flags.ContainsKey("remote"))
                acessoLocal = false;
            int? ambienteId = flags.ContainsKey("ambiente") ? int.Parse(flags["ambiente"]) : (int?)null;
            string totp = Get(flags, "totp", Environment.GetEnvironmentVariable("LINX_TOTP"));

            try
            {
                using (var client = new LinxUxAuthClient(service))
                {
                    string login;
                    if (useSso)
                    {
                        string clientId = Req(flags, "client-id", "LINX_SSO_CLIENT_ID");
                        string tenantId = Req(flags, "tenant-id", "LINX_SSO_TENANT_ID");
                        string redirect = Get(flags, "redirect", "http://localhost");
                        Console.WriteLine("Abrindo login Microsoft...");
                        string prefix = await LinxDesktopSso.ObterNomeAutenticacaoAsync(clientId, tenantId, redirect)
                            .ConfigureAwait(false);
                        Console.WriteLine("UPN prefixo: " + prefix);
                        login = await client.LoginAposSsoAsync(prefix).ConfigureAwait(false);
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(password))
                        {
                            Console.Error.WriteLine("Informe --user e --password (ou LINX_USER / LINX_PASSWORD), ou --sso.");
                            return 2;
                        }
                        login = await client.LoginComSenhaAsync(user, password).ConfigureAwait(false);
                    }

                    Console.WriteLine("1º fator OK: " + login);

                    List<AmbienteAcesso> ambientes = await client.ListarAmbientesAsync(login, acessoLocal)
                        .ConfigureAwait(false);
                    if (ambientes.Count == 0)
                        throw new InvalidOperationException(
                            "Nenhum ambiente (PortalUserAccess). Tente --local ou --remote.");
                    Console.WriteLine("Ambientes: " + ambientes.Count);
                    foreach (AmbienteAcesso a in ambientes)
                        Console.WriteLine("  - {0} id={1} gpecon={2} padrao={3}", a.DescricaoAmbiente, a.IdTcsAmbiente, a.IdLinxGpecon, a.IndicaAcessoPadrao);

                    AmbienteAcesso ambiente = ambienteId.HasValue
                        ? ambientes.FirstOrDefault(x => x.IdTcsAmbiente == ambienteId.Value)
                        : LinxUxAuthClient.EscolherAmbiente(ambientes);
                    if (ambiente == null)
                        throw new InvalidOperationException("Ambiente não encontrado.");

                    Console.WriteLine("Usando ambiente {0} GPECON {1}", ambiente.IdTcsAmbiente, ambiente.IdLinxGpecon);

                    MfaTicketDto mfa = await client.CompletarMfaAsync(
                        ambiente,
                        enroll =>
                        {
                            if (!string.IsNullOrEmpty(enroll.QrCodePngBase64))
                            {
                                string png = Path.Combine(Path.GetTempPath(), "linx-mfa-enroll.png");
                                File.WriteAllBytes(png, Convert.FromBase64String(enroll.QrCodePngBase64));
                                Console.WriteLine("QR salvo em " + png);
                                Console.WriteLine(enroll.AccountLabel);
                            }
                            return AskTotp(totp, "Informe o código de 6 dígitos do QR: ");
                        },
                        () => AskTotp(totp, "Código de 6 dígitos: "))
                        .ConfigureAwait(false);

                    if (mfa == null || !mfa.Success)
                        throw new InvalidOperationException(mfa != null ? mfa.Message : "MFA recusado.");

                    Console.WriteLine("MFA OK. Ticket expira {0:u}", mfa.TicketExpiresUtc);

                    LoginInfoDto sessao = await client.AbrirSessaoApplicationAsync(ambiente, login, mfa.Ticket)
                        .ConfigureAwait(false);
                    Guid token = sessao.Ambientes != null && sessao.Ambientes.Count > 0
                        ? sessao.Ambientes[0].Token
                        : Guid.Empty;
                    Console.WriteLine("AuthenticateUser OK: {0} ({1}) token={2}", sessao.NomeUsuario, sessao.UidUsuario, token);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Falha: " + ex.Message);
                return 1;
            }
        }

        private static string AskTotp(string fromFlag, string prompt)
        {
            if (!string.IsNullOrWhiteSpace(fromFlag))
                return fromFlag.Trim();
            Console.Write(prompt);
            return Console.ReadLine() ?? "";
        }

        private static void PrintHelp()
        {
            Console.WriteLine(@"POC desktop Linx UX — senha ou SSO + MFA + AuthenticateUser

Uso:
  dotnet run --project samples/LinxUxAuthDesktopPoc -- --libs
  dotnet run --project samples/LinxUxAuthDesktopPoc -- --user NOME --password SENHA --service http://localhost:1710/
  dotnet run --project samples/LinxUxAuthDesktopPoc -- --sso --client-id ID --tenant-id TID

Flags:
  --service URL     Service (default http://localhost:1710/ ou LINX_SERVICE_URL)
  --user NOME       Login local (ou LINX_USER)
  --password SENHA  Senha (ou LINX_PASSWORD)
  --totp 123456     Código MFA (ou LINX_TOTP); senão pergunta no console
  --ambiente N      IdTcsAmbiente se houver vários
  --local           AcessoLocal=true (EmDesenvolvimento)
  --remote          AcessoLocal=false (não tenta o outro valor)
  --sso             MSAL interactive (precisa --client-id --tenant-id)
  --redirect URI    Default http://localhost
  --libs            Lista bibliotecas e o fluxo (não chama a rede)
");
            PrintLibraries();
        }

        private static void PrintLibraries()
        {
            Console.WriteLine(@"
Bibliotecas necessárias
-----------------------
NuGet:
  Newtonsoft.Json            13.0.3     JSON do Service
  Microsoft.Identity.Client  4.83.0     SSO Azure (só se usar --sso)

BCL (.NET 8 / também no .NET Framework 4.8):
  System.Net.Http            HttpClient
  System.Security.Cryptography  via Linx.Security.Cryptography

Produto Linx (já no repositório, linkado neste csproj):
  Main/Common/.../Cryptography.cs
  namespace Linx.Security — a mesma classe do Portal.
  Em um app Windows de produção, referencie Linx.Tools.dll
  (Binary/Library/Common/Linx/Desktop/GAC/Linx.Tools.dll).

NÃO use:
  Client secret do Portal (desktop = public client)
  Token Azure no Service
");
            var crypto = new Linx.Security.Cryptography();
            string round = crypto.Decrypt(crypto.Encrypt("poc-ok"));
            Console.WriteLine("Linx.Security.Cryptography round-trip: " + (round == "poc-ok" ? "OK" : "FALHOU"));
            string emptyParams = crypto.Encrypt("");
            string emptyDec = crypto.Decrypt(emptyParams);
            Console.WriteLine("PortalUserAccess Parametros=Encrypt(\"\"): "
                              + (emptyParams.Length >= 4 && emptyDec == "" ? "OK (" + emptyParams.Length + " chars)" : "FALHOU"));
        }

        private static void PrintFlow()
        {
            Console.WriteLine(@"Fluxo deste esqueleto
---------------------
1. AuthenticatePortal  OU  MSAL + AuthenticatePortalSso
2. PortalUserAccess
3. GetMfaStatus → enroll/ValidateMfaCode/skip
4. ValidateMfaTicket → AuthenticateUser
");
        }

        private static Dictionary<string, string> Parse(string[] args)
        {
            var d = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < args.Length; i++)
            {
                string a = args[i];
                if (!a.StartsWith("--", StringComparison.Ordinal))
                    continue;
                string key = a.Substring(2);
                if (key.Length == 0)
                    continue;
                string val = "1";
                if (i + 1 < args.Length && !args[i + 1].StartsWith("--", StringComparison.Ordinal))
                {
                    val = args[i + 1];
                    i++;
                }
                d[key] = val;
            }
            return d;
        }

        private static string Get(Dictionary<string, string> flags, string key, string fallback)
        {
            string v;
            if (flags.TryGetValue(key, out v) && v != "1")
                return v;
            return fallback;
        }

        private static string Req(Dictionary<string, string> flags, string key, string env)
        {
            string v = Get(flags, key, Environment.GetEnvironmentVariable(env));
            if (string.IsNullOrWhiteSpace(v))
                throw new InvalidOperationException("Informe --" + key + " ou " + env);
            return v;
        }
    }
}
