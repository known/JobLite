@echo off 
set filename=JobLite.Service.exe
set servicename=JobLiteService
set Frameworkdc=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319
 
if exist "%Frameworkdc%" goto netOld 
:DispError 
echo ���Ļ�����û�а�װ .net Framework 4.0,��װ������ֹ.
echo ���Ļ�����û�а�װ .net Framework 4.0,��װ������ֹ  >InstallService.log
goto LastEnd 
:netOld 
cd %Frameworkdc%
echo ���Ļ����ϰ�װ����Ӧ��.net Framework 4.0,���԰�װ������. 
echo ���Ļ����ϰ�װ����Ӧ��.net Framework 4.0,���԰�װ������ >InstallService.log
echo.
echo. >>InstallService.log

call UnInstall.bat

echo *********************
echo ��װ����
%Frameworkdc%\installutil.exe %filename% >>InstallService.log
echo ��������
net start %servicename% >>InstallService.log
echo *********************
echo �������������Բ鿴��־�ļ�InstallService.log �о���Ĳ��������
:LastEnd 
pause
rem exit