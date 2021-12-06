# 介绍

Win32API的.NET下封装，包含
1: 常用win32的API的net封装
2：鼠标、键盘、热键hook钩子模块，
3：模拟键盘输入文字（支持各种字符文字、不同语言的文字）、模拟鼠标点击移动滚动等操作
4：延迟函数Delay方法

# 版本更新

#### v1.0.1

添加net40;net45;net48,暂时移除net6框架

# 安装教程

1. Nuget搜索win32net,安装即可

# 使用说明

Win32命名空间下包含各种常用api，如：win32.User32.GetDesktopWindow()//获取桌面窗口句柄；

Win32.Hooks，命名空间下包含鼠标键盘热荐的钩子相关类。 
例如鼠标钩子：
Win32.Hooks.MouseHook mouseHook = new Hooks.MouseHook();//实例化鼠标钩子对象

mouseHook.LeftDown += MouseHook_LeftDown;//鼠标左键按下的事件监听回调方法

mouseHook.Start();//开始监听

mouseHook.Stop();//停止监听

 Win32.Input.Keyboard.KeyDown(System.Windows.Forms.Keys.Enter);//按回车键

Win32.Input.Mouse.LeftClick(150, 100);//鼠标左键单击

Win32.Input.Wait.Delay(1500);//延迟1500毫秒

 Win32.Input.Keyboard.Type("输入文字1234abc");//模拟输入文字，支持各种语言文字字符










