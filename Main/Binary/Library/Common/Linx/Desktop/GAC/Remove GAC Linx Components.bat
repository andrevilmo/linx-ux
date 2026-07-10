call "%programfiles(x86)%\Microsoft Visual Studio 14.0\Common7\Tools\VsDevCmd.bat"

@echo off

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

@echo Copying dll SciLexer
echo f | xcopy /Y /C /I /Q /H /R "C:\VSTS - GrupoLinx\Framework\Linx Framework\Main\Binary\Library\Common\ScintillaNET\SciLexer*.dll" "C:\Windows\SysWOW64\"
