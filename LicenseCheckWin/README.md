# LicenseCheckWin

## 📌 项目简介

LicenseCheckWin 是一个基于 WPF 的软件授权校验工具，支持 Windows 桌面环境。它通过集成 ILicense2 授权核心库，实现授权文件的自动校验、设备绑定、弹窗注册等功能。适用于需要在客户端进行本地授权验证的应用场景。

---

## 📂 项目结构

```
LicenseCheckWin/
├── LicenseCheckMain.cs         # 授权校验主入口
├── WinRegisiter.xaml           # 注册弹窗界面
├── WinRegisiter.xaml.cs        # 注册弹窗逻辑
├── LicenseCheckWin.csproj      # 项目文件
└── bin/ obj/ Properties/       # 编译输出与资源
```

- **LicenseCheckMain.cs**：授权校验主逻辑，自动读取授权文件并校验。
- **WinRegisiter.xaml / .cs**：弹窗注册界面与交互逻辑，支持手动输入授权码。
- **LicenseCheckWin.csproj**：项目配置，支持 .NET Framework 4.8 和 .NET 8.0。

---

## ⚙️ 功能说明

- **授权文件自动校验**：自动读取本地授权文件（默认 `license.lic`），校验有效性。
- **设备绑定**：通过采集硬件信息生成唯一注册码，实现授权与设备绑定。
- **弹窗注册**：授权文件无效或缺失时，弹窗提示用户输入授权码。
- **与 ILicense2 集成**：调用 ILicense2 的授权生成与校验 API。
- **多平台支持**：兼容 .NET Framework 4.8 和 .NET 8.0 Windows 桌面。

---

## 🚀 安装与运行

### 运行环境要求

- Windows 10/11
- .NET Framework 4.8 或 .NET 8.0 Windows 桌面运行时
- 推荐使用 Visual Studio 2022 及以上

### 安装步骤

1. 克隆项目到本地：
   ```powershell
   git clone https://github.com/skybc/ILicense.git
   ```
2. 打开 `ILicense.sln` 解决方案。
3. 还原 NuGet 包依赖。
4. 编译并运行 `LicenseCheckWin` 项目。

### 启动方法

- 在 Visual Studio 中设置 `LicenseCheckWin` 为启动项目，点击“启动”即可运行。
- 或在 `bin/Debug/net48/` 或 `bin/Debug/net8.0-windows/` 目录下双击 `LicenseCheckWin.exe` 运行。

---

## 🛠️ 配置说明

- **授权文件路径**：默认 `license.lic`，可通过参数自定义。
- **依赖库**：需引用 ILicense2 授权核心库。
- **多平台配置**：项目文件已配置支持多目标框架。

---

## 🧪 使用示例

### 1. 授权校验主入口

```csharp
// 校验授权文件
var secretKey = LicenseCheckMain.CheckLicense(privateKey, softcode);
if (secretKey != null) {
    // 授权有效
}
```

### 2. 弹窗注册流程

- 当授权文件无效或缺失时，自动弹出注册窗口，用户输入授权码后完成校验与写入。

---

## 🔧 开发与贡献

### 参与开发

1. Fork 仓库并提交 Pull Request。
2. 建议分支命名：`feature/xxx`、`bugfix/xxx`。
3. 提交信息建议格式：`[模块] 描述`。

### 代码风格

- C# 代码遵循 Microsoft 官方编码规范。
- WPF 界面采用标准布局与命名。
- 重要逻辑需添加 XML 注释。

### 贡献流程

1. Issue 讨论需求或问题。
2. 分支开发，提交 PR。
3. 由项目维护者审核合并。

---

如需更多帮助，请查阅源码或提交 Issue。
