@echo off 
set currentdir=%cd%

REM WebDev
start ..\Library\Common\WebDev\WebDev.WebServer40 /port:8172 /path:"%currentdir%"

popd
