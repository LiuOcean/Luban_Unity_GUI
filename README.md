# README

## 使用方式

- 首先，项目中必须要有 `Odin` 的基础代码
- 如果使用 `UPM`可以在 `package.json` 中增加如下内容

```json
"com.liuocean.luban_gui":"https://github.com/LiuOcean/Luban_Unity_GUI.git?path=Assets/"
```

## 创建配置文件

在 `Assets` 中，右键 `Create/Luban/ExportConfig` 即可

如果项目中需要多种配置，可以考虑创建多个配置文件，加载好后，调用 `Gen` 来生成

## 生命周期

在项目 `Editor` 代码定义继承 `IBeforeGen` 和 `IAfterGen`

![image](https://github.com/LiuOcean/Luban_Unity_GUI/raw/main/Pics/GUI_Display.png)