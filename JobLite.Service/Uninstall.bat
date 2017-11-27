@echo off 
set filename=JobLite.Service.exe
set servicename=JobLiteService
set Frameworkdc=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319
 
if exist "%Frameworkdc%" goto netOld 
:DispError 
echo 您的机器上没有安装 .net Framework 4.0,安装即将终止.
echo 您的机器上没有安装 .net Framework 4.0,安装即将终止  >UnInstallService.log
goto LastEnd 
:netOld 
cd %Frameworkdc%
echo 您的机器上安装了相应的.net Framework 4.0,可以安装本服务. 
echo 您的机器上安装了相应的.net Framework 4.0,可以安装本服务 >UnInstallService.log
echo.
echo. >>UnInstallService.log

echo *********************
echo 停止服务
net stop %servicename% >>UnInstallService.log
echo 清理服务
%Frameworkdc%\installutil.exe /U %filename% >>UnInstallService.log
echo 清理完毕
echo.
echo *********************
echo 操作结束，可以查看日志文件UnInstallService.log 中具体的操作结果。
:LastEnd 
pause
rem exit