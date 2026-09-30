@echo off
setlocal EnableExtensions
REM Atualiza a pasta INSTALL_MFA_SSO a partir de uma instalacao IIS ja publicada
REM (DLLs e views) e gera de novo INSTALL_MFA_SSO.zip ao lado desta pasta.
REM Uso:
REM   Atualizar-Pacote.bat
REM   Atualizar-Pacote.bat "C:\Linx Program Files\Linx Framework 6.0.0"

set "BASE=%~1"
if "%BASE%"=="" set "BASE=C:\Linx Program Files\Linx Framework 6.0.0"
set "PACK=%~dp0"
if "%PACK:~-1%"=="\" set "PACK=%PACK:~0,-1%"
for %%I in ("%PACK%") do set "PARENT=%%~dpI"
if "%PARENT:~-1%"=="\" set "PARENT=%PARENT:~0,-1%"
set "ZIP=%PARENT%\INSTALL_MFA_SSO.zip"

echo Origem IIS : "%BASE%"
echo Pacote     : "%PACK%"
echo Zip        : "%ZIP%"
echo.

if not exist "%BASE%\Portal\bin\Linx.Portal.dll" (
  echo ERRO: nao achei "%BASE%\Portal\bin\Linx.Portal.dll"
  exit /b 1
)

call :PULL "Portal\bin\Linx.Portal.dll"
call :PULL "Portal\bin\Microsoft.Identity.Client.dll"
call :PULL "Portal\bin\Microsoft.IdentityModel.Abstractions.dll"
call :PULL "Portal\Views\Account\Login.cshtml"
call :PULL "Portal\Views\Mfa\Challenge.cshtml"
call :PULL "Service\bin\Linx.Framework.BV.dll"
call :PULL "Service\bin\Linx.Framework.BV.WebAPI.DS.dll"
call :PULL "Application\bin\Linx.Internet.Application.dll"
call :PULL "Application\bin\Linx.Framework.BV.SPA.dll"
call :PULL "Application\App\views\CadastroUsuario.html"
call :PULL "Application\App\views\CadastroUsuarioLocal.html"
call :PULL "Application\App\views\CadastroUsuarioAutenticacao.html"
call :PULL "Application\App\viewmodels\CadastroUsuario.js"
call :PULL "Application\App\viewmodels\CadastroUsuarioLocal.js"
call :PULL "Application\App\viewmodels\CadastroUsuarioAutenticacao.js"
call :PULL "Application\App\resources\CadastroUsuario_pt-br.js"
call :PULL "Application\App\resources\CadastroUsuarioLocal_pt-br.js"
call :PULL "Application\App\resources\CadastroUsuarioAutenticacao_pt-br.js"
call :PULL "Application\App\widgets\datatoolbar\view.html"

echo.
echo Gerando zip...
if exist "%ZIP%" del /F /Q "%ZIP%"
powershell -NoProfile -ExecutionPolicy Bypass -Command ^
  "Compress-Archive -Path '%PACK%' -DestinationPath '%ZIP%' -Force"
if errorlevel 1 (
  echo FALHA ao gerar o zip.
  exit /b 1
)

echo.
echo Pacote atualizado. Recalcule hashes em FILES_TO_INSTALL.txt se for redistribuir.
echo Zip: "%ZIP%"
exit /b 0

:PULL
set "REL=%~1"
set "SRC=%BASE%\%REL%"
set "DST=%PACK%\%REL%"
if not exist "%SRC%" (
  echo FALTA NO IIS    %REL%
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
