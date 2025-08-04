# ILicense2

## 📌 项目简介

ILicense2 是一个基于 .NET Standard 2.0 的软件授权核心库，支持 RSA 加密、授权密钥生成、授权校验、设备绑定等功能。适用于需要灵活授权管理的桌面、服务端或跨平台应用。该库可独立集成到各类 .NET 项目中，帮助开发者实现安全、高效的软件授权机制。

---

## 📂 项目结构

```
ILicense2/
├── LicenseManage.cs         # 授权管理核心类
├── MachineHelper.cs         # 设备信息采集工具
├── RSACryption.cs           # RSA 加解密与签名工具
├── Entity/
│   ├── ClientType.cs        # 客户端类型枚举
│   ├── LicenseConfig.cs     # 授权配置模型
│   ├── LicenseType.cs       # 授权类型枚举
│   ├── SecretKey.cs         # 授权密钥数据结构
├── ILicense2.csproj         # 项目文件
├── ILicense2.nuspec         # NuGet 包描述
├── packages.config          # 依赖包列表
└── Properties/
    ├── Resources.Designer.cs
    ├── Resources.resx
```

- **LicenseManage.cs**：授权生成、校验、设备绑定等核心逻辑。
- **RSACryption.cs**：RSA 密钥生成、加解密、签名与验签。
- **MachineHelper.cs**：采集 CPU、主板、硬盘、MAC 等设备信息，用于生成唯一注册码。
- **Entity/**：授权相关的数据模型与枚举类型。

---

## ⚙️ 功能说明

- **RSA 密钥生成与加解密**：支持 1024 位密钥对生成，授权信息加密存储。
- **授权密钥生成**：根据授权配置、设备码等参数生成加密授权密钥。
- **授权校验**：校验密钥有效性、授权期限、设备绑定等。
- **设备绑定**：通过采集硬件信息生成唯一注册码，实现授权与设备绑定。
- **文件授权与本地缓存**：支持授权文件读写与本地缓存。
- **授权类型支持**：支持永久授权、限时授权等多种授权类型。

模块化设计：
- `LicenseManage`：授权生成、校验、设备绑定等核心方法。
- `RSACryption`：RSA 加密、解密、签名、验签工具。
- `MachineHelper`：硬件信息采集。
- `Entity`：授权相关数据结构。

---

## 🚀 安装与运行

### 运行环境要求

- .NET Standard 2.0 及以上（兼容 .NET Framework 4.6.1+、.NET Core 2.0+、.NET 5/6/8+）
- 推荐使用 Visual Studio 2019/2022

### 依赖库

- Microsoft.Win32.Registry
- Newtonsoft.Json
- System.Management

### 安装步骤

1. 通过 NuGet 安装（如已发布）：
   ```powershell
   Install-Package ILicense2
   ```
2. 或将源码直接集成到你的项目中。

### 启动方法

- 在项目中引用 `ILicense2`，调用相关 API 即可。

---

## 🛠️ 配置说明

- **LicenseConfig**（`Entity/LicenseConfig.cs`）：授权配置模型，包含软件代号、客户名、授权类型、到期时间、公钥/私钥等。
- **SecretKey**（`Entity/SecretKey.cs`）：授权密钥数据结构，包含授权信息、设备码、随机码等。
- **授权文件路径**：默认 `license.lic`，可自定义。

如需适配不同授权场景，可自定义 `LicenseConfig` 参数。

---

## 🧪 使用示例

### 1. 生成 RSA 密钥对

```csharp
var (privatekey, publickey) = LicenseManage.CreateRsaKey();
```

### 2. 生成授权密钥

```csharp
var config = new LicenseConfig {
    SoftCode = "MyApp",
    CustomerName = "客户A",
    PublicKey = publickey,
    Due = DateTime.Now.AddYears(1)
};
string registration = LicenseManage.GetRegistration();
string license = LicenseManage.GetLicense(config, registration, "自定义数据");
```

### 3. 校验授权密钥

```csharp
var result = LicenseManage.Verify(privatekey, config.SoftCode, registration, license);
if (result.result == "ok") {
    // 授权有效
}
```

### 4. 设备码采集

```csharp
string regCode = LicenseManage.GetRegistration(); // 采集设备唯一码
```

---

## 🔧 开发与贡献

### 参与开发

1. Fork 仓库并提交 Pull Request。
2. 建议分支命名：`feature/xxx`、`bugfix/xxx`。
3. 提交信息建议格式：`[模块] 描述`。

### 代码风格

- C# 代码遵循 Microsoft 官方编码规范。
- 重要逻辑需添加 XML 注释。

### 贡献流程

1. Issue 讨论需求或问题。
2. 分支开发，提交 PR。
3. 由项目维护者审核合并。

---

如需更多帮助，请查阅源码或提交 Issue。
