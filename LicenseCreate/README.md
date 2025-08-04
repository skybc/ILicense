# LicenseCreate

## 📌 项目简介

LicenseCreate 是一款用于软件授权密钥生成与管理的桌面应用程序，基于 WPF 技术开发。它支持 RSA 密钥对生成、授权文件生成、授权校验等功能，适用于需要对软件进行授权管理的开发者和企业。通过图形界面，用户可便捷地生成、校验和管理授权密钥，适用于 Windows 平台的客户端软件授权场景。

---

## 📂 项目结构

```
LicenseCreate/
├── App.config                # 应用程序配置文件
├── App.xaml                 # WPF 应用入口
├── App.xaml.cs              # 应用启动逻辑
├── FodyWeavers.xml          # Fody 配置
├── FodyWeavers.xsd          # Fody 配置描述文件
├── LicenseCreate.csproj     # 项目文件
├── MainWindow.xaml          # 主窗口界面
├── MainWindow.xaml.cs       # 主窗口逻辑
├── packages.config          # NuGet 包依赖
├── 密钥管理系统 [48x48].ico # 应用图标
├── bin/                     # 编译输出目录
├── obj/                     # 编译中间文件
└── Properties/
    ├── Resources.Designer.cs
    ├── Resources.resx
    ├── Settings.Designer.cs
    └── Settings.settings
```

- **MainWindow.xaml / MainWindow.xaml.cs**：主界面与交互逻辑，包含密钥生成、授权生成、授权校验等功能按钮。
- **App.config**：配置 .NET 运行环境。
- **FodyWeavers.xml**：自动嵌入依赖库的配置。
- **Properties/**：资源与设置相关文件。

---

## ⚙️ 功能说明

- **RSA密钥对生成**：一键生成公钥和私钥，支持授权加密。
- **授权文件生成**：根据软件代号、客户信息、到期时间等参数生成授权密钥文件。
- **授权校验**：支持本地授权文件校验，验证密钥有效性与授权信息。
- **永久/限时授权**：支持选择永久授权或限时授权。
- **设备绑定**：可选绑定注册码，实现设备级授权。
- **界面友好**：集成 SweetAlertSharp 弹窗提示，提升用户体验。

模块化设计：
- `ILicense2.LicenseManage`：核心授权逻辑，包括密钥生成、授权生成、授权校验等。
- `LicenseCheckWin.LicenseCheckMain`：授权校验辅助模块。

---

## 🚀 安装与运行

### 运行环境要求

- Windows 10/11
- .NET 8.0 桌面运行时（或 .NET Framework 4.8，兼容配置见 App.config）
- 推荐使用 Visual Studio 2022 及以上

### 依赖库

- Newtonsoft.Json
- SweetAlertSharp
- Fody & Costura.Fody
- System.Management
- System.Security.AccessControl

### 安装步骤

1. 克隆项目到本地：
   ```powershell
   git clone https://github.com/skybc/ILicense.git
   ```
2. 打开 `ILicense.sln` 解决方案。
3. 还原 NuGet 包依赖。
4. 编译并运行 `LicenseCreate` 项目。

### 启动方法

- 在 Visual Studio 中设置 `LicenseCreate` 为启动项目，点击“启动”即可运行。
- 或在 `bin/Debug/net8.0-windows/` 目录下双击 `LicenseCreate.exe` 运行。

---

## 🛠️ 配置说明

- **App.config**：指定 .NET 运行时版本，默认支持 .NETFramework,Version=v4.8。
- **FodyWeavers.xml**：自动嵌入依赖库，无需手动配置。
- **密钥管理系统 [48x48].ico**：应用图标，可替换为自定义图标。

如需适配不同环境，可修改 `App.config` 的 `<supportedRuntime>` 节点，或在项目属性中调整目标框架。

---

## 🧪 使用示例

### 1. 生成 RSA 密钥对

在主界面点击“密钥生成”按钮，自动生成公钥和私钥，显示在文本框中。

### 2. 生成授权密钥

填写软件代号、客户信息、到期时间、注册码等信息，点击“授权生成”按钮，生成授权密钥并显示。

### 3. 校验授权密钥

填写私钥、软件代号、授权密钥等信息，点击“授权校验”按钮，弹窗提示校验结果。

#### 代码调用示例

```csharp
// 生成密钥
var (privatekey, publickey) = ILicense2.LicenseManage.CreateRsaKey();

// 生成授权
var license = ILicense2.LicenseManage.GetLicense(config, registration, data);

// 校验授权
var result = ILicense2.LicenseManage.Verify(privatekey, softcode, regcode, license);
if (result.result == "ok") {
    // 授权有效
}
```

---

## 🔧 开发与贡献

### 参与开发

1. Fork 仓库并提交 Pull Request。
2. 建议使用 `feature/xxx`、`bugfix/xxx` 等分支命名规范。
3. 提交信息需简明扼要，建议格式：`[模块] 描述`。

### 代码风格

- C# 代码遵循 Microsoft 官方编码规范。
- XAML 采用标准 WPF 布局与命名。
- 重要逻辑需添加 XML 注释。

### 贡献流程

1. Issue 讨论需求或问题。
2. 分支开发，提交 PR。
3. 由项目维护者审核合并。

---

如需更多帮助，请查阅源码或提交 Issue。
