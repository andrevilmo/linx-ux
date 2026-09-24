using Linx.Framework.BV.LicenseServer;
using Xunit;

namespace Linx.License.Server.Access.Tests
{
    public class LicenseAccessDecisionTests
    {
        private static LicenseAccessSnapshot LicensedCustomer()
        {
            return new LicenseAccessSnapshot
            {
                LxStatusLicenca = LicenseAccessDecision.StatusLicencaProducao,
                LxStatusLicencaCliente = LicenseAccessDecision.StatusLicencaProducao,
                LxStatusLicencaClienteName = "Produção"
            };
        }

        [Fact]
        public void Allows_production_and_test_customer_licenses()
        {
            LicenseAccessResult production = LicenseAccessDecision.EvaluateCustomer(LicensedCustomer());
            Assert.True(production.Allowed);

            LicenseAccessSnapshot test = LicensedCustomer();
            test.LxStatusLicenca = LicenseAccessDecision.StatusLicencaTestes;
            test.LxStatusLicencaCliente = LicenseAccessDecision.StatusLicencaTestes;
            LicenseAccessResult testResult = LicenseAccessDecision.EvaluateCustomer(test);
            Assert.True(testResult.Allowed);
        }

        [Fact]
        public void Blocks_when_customer_license_is_missing()
        {
            LicenseAccessResult result = LicenseAccessDecision.EvaluateCustomer(null);
            Assert.False(result.Allowed);
            Assert.Equal("CUSTOMER_LICENSE_MISSING", result.ReasonCode);
        }

        [Fact]
        public void Blocks_when_customer_license_status_is_not_declared()
        {
            LicenseAccessResult result = LicenseAccessDecision.EvaluateCustomer(new LicenseAccessSnapshot());
            Assert.False(result.Allowed);
            Assert.Equal("CUSTOMER_LICENSE_MISSING", result.ReasonCode);
        }

        [Fact]
        public void Blocks_inactive_customer()
        {
            LicenseAccessSnapshot snapshot = LicensedCustomer();
            snapshot.InativoCliente = true;
            LicenseAccessResult result = LicenseAccessDecision.EvaluateCustomer(snapshot);
            Assert.False(result.Allowed);
            Assert.Equal("CUSTOMER_INACTIVE", result.ReasonCode);
        }

        [Fact]
        public void Blocks_inactive_license()
        {
            LicenseAccessSnapshot snapshot = LicensedCustomer();
            snapshot.InativoLicenca = true;
            LicenseAccessResult result = LicenseAccessDecision.EvaluateCustomer(snapshot);
            Assert.False(result.Allowed);
            Assert.Equal("CUSTOMER_LICENSE_INACTIVE", result.ReasonCode);
        }

        [Fact]
        public void Blocks_inactive_product()
        {
            LicenseAccessSnapshot snapshot = LicensedCustomer();
            snapshot.InativoProduto = true;
            LicenseAccessResult result = LicenseAccessDecision.EvaluateCustomer(snapshot);
            Assert.False(result.Allowed);
            Assert.Equal("PRODUCT_INACTIVE", result.ReasonCode);
        }

        [Fact]
        public void Blocks_financial_lock_on_customer()
        {
            LicenseAccessSnapshot snapshot = LicensedCustomer();
            snapshot.IndicaBloqueioFinanceiroCliente = true;
            LicenseAccessResult result = LicenseAccessDecision.EvaluateCustomer(snapshot);
            Assert.False(result.Allowed);
            Assert.Equal("CUSTOMER_FINANCIAL_BLOCK", result.ReasonCode);
        }

        [Fact]
        public void Blocks_financial_lock_on_license()
        {
            LicenseAccessSnapshot snapshot = LicensedCustomer();
            snapshot.IndicaBloqueioFinanceiroLicenca = true;
            LicenseAccessResult result = LicenseAccessDecision.EvaluateCustomer(snapshot);
            Assert.False(result.Allowed);
            Assert.Equal("LICENSE_FINANCIAL_BLOCK", result.ReasonCode);
        }

        [Fact]
        public void Blocks_discontinued_product_and_customer_license()
        {
            LicenseAccessSnapshot product = LicensedCustomer();
            product.LxStatusLicenca = LicenseAccessDecision.StatusLicencaDescontinuada;
            Assert.Equal("PRODUCT_LICENSE_DISCONTINUED", LicenseAccessDecision.EvaluateCustomer(product).ReasonCode);

            LicenseAccessSnapshot customer = LicensedCustomer();
            customer.LxStatusLicencaCliente = LicenseAccessDecision.StatusLicencaDescontinuada;
            customer.LxStatusLicencaClienteName = "Descontinuada";
            LicenseAccessResult result = LicenseAccessDecision.EvaluateCustomer(customer);
            Assert.Equal("CUSTOMER_LICENSE_DISCONTINUED", result.ReasonCode);
            Assert.Contains("Descontinuada", result.Message);
        }

