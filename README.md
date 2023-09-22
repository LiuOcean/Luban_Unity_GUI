# README

[![license](http://img.shields.io/badge/license-MIT-blue.svg)](https://opensource.org/licenses/MIT)

## 项目依赖

项目中需要有[Odin](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041)这个工具

## 导入工程方式

- 如果使用 `UPM`可以在 `package.json` 中增加如下内容

```json
"com.liuocean.luban_unity_gui":"https://github.com/LiuOcean/Luban_Unity_GUI.git?path=Assets/"
```

- 或者直接Assets文件夹下的Editor文件夹和package.json直接复制到项目

## 创建配置文件

在 `Assets` 中，右键 `Create/Luban/ExportConfig` 即可, 或者打开 ProjectSetting/Luban, 会自动创建

## 自定义下拉菜单

在 `预配置项/选单扩展` 中, 你可以自由定义 key 和 value, 工具提供了 Luban 默认的实现, 如果你的项目有自定义的功能按需修改即可

在 `参数配置/用户自定义额外参数` 中, 下拉菜单的数据都来自于选单扩展中的配置, 最后会生成 `-x xxxkey=value` 或者 `-x xxxkey` 

![image](https://github.com/LiuOcean/Luban_Unity_GUI/raw/main/Pics/GUI_Display.png)
![image](https://github.com/LiuOcean/Luban_Unity_GUI/raw/main/Pics/GUI_Display_2.png)
