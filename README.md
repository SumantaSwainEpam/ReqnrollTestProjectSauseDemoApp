# Reqnroll Test Automation Framework for Sauce Demo App

## Author
Sumanta Swain (EPAM Systems)

## Overview
This is a Reqnroll-based test automation framework for the Sauce Demo application. The framework is built using .NET 8, Selenium WebDriver, and follows the Page Object Model design pattern.

## Project Structure

```
ReqnrollTestProjectSauseDemoApp/
├── Credentials/
│   ├── AppConfig.json         # Configuration settings
│   └── CredentialsManager.cs  # Configuration management
├── Drivers/
│   ├── ChromeDriverFactory.cs # Chrome driver implementation
│   ├── EdgeDriverFactory.cs   # Edge driver implementation
│ ├── FirefoxDriverFactory.cs# Firefox driver implementation
│   ├── IDriverFactory.cs      # Driver factory interface
│   └── WebDriverFactory.cs    # WebDriver management
├── Features/
│   ├── Login.feature    # Login feature specifications
│   └── Logout.feature        # Logout feature specifications
├── Helper/
│   └── SeleniumHelper.cs     # Common Selenium operations
├── Hooks/
│   └── Hook.cs           # Test execution hooks
├── Pages/
│   └── LoginPage.cs         # Login page object model
└── StepDefinitions/
    └── LoginStepDefinitions.cs # Step implementations
```

## Features
- Multi-browser support (Chrome, Firefox, Edge)
- Configuration-driven testing
- Page Object Model implementation
- Fluent assertions for verifications
- Common Selenium operation abstractions
- Thread-safe WebDriver management

## Prerequisites
- .NET 8 SDK
- Visual Studio 2022 or later
- Supported browsers (Chrome, Firefox, Edge)
- Selenium WebDriver

## Configuration
The framework uses `AppConfig.json` for configuration:
```json
{
  "TestSettings": {
    "BaseUrl": "https://www.saucedemo.com/",
    "Username": "standard_user",
    "Password": "secret_sauce",
    "BrowserTypes": [ "Chrome", "Firefox", "Edge" ]
  }
}
```

## Key Components

### WebDriver Management
- Factory pattern for browser initialization
- Thread-safe WebDriver instance handling
- Automatic cleanup of browser sessions

### Page Objects
- Encapsulated UI elements and operations
- Reusable page methods
- Clean separation of concerns

### Helpers
- Centralized Selenium operations
- Wait strategies
- Common UI interactions

### Step Definitions
- Gherkin step implementations
- Business logic integration
- Page object utilization

## Running Tests
1. Open the solution in Visual Studio
2. Build the solution
3. Open Test Explorer
4. Run desired tests

## Writing Tests
1. Create feature files in the Features folder
2. Implement step definitions in StepDefinitions folder
3. Create page objects for new pages in Pages folder
4. Update AppConfig.json if needed

## Best Practices
- Use page objects for element interactions
- Maintain single responsibility principle
- Use configuration for test data
- Implement proper wait strategies
- Follow Reqnroll best practices

## Contributing
1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License
[MIT]