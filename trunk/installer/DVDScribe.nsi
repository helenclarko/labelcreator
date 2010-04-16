;Include modern UI
!include "MUI2.nsh"

;Request application privileges for Windows Vista
RequestExecutionLevel admin

;Variables
Var StartMenuFolder

;Macro for license
!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_LICENSE "..\doc\license.txt"
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_STARTMENU Application $StartMenuFolder

!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH

!insertmacro MUI_UNPAGE_WELCOME
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES
!insertmacro MUI_UNPAGE_FINISH

;Name and file 
Name "LabelCreator"
OutFile "LabelCreatorSetup.exe"

;Default install location
InstallDir $ProgramFiles\LabelCreator

;Languages
!insertmacro MUI_LANGUAGE "Spanish"
!insertmacro MUI_LANGUAGE "English"

Section
	setOutPath $INSTDIR
	file ..\DVDScribe\bin\Release\LabelCreator.exe
	file ..\DVDScribe\bin\Release\ExpTreeLib.dll
	file ..\doc\ReadMe.txt
	file ..\doc\license.txt
	file ..\doc\ReleaseNotes.txt

	setOutPath $INSTDIR\Backgrounds

	file ..\DVDScribe\bin\Release\Backgrounds\1.jpg
	file ..\DVDScribe\bin\Release\Backgrounds\2.jpg

	writeUninstaller $INSTDIR\uninstall.exe

	CreateDirectory "$SMPROGRAMS\LabelCreator"

	createShortCut "$SMPROGRAMS\LabelCreator\LabelCreator.lnk" "$INSTDIR\LabelCreator.exe"
	createShortCut "$SMPROGRAMS\LabelCreator\Readme.lnk" "$INSTDIR\ReadMe.txt"
	createShortCut "$SMPROGRAMS\LabelCreator\uninstall.lnk" "$INSTDIR\uninstall.exe"

SectionEnd
	
Section "Uninstall"
	delete $INSTDIR\uninstall.exe
	delete $INSTDIR\LabelCreator.exe
	delete $INSTDIR\ExpTreeLib.dll
	delete $INSTDIR\ReadMe.txt
	delete $INSTDIR\license.txt
	delete $INSTDIR\ReleaseNotes.txt

	delete $INSTDIR\Backgrounds\1.jpg
	delete $INSTDIR\Backgrounds\2.jpg

	RMDir  $INSTDIR\Backgrounds		
	RMDir  $INSTDIR		

	!insertmacro MUI_STARTMENU_GETFOLDER Application $StartMenuFolder

	delete "$SMPROGRAMS\LabelCreator\LabelCreator.lnk"
	delete "$SMPROGRAMS\LabelCreator\Readme.lnk"
	delete "$SMPROGRAMS\LabelCreator\uninstall.lnk"
	RMDIR  "$SMPROGRAMS\LabelCreator"
SectionEnd