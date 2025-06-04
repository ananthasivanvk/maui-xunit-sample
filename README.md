# MauiBmiCalculator

This is a .NET 8 MAUI application for calculating BMI, targeting iOS and Android only. The app uses the MVVM pattern for separation of concerns and testability. Unit tests for the ViewModel are provided in the `MauiBmiCalculator.Tests` project using xUnit.

## How to Build and Run

1. **Build the solution:**
   ```sh
   dotnet build
   ```
2. **Run the app:**
   - For iOS: `dotnet build -t:Run -f net8.0-ios`
   - For Android: `dotnet build -t:Run -f net8.0-android`

3. **Run the tests in terminal:**
   ```sh
   dotnet test ../MauiBmiCalculator.Tests/MauiBmiCalculator.Tests.csproj
   ```

## Project Structure
- `ViewModels/` — Contains the BMI calculation logic.
- `Views/` — Contains the UI pages.
- `Models/` — (Optional) For data models.
- `MauiBmiCalculator.Tests/` — xUnit tests for ViewModels.

## Notes
- Only iOS and Android targets are supported.
- MVVM pattern is used for testability.

---

### Approach for xUnit Testing

To enable robust unit testing of shared ViewModel logic in a .NET MAUI solution—while avoiding common pitfalls such as duplicate `TargetFrameworkAttribute` errors, XAML type resolution issues, or test runner incompatibilities—the following approach was adopted:

#### 1. **Single Source of Truth for ViewModel Logic**
- The ViewModel (`BmiViewModel.cs`) is placed in the `ViewModels/` folder of the main MAUI project.
- This ensures that both the application and the test project use the exact same implementation, eliminating code duplication and synchronization problems.

#### 2. **No Core or Shared Project**
- No separate Core or shared project is used. This avoids issues with multi-targeting, duplicate attributes, and test runner incompatibilities, especially on macOS where `.shproj` is not supported by the .NET CLI.

#### 3. **xUnit Test Project Uses File Link**
- The xUnit test project includes the ViewModel file via a file link in its `.csproj`:
  ```xml
  <Compile Include="..\MauiTest\ViewModels\BmiViewModel.cs" Link="ViewModels\BmiViewModel.cs" />
  ```
- This method allows the test project to compile and test the same ViewModel logic as the app, without referencing the entire MAUI project or introducing platform dependencies.

#### 4. **No Exclusion of ViewModels in the App Project**
- The main MAUI project’s `.csproj` does not contain any `<Compile Remove="ViewModels/*.cs" />` or similar exclusions. This ensures the ViewModel is included in the app build and available for XAML binding.

#### 5. **Consistent Namespaces**
- Both the app and the test project use the same namespace for the ViewModel (e.g., `MauiBmiCalculator.ViewModels`), ensuring type resolution works in both C# and XAML.

#### 6. **Clean XAML Usage**
- The XAML references the ViewModel with a namespace declaration such as:
  ```xml
  xmlns:viewModels="clr-namespace:MauiBmiCalculator.ViewModels"
  ```
  and instantiates the ViewModel as:
  ```xml
  <viewModels:BmiViewModel />
  ```
- This setup allows the XAML parser to resolve the ViewModel type without errors.

#### 7. **No Unnecessary Files or Projects**
- Any unused shared project files (e.g., `.shproj` and its folder) are removed from the solution to prevent confusion and build issues.

---

### Step-by-Step Guide for Adoption

1. **Place all shared ViewModel logic in the main MAUI project’s `ViewModels/` folder.**
2. **In the xUnit test project, add a file link to the ViewModel file(s):**
   ```xml
   <Compile Include="..\PathToMauiProject\ViewModels\BmiViewModel.cs" Link="ViewModels\BmiViewModel.cs" />
   ```
3. **Ensure the main MAUI project’s `.csproj` does not exclude the ViewModel files from compilation.**
4. **Use consistent namespaces for ViewModels in both projects.**
5. **Reference the ViewModel in XAML using the correct `clr-namespace` syntax.**
6. **Build and run both the MAUI app and the xUnit tests to verify that:**
   - The app builds and runs on all target platforms (Android, iOS, etc.).
   - The test project builds and all tests pass, confirming the shared logic is testable and correct.

---

### Benefits

- **No duplicate attribute or type errors.**
- **No platform-specific test runner issues.**
- **Single source of truth for business logic.**
- **Easy maintenance and high test coverage.**
- **Works seamlessly on macOS and Windows.**

This approach is recommended for any .NET MAUI project that requires unit testing of shared ViewModel logic without introducing cross-platform build or test complications.
