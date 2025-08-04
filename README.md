# ILicense 软件授权系统

## 📌 项目简介

ILicense 是一套完整的软件授权解决方案，包含授权核心库（ILicense2）、授权生成工具（LicenseCreate）、授权校验工具（LicenseCheckWin）。系统支持 RSA 加密、设备绑定、授权文件生成与校验，适用于 Windows 桌面软件、服务端、跨平台应用的授权管理。

---

## 📂 项目结构

```
ILicense/
├── ILicense2/           # 授权核心库（.NET Standard）
│   ├── LicenseManage.cs
│   ├── RSACryption.cs
│   ├── MachineHelper.cs
│   ├── Entity/
│   └── README.md
├── LicenseCreate/       # 授权生成工具（WPF 桌面应用）
│   ├── MainWindow.xaml
│   ├── MainWindow.xaml.cs
│   └── README.md
├── LicenseCheckWin/     # 授权校验工具（WPF 桌面应用）
│   ├── LicenseCheckMain.cs
│   ├── WinRegisiter.xaml
│   ├── WinRegisiter.xaml.cs
│   └── README.md
├── ILicense.sln         # 解决方案文件
└── README.md            # 项目总览说明
```

- **ILicense2/**：授权核心库，提供密钥生成、授权生成、授权校验、设备绑定等 API。
- **LicenseCreate/**：授权生成工具，图形界面生成授权密钥，适合管理员或开发者使用。
- **LicenseCheckWin/**：授权校验工具，集成于客户端软件，实现本地授权校验与弹窗注册。

---

## ⚙️ 主要功能

- **RSA 密钥生成与加解密**：安全生成公钥/私钥，授权信息加密存储。
- **授权文件生成与校验**：支持永久/限时授权，设备绑定，授权信息本地存储与校验。
- **设备信息采集**：自动采集 CPU、主板、硬盘、MAC 等信息生成唯一注册码。
- **弹窗注册与交互**：授权无效时弹窗提示，支持手动输入授权码。
- **多平台支持**：核心库兼容 .NET Standard，工具支持 .NET Framework 4.8 和 .NET 8.0。

---

## 🚀 安装与运行

### 运行环境要求

- Windows 10/11
- .NET Framework 4.8 或 .NET 8.0 桌面运行时
- Visual Studio 2019/2022 及以上

### 安装步骤

1. 克隆项目到本地：
   ```powershell
   git clone https://github.com/skybc/ILicense.git
   ```
2. 打开 `ILicense.sln` 解决方案。
3. 还原 NuGet 包依赖。
4. 编译并运行各子项目。

### 启动方法

- 在 Visual Studio 中分别设置 `LicenseCreate` 或 `LicenseCheckWin` 为启动项目，点击“启动”即可运行。
- 或在对应 `bin/Debug/` 目录下双击 `.exe` 文件运行。

---

## 🛠️ 配置说明

- **授权文件路径**：默认 `license.lic`，可自定义。
- **依赖库**：Newtonsoft.Json、System.Management、Microsoft.Win32.Registry 等。
- **多平台配置**：项目文件已配置支持多目标框架。
- **授权参数**：可通过 `LicenseConfig` 自定义软件代号、客户名、授权类型、到期时间、公钥/私钥等。

---

## 🧪 使用示例

### 1. 生成密钥与授权

```csharp
// 生成密钥
var (privatekey, publickey) = ILicense2.LicenseManage.CreateRsaKey();

// 生成授权
var config = new LicenseConfig {
	SoftCode = "MyApp",
	CustomerName = "客户A",
	PublicKey = publickey,
	Due = DateTime.Now.AddYears(1)
};
string registration = ILicense2.LicenseManage.GetRegistration();
string license = ILicense2.LicenseManage.GetLicense(config, registration, "自定义数据");
```

### 2. 校验授权

```csharp
var result = ILicense2.LicenseManage.Verify(privatekey, config.SoftCode, registration, license);
if (result.result == "ok") {
	// 授权有效
}
```

### 3. 客户端授权校验

```csharp
var secretKey = LicenseCheckWin.LicenseCheckMain.CheckLicense(privateKey, softcode);
if (secretKey != null) {
	// 授权有效
}
```

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

如需更多帮助，请查阅各子项目 README 或提交 Issue。