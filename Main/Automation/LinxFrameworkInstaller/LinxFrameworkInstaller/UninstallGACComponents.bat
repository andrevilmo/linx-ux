@echo off

:: BatchGotAdmin
:-------------------------------------
REM  --> Check for permissions
    IF "%PROCESSOR_ARCHITECTURE%" EQU "amd64" (
>nul 2>&1 "%SYSTEMROOT%\SysWOW64\cacls.exe" "%SYSTEMROOT%\SysWOW64\config\system"
) ELSE (
>nul 2>&1 "%SYSTEMROOT%\system32\cacls.exe" "%SYSTEMROOT%\system32\config\system"
)

REM --> If error flag set, we do not have admin.
if '%errorlevel%' NEQ '0' (
    echo Requesting administrative privileges...
    goto UACPrompt
) else ( goto gotAdmin )

:UACPrompt
    echo Set UAC = CreateObject^("Shell.Application"^) > "%temp%\getadmin.vbs"
    set params = %*:"=""
    echo UAC.ShellExecute "cmd.exe", "/c ""%~s0"" %params%", "", "runas", 1 >> "%temp%\getadmin.vbs"

    "%temp%\getadmin.vbs"
    del "%temp%\getadmin.vbs"
    exit /B

:gotAdmin
    pushd "%CD%"
    CD /D "%~dp0"
:--------------------------------------  


call "%programfiles(x86)%\Microsoft Visual Studio 14.0\Common7\Tools\VsDevCmd.bat"

@echo Uninstall Tfs assemblies
gacutil -uf Microsoft.TeamFoundation.Client /nologo /silent
gacutil -uf Microsoft.TeamFoundation.Common /nologo /silent
gacutil -uf Microsoft.TeamFoundation.VersionControl.Client /nologo /silent
gacutil -uf Microsoft.TeamFoundation.VersionControl.Common /nologo /silent
gacutil -uf Microsoft.VisualStudio.Services.Common /nologo /silent
gacutil -uf Microsoft.VisualStudio.Services.WebApi /nologo /silent

@echo Uninstall Linx.Builder.Resources...
gacutil -uf Linx.Builder.Resources /nologo /silent

@echo Uninstall Linx.Tools...
gacutil -uf Linx.Tools /nologo /silent

@echo Uninstall Linx.Data...
gacutil -uf Linx.Data /nologo /silent

@echo Uninstall Linx.LinqExtensions...
gacutil -uf Linx.LinqExtensions /nologo /silent

@echo Uninstall XmlConfigMerge...
gacutil -uf XmlConfigMerge /nologo /silent

@echo Uninstall Linx.Dsl.Components...
gacutil -uf Linx.Dsl.Components /nologo /silent

@echo Uninstall Microsoft.AnalysisServices.AdomdClient...
gacutil -uf Microsoft.AnalysisServices.AdomdClient /nologo /silent

@echo Uninstall LinxHttpContext...
gacutil -uf LinxHttpContext /nologo /silent

@echo Uninstall Newtonsoft.Json...
gacutil -uf Newtonsoft.Json /nologo /silent

@echo Uninstall LinxHttpContext...
gacutil -uf System.Data.SQLite111 /nologo /silent

@echo Uninstall Linx.SourceControl...
gacutil -uf Linx.SourceControl /nologo /silent

@echo.
