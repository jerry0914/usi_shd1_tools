@echo off

 FOR /F "tokens=1-4 delims=/ " %%a IN ("%date%") DO (
 SET _MyDate=%%a%%b%%c%
 )

 FOR /F "tokens=1-3 delims=:." %%a IN ("%time%") DO (
 SET _MyTime=%%a%%b%%c
 )
 
SET folderName=%_MyDate%_%_MyTime%
echo "Get log files and save to ../Logs/%folderName%/ ,please wait..."
echo "Retrieving /sdcard/ATST/ToolInfo/..."
.\adb pull "/sdcard/ATST/ToolInfo/" "../Logs/%folderName%/ToolInfo"
echo "Retrieving /sdcard/ATST/Logs/..."
.\adb pull "/sdcard/ATST/Logs/" "../Logs/%folderName%"
echo "Retrieving /sdcard/ATST/ScreenShots/..."
.\adb pull "/sdcard/ATST/ScreenShots/" "../Logs/%folderName%/ScreenShots"
echo "Retrieving /sdcard/ATST/StressResults/..."
.\adb pull "/sdcard/ATST/StressResults/" "../Logs/%folderName%/StressResults"
echo "Retrieving /data/anr/..."
.\adb pull "/data/anr" "../Logs/%folderName%/anr"

echo "Retrieving /Removable/MicroSD/logcat_log/"
.\adb pull "/Removable/MicroSD/logcat_log/" "../Logs/%folderName%"
echo "Retrieving /Removable/MicroSD/Logs/"
.\adb pull "/Removable/MicroSD/Logs/" "../Logs/%folderName%"
echo "Retrieving /sdcard/Logs"
.\adb pull "/sdcard/Logs" "../Logs/%folderName%"/Logs

echo "Done!"