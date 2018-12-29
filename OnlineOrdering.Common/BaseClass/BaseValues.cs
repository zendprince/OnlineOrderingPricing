using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OnlineOrdering.Common.Enums;
using System;

namespace OnlineOrdering.Common.BaseClass
{
    internal static class BaseValues
    {
        internal static string BrowserTarget { get; set; }

        internal static string Protocol { get; set; }

        internal static string PathReportFile { get; set; }

        internal static string ApplicationDomain { get; set; }

        internal static bool IsAutoLogin { get; set; }

        internal static int PageloadTimeouts { get; set; }

        internal static bool SaveImageAsBase64 { get; set; }

        internal static IWebDriver DriverSession { get; set; }

        internal static IWebDriver InitDriverSession
        {
            get
            {
                if (BrowserTarget.Equals(BrowserType.Chrome.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    ChromeOptions driver = new ChromeOptions();
                    return new ChromeDriver(driver);
                }
                else if (BrowserTarget.Equals(BrowserType.Firefox.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    FirefoxOptions driver = new FirefoxOptions();
                    return new FirefoxDriver(driver);
                }
                else if (BrowserTarget.Equals(BrowserType.InternetExplorer.ToString(), StringComparison.InvariantCultureIgnoreCase))
                { // For IE

                    InternetExplorerOptions driver = new InternetExplorerOptions();
                    return new InternetExplorerDriver(driver);
                }
                else
                    throw new ArgumentException("You need to set a valid browser type. It should Chrome/Firefox/InternetExplorer.");

            }

        }

        private static readonly Lazy<ExtentReports> _lazy = new Lazy<ExtentReports>(() => new ExtentReports());

        internal static ExtentReports ExtentReports { get { return _lazy.Value; } }
    }
}
