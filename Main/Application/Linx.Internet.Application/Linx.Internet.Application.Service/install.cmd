@ECHO OFF
pushd "%~dp0" 
LinxUXLIAService.exe -i -sname:LinxUXLIAService -sdisplayname:"LinxUX Application Service"
popd

pause.exe