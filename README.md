# README

## 项目依赖

项目中需要有[Odin](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041)这个工具

## 导入工程方式

- 如果使用 `UPM`可以在 `package.json` 中增加如下内容

```json
"com.liuocean.luban_unity_gui":"https://github.com/LiuOcean/Luban_Unity_GUI.git?path=Assets/"
```

- 或者直接Assets文件夹下的Editor文件夹和package.json直接复制到项目



## 创建配置文件

在 `Assets` 中，右键 `Create/Luban/ExportConfig` 即可

如果项目中需要多种配置，可以考虑创建多个配置文件，加载好后，调用 `Gen` 来生成

## 生命周期

在项目 `Editor` 代码定义继承 `IBeforeGen` 和 `IAfterGen`

![image](https://github.com/LiuOcean/Luban_Unity_GUI/raw/main/Pics/GUI_Display.png)
