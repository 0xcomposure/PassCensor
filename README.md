**PassCensor**
============================

This is just a simple program created by the need to censor passwords for some reports. 

Since I had to do this task both in Windows and Linux, I decided to write this using .NET Core.

**Build**

``dotnet publish -r linux-x64`` Linux

``dotnet publish -r win-x64``   Windows

**Usage**

``PassCensor -H``  Help

``PassCensor /path/to/inputfile /path/to/outputfile```

**Download compiled binaries**

[Linux x64](https://github.com/0xcomposure/PassCensor/releases/download/PassCesnor/PassCensor)

[Windows x64](https://github.com/0xcomposure/PassCensor/releases/download/PassCesnor/PassCensor.exe)
