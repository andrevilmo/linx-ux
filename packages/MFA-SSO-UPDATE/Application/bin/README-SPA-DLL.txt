Linx.Framework.BV.SPA.dll is required for Cadastro e Manutenção de Usuários
(#Linx-Administrativo/.../Cadastro-e-Manutencao-de-Usuarios) and the other
pkg_linx-framework-bv-spa user screens. Those views are embedded in this DLL.

Copy to: Application\bin\Linx.Framework.BV.SPA.dll
(relative to Application\Web.config)

Loose App\views\CadastroUsuario.html does not win over the embedded resource.
Rebuild this DLL after changing CadastroUsuario / Local / Autenticacao views.
