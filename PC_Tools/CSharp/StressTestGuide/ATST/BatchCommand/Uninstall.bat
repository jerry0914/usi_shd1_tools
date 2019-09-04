@echo off
echo ============================================
echo Erasing all data...
echo ============================================

echo shell rm -r /sdcard/ATST
.\adb shell rm -r /sdcard/ATST
echo uninstall com.asus.at
.\adb uninstall com.asus.at
echo uninstall com.asus.at.fillupstorage
.\adb uninstall com.asus.at.fillupstorage
echo uninstall com.asus.at.bootdetector
.\adb uninstall com.asus.at.bootdetector
echo uninstall com.asus.at.signkey
.\adb uninstall com.asus.at.signkey

echo "Done!"
pause