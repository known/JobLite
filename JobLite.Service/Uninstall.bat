@echo off 
set filename=JobLite.Service.exe
set servicename=JobLiteService
set Frameworkdc=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319
 
if exist "%Frameworkdc%" goto netOld 
:DispError 
echo ���Ļ�����û�а�װ .net Framework 4.0,��װ������ֹ.
echo ���Ļ�����û�а�װ .net Framework 4.0,��װ������ֹ  >UnInstallService.log
goto LastEnd 
:netOld 
cd %Frameworkdc%
echo ���Ļ����ϰ�װ����Ӧ��.net Framework 4.0,���԰�װ������. 
echo ���Ļ����ϰ�װ����Ӧ��.net Framework 4.0,���԰�װ������ >UnInstallService.log
echo.
echo. >>UnInstallService.log

echo *********************
echo ֹͣ����
net stop %servicename% >>UnInstallService.log
echo �������
%Frameworkdc%\installutil.exe /U %filename% >>UnInstallService.log
echo �������
echo.
echo *********************
echo �������������Բ鿴��־�ļ�UnInstallService.log �о���Ĳ��������
:LastEnd 
pause
rem exit