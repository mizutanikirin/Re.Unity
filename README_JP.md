README ([English](https://github.com/mizutanikirin/Re.Unity/blob/main/README.md) | Japanease)
# Re.Unity
![reUnity](https://user-images.githubusercontent.com/4795806/104938985-2e153980-59f3-11eb-8442-9ae096303a6f.png)

## 概要
このアプリはUnityを強制的に終了させ、UnityHubを起動するWindwos10用アプリケーションです。  
Unityが応答不能になったときなどに使用するとスピーディーにプロジェクトを再起動することができます。  
[Download ver1.0.0](https://github.com/mizutanikirin/Re.Unity/releases/tag/ver1.0.0)

## 使用方法
1. [ここ](https://github.com/mizutanikirin/Re.Unity/releases/tag/ver1.0.0)から最新のzipをダウンロードしてReUnity.exeを実行します。  
2. 一覧から強制終了させたいUnityを選択しボタンを押してください。  
※ スピーディーにUnityを終了させるためのツールであるため、終了時に確認ダイアログは表示されませんのでご注意ください。

## 設定方法
アプリ右上の設定ボタンを押すと設定ウィンドウが表示されそこでアプリ設定ができます。  
UnityHubPath欄にUnity Hubのexeを指定するとUnity Editor強制終了後にUnity Hubが起動します。その欄が空のときはUnityEditorを終了させた後何も起こりません。その欄を空にしたい場合はTextFeild右にある消しゴムボタンを押してください。
![reUnitySetting](https://user-images.githubusercontent.com/4795806/104939002-353c4780-59f3-11eb-9133-49ea35e7b85c.png)

## 開発環境/使用Asset
- Windows 10 Pro
- Unity 2019.4.16f1
- [KirinUtil](https://github.com/mizutanikirin/KirinUtil)
- [iTween](https://assetstore.unity.com/packages/tools/animation/itween-84)
- [UnityStandaloneFileBrowser](https://github.com/gkngkc/UnityStandaloneFileBrowser)

## License
[MIT License](https://github.com/mizutanikirin/Re.Unity/blob/main/LICENSE)