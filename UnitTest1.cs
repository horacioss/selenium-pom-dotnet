using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium_Test.Pages;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Selenium_Test;

public class Tests
{
    BasePage page;

    [SetUp]
    public void Setup()
    {
        page = new();
        page.InitializeWebDriver();
        page.NavigateTo("https://www.google.com/");
    }

    [Test]
    public void Pilot()
    {
        
        Assert.That(page.GetPageTatile().Contains("Google"), Is.EqualTo(true),
            "The title doesn't contain the search term");
    }

    [TearDown]
    public void TearDown()
    {
        page.Quit();
    }
}