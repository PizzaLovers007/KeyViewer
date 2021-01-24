set /p version=<VERSION.txt
cp -r KeyViewer/bin/Release .
cd Release
tar -a -c -f ../KeyViewer-%version%.zip *
cd ..
rm -rf Release