using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using OpenQA.Selenium;
using OnlineOrdering.Common.BaseClass;
using OnlineOrdering.Common.Constants;
using OnlineOrdering.Common.Enums;
using OnlineOrdering.Common.Utils;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Reflection;
using NUnit.Framework.Interfaces;

namespace OnlineOrdering.Common
{
    /// <summary>
    /// This class is called automatically by NUnit, allows to run setup and/or teardown code once for all tests under the same namespace.
    /// </summary>
    [SetUpFixture]
    public class InitialSetting
    {
        // Initital Extent reports
        static InitialSetting()
        {
            DateTime now = DateTime.Now;
            BaseValues.PathReportFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Report " + now.ToString("ddd-MMM-yyyy hh-mm-ss") + ".html";
            var htmlReporter = new ExtentHtmlReporter(BaseValues.PathReportFile);
            
            htmlReporter.LoadConfig(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\extent-config.xml");
            BaseValues.ExtentReports.AttachReporter(htmlReporter);

            bool.TryParse(ConfigurationManager.AppSettings[BaseConstants.SaveImageAsBase64],out bool isBase64);
            BaseValues.SaveImageAsBase64 = isBase64;
        }

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            ExtentReportsHelper.CreateParentTest(GetType().Name);

            string domain = ConfigurationManager.AppSettings[BaseConstants.ApplicationDomain];
            string protocol = ConfigurationManager.AppSettings[BaseConstants.ApplicationProtocol];
            string browser = ConfigurationManager.AppSettings[BaseConstants.BrowserTarget];
            string userName = ConfigurationManager.AppSettings[BaseConstants.UserName];
            string password = ConfigurationManager.AppSettings[BaseConstants.Password];

            bool autoLogin;
            try
            {
                autoLogin = bool.Parse(ConfigurationManager.AppSettings[BaseConstants.IsAutoLogin]);
            }
            catch (Exception e)
            {
                autoLogin = false;
                Console.WriteLine("The information about 'IsAutoLogIn' which user input is not correct." + e.Message);
            }

            // Initial driver
            BaseValues.ApplicationDomain = domain;
            BaseValues.Protocol = protocol;
            var URL = protocol + "://" + domain;
            BaseValues.BrowserTarget = browser;
            BaseValues.IsAutoLogin = autoLogin;

            // Should kill all driver before initial new driver
            CleanUpAllDriver();

            // Invoke the browser
            BaseValues.DriverSession = BaseValues.InitDriverSession;
            // Maximize windows
            try
            {
                BaseValues.DriverSession.Manage().Window.Maximize();
            }
            catch (WebDriverException) { Console.WriteLine("This browser already set maximize."); }
            #region "Setting global wait => Do not using implicitwait => using fluent wait"
            // Set the timeout session of driver for global
            //BaseValues.DriverSession.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);
            #endregion

            // Setting the driver will wait 120s when invoke browser then navigate to URL
            int timeout = 60;
            try
            {
                int temp = int.Parse(ConfigurationManager.AppSettings[BaseConstants.PageloadTimeouts]);
                if (timeout != temp) timeout = temp;
            }
            catch (Exception e)
            {
                autoLogin = false;
                Console.WriteLine("The information about 'PageloadTimeouts' which user input is not correct." + e.Message);
            }

            BaseValues.DriverSession.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(timeout);

            // Log in automatically         
            try
            {
                // Navigate to the URL
                BaseValues.DriverSession.Url = URL;
                // Log in
                if (BaseValues.IsAutoLogin)
                    CommonFuncs.LoginToOnlineOrdering(userName, password);
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException(string.Format("The response time when the user sign in and load completed dashboard page is over {0}s.", timeout));
            }
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            if (status == TestStatus.Failed && ExtentReportsHelper.GetTest() == null)
            {
                ExtentReportsHelper.CreateTest(TestContext.CurrentContext.Test.Name);
                ExtentReportsHelper.LogFail(null, "Test Fail.");
            }
            BaseValues.ExtentReports.Flush();
            // Dispose driver
            BaseValues.DriverSession.Dispose();
            Process.Start(BaseValues.PathReportFile);
        }

        private void CleanUpAllDriver()
        {
            if (BaseValues.BrowserTarget.Equals(BrowserType.Chrome.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                KillProcessAndChildren("chromedriver.exe");
            }
            else if (BaseValues.BrowserTarget.Equals(BrowserType.Firefox.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                KillProcessAndChildren("geckodriver.exe");
            }
            else if (BaseValues.BrowserTarget.Equals(BrowserType.InternetExplorer.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                // For IE
                KillProcessAndChildren("IEDriverServer.exe");
            }
        }

        private void KillProcessAndChildren(string p_name)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher
              ("Select * From Win32_Process Where Name = '" + p_name + "'");

            ManagementObjectCollection moc = searcher.Get();
            foreach (ManagementObject mo in moc)
            {
                try
                {
                    KillProcessAndChildren(Convert.ToInt32(mo["ProcessID"]));
                }
                catch (ArgumentException)
                {
                    break;
                }
            }

        }

        private void KillProcessAndChildren(int pid)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher
             ("Select * From Win32_Process Where ParentProcessID=" + pid);
            ManagementObjectCollection moc = searcher.Get();
            foreach (ManagementObject mo in moc)
            {

                try
                {
                    KillProcessAndChildren(Convert.ToInt32(mo["ProcessID"]));
                }
                catch
                {
                    break;
                }
            }

            try
            {
                Process proc = Process.GetProcessById(pid);

                proc.Kill();
            }
            catch (ArgumentException)
            {
                // Process already exited.
            }
        }

    }
}
