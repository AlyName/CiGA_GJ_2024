set WORKSPACE=..
set LUBAN_DLL=.\Luban\Luban.dll
set CONF_ROOT=.

dotnet %LUBAN_DLL% ^
    -t client ^
    -d json ^
    -c cs-simple-json ^
    --conf .\luban.conf ^
    -x outputDataDir=../TableJson^
    -x outputCodeDir=../scripts/TableData
pause