        [Fact]
        public void Blocks_when_controlled_quantity_is_exceeded()
        {
            LicenseAccessSnapshot snapshot = LicensedCustomer();
            snapshot.ControlaQtde = true;
            snapshot.QtdeContratada = 5;
            snapshot.QtdeEmUso = 6;
            LicenseAccessResult result = LicenseAccessDecision.EvaluateCustomer(snapshot);
            Assert.False(result.Allowed);
            Assert.Equal("LICENSE_QUOTA_EXCEEDED", result.ReasonCode);
        }

        [Fact]
        public void Allows_quantity_at_contracted_limit()
        {
            LicenseAccessSnapshot snapshot = LicensedCustomer();
            snapshot.ControlaQtde = true;
            snapshot.QtdeContratada = 5;
            snapshot.QtdeEmUso = 5;
            Assert.True(LicenseAccessDecision.EvaluateCustomer(snapshot).Allowed);
        }

        [Fact]
        public void Allows_active_usage_key()
        {
            LicenseAccessResult result = LicenseAccessDecision.EvaluateUsageKey(new LicenseUsageSnapshot
            {
                LxStatusChave = LicenseAccessDecision.StatusChaveAtivo,
                Mensagem = "Licença Ativa."
            });
            Assert.True(result.Allowed);
        }

        [Theory]
        [InlineData((byte)2, "USAGE_KEY_PENDING")]
        [InlineData((byte)3, "USAGE_KEY_REVOKED")]
        [InlineData((byte)4, "USAGE_KEY_UNAUTHORIZED")]
        [InlineData((byte)0, "USAGE_KEY_INVALID")]
        public void Blocks_non_active_usage_keys(byte status, string expectedReason)
        {
            LicenseAccessResult result = LicenseAccessDecision.EvaluateUsageKey(new LicenseUsageSnapshot
            {
                LxStatusChave = status
            });
            Assert.False(result.Allowed);
            Assert.Equal(expectedReason, result.ReasonCode);
        }

        [Fact]
        public void Blocks_missing_usage_key()
        {
            LicenseAccessResult result = LicenseAccessDecision.EvaluateUsageKey(null);
            Assert.False(result.Allowed);
            Assert.Equal("USAGE_KEY_MISSING", result.ReasonCode);
        }

        [Fact]
        public void Uses_server_message_for_usage_key_block()
        {
            LicenseAccessResult result = LicenseAccessDecision.EvaluateUsageKey(new LicenseUsageSnapshot
            {
                LxStatusChave = LicenseAccessDecision.StatusChaveNaoAutorizado,
                Mensagem = "Quantidade de licenças excedida."
            });
            Assert.Contains("Quantidade de licenças excedida.", result.Message);
        }

        [Fact]
        public void Omni_Validate_allows_active_key()
        {
            LicenseAccessResult result = LicenseAccessDecision.EvaluateValidation(new LicenseValidationResult
            {
                Licenca = new LicenseInfo { LxStatusChave = 1 }
            });
            Assert.True(result.Allowed);
        }

        [Fact]
        public void Omni_Validate_blocks_missing_payload()
        {
            LicenseAccessResult result = LicenseAccessDecision.EvaluateValidation(null);
            Assert.False(result.Allowed);
            Assert.Equal("USAGE_KEY_MISSING", result.ReasonCode);
        }

        [Fact]
        public void Omni_Validate_blocks_financial_and_contract_origins()
        {
            LicenseAccessResult financial = LicenseAccessDecision.EvaluateValidation(new LicenseValidationResult
            {
                Licenca = new LicenseInfo { LxStatusChave = 4, OrigemBloqueio = "F", Mensagem = "Bloqueio financeiro." }
            });
            Assert.Equal("CUSTOMER_FINANCIAL_BLOCK", financial.ReasonCode);
            Assert.Contains("Bloqueio financeiro.", financial.Message);

            LicenseAccessResult contract = LicenseAccessDecision.EvaluateValidation(new LicenseValidationResult
            {
                Licenca = new LicenseInfo { LxStatusChave = 4, OrigemBloqueio = "C" }
            });
            Assert.Equal("CUSTOMER_LICENSE_DISCONTINUED", contract.ReasonCode);
        }

        [Fact]
        public void Omni_Validate_blocks_revoked_key()
        {
            LicenseAccessResult result = LicenseAccessDecision.EvaluateValidation(new LicenseValidationResult
            {
                Licenca = new LicenseInfo { LxStatusChave = 3 }
            });
            Assert.Equal("USAGE_KEY_REVOKED", result.ReasonCode);
        }

        [Fact]
        public void Normalizes_base_url_and_cnpj_from_Web_config_values()
        {
            Assert.Equal(
                "https://api-hml.linx.com.br/app-licensing/",
                LicenseServerSettings.NormalizeBaseUrl("https://api-hml.linx.com.br/app-licensing/api/v1/"));
            Assert.Equal("45510647000100", LicenseServerSettings.SanitizeCnpj("45.510.647/0001-00"));
        }
    }
}
