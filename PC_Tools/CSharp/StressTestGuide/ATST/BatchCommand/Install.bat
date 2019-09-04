::============================================
:: Setting Variable
::============================================
@echo off 
setlocal
set Root=/sdcard/ATST
set Core=%Root%/Core
set Scripts=%Root%/Scripts
set Xml=%Root%/Xml
set ToolInfo=%Root%/ToolInfo
set Logs=%Root%/Logs
set ScreenShots=%Root%/ScreenShots
set Docs=%Root%/Docs
set Musics=%Root%/Musics
set Images=%Root%/Images
set Videos=%Root%/Videos
set ATSTRunCount=%ToolInfo%/ATSTRunCount

set LocalCore=..\Core
set LocalScripts=..\Scripts
set LocalDataXml=..\Xml
set LocalDocs=..\Files\Docs
set LocalMusic=..\Files\Musics
set LocalImages=..\Files\Images
set LocalVideos=..\Files\Videos
set LocalComparedImages=..\ComparedImages
set Apks=..\Files\APKs
set OthersAPK=..\OthersAPK


echo ============================================
echo Erase Old Data
echo ============================================

echo shell rm -r /sdcard/ATST
.\adb shell rm -r /sdcard/ATST
::echo uninstall com.asus.at
::.\adb uninstall com.asus.at
::echo uninstall com.asus.at.fillupstorage
::.\adb uninstall com.asus.at.fillupstorage
::echo uninstall com.asus.at.bootdetector
::.\adb uninstall com.asus.at.bootdetector
::echo uninstall com.asus.at.signkey
::.\adb uninstall com.asus.at.signkey

echo ============================================
echo Create File
echo ============================================
echo mkdir "%Root%"
adb shell "mkdir -p %Root%"
echo mkdir %Core%"
adb shell "mkdir -p %Core%"
echo mkdir %Scripts%"
adb shell "mkdir -p %Scripts%"
echo mkdir %ToolInfo%"
adb shell "mkdir -p %ToolInfo%"
echo mkdir %Logs%"
adb shell "mkdir -p %Logs%"
echo mkdir %ScreenShots%"
adb shell "mkdir -p %ScreenShots%"
echo mkdir %Docs%"
adb shell "mkdir -p %Docs%"
echo mkdir %Xml%"
adb shell "mkdir %Xml%"
echo mkdir %Musics%"
adb shell "mkdir -p %Musics%"
echo mkdir %Images%"
adb shell "mkdir -p %Images%"
echo mkdir %Videos%"
adb shell "mkdir -p %Videos%"
echo create file %ATSTRunCount%"
adb shell "echo 0 >> %ATSTRunCount%"

echo ============================================
echo Push Files
echo ============================================
echo push %Core%
adb push  %LocalCore% %Core%
::echo push %Scripts%
::adb push  %LocalScripts% %Scripts%
::echo push %Xml%
::adb push  %LocalDataXml% %Xml%

echo push %Docs%
for %%i in (%LocalDocs%\*.*) do adb push %%i %Docs%/
::IF "%1"=="MTBF" (
::for %%i in (%LocalDocs%\MTBF\*.*) do adb push %%i %Docs%/)
::IF "%1"=="SSI" (
::for %%i in (%LocalDocs%\SSI\*.*) do adb push %%i %Docs%/)
::IF "%1"=="Stress" (
::for %%i in (%LocalDocs%\SSI\*.*) do adb push %%i %Docs%/)

echo push %Musics%
for %%i in (%LocalMusic%\*.*) do adb push %%i %Musics%/
::IF "%1"=="MTBF" (
::for %%i in (%LocalMusic%\MTBF\*.*) do adb push %%i %Musics%/)
::IF "%1"=="SSI" (
::for %%i in (%LocalMusic%\SSI\*.*) do adb push %%i %Musics%/)
::IF "%1"=="Stress" (
::for %%i in (%LocalMusic%\SSI\*.*) do adb push %%i %Musics%/)

echo push %Images%
for %%i in (%LocalImages%\*.*) do adb push %%i %Images%/
::for %%i in (%LocalComparedImages%\*.*) do adb push %%i %Images%/
::IF "%1"=="MTBF" (
::for %%i in (%LocalImages%\MTBF\*.*) do adb push %%i %Images%/)
::IF "%1"=="SSI" (
::for %%i in (%LocalImages%\SSI\*.*) do adb push %%i %Images%/)
::IF "%1"=="Stress" (
::for %%i in (%LocalImages%\SSI\*.*) do adb push %%i %Images%/)

echo push %Videos%
for %%i in (%LocalVideos%\*.*) do adb push %%i %Videos%/
::IF "%1"=="MTBF" (
::for %%i in (%LocalVideos%\MTBF\*.*) do adb push %%i %Videos%/)
::IF "%1"=="SSI" (
::for %%i in (%LocalVideos%\SSI\*.*) do adb push %%i %Videos%/)
::IF "%1"=="Stress" (
::for %%i in (%LocalVideos%\SSI\*.*) do adb push %%i %Videos%/)


echo ============================================
echo Install APKs
echo ============================================
::for %%i in (%Apks%\*.apk) do adb install -r %%i
for %%i in (%OthersAPK%\*.apk) do adb install -r %%i
IF "%1"=="MTBF" (
)

::IF "%1"=="MTBF" (
::for %%i in (%Apks%\MTBF\*.apk) do adb install -r %%i)
::IF "%1"=="SSI" (
::for %%i in (%Apks%\SSI\*.apk) do adb install -r %%i)
::IF "%1"=="Stress" (
::for %%i in (%Apks%\SSI\*.apk) do adb install -r %%i)
::
::adb shell am start -n com.asus.at.bootdetector/.EmptyActivity
::echo Finished! Device will be reboot.
::timeout 2
::adb reboot
endlocal