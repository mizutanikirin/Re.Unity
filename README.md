README (English | [Japanease](https://github.com/mizutanikirin/Re.Unity/blob/main/README_JP.md))
# Re.Unity
![reUnity](https://user-images.githubusercontent.com/4795806/104938985-2e153980-59f3-11eb-8442-9ae096303a6f.png)

## Overview
This app is an application for Windwos10 that forcibly kills Unity and starts UnityHub. 
You can use it to restart your project quickly when Unity becomes unresponsive.  
[Download ver1.0.0](https://github.com/mizutanikirin/Re.Unity/releases/tag/ver1.0.0)

## How to use
1. Download the latest zip from [here](https://github.com/mizutanikirin/Re.Unity/releases/tag/ver1.0.0) and run ReUnity.exe.
2. Select the Unity you want to force quit from the list and click the button.  (Please note that the confirmation dialog will not be displayed when you exit Unity, as this is a tool to exit Unity quickly.)

## Setup
Click the Settings button in the upper right corner of the app to open the Settings window, where you can configure the app settings.
If you specify the Unity Hub exe in the UnityHubPath field, the Unity Hub will be launched after the Unity Editor is forced to close. If the field is empty, nothing will happen after the Unity Editor is closed. If you want to leave the field empty, click the eraser button on the right side of TextFeild.
![reUnitySetting](https://user-images.githubusercontent.com/4795806/104939002-353c4780-59f3-11eb-9133-49ea35e7b85c.png)

## Development environment / assets used
- Windows 10 Pro
- Unity 2019.4.16f1
- [KirinUtil](https://github.com/mizutanikirin/KirinUtil)
- [iTween](https://assetstore.unity.com/packages/tools/animation/itween-84)
- [UnityStandaloneFileBrowser](https://github.com/gkngkc/UnityStandaloneFileBrowser)

## License
[MIT License](https://github.com/mizutanikirin/Re.Unity/blob/main/LICENSE)