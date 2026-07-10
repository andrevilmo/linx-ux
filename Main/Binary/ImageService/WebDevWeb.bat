@echo off 
SET LocalProgramFiles="%ProgramFiles(x86)%" 
IF %LocalProgramFiles% == "" SET LocalProgramFiles="%ProgramFiles%" 
set currentdir=%cd% 
pushd "%LocalProgramFiles%\Common Files\microsoft shared\DevServer\11.0\" 
start WebDev.WebServer40 /port:1711 /path:"%currentdir%"
popd
