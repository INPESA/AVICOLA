@echo off
setlocal

:: Usa la carpeta que contiene el archivo .bat
set "folderPath=%~dp0"

:: Define el icono embebido en SHELL32.dll
set "iconResource=C:\WINDOWS\System32\SHELL32.dll,45"

:: Crea o edita el archivo desktop.ini en la carpeta objetivo
echo [.ShellClassInfo] > "%folderPath%desktop.ini"
echo IconResource=%iconResource% >> "%folderPath%desktop.ini"

:: Aplica atributos oculto y de sistema al archivo desktop.ini
attrib +h +s "%folderPath%desktop.ini"

:: Cambia el atributo de la carpeta para que use desktop.ini
attrib +r "%folderPath%"

:: Confirma los cambios
echo El icono de la carpeta se ha cambiado correctamente.

endlocal
