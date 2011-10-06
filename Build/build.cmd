@cls
@call "%VS100COMNTOOLS%\vsvars32.bat"
@MSBuild Scripts\_release.msbuild /fileLogger %*
