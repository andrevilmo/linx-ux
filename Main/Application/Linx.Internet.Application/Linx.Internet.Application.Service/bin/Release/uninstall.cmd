@ECHO OFF
net stop "LinxUX Application Service"

pushd "%~dp0" 
LinxUXLIAService.exe -u -sname:LinxUXLIAService
popd

pause.exe
