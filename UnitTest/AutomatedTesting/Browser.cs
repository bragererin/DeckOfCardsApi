using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace UnitTest.AutomatedTesting
{
    public class Browser
    {
        IWebDriver webDriver;
        ChromeOptions options;
        public void Init_Browser()
        {
            options = new ChromeOptions();
            string path = @"C:\Program Files (x86)\Google\Chrome\Application";
            webDriver = new ChromeDriver(path, options);
            webDriver.Manage().Window.Maximize();
        }

        public string Title
        {
            get { return webDriver.Title; }
        }
        public void Goto(string url)
        {
            webDriver.Url = url;
        }
        public void Close()
        {
            webDriver.Quit();
        }
        public IWebDriver getDriver
        {
            get { return webDriver; }
        }
    }
}
