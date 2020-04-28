!include MUI2.nsh

Name "Remote Desktop Connection List Manager"
BrandingText " "
OutFile "RDCLM Installer.exe"
Unicode True
InstallDir $PROGRAMFILES\RDCLM

!define PRODUCT "Remote Desktop Connection List Manager"
!define SHORT_PRODUCT_NAME "RDCLM"
!define MUI_COMPONENTSPAGE_SMALLDESC
!define MUI_FINISHPAGE_NOAUTOCLOSE

!insertmacro MUI_PAGE_LICENSE "LICENCE"
!insertmacro MUI_PAGE_COMPONENTS
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH

!insertmacro MUI_UNPAGE_WELCOME
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES
!insertmacro MUI_UNPAGE_FINISH

!insertmacro MUI_LANGUAGE "English"

Section "Remote Desktop Connection List Manager" S1
	SectionIn RO
	SetOutPath $INSTDIR
	File "Remote Desktop Connection List Manager.exe"
	WriteUninstaller "$INSTDIR\uninstall.exe"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\RDCLM" "DisplayName" "Remote Desktop Connection List Manager"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\RDCLM" "InstallLocation" "$INSTDIR"
	WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\RDCLM" "NoModify" "1"
	WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\RDCLM" "NoRepair" "1"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\RDCLM" "UninstallString" "$INSTDIR\uninstall.exe"
SectionEnd

Section "Add To Start Menu" S2
	CreateDirectory "$SMPROGRAMS\Remote Desktop Connection List Manager"
	CreateShortcut "$SMPROGRAMS\Remote Desktop Connection List Manager\Remote Desktop Connection List Manager.lnk" "$INSTDIR\Remote Desktop Connection List Manager.exe"
SectionEnd

Section "Create Desktop Shortcut" S3
	CreateShortcut "$DESKTOP\Remote Desktop Connection List Manager.lnk" "$INSTDIR\Remote Desktop Connection List Manager.exe"
SectionEnd

Section "Uninstall"
	Delete "$INSTDIR\uninstall.exe"
	Delete "$INSTDIR\Remote Desktop Connection List Manager.exe"
	Delete "$SMPROGRAMS\Remote Desktop Connection List Manager\Remote Desktop Connection List Manager.lnk"
	Delete "$DESKTOP\Remote Desktop Connection List Manager.lnk"
	RMDir $INSTDIR
	DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\RDCLM"
SectionEnd

!insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
!insertmacro MUI_DESCRIPTION_TEXT ${S1} "Install Remote Desktop Connection List Manager and its core file(s)"
!insertmacro MUI_DESCRIPTION_TEXT ${S2} "Add a shortcut to Remote Desktop Connection List Manager to the Start Menu"
!insertmacro MUI_DESCRIPTION_TEXT ${S3} "Add a shortcut to Remote Desktop Connection List Manager to the Desktop folder"
!insertmacro MUI_FUNCTION_DESCRIPTION_END