````markdown
# OgistoInjection

**OgistoInjection** is a lightweight .NET library that provides automatic dependency injection (DI) registration using custom attributes. No more manual service registration—just decorate your classes and call one method!

📦 **[View on NuGet](https://www.nuget.org/packages/OgistoInjection)**

## 🧩 Features

- ✅ Supports `Scoped`, `Singleton`, and `Transient` lifetimes
- ✅ Auto-registers interfaces and/or classes with minimal setup
- ✅ Clean and simple API using `[Injectable]` attribute
- ✅ Works with `Microsoft.Extensions.DependencyInjection`

## 📦 Installation

Install the NuGet package:

```bash
dotnet add package OgistoInjection
````

## 🚀 Getting Started

### 1. Add the `[Injectable]` attribute to your class

```csharp
using OgistoInjection;

[Injectable(DITypes.Scoped)]
public class MyService : IMyService
{
    // Your logic
}
```

Or without an interface:

```csharp
[Injectable(DITypes.Singleton)]
public class BackgroundJobRunner
{
    // No interface needed
}
```

### 2. Call `AddAutoDiServices()` in your `Program.cs`

```csharp
using OgistoInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoDiServices(); // Automatically scans and registers services

var app = builder.Build();
```

> ℹ️ Optionally, you can pass a specific `Assembly` to scan:

```csharp
builder.Services.AddAutoDiServices(typeof(MyService).Assembly);
```

## 🧠 How It Works

OgistoInjection scans your assembly for all classes marked with `[Injectable]`. Based on the `DITypes` specified (`Scoped`, `Singleton`, `Transient`), it registers the class (and its interfaces if any) in the DI container.

### Example

```csharp
[Injectable(DITypes.Singleton)]
public class LoggingService : ILoggingService
{
    // Implementation
}
```

This will register:

```csharp
services.AddSingleton<ILoggingService, LoggingService>();
```

## 🛠️ Dependencies

* `Microsoft.Extensions.DependencyInjection`
* .NET 6.0+ (works on .NET 7 and .NET 8 too)

## 📄 License

MIT License – use it freely.

> 📝 This is the first version of **OgistoInjection**. More features are planned in future updates.

---

Made by Ghassene Ouadia.

