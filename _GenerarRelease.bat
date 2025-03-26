@echo off
setlocal

rem Obtener la fecha y hora en formato YYYYMMDDHHMMSS desde WMIC
for /f "tokens=2 delims==" %%I in ('wmic os get localdatetime /value') do set datetime=%%I
set year=%datetime:~0,4%
set month=%datetime:~4,2%
set day=%datetime:~6,2%
set hour=%datetime:~8,2%

rem Crear el formato de fecha para el mensaje de commit y el tag de release
set date=%year%-%month%-%day%
rem Se usa un guion bajo para evitar espacios
set tag=version_%year:~2,2%.%month%.%day%.%hour%

rem Mover los archivos index.cod e Indicador.920 a la carpeta Respaldos
if not exist Respaldos mkdir Respaldos
move index.cod Respaldos
move Indicador.920 Respaldos

rem Formatear el mensaje de commit
set message="cambios de %date%"

rem Ejecutar comandos Git
git add .
git commit -m %message%
git tag %tag%
git push origin main --tags

pause
