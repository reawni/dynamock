# DynaMock

[![License](https://img.shields.io/badge/license-Apache%202.0-blue.svg)](LICENSE)

**DynaMock** is a lightweight .NET library for **dynamic dependency injection mock creation** and testing, especially tailored for use with **xUnit** and **Moq**. It simplifies:

- Generating mock DI containers dynamically  
- Invoking private methods for unit testing  
- Reducing boilerplate when your service has many dependencies

---

## 💡 Features

- **Automatic mock resolution:** Instantiates classes with many dependencies without manually wiring up each mock  
- **Invoke private / non-public members:** Call and test private or internal methods via reflection  
- **Integration with Moq:** Retrieve `Mock<T>` instances to set up behavior, verify calls, etc.  
- **Designed for xUnit / unit testing:** Helps reduce test setup complexity  

---

## 📦 Installation

1. Clone or add **dynamock** as a dependency in your test project.  
2. Add the project reference or install via your package manager / project file.

---

## 🚀 Usage Examples

### 1. Creating a DynaMock instance

```csharp
// Automatically creates an instance of TestService
var testService = DynaMock.NewInstance<TestService>();
```

### 2. Setting up behavior for dependencies

```csharp
// Assuming TestService depends on IRepository5
testService.GetMock<IRepository5>()
    .Setup(repo => repo.GetNumber())
    .Returns(42);
```

### 3. Calling public methods

```csharp
testService.DoSomething();  
// Your public method can run using the dynamically created mocks
```

### 4. Invoking private / non-public methods

```csharp
var result = testService.InvokeMethod("PrivateMethodName", arg1, arg2);
// `InvokeMethod` will call the private method via reflection and return the result
```

### 5. Example in an xUnit test

```csharp
public class TestServiceTests
{
    private readonly TestService _service;

    public TestServiceTests()
    {
        _service = DynaMock.NewInstance<TestService>();
    }

    [Fact]
    public void PrivateMethod_WhenCondition_ShouldReturnExpected()
    {
        // Arrange
        _service.GetMock<IRepository5>()
            .Setup(r => r.GetNumber())
            .Returns(10);

        // Act
        var result = _service.InvokeMethod("Foo_2", 1, 2);

        // Assert
        Assert.Equal(12, result);
    }
}
```

---

## 🧪 Sample Project Structure

```
/DynamicMock            # Main library project
/Sample                 # Sample / Test project demonstrating usage
  ├─ TestService.cs
  ├─ IRepository1.cs
  ├─ IRepository5.cs
  └─ TestServiceTests.cs
```

---

## 📚 How It Works

1. **Dynamic DI Container**  
   `DynaMock.NewInstance<T>()` uses reflection to create a new instance of `T` and automatically instantiates mocks for its dependencies.  
2. **Mock retrieval**  
   Each dependency is exposed as a `Mock<TDependency>` so you can configure behaviors.  
3. **Private method invocation**  
   `InvokeMethod` uses reflection to find and execute a private / internal method by name and parameter values.

---

## ✅ When to Use DynaMock

- You have a class with **many constructor dependencies**, and writing manual mocks is tedious.  
- You want to **unit test private methods** (e.g., for complex internal logic).  
- You use **xUnit + Moq** and prefer a DRY test setup.  
- You want to simplify test maintenance by reducing boilerplate.

---

## ⚠️ Limitations & Considerations

- Reflection-based invocation of private methods may **break with obfuscation** or certain access modifiers.  
- Overuse of testing non-public methods can lead to **fragile tests**; prefer testing behavior via public interfaces when feasible.  
- While dynamic DI is convenient, explicit mock registration may still be needed for **very complex dependency graphs** or non-default constructors.

---

## 🛠️ Contributing

1. Fork the repo  
2. Create a feature branch (`git checkout -b feat/YourFeature`)  
3. Commit your changes (`git commit -m 'Add new feature'`)  
4. Push to the branch (`git push origin feat/YourFeature`)  
5. Open a Pull Request  

---

## 📄 License

This project is licensed under the **Apache License 2.0**. See the [LICENSE](LICENSE) file for details.

---

## 👍 Acknowledgments

- Inspired by common needs in unit testing for **dependency injection** and **private logic coverage**  
- Built with **Moq** and **xUnit** in mind
