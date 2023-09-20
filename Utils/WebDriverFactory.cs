// Purpose: Factory class for creating WebDriver instances
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;


namespace Selenium_Test;
public static class WebDriverFactory
{
    public static IWebDriver CreateWebDriver()
    {

        HostApplicationBuilder builder = Host.CreateApplicationBuilder();

        builder.Configuration.Sources.Clear();

        IHostEnvironment env = builder.Environment;

#if DEBUG
    env.EnvironmentName = "development";
#else
    env.EnvironmentName = "staging";
#endif

        Console.WriteLine($"Environment: {env.EnvironmentName}");

        IConfigurationRoot config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json")
            .Build();


        // string? driverTypeStr = Environment.GetEnvironmentVariable("DriverType");
        string? driverTypeStr = config["DriverType"];
        // string? driverTypeStr = ConfigurationManager.AppSettings["DriverType"];

        if (!Enum.TryParse(driverTypeStr, out BrowserType browserType))
        {
            throw new ArgumentException("Invalid or missing DriverType in configuration");
        }

        IWebDriver driver;

        switch (browserType)
        {
            case BrowserType.Chrome:
                new DriverManager().SetUpDriver(new ChromeConfig());
                driver = new ChromeDriver();
                break;
            case BrowserType.Firefox:
                new DriverManager().SetUpDriver(new FirefoxConfig());
                driver = new FirefoxDriver();
                break;
            case BrowserType.Edge:
                new DriverManager().SetUpDriver(new EdgeConfig());
                driver = new EdgeDriver();
                break;
            // Add support for other browsers as needed
            default:
                throw new ArgumentOutOfRangeException(nameof(browserType), browserType, null);
        }

        return driver;
    }
}

public enum BrowserType
{
    Chrome,
    Firefox,
    Edge
}