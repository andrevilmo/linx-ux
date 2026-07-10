@echo off 
set currentdir=%cd%

REM WebDev
start ..\Library\Common\WebDev\WebDev.WebServer40 /port:1710 /path:"%currentdir%"

popd
