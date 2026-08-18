# Core source files (121)

First-party source, docs, CI, VS Code, Binary **configs/views** (excludes `packages/`, `node_modules/`, `bin/`, `obj/`, `publish-output/`, `.vs/`, SelfHost Dependencies, BarcodeScanner, WebBundleResources).

| Status | File | Theme |
| --- | --- | --- |
| A | .github/workflows/si-pdr-aws-iis.yml | T7 AWS IIS CI/CD |
| M | .vscode/TASKS.README.md | T8 VS Code tasks / SI-PDR package |
| M | .vscode/deploy-to-linx-framework.ps1 | T8 VS Code tasks / SI-PDR package |
| A | .vscode/pack-si-pdr-INVENTORY.template.md | T8 VS Code tasks / SI-PDR package |
| A | .vscode/pack-si-pdr-LEIA-ME.template.md | T8 VS Code tasks / SI-PDR package |
| A | .vscode/pack-si-pdr.ps1 | T8 VS Code tasks / SI-PDR package |
| A | .vscode/settings.json | T8 VS Code tasks / SI-PDR package |
| M | .vscode/stack-to-deploy.ps1 | T8 VS Code tasks / SI-PDR package |
| M | .vscode/stack-to-publish.ps1 | T8 VS Code tasks / SI-PDR package |
| M | .vscode/tasks.json | T8 VS Code tasks / SI-PDR package |
| A | AGENTS.md | T8 VS Code tasks / SI-PDR package |
| M | Main/Application/Linx.Internet.Application/.vscode/msbuild-build.ps1 | T8 VS Code tasks / SI-PDR package |
| M | Main/Application/Linx.Internet.Application/Linx.Framework.BV.SPA/App_Start/ModuleConfig.cs | T2 GpeCon export filter |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/App/viewmodels/shared/modalChangePassword.js | T3 Password flow / lock / email |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/App/viewmodels/shell/_footer.js | T1 Application footer / layout |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/App/views/shell.html | T1 Application footer / layout |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/App/views/shell/_footer.html | T1 Application footer / layout |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/App/widgets/datatoolbar/view.html | T2 GpeCon export filter |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/App_Start/BundleConfig.cs | T0 Other product source |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/Controllers/AppCacheController.cs | T0 Other product source |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/Controllers/LIAController.cs | T0 Other product source |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/SelfHost/SelfHostBase.zip | T0 Other product source |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/Views/Shared/Authentication.cshtml | T0 Other product source |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/Views/Shared/Unauthorized.cshtml | T0 Other product source |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/Views/Shared/_Footer.cshtml | T1 Application footer / layout |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/Views/Shared/_FooterClean.cshtml | T1 Application footer / layout |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/Views/Shared/_Layout.cshtml | T1 Application footer / layout |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/Views/Shared/_LayoutClean.cshtml | T1 Application footer / layout |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/Web.config | T9 QA AWS web.config / ports / SQL |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/lib/linx/css/linx-common.less | T1 Application footer / layout |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/lib/linx/css/linx-theme-default.less | T1 Application footer / layout |
| M | Main/Application/Linx.Portal/.vscode/msbuild-build.ps1 | T8 VS Code tasks / SI-PDR package |
| A | Main/Application/Linx.Portal/Linx.Portal/Authentication/AuthenticatedUser.cs | T6 Azure AD SSO (MSAL) |
| A | Main/Application/Linx.Portal/Linx.Portal/Authentication/AuthenticationResultModel.cs | T6 Azure AD SSO (MSAL) |
| A | Main/Application/Linx.Portal/Linx.Portal/Authentication/AzureAdOptions.cs | T6 Azure AD SSO (MSAL) |
| A | Main/Application/Linx.Portal/Linx.Portal/Authentication/FileTokenCacheStore.cs | T6 Azure AD SSO (MSAL) |
| A | Main/Application/Linx.Portal/Linx.Portal/Authentication/IAuthenticationService.cs | T6 Azure AD SSO (MSAL) |
| A | Main/Application/Linx.Portal/Linx.Portal/Authentication/ITokenCacheStore.cs | T6 Azure AD SSO (MSAL) |
| A | Main/Application/Linx.Portal/Linx.Portal/Authentication/MsalAuthenticationService.cs | T6 Azure AD SSO (MSAL) |
| A | Main/Application/Linx.Portal/Linx.Portal/Authentication/SsoLoginHelper.cs | T6 Azure AD SSO (MSAL) |
| M | Main/Application/Linx.Portal/Linx.Portal/Controllers/AccountController.cs | T5 Portal login / Bloqueado / forget-password |
| M | Main/Application/Linx.Portal/Linx.Portal/Linx.Portal.csproj | T0 Other product source |
| M | Main/Application/Linx.Portal/Linx.Portal/Utils/Utils.cs | T0 Other product source |
| M | Main/Application/Linx.Portal/Linx.Portal/Views/Account/Login.cshtml | T5 Portal login / Bloqueado / forget-password |
| M | Main/Application/Linx.Portal/Linx.Portal/Web.config | T9 QA AWS web.config / ports / SQL |
| M | Main/Application/Linx.Portal/Linx.Portal/assets/css/portal.css | T5 Portal login / Bloqueado / forget-password |
| M | Main/Application/Linx.Portal/Linx.Portal/packages.config | T0 Other product source |
| M | Main/BM/Linx.Framework.Autorizacao.BM/Linx.Framework.Autorizacao.BM/Autorizacao.bmd | T0 Other product source |
| M | Main/BM/Linx.Framework.Autorizacao.BM/Linx.Framework.Autorizacao.BM/Migrations/Configuration.cs | T0 Other product source |
| M | Main/BM/Linx.Framework.Autorizacao.BM/Linx.Framework.Autorizacao.BM/Model/BusinessDataModel.cs | T0 Other product source |
| M | Main/BM/Linx.Framework.Autorizacao.BM/Linx.Framework.Autorizacao.BM/Model/BusinessDataModel.tt | T0 Other product source |
| A | Main/BM/Linx.Framework.Autorizacao.BM/Linx.Framework.Autorizacao.BM/Scripts/INDICA_USUARIO_SERVICO.sql | T4 Auth audit (TCS_LOG_ACESSO_AUTH) |
| A | Main/BM/Linx.Framework.Autorizacao.BM/Linx.Framework.Autorizacao.BM/Scripts/TCS_LOG_ACESSO_AUTH.sql | T4 Auth audit (TCS_LOG_ACESSO_AUTH) |
| M | Main/Binary/Application/Views/Shared/_Footer.cshtml | T1 Application footer / layout |
| M | Main/Binary/Application/Views/Shared/_FooterClean.cshtml | T1 Application footer / layout |
| M | Main/Binary/Application/Views/Shared/_Layout.cshtml | T1 Application footer / layout |
| M | Main/Binary/Application/Views/Shared/_LayoutClean.cshtml | T1 Application footer / layout |
| M | Main/Binary/Application/Web.config | T9 QA AWS web.config / ports / SQL |
| M | Main/Binary/Library/Business Model/Linx.Framework.Autorizacao.BM.dll.config | T9 QA AWS web.config / ports / SQL |
| M | Main/Binary/Library/Business Model/Linx.Framework.ControleSistema.BM.dll.config | T9 QA AWS web.config / ports / SQL |
| M | Main/Binary/Portal/Views/Account/Login.cshtml | T5 Portal login / Bloqueado / forget-password |
| M | Main/Binary/Portal/Web.config | T9 QA AWS web.config / ports / SQL |
| M | Main/Binary/Portal/assets/css/portal.css | T5 Portal login / Bloqueado / forget-password |
| A | Main/Binary/Service/SqlScripts/Disable_Update_aspnet_Membership_Trigger.sql | T10 Binary deploy artifacts |
| M | Main/Binary/Service/Web.config | T9 QA AWS web.config / ports / SQL |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.AuthenticateUserExtension/AutorizacaoDomainService.UserExtension.cs | T3 Password flow / lock / email |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.AuthenticateUserExtension/Linx.Framework.BV.AuthenticateUserExtension.csproj | T3 Password flow / lock / email |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.Implementations/Linx.Framework.BV.Implementations.csproj | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.Reports/Linx.Framework.BV.Reports.csproj | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/.vscode/msbuild-build.ps1 | T8 VS Code tasks / SI-PDR package |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkAutorizacao.cs | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkAutorizacaoAutoGen.cs | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkEmpresaAutoGen.cs | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkModulo.cs | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkObjeto.cs | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkParametro.cs | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkPerfilAutoGen.cs | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkUsuarioAutorizacaoAutoGen.cs | T3 Password flow / lock / email |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkUsuarioFranquiaAutoGen.cs | T3 Password flow / lock / email |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkUtilitariosAutoGen.cs | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Linx.Framework.BV.WebAPI.DS.csproj | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI/Linx.Framework.BV.WebAPI.csproj | T0 Other product source |
| A | Main/Business/Linx.Framework.BV/Linx.Framework.BV/Autorizacao.AuthAccessAudit.Operations.cs | T4 Auth audit (TCS_LOG_ACESSO_AUTH) |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV/Autorizacao.AuthorizationServices.Operations.cs | T0 Other product source |
| A | Main/Business/Linx.Framework.BV/Linx.Framework.BV/Help For Accessing/README.txt | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV/Linx.Framework.BV.csproj | T0 Other product source |
| A | Main/Business/Linx.Framework.BV/Linx.Framework.BV/Modulo.TcsVersao.Operations.cs | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV/TcsAutorizacao.AuthorizationServices.Operations.cs | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV/UsuarioAutorizacao.DomainService.cs | T3 Password flow / lock / email |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV/UsuarioAutorizacao.UsuarioAutorizacaoDomainService.Operations.cs | T3 Password flow / lock / email |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV/UsuarioFranquia.DomainService.cs | T3 Password flow / lock / email |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV/UsuarioFranquia.TcsUsuarioAutenticacao.Events.cs | T3 Password flow / lock / email |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV/Web.config | T9 QA AWS web.config / ports / SQL |
| A | Main/Common/Linx.Tools.Library/Desktop/Linx.Desktop.Tools/.vscode/msbuild-build.ps1 | T8 VS Code tasks / SI-PDR package |
| M | Main/Common/Linx.Tools.Library/Desktop/Linx.Desktop.Tools/LinxErrorConstants.cs | T3 Password flow / lock / email |
| M | Main/Common/Linx.Tools.Library/Desktop/Linx.Desktop.Tools/LinxMail.cs | T3 Password flow / lock / email |
| M | Main/Common/Linx.Tools.Library/Desktop/Linx.Tools.Core/LinxErrorConstants.cs | T3 Password flow / lock / email |
| A | Main/NLOG.log | T11 Noise / generated |
| M | Main/User Interface/.vscode/msbuild-build.ps1 | T8 VS Code tasks / SI-PDR package |
| M | Main/User Interface/Linx.Framework.BV/.vscode/msbuild-build.ps1 | T8 VS Code tasks / SI-PDR package |
| M | Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/resources/CadastroUsuarioAutenticacao_pt-br.js | T3 Password flow / lock / email |
| M | Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/resources/CadastroUsuarioLocal_pt-br.js | T3 Password flow / lock / email |
| M | Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/services/UsuarioAutorizacaoContext.js | T3 Password flow / lock / email |
| M | Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/services/UsuarioFranquiaContext.js | T3 Password flow / lock / email |
| M | Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/viewmodels/CadastroUsuarioAutenticacao.js | T3 Password flow / lock / email |
| M | Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/viewmodels/CadastroUsuarioLocal.js | T3 Password flow / lock / email |
| M | Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/views/CadastroUsuarioAutenticacao.html | T3 Password flow / lock / email |
| M | Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/views/CadastroUsuarioLocal.html | T3 Password flow / lock / email |
| A | Main/User Interface/Linx.Framework.BV/docker-framework-bv/Dockerfile | T8 VS Code tasks / SI-PDR package |
| A | Main/User Interface/Linx.Framework.BV/docker-framework-bv/docker-compose.yml | T8 VS Code tasks / SI-PDR package |
| A | docs/si-pdr-aws-iis.md | T7 AWS IIS CI/CD |
| A | docs/si-pdr-portal-sso.md | T6 Azure AD SSO (MSAL) |
| A | docs/sso-azure-ad-msal-guide.md | T6 Azure AD SSO (MSAL) |
| A | infra/si-pdr-cicd/README.md | T7 AWS IIS CI/CD |
| A | infra/si-pdr-cicd/scripts/Clear-BuildWorkspace.ps1 | T7 AWS IIS CI/CD |
| A | infra/si-pdr-cicd/scripts/Diagnose-SiPdrRuntime.ps1 | T7 AWS IIS CI/CD |
| A | infra/si-pdr-cicd/scripts/Ensure-BuildTools.ps1 | T7 AWS IIS CI/CD |
| A | infra/si-pdr-cicd/scripts/Ensure-IisSiPdr.ps1 | T7 AWS IIS CI/CD |
| A | infra/si-pdr-cicd/scripts/Invoke-SiPdrAwsPipeline.ps1 | T7 AWS IIS CI/CD |
| A | infra/si-pdr-cicd/scripts/Set-SiPdrSqlConnectionStrings.ps1 | T7 AWS IIS CI/CD |
| A | infra/si-pdr-cicd/scripts/Sync-SiPdrWorkspace.ps1 | T7 AWS IIS CI/CD |
