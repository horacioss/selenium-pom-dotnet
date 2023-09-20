

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Selenium_Test.Pages;

public class BasePage
{
    protected IWebDriver? Driver { get; set; }
    protected WebDriverWait? Wait { get; set; }

    public void InitializeWebDriver()
    {
        Driver = WebDriverFactory.CreateWebDriver();
        Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
    }

    public void NavigateTo(string url)
    {
        Driver!.Navigate().GoToUrl(url);
    }


    public string GetText(By locator)
    {
        return Driver!.FindElement(locator).Text;
    }


    public void Type(By locator, string text)
    {
        Driver!.FindElement(locator).SendKeys(text);
    }

    public void Click(By locator)
    {
        Driver!.FindElement(locator).Click();
    }

    public string GetPageTatile()
    {
        return Driver!.Title.ToString();
    }

    public void Quit()
    {
        Driver!.Quit();
    }

}
