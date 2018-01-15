#define MyAppName "OrionDocs"
#define MyAppVersion "0.17.3.1ß"
#define MyAppPublisher "Víctor Cruz"
#define MyAppExeName "OrionDocs.exe"
#define GUID "D3EABBBA-0BF7-409B-B133-FEC0058B0913"

[Setup]
AppId={{{#GUID}}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={pf}\{#MyAppName}
DefaultGroupName={#MyAppName}
OutputDir=C:\Users\Víctor\Documents\Visual Studio 2017\Projects\OrionDocs
OutputBaseFilename=OrionDocs_v0.17.3.1b
Compression=lzma
SolidCompression=yes
UninstallDisplayIcon={app}\{#MyAppExeName},0
UninstallDisplayName={#MyAppName} v{#MyAppVersion}
WizardSmallImageFile=ODocsInno.bmp

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"; LicenseFile: "ccl-4.0-by-nc-nd.txt"

[Files]
Source: "C:\Users\Víctor\Documents\Visual Studio 2017\Projects\OrionDocs\OrionDocs\bin\Release\OrionDocs.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Víctor\Documents\Visual Studio 2017\Projects\OrionDocs\OrionDocs\bin\Release\templates\*"; DestDir: "{app}\templates"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "ccl-4.0-by-nc-nd.txt"; DestDir: "{app}"; DestName: "LICENSE.txt"; Flags: ignoreversion

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"

[Run]
Filename: "{app}\{#MyAppExeName}"; Parameters: "--config"; Flags: nowait postinstall skipifsilent; Description: "Run OrionDocs configuration wizard"

[Registry]
Root: "HKCU"; Subkey: "Software\Classes\*\shell\{{{#GUID}}"; ValueType: string; ValueName: "Icon"; ValueData: "{app}\OrionDocs.exe,0"; Flags: uninsdeletekey; MinVersion: 0,5.01sp3
Root: "HKCU"; Subkey: "Software\Classes\*\shell\{{{#GUID}}"; ValueType: string; ValueName: "MUIVerb"; ValueData: "Generate documentation"; Flags: uninsdeletekey; MinVersion: 0,5.01sp3
Root: "HKCU"; Subkey: "Software\Classes\*\shell\{{{#GUID}}\command"; ValueType: string; ValueData: "{app}\OrionDocs.exe %1!"; Flags: uninsdeletekey; MinVersion: 0,5.01sp3
Root: "HKCU"; Subkey: "Software\Classes\Directory\background\shell\{{{#GUID}}"; ValueType: string; ValueName: "Icon"; ValueData: "{app}\OrionDocs.exe,0"; Flags: uninsdeletekey; MinVersion: 0,5.01sp3
Root: "HKCU"; Subkey: "Software\Classes\Directory\background\shell\{{{#GUID}}"; ValueType: string; ValueName: "MUIVerb"; ValueData: "OrionDocs Fallback Settings Configuration"; Flags: uninsdeletekey; MinVersion: 0,5.01sp3
Root: "HKCU"; Subkey: "Software\Classes\Directory\background\shell\{{{#GUID}}\command"; ValueType: string; ValueData: "{app}\OrionDocs.exe --config"; Flags: uninsdeletekey; MinVersion: 0,5.01sp3
