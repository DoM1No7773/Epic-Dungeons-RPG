## Epic Dungeons

C# Monogame simple game project

### Required to build

- [Dotnet SDK 6.0 or higher](https://dotnet.microsoft.com/en-us/download) 
- [OpenJDK 11](https://learn.microsoft.com/en-us/java/openjdk/download#openjdk-11)
- [Android SDK 31](https://developer.android.com/studio) and  build tools
android phone with usb cable to run app

### to build and run
- build
```bash
dotnet msbuild /p:AndroidSdkDirectory=<path_to_AndroidSDK> EpicDungeonsRPG.csproj /verbosity:normal /t:Rebuild /t:PackageForAndroid /t:SignAndroidPackage /p:Configuration=Debug
```
- run
plug a phone to pc with usb and go to `bin\Debug\net<dotnet-sdk-version>-android` 
```bash
adb install -r EpicDungeonsRPG.EpicDungeonsRPG-Signed.apk
```
- new app should appear on your phone