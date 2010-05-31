;Include modern UI
!include "MUI2.nsh"
!include "FileFunc.nsh"


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

Function .onInit
  System::Call 'kernel32::CreateMutexA(i 0, i 0, t "LabelCreatorInstaller") ?e'
  Pop $R0
  StrCmp $R0 0 +3
	MessageBox MB_OK "The installer is already running."
	Abort
FunctionEnd	

Function un.onInit
  FindWindow $0 "WindowsForms10.Window.8.app.0.33c0d9d" "Label Creator"
  StrCmp $0 0 Remove
    MessageBox MB_ICONSTOP|MB_OK "It appears that Label Creator is currently open.$\n\
                                  Close it and restart uninstaller."
    Quit
Remove:
FunctionEnd
 
Section
	SetOutPath $INSTDIR
	File ..\DVDScribe\bin\Release\LabelCreator.exe
	File ..\DVDScribe\bin\Release\ExpTreeLib.dll
	File ..\DVDScribe\bin\Release\ICSharpCode.SharpZipLib.dll
	File ..\doc\ReadMe.txt
	File ..\doc\license.txt
	File ..\doc\ReleaseNotes.txt
	SetOutPath $INSTDIR\Backgrounds
	File ..\DVDScribe\bin\Release\Backgrounds\1.jpg
	File ..\DVDScribe\bin\Release\Backgrounds\2.jpg
	WriteUninstaller $INSTDIR\Uninstall.exe

	CreateDirectory "$SMPROGRAMS\LabelCreator"
	CreateShortCut "$SMPROGRAMS\LabelCreator\LabelCreator.lnk" "$INSTDIR\LabelCreator.exe"
	CreateShortCut "$SMPROGRAMS\LabelCreator\Readme.lnk" "$INSTDIR\ReadMe.txt"
	CreateShortCut "$SMPROGRAMS\LabelCreator\Uninstall.lnk" "$INSTDIR\Uninstall.exe"

	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\LabelCreator" "DisplayName" "Label Creator"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\LabelCreator" "DisplayIcon" "$\"$INSTDIR\LabelCreator.exe$\""
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\LabelCreator" "DisplayVersion" "Trunk"		
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\LabelCreator" "UninstallString" "$\"$INSTDIR\uninstall.exe$\""
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\LabelCreator" "URLInfoAbout" "http://code.google.com/p/labelcreator/"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\LabelCreator" "Publisher" "beford.net"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\LabelCreator" "HelpLink" "http://code.google.com/p/labelcreator/issues/list"
	${GetSize} "$INSTDIR" "/S=0K" $0 $1 $2
	IntFmt $0 "0x%08X" $0
	WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\LabelCreator" "EstimatedSize" "$0"
	WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\LabelCreator" "NoModify" "1"
	WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\LabelCreator" "NoRepair" "1"	
SectionEnd
	
Section "Uninstall"
	Delete $INSTDIR\Uninstall.exe
	Delete $INSTDIR\LabelCreator.exe
	Delete $INSTDIR\ExpTreeLib.dll
	Delete $INSTDIR\ReadMe.txt
	Delete $INSTDIR\license.txt
	Delete $INSTDIR\ReleaseNotes.txt
	Delete $INSTDIR\ICSharpCode.SharpZipLib.dll
	Delete $INSTDIR\Backgrounds\1.jpg
	Delete $INSTDIR\Backgrounds\2.jpg
	RMDir  $INSTDIR\Backgrounds		
	RMDir  $INSTDIR		
	!insertmacro MUI_STARTMENU_GETFOLDER Application $StartMenuFolder
	Delete "$SMPROGRAMS\LabelCreator\LabelCreator.lnk"
	Delete "$SMPROGRAMS\LabelCreator\Readme.lnk"
	Delete "$SMPROGRAMS\LabelCreator\Uninstall.lnk"
	RMDir  "$SMPROGRAMS\LabelCreator"
	DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\LabelCreator"	
SectionEnd