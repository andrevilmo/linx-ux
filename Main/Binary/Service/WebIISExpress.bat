@echo off 
SET LocalProgramFiles="%ProgramFiles(x86)%" 
IF %LocalProgramFiles% == "" SET LocalProgramFiles="%ProgramFiles%" 
set currentdir=%cd%

REM IIS Express
pushd "%LocalProgramFiles%\IIS Express\" 
iisexpress /path:"%currentdir%" /port:1710 /clr:v4.0

popd