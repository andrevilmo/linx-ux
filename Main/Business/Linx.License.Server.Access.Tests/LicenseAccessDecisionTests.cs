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
    }
}
