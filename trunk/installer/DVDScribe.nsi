;XPStyle on

;!include "MUI.nsh"
;!insertMacro MUI_PAGE_LICENCE ttt.txt

OutFile "DVDScribeInstaller.exe"
InstallDir $ProgramFiles\DVDScribe
	section
		setOutPath $INSTDIR
		file ..\DVDScribe\bin\Release\DVDScribe.exe
		file ..\DVDScribe\bin\Release\WindowsExplorer.dll
		file ..\doc\ReadMe.txt
		file ..\doc\ReleaseNotes.txt

		writeUninstaller $INSTDIR\uninstall.exe
	
		CreateDirectory "$SMPROGRAMS\DVDScribe"

		createShortCut "$SMPROGRAMS\DVDScribe\DVDScribe.lnk" "$INSTDIR\DVDScribe.exe"
		createShortCut "$SMPROGRAMS\DVDScribe\Readme.lnk" "$INSTDIR\ReadMe.txt"
		createShortCut "$SMPROGRAMS\DVDScribe\ReleaseNotes.lnk" "$INSTDIR\ReleaseNotes.txt"
		createShortCut "$SMPROGRAMS\DVDScribe\uninstall.lnk" "$INSTDIR\uninstall.exe"
	
	sectionEnd
	
	section "Uninstall"
		delete $INSTDIR\uninstall.exe
		delete $INSTDIR\DVDScribe.exe
		delete $INSTDIR\WindowsExplorer.dll
		delete $INSTDIR\ReadMe.txt
		delete $INSTDIR\ReleaseNotes.txt
		RMDir  $INSTDIR		
		delete "$SMPROGRAMS\DVDScribe\DVDScribe.lnk"
		delete "$SMPROGRAMS\DVDScribe\Readme.lnk"
		delete "$SMPROGRAMS\DVDScribe\ReleaseNotes.lnk"
		delete "$SMPROGRAMS\DVDScribe\uninstall.lnk"
		RMDIR  "$SMPROGRAMS\DVDScriber"
	sectionEnd