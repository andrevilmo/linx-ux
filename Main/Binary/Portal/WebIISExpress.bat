@echo off 
SET LocalProgramFiles="%ProgramFiles(x86)%" 
IF %LocalProgramFiles% == "" SET LocalProgramFiles="%ProgramFiles%" 
set currentdir=%cd%

REM IIS Express
pushd "%LocalProgramFiles%\IIS Express\" 
start http://localhost:8172/
iisexpress /path:"%currentdir%" /port:8172 /clr:v4.0

popd
