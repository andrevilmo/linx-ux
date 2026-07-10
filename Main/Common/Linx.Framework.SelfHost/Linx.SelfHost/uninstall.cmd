@ECHO OFF
net stop "Linx Framework SelfHost"

pushd "%~dp0" 
Linx.SelfHost.exe -u -sname:Linx.SelfHost
popd

pause.exe