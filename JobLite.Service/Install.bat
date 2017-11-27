@echo off 
set filename=JobLite.Service.exe
set servicename=JobLiteService
set Frameworkdc=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319
 
if exist "%Frameworkdc%" goto netOld 
:DispError 
echo 您的机器上没有安装 .net Framework 4.0,安装即将终止.
echo 您的机器上没有安装 .net Framework 4.0,安装即将终止  >InstallService.log
goto LastEnd 
:netOld 
cd %Frameworkdc%
echo 您的机器上安装了相应的.net Framework 4.0,可以安装本服务. 
echo 您的机器上安装了相应的.net Framework 4.0,可以安装本服务 >InstallService.log
echo.
echo. >>InstallService.log

call UnInstall.bat

echo *********************
echo 安装服务
%Frameworkdc%\installutil.exe %filename% >>InstallService.log
echo 启动服务
net start %servicename% >>InstallService.log
echo *********************
echo 操作结束，可以查看日志文件InstallService.log 中具体的操作结果。
:LastEnd 
pause
rem exit