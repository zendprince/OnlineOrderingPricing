using NUnit.Framework;
using OnlineOrdering.Common.BaseClass;
using OnlineOrdering.Common.Pages;
using OnlineOrdering.Testing.Pages.Login;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.IO;
using System.Reflection;

namespace OnlineOrdering.Testing.Script.Login
{
    [TestFixture]
    public class Login : BaseTestScript
    {
        // Setup
        readonly string pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\FileNameExcel.xlsx";

        // Define the page which you test
        private LoginPage _loginPage;

        // Set up and initial this page
        [SetUp]
        public void SetupTest()
        {
            _loginPage = new LoginPage(pathExcelFile);
        }

        // Test case
        [Test]
        [Category("Login1"), Order(1)]
        public void Test1()
        {
            //Login 
            FirefoxOptions ffOptions = new FirefoxOptions();
            ffOptions.BrowserExecutableLocation = @"C:\\Users\\hluong\\AppData\\Local\\Mozilla Firefox\\firefox.exe";

            // Step #1: Launch a new Firefox browser
            FirefoxDriver driver = new FirefoxDriver(ffOptions);
            ////---------------------------------------------



            driver.Url = "http://dvronrwprd01/OPPAv2/Account/Login";

            IWebElement label = driver.FindElement(By.Id("User"));
            label.Click();
            // =>
            _loginPage.UserName_txt.CoordinateClick();




            driver.FindElement(By.Id("User")).SendKeys("hluong");
            //
            _loginPage.LoginWithUser("");

            _loginPage.UserName_txt.SetText("");



            driver.FindElement(By.Id("Password")).SendKeys("Hung123456");
            driver.FindElement(By.Id("login")).Click();

            // Search 
            driver.FindElement(By.Id("productFindInput")).SendKeys("A12A187DNB");
            driver.FindElement(By.Id("productFindBtn")).Click();

            // Verify 
            var each = driver.FindElement(By.XPath("//*[@id='prcTable']/tbody/tr[1]/td[1]"));
            Assert.AreEqual(each.Text, "Each");

            var var1 = driver.FindElement(By.XPath("//*[@id='prcTable']/tbody/tr[1]/td[2]"));
            Assert.AreEqual(var1.Text, "1");

            Console.WriteLine("Test is Passed");

        }





        // Tear down
        // After rune all test
        [TearDown]
        public void TearDownTest()
        {

        }
    }
}
