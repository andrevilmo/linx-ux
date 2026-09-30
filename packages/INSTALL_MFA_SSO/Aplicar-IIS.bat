@echo off
setlocal EnableExtensions
REM Aplica INSTALL_MFA_SSO sobre uma instalacao IIS existente (somente MFA/SSO).
REM NAO copia Web.config.
REM Uso:
REM   Aplicar-IIS.bat
REM   Aplicar-IIS.bat "C:\Linx Program Files\Linx Framework 6.0.0"

set "BASE=%~1"
if "%BASE%"=="" set "BASE=C:\Linx Program Files\Linx Framework 6.0.0"
set "PACK=%~dp0"
if "%PACK:~-1%"=="\" set "PACK=%PACK:~0,-1%"

echo Pacote : "%PACK%"
echo Destino: "%BASE%"
echo.

if not exist "%BASE%\Portal\Web.config" (
  echo ERRO: nao achei "%BASE%\Portal\Web.config"
  echo Passe a pasta BASE que contem Portal, Service e Application.
  exit /b 1
)

call :COPY "Portal\bin\Linx.Portal.dll"
call :COPY "Portal\bin\Microsoft.Identity.Client.dll"
call :COPY "Portal\bin\Microsoft.IdentityModel.Abstractions.dll"
call :COPY "Portal\Views\Account\Login.cshtml"
call :COPY "Portal\Views\Mfa\Challenge.cshtml"
call :COPY "Service\bin\Linx.Framework.BV.dll"
call :COPY "Service\bin\Linx.Framework.BV.WebAPI.DS.dll"
call :COPY "Application\bin\Linx.Internet.Application.dll"
call :COPY "Application\bin\Linx.Framework.BV.SPA.dll"
call :COPY "Application\App\views\CadastroUsuario.html"
call :COPY "Application\App\views\CadastroUsuarioLocal.html"
call :COPY "Application\App\views\CadastroUsuarioAutenticacao.html"
call :COPY "Application\App\viewmodels\CadastroUsuario.js"
call :COPY "Application\App\viewmodels\CadastroUsuarioLocal.js"
call :COPY "Application\App\viewmodels\CadastroUsuarioAutenticacao.js"
call :COPY "Application\App\resources\CadastroUsuario_pt-br.js"
call :COPY "Application\App\resources\CadastroUsuarioLocal_pt-br.js"
call :COPY "Application\App\resources\CadastroUsuarioAutenticacao_pt-br.js"
call :COPY "Application\App\widgets\datatoolbar\view.html"

echo.
echo Concluido. Recicle os Application Pools Service, Portal e Application.
echo Ajuste SSO no Portal\Web.config — veja README_INSTALL.TXT secao 4.
echo Este script nao altera Web.config.
exit /b 0

:COPY
set "REL=%~1"
set "SRC=%PACK%\%REL%"
set "DST=%BASE%\%REL%"
if not exist "%SRC%" (
  echo FALTA NO PACOTE  %REL%
  goto :eof
)
for %%I in ("%DST%") do if not exist "%%~dpI" mkdir "%%~dpI"
copy /Y "%SRC%" "%DST%" >nul
if errorlevel 1 (
  echo FALHA   %REL%
) else (
  echo OK      %REL%
)
goto :eof
