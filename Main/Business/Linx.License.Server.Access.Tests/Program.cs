using System;
using Linx.Framework.BV.LicenseServer;

namespace Linx.License.Server.Access.Tests
{
    internal static class Program
    {
        private static int failures;

        private static LicenseAccessSnapshot LicensedCustomer()
        {
            return new LicenseAccessSnapshot
            {
                LxStatusLicenca = LicenseAccessDecision.StatusLicencaProducao,
                LxStatusLicencaCliente = LicenseAccessDecision.StatusLicencaProducao,
                LxStatusLicencaClienteName = "Produção"
            };
        }

        internal static void AssertTrue(bool condition, string name)
        {
            if (condition)
            {
                Console.WriteLine("PASS  " + name);
                return;
            }

            failures++;
            Console.WriteLine("FAIL  " + name);
        }

        internal static void AssertEqual(string expected, string actual, string name)
        {
            AssertTrue(expected == actual, name + " (expected '" + expected + "', got '" + actual + "')");
        }

        internal static void AssertContains(string expected, string actual, string name)
        {
            AssertTrue(actual != null && actual.IndexOf(expected, StringComparison.Ordinal) >= 0, name);
        }

        private static int Main()
        {
            LicenseAccessResult production = LicenseAccessDecision.EvaluateCustomer(LicensedCustomer());
            AssertTrue(production.Allowed, "Allows production customer license");

            LicenseAccessSnapshot test = LicensedCustomer();
            test.LxStatusLicenca = LicenseAccessDecision.StatusLicencaTestes;
            test.LxStatusLicencaCliente = LicenseAccessDecision.StatusLicencaTestes;
            AssertTrue(LicenseAccessDecision.EvaluateCustomer(test).Allowed, "Allows test customer license");

            LicenseAccessResult missing = LicenseAccessDecision.EvaluateCustomer(null);
            AssertTrue(!missing.Allowed, "Blocks missing customer license");
            AssertEqual("CUSTOMER_LICENSE_MISSING", missing.ReasonCode, "Missing customer reason");

            LicenseAccessResult undeclared = LicenseAccessDecision.EvaluateCustomer(new LicenseAccessSnapshot());
            AssertTrue(!undeclared.Allowed, "Blocks undeclared customer license");
            AssertEqual("CUSTOMER_LICENSE_MISSING", undeclared.ReasonCode, "Undeclared customer reason");

            LicenseAccessSnapshot inactiveCustomer = LicensedCustomer();
            inactiveCustomer.InativoCliente = true;
            AssertEqual("CUSTOMER_INACTIVE", LicenseAccessDecision.EvaluateCustomer(inactiveCustomer).ReasonCode, "Blocks inactive customer");

            LicenseAccessSnapshot inactiveLicense = LicensedCustomer();
            inactiveLicense.InativoLicenca = true;
            AssertEqual("CUSTOMER_LICENSE_INACTIVE", LicenseAccessDecision.EvaluateCustomer(inactiveLicense).ReasonCode, "Blocks inactive license");

            LicenseAccessSnapshot inactiveProduct = LicensedCustomer();
            inactiveProduct.InativoProduto = true;
            AssertEqual("PRODUCT_INACTIVE", LicenseAccessDecision.EvaluateCustomer(inactiveProduct).ReasonCode, "Blocks inactive product");

            LicenseAccessSnapshot financialCustomer = LicensedCustomer();
            financialCustomer.IndicaBloqueioFinanceiroCliente = true;
            AssertEqual("CUSTOMER_FINANCIAL_BLOCK", LicenseAccessDecision.EvaluateCustomer(financialCustomer).ReasonCode, "Blocks customer financial lock");

            LicenseAccessSnapshot financialLicense = LicensedCustomer();
            financialLicense.IndicaBloqueioFinanceiroLicenca = true;
            AssertEqual("LICENSE_FINANCIAL_BLOCK", LicenseAccessDecision.EvaluateCustomer(financialLicense).ReasonCode, "Blocks license financial lock");

            LicenseAccessSnapshot discontinuedProduct = LicensedCustomer();
            discontinuedProduct.LxStatusLicenca = LicenseAccessDecision.StatusLicencaDescontinuada;
            AssertEqual("PRODUCT_LICENSE_DISCONTINUED", LicenseAccessDecision.EvaluateCustomer(discontinuedProduct).ReasonCode, "Blocks discontinued product");

            LicenseAccessSnapshot discontinuedCustomer = LicensedCustomer();
            discontinuedCustomer.LxStatusLicencaCliente = LicenseAccessDecision.StatusLicencaDescontinuada;
            discontinuedCustomer.LxStatusLicencaClienteName = "Descontinuada";
            LicenseAccessResult discontinued = LicenseAccessDecision.EvaluateCustomer(discontinuedCustomer);
            AssertEqual("CUSTOMER_LICENSE_DISCONTINUED", discontinued.ReasonCode, "Blocks discontinued customer");
            AssertContains("Descontinuada", discontinued.Message, "Discontinued message includes status name");

            LicenseAccessSnapshot quota = LicensedCustomer();
            quota.ControlaQtde = true;
            quota.QtdeContratada = 5;
            quota.QtdeEmUso = 6;
            AssertEqual("LICENSE_QUOTA_EXCEEDED", LicenseAccessDecision.EvaluateCustomer(quota).ReasonCode, "Blocks exceeded quota");

            LicenseAccessSnapshot atLimit = LicensedCustomer();
            atLimit.ControlaQtde = true;
            atLimit.QtdeContratada = 5;
            atLimit.QtdeEmUso = 5;
            AssertTrue(LicenseAccessDecision.EvaluateCustomer(atLimit).Allowed, "Allows quantity at contracted limit");

            AssertTrue(LicenseAccessDecision.EvaluateUsageKey(new LicenseUsageSnapshot
            {
                LxStatusChave = LicenseAccessDecision.StatusChaveAtivo
            }).Allowed, "Allows active usage key");

            AssertEqual("USAGE_KEY_PENDING", LicenseAccessDecision.EvaluateUsageKey(new LicenseUsageSnapshot { LxStatusChave = 2 }).ReasonCode, "Blocks pending key");
            AssertEqual("USAGE_KEY_REVOKED", LicenseAccessDecision.EvaluateUsageKey(new LicenseUsageSnapshot { LxStatusChave = 3 }).ReasonCode, "Blocks revoked key");
            AssertEqual("USAGE_KEY_UNAUTHORIZED", LicenseAccessDecision.EvaluateUsageKey(new LicenseUsageSnapshot { LxStatusChave = 4 }).ReasonCode, "Blocks unauthorized key");
            AssertEqual("USAGE_KEY_INVALID", LicenseAccessDecision.EvaluateUsageKey(new LicenseUsageSnapshot { LxStatusChave = 0 }).ReasonCode, "Blocks invalid key");
            AssertEqual("USAGE_KEY_MISSING", LicenseAccessDecision.EvaluateUsageKey(null).ReasonCode, "Blocks missing usage key");

            LicenseAccessResult serverMessage = LicenseAccessDecision.EvaluateUsageKey(new LicenseUsageSnapshot
            {
                LxStatusChave = LicenseAccessDecision.StatusChaveNaoAutorizado,
                Mensagem = "Quantidade de licenças excedida."
            });
            AssertContains("Quantidade de licenças excedida.", serverMessage.Message, "Uses server message for usage key block");

            AssertTrue(LicenseAccessDecision.EvaluateValidation(new LicenseValidationResult
            {
                Licenca = new LicenseInfo { LxStatusChave = 1 }
            }).Allowed, "Omni Validate allows active key");

            LicenseAccessResult missingValidation = LicenseAccessDecision.EvaluateValidation(null);
            AssertEqual("USAGE_KEY_MISSING", missingValidation.ReasonCode, "Omni Validate blocks missing payload");

            LicenseAccessResult financial = LicenseAccessDecision.EvaluateValidation(new LicenseValidationResult
            {
                Licenca = new LicenseInfo { LxStatusChave = 4, OrigemBloqueio = "F", Mensagem = "Bloqueio financeiro." }
            });
            AssertEqual("CUSTOMER_FINANCIAL_BLOCK", financial.ReasonCode, "Omni Validate blocks financial origin");
            AssertContains("Bloqueio financeiro.", financial.Message, "Omni Validate keeps server financial message");

            LicenseAccessResult contract = LicenseAccessDecision.EvaluateValidation(new LicenseValidationResult
            {
                Licenca = new LicenseInfo { LxStatusChave = 4, OrigemBloqueio = "C" }
            });
            AssertEqual("CUSTOMER_LICENSE_DISCONTINUED", contract.ReasonCode, "Omni Validate blocks contract origin");

            LicenseAccessResult revoked = LicenseAccessDecision.EvaluateValidation(new LicenseValidationResult
            {
                Licenca = new LicenseInfo { LxStatusChave = 3 }
            });
            AssertEqual("USAGE_KEY_REVOKED", revoked.ReasonCode, "Omni Validate blocks revoked key");

            AssertEqual("https://api-hml.linx.com.br/app-licensing/", LicenseServerSettings.NormalizeBaseUrl("https://api-hml.linx.com.br/app-licensing/api/v1/"), "Strips trailing api/v1 from BaseUrl");
            AssertEqual("https://api-hml.linx.com.br/app-licensing/", LicenseServerSettings.NormalizeBaseUrl("https://api-hml.linx.com.br/app-licensing"), "Adds trailing slash to BaseUrl");
            AssertEqual("45510647000100", LicenseServerSettings.SanitizeCnpj("45.510.647/0001-00"), "Sanitizes CNPJ digits");

            string requestJson = Newtonsoft.Json.JsonConvert.SerializeObject(new LicenseValidationRequest
            {
                IdLicenca = 4,
                Cnpj = "45510647000100",
                Chave = "MAQUINA-01",
                Usuario = "linxpos@linx.com.br",
                Terminal = "MAQUINA-01",
                Versao = "1.0"
            });
            AssertContains("\"idLicenca\":4", requestJson, "Serializes idLicenca");
            AssertContains("\"cnpj\":\"45510647000100\"", requestJson, "Serializes cnpj");
            AssertContains("\"chave\":\"MAQUINA-01\"", requestJson, "Serializes chave");
            AssertTrue(requestJson.IndexOf("IdLicenca", StringComparison.Ordinal) < 0, "Does not emit PascalCase IdLicenca");

            var missingPassword = new LicenseServerSettings
            {
                Email = "linxpos@linx.com.br",
                Password = "",
                Cnpj = "45510647000100",
                Key = "MAQUINA-01",
                LicenseId = 4,
                ProductId = "LINX-POS",
                BaseUrl = "https://api-hml.linx.com.br/app-licensing/"
            };
            AssertTrue(!missingPassword.IsValid(out string missingPasswordMessage), "Rejects empty password in Web.config");
            AssertContains("Password", missingPasswordMessage, "Empty password mentions Password");

            LicenseServerLiveTests.Run();

            Console.WriteLine();
            if (failures == 0)
            {
                Console.WriteLine("All license access decision tests passed.");
                return 0;
            }

            Console.WriteLine(failures + " test(s) failed.");
            return 1;
        }
    }
}